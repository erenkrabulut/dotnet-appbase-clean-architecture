using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Dtos;
using core.Domain.Entities.Identity;
using MediatR;

namespace core.Application.Features.Roles.Queries.GetRoleByName
{
    public sealed class GetRoleByNameQueryHandler : IRequestHandler<GetRoleByNameQuery, Response<RoleDto>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public GetRoleByNameQueryHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<Response<RoleDto>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            Role role = await _roleService.GetByNameAsync(request.Name, cancellationToken);
            RoleDto dto = _mapper.Map<RoleDto>(role);

            return Response<RoleDto>.Ok(dto);
        }
    }
}
