namespace Lms.Shared.IntegrationEvents.courseManagement
{
    public sealed record CoursePublishedIntegrationEvent(Guid CourseId, string CourseTitle, Guid InstructorId, DateTime PublishedOn)
    {
    }
}
