using Lms.Shared.Abstractions.Interfaces.Request;

namespace Lms.Shared.Abstractions.Interfaces.Sender
{
    public interface ISender
    {
  
        /// <summary>
        /// Asynchronously send a request to a single handler with no response
        /// </summary>
        /// <param name="request">Request object</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>A task that represents the send operation.</returns>
        Task<TResponse> Send<TResponse>(ICustomRequest<TResponse> request, CancellationToken cancellationToken = default);

    }
}
