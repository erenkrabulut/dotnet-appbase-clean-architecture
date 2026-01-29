using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Dtos;
using core.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Roles.Commands.CreateRole
{
    public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Response<RoleDto>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<Response<RoleDto>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = new Role(request.Name);

            Role created = await _roleService.CreateAsync(role, cancellationToken);
            RoleDto dto = _mapper.Map<RoleDto>(created);

            return Response<RoleDto>.Ok(dto);
        }
    }
}
