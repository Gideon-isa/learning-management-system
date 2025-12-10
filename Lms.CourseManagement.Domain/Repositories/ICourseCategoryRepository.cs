using Lms.CourseManagement.Domain.Entities;

namespace Lms.CourseManagement.Domain.Repositories
{
    public interface ICourseCategoryRepository
    {
        Task AddAsync(CourseCategory courseCategory, CancellationToken cancellationToken);
        Task<CourseCategory?> GetAsync(Guid courseCategoryId, CancellationToken cancellationToken);
        Task<IEnumerable<CourseCategory>> GetAllAsync(CancellationToken cancellationToken);
    }
}
