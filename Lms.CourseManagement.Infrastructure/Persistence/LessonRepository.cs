using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Repositories;
using Lms.CourseManagement.Infrastructure.DbContex;
using Microsoft.EntityFrameworkCore;

namespace Lms.CourseManagement.Infrastructure.Persistence
{
    public class LessonRepository : ILessonRespositoy
    {
        private readonly CourseManagementDbContext _dbContext;

        public LessonRepository(CourseManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Lesson?> GetByIdAsync(Guid lessonId, CancellationToken cancellationToken)
        {
            return await _dbContext
                        .Lessons
                        .AsNoTracking()
                        .Where(l => l.Id == lessonId)
                        .Include(l => l.Videos)
                        .Include(l => l.Images)
                        .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
