using core.Application.Abstractions.Cqrs;
using core.Application.Common.Responses;
using core.Application.Features.Auth.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace core.Application.Features.Auth.Commands.Refresh
{
    public sealed record RefreshCommand(string RefreshToken, string? IpAddress) : ICommand<Response<TokenPairDto>>;
}
