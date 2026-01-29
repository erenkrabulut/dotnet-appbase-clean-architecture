using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserRoles.Commands.AddRoleToUser
{
    public sealed class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, Response>
    {
        private readonly IUserRoleService _userRoleService;

        public AddRoleToUserCommandHandler(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        public async Task<Response> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            await _userRoleService.AddRoleToUserAsync(request.UserId, request.RoleId, cancellationToken);
            return Response.Ok();
        }
    }
}
