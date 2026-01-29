using core.Application.Abstractions.Security.Hashing;
using core.Application.Abstractions.Security.Token;
using core.Application.Abstractions.Security.Tokens;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Responses;
using core.Application.Features.Auth.Dtos;
using core.Domain.Errors;
using core.Domain.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Auth.Commands.Login
{
    public sealed class LoginCommandHandler : IRequestHandler<LoginCommand, Response<TokenPairDto>>
    {

        private readonly IUserService _userService;
        private readonly IUserLoginService _userLoginService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IIdentityClaimsService _identityClaimsService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(
            IUserService userService,
            IUserLoginService userLoginService,
            IPasswordHasher passwordHasher,
            IIdentityClaimsService identityClaimsService,
            IRefreshTokenService refreshTokenService,
            ITokenService tokenService)
        {
            _userService = userService;
            _userLoginService = userLoginService;
            _passwordHasher = passwordHasher;
            _identityClaimsService = identityClaimsService;
            _refreshTokenService = refreshTokenService;
            _tokenService = tokenService;
        }

        public async Task<Response<TokenPairDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.TryGetByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                throw new AuthorizationException(AuthErrors.NotAuthorized);

            if (!user.IsActive)
                throw new AuthorizationException(AuthErrors.NotAuthorized);

            if(user.PasswordHash is null)
                throw new AuthorizationException(AuthErrors.NotAuthorized);

            var valid = _passwordHasher.Verify(request.Password, user.PasswordHash);
            if (!valid)
                throw new AuthorizationException(AuthErrors.NotAuthorized);

            var snapshot = await _identityClaimsService.GetSnapshotAsync(user.Id, cancellationToken);

            var claims = new JwtClaims
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = snapshot.Roles,
                Permissions = snapshot.Permissions
            };

            var access = _tokenService.CreateAccessToken(claims);

            var refreshResult = _tokenService.CreateRefreshToken(user.Id, request.IpAddress ?? string.Empty);

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
