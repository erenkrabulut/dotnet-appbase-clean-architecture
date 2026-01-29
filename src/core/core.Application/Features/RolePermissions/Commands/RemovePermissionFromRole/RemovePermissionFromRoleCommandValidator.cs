using core.Application.Abstractions.Services.Identity;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.RolePermissions.Commands.RemovePermissionFromRole
{
    public sealed class RemovePermissionFromRoleCommandValidator : AbstractValidator<RemovePermissionFromRoleCommand>
    {
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;
        private readonly IRolePermissionService _rolePermissionService;

        public RemovePermissionFromRoleCommandValidator(
            IRoleService roleService,
            IPermissionService permissionService,
            IRolePermissionService rolePermissionService)
        {
            _roleService = roleService;
            _permissionService = permissionService;
            _rolePermissionService = rolePermissionService;

            RuleFor(x => x.RoleId).NotEmpty();
            RuleFor(x => x.PermissionId).GreaterThan(0);

            RuleFor(x => x)
                .MustAsync(RoleExistsAsync)
                .WithMessage("Role does not exist.");

            RuleFor(x => x)
                .MustAsync(PermissionExistsAsync)
                .WithMessage("Permission does not exist.");

            RuleFor(x => x)
                .MustAsync(AssignmentExistsAsync)
                .WithMessage("Permission is not assigned to role.");
        }

        private async Task<bool> RoleExistsAsync(RemovePermissionFromRoleCommand command, CancellationToken ct)
        {
            var role = await _roleService.TryGetByIdAsync(command.RoleId, ct);
            return role is not null;
        }

        private async Task<bool> PermissionExistsAsync(RemovePermissionFromRoleCommand command, CancellationToken ct)
        {
            var permission = await _permissionService.TryGetByIdAsync(command.PermissionId, ct);
            return permission is not null;
        }

        private async Task<bool> AssignmentExistsAsync(RemovePermissionFromRoleCommand command, CancellationToken ct)
        {
            return await _rolePermissionService.IsPermissionAssignedToRoleAsync(command.RoleId, command.PermissionId, ct);
        }
    }
}
