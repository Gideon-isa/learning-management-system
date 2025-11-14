using Lms.ContentDelivery.Application.Features.Queries.StudentCourse;
using Lms.ContentDelivery.Domain.Entities;
using Mapster;

namespace Lms.ContentDelivery.Application.Mapping
{
    public static class CourseContentMappingConfig
    {
        public static void RegisterCourseContentConfig()
        {
            TypeAdapterConfig<CourseContent, CourseContentDto>.NewConfig()
                .Map(dest => dest.CourseId, src => src.CourseId)
                .Map(dest => dest.CourseTitle, src => src.CourseTitle)
                .Map(dest => dest.CourseCode, src => src.CourseCode);

            TypeAdapterConfig<StudentAccess, StudentAccessCourseResponse>.NewConfig()
                .Map(dest => dest.StudentCode, src => src.StudentCode);
        }
    }
}
