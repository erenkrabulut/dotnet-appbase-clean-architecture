using core.Application.Abstractions.Security.ExternalAuthService;
using core.Application.Abstractions.Security.ExternalLoginService;
using core.Application.Abstractions.Security.Token;
using core.Application.Abstractions.Security.Tokens;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Responses;
using core.Application.Features.Auth.Dtos;
using core.Domain.Entities.Identity;
using core.Domain.Errors;
using core.Domain.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Auth.Commands.GoogleRegister
{
    public sealed class GoogleRegisterCommandHandler : IRequestHandler<GoogleRegisterCommand, Response<TokenPairDto>>
    {
        private readonly IExternalAuthService _externalAuthService;
        private readonly IUserService _userService;
        private readonly IUserLoginService _userLoginService;
        private readonly IIdentityClaimsService _identityClaimsService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ITokenService _tokenService;

        public GoogleRegisterCommandHandler(
            IExternalAuthService externalAuthService,
            IUserService userService,
            IUserLoginService userLoginService,
            IIdentityClaimsService identityClaimsService,
            IRefreshTokenService refreshTokenService,
            ITokenService tokenService)
        {
            _externalAuthService = externalAuthService;
            _userService = userService;
            _userLoginService = userLoginService;
            _identityClaimsService = identityClaimsService;
            _refreshTokenService = refreshTokenService;
            _tokenService = tokenService;
        }

        public async Task<Response<TokenPairDto>> Handle(GoogleRegisterCommand request, CancellationToken cancellationToken)
        {
            var ip = request.IpAddress ?? string.Empty;

            var result = await _externalAuthService.GoogleLoginAsync(
                idToken: request.IdToken,
                ipAddress: ip,
                cancellationToken: cancellationToken);

            if (!result.Succeeded || result.Status == ExternalAuthStatus.Failed)
                throw new AuthorizationException(AuthErrors.NotAuthorized);

            if (result.Status != ExternalAuthStatus.NewUserRequired)
                throw new ConflictException(IdentityErrors.User.EmailAlreadyExists);

            if (string.IsNullOrWhiteSpace(result.Email))
                throw new AuthorizationException(AuthErrors.NotAuthorized);

            if (string.IsNullOrWhiteSpace(result.ProviderKey))
                throw new AuthorizationException(AuthErrors.NotAuthorized);

            await _userService.EnsureEmailUniqueAsync(result.Email, cancellationToken);

            var user = new User(
                firstName: request.FirstName,
                lastName: request.LastName,
                email: result.Email)
            {
                IsActive = true
            };

            await _userService.CreateAsync(user, cancellationToken);

            var login = new UserLogin(
                userId: user.Id,
                provider: AuthenticationProvider.Google,
                providerKey: result.ProviderKey,
                providerValue: result.Email);

            await _userLoginService.CreateAsync(login, cancellationToken);

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
