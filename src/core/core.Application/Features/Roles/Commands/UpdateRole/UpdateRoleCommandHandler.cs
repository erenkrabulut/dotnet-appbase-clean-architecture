using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Dtos;
using core.Domain.Entities.Identity;
using MediatR;

namespace core.Application.Features.Roles.Commands.UpdateRole
{
    public sealed class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Response<RoleDto>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<Response<RoleDto>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            Role role = await _roleService.GetByIdAsync(request.Id, cancellationToken);

            role.Name = request.Name;

            await _roleService.UpdateAsync(role, cancellationToken);

            RoleDto dto = _mapper.Map<RoleDto>(role);
            return Response<RoleDto>.Ok(dto);
        }
    }
}
