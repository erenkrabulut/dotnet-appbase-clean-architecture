using core.Application.Abstractions.Services.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.RolePermissions.Commands.ReplaceRolePermissions
{
    public sealed class ReplaceRolePermissionsCommandValidator : AbstractValidator<ReplaceRolePermissionsCommand>
    {
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;

        public ReplaceRolePermissionsCommandValidator(IRoleService roleService, IPermissionService permissionService)
        {
            _roleService = roleService;
            _permissionService = permissionService;

            RuleFor(x => x.RoleId).NotEmpty();

            RuleFor(x => x.PermissionIds)
                .NotNull()
                .Must(x => x.Count > 0)
                .WithMessage("PermissionIds must not be empty.");

            RuleFor(x => x.PermissionIds)
                .Must(AllDistinct)
                .WithMessage("PermissionIds must be distinct.");

            RuleFor(x => x)
                .MustAsync(RoleExistsAsync)
                .WithMessage("Role does not exist.");

            RuleFor(x => x)
                .MustAsync(AllPermissionsExistAsync)
                .WithMessage("One or more permissions do not exist.");
        }

        private static bool AllDistinct(List<int> permissionIds)
        {
            return permissionIds.Distinct().Count() == permissionIds.Count;
        }

        private async Task<bool> RoleExistsAsync(ReplaceRolePermissionsCommand command, CancellationToken ct)
        {
            var role = await _roleService.TryGetByIdAsync(command.RoleId, ct);
            return role is not null;
        }

        private async Task<bool> AllPermissionsExistAsync(ReplaceRolePermissionsCommand command, CancellationToken ct)
        {
            foreach (var permissionId in command.PermissionIds.Distinct())
            {
                var permission = await _permissionService.TryGetByIdAsync(permissionId, ct);
                if (permission is null)
                    return false;
            }

            return true;
        }
    }
}
