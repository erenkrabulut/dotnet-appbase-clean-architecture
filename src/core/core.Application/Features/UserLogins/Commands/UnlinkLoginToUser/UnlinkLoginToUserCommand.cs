using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.UserLogins.Constants;
using core.Domain.Security;

namespace core.Application.Features.UserLogins.Commands.UnlinkLoginToUser
{
    public sealed record UnlinkLoginCommand(
        Guid UserId,
        AuthenticationProvider Provider
    ) : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        public IReadOnlyCollection<string> Permissions =>
            new[] { UserLoginsPermissions.Admin, UserLoginsPermissions.Delete, UserLoginsPermissions.Write };
    }

}
