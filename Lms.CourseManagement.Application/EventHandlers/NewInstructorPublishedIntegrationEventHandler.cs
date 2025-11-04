using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Messaging;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.IntegrationEvents.Identity;
using Lms.SharedKernel.Interfaces;

namespace Lms.CourseManagement.Application.EventHandlers
{
    public class NewInstructorPublishedIntegrationEventHandler : ICustomNotificationHandler<IntegrationEventNotification<CreateInstructorPublishedIntegrationEvent>>
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly IUnitOfWork _unitOfWork;

        // Saving the
        public Task Handle(IntegrationEventNotification<CreateInstructorPublishedIntegrationEvent> notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
