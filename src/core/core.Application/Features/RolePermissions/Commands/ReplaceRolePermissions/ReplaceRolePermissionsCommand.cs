using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.RolePermissions.Constants;

namespace core.Application.Features.RolePermissions.Commands.ReplaceRolePermissions
{
    public sealed record ReplaceRolePermissionsCommand(Guid RoleId, List<int>? PermissionIds = null)
    : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        public List<int> PermissionIds { get; init; } = PermissionIds ?? new();

        public IReadOnlyCollection<string> Permissions =>
            new[] { RolePermissionsPermissions.Admin, RolePermissionsPermissions.Update, RolePermissionsPermissions.Write };
    }
}
