using Lms.ContentDelivery.Application.Abstractions;
using Lms.ContentDelivery.Domain.Repositories;
using Lms.Shared.Abstractions.Messaging;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.IntegrationEvents.courseManagement;

namespace Lms.ContentDelivery.Application.EventHandlers
{
    public class DeletedPublishedCourseModuleIntegrationEventHandler : ICustomNotificationHandler<IntegrationEventNotification<DeletePublishedCourseModuleIntegrationEvent>>
    {
        private readonly ICourseContentRepository _courseContentRepository;
        private readonly IContentDeliveryUnitOfWork _contentDeliveryUnitOfWork;
        public DeletedPublishedCourseModuleIntegrationEventHandler(ICourseContentRepository courseContentRepository, 
            IContentDeliveryUnitOfWork contentDeliveryUnitOfWork)
        {
            _courseContentRepository = courseContentRepository;
            _contentDeliveryUnitOfWork = contentDeliveryUnitOfWork;
        }
        public async Task Handle(IntegrationEventNotification<DeletePublishedCourseModuleIntegrationEvent> notification, CancellationToken cancellationToken)
        {
            var @event = notification.IntegrationEvent;

            var courseContent = await _courseContentRepository.GetCourseContentByCourseIdAsync(@event.CourseId, cancellationToken);
            if (courseContent is null)
            {
                // TODO: Log Course content not found for CourseId {@event.CourseId
                return;
            }
            courseContent?.RemoveModuleFromCourse(@event.ModuleId); 
            await _contentDeliveryUnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
