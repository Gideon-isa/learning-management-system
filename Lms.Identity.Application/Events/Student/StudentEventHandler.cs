using Lms.Identity.Application.Abstractions;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.Application.EventDispatcher;
using Lms.Shared.IntegrationEvents.Identity;

namespace Lms.Identity.Application.Events.Student
{
    public class StudentEventHandler : ICustomNotificationHandler<DomainEventNotification<StudentEvent>>
    {
        private IIdentityIntegrationEventPublisher _publisher;

        public StudentEventHandler(IIdentityIntegrationEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task Handle(DomainEventNotification<StudentEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            var integrationEvent = new CreateStudentPublishedIntegrationEvent(
                domainEvent.Id,
                domainEvent.FirstName,
                domainEvent.LastName,
                domainEvent.UserName);

            // Saving to the Outbox Message
            await _publisher.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}
