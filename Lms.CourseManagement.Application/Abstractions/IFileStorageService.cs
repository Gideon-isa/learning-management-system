using Lms.CourseManagement.Application.Features.Lesson.Command;
using Lms.CourseManagement.Domain.ValueObjects;

namespace Lms.CourseManagement.Application.Abstractions
{
    public interface IFileStorageService
    {
        Task<string> UploadVideoFileAsync(CreateLessonCommand command, CancellationToken cancellationToken);
        Task<IEnumerable<LessonImage>> UploadImageFileAsync(CreateLessonCommand command, CancellationToken cancellationToken);
        Task<string> GetFolderPath(CreateLessonCommand command, CancellationToken cancellationToken);
        Task<string> GetFilePath(string contentFileName, string folder, CancellationToken cancellationToken);
        
    }
}
