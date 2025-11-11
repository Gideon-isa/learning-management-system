using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Application.EventHandlers;
using Lms.Enrollment.Application.Mapping;
using Lms.Enrollment.Domain.Services;
using Lms.Shared.Application;
using Lms.Shared.Application.CustomMediator;
using Lms.Shared.Application.CustomMediator.Interfaces.Mediator;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lms.Enrollment.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEnrollmentApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // 1️⃣ Register all ICustomNotificationHandlers in this assembly
            //services.Scan(scan => scan
            //    .FromAssemblies(assembly)
            //    .AddClasses(classes => classes.AssignableTo(typeof(CoursePublishedIntegrationEventHandler)))
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());

            // 2️⃣ Register the MiniMediator and supporting types (if not already in Shared.Application)
            services.AddScoped<ICustomMediator, MiniMediator>();
            
            //services.AddScoped<IDomainEnrollmentService, EnrollmentService>();
            //services.AddCustomMediator(typeof(CoursePublishedIntegrationEventHandler).Assembly);

            // Registering the Mapster
            EnrollmentMappingConfig.RegisterEnrollmentConfig();

            return services;
        }

    }
}
