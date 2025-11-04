using Lms.Shared.Abstractions.Messaging;
using Lms.Shared.Application;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.IntegrationEvents.courseManagement;

namespace Lms.Assessment.Application.EventHandlers
{
    public class CoursePublishedIntegration : ICustomNotificationHandler<IntegrationEventNotification<CoursePublishedIntegrationEvent>>
    {
        public Task Handle(IntegrationEventNotification<CoursePublishedIntegrationEvent> notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
