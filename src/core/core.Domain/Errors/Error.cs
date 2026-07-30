namespace core.Domain.Errors
{
    public sealed record Error(
        string Code,
        string Message,
        ErrorType Type,
        IReadOnlyDictionary<string, object>? Meta = null);
}
