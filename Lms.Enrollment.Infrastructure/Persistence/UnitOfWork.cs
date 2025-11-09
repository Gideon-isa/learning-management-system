using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Infrastructure.DataContext;
using Lms.SharedKernel.Application;
using Lms.SharedKernel.Domain;

namespace Lms.Enrollment.Infrastructure.Persistence
{
    public class UnitOfWork : IEnrollmentUnitOfWork
    {
        private readonly EnrollmentDbContext _enrollmentDbContext;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public UnitOfWork(EnrollmentDbContext enrollmentDbContext, IDomainEventDispatcher domainEventDispatcher)
        {
            _enrollmentDbContext = enrollmentDbContext;
            _domainEventDispatcher = domainEventDispatcher;
        }



        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result =  await _enrollmentDbContext.SaveChangesAsync(cancellationToken);

            var domainEvent = _enrollmentDbContext.ChangeTracker.Entries<AggregateRoot<Guid>>().SelectMany(e => e.Entity.DomainEvents).ToList();

            _enrollmentDbContext.ChangeTracker.Entries<AggregateRoot<Guid>>().ToList().ForEach(e => e.Entity.ClearDomainEvents());

            if (domainEvent.Count != 0)
            { 
                await _domainEventDispatcher.DispatchAsync(domainEvent, cancellationToken);
            }
            return result;
        }
    }
}
