using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Domain.Events;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.Application.EventDispatcher;
using Lms.Shared.IntegrationEvents.courseManagement;

namespace Lms.CourseManagement.Application.EventHandlers
{
    public class DeletePublishedCourseModuleEventHandler : ICustomNotificationHandler<DomainEventNotification<DeletedPublishedCourseModuleEvent>>
    {
        private readonly ICourseIntegrationEventPublisher _publisher;

        public DeletePublishedCourseModuleEventHandler(ICourseIntegrationEventPublisher publisher)
        {
            _publisher = publisher;
        }

        public async Task Handle(DomainEventNotification<DeletedPublishedCourseModuleEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            var integrationEvent = new DeletePublishedCourseModuleIntegrationEvent(domainEvent.CourseId, domainEvent.ModuleId);
            await _publisher.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}
