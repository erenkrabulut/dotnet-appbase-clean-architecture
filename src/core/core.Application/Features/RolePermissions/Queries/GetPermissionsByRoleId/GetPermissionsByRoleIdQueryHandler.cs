using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.Permissions.Dtos;
using core.Application.Features.RolePermissions.Dtos;
using core.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.RolePermissions.Queries.GetPermissionsByRoleId
{
    public sealed class GetPermissionsByRoleIdQueryHandler
        : IRequestHandler<GetPermissionsByRoleIdQuery, Response<RolePermissionsSnapshotDto>>
    {
        private readonly IRoleService _roleService;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IMapper _mapper;

        public GetPermissionsByRoleIdQueryHandler(
            IRoleService roleService,
            IRolePermissionService rolePermissionService,
            IMapper mapper)
        {
            _roleService = roleService;
            _rolePermissionService = rolePermissionService;
            _mapper = mapper;
        }

        public async Task<Response<RolePermissionsSnapshotDto>> Handle(GetPermissionsByRoleIdQuery request, CancellationToken cancellationToken)
        {
            await _roleService.GetByIdAsync(request.RoleId, cancellationToken);

            IReadOnlyList<Permission> permissions = await _rolePermissionService.GetPermissionsByRoleIdAsync(request.RoleId, cancellationToken);

            List<PermissionDto> permissionDtos = permissions.Select(p => _mapper.Map<PermissionDto>(p)).ToList();

            var snapshot = new RolePermissionsSnapshotDto
            {
                RoleId = request.RoleId,
                Permissions = permissionDtos
            };

            return Response<RolePermissionsSnapshotDto>.Ok(snapshot);
        }
    }
}
