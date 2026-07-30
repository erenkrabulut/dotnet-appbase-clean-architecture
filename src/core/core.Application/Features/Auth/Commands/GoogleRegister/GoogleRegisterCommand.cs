using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Logging;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using core.Application.Features.Auth.Dtos;

namespace core.Application.Features.Auth.Commands.GoogleRegister
{
    public sealed record GoogleRegisterCommand(
        string IdToken,
        string FirstName,
        string LastName,
        string? IpAddress)
        : ICommand<Response<TokenPairDto>>, ILoggableRequest, ITransactionalRequest;
}
