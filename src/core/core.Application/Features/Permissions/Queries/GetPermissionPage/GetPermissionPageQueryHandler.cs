using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Application.Features.Permissions.Dtos;
using core.Domain.Entities.Identity;
using MediatR;

namespace core.Application.Features.Permissions.Queries.GetPermissionPage
{
    public sealed class GetPermissionsPageQueryHandler : IRequestHandler<GetPermissionsPageQuery, Response<PageResponse<PermissionDto>>>
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public GetPermissionsPageQueryHandler(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }

        public async Task<Response<PageResponse<PermissionDto>>> Handle(GetPermissionsPageQuery request, CancellationToken cancellationToken)
        {
            PageResponse<Permission> page = await _permissionService.GetPageAsync(request.PageRequest, cancellationToken);
            PageResponse<PermissionDto> dtoPage = _mapper.Map<PageResponse<PermissionDto>>(page);

            return Response<PageResponse<PermissionDto>>.Ok(dtoPage);
        }
    }
}
