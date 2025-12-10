using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Features.Course.Queries.GetAllCourse;
using Lms.CourseManagement.Application.Features.CourseFeatures.Mappings;
using Lms.CourseManagement.Application.Features.CourseFeatures.Sorting;
using Lms.CourseManagement.Application.Features.LessonFeatures.Mappings;
using Lms.CourseManagement.Application.Services;
using Lms.Shared.Abstractions.FileStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.CourseManagement.Application.DI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCourseManagementApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
            services.AddScoped<IMimeTypeService, MimeTypeService>();
            services.AddTransient<SortMappingProvider>();
            services.AddTransient<ICourseSortMappingDefinition, CourseMappingDefinition<GetCoursesQuery, Domain.Entities.Course>>(_ => CourseMapping.Sortmapping);
            CourseMapsterConfig.RegisterCourseMappings();
            LessonMapsterConfig.RegisterLessonMappings();
            return services;
        }

    }
}
