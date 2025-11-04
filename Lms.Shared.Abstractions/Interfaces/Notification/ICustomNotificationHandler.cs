namespace Lms.Shared.Application.CustomMediator.Interfaces.Notification
{
    public interface ICustomNotificationHandler<in TNotification> where TNotification: ICustomNotification
    {
        Task Handle(TNotification notification, CancellationToken cancellationToken);
    }
}
