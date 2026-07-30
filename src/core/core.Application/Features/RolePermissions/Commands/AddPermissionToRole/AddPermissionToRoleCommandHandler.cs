using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using MediatR;

namespace core.Application.Features.RolePermissions.Commands.AddPermissionToRole
{
    public sealed class AddPermissionToRoleCommandHandler : IRequestHandler<AddPermissionToRoleCommand, Response>
    {
        private readonly IRolePermissionService _rolePermissionService;

        public AddPermissionToRoleCommandHandler(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        public async Task<Response> Handle(AddPermissionToRoleCommand request, CancellationToken cancellationToken)
        {
            await _rolePermissionService.AddPermissionToRoleAsync(request.RoleId, request.PermissionId, cancellationToken);
            return Response.Ok();
        }
    }
}
