using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Responses;
using core.Application.Features.Permissions.Constants;
using core.Application.Features.Permissions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Permissions.Queries.GetPermissionByName
{
    public sealed class GetPermissionByNameQuery : IQuery<Response<PermissionDto>>, ISecuredRequest
    {
        public string Name { get; init; } = string.Empty;

        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { PermissionsPermissions.Admin, PermissionsPermissions.Read };
    }
}
