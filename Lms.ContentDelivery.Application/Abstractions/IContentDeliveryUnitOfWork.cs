namespace Lms.ContentDelivery.Application.Abstractions
{
    public interface IContentDeliveryUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellation = default);
    }
}
