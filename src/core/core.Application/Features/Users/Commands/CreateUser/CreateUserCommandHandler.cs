using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.Users.Dtos;
using core.Domain.Constants;
using core.Domain.Entities.Identity;
using MediatR;

namespace core.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        private readonly IUserRoleService _userRoleService;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper,
            IRoleService roleService,
            IUserRoleService userRoleService)
        {
            _userService = userService;
            _mapper = mapper;
            _roleService = roleService;
            _userRoleService = userRoleService;
        }

        public async Task<Response<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User(firstName: request.FirstName, lastName: request.LastName, email: request.Email);

            User created = await _userService.CreateAsync(user, cancellationToken);

            var defaultUserRole = await _roleService.TryGetByNameAsync(RoleNames.User, cancellationToken);

            if (defaultUserRole != null)
            {
                bool alreadyLinked = await _userRoleService.IsRoleAssignedToUserAsync(created.Id, defaultUserRole.Id, cancellationToken);
                if (!alreadyLinked)
                {
                    await _userRoleService.AddRoleToUserAsync(created.Id, defaultUserRole.Id, cancellationToken);
                }
            }

            UserDto dto = _mapper.Map<UserDto>(created);

            return Response<UserDto>.Ok(dto);
        }
    }
}
