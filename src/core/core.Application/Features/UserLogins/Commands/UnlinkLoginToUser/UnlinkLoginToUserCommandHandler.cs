using core.Application.Abstractions.Services.Identity;
using core.Application.Common.Responses;
using MediatR;

namespace core.Application.Features.UserLogins.Commands.UnlinkLoginToUser
{
    public sealed class UnlinkLoginCommandHandler : IRequestHandler<UnlinkLoginCommand, Response>
    {
        private readonly IUserLoginService _userLoginService;

        public UnlinkLoginCommandHandler(IUserLoginService userLoginService)
        {
            _userLoginService = userLoginService;
        }

        public async Task<Response> Handle(UnlinkLoginCommand request, CancellationToken cancellationToken)
        {
            await _userLoginService.UnlinkAsync(request.UserId, request.Provider, cancellationToken);
            return Response.Ok();
        }
    }
}
