using Lms.Enrollment.Application.Features.CourseEnrollment;
using Lms.Enrollment.Domain.Entities;
using Mapster;

namespace Lms.Enrollment.Application.Mapping
{
    public static class EnrollmentMappingConfig
    {
        public static void RegisterEnrollmentConfig()
        {
            TypeAdapterConfig<CourseEnrollment, CourseEnrollmentResponse>.NewConfig()
                .Map(dest => dest.CourseId, src => src.CourseId)
                .Map(dest => dest.CourseTitle, src => src.CourseTitle)
                .Map(dest => dest.StudentEnrollments, src => src.StudentEnrollments);            

        }
    }
}
