using Lms.Identity.Application.Exceptions;
using Lms.Identity.Application.Features.Identity;
using Lms.Identity.Application.Features.Identity.Tokens;
using Lms.Identity.Application.Features.Identity.Tokens.Queries;
using Lms.Identity.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Lms.Identity.Infrastructure.Identity.Tokens
{
    public class TokenService(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        JwtOptions jwtOptions)
        : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
        private readonly JwtOptions _jwtOptions = jwtOptions;

        public async Task<TokenResponse> LoginAsync(GetTokenQuery request)
        {
            #region Validation

            var userInDb = await _userManager.FindByNameAsync(request.TokenRequest.Username) ?? throw new UnauthorizedException(["Authentication not successful"]);

            if (await _userManager.CheckPasswordAsync(userInDb, request.TokenRequest.Password) is false)
            {
                throw new UnauthorizedException(["Invalid username or password."]);
            }

            if (userInDb.IsActive is false)
            {
                throw new UnauthorizedException(["User Not Active. Contact Administration"]);
            }

            #endregion
            // Generate jwt
            return await GenerateTokenAndUpdateUserAsync(userInDb);

        }

        public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var userPrincipal = GetClaimsPrincipalFromExpiringToken(refreshTokenRequest.CurrentJwt);
            var userEmail = userPrincipal.GetEmail();
            var userInDb = await _userManager.FindByEmailAsync(userEmail) ?? throw new UnauthorizedException(["Authentication failed"]);

            if (userInDb.RefreshToken != refreshTokenRequest.CurrentRefreshToken || userInDb.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                throw new UnauthorizedException(["Invalid token"]);
            }
            return await GenerateTokenAndUpdateUserAsync(userInDb);
        }

        private ClaimsPrincipal GetClaimsPrincipalFromExpiringToken(string expiringToken)
        {
            var tokenValidationParams = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
                RoleClaimType = ClaimTypes.Role,
                ValidateLifetime = true, // -> set to false if you want to validate expired jwt to issue an new jwt token
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret))
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(expiringToken, tokenValidationParams, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new UnauthorizedException(["Invalid token provided. Failed to generate new token."]);
            }
            return principal;
        }

        private async Task<TokenResponse> GenerateTokenAndUpdateUserAsync(ApplicationUser user)
        {
            // Generate Jwt
            var newJwt = await GenerateToken(user);

             // Refresh Token
            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpiryTimeInDays);

            await _userManager.UpdateAsync(user);

            return new TokenResponse()
            {
                Jwt = newJwt,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryDate = user.RefreshTokenExpiryTime
            };
        }

        private async Task<string> GenerateToken(ApplicationUser user)
        {
            // generate encrypted token
            return GenerateEncryptedToken(GenerateSigningCredentials(), await GetUserClaims(user));
        }

        private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpiryTimeInMinutes),
                signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private SigningCredentials GenerateSigningCredentials()
        {
            byte[] secret = Encoding.UTF8.GetBytes(_jwtOptions.Secret);
            return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
        }

        private async Task<IEnumerable<Claim>> GetUserClaims(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();
            var permissionClaims = new List<Claim>();

            foreach (var userRole in userRoles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, userRole));
                var currentRole = await _roleManager.FindByNameAsync(userRole);
                var allPermissionForCurrentRole = await _roleManager.GetClaimsAsync(currentRole);
                permissionClaims.AddRange(allPermissionForCurrentRole);
            }

            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Email, user.Email ?? string.Empty),
                new (ClaimTypes.Name, user.FirstName ?? string.Empty),
                new (ClaimTypes.Surname, user.LastName ?? string.Empty),
                new (ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
            }
            .Union(roleClaims)
            .Union(userClaims)
            .Union(permissionClaims);

            return claims;
        }

        private string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
