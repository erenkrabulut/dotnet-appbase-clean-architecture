using core.Application.Abstractions.Cqrs;
using core.Application.Common.Paging;
using core.Application.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Application.Abstractions.Paging
{
    public interface IPagedRequest<TResponse> : IRequestBase<TResponse>
        where TResponse : Response
    {
        PageRequest PageRequest { get; }
    }
}
