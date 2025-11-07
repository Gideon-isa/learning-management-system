using Lms.Enrollment.Domain.Entities;
using Lms.Enrollment.Domain.Respositories;
using Lms.Enrollment.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Lms.Enrollment.Infrastructure.Persistence
{
    public class CourseEnrollmentRepository : ICourseEnrollmentRespository
    {
        private readonly EnrollmentDbContext _dbContext;

        public CourseEnrollmentRepository(EnrollmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CourseEnrollment> CreateCourseEnrollment(CourseEnrollment enrollment, CancellationToken cancellationToken)
        {
            await _dbContext.CourseEnrollments.AddAsync(enrollment, cancellationToken);
            return enrollment;

        }

        public Task DeleteCourseEnrollmentAsync(CourseEnrollment enrollment, CancellationToken cancellationToken)
        {
            _dbContext.CourseEnrollments.Remove(enrollment);
            return Task.CompletedTask;    
        }

        public async Task<CourseEnrollment?> GetCourseEnrollmentByCourseId(Guid courseId, CancellationToken cancellationToken)
        {
            return await _dbContext.CourseEnrollments.FirstOrDefaultAsync(c => c.CourseId == courseId);
        }

        public async Task<CourseEnrollment?> GetCourseEnrollmentById(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.CourseEnrollments.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        }

        public async Task<List<CourseEnrollment>> GetCourseEnrollmentsByCourseId(Guid courseId, CancellationToken cancellationToken)
        {
            return await _dbContext.CourseEnrollments.Where(c => c.CourseId ==  courseId).ToListAsync(cancellationToken);
        }

        public Task UpdateCourseEnrollmentAsync(CourseEnrollment courseEnrollment, CancellationToken cancellationToken)
        {
            _dbContext.CourseEnrollments.Update(courseEnrollment);
            return Task.CompletedTask;
        }

    }
}
