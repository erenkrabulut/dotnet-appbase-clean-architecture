using core.Application.Abstractions.Cqrs;
using core.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Auth.Commands.Logout
{
    public sealed record LogoutCommand(string RefreshToken, string? IpAddress) : ICommand<Response>;
}
