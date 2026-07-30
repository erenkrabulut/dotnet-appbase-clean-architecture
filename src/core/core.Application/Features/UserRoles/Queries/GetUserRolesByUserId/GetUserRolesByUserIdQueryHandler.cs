using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Dtos;
using core.Application.Features.UserRoles.Dtos;
using core.Domain.Entities.Identity;
using MediatR;

namespace core.Application.Features.UserRoles.Queries.GetUserRolesByUserId
{
    public sealed class GetUserRolesByUserIdQueryHandler : IRequestHandler<GetUserRolesByUserIdQuery, Response<UserRolesSnapshotDto>>
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;
        private readonly IMapper _mapper;

        public GetUserRolesByUserIdQueryHandler(
            IUserService userService,
            IUserRoleService userRoleService,
            IMapper mapper)
        {
            _userService = userService;
            _userRoleService = userRoleService;
            _mapper = mapper;
        }

        public async Task<Response<UserRolesSnapshotDto>> Handle(GetUserRolesByUserIdQuery request, CancellationToken cancellationToken)
        {
            await _userService.GetByIdAsync(request.UserId, cancellationToken);

            IReadOnlyList<Role> roles = await _userRoleService.GetRolesByUserIdAsync(request.UserId, cancellationToken);

            List<RoleDto> roleDtos = roles.Select(r => _mapper.Map<RoleDto>(r)).ToList();

            var snapshot = new UserRolesSnapshotDto
            {
                UserId = request.UserId,
                Roles = roleDtos
            };

            return Response<UserRolesSnapshotDto>.Ok(snapshot);
        }
    }
}
