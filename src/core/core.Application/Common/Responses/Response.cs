namespace core.Application.Common.Responses
{
    public class Response
    {
        public bool Success { get; init; }
        public ExceptionResponse? Error { get; init; }

        public static Response Ok() => new() { Success = true };
        public static Response Fail(ExceptionResponse error) => new() { Success = false, Error = error };
    }

    public sealed class Response<T> : Response
    {
        public T? Data { get; init; }

        public static Response<T> Ok(T data) => new() { Success = true, Data = data };
        public static new Response<T> Fail(ExceptionResponse error) => new() { Success = false, Error = error };
    }
}
