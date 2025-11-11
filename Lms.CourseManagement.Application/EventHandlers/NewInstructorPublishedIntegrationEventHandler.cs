using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Domain.Repositories;
using Lms.Shared.Abstractions.Messaging;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.IntegrationEvents.Identity;

namespace Lms.CourseManagement.Application.EventHandlers
{
    /// <summary>
    /// Handles the integration event for when a new instructor is published.
    /// </summary>
    /// <remarks>This handler processes the <see cref="CreateInstructorPublishedIntegrationEvent"/> by
    /// creating a new instructor entity and saving it to the repository. It ensures that the new instructor's details
    /// are persisted in the course management system. This is so becuase the Instructor responsibilities belongs to the Course Management</remarks>
    public class NewInstructorPublishedIntegrationEventHandler : ICustomNotificationHandler<IntegrationEventNotification<CreateInstructorPublishedIntegrationEvent>>
    {
        private readonly IInstructorRepository _instructorRepository;
        private readonly ICourseManagementUnitOfWork _unitOfWork;

        public NewInstructorPublishedIntegrationEventHandler(IInstructorRepository instructorRepository, 
            ICourseManagementUnitOfWork unitOfWork)
        {
            _instructorRepository = instructorRepository;
            _unitOfWork = unitOfWork;
        }

        // Saving the
        public async Task Handle(IntegrationEventNotification<CreateInstructorPublishedIntegrationEvent> notification, 
            CancellationToken cancellationToken)
        {
            var instructor = notification.IntegrationEvent;

            var newInstructor = Instructor.Create(
                instructor.Id,
                instructor.FirstName,
                instructor.LastName,
                instructor.ProfileImageUrl,
                instructor.DisplayName,
                instructor.Bio,
                instructor.Department,
                instructor.Institution,
                instructor.StaffId);

            await _instructorRepository.AddInstructorAsync(newInstructor, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await Task.CompletedTask;
        }
    }
}
