using core.Domain.Errors;


namespace core.Application.Common.Exceptions.ExceptionTypes
{
    public abstract class AppException : Exception
    {
        public Error Error { get; }

        protected AppException(Error error) : base(error.Message)
        {
            Error = error;
        }
    }
}
