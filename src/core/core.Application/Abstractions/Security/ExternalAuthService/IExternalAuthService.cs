using core.Application.Abstractions.Security.ExternalAuthService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Security.ExternalLoginService
{
    public interface IExternalAuthService
    {
        Task<ExternalAuthResult> AuthenticateAsync(string token);
    }
}
