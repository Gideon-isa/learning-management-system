using Lms.CourseManagement.Application.Features.CourseFeatures.Queries.CourseModule;
using Lms.CourseManagement.Domain.Entities;
using Mapster;

namespace Lms.CourseManagement.Application.Features.CourseFeatures.Mappings
{
    public static class CourseMapsterConfig
    {
        public static void RegisterCourseMappings()
        {
            TypeAdapterConfig<Domain.Entities.Course, CourseModuleDto>
                .NewConfig()
                .Map(dest => dest.Modules, src => src.Modules.Adapt<List<CourseModuleDetailDto>>());
        }
    }
}
