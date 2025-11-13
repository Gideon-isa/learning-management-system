using Lms.CourseManagement.Domain.Entities;
using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Domain.Events
{
    public sealed record CoursePublishedEvent(
        Guid CourseId, 
        string CourseTitle,
        string CourseCode,
        string Category,
        Guid InstructorId,
        DateTime PublishedOn,
        List<CourseModule> Modules) : DomainEvent
    {
        
    }
}
