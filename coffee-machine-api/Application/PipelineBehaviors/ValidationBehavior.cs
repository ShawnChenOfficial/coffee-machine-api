using System;
using coffee_machine_api.Application.Exceptions;
using FluentValidation;
using MediatR;

namespace coffee_machine_api.Application.PipelineBehaviors
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                // trying to get all failures
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    if (failures.Any( a => a.CustomState is StatusCodeBasedValidationException))
                    {
                        var exception = (StatusCodeBasedValidationException)failures.First(f => f.CustomState is StatusCodeBasedValidationException).CustomState;
                        throw new StatusCodeBasedValidationException(exception.GetStatusCode());
                    }
                    else
                    { 
                        throw new ValidationException(failures);
                    }
                }
            }
            return await next();
        }
    }
}

