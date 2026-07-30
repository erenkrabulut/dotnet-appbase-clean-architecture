using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Dtos;
using core.Domain.Entities.Identity;
using MediatR;

namespace core.Application.Features.Roles.Queries.GetRoleById
{
    public sealed class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Response<RoleDto>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public GetRoleByIdQueryHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<Response<RoleDto>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            Role role = await _roleService.GetByIdAsync(request.Id, cancellationToken);
            RoleDto dto = _mapper.Map<RoleDto>(role);

            return Response<RoleDto>.Ok(dto);
        }
    }
}
