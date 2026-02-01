using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.Users.Constants;
using core.Application.Features.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Users.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(Guid Id, string Email, string FirstName, string LastName)
    : ICommand<Response<UserDto>>, ISecuredRequest, ITransactionalRequest
    {
        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { UsersPermissions.Admin, UsersPermissions.Write, UsersPermissions.Update };
    }
}
