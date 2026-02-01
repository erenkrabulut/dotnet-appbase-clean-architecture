using core.Application.Features.RolePermissions.Commands.AddPermissionToRole;
using core.Application.Features.RolePermissions.Commands.RemovePermissionFromRole;
using core.Application.Features.RolePermissions.Commands.ReplaceRolePermissions;
using core.Application.Features.RolePermissions.Queries.GetPermissionsByRoleId;
using core.Application.Features.UserRoles.Commands.ReplaceUserRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/roles/{roleId:guid}/permissions")]
    [Authorize]
    [ApiController]
    public sealed class RolePermissionsController : BaseController
    {
        public RolePermissionsController() { }

        [HttpGet]
        public async Task<IActionResult> GetRolePermissions([FromRoute] Guid roleId)
        {
            var response = await Mediator.Send(new GetPermissionsByRoleIdQuery(roleId));
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddPermission(
            [FromRoute] Guid roleId,
            [FromBody] AddPermissionToRoleCommand command)
        {
            var enrichedCommand = command with { RoleId = roleId };
            var response = await Mediator.Send(enrichedCommand);
            return Ok(response);
        }

        [HttpDelete("{permissionId:int}")]
        public async Task<IActionResult> RemovePermission(
            [FromRoute] Guid roleId,
            [FromRoute] int permissionId)
        {
            var response = await Mediator.Send(new RemovePermissionFromRoleCommand(roleId, permissionId));
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Replace([FromRoute] Guid roleId, [FromBody] ReplaceRolePermissionsCommand command)
        {
            var enrichedCommand = command with { RoleId = roleId};
            var response = await Mediator.Send(enrichedCommand);
            return Ok(response);
        }
    }
}
