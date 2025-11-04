using FluentValidation;
using System.Reflection;

namespace Lms.Api.DependencyInjections
{
    public static class MediatRService
    {
        public static IServiceCollection AddMediatetorService(this IServiceCollection services) 
        {
            // Registers Fluent Validation
            var assembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(assembly)
                    .AddMediatR(cfg =>
                    {
                        cfg.RegisterServicesFromAssembly(assembly);
                    });
            return services;
        }
    }
}
