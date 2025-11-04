using Lms.CourseManagement.Application.Services;
using Lms.SharedKernel.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.CourseManagement.Application.DI
{
    public static class DomainEventDispatcherService
    {
        //public static IServiceCollection AddDomainEventDispatcher(this IServiceCollection services)
        //{
        //    services.AddMediatR(config =>
        //    {
        //        config.RegisterServicesFromAssembly(typeof(DomainEventDispatcher).Assembly);
        //    });
        //    services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        //    return services;
        //}
    }
}
