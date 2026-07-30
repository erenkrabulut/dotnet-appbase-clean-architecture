using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Constants;
using core.Application.Features.Roles.Dtos;

namespace core.Application.Features.Roles.Commands.CreateRole
{
    public sealed record CreateRoleCommand(string Name)
    : ICommand<Response<RoleDto>>, ISecuredRequest, ITransactionalRequest
    {
        IReadOnlyCollection<string> ISecuredRequest.Permissions =>
            new[] { RolesPermissions.Admin, RolesPermissions.Add, RolesPermissions.Write };
    }
}
