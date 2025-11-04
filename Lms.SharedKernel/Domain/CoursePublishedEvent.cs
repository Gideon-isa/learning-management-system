namespace Lms.SharedKernel.Domain
{
    public sealed record CoursePublishedEvent(
        Guid CourseId, 
        string CourseTitle, 
        Guid InstructorId, 
        DateTime PublishedOn) : DomainEvent
    {
    }
}
