using Lms.Identity.Application.Abstractions;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.Application.EventDispatcher;
using Lms.Shared.IntegrationEvents.Identity;

namespace Lms.Identity.Application.Events
{
    public class ApprovedInstructorEventHandler : ICustomNotificationHandler<DomainEventNotification<ApprovedInstructorEvent>>
    {
        private IIdentityIntegrationEventPublisher _identityIntegrationEventPublisher;

        public ApprovedInstructorEventHandler(IIdentityIntegrationEventPublisher identityIntegrationEventPublisher)
        {
            _identityIntegrationEventPublisher = identityIntegrationEventPublisher;
        }

        public async Task Handle(DomainEventNotification<ApprovedInstructorEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            // using the CreateInstructorPublishedIntegrationEvent as content of the notification
            var integrationEvent = new CreateInstructorPublishedIntegrationEvent(
                domainEvent.Id, 
                domainEvent.FirstName, 
                domainEvent.LastName, 
                domainEvent.ProfileImageUrl,
                domainEvent.DisplayName,
                domainEvent.Bio, 
                domainEvent.Department, 
                domainEvent.Institution, 
                domainEvent.StaffId);

            await _identityIntegrationEventPublisher.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}
