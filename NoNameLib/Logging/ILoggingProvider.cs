using NoNameLib.Enums;

namespace NoNameLib.Logging
{
    public interface ILoggingProvider
    {
        void Log(LoggingLevel level, string text, params object[] args);

        void Verbose(string text, params object[] args);

        void Debug(string text, params object[] args);

        void Information(string text, params object[] args);

        void Warning(string text, params object[] args);

        void Error(string text, params object[] args);
    }
}
