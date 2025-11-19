using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Course.DTO;
using Lms.CourseManagement.Application.Features.Course.Queries.GetAllCourse;
using Lms.CourseManagement.Application.Features.CourseFeatures.Mappings;
using Lms.CourseManagement.Application.Features.CourseFeatures.Sorting;
using Lms.CourseManagement.Application.Services;
using Lms.Shared.Abstractions.FileStorage;
using Lms.Shared.Application.Sorting;
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
            services.AddTransient<SortMappingProvider>();
            services.AddTransient<ICourseSortMappingDefinition, CourseMappingDefinition<GetCoursesQuery, Domain.Entities.Course>>(_ => CourseMapping.Sortmapping);

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
