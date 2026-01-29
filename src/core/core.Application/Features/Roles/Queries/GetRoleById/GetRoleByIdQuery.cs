using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Constants;
using core.Application.Features.Roles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Roles.Queries.GetRoleById
{
    public sealed class GetRoleByIdQuery : IQuery<Response<RoleDto>>, ISecuredRequest
    {
        public Guid Id { get; init; }

        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { RolesPermissions.Admin, RolesPermissions.Read };
    }
}
