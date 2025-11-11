using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Domain.Events;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Lms.Shared.Application.EventDispatcher;
using Lms.Shared.IntegrationEvents.courseManagement;
using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Application.EventHandlers
{
    /// <summary>
    ///  This is for Integration Events
    /// </summary>
    public class CoursePublishedDomainEventHandler : ICustomNotificationHandler<DomainEventNotification<CoursePublishedEvent>>
    {
        private readonly ICourseIntegrationEventPublisher _publisher; // for saving into the Outbox table

        public CoursePublishedDomainEventHandler(ICourseIntegrationEventPublisher publisher)
        {
            _publisher = publisher;
        }
        public async Task Handle(DomainEventNotification<CoursePublishedEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            // Fix: domainEvent.Modules is object[], so cast each element to PublishedModuleDto
            var moduleDtos = (domainEvent.Modules)
                .OfType<PublishedModuleDto>() // safely filters CourseModule objects
                .Select(module => new PublishedModuleDto(
                    module.ModuleId,
                    module.Title,
                    module.Description,
                    module.Order,
                    module.Lessons.Select(lesson => new PublishedLessonDto(
                        lesson.LessonId,
                        lesson.Title,
                        lesson.Description,
                        lesson.Duration,
                        lesson.LessonImages.Select(img => new PublishedLessonImageDto(img.Id, img.FileName, img.Path, img.Caption)).ToList(),
                        lesson.LessonTags.Select(tag => new PublishedLessonTagDto(tag.TagId, tag.TagName)).ToList(),
                        lesson.Videos.Select(vid => new PublishedLessonVideoDto(vid.Title, vid.Title, vid.ThumbNail, vid.Description)).ToList()
                        )).ToList()));

            var integrationEvent = new CoursePublishedIntegrationEvent(
                domainEvent.CourseId,
                domainEvent.CourseTitle,
                domainEvent.InstructorId,
                domainEvent.PublishedOn,
                moduleDtos // Pass the list of PublishedModuleDto to the integration event
            );

            // publish integration event to outbox table
            await _publisher.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}
