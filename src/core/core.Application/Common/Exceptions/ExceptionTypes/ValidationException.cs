using core.Domain.Errors;



namespace core.Application.Common.Exceptions.ExceptionTypes
{
    public sealed class ValidationException : AppException
    {
        public ValidationException(Error error) : base(error) { }
    }
}
