using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Domain.Events;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.Application.EventDispatcher;
using Lms.Shared.IntegrationEvents.Enrollment;

namespace Lms.Enrollment.Application.EventHandlers
{
    public class StudentEnrollmentDomainEventHandler : ICustomNotificationHandler<DomainEventNotification<EnrollStudentEvent>>
    {
        private IEnrollmentIntegrationEventPublisher _enrollmentIntegrationEventPublisher;

        public StudentEnrollmentDomainEventHandler(IEnrollmentIntegrationEventPublisher enrollmentIntegrationEventPublisher)
        {
            _enrollmentIntegrationEventPublisher = enrollmentIntegrationEventPublisher;
        }

        public async Task Handle(DomainEventNotification<EnrollStudentEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;
            var integrationEvent = new EnrollmentPublishIntegrationEvent(domainEvent.StudentCode, domainEvent.courseId);

            await _enrollmentIntegrationEventPublisher.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}
