using core.Application.Abstractions.Services.Identity;
using FluentValidation;

namespace core.Application.Features.UserRoles.Commands.RemoveRoleFromUser
{
    public sealed class RemoveRoleFromUserCommandValidator : AbstractValidator<RemoveRoleFromUserCommand>
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;

        public RemoveRoleFromUserCommandValidator(
            IUserService userService,
            IRoleService roleService,
            IUserRoleService userRoleService)
        {
            _userService = userService;
            _roleService = roleService;
            _userRoleService = userRoleService;

            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.RoleId).NotEmpty();

            RuleFor(x => x)
                .MustAsync(UserExistsAsync)
                .WithMessage("User does not exist.");

            RuleFor(x => x)
                .MustAsync(RoleExistsAsync)
                .WithMessage("Role does not exist.");

            RuleFor(x => x)
                .MustAsync(AssignmentExistsAsync)
                .WithMessage("Role is not assigned to user.");
        }

        private async Task<bool> UserExistsAsync(RemoveRoleFromUserCommand command, CancellationToken ct)
        {
            var user = await _userService.TryGetByIdAsync(command.UserId, ct);
            return user is not null;
        }

        private async Task<bool> RoleExistsAsync(RemoveRoleFromUserCommand command, CancellationToken ct)
        {
            var role = await _roleService.TryGetByIdAsync(command.RoleId, ct);
            return role is not null;
        }

        private async Task<bool> AssignmentExistsAsync(RemoveRoleFromUserCommand command, CancellationToken ct)
        {
            bool assigned = await _userRoleService.IsRoleAssignedToUserAsync(command.UserId, command.RoleId, ct);
            return assigned;
        }
    }
}
