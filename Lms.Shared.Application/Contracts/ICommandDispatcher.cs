namespace Lms.Shared.Application.Contracts
{
    public interface ICommandDispatcher
    {
        Task DispatcherAsync<TCommand>(TCommand command, CancellationToken token);
    }
}
