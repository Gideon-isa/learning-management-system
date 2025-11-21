using Lms.Enrollment.Application.Abstractions;
using Lms.Enrollment.Domain.Entities;
using Lms.Enrollment.Infrastructure.DataContext;

namespace Lms.Enrollment.Infrastructure.Outbox
{
    public class OutboxService : IEnrollmentIntegrationEventPublisher
    {
        private readonly EnrollmentSupportDbContext _enrollmentSupportContext;

        public OutboxService(EnrollmentSupportDbContext enrollmentSupportContext)
        {
            _enrollmentSupportContext = enrollmentSupportContext;
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) where TIntegrationEvent : class
        {
            var outboxMessage = new EnrollmentOutboxMessage(integrationEvent);
            await _enrollmentSupportContext.EnrollmentOutboxMessages.AddAsync(outboxMessage, cancellationToken);
            await _enrollmentSupportContext.SaveChangesAsync(cancellationToken);
        }
    }
}
