using Lms.Identity.Application.Features.Identity.Tokens;
using Lms.Identity.Infrastructure.Context;
using Lms.Identity.Infrastructure.Identity.Models;
using Lms.Identity.Infrastructure.Identity.Tokens;
using Lms.Identity.Infrastructure.Persistence;
using Lms.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Identity.Infrastructure.DI
{
    internal static class IdentityService
    {
        internal static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            return services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;

                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<UserIdentityDbContext>()
            .AddDefaultTokenProviders()
            .Services
            .AddTransient<ITokenService, TokenService>();
            
        }
    }
}
