using core.Application.Abstractions.Services.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserRoles.Commands.ReplaceUserRoles
{
    public sealed class ReplaceUserRolesCommandValidator : AbstractValidator<ReplaceUserRolesCommand>
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public ReplaceUserRolesCommandValidator(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;

            RuleFor(x => x.UserId).NotEmpty();

            RuleFor(x => x.RoleIds)
                .NotNull()
                .Must(x => x.Count > 0)
                .WithMessage("RoleIds must not be empty.");

            RuleFor(x => x.RoleIds)
                .Must(AllDistinct)
                .WithMessage("RoleIds must be distinct.");

            RuleFor(x => x)
                .MustAsync(UserExistsAsync)
                .WithMessage("User does not exist.");

            RuleFor(x => x)
                .MustAsync(AllRolesExistAsync)
                .WithMessage("One or more roles do not exist.");
        }

        private static bool AllDistinct(List<System.Guid> roleIds)
        {
            return roleIds.Distinct().Count() == roleIds.Count;
        }

        private async Task<bool> UserExistsAsync(ReplaceUserRolesCommand command, CancellationToken ct)
        {
            var user = await _userService.TryGetByIdAsync(command.UserId, ct);
            return user is not null;
        }

        private async Task<bool> AllRolesExistAsync(ReplaceUserRolesCommand command, CancellationToken ct)
        {
            foreach (var roleId in command.RoleIds.Distinct())
            {
                var role = await _roleService.TryGetByIdAsync(roleId, ct);
                if (role is null)
                    return false;
            }

            return true;
        }
    }
}
