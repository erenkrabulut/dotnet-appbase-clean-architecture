using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Application.Features.Roles.Commands.CreateRole;
using core.Application.Features.Roles.Commands.DeleteRole;
using core.Application.Features.Roles.Commands.UpdateRole;
using core.Application.Features.Roles.Dtos;
using core.Application.Features.Roles.Queries.GetRoleByName;
using core.Application.Features.Roles.Queries.GetRolesPage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class RolesController : BaseController
    {
        public RolesController() { }

        [HttpGet("by-name/{name}")]
        public async Task<IActionResult> GetByName([FromRoute] string name)
        {
            var response = await Mediator.Send(new GetRoleByNameQuery(name));
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] GetRolesPageQuery query)
        {
            Response<PageResponse<RoleDto>> response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        {
            Response<RoleDto> response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRoleCommand command)
        {
            var enrichedCommand = command with { Id = id };
            Response<RoleDto> response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await Mediator.Send(new DeleteRoleCommand(id));
            return Ok(response);
        }
    }
}
