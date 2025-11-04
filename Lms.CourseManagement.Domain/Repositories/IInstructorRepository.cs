using Lms.CourseManagement.Domain.Entities;

namespace Lms.CourseManagement.Domain.Repositories
{
    public interface IInstructorRepository
    {
        Task<bool> AddInstructorAsync(Instructor instructor, CancellationToken cancellationToken);
    }
}
