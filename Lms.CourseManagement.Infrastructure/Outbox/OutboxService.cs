using Lms.CourseManagement.Application.Abstractions;
using Lms.CourseManagement.Application.Models;
using Lms.CourseManagement.Infrastructure.DbContex;

namespace Lms.CourseManagement.Infrastructure.Outbox
{
    public class OutboxService : ICourseIntegrationEventPublisher
    {
        private readonly CourseSupportDbContext _supportDbContext;

        public OutboxService(CourseSupportDbContext coursedbContext)
        {
            _supportDbContext = coursedbContext;
        }
        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) where TIntegrationEvent : class
        {
            var outboxMessage = new CourseOutboxMessage (integrationEvent);
            await _supportDbContext.CourseOutboxMessages.AddAsync(outboxMessage);
            await _supportDbContext.SaveChangesAsync(cancellationToken); 
        }
    }
}
