using core.Domain.Errors;


namespace core.Application.Common.Exceptions.ExceptionTypes
{

    public sealed class PersistenceException : AppException
    {
        public PersistenceException(Error error) : base(error) { }
    }
}
