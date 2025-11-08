
using Lms.Enrollment.Domain.Entities;

namespace Lms.Enrollment.Domain.Respositories
{
    public interface ICourseEnrollmentRespository
    {
        Task<CourseEnrollment> CreateCourseEnrollment(CourseEnrollment enrollment, CancellationToken cancellationToken);
        Task<CourseEnrollment?> GetCourseEnrollmentById(Guid id, CancellationToken cancellationToken);
        Task<List<CourseEnrollment>> GetAllAsync(CancellationToken cancellationToken);
        Task<CourseEnrollment?> GetCourseEnrollmentByCourseId(Guid courseId, CancellationToken cancellationToken);
        Task DeleteCourseEnrollmentAsync(CourseEnrollment courseEnrollment, CancellationToken cancellationToken);
        Task UpdateCourseEnrollmentAsync(CourseEnrollment courseEnrollment, CancellationToken cancellationToken);
    }
}
 