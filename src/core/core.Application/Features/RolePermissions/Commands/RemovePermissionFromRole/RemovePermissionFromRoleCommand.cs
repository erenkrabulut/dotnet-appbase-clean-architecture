using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.RolePermissions.Constants;

namespace core.Application.Features.RolePermissions.Commands.RemovePermissionFromRole
{
    public sealed record RemovePermissionFromRoleCommand(Guid RoleId, int PermissionId)
    : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        public IReadOnlyCollection<string> Permissions =>
            new[] { RolePermissionsPermissions.Admin, RolePermissionsPermissions.Delete, RolePermissionsPermissions.Write };
    }
}
