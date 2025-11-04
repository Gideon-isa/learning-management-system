using Lms.Identity.Application.Features.Identity.Tokens.Queries;

namespace Lms.Identity.Application.Features.Identity.Tokens
{
    public interface ITokenService
    {
        Task<TokenResponse> LoginAsync(GetTokenQuery request);
        Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
    }
}
