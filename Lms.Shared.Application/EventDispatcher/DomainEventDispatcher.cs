using Lms.Shared.Application.CustomMediator.Interfaces.Mediator;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.SharedKernel.Application;
using Lms.SharedKernel.Domain.Abstractions;
using MediatR;

namespace Lms.Shared.Application.EventDispatcher
{
    /// <summary>
    /// Dispatches kernel domain events by wrapping them into DomainEventNotification<T>
    /// and publishing via MediatR.
    /// </summary>
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly ICustomMediator _mediator;

        public DomainEventDispatcher(ICustomMediator mediator) => _mediator = mediator;

        public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());
                var notification = (ICustomNotification)Activator.CreateInstance(notificationType, domainEvent)!;
                await _mediator.Publish(notification, cancellationToken);  
            }
        }
    }
}
