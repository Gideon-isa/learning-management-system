using Lms.ContentDelivery.Application.Abstractions;
using Lms.ContentDelivery.Domain.Entities;
using Lms.ContentDelivery.Domain.Repositories;
using Lms.Shared.Abstractions.Messaging;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.IntegrationEvents.Enrollment;

namespace Lms.ContentDelivery.Application.EventHandlers
{
    public class StudentEnrollmentPublishedIntegrationEventHandler : ICustomNotificationHandler<IntegrationEventNotification<EnrollmentPublishIntegrationEvent>>
    {
        private readonly IStudentAccessRepository _studentAccessRepository;
        private readonly IContentDeliveryUnitOfWork _contentDeliveryUnitOfWork;

        public StudentEnrollmentPublishedIntegrationEventHandler(IStudentAccessRepository 
            studentAccessRepository, IContentDeliveryUnitOfWork contentDeliveryUnitOfWork)
        {
            _studentAccessRepository = studentAccessRepository;
            _contentDeliveryUnitOfWork = contentDeliveryUnitOfWork;
        }

        public async Task Handle(IntegrationEventNotification<EnrollmentPublishIntegrationEvent> notification, CancellationToken cancellationToken)
        {
            var studentEvent = notification.IntegrationEvent;

            var studentCourseAccess = StudentAccess.Create(studentEvent.StudentCode, studentEvent.CourseId);
            await _studentAccessRepository.CreateStudentAccessAsync(studentCourseAccess, cancellationToken);
            await _contentDeliveryUnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
