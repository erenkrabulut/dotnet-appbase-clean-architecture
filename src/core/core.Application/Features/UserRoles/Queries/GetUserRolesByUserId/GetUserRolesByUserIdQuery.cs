using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Responses;
using core.Application.Features.UserRoles.Constants;
using core.Application.Features.UserRoles.Dtos;

namespace core.Application.Features.UserRoles.Queries.GetUserRolesByUserId
{
    public sealed record GetUserRolesByUserIdQuery(Guid UserId)
    : IQuery<Response<UserRolesSnapshotDto>>, ISecuredRequest
    {
        public IReadOnlyCollection<string> Permissions =>
            new[] { UserRolesPermissions.Admin, UserRolesPermissions.Read };
    }
}
