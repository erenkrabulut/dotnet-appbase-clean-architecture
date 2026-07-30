using core.Application.Abstractions.Security.Hashing;
using core.Application.Abstractions.Security.UserContext;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Exceptions.ExceptionTypes;
using core.Application.Common.Responses;
using core.Domain.Errors;
using MediatR;

namespace core.Application.Features.Auth.Commands.ChangePassword
{
    public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response>
    {
        private readonly ICurrentUser _currentUser;
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRefreshTokenService _refreshTokenService;

        public ChangePasswordCommandHandler(
            ICurrentUser currentUser,
            IUserService userService,
            IPasswordHasher passwordHasher,
            IRefreshTokenService refreshTokenService)
        {
            _currentUser = currentUser;
            _userService = userService;
            _passwordHasher = passwordHasher;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<Response> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.IsAuthenticated || _currentUser.UserId is null)
                throw new AuthorizationException(AuthErrors.NotAuthenticated);

            var userId = _currentUser.UserId.Value;

            var user = await _userService.TryGetByIdAsync(userId, cancellationToken);
            if (user is null)
                throw new NotFoundException(IdentityErrors.User.NotFound);

            if (!user.IsActive)
                throw new AuthorizationException(AuthErrors.NotAuthorized);

            if (string.IsNullOrWhiteSpace(user.PasswordHash))
                throw new AuthorizationException(AuthErrors.NotAuthorized);

            var valid = _passwordHasher.Verify(request.CurrentPassword, user.PasswordHash);
            if (!valid)
                throw new AuthorizationException(AuthErrors.NotAuthorized);

            user.PasswordHash = _passwordHasher.Hash(request.NewPassword);

            await _userService.UpdateAsync(user, cancellationToken);

            await _refreshTokenService.RevokeAllByUserIdAsync(
                userId: userId,
                ipAddress: string.Empty,
                reason: "PasswordChanged",
                ct: cancellationToken);

            return Response.Ok();
        }
    }
}
