namespace core.Domain.Errors
{
    public static class ValidationErrors
    {
        public static readonly Error ValidationFailed =
            new("VALIDATION.VALIDATION_FAILED", "Validation failed.", ErrorType.Validation);
    }
}
