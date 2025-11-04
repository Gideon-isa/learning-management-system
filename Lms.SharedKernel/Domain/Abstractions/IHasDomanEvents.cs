using Lms.SharedKernel.Domain.Abstractions;

/// <summary>
/// It gives read-only access and clearing capability
/// </summary>
public interface IHasDomanEvents
{
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
