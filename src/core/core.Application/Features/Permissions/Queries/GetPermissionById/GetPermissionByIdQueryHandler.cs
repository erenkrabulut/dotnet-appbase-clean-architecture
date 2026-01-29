using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.Permissions.Dtos;
using core.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Permissions.Queries.GetPermissionById
{
    public sealed class GetPermissionByIdQueryHandler : IRequestHandler<GetPermissionByIdQuery, Response<PermissionDto>>
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public GetPermissionByIdQueryHandler(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }

        public async Task<Response<PermissionDto>> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            Permission permission = await _permissionService.GetByIdAsync(request.Id, cancellationToken);
            PermissionDto dto = _mapper.Map<PermissionDto>(permission);

            return Response<PermissionDto>.Ok(dto);
        }
    }
}
