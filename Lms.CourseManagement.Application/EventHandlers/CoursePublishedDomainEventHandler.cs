using Lms.CourseManagement.Application.Abstractions;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.Application.EventDispatcher;
using Lms.Shared.IntegrationEvents.courseManagement;
using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Application.EventHandlers
{
    /// <summary>
    ///  This is for Integration Events
    /// </summary>
    public class CoursePublishedDomainEventHandler : ICustomNotificationHandler<DomainEventNotification<CoursePublishedEvent>>
    {
        private readonly ICourseIntegrationEventPublisher _publisher; // for saving into the Outbox table

        public CoursePublishedDomainEventHandler(ICourseIntegrationEventPublisher publisher)
        {
            _publisher = publisher;
        }
        public async Task Handle(DomainEventNotification<CoursePublishedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            // using the CoursePublishedIntegrationEvent as the content of the notification
            var integrationEvent = new CoursePublishedIntegrationEvent(
                domainEvent.CourseId, 
                domainEvent.CourseTitle, 
                domainEvent.InstructorId, 
                domainEvent.PublishedOn);

            // publish integration event to outbox table
            await _publisher.PublishAsync(integrationEvent, cancellationToken);
            
        }
    }
}
