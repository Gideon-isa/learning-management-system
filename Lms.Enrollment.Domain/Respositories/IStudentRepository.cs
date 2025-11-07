using Lms.Enrollment.Domain.Entities;

namespace Lms.Enrollment.Domain.Respositories
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<Student>> GetStudentsByIds(List<Guid> studentIds, CancellationToken cancellationToken);
        Task<List<Student>> GetAllStudents(CancellationToken cancellationToken);
        Task<bool> DoesStudentExistAsync(Guid id, CancellationToken cancellationToken);
        Task CreateAsync(Student student, CancellationToken cancellationToken);
    }
}
