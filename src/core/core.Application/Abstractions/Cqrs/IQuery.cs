using core.Application.Common.Responses;

namespace core.Application.Abstractions.Cqrs
{
    public interface IQuery<TResponse> : IRequestBase<TResponse>
        where TResponse : Response
    {
    }
}
