using Lms.Shared.Abstractions.Messaging;
using Lms.Shared.Application;
using Lms.Shared.Application.CustomMediator.Interfaces.Mediator;
using Lms.Shared.IntegrationEvents;

namespace Lms.Shared.IntegrationEvents.Integration
{
    /// <summary>
    /// Integration Event Publisher
    /// </summary>
    public class MediatRIntegrationEventPublisher
    {
        private readonly ICustomMediator _customMediator;
        public MediatRIntegrationEventPublisher(ICustomMediator mediator)
        {
            _customMediator = mediator;
        }
        // called by the Outbox Processor
        public async Task PublishEventAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) where TIntegrationEvent : class
        {
            // IntegrationEventNotification extends the ICustomeNotification. so it is a type of ICustomNotification
            var notification = new IntegrationEventNotification<TIntegrationEvent>(integrationEvent);
            await _customMediator.Publish(notification, cancellationToken);
        }
    }
}
