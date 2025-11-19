using Lms.CourseManagement.Domain.Entities;

namespace Lms.CourseManagement.Domain.Repositories
{
    /// <summary>
    /// This should be used for Read Only and for write
    /// </summary>
    public interface ILessonRespositoy
    {
        Task<Content?> GetByIdAsync(Guid lessonId, CancellationToken cancellationToken);
    }
}
