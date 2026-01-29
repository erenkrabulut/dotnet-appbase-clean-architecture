using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using core.Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.UserLogins.Commands.LinkLoginToUser
{
    public sealed class LinkLoginToUserCommandHandler : IRequestHandler<LinkLoginToUserCommand, Response>
    {
        private readonly IUserLoginService _userLoginService;

        public LinkLoginToUserCommandHandler(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        public async Task<Response> Handle(LinkLoginToUserCommand request, CancellationToken cancellationToken)
        {
            UserLogin userLogin = new UserLogin(request.UserId, request.Provider, request.ProviderKey, request.ProviderValue);
            await _userLoginService.LinkAsync(
                request.UserId,
                request.Provider,
                request.ProviderKey,
                request.ProviderValue
                , cancellationToken);

            return Response.Ok();
        }
    }
}
