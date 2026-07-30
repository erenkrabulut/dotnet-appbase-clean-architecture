using core.Application.Features.UserLogins.Commands.LinkLoginToUser;
using core.Application.Features.UserLogins.Commands.UnlinkLoginToUser;
using core.Application.Features.UserLogins.Queries.GetUserLoginsByUserId;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/users/{userId:guid}/logins")]
    [ApiController]
    public sealed class UserLoginsController : BaseController
    {
        public UserLoginsController() { }

        [HttpGet]
        public async Task<IActionResult> GetUserLogins([FromRoute] Guid userId)
        {
            var response = await Mediator.Send(new GetUserLoginsByUserIdQuery(userId));
            return ToActionResult(response);
        }

        [HttpPost("link")]
        public async Task<IActionResult> Link([FromRoute] Guid userId, [FromBody] LinkLoginToUserCommand command)
        {
            var enrichedCommand = command with { UserId = userId };
            var response = await Mediator.Send(enrichedCommand);
            return ToActionResult(response);
        }

        [HttpPost("unlink")]
        public async Task<IActionResult> Unlink([FromRoute] Guid userId, [FromBody] UnlinkLoginCommand command)
        {
            var enrichedCommand = command with { UserId = userId };
            var response = await Mediator.Send(enrichedCommand);
            return ToActionResult(response);
        }
    }
}
