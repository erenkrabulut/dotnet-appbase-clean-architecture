using core.Application.Abstractions.Security.Hashing;
using core.Application.Abstractions.Security.Token;
using core.Application.Abstractions.Security.Tokens;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.Auth.Dtos;
using core.Domain.Constants;
using core.Domain.Entities.Identity;
using core.Domain.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Auth.Commands.Register
{
    public sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Response<TokenPairDto>>
    {

        private readonly IUserService _userService;
        private readonly IUserLoginService _userLoginService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IIdentityClaimsService _identityClaimsService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ITokenService _tokenService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;

        public RegisterCommandHandler(
            IUserService userService,
            IUserLoginService userLoginService,
            IPasswordHasher passwordHasher,
            IIdentityClaimsService identityClaimsService,
            IRefreshTokenService refreshTokenService,
            ITokenService tokenService,
            IRoleService roleService,
            IUserRoleService userRoleService)
        {
            _userService = userService;
            _userLoginService = userLoginService;
            _passwordHasher = passwordHasher;
            _identityClaimsService = identityClaimsService;
            _refreshTokenService = refreshTokenService;
            _tokenService = tokenService;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }

        public async Task<Response<TokenPairDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _userService.EnsureEmailUniqueAsync(request.Email, cancellationToken);

            var passwordHash = _passwordHasher.Hash(request.Password);

            var user = new User(
                firstName: request.FirstName,
                lastName: request.LastName,
                email: request.Email,
                passwordHash: passwordHash
                )
            {
                IsActive = true
            };

            user = await _userService.CreateAsync(user, cancellationToken);

            
            var defaultUserRole = await _roleService.TryGetByNameAsync(RoleNames.User, cancellationToken);

            if (defaultUserRole != null)
            {
                bool alreadyLinked = await _userRoleService.IsRoleAssignedToUserAsync(user.Id, defaultUserRole.Id, cancellationToken);
                if (!alreadyLinked)
                {
                    await _userRoleService.AddRoleToUserAsync(user.Id, defaultUserRole.Id, cancellationToken);
                }
            }


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
