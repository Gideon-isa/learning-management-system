using Lms.CourseManagement.Application.Models;

namespace Lms.CourseManagement.Application.Abstractions
{
    public interface IVideoMetadataRepository
    {
        Task AddAsync(VideoMetadata videoMetadata, CancellationToken cancellationToken);
        Task<VideoMetadata?> GetByIdAsync(Guid videoId, CancellationToken cancellationToken);
    }
}
