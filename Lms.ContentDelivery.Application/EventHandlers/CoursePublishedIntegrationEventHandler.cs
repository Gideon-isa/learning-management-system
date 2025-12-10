using Lms.ContentDelivery.Application.Abstractions;
using Lms.ContentDelivery.Domain.Entities;
using Lms.ContentDelivery.Domain.Repositories;
using Lms.ContentDelivery.Domain.ValueObjects;
using Lms.Shared.Abstractions.Messaging;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.IntegrationEvents.courseManagement;

namespace Lms.ContentDelivery.Application.EventHandlers
{
    public class CoursePublishedIntegrationEventHandler : ICustomNotificationHandler<IntegrationEventNotification<CoursePublishedIntegrationEvent>>
    {
        private readonly ICourseContentRepository _courseContentRepository;
        private readonly IContentDeliveryUnitOfWork _contentDeliveryUnitOfWork;

        public CoursePublishedIntegrationEventHandler(ICourseContentRepository courseContentRepository, 
            IContentDeliveryUnitOfWork contentDeliveryUnitOfWork)
        {
            _courseContentRepository = courseContentRepository;
            _contentDeliveryUnitOfWork = contentDeliveryUnitOfWork;
        }

        // CourseContent Module
        public async Task Handle(IntegrationEventNotification<CoursePublishedIntegrationEvent> notification, CancellationToken cancellationToken)
        {

            // Get the published-course from the Event Args
            var courseEvent = notification.IntegrationEvent;
            var course = CourseContent.Create(courseEvent.CourseId, courseEvent.CourseTitle, courseEvent.CourseCode, courseEvent.CourseCategoryCode, courseEvent.InstructorId);

            // get the published-module from the Event Args
            var moduleEvents = notification.IntegrationEvent.ModuleEvent;

            foreach (var module in moduleEvents)
            {
                // creating a courseModule
                var newCourseModule = CourseModuleContent.Create(module.ModuleId, module.Title, module.Description);

                // for each lesson, creating lessons and videos
                foreach (var lesson in module.Lessons)
                {
                    var lessonVideos = lesson.Videos.ToList().Select(v => new LessonVideo(v.VideoId ,v.Path, v.Title, v.ThumbNail, v.Description));
                    var newlesson = LessonContent.Create(lesson.Title, lesson.Description, lesson.Duration, [..lessonVideos]);

                    // adding each lesson in to its module contained in the module
                    newCourseModule.AddLessonToModule(newlesson);
                }
                // 
                course.AddModuleToCourse(newCourseModule);
            }
            await _courseContentRepository.AddAsync(course, cancellationToken);
            await _contentDeliveryUnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
