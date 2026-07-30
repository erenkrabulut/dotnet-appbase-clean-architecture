using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Constants;
using core.Application.Features.Roles.Dtos;

namespace core.Application.Features.Roles.Commands.UpdateRole
{
    public sealed record UpdateRoleCommand(Guid Id, string Name)
    : ICommand<Response<RoleDto>>, ISecuredRequest, ITransactionalRequest
    {
        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { RolesPermissions.Admin, RolesPermissions.Update, RolesPermissions.Write };
    }
}
