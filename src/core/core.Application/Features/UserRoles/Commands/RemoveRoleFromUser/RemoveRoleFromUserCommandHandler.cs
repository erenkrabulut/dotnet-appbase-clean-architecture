using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserRoles.Commands.RemoveRoleFromUser
{
    public sealed class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, Response>
    {
        private readonly IUserRoleService _userRoleService;

        public RemoveRoleFromUserCommandHandler(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        public async Task<Response> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            await _userRoleService.RemoveRoleFromUserAsync(request.UserId, request.RoleId, cancellationToken);
            return Response.Ok();
        }
    }

}
