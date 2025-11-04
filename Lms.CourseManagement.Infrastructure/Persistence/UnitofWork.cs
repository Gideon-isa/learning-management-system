using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Infrastructure.DbContex;
using Lms.SharedKernel.Application;
using Lms.SharedKernel.Domain;

namespace Lms.CourseManagement.Infrastructure.Persistence
{
    public class UnitofWork : ICourseManagementUnitOfWork
    {
        private readonly CourseManagementDbContext _dbContext;
        private readonly IDomainEventDispatcher _domainEventDispatcher;

        public UnitofWork(CourseManagementDbContext dbContext, IDomainEventDispatcher domainEventDispatcher)
        {
            _dbContext = dbContext;
            _domainEventDispatcher = domainEventDispatcher;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // 1.Save cahnges to the databae first before dispatching
            var result = await _dbContext.SaveChangesAsync(cancellationToken);

            // 2. Collect all doamin events from tracked entities
            var domainEvents = _dbContext.ChangeTracker.Entries<AggregateRoot<Guid>>()
                .SelectMany(c => c.Entity.DomainEvents).ToList();

            // Clear domain events from entities
            _dbContext.ChangeTracker.Entries<AggregateRoot<Guid>>()
                .ToList()
                .ForEach(e => e.Entity.ClearDomainEvents());

            // 4. Dispatch domain event (internal events via Dispatcher)
            if (domainEvents.Any())
            {
                await _domainEventDispatcher.DispatchAsync(domainEvents, cancellationToken);
            }

            return result;
            
        }

        //private static bool IsAggregateRoot(object entity)
        //{
        //    if (entity == null)
        //        return false;

        //    var type = entity.GetType();
        //    while (type != null)
        //    {
        //        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(AggregateRoot<>))
        //            return true;

        //        type = type.BaseType;
        //    }

        //    return false;
        //}
    }
}
