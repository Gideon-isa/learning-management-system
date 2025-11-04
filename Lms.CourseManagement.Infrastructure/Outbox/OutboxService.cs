using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Domain.Entities;
using Lms.CourseManagement.Infrastructure.DbContex;

namespace Lms.CourseManagement.Infrastructure.Outbox
{
    public class OutboxService : ICourseIntegrationEventPublisher
    {
        private readonly CourseManagementDbContext _coursedbContext;

        public OutboxService(CourseManagementDbContext coursedbContext)
        {
            _coursedbContext = coursedbContext;
        }
        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) where TIntegrationEvent : class
        {
            var outboxMessage = new OutboxMessage(integrationEvent);
            await _coursedbContext.OutboxMessages.AddAsync(outboxMessage);
            await _coursedbContext.SaveChangesAsync(cancellationToken); 
        }
    }
}
