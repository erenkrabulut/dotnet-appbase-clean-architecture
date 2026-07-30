using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.Users.Constants;

namespace core.Application.Features.Users.Commands.DeleteUser
{
    public sealed record DeleteUserCommand(Guid Id, bool IsSoftDelete = true)
    : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { UsersPermissions.Admin, UsersPermissions.Write, UsersPermissions.Delete };
    }
}
