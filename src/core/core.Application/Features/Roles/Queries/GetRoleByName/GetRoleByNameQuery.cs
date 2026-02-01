using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Paging;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Constants;
using core.Application.Features.Roles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Roles.Queries.GetRoleByName
{
    public sealed record GetRoleByNameQuery(string Name)
    : IQuery<Response<RoleDto>>, ISecuredRequest
    {
        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { RolesPermissions.Admin, RolesPermissions.Read };
    }


}
