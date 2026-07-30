using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.UserLogins.Constants;
using core.Domain.Security;

namespace core.Application.Features.UserLogins.Commands.LinkLoginToUser
{
    public sealed record LinkLoginToUserCommand(
         Guid UserId,
         AuthenticationProvider Provider,
         string ProviderKey,
         string? ProviderValue
     ) : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        public IReadOnlyCollection<string> Permissions =>
            new[] { UserLoginsPermissions.Admin, UserLoginsPermissions.Add, UserLoginsPermissions.Write };
    }

}
