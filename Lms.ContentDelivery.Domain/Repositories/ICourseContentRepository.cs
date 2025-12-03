using Lms.ContentDelivery.Domain.Entities;

namespace Lms.ContentDelivery.Domain.Repositories
{
    public interface ICourseContentRepository
    {
        Task<CourseContent> AddAsync(CourseContent courseContent, CancellationToken cancellationToken);
        Task<CourseContent?> GetCourseContentByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<CourseContent?> GetCourseContentByCourseIdAsync(Guid courseId, CancellationToken cancellationToken);
        Task<IEnumerable<CourseContent>> GetCourseContentsByCourseIdsAsync(List<Guid> courseIds, CancellationToken cancellationToken);

    }
}
