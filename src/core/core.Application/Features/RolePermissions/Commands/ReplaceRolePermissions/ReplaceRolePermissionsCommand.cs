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

namespace core.Application.Features.RolePermissions.Commands.ReplaceRolePermissions
{
    public sealed class ReplaceRolePermissionsCommand : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        public Guid RoleId { get; init; }

        public List<int> PermissionIds { get; init; } = new();

        public IReadOnlyCollection<string> Permissions =>
            new[] { RolePermissionsPermissions.Admin, RolePermissionsPermissions.Update , RolePermissionsPermissions.Write };
    }
}
