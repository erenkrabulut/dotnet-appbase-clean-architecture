using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Responses;
using core.Application.Features.Permissions.Constants;
using core.Application.Features.Permissions.Dtos;

namespace core.Application.Features.Permissions.Queries.GetPermissionByName
{
    public sealed record GetPermissionByNameQuery(string Name)
    : IQuery<Response<PermissionDto>>, ISecuredRequest
    {
        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { PermissionsPermissions.Admin, PermissionsPermissions.Read };
    }
}
