namespace core.Domain.Errors
{
    public static class GeneralErrors
    {
        public static readonly Error Internal =
            new("GENERAL.INTERNAL", "Unexpected error occurred.", ErrorType.Internal);

        public static readonly Error NotFound =
            new("GENERAL.NOT_FOUND", "Not found error occured.", ErrorType.NotFound);

        public static readonly Error Conflict =
            new("GENERAL.CONFLICT", "Conflict error occured.", ErrorType.Conflict);
    }
}
