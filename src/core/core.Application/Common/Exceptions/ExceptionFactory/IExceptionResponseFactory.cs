using core.Application.Common.Responses;


namespace core.Application.Common.Exceptions.ExceptionFactory
{
    public interface IExceptionResponseFactory
    {
        ExceptionResponse Create(Exception exception);
    }
}
