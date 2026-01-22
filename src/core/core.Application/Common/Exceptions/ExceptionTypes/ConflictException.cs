using core.Domain.Errors;

namespace core.Application.Common.Exceptions.ExceptionTypes
{
    public sealed class ConflictException : AppException
    {
        public ConflictException(Error error) : base(error) { }
    }
}
