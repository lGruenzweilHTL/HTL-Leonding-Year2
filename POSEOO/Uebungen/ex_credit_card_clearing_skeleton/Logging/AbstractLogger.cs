namespace Logging
{
    public abstract class AbstractLogger : ILogger
    {
        protected AbstractLogger(LogLevel level)
        {
            LogLevel = level;
        }

        public LogLevel LogLevel { get; private set; }

        /// <summary>
        /// Logs an error message if the log level is set to Error or higher.
        /// </summary>
        /// <param name="logText">The error message to log.</param>
        public virtual void LogError(string logText)
        {
            if (LogLevel >= LogLevel.Error)
            {
                Log(LogLevel.Error, logText);
            }
        }

        /// <summary>
        /// Logs an informational message if the log level is set to Info or higher.
        /// </summary>
        /// <param name="logText">The informational message to log.</param>
        public virtual void LogInfo(string logText)
        {
            if (LogLevel >= LogLevel.Info)
            {
                Log(LogLevel.Info, logText);
            }
        }

        /// <summary>
        /// Logs a warning message if the log level is set to Warning or higher.
        /// </summary>
        /// <param name="logText">The warning message to log.</param>
        //public virtual void LogWarning(string logText)
        //{
        //    if (LogLevel >= LogLevel.Warning)
        //    {
        //        Log(LogLevel.Warning, logText);
        //    }
        //}
        protected abstract void Log(LogLevel level, string message);
    }
}