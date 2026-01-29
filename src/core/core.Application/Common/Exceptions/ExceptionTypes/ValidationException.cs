using core.Domain.Errors;
using FluentValidation.Results;



namespace core.Application.Common.Exceptions.ExceptionTypes
{
    public sealed class ValidationException : AppException
    {
        public IReadOnlyDictionary<string, string[]> FieldErrors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base(ValidationErrors.ValidationFailed)
        {
            FieldErrors = failures
                .GroupBy(f => f.PropertyName ?? string.Empty)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage)
                          .Where(m => !string.IsNullOrWhiteSpace(m))
                          .Distinct()
                          .ToArray()
                );
        }
    }
}
