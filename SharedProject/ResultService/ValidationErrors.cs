namespace Shared.ResultService;

public interface IValidationErrors
{
    Error[] Errors { get; }
}

public class ValidationErrors : Result, IValidationErrors
{
    private ValidationErrors(Error[] errors) 
        : base(false, new Error(code: "ValidationError", message: "A validation problem occurred."))
    {
        Errors = errors;
    }

    public Error[] Errors { get; }

    public static ValidationErrors WithErrors(Error[] errors) => new(errors);
}

public class ValidationErrors<TValue> : Result<TValue> , IValidationErrors
{
    public ValidationErrors(Error[] errors)
        : base(default!, false, new Error(code: "ValidationError", message: "A validation problem occurred."))
    {
        Errors = errors;
    }
    public Error[] Errors { get; }
    public static ValidationErrors<TValue> WithErrors(Error[] errors) => new(errors);
}



