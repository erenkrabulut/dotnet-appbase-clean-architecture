using core.Application.Common.Responses;
using core.Application.Features.Auth.Commands.ChangePassword;
using core.Application.Features.Auth.Commands.GoogleLogin;
using core.Application.Features.Auth.Commands.GoogleRegister;
using core.Application.Features.Auth.Commands.Login;
using core.Application.Features.Auth.Commands.Logout;
using core.Application.Features.Auth.Commands.Refresh;
using core.Application.Features.Auth.Commands.Register;
using core.Application.Features.Auth.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AuthController : BaseController
    {
        public AuthController() { }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken ct)
        {
            Response<TokenPairDto> result = await Mediator.Send(command, ct);
            if (result.Success && result?.Data?.RefreshToken is not null)
            {
                SetRefreshTokenCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiresAt);
            }

            return ToActionResult(result);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken ct)
        {
            var enrichedCommand = command with
            {
                IpAddress = GetClientIpAddress()
            };

            Response<TokenPairDto> result = await Mediator.Send(enrichedCommand, ct);

            if (result.Success && result?.Data?.RefreshToken is not null)
            {
                SetRefreshTokenCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiresAt);
            }

            return ToActionResult(result);
        }

        [HttpPost("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> Refresh([FromBody] RefreshCommand command, CancellationToken ct)
        {
            var enrichedCommand = command with
            {
                IpAddress = GetClientIpAddress()
            };

            Response<TokenPairDto> result = await Mediator.Send(enrichedCommand, ct);
            if (result.Success && result?.Data?.RefreshToken is not null)
            {
                SetRefreshTokenCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiresAt);
            }

            return ToActionResult(result);

        }
        
       
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command, CancellationToken ct)
        {
            var enrichedCommand = command with
            {
                IpAddress = GetClientIpAddress()
            };
            Response result = await Mediator.Send(enrichedCommand, ct);
            
            DeleteRefreshTokenCookie();
            return ToActionResult(result);
        }


        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command, CancellationToken ct)
        {
            Response result = await Mediator.Send(command, ct);
            return ToActionResult(result);
        }

        [HttpPost("google/login")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginCommand command, CancellationToken ct)
        {
            var enrichedCommand = command with
            {
                IpAddress = GetClientIpAddress()
            };

            Response<TokenPairDto> result = await Mediator.Send(enrichedCommand, ct);

            if (result.Success && result?.Data?.RefreshToken is not null)
            {
                SetRefreshTokenCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiresAt);
            }

            return ToActionResult(result);
        }

        [HttpPost("google/register")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleRegister([FromBody] GoogleRegisterCommand command, CancellationToken ct)
        {
            Response<TokenPairDto> result = await Mediator.Send(command, ct);
            if (result.Success && result?.Data?.RefreshToken is not null)
            {
                SetRefreshTokenCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiresAt);
            }

            return ToActionResult(result);
        }
    }
}
