using Lms.ContentDelivery.Application.Abstractions;
using Lms.ContentDelivery.Infrastructure.DataContext;
using Lms.SharedKernel.Domain;
using System.Threading;

namespace Lms.ContentDelivery.Infrastructure.Persistence
{
    public class ContentDeliveryUnitOfWork : IContentDeliveryUnitOfWork
    {
        private readonly CourseContentDbContext _contentCourseDbContext;

        public ContentDeliveryUnitOfWork(CourseContentDbContext contentCourseDbContext)
        {
            _contentCourseDbContext = contentCourseDbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await _contentCourseDbContext.SaveChangesAsync(cancellationToken);

            var domainEvent = _contentCourseDbContext.ChangeTracker.Entries<AggregateRoot<Guid>>().SelectMany(e => e.Entity.DomainEvents).ToList();

            _contentCourseDbContext.ChangeTracker.Entries<AggregateRoot<Guid>>().ToList().ForEach(e => e.Entity.ClearDomainEvents());

            //if (domainEvent.Count != 0)
            //{
            //    await _domainEventDispatcher.DispatchAsync(domainEvent, cancellationToken);
            //}
            return result;
        }
    }
}
