using FluentValidation;
using Lms.Shared.Application.Contracts;
using Lms.SharedKernel.Common.Wrappers;
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
            var validator = _serviceProvider.GetService<IValidator<TCommand>>() 
                ?? throw new ValidationException($"No validator found for command type {typeof(TCommand).Name}");
            
            var result = await validator.ValidateAsync(command, token);
            if (!result.IsValid)
                throw new ValidationException(string.Join(Environment.NewLine, 
                    result.Errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}")));
        }
    }
}
