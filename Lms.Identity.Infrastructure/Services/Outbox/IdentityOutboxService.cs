using Lms.Identity.Application.Abstractions;
using Lms.Identity.Infrastructure.Context;
using Lms.Identity.Infrastructure.Identity.Models;

namespace Lms.Identity.Infrastructure.Services.Outbox
{
    public class IdentityOutboxService : IIdentityIntegrationEventPublisher
    {
        private IdentitySupportDbContext _identitySupportDbContext;

        public IdentityOutboxService(IdentitySupportDbContext identitySupportDbContex)
        {
            _identitySupportDbContext = identitySupportDbContex;
        }

        public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) where TIntegrationEvent : class
        {
            var outboxMessage = new IdentityOutboxMessage(integrationEvent);
            await _identitySupportDbContext.IdentityOutboxMessages.AddAsync(outboxMessage);
            await _identitySupportDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
