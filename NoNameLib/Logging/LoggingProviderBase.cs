using NoNameLib.Enums;

namespace NoNameLib.Logging
{
    public abstract class LoggingProviderBase : ILoggingProvider
    {
        public abstract void Log(LoggingLevel level, string text, params object[] args);

        public void Verbose(string text, params object[] args)
        {
            Log(LoggingLevel.Verbose, text, args);
        }

        public void Debug(string text, params object[] args)
        {
            Log(LoggingLevel.Debug, text, args);
        }

        public void Information(string text, params object[] args)
        {
            Log(LoggingLevel.Info, text, args);
        }

        public void Warning(string text, params object[] args)
        {
            Log(LoggingLevel.Warning, text, args);
        }

        public void Error(string text, params object[] args)
        {
            Log(LoggingLevel.Error, text, args);
        }
    }
}
