using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Logging;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Responses;
using core.Application.Features.Users.Constants;
using core.Application.Features.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Users.Queries.GetUserById
{
    public sealed class GetUserByIdQuery : IQuery<Response<UserDto>>, ISecuredRequest, ILoggableRequest
    {
        public Guid Id { get; init; }

        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { UsersPermissions.Admin, UsersPermissions.Read };
    }
}
