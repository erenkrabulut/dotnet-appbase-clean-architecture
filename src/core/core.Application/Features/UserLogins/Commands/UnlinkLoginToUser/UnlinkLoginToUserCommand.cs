using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.UserLogins.Constants;
using core.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserLogins.Commands.UnlinkLoginToUser
{
    public sealed class UnlinkLoginCommand : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        public Guid UserId { get; init; }

        public AuthenticationProvider Provider { get; init; }

        public IReadOnlyCollection<string> Permissions =>
            new[] { UserLoginsPermissions.Admin, UserLoginsPermissions.Delete, UserLoginsPermissions.Write };
    }
}
