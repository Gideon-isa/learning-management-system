using Lms.SharedKernel.Domain;

namespace Lms.Identity.Application.Events
{
    public sealed record InstructorRoleRequestedEvent(string Username, string FirstName, string LastName) : DomainEvent
    {

    }
}
