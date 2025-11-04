using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.SharedKernel.Domain.Abstractions;

namespace Lms.Shared.Application.EventDispatcher
{
    public sealed record DomainEventNotification<TEvent>(TEvent DomainEvent) : ICustomNotification where TEvent : IDomainEvent{}
}
