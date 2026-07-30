using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Responses;
using core.Domain.Errors;

namespace core.Application.Common.Exceptions.ExceptionFactory
{
    public sealed class ExceptionResponseFactory : IExceptionResponseFactory
    {
        public ExceptionResponse Create(Exception exception)
        {
            if (exception is AppException appEx)
                return FromError(appEx.Error);

            return FromError(GeneralErrors.Internal);
        }

        private static ExceptionResponse FromError(Error error)
        {

            int status = error.Type switch
            {
                ErrorType.Validation => 422,
                ErrorType.Authorization => 401,
                ErrorType.NotFound => 404,
                ErrorType.Conflict => 409,
                ErrorType.Business => 400,
                ErrorType.External => 503,
                ErrorType.Persistence => 500,
                ErrorType.Identity => 400,
                _ => 500
            };

            string title = error.Type switch
            {
                ErrorType.Validation => "Validation error",
                ErrorType.Authorization => "Authorization error",
                ErrorType.NotFound => "Not found",
                ErrorType.Conflict => "Conflict",
                ErrorType.Business => "Business error",
                ErrorType.External => "External service error",
                ErrorType.Persistence => "Persistence error",
                ErrorType.Identity => "Identity error",
                _ => "Internal error"
            };

            return new ExceptionResponse
            {
                Title = title,
                Detail = error.Message,
                Status = status,
                Type = $"urn:problem:{error.Type.ToString().ToLowerInvariant()}",
                Code = error.Code
            };
        }
    }
}
