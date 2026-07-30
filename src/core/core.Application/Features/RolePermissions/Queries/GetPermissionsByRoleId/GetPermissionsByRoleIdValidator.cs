using core.Application.Abstractions.Services.Identity;
using FluentValidation;

namespace core.Application.Features.RolePermissions.Queries.GetPermissionsByRoleId
{
    public sealed class GetPermissionsByRoleIdQueryValidator : AbstractValidator<GetPermissionsByRoleIdQuery>
    {
        private readonly IRoleService _roleService;

        public GetPermissionsByRoleIdQueryValidator(IRoleService roleService)
        {
            _roleService = roleService;

            RuleFor(x => x.RoleId).NotEmpty();

            RuleFor(x => x)
                .MustAsync(RoleExistsAsync)
                .WithMessage("Role does not exist.");
        }

        private async Task<bool> RoleExistsAsync(GetPermissionsByRoleIdQuery query, CancellationToken ct)
        {
            var role = await _roleService.TryGetByIdAsync(query.RoleId, ct);
            return role is not null;
        }
    }
}
