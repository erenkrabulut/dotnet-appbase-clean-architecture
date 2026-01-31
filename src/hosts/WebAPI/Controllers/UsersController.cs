using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Application.Features.Users.Commands.CreateUser;
using core.Application.Features.Users.Commands.DeleteUser;
using core.Application.Features.Users.Commands.UpdateUser;
using core.Application.Features.Users.Dtos;
using core.Application.Features.Users.Queries.GetUserByEmail;
using core.Application.Features.Users.Queries.GetUserById;
using core.Application.Features.Users.Queries.GetUsersPage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class UsersController : BaseController
    {
        public UsersController() { }

        [HttpGet("{id:guid}")]
        [Authorize] 
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var query = new GetUserByIdQuery { Id = id };
            Response<UserDto> result = await Mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpGet("by-email/{email}")]
        [Authorize]
        public async Task<IActionResult> GetByEmail([FromRoute] string email, CancellationToken ct)
        {
            var query = new GetUserByEmailQuery { Email = email };
            Response<UserDto> result = await Mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetPage([FromQuery] GetUsersPageQuery query, CancellationToken ct)
        {
            Response<PageResponse<UserDto>> result = await Mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command, CancellationToken ct)
        {
            Response<UserDto> result = await Mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateUserCommand command, CancellationToken ct)
        {
            var enriched = command with { Id = id };

            Response<UserDto> result = await Mediator.Send(enriched, ct);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] Guid id, [FromQuery] bool isSoftDelete = true, CancellationToken ct = default)
        {
            var command = new DeleteUserCommand { Id = id, IsSoftDelete = isSoftDelete };

            Response result = await Mediator.Send(command, ct);
            return Ok(result);
        }
    }
}
