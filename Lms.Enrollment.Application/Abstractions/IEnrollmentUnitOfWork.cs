namespace Lms.Enrollment.Application.Abstractions
{
    public interface IEnrollmentUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
