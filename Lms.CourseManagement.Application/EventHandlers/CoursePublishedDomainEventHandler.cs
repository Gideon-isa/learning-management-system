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
            var moduleDtos = domainEvent.Modules
                // safely filters CourseModule objects
                .Select(module => new PublishedModuleDto(
                    module.Id,
                    module.Title,
                    module.Description,
                    module.Order,
                    [..module.Lessons.Select(lesson => new PublishedLessonDto(
                        lesson.Id,
                        lesson.Title,
                        lesson.Description,
                        lesson.Duration,
                        [..lesson.Images.Select(img => new PublishedLessonImageDto(img.FileName, img.Path, img.Caption))],
                        [..lesson.Tags.Select(tag => new PublishedLessonTagDto(tag.TagId, tag.TagName))],
                        [..lesson.Videos.Select(vid => new PublishedLessonVideoDto(vid.VideoId, vid.Path, vid.Title, vid.Thumbnail, vid.Description))]
                        )) ]));

            var integrationEvent = new CoursePublishedIntegrationEvent(
                domainEvent.CourseId,
                domainEvent.CourseTitle,
                domainEvent.CourseCode,
                domainEvent.Category,
                domainEvent.InstructorId,
                domainEvent.PublishedOn,
                moduleDtos // Pass the list of PublishedModuleDto to the integration event
            );

            // publish integration event to outbox table
            await _publisher.PublishAsync(integrationEvent, cancellationToken);
        }
    }
}
