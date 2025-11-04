using Lms.SharedKernel.Domain;

namespace Lms.Identity.Application.Events
{
    public record ApprovedInstructorEvent : DomainEvent
    {
        public Guid Id { get; init; }
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? DisplayName { get; init; }
        public string? ProfileImageUrl { get; init; }
        public string? Bio { get; init; }
        public string? Department { get; init; }
        public string? Institution { get; init; }
        public string? StaffId { get; init; }

        public ApprovedInstructorEvent() { }
    }
}
