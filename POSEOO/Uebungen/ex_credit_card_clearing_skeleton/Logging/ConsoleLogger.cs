using System;

namespace Logging
{
    /// <summary>
    /// Logger that writes messages to the console.
    /// </summary>
    public class ConsoleLogger : AbstractLogger
    {
        public ConsoleLogger(LogLevel level) : base(level)
        {
        }

        protected override void Log(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.Error: 
                    Console.ForegroundColor = ConsoleColor.Red; 
                    message = $"ERROR>> {message}"; 
                    break;
                case LogLevel.Warning: 
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    message = $"WARN>> {message}";
                    break;
                case LogLevel.Info: 
                    Console.ForegroundColor = ConsoleColor.Green;
                    message = $"INFO>> {message}";
                    break;
                default: break;
            }
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
