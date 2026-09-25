using Shared.Respons;

namespace Web.Common.Exceptions;

public class ValidationException : Exception   
{
    public List<FailureError> ErrorsGrouped { get; }
    public ValidationException(FailureResponse error) {

        ErrorsGrouped = error.Errors;          
    }
}
