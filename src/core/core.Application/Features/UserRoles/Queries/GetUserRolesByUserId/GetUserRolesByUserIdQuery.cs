using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Responses;
using core.Application.Features.UserRoles.Constants;
using core.Application.Features.UserRoles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserRoles.Queries.GetUserRolesByUserId
{
    public sealed class GetUserRolesByUserIdQuery : IQuery<Response<UserRolesSnapshotDto>>, ISecuredRequest
    {
        public Guid UserId { get; init; }

        public IReadOnlyCollection<string> Permissions => 
            new[] {UserRolesPermissions.Admin, UserRolesPermissions.Read};
    }
}
