using Microsoft.Extensions.Logging;

namespace Application.Common.Extensions.Logger
{
    public static class LoggerExtension
    {
        private static ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
            .AddConsole()
            .AddDebug();
        });

        public static ILogger CreateLogger<T>() => _loggerFactory.CreateLogger<T>();

        public static ILogger CreateLogger(string categoryName) => _loggerFactory.CreateLogger(categoryName);

        public static void LogInformation(string message, string name = null)
        {
            string categoryName = name ?? "GlobalLogger";

            ILogger logger = _loggerFactory.CreateLogger(categoryName);

            logger.LogInformation("Information message: {Message}", message);
        }

        public static void LogError(Exception ex, string message, string name = null)
        {
            string categoryName = name ?? "GlobalLogger";

            ILogger logger = _loggerFactory.CreateLogger(categoryName);

            logger.LogError(ex, "Error message: {Message}", message);
        }

        public static void LogWarning(string message, string name = null)
        {
            string categoryName = name ?? "GlobalLogger";

            ILogger logger = _loggerFactory.CreateLogger(categoryName);

            logger.LogWarning("Warning message: {Message}", message);
        }
    }
}