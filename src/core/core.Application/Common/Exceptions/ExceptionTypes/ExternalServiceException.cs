using core.Domain.Errors;

namespace core.Application.Common.Exceptions.ExceptionTypes
{
    public sealed class ExternalServiceException : AppException
    {
        public ExternalServiceException(Error error) : base(error) { }
    }
}
