using Lms.ContentDelivery.Domain.Entities;

namespace Lms.ContentDelivery.Domain.Repositories
{
    public interface ICourseContentRepository
    {
        Task<CourseContent> AddAsync(CourseContent courseContent, CancellationToken cancellationToken);
        Task<CourseContent?> GetCourseContentByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
