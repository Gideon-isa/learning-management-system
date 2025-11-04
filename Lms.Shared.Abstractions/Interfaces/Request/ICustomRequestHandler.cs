namespace Lms.Shared.Abstractions.Interfaces.Request
{
    public interface ICustomRequestHandler<in TRequest, TResponse>
        where TRequest : ICustomRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
