using Lms.SharedKernel.Domain;

namespace Lms.Identity.Application.Events.Student
{
    public record StudentEvent : DomainEvent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
