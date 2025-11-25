using FluentValidation;
using Lms.Shared.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Lms.Shared.Application.Validations
{
    /// <summary>
    /// 
    /// </summary>
    internal class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task DispatcherAsync<TCommand>(TCommand command, CancellationToken token)
        {
            var validator = _serviceProvider.GetService<IValidator<TCommand>>();
            if (validator is not null)
            {
                var result = await validator.ValidateAsync(command, token);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
            }
            await Task.CompletedTask;
        }
    }
}
