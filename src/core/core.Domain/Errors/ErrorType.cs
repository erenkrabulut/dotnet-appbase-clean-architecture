namespace core.Domain.Errors
{
    public enum ErrorType
    {
        Validation,
        Authorization,
        NotFound,
        Conflict,
        Business,
        External,
        Persistence,
        Internal,
        Identity
    }
}
