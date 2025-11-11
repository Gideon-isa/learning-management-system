using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Domain.Entities;
using Lms.Enrollment.Infrastructure.DataContext;

namespace Lms.Enrollment.Infrastructure.Outbox
{
    public class OutboxService : IEnrollmentIntegrationEventPublisher
    {
        private readonly EnrollmentDbContext _enrollmentContext;

        public OutboxService(EnrollmentDbContext context)
        {
            _enrollmentContext = context;
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) where TIntegrationEvent : class
        {
            var outboxMessage = new EnrollmentOutboxMessage(integrationEvent);
            await _enrollmentContext.EnrollmentOutboxMessages.AddAsync(outboxMessage, cancellationToken);
            await _enrollmentContext.SaveChangesAsync(cancellationToken);
        }
    }
}
