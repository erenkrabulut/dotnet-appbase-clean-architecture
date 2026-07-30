using core.Application.Abstractions.Paging;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Constants;
using core.Application.Features.Roles.Dtos;

namespace core.Application.Features.Roles.Queries.GetRolesPage
{
    public sealed record GetRolesPageQuery(PageRequest? PageRequest = null)
        : IPagedRequest<Response<PageResponse<RoleDto>>>, ISecuredRequest
    {
        public PageRequest PageRequest { get; init; } = PageRequest ?? new PageRequest();

        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { RolesPermissions.Admin, RolesPermissions.Read };
    }
}
