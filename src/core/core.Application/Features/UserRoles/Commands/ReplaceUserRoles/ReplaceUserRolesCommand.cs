using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Security.Authorization;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.UserRoles.Constants;

namespace core.Application.Features.UserRoles.Commands.ReplaceUserRoles
{
    public sealed record ReplaceUserRolesCommand(Guid UserId, List<Guid>? RoleIds = null)
    : ICommand<Response>, ISecuredRequest, ITransactionalRequest
    {
        public List<Guid> RoleIds { get; init; } = RoleIds ?? new();

        public IReadOnlyCollection<string> Permissions =>
            new[] { UserRolesPermissions.Admin, UserRolesPermissions.Write, UserRolesPermissions.Update };
    }
}
