using Lms.ContentDelivery.Domain.Entities;

namespace Lms.ContentDelivery.Domain.Repositories
{
    public interface IStudentAccessRepository
    {
        Task<StudentAccess> CreateStudentAccess(StudentAccess studentAccess, CancellationToken cancellationToken);
        Task<StudentAccess> GetStudentAccess(string studentCode, CancellationToken cancellationToken);

    }
}
