using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.UserRoles.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserRoles.Commands.RemoveRoleFromUser
{
    public sealed record RemoveRoleFromUserCommand(Guid UserId, Guid RoleId)
    : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        public IReadOnlyCollection<string> Permissions =>
            new[] { UserRolesPermissions.Admin, UserRolesPermissions.Write, UserRolesPermissions.Delete };
    }
}
