using core.Application.Abstractions.Cqrs;
using core.Application.Common.Paging;
using core.Application.Common.Responses;

namespace core.Application.Abstractions.Paging
{
    public interface IPagedRequest<TResponse> : IRequestBase<TResponse>
        where TResponse : Response
    {
        PageRequest PageRequest { get; }
    }
}
