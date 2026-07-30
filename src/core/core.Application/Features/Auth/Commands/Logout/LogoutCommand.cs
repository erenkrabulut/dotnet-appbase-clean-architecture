using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Logging;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;

namespace core.Application.Features.Auth.Commands.Logout
{
    public sealed record LogoutCommand(string RefreshToken, string? IpAddress)
        : ICommand<Response>, ILoggableRequest, ITransactionalRequest;
}
