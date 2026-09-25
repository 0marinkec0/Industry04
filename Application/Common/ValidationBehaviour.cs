using FluentValidation;
using MediatR;
using Shared.ResultService;

namespace Application.Common
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if(!_validators.Any())
            {
               return await next();
            }

            Error[] errors = _validators
                .Select(validators => validators.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure is not null)
                .Select(failure => new Error(code: failure.PropertyName, message: failure.ErrorMessage))
                .Distinct()
                .ToArray();

            if (errors.Any())
            {
                return (ValidationErrors.WithErrors(errors) as TResponse)!;
            }

            return await next();
        }
    }
}
