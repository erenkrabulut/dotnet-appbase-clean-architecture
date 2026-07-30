using core.Application.Common.Responses;
using MediatR;

namespace core.Application.Abstractions.Cqrs
{
    public interface IRequestBase<TResponse> : IRequest<TResponse>
        where TResponse : Response
    {
    }
}
