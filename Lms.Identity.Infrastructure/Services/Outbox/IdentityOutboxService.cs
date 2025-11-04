using Lms.Identity.Application.Abstractions;
using Lms.Identity.Infrastructure.Context;
using Lms.Identity.Infrastructure.Identity.Models;

namespace Lms.Identity.Infrastructure.Services.Outbox
{
    public class IdentityOutboxService : IIdentityIntegrationEventPublisher
    {
        private UserIdentityDbContext _userIdentityDbContext;

        public IdentityOutboxService(UserIdentityDbContext userIdentityDbContext)
        {
            _userIdentityDbContext = userIdentityDbContext;
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) where TIntegrationEvent : class
        {
            var outboxMessage = new IdentityOutboxMessage(integrationEvent);
            await _userIdentityDbContext.IdentityOutboxMessages.AddAsync(outboxMessage);
            await _userIdentityDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
