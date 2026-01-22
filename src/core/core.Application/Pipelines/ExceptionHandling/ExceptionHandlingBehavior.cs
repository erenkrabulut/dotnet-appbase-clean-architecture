using core.Application.Common.Exceptions.ExceptionFactory;
using core.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Pipelines.ExceptionHandling
{
    public sealed class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    {
        private readonly IExceptionResponseFactory _factory;

        public ExceptionHandlingBehavior(IExceptionResponseFactory factory)
        {
            _factory = factory;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                var error = _factory.Create(ex);

                if (typeof(TResponse) == typeof(Response))
                    return (TResponse)(object)Response.Fail(error);

                if (typeof(TResponse).IsGenericType &&
                    typeof(TResponse).GetGenericTypeDefinition() == typeof(Response<>))
                {
                    var t = typeof(TResponse).GetGenericArguments()[0];
                    var responseType = typeof(Response<>).MakeGenericType(t);

                    var failMethod = responseType.GetMethod(nameof(Response<object>.Fail), new[] { typeof(ExceptionResponse) });
                    return (TResponse)failMethod!.Invoke(null, new object[] { error })!;
                }

                throw;
            }
        }
    }
}
