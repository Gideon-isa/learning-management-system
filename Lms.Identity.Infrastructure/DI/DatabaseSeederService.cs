using Lms.Identity.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Identity.Infrastructure.DI
{
    internal static class DatabaseSeederService
    {
        public static IServiceCollection AddDatabaseSeeder(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDbSeeder>();
            return services;
        }

    }
}
