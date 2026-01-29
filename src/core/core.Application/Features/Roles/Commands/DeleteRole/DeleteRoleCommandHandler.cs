using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Roles.Commands.DeleteRole
{
    public sealed class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Response>
    {
        private readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<Response> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            await _roleService.DeleteAsync(request.Id, request.IsSoftDelete, cancellationToken);
            return Response.Ok();
        }
    }
}
