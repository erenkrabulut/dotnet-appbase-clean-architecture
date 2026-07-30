using core.Application.Abstractions.Logging;
using core.Application.Abstractions.Paging;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Application.Features.Users.Constants;
using core.Application.Features.Users.Dtos;

namespace core.Application.Features.Users.Queries.GetUsersPage
{
    public sealed record GetUsersPageQuery(PageRequest? PageRequest = null)
    : IPagedRequest<Response<PageResponse<UserDto>>>, ISecuredRequest, ILoggableRequest
    {
        public PageRequest PageRequest { get; init; } = PageRequest ?? new PageRequest();

        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { UsersPermissions.Admin, UsersPermissions.Read };
    }
}
