using Contracts;
using NLog;

namespace LoggerSevice
{
    public class LoggerManager : ILoggerManager
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public static ILogger Logger { get => logger; set => logger = value; }

        public void LogDebug(string message) => Logger.Debug(message);

        public void LogError(string message) => Logger.Error(message);

        public void LogInfo(string message) => Logger.Info(message);

        public void LogWarn(string message) => Logger.Warn(message);
    }
}
