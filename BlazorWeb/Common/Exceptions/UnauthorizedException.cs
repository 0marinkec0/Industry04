namespace Web.Common.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() : base("You are not authorized for this page") { }
    }
}
