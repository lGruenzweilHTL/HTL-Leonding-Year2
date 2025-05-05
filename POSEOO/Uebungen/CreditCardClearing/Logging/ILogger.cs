namespace Logging
{
    public enum LogLevel
    {
        Error,
        Warning,
        Info
    };

    public interface ILogger
    {
        void LogError(string message);
        void LogInfo(string message);
        //void LogWarning(string message);
    }
}
