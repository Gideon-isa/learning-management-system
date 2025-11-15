using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Models;
using Lms.CourseManagement.Infrastructure.DbContex;

namespace Lms.CourseManagement.Infrastructure.Persistence
{
    public class VideoMetadateRepository : IVideoMetadataRepository
    {
        private readonly CourseSupportDbContext _context;

        public VideoMetadateRepository(CourseSupportDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(VideoMetadata videoMetadata, CancellationToken cancellationToken)
        {
            await _context.CourseManagementVideoMetadata.AddAsync(videoMetadata, cancellationToken);
            _context.SaveChanges();
        }

        public Task<VideoMetadata?> GetByIdAsync(Guid videoId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
