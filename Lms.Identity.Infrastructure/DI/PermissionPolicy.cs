using Lms.Identity.Infrastructure.Identity.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Identity.Infrastructure.DI
{
    internal static class PermissionPolicy
    {
        internal static IServiceCollection AddPermissions(this IServiceCollection services)
        {
            return services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                           .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        }
    }
}
