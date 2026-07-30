using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using MediatR;

namespace core.Application.Features.RolePermissions.Commands.RemovePermissionFromRole
{
    public sealed class RemovePermissionFromRoleCommandHandler : IRequestHandler<RemovePermissionFromRoleCommand, Response>
    {
        private readonly IRolePermissionService _rolePermissionService;

        public RemovePermissionFromRoleCommandHandler(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        public async Task<Response> Handle(RemovePermissionFromRoleCommand request, CancellationToken cancellationToken)
        {
            await _rolePermissionService.RemovePermissionFromRoleAsync(request.RoleId, request.PermissionId, cancellationToken);
            return Response.Ok();
        }
    }
}
