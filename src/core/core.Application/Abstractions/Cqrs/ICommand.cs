using core.Application.Common.Responses;


namespace core.Application.Abstractions.Cqrs
{
    public interface ICommand<TResponse> : IRequestBase<TResponse>
        where TResponse : Response
    {
    }
}
