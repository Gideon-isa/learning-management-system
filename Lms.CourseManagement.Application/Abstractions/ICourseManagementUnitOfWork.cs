namespace Lms.CourseManagement.Application.Abstractions
{
    public interface ICourseManagementUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
