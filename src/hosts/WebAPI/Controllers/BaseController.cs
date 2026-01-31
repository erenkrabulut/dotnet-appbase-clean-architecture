using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private IMediator? _mediator;
        protected IMediator Mediator =>
            _mediator ??=
                HttpContext.RequestServices.GetService<IMediator>()
                ?? throw new InvalidOperationException("IMediator cannot be retrieved from request services.");

        protected string GetClientIpAddress()
        => HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString()
           ?? throw new InvalidOperationException("IP address cannot be retrieved from request.");

        protected string? GetUserAgent()
            => Request.Headers.UserAgent.ToString();


        protected const string RefreshTokenCookieName = "refreshToken";

        protected string GetRefreshTokenFromCookies()
        => Request.Cookies[RefreshTokenCookieName]
           ?? throw new ArgumentException("Refresh token is not found in request cookies.");

        protected void SetRefreshTokenCookie(string refreshToken, DateTime expiresUtc)
        {
            var options = new CookieOptions
            {
                HttpOnly = true,
                Expires = expiresUtc,
                Secure = true,                    
                SameSite = SameSiteMode.Strict,
                Path = "/"
            };

            Response.Cookies.Append(RefreshTokenCookieName, refreshToken, options);
        }

        protected void DeleteRefreshTokenCookie()
        {
            Response.Cookies.Delete(RefreshTokenCookieName, new CookieOptions
            {
                Path = "/",
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
        }
    }
}
