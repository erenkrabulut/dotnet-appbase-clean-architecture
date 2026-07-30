using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Responses;
using core.Application.Features.RolePermissions.Constants;
using core.Application.Features.RolePermissions.Dtos;

namespace core.Application.Features.RolePermissions.Queries.GetPermissionsByRoleId
{
    public sealed record GetPermissionsByRoleIdQuery(Guid RoleId)
    : IQuery<Response<RolePermissionsSnapshotDto>>, ISecuredRequest
    {
        public IReadOnlyCollection<string> Permissions =>
            new[] { RolePermissionsPermissions.Admin, RolePermissionsPermissions.Read };
    }
}
