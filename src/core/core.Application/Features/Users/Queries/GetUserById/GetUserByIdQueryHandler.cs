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

namespace core.Application.Features.Users.Queries.GetUserById
{
    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Response<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetByIdAsync(request.Id, cancellationToken);

            UserDto dto = _mapper.Map<UserDto>(user);

            return Response<UserDto>.Ok(dto);
        }
    }
}
