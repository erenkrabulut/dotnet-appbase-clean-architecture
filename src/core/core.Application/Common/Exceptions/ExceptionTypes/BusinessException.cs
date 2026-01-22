using core.Domain.Errors;

namespace core.Application.Common.Exceptions.ExceptionTypes
{
    public sealed class BusinessException : AppException
    {
        public BusinessException(Error error) : base(error) { }
    }
}
