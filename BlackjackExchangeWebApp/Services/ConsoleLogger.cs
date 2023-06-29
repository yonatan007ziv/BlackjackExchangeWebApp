namespace BlackjackExchangeWebApp.Services
{
    public class ConsoleLogger : ILogger
    {
        private static readonly object _WriteLock = new object();

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (_WriteLock)
            {
                string message = formatter(state, exception);
                string logLevelString = logLevel.ToString();

                Console.ForegroundColor = FindLogLevelColor(logLevel);
                Console.Write($"[{logLevelString}] ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(message);
            }
        }

        private ConsoleColor FindLogLevelColor(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return ConsoleColor.Yellow;
                case LogLevel.Information:
                    return ConsoleColor.Cyan;
                case LogLevel.Error:
                    return ConsoleColor.Red;
                case LogLevel.Critical:
                    return ConsoleColor.DarkRed;

                default:
                case LogLevel.Trace:
                case LogLevel.Warning:
                case LogLevel.None:
                    return ConsoleColor.White;
            }
        }
    }
}