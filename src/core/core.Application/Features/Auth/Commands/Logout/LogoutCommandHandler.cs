using core.Application.Abstractions.Security.Token;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Responses;
using core.Domain.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Auth.Commands.Logout
{
    public sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, Response>
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ITokenService _tokenService;

        public LogoutCommandHandler(
            IRefreshTokenService refreshTokenService,
            ITokenService tokenService)
        {
            _refreshTokenService = refreshTokenService;
            _tokenService = tokenService;
        }

        public async Task<Response> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var tokenHash = _tokenService.HashRefreshToken(request.RefreshToken);

            var existing = await _refreshTokenService.TryGetByTokenHashAsync(tokenHash, cancellationToken);
            if (existing is null)
                throw new NotFoundException(IdentityErrors.RefreshToken.NotFound);

            if (existing.IsRevoked)
                return Response.Ok();

            await _refreshTokenService.RevokeAsync(
                tokenHash: existing.Token,
                ipAddress: request.IpAddress,
                reason: "Logout",
                replacedByTokenHash: null,
                ct: cancellationToken);

            return Response.Ok();
        }
    }
}
