using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.Users.Dtos;
using core.Domain.Entities.Identity;
using MediatR;

namespace core.Application.Features.Users.Queries.GetUserByEmail
{
    public sealed class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, Response<UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUserByEmailQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Response<UserDto>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            User user = await _userService.GetByEmailAsync(request.Email, cancellationToken);

            UserDto dto = _mapper.Map<UserDto>(user);

            return Response<UserDto>.Ok(dto);
        }
    }
}
