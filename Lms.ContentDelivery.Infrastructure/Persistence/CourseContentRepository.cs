using Lms.ContentDelivery.Domain.Entities;
using Lms.ContentDelivery.Domain.Repositories;
using Lms.ContentDelivery.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Lms.ContentDelivery.Infrastructure.Persistence
{
    public class CourseContentRepository : ICourseContentRepository
    {
        private readonly CourseContentDbContext _context;

        public CourseContentRepository(CourseContentDbContext context)
        {
            _context = context;
        }

        public async Task<CourseContent> AddAsync(CourseContent courseContent, CancellationToken cancellationToken)
        {
            await _context.CourseContentDelivery.AddAsync(courseContent, cancellationToken);
            return courseContent;
        }

        public async Task<CourseContent?> GetCourseContentByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.CourseContentDelivery
                .Include(c => c.Modules)
                .ThenInclude(c => c.Lessons)
                .ThenInclude(c => c.Videos).FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }
    }
}
