using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Shared.ResultService;
using System.ComponentModel.DataAnnotations;

namespace Application.Common
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
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
              return CreateValidationResult<TResponse>(errors);
            }

            return await next();
        }

        private static TResult CreateValidationResult<TResult>(Error[] errors)
            where TResult : Result
        {
            if (typeof(TResult) == typeof(Result))
            {
                return (ValidationErrors.WithErrors(errors) as TResult)!;
            }

            object validationResult = typeof(ValidationErrors<>)
                .GetGenericTypeDefinition()
                .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                .GetMethod(nameof(ValidationErrors.WithErrors))!
                .Invoke(null, new object[] { errors })!;

            return (TResult)validationResult;
        }
    }
}
