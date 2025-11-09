using Lms.SharedKernel.Domain;

namespace Lms.Enrollment.Domain.Events
{
    public sealed record EnrollStudentEvent(string StudentCode, Guid courseId) : DomainEvent
    {
        
    }

}
