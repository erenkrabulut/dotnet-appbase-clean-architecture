using core.Application.Common.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Cqrs
{
    public interface IRequestBase<TResponse> : IRequest<TResponse>
        where TResponse : Response
    {
    }
}
