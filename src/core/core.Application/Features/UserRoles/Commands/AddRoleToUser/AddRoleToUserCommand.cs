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

namespace core.Application.Features.UserRoles.Commands.AddRoleToUser
{
    public sealed class AddRoleToUserCommand : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        public Guid UserId { get; init; }
        public Guid RoleId { get; init; }
        public IReadOnlyCollection<string> Permissions =>
                 new[] { UserRolesPermissions.Admin, UserRolesPermissions.Write, UserRolesPermissions.Add };
    }
}
