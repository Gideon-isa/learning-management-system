using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Repositories;
using Lms.CourseManagement.Infrastructure.DbContex;

namespace Lms.CourseManagement.Infrastructure.Persistence
{
    public class InstructorRepository : IInstructorRepository
    {
        private readonly CourseManagementDbContext _dbContext;

        public InstructorRepository(CourseManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddInstructorAsync(Instructor instructor, CancellationToken cancellationToken)
        {
            await _dbContext.Instructors.AddAsync(instructor);
        }
    }
}
