using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Repositories;
using Lms.CourseManagement.Infrastructure.DbContex;
using Microsoft.EntityFrameworkCore;

namespace Lms.CourseManagement.Infrastructure.Persistence
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseManagementDbContext _dbContext;

        public CourseRepository(CourseManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Course course, CancellationToken cancellationToken)
        {
            await _dbContext.Courses.AddAsync(course, cancellationToken);
        }

        public async Task<IEnumerable<Course>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Courses.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Course?> GetByIdAsync(Guid courseId, CancellationToken cancellationToken)
        {
            // Using Eager Loading to load related Modules and Lessons
            return await _dbContext.Courses
                .Include(c => c.Modules)
                .ThenInclude(m => m.Lessons)
                .FirstOrDefaultAsync(c => c.Id == courseId, cancellationToken);
        }

        public Task UpdateAsync(Course course, CancellationToken cancellationToken)
        {
            _dbContext.Courses.Update(course);
            return Task.CompletedTask;
        }
    }
}
