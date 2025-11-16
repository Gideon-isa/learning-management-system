using Lms.ContentDelivery.Application.Abstractions;
using Lms.ContentDelivery.Application.Models;
using Lms.ContentDelivery.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Lms.ContentDelivery.Infrastructure.Persistence
{
    public class CourseVideoRepository : ICourseVideoRespository
    {
        private readonly CourseContentSupportDbContext _context;

        public CourseVideoRepository(CourseContentSupportDbContext context)
        {
            _context = context;
        }

        public async Task<VideoMetadataModel?> GetVideoMetadataByIdAsync(Guid videoId, CancellationToken cancellationToken)
        {
            return await _context.VideoMetadata.FirstOrDefaultAsync(v => v.Id == videoId, cancellationToken);
        }
    }
}
