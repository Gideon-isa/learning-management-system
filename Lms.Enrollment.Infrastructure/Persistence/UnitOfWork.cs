using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Infrastructure.DataContext;
using Lms.SharedKernel.Application;

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
            return await _enrollmentDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
