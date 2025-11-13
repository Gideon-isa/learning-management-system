using Lms.ContentDelivery.Domain.Entities;
using Lms.ContentDelivery.Domain.Repositories;
using Lms.ContentDelivery.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Lms.ContentDelivery.Infrastructure.Persistence
{
    public class StudentAccessRepository : IStudentAccessRepository
    {
        private readonly CourseContentDbContext _courseContentDbContext;

        public StudentAccessRepository(CourseContentDbContext courseContentDbContext)
        {
            _courseContentDbContext = courseContentDbContext;
        }

        public async Task<StudentAccess> CreateStudentAccessAsync(StudentAccess studentAccess, CancellationToken cancellationToken)
        {
            await _courseContentDbContext.StudentAccesses.AddAsync(studentAccess, cancellationToken);
            return studentAccess;
        }

        public async Task<StudentAccess?> GetStudentAccessAsync(string studentCode, Guid courseId, CancellationToken cancellationToken)
        {
            return  await _courseContentDbContext.StudentAccesses.FirstOrDefaultAsync(st => st.StudentCode == studentCode && st.CourseId == courseId);
        }

        public async Task<IEnumerable<StudentAccess>> GetStudentAllAccessAsync(string studentCode, CancellationToken cancellationToken)
        {
            return await _courseContentDbContext.StudentAccesses.Where(st => st.StudentCode == studentCode).ToListAsync(cancellationToken);
        }
    }
}
