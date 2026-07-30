using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Logging;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;

namespace core.Application.Features.Auth.Commands.ChangePassword
{
    public sealed record ChangePasswordCommand(string CurrentPassword, string NewPassword, string? IpAddress)
        : ICommand<Response>, ILoggableRequest, ITransactionalRequest;
}
