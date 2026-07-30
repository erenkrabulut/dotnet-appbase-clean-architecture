namespace core.Application.Abstractions.Logging
{
    public sealed class LogContext
    {
        public string? CorrelationId { get; init; }

        public Guid? UserId { get; init; }
        public bool IsAuthenticated { get; init; }

        public IReadOnlyCollection<string> Roles { get; init; } = Array.Empty<string>();
        public IReadOnlyCollection<string> Permissions { get; init; } = Array.Empty<string>();

        public string? IpAddress { get; init; }
        public string? UserAgent { get; init; }

        public string? RequestPath { get; init; }
        public string? HttpMethod { get; init; }
    }
}
