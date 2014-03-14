using NoNameLib.Enums;
using NoNameLib.Extension;

namespace NoNameLib.Logging
{
    internal class LogEntry
    {
        public LogEntry(LoggingLevel level, string text, params object[] args)
        {
            this.Level = level;
            this.Text = text.FormatSafe(args);
        }
        public string Text { get; set; }
        public LoggingLevel Level { get; set; }
        public char LevelCharacter
        {
            get
            {
                switch (this.Level)
                {
                    case LoggingLevel.Debug:
                        return 'D';
                    case LoggingLevel.Verbose:
                        return 'V';
                    case LoggingLevel.Info:
                        return 'I';
                    case LoggingLevel.Warning:
                        return 'W';
                    case LoggingLevel.Error:
                        return 'E';
                    default:
                        return 'I';
                }
            }
        }
    }
}
