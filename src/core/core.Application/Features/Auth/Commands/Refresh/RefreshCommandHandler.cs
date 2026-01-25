using core.Application.Abstractions.Logging;
using core.Application.Abstractions.Security.Token;
using core.Application.Abstractions.Security.Tokens;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Responses;
using core.Application.Features.Auth.Dtos;
using core.Domain.Entities.Identity;
using core.Domain.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Auth.Commands.Refresh
{
    public class RefreshCommandHandler : IRequestHandler<RefreshCommand, Response<TokenPairDto>>
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IIdentityClaimsService _identityClaimsService;
        private readonly ILoggerService _loggerService;

        public RefreshCommandHandler(
            IRefreshTokenService refreshTokenService,
            IUserService userService,
            ITokenService tokenService,
            IIdentityClaimsService identityClaimsService,
            ILoggerService loggerService)
        {
            _refreshTokenService = refreshTokenService;
            _userService = userService;
            _tokenService = tokenService;
            _identityClaimsService = identityClaimsService;
            _loggerService = loggerService;
        }

        public async Task<Response<TokenPairDto>> Handle(RefreshCommand request, CancellationToken cancellationToken = default) {
            string presentedHash = _tokenService.HashRefreshToken(request.RefreshToken);

            var existing = await _refreshTokenService.TryGetByTokenHashAsync(presentedHash, cancellationToken);
            if (existing == null)
                throw new NotFoundException(Errors.Identity.RefreshTokenNotFound);

            if (existing.IsRevoked) { 
                await _refreshTokenService.RevokeAllByUserIdAsync(
                    userId: existing.UserId,
                    ipAddress: request.IpAddress ?? string.Empty,
                    reason: "ReplayDetected",
                    ct: cancellationToken);

                _loggerService.LogWarning("Refresh token replay detected. Revoked all sessions.", new
                {
                    UserId = existing.UserId,
                    IpAddress = request.IpAddress
                });

                throw new AuthorizationException(Errors.Auth.NotAuthorized);
            }
            

            if (existing.IsExpired)
                throw new AuthorizationException(Errors.Auth.NotAuthorized);

            var user = await _userService.TryGetByIdAsync(existing.UserId, cancellationToken);

            if(user == null)
                throw new NotFoundException(Errors.Identity.UserNotFound);
            
            if(!user.IsActive)
                throw new AuthorizationException(Errors.Auth.NotAuthorized);



            var tokenResult = _tokenService.CreateRefreshToken(user.Id, ipAddress: request.IpAddress ?? string.Empty);

            existing.ReplacedByToken = tokenResult.TokenHash;

            await _refreshTokenService.RevokeAsync(
                tokenHash: existing.Token,
                ipAddress: request.IpAddress,
                reason: "Rotated",
                replacedByTokenHash: existing.ReplacedByToken,
                ct: cancellationToken);


            await _refreshTokenService.CreateAsync(tokenResult.Entity, cancellationToken);

            var snapshot = await _identityClaimsService.GetSnapshotAsync(user.Id, cancellationToken);

            var claims = new JwtClaims
            {
                UserId = user.Id,
                Email = user.Email,
                Roles = snapshot.Roles,
                Permissions = snapshot.Permissions,
            };

            var access = _tokenService.CreateAccessToken(claims);

            var dto = new TokenPairDto(
                accessToken: access.Token,
                accessTokenExpiresAt: access.Expires,
                refreshToken: tokenResult.RawToken,
                refreshTokenExpiresAt: tokenResult.ExpiresAt);

            return Response<TokenPairDto>.Ok(dto);

        }

        
    }
}
