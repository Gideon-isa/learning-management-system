using Lms.Identity.Infrastructure.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Lms.Identity.Infrastructure.Identity.Auth
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var permission = context.User.Claims
                .Where(claim => claim.Type == ClaimConstants.Permission && claim.Value == requirement.Permission);

            if (permission.Any())
            {
                context.Succeed(requirement);
                await Task.CompletedTask;
            }
        }
    }
}
