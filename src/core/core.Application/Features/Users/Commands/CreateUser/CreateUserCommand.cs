using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.Users.Constants;
using core.Application.Features.Users.Dtos;

namespace core.Application.Features.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(string Email, string FirstName, string LastName)
    : ICommand<Response<UserDto>>, ISecuredRequest, ITransactionalRequest
    {
        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { UsersPermissions.Admin, UsersPermissions.Write, UsersPermissions.Add };
    }
}
