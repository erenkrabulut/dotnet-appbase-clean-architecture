using core.Application.Abstractions.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Pipelines.Logging
{
    public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ILoggerService _logger;
        private readonly ILogContextAccessor _logContextAccessor;

        public LoggingBehavior(ILoggerService logger, ILogContextAccessor logContextAccessor)
        {
            _logger = logger;
            _logContextAccessor = logContextAccessor;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {

            if (request is not ILoggableRequest)
                return await next();


            LogContext ctx = _logContextAccessor.Get();

            string requestName = typeof(TRequest).Name;
            string correlationId = ctx.CorrelationId ?? "N/A";

            var sw = Stopwatch.StartNew();

            _logger.LogInfo("Request started", new
            {
                CorrelationId = correlationId,
                Request = requestName,
                UserId = ctx.UserId,
                IsAuthenticated = ctx.IsAuthenticated,
                Roles = ctx.Roles,
                Permissions = ctx.Permissions,
                IpAddress = ctx.IpAddress,
                UserAgent = ctx.UserAgent,
                RequestPath = ctx.RequestPath,
                HttpMethod = ctx.HttpMethod
            });

            try
            {
                TResponse response = await next();

                sw.Stop();

                _logger.LogInfo("Request finished", new
                {
                    CorrelationId = correlationId,
                    Request = requestName,
                    ElapsedMs = sw.ElapsedMilliseconds
                });

                return response;
            }
            catch (Exception ex)
            {
                sw.Stop();

                _logger.LogError("Request failed", ex, new
                {
                    CorrelationId = correlationId,
                    Request = requestName,
                    ElapsedMs = sw.ElapsedMilliseconds
                });

                throw;
            }
        }
    }
}
