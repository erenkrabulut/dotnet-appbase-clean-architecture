using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using MediatR;

namespace core.Application.Features.RolePermissions.Commands.ReplaceRolePermissions
{
    public sealed class ReplaceRolePermissionsCommandHandler : IRequestHandler<ReplaceRolePermissionsCommand, Response>
    {
        private readonly IRolePermissionService _rolePermissionService;

        public ReplaceRolePermissionsCommandHandler(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        public async Task<Response> Handle(ReplaceRolePermissionsCommand request, CancellationToken cancellationToken)
        {
            int[] distinctPermissionIds = request.PermissionIds.Distinct().ToArray();

            await _rolePermissionService.ReplaceRolePermissionsAsync(request.RoleId, distinctPermissionIds, cancellationToken);

            return Response.Ok();
        }
    }
}
