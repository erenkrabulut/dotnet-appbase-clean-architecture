using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using MediatR;

namespace core.Application.Features.UserRoles.Commands.ReplaceUserRoles
{
    public sealed class ReplaceUserRolesCommandHandler : IRequestHandler<ReplaceUserRolesCommand, Response>
    {
        private readonly IUserRoleService _userRoleService;

        public ReplaceUserRolesCommandHandler(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        public async Task<Response> Handle(ReplaceUserRolesCommand request, CancellationToken cancellationToken)
        {
            IReadOnlyCollection<System.Guid> distinctRoleIds = request.RoleIds.Distinct().ToArray();

            await _userRoleService.ReplaceUserRolesAsync(request.UserId, distinctRoleIds, cancellationToken);

            return Response.Ok();
        }
    }
}
