using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Logging;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Responses;
using core.Application.Features.Users.Constants;
using core.Application.Features.Users.Dtos;

namespace core.Application.Features.Users.Queries.GetUserByEmail
{
    public sealed record GetUserByEmailQuery(string Email)
    : IQuery<Response<UserDto>>, ISecuredRequest, ILoggableRequest
    {
        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { UsersPermissions.Admin, UsersPermissions.Read };
    }

}
