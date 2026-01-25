using core.Application.Abstractions.Cqrs;
using core.Application.Abstractions.Logging;
using core.Application.Abstractions.Transactions;
using core.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Features.Auth.Commands.ChangePassword
{
    public sealed record ChangePasswordCommand(string CurrentPassword, string NewPassword)
        : ICommand<Response>, ILoggableRequest, ITransactionalRequest;
}
