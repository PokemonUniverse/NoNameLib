using NoNameLib.Extension;

namespace NoNameLib.Logging
{
    /// <summary>
    /// Logging Wrapper
    /// </summary>
    public static class Logger
    {
        private const string CLASS_LOG_FORMAT = "{0}.{1} - {2}";

        public static void Debug(string className, string method, string text, params object[] args)
        {
            Global.LoggingProvider.Debug(CLASS_LOG_FORMAT.FormatSafe(className, method, text), args);
        }

        public static void Verbose(string className, string method, string text, params object[] args)
        {
            Global.LoggingProvider.Verbose(CLASS_LOG_FORMAT.FormatSafe(className, method, text), args);
        }

        public static void Info(string className, string method, string text, params object[] args)
        {
            Global.LoggingProvider.Information(CLASS_LOG_FORMAT.FormatSafe(className, method, text), args);
        }

        public static void Warning(string className, string method, string text, params object[] args)
        {
            Global.LoggingProvider.Warning(CLASS_LOG_FORMAT.FormatSafe(className, method, text), args);
        }

        public static void Error(string className, string method, string text, params object[] args)
        {
            Global.LoggingProvider.Error(CLASS_LOG_FORMAT.FormatSafe(className, method, text), args);
        }
    }
}
