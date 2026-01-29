using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Dtos;
using core.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Roles.Queries.GetRolesPage
{
    public sealed class GetRolesPageQueryHandler : IRequestHandler<GetRolesPageQuery, Response<PageResponse<RoleDto>>>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public GetRolesPageQueryHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        public async Task<Response<PageResponse<RoleDto>>> Handle(GetRolesPageQuery request, CancellationToken cancellationToken)
        {
            PageResponse<Role> page = await _roleService.GetPageAsync(request.PageRequest, cancellationToken);
            PageResponse<RoleDto> dtoPage = _mapper.Map<PageResponse<RoleDto>>(page);

            return Response<PageResponse<RoleDto>>.Ok(dtoPage);
        }
    }
}
