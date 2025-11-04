//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Lms.Shared.Application.Messaging
//{
//    internal class CustomMediatorImplementation : IMediator
//    {
//        private readonly IServiceProvider _serviceProvider;

//        public CustomMediatorImplementation(IServiceProvider serviceProvider)
//        {
//            _serviceProvider = serviceProvider;
//        }

//        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
//        {
//            throw new NotImplementedException();
//        }

//        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
//        {
//            var requestType = request.GetType();
//            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

//            var handler = _serviceProvider.GetService(handlerType);
//            if (handler == null)
//                throw new InvalidOperationException($"No handler found for request type {requestType.Name}");

//            var method = handlerType.GetMethod("Handle")!;
//            return await (Task<TResponse>) method.Invoke(handler, new object[] { request, cancellationToken })!;
//        }
//    }
//}
