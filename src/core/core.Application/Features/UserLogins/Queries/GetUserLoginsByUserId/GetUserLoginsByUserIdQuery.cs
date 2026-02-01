using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Common.Responses;
using core.Application.Features.UserLogins.Constants;
using core.Application.Features.UserLogins.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserLogins.Queries.GetUserLoginsByUserId
{
    public sealed record GetUserLoginsByUserIdQuery(Guid UserId)
    : IQuery<Response<UserLoginsSnapshotDto>>, ISecuredRequest
    {
        public IReadOnlyCollection<string> Permissions =>
            new[] { UserLoginsPermissions.Admin, UserLoginsPermissions.Read };
    }
}
