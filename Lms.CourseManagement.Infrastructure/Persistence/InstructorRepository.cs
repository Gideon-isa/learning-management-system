using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Repositories;

namespace Lms.CourseManagement.Infrastructure.Persistence
{
    public class InstructorRepository : IInstructorRepository
    {
        public Task<bool> AddInstructorAsync(Instructor instructor, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
