using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.Users.Dtos;
using core.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Users.Commands.CreateUser
{
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Response<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User(firstName: request.FirstName, lastName: request.LastName, email: request.Email);

            User created = await _userService.CreateAsync(user, cancellationToken);

            UserDto dto = _mapper.Map<UserDto>(created);

            return Response<UserDto>.Ok(dto);
        }
    }
}
