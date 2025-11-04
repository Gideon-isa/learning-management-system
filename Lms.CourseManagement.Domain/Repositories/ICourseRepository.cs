using Lms.CourseManagement.Domain.Entities;

namespace Lms.CourseManagement.Domain.Repositories
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken);
        Task<Course?> GetByIdAsync(Guid courseId, CancellationToken cancellationToken);
        Task AddAsync(Course course, CancellationToken cancellationToken);
        Task UpdateAsync(Course course, CancellationToken cancellationToken);
    }
}
