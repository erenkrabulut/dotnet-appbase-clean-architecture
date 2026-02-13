using core.Application.Common.Paging;
using core.Application.Common.Responses;
using core.Application.Features.Permissions.Dtos;
using core.Application.Features.Permissions.Queries.GetPermissionById;
using core.Application.Features.Permissions.Queries.GetPermissionPage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public sealed class PermissionsController : BaseController
    {
        public PermissionsController() { }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Response<PermissionDto> response = await Mediator.Send(new GetPermissionByIdQuery(id));
            return ToActionResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] GetPermissionsPageQuery query)
        {
            Response<PageResponse<PermissionDto>> response = await Mediator.Send(query);
            return ToActionResult(response);
        }
        
    }
}
