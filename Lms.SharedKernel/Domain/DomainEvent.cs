using Lms.SharedKernel.Domain.Abstractions;

namespace Lms.SharedKernel.Domain
{
    public abstract record DomainEvent : IDomainEvent
    {
        public DateTime OccurredOn { get; init;} = DateTime.UtcNow;
    }
}
