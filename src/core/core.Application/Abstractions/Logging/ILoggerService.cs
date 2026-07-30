namespace core.Application.Abstractions.Logging
{
    public interface ILoggerService
    {
        void LogInfo(string message, object? data = null);
        void LogWarning(string message, object? data = null);
        void LogError(string message, Exception? ex = null, object? data = null);
        void LogDebug(string message, object? data = null);
    }
}
