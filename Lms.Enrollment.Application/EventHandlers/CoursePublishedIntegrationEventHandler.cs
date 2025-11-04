using Lms.Shared.Abstractions.Messaging;
using Lms.Shared.Application;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.IntegrationEvents.courseManagement;

namespace Lms.Enrollment.Application.EventHandlers
{
    public class CoursePublishedIntegrationEventHandler : ICustomNotificationHandler<IntegrationEventNotification<CoursePublishedIntegrationEvent>>
    {
        public async Task Handle(IntegrationEventNotification<CoursePublishedIntegrationEvent> notification, CancellationToken cancellationToken)
        {
            var integrationEvent = notification.IntegrationEvent;

            await Task.CompletedTask;
            Console.WriteLine("Test");
           
        }
    }
}
