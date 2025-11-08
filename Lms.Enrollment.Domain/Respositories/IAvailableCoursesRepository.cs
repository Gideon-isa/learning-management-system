using Lms.Enrollment.Domain.Entities;

namespace Lms.Enrollment.Domain.Respositories
{
    public interface IAvailableCoursesRepository
    {
        Task AddCourseAsync(AvailableCourse course, CancellationToken cancellationToken);
        Task<List<AvailableCourse>> GetAvailableCoursesAsync(CancellationToken cancellationToken);
        Task<AvailableCourse?> GetAvailableCourseByIdAsync(Guid courseId, CancellationToken cancellationToken);

    }
}
