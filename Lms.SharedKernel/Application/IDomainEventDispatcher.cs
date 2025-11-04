using Lms.SharedKernel.Domain.Abstractions;

namespace Lms.SharedKernel.Application
{
    
    public interface IDomainEventDispatcher
    {
        Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default);
    }
}
