using Lms.Shared.Application;
using System.Reflection;

namespace Lms.Api.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
           

            return services
                .AddOpenApiDocumentation(configuration);
                
        }
    }
}
