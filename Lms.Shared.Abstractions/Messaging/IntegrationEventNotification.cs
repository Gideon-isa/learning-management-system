using Lms.Shared.Application.CustomMediator.Interfaces.Notification;

namespace Lms.Shared.Abstractions.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TIntegrationEvent"></typeparam>
    /// <param name="IntegrationEvent"></param>
    public sealed record IntegrationEventNotification<TIntegrationEvent>(TIntegrationEvent IntegrationEvent) : ICustomNotification where TIntegrationEvent : class 
    {
    }
}
