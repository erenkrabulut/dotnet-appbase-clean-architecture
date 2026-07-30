using core.Application.Abstractions.Security.UserContext;
using core.Infrastructure.Security.Tokens;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace core.Infrastructure.Security.UserContext
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;

        public Guid? UserId
        {
            get
            {
                var claim = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (Guid.TryParse(claim, out var id))
                    return id;
                return null;
            }
        }

        public IReadOnlyCollection<string> Roles =>
            User?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList() ?? new List<string>();

        public IReadOnlyCollection<string> Permissions =>
            User?.FindAll(CustomClaimTypes.Permission).Select(c => c.Value).ToList() ?? new List<string>();

        public bool IsAuthenticated => User?.Identity?.IsAuthenticated ?? false;


    }
}
