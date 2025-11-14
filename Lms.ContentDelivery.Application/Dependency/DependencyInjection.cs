using Lms.ContentDelivery.Application.Mapping;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.ContentDelivery.Application.Dependency
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCourseContentApplicationServices(this IServiceCollection services) 
        {

            CourseContentMappingConfig.RegisterCourseContentConfig();
            return services;
        }
    }
}
