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

namespace core.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommand : ICommand<Response<UserDto>>, ISecuredRequest, ITransactionalRequest
    {
        public string Email { get; init; } = string.Empty;

        public string FirstName { get; init; } = string.Empty;

        public string LastName { get; init; } = string.Empty;

        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { UsersPermissions.Admin, UsersPermissions.Write, UsersPermissions.Add };
    }
}
