using FluentValidation;
using Lms.SharedKernel.Common.Wrappers;
using MediatR;

namespace Lms.Shared.Application.Validation.Pipeline
{
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
        where TRequest : IRequest<TResponse>, IValidateRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context= new ValidationContext<TRequest>(request);
                var validationResults = await Task
                    .WhenAll(_validators
                    .Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults
                    .SelectMany(result => result.Errors)
                    .Where(failure => failure != null)
                    .ToList();

                List<string> errors = [];
                foreach (var failure in failures)
                {
                    errors.Add(failure.ErrorMessage);
                }
                return (TResponse)await ResponseWrapper.FailAsync(message: errors);
            }
            return await next(cancellationToken);
        }
    }
}
