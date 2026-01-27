using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Application.Features.Users.Dtos;
using core.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Users.Queries.GetUsersPage
{
    public sealed class GetUsersPageQueryHandler : IRequestHandler<GetUsersPageQuery, Response<PageResponse<UserDto>>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetUsersPageQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Response<PageResponse<UserDto>>> Handle(GetUsersPageQuery request, CancellationToken cancellationToken)
        {
            PageResponse<User> page =
                await _userService.GetPageAsync(request.PageRequest, cancellationToken);

            PageResponse<UserDto> dtoPage = _mapper.Map<PageResponse<UserDto>>(page);

            return Response<PageResponse<UserDto>>.Ok(dtoPage);
        }
    }
}
