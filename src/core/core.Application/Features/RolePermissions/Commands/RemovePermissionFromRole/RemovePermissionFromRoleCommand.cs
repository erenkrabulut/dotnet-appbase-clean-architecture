using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.RolePermissions.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.RolePermissions.Commands.RemovePermissionFromRole
{
    public sealed class RemovePermissionFromRoleCommand : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        public Guid RoleId { get; init; }
        public int PermissionId { get; init; }

        public IReadOnlyCollection<string> Permissions =>
            new[] { RolePermissionsPermissions.Admin, RolePermissionsPermissions.Delete, RolePermissionsPermissions.Write };
    }
}
