using Lms.ContentDelivery.Domain.Entities;

namespace Lms.ContentDelivery.Domain.Repositories
{
    public interface IStudentAccessRepository
    {
        Task<StudentAccess> CreateStudentAccessAsync(StudentAccess studentAccess, CancellationToken cancellationToken);
        Task<StudentAccess?> GetStudentAccessAsync(string studentCode, Guid courseId, CancellationToken cancellationToken);
        Task<IEnumerable<StudentAccess>> GetStudentAllAccessAsync(string studentCode, CancellationToken cancellationToken);
    }
}
