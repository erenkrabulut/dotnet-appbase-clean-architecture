using AutoMapper;
using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Application.Features.UserLogins.Dtos;
using core.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserLogins.Queries.GetUserLoginsByUserId
{
    public sealed class GetUserLoginsByUserIdQueryHandler
        : IRequestHandler<GetUserLoginsByUserIdQuery, Response<UserLoginsSnapshotDto>>
    {
        private readonly IUserService _userService;
        private readonly IUserLoginService _userLoginService;
        private readonly IMapper _mapper;

        public GetUserLoginsByUserIdQueryHandler(
            IUserService userService,
            IUserLoginService userLoginService,
            IMapper mapper)
        {
            _userService = userService;
            _userLoginService = userLoginService;
            _mapper = mapper;
        }

        public async Task<Response<UserLoginsSnapshotDto>> Handle(GetUserLoginsByUserIdQuery request, CancellationToken cancellationToken)
        {
            await _userService.GetByIdAsync(request.UserId, cancellationToken);

            IReadOnlyList<UserLogin> logins = await _userLoginService.GetByUserIdAsync(request.UserId, cancellationToken);

            var dto = new UserLoginsSnapshotDto
            {
                UserId = request.UserId,
                Logins = logins.Select(x => _mapper.Map<UserLoginDto>(x)).ToList()
            };

            return Response<UserLoginsSnapshotDto>.Ok(dto);
        }
    }
}
