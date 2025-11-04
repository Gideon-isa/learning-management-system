using Lms.CourseManagement.Domain.Entities;

namespace Lms.CourseManagement.Domain.Repositories
{
    public interface ILessonTagRespository
    {
        Task AddAsync(LessonTag lessonTag, CancellationToken cancellationToken);
        Task<List<LessonTag>> GetAllAsync(CancellationToken cancellationToken);
        Task<LessonTag?> GetByIdAsync(Guid tagId, CancellationToken cancellationToken);
        Task<List<LessonTag>> GetByIdsAsync(IEnumerable<Guid> tagIds, CancellationToken cancellationToken);
        Task DeleteAsync(Guid tagId, CancellationToken cancellationToken);
    }
}
