using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Lms.CourseManagement.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCourseManagementApplicationServices(this IServiceCollection services)
        {
            // You can add application-specific services here in the future
            var assembly = Assembly.GetExecutingAssembly();

            //services.AddDomainEventDispatcher();
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
            services.AddScoped<IMimeTypeService, MimeTypeService>();

            //.AddCustomMediator(typeof(DomainEventDispatcherService).Assembly);

            //services.Scan(scan => scan
            //    .FromAssemblies(assembly)
            //    .AddClasses(classes => classes.AssignableTo(typeof(CoursePublishedDomainEventHandler)))
            //    .AsImplementedInterfaces()
            //    .WithScopedLifetime());


            return services;
        }

    }
}
