using core.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Cqrs
{
    public interface IQuery<TResponse> : IRequestBase<TResponse>
        where TResponse : Response
    {
    }
}
