using Lms.Identity.Application.Abstractions;
using Lms.Identity.Application.Events;
using Lms.Identity.Infrastructure.Context;
using Lms.SharedKernel.Application;
using Lms.SharedKernel.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lms.Identity.Infrastructure.Persistence
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        private readonly UserIdentityDbContext _dbContext;
        private readonly IDomainEventDispatcher _domainEventDispatcher;
        

        public IdentityUnitOfWork(UserIdentityDbContext context, IDomainEventDispatcher domainEventDispatcher)
        {
            _dbContext = context;
            _domainEventDispatcher = domainEventDispatcher;
            
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // 1.Save cahnges to the databae first before dispatching
            var result = await _dbContext.SaveChangesAsync(cancellationToken);
            var domainEvent = _dbContext.ChangeTracker.Entries<AggregateRoot<Guid>>().SelectMany(c => c.Entity.DomainEvents).ToList();

            return result;

        }
    }
}
