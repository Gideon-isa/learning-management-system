using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.Application.EventDispatcher;

namespace Lms.Identity.Application.Events
{
    public class InstructorRoleRequestEventHandler : ICustomNotificationHandler<DomainEventNotification<InstructorRoleRequestedEvent>>
    {

        public InstructorRoleRequestEventHandler()
        {
            
        }

        public async Task Handle(DomainEventNotification<InstructorRoleRequestedEvent> notification, CancellationToken cancellationToken)
        {
            // 
            
            //throw new NotImplementedException();
        }
    }
}
