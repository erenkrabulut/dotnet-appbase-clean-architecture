using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Logging;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.Auth.Dtos;

namespace core.Application.Features.Auth.Commands.GoogleLogin
{
    public sealed record GoogleLoginCommand(string IdToken, string? IpAddress)
        : ICommand<Response<TokenPairDto>>, ILoggableRequest, ITransactionalRequest;
}
