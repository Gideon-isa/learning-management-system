using Lms.Identity.Application.Features.Identity;
using Lms.Shared.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Identity.Infrastructure.DI
{
    internal static class OptionService
    {
        public static IServiceCollection AddOptions(this IServiceCollection services, IConfiguration config)
        {
            return services.OptionsConfigure<JwtOptions>(config);

        }
    }
}
