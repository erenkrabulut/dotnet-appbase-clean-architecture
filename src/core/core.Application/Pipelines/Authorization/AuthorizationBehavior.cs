using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Security.UserContext;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Domain.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Pipelines.Authorization
{
    public sealed class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ICurrentUser _currentUser;

        public AuthorizationBehavior(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (request is not ISecuredRequest securedRequest)
                return await next();

            if (!_currentUser.IsAuthenticated)
                throw new AuthorizationException(AuthErrors.NotAuthenticated);

            IReadOnlyCollection<string> requiredPermissions = securedRequest.Permissions ?? Array.Empty<string>();

            if (requiredPermissions.Count > 0)
            {
                bool hasPermission = _currentUser.Permissions.Any(userPerm =>
                    requiredPermissions.Any(required =>
                        string.Equals(required, userPerm, StringComparison.OrdinalIgnoreCase)));

                if (!hasPermission)
                    throw new AuthorizationException(AuthErrors.NotAuthorized);
            }

            return await next();
        }
    }
}
