using Lms.Enrollment.Domain.Entities;
using Lms.Enrollment.Domain.Respositories;
using Lms.Enrollment.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Lms.Enrollment.Infrastructure.Persistence
{
    public class AvailableCourseRepository : IAvailableCoursesRepository
    {
        private EnrollmentDbContext _dbContext;

        public AvailableCourseRepository(EnrollmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCourseAsync(AvailableCourse course, CancellationToken cancellationToken)
        {
            await _dbContext.AvailableCourses.AddAsync(course, cancellationToken);
        }

        public async Task<AvailableCourse?> GetAvailableCourseByIdAsync(Guid courseId, CancellationToken cancellationToken)
        {
            return await _dbContext.AvailableCourses.FirstOrDefaultAsync((c => c.CourseId == courseId), cancellationToken);
        }

        public async Task<List<AvailableCourse>> GetAvailableCoursesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.AvailableCourses.ToListAsync();
        }
    }
}
