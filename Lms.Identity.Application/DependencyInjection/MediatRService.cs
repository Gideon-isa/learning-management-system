using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lms.Identity.Application.DependencyInjection
{
    public static class MediatRService
    {
        public static IServiceCollection AddMediatetorService(this IServiceCollection services) 
        {
            // Registers Fluent Validation
            var assembly = Assembly.GetExecutingAssembly();
            services.AddValidatorsFromAssembly(assembly);
                    //.AddMediatR(cfg =>
                    //{
                    //    cfg.RegisterServicesFromAssembly(assembly);
                    //});
            return services;
        }
    }
}
