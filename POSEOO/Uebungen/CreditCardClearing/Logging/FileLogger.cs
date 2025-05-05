using System;
using System.IO;
using System.Text;

namespace Logging
{
    /// <summary>
    /// Logger that writes messages to a file.
    /// </summary>
    public class FileLogger : AbstractLogger
    {
        private readonly string _filePath;

        public FileLogger(string filePath) : this(filePath, LogLevel.Info)
        {
        }

        public FileLogger(string filePath, LogLevel level) : base(level)
        {
            string[] parts = filePath.Split('.');
            _filePath = $"{parts[0]}-{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}.{parts[1]}";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        protected override void Log(LogLevel level, string message)
        {
            message = $"{DateTime.Now};{message};";
            switch (level)
            {
                case LogLevel.Info: message = "INFO;" + message; break;
                case LogLevel.Warning: message = "WARN;" + message; break;
                case LogLevel.Error: message = "ERROR;" + message; break;
                default: break;
            }
            File.AppendAllText(_filePath, message + Environment.NewLine, Encoding.Default);
        }
    }
}
