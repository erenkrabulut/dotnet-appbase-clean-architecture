using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.Users.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Users.Commands.DeleteUser
{
    public sealed record DeleteUserCommand : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        public Guid Id { get; init; }

        public bool IsSoftDelete { get; init; } = true;
        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
                    new[] { UsersPermissions.Admin, UsersPermissions.Write, UsersPermissions.Delete };
    }
}
