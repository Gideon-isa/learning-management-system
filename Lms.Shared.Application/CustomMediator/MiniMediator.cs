using Lms.Shared.Abstractions.Interfaces.Request;
using Lms.Shared.Application.CustomMediator.Interfaces.Mediator;
using Lms.Shared.Application.CustomMediator.Interfaces.Notification;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Shared.Application.CustomMediator
{
    public class MiniMediator(IServiceProvider serviceProvider) : ICustomMediator
    {
        private readonly IServiceProvider serviceProvider = serviceProvider;

        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : ICustomNotification
        {
            //#region TO PUBLISH TO A SINGLE HANDLER
            //// Please note that only one Handler will be called
            ////var types = new Type[] { notification.GetType(), typeof(TNotification) };
            //var genericBase = typeof(NotificationHandlerDecordatorImplementation<>);
            //var myHandlertype = genericBase.MakeGenericType(notification.GetType());
            //var myHandler = (NotificationHandlerDecorator)Activator.CreateInstance(myHandlertype)!;
            //await myHandler!.Handle(notification, serviceProvider, cancellationToken);
            //#endregion

            #region KINDLY UNCOMMENT THE CODE BELOW AND COMMENT ABOVE TO PUBLISH TO MANY HANDLERS
           // Resolve all handlers for this notification type

           var handlerType = typeof(ICustomNotificationHandler<>).MakeGenericType(notification.GetType());
           var handlers = serviceProvider.GetServices(handlerType)?.Cast<object>().ToList();

            // If no handlers, exit early
            if (handlers == null || handlers.Count == 0)
                    return;

            var handleMethod = handlerType.GetMethod("Handle");
            if (handleMethod == null)
            {
                return;
            }

            // Loop through each handler and invoke its Handle() method
            foreach (var handler in handlers)
            {
                var task = (Task?)handleMethod.Invoke(handler, new object[] { notification, cancellationToken });
                if (task != null)
                    await task.ConfigureAwait(false);
            }
            #endregion
        }

        public async Task<TResponse> Send<TResponse>(ICustomRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            var types = new Type[] { request.GetType(), typeof(TResponse)};
            var genericBase = typeof(RequestHandlerDecordatorImplementation<,>);
            var myHandlertype = genericBase.MakeGenericType(types);
            var myHandler = (RequestHandlerDecorator<TResponse>)Activator.CreateInstance(myHandlertype)!;
            return await myHandler!.Handle(request, serviceProvider, cancellationToken);
        }
    }

    // Request Handler Decorator Abstract Class
    internal abstract class RequestHandlerDecorator<TResponse> 
    {
        public abstract Task<TResponse> Handle(ICustomRequest<TResponse> request, IServiceProvider serviceProvider, CancellationToken cancellationToken);
    }


    // Request Handler Decorator Implementation
    internal class RequestHandlerDecordatorImplementation<TRequest, TResponse> : RequestHandlerDecorator<TResponse>
            where TRequest : ICustomRequest<TResponse>
    {
        public override Task<TResponse> Handle(ICustomRequest<TResponse> request, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            return serviceProvider.GetRequiredService<ICustomRequestHandler<TRequest, TResponse>>()
                .Handle((TRequest)request, cancellationToken);
        }
    }

    // 
    internal abstract class NotificationHandlerDecorator
    {
        public abstract Task Handle(ICustomNotification notification, IServiceProvider serviceProvider, CancellationToken cancellationToken);
    }

    // Notification Handler Decorator Implementation
    internal class NotificationHandlerDecordatorImplementation<TNotification> : NotificationHandlerDecorator
            where TNotification : ICustomNotification
    {
        public override Task Handle(ICustomNotification notification, IServiceProvider serviceProvider, CancellationToken cancellationToken)
        {
            return serviceProvider.GetRequiredService<ICustomNotificationHandler<TNotification>>()
                .Handle((TNotification)notification, cancellationToken);
        }
    }
}
