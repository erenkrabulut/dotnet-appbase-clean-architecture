using core.Domain.Errors;


namespace core.Application.Common.Exceptions.ExceptionTypes
{
    public sealed class AuthorizationException : AppException
    {
        public AuthorizationException(Error error) : base(error) { }
    }
}
