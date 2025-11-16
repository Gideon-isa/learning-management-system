using Lms.ContentDelivery.Application.Models;

namespace Lms.ContentDelivery.Application.Abstractions
{
    public interface ICourseVideoRespository
    {
        Task<VideoMetadataModel?> GetVideoMetadataByIdAsync(Guid videoId, CancellationToken cancellationToken);
    }
}
