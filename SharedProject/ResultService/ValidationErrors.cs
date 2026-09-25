namespace Shared.ResultService
{
    public class ValidationErrors : Result
    {
        private ValidationErrors(Error[] errors) 
            : base(false, new Error(code: "ValidationError", message: "A validation problem occurred."))
        {
            Errors = errors;
        }

        public Error[] Errors { get; }

        public static ValidationErrors WithErrors(Error[] errors) => new(errors);
    }
}
