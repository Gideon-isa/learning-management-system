namespace Lms.Shared.Abstractions.Messaging
{
    /// <summary>
    /// Persist or publishes an integration event for cross-context communication
    /// </summary>
    public interface IIntegrationEventPublisher
    {
        Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default) where TIntegrationEvent : class;
    }
}
