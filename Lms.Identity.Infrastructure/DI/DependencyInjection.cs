using Lms.Identity.Application.Abstractions;
using Lms.Identity.Application.Features.Identity.Users;
using Lms.Identity.Infrastructure.Context;
using Lms.Identity.Infrastructure.Mapping;
using Lms.Identity.Infrastructure.Persistence;
using Lms.Identity.Infrastructure.Services;
using Lms.Identity.Infrastructure.Services.Outbox;
using Lms.Shared.IntegrationEvents.Integration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Identity.Infrastructure.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIdentityInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {

            // Registering Mapster config
            MappingConfig.RegisterConfig();

            return services.AddDbContext<UserIdentityDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            })
            .AddDbContext<IdentitySupportDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            })
            .AddTransient<ApplicationDbSeeder>()
            .AddPermissions()
            .AddDatabaseSeeder()
            .AddIdentityService()
            .AddOptions(config)
            .AddJwtAuthentication(config)
            .AddHostedService<IdentityOutboxProcessor>()
            .AddScoped<MediatRIntegrationEventPublisher>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>()
            .AddScoped<IIdentityIntegrationEventPublisher, IdentityOutboxService>()
            .AddScoped<IStudentCodeGenerator, StudentCodeGenerator>();

            //.AddCustomMediator(typeof(DependencyInjection).Assembly);


        }


        public static async Task AddDatabaseInintializerAsync(this WebApplication app, CancellationToken cancellation = default)
        {
            using var scope = app.Services.CreateScope();
            var dbSeeder = scope.ServiceProvider.GetRequiredService<ApplicationDbSeeder>();
            await dbSeeder.InitializeDatabaseAsync(cancellation);
        }

    }

}
