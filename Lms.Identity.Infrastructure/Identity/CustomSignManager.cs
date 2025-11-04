using Lms.Identity.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lms.Identity.Infrastructure.Identity
{
    public class CustomSignManager : SignInManager<ApplicationUser>
    {
        public CustomSignManager(UserManager<ApplicationUser> userManager, 
            IHttpContextAccessor contextAccessor, 
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, 
            IOptions<IdentityOptions> optionsAccessor, 
            ILogger<SignInManager<ApplicationUser>> logger, 
            IAuthenticationSchemeProvider schemes, 
            IUserConfirmation<ApplicationUser> confirmation) 
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }
        public override async Task<bool> CanSignInAsync(ApplicationUser user)
        {
            // Block sign-in if the user is not active
            if(!user.IsActive)
            {
                return false;
            }

            return await base.CanSignInAsync(user);
        }
    }
}
