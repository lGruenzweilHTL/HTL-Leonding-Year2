namespace Logging
{
    /// <summary>
    /// Logger that aggregates multiple loggers and forwards log messages to all of them.
    /// </summary>
    public class LoggerCollection : ILogger
    {
        private ILogger[] _loggers;

        public LoggerCollection(params ILogger[] loggers)
        {
            _loggers = loggers;
        }

        public void LogError(string message)
        {
            for (int i = 0; i < _loggers.Length; i++)
            {
                _loggers[i].LogError(message);
            }
        }

        public void LogInfo(string message)
        {
            for (int i = 0; i < _loggers.Length; i++)
            {
                _loggers[i].LogInfo(message);
            }
        }
    }
}