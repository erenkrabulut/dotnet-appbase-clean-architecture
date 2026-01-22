using core.Application.Abstractions.Logging;
using core.Application.Abstractions.Security.UserContext;
using Microsoft.AspNetCore.Http;

namespace core.Infrastructure.Logging
{

    public sealed class HttpLogContextAccessor : ILogContextAccessor
    {
        private readonly IHttpContextAccessor _http;
        private readonly ICurrentUser _currentUser;

        public HttpLogContextAccessor(IHttpContextAccessor http, ICurrentUser currentUser)
        {
            _http = http;
            _currentUser = currentUser;
        }

        public LogContext Get()
        {
            var ctx = _http.HttpContext;

            return new LogContext
            {
                CorrelationId = ctx?.TraceIdentifier,

                UserId = _currentUser.UserId,
                IsAuthenticated = _currentUser.IsAuthenticated,
                Roles = _currentUser.Roles,
                Permissions = _currentUser.Permissions,

                IpAddress = ctx?.Connection.RemoteIpAddress?.ToString(),
                UserAgent = ctx?.Request.Headers.TryGetValue("User-Agent", out var ua) == true ? ua.ToString() : null,

                RequestPath = ctx?.Request.Path.ToString(),
                HttpMethod = ctx?.Request.Method
            };
        }

    }

}
