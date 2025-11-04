namespace Lms.CourseManagement.Domain.Services
{
    public interface ILessonTagService
    {
        Task RemoveTagFromLessonAsync(Guid lessonId, Guid lessonTagId, CancellationToken cancellationToken);
    }
}
