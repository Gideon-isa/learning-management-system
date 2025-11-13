using Lms.CourseDelivery.Domain.Entities;

namespace Lms.CourseDelivery.Domain.Repositories
{
    public interface IStudentAccessRepository
    {
        Task<StudentAccess> CreateStudentAccess(StudentAccess studentAccess, CancellationToken cancellationToken);
        Task<StudentAccess> GetStudentAccess(string studentCode, CancellationToken cancellationToken);

    }
}
