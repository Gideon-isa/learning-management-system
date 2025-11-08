using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Domain.Entities;
using Lms.Enrollment.Domain.Respositories;
using Lms.Shared.Abstractions.Messaging;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.IntegrationEvents.courseManagement;

namespace Lms.Enrollment.Application.EventHandlers
{
    public class CoursePublishedIntegrationEventHandler : ICustomNotificationHandler<IntegrationEventNotification<CoursePublishedIntegrationEvent>>
    {
        private readonly IAvailableCoursesRepository _coursesRepository;
        private readonly IEnrollmentUnitOfWork _enrollmentUnitOfWork;

        public CoursePublishedIntegrationEventHandler(IAvailableCoursesRepository coursesRepository, IEnrollmentUnitOfWork enrollmentUnitOfWork)
        {
            _coursesRepository = coursesRepository;
            _enrollmentUnitOfWork = enrollmentUnitOfWork;
        }

        public async Task Handle(IntegrationEventNotification<CoursePublishedIntegrationEvent> notification, CancellationToken cancellationToken)
        {
            var availableCourseEvent = notification.IntegrationEvent;

            var newCourse = AvailableCourse.Create(
                availableCourseEvent.CourseId, 
                availableCourseEvent.CourseTitle, 
                "Course Description", 
                availableCourseEvent.InstructorId.ToString(), 
                "Course ThumbNail");

            await _coursesRepository.AddCourseAsync(newCourse, cancellationToken);
            await _enrollmentUnitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
