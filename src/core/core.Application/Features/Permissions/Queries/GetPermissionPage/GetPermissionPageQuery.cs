using core.Application.Abstractions.Paging;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Application.Features.Permissions.Constants;
using core.Application.Features.Permissions.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Permissions.Queries.GetPermissionPage
{
    public sealed class GetPermissionsPageQuery : IPagedRequest<Response<PageResponse<PermissionDto>>>, ISecuredRequest
    {
        public PageRequest PageRequest { get; init; } = new();
        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { PermissionsPermissions.Admin, PermissionsPermissions.Read };
    }
}
