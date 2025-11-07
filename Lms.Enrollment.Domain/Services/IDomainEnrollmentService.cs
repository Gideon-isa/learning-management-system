using Lms.Enrollment.Domain.Entities;
using Lms.Enrollment.Domain.ValueObjects;

namespace Lms.Enrollment.Domain.Services
{
    public interface IDomainEnrollmentService
    {
        Task<EnrollmentResult> EnrollStudents(CourseEnrollment courseEnrollment, IEnumerable<Guid> studentIds, Guid courseId, CancellationToken cancellationToken);
    }
}
