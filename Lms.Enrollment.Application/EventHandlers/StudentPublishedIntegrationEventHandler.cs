using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Domain.Entities;
using Lms.Enrollment.Domain.Respositories;
using Lms.Shared.Abstractions.Messaging;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.IntegrationEvents.Identity;

namespace Lms.Enrollment.Application.EventHandlers
{
    public class StudentPublishedIntegrationEventHandler : ICustomNotificationHandler<IntegrationEventNotification<CreateStudentPublishedIntegrationEvent>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollmentUnitOfWork _unitOfWork;

        public StudentPublishedIntegrationEventHandler(IStudentRepository studentRepository, IEnrollmentUnitOfWork unitOfWork)
        {
            _studentRepository = studentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(IntegrationEventNotification<CreateStudentPublishedIntegrationEvent> notification, CancellationToken cancellationToken)
        {
            var studentEvent = notification.IntegrationEvent;

            var newStudent = Student.Create(studentEvent.Id, studentEvent.FirstName, studentEvent.LastName, studentEvent.Username);

            await _studentRepository.CreateAsync(newStudent, cancellationToken);
            
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
