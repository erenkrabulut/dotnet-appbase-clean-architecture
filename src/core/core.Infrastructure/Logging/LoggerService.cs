using core.Application.Abstractions.Logging;
using Microsoft.Extensions.Logging;

namespace core.Infrastructure.Logging
{
    public sealed class LoggerService : ILoggerService
    {
        private readonly ILogger<LoggerService> _logger;
        private readonly ILogContextAccessor _context;

        public LoggerService(ILogger<LoggerService> logger, ILogContextAccessor context)
        {
            _logger = logger;
            _context = context;
        }

        public void LogInfo(string message, object? data = null)
            => _logger.LogInformation("{Message} {@Context} {@Data}", message, _context.Get(), data);

        public void LogWarning(string message, object? data = null)
            => _logger.LogWarning("{Message} {@Context} {@Data}", message, _context.Get(), data);

        public void LogDebug(string message, object? data = null)
            => _logger.LogDebug("{Message} {@Context} {@Data}", message, _context.Get(), data);

        public void LogError(string message, Exception? ex = null, object? data = null)
            => _logger.LogError(ex, "{Message} {@Context} {@Data}", message, _context.Get(), data);
    }
}
