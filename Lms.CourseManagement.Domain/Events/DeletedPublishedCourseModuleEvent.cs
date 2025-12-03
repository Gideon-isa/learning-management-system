using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Domain.Events
{
    public record DeletedPublishedCourseModuleEvent(Guid CourseId, Guid ModuleId) : DomainEvent
    {
    }
}
