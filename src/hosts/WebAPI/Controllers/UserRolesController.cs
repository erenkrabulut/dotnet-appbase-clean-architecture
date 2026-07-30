using core.Application.Features.UserRoles.Commands.AddRoleToUser;
using core.Application.Features.UserRoles.Commands.RemoveRoleFromUser;
using core.Application.Features.UserRoles.Commands.ReplaceUserRoles;
using core.Application.Features.UserRoles.Queries.GetUserRolesByUserId;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/users/{userId:guid}/roles")]
    [Authorize]
    [ApiController]
    public sealed class UserRolesController : BaseController
    {
        public UserRolesController() { }

        [HttpGet]
        public async Task<IActionResult> GetUserRoles([FromRoute] Guid userId)
        {
            var response = await Mediator.Send(new GetUserRolesByUserIdQuery(userId));
            return ToActionResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole([FromRoute] Guid userId, [FromBody] AddRoleToUserCommand command)
        {
            var enrichedCommand = command with { UserId = userId };
            var response = await Mediator.Send(enrichedCommand);
            return ToActionResult(response);
        }

        [HttpDelete("{roleId:guid}")]
        public async Task<IActionResult> RemoveRole([FromRoute] Guid userId, Guid roleId)
        {
            var response = await Mediator.Send(new RemoveRoleFromUserCommand(userId, roleId));

            return ToActionResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Replace([FromRoute] Guid userId, [FromBody] ReplaceUserRolesCommand command)
        {
            var enrichedCommand = command with { UserId = userId };
            var response = await Mediator.Send(enrichedCommand);
            return ToActionResult(response);
        }
    }
}
