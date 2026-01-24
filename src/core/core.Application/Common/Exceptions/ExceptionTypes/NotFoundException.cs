using core.Domain.Errors;

namespace core.Application.Common.Exceptions.ExceptionTypes
{

    public sealed class NotFoundException : AppException
    {
        public NotFoundException() : base(Errors.General.NotFound) { }
        public NotFoundException(Error error) : base(error) { }

    }
}
