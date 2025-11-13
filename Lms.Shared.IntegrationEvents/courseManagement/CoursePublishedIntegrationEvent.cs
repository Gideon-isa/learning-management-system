namespace Lms.Shared.IntegrationEvents.courseManagement
{
    public sealed record CoursePublishedIntegrationEvent(
        Guid CourseId, 
        string CourseTitle,
        string CourseCode,
        string Category,
        Guid InstructorId, 
        DateTime PublishedOn,
        IEnumerable<PublishedModuleDto> ModuleEvent);

    public sealed record PublishedModuleDto(
        Guid ModuleId,
        string Title,
        string Description,
        int Order,
        IReadOnlyCollection<PublishedLessonDto> Lessons);

    public sealed record PublishedLessonDto(
        Guid LessonId,
        string Title,
        string Description,
        TimeSpan Duration,
        IReadOnlyCollection<PublishedLessonImageDto> LessonImages,
        IReadOnlyCollection<PublishedLessonTagDto> LessonTags,
        IReadOnlyCollection<PublishedLessonVideoDto> Videos
        );

    public sealed record PublishedLessonImageDto(
        string FileName,
        string Path,
        string Caption);

    public sealed record PublishedLessonTagDto(
        Guid TagId,
        string TagName);

    public sealed record PublishedLessonVideoDto(
        string Path,
        string Title,
        string ThumbNail,
        string Description);
}
