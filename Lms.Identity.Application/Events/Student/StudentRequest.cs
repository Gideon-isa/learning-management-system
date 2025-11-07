using Lms.SharedKernel.Domain;
using Lms.SharedKernel.Domain.Abstractions;

namespace Lms.Identity.Application.Events.Student
{
    public class StudentRequest : IHasDomanEvents
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
       
        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}
