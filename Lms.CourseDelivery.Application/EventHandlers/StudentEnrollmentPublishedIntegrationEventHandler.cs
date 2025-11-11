using Lms.Shared.Abstractions.Messaging;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.IntegrationEvents.Enrollment;

namespace Lms.CourseDelivery.Application.EventHandlers
{
    public class StudentEnrollmentPublishedIntegrationEventHandler : ICustomNotificationHandler<IntegrationEventNotification<EnrollmentPublishIntegrationEvent>>
    {

        public Task Handle(IntegrationEventNotification<EnrollmentPublishIntegrationEvent> notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
