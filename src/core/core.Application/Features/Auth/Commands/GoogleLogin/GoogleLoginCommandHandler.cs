using core.Application.Abstractions.Security.ExternalAuthService;
using core.Application.Abstractions.Security.ExternalLoginService;
using core.Application.Abstractions.Security.Token;
using core.Application.Abstractions.Security.Tokens;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Responses;
using core.Application.Features.Auth.Dtos;
using core.Domain.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Auth.Commands.GoogleLogin
{
    public sealed class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, Response<TokenPairDto>>
    {
        private readonly IExternalAuthService _externalAuthService;
        private readonly IUserService _userService;
        private readonly IIdentityClaimsService _identityClaimsService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ITokenService _tokenService;

        public GoogleLoginCommandHandler(
            IExternalAuthService externalAuthService,
            IUserService userService,
            IIdentityClaimsService identityClaimsService,
            IRefreshTokenService refreshTokenService,
            ITokenService tokenService)
        {
            _externalAuthService = externalAuthService;
            _userService = userService;
            _identityClaimsService = identityClaimsService;
            _refreshTokenService = refreshTokenService;
            _tokenService = tokenService;
        }

        public async Task<Response<TokenPairDto>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            var ip = request.IpAddress ?? string.Empty;

            var result = await _externalAuthService.GoogleLoginAsync(
                idToken: request.IdToken,
                ipAddress: ip,
                cancellationToken: cancellationToken);

            if (!result.Succeeded || result.Status == ExternalAuthStatus.Failed)
                throw new AuthorizationException(Errors.Auth.NotAuthorized);

            if (result.Status == ExternalAuthStatus.NewUserRequired)
                throw new NotFoundException(Errors.Identity.UserNotFound);

            if (result.UserId is null)
                throw new AuthorizationException(Errors.Auth.NotAuthorized);

            var user = await _userService.TryGetByIdAsync(result.UserId.Value, cancellationToken);
            if (user is null)
                throw new NotFoundException(Errors.Identity.UserNotFound);

            if (!user.IsActive)
                throw new AuthorizationException(Errors.Auth.NotAuthorized);

            var snapshot = await _identityClaimsService.GetSnapshotAsync(user.Id, cancellationToken);

            var claims = new JwtClaims
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = snapshot.Roles,
                Permissions = snapshot.Permissions
            };

            var access = _tokenService.CreateAccessToken(claims);

            var refreshResult = _tokenService.CreateRefreshToken(user.Id, ip);

            await _refreshTokenService.CreateAsync(refreshResult.Entity, cancellationToken);

            var dto = new TokenPairDto(
                accessToken: access.Token,
                accessTokenExpiresAt: access.Expires,
                refreshToken: refreshResult.RawToken,
                refreshTokenExpiresAt: refreshResult.ExpiresAt);

            return Response<TokenPairDto>.Ok(dto);
        }
    }
}
