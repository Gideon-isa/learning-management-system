namespace Lms.Shared.Application.Messaging
{
    //internal class CustomMediator
    //{
       
    //}

    //// ---- REQUESTS ----
    //public interface IRequest<TResponse> { }

    //public interface IRequestHandler<TRequest, TResponse>
    //    where TRequest : IRequest<TResponse>
    //{
    //    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    //}

    //// ---- NOTIFICATIONS (EVENTS) ----
    //public interface INotification { }

    //public interface INotificationHandler<in TNotification>
    //    where TNotification : INotification
    //{
    //    Task Handle(TNotification notification, CancellationToken cancellationToken);
    //}

    //// ---- MEDIATOR ----
    //public interface IMediator
    //{
    //    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default);
    //    Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
    //        where TNotification : INotification;
    //}
}
