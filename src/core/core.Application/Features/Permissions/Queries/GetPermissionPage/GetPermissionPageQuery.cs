using core.Application.Abstractions.Paging;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Application.Features.Permissions.Constants;
using core.Application.Features.Permissions.Dtos;

namespace core.Application.Features.Permissions.Queries.GetPermissionPage
{
    public sealed record GetPermissionsPageQuery(PageRequest? PageRequest = null)
    : IPagedRequest<Response<PageResponse<PermissionDto>>>, ISecuredRequest
    {
        public PageRequest PageRequest { get; init; } = PageRequest ?? new();

        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { PermissionsPermissions.Admin, PermissionsPermissions.Read };
    }

}
