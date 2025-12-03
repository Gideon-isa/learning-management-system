using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Application.EventHandlers;
using Lms.Enrollment.Application.Mapping;
using Lms.Enrollment.Domain.Services;
using Lms.Shared.Abstractions.Interfaces.Mediator;
using Lms.Shared.Application;
using Lms.Shared.Application.CustomMediator;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lms.Enrollment.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddEnrollmentApplication(this IServiceCollection services)
        {
            // Register the MiniMediator and supporting types (if not already in Shared.Application)
            services.AddScoped<ICustomMediator, MiniMediator>();

            // Registering the Mapster
            EnrollmentMappingConfig.RegisterEnrollmentConfig();

            return services;
        }

    }
}
