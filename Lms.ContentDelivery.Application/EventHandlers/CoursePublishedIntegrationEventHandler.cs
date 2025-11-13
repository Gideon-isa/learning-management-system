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
            var course = CourseContent.Create(courseEvent.CourseId, courseEvent.CourseTitle, courseEvent.CourseCode, courseEvent.Category, courseEvent.InstructorId);

            // get the published-module from the Event Args
            var moduleEvents = notification.IntegrationEvent.ModuleEvent;

            foreach (var module in moduleEvents)
            {
                // creating a courseModule
                var newCourseModule = CourseModuleContent.Create(module.Title, module.Description);

                // for each lesson, creating lessons and videos
                foreach (var lesson in module.Lessons)
                {
                    var lessonVideos = lesson.Videos.ToList().Select(v => new LessonVideo(v.Path, v.Title, "thumbnail", v.Description));
                    var newlesson = LessonContent.Create(lesson.Title, lesson.Description, lesson.Duration, [..lessonVideos]);

                    // adding each lesson in to its module contained in the module
                    newCourseModule.AddLessonToModule(newlesson);
                }
                // 
                course.AddModuleToCourse(newCourseModule);
            }

            //var courseEvent = notification.IntegrationEvent;
                
            //var course = CourseContent.Create(courseEvent.CourseId, courseEvent.CourseTitle, courseEvent.CourseCode,courseEvent .Category, courseEvent.InstructorId);

            ////courseEvent.ModuleEvent
            //var courseModules = courseEvent.ModuleEvent
            //    .Select(mod => CourseModuleContent.Create(mod.Title, mod.Description));

            //var lessons = courseEvent.ModuleEvent.SelectMany(m => m.Lessons);
            
            //// For each courseModule
            //foreach (var module in courseModules)
            //{
            //    // Each courseModule contains list of lessons
            //    foreach (var lesson in lessons)
            //    {
            //        // list of videos contained in a single lesson
            //        var lessonVideos = lesson.Videos.ToList();

            //        // each lesson contains list of videos
            //        var videos = lessonVideos.Select(v => new LessonVideo(v.Path, v.Title, "thumbnail", v.Description)).ToList();
            //        var newLesson = LessonContent.Create(lesson.Title, lesson.Description, lesson.Duration, videos);

            //        module.AddLessonToModule(newLesson);
            //    }

            //    course.AddModuleToCourse(module);
            //}

            await _courseContentRepository.AddAsync(course, cancellationToken);
            await _contentDeliveryUnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
