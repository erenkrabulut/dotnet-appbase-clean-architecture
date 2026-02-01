using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Roles.Commands.DeleteRole
{
    public sealed record DeleteRoleCommand(Guid Id, bool IsSoftDelete = true)
    : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { RolesPermissions.Admin, RolesPermissions.Delete, RolesPermissions.Write };
    }
}
