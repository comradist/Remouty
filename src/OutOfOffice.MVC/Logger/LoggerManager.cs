
using NLog;
using OutOfOffice.MVC.Contracts;

namespace OutOfOffice.MVC.Logger;

public class LoggerManager : ILoggerManager
{
    private static NLog.ILogger logger = LogManager.GetCurrentClassLogger();

    public LoggerManager()
    {

    }

    public void LogInfo(string message) => logger.Info(message);
    public void LogWarn(string message) => logger.Warn(message);
    public void LogDebug(string message) => logger.Debug(message);
    public void LogError(string message) => logger.Error(message);

}
