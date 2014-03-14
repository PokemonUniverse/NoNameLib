using System;
using NoNameLib.Enums;

namespace NoNameLib.Logging
{
    public class ConsoleLoggingProvider : LoggingProviderBase
    {
        public override void Log(LoggingLevel level, string text, params object[] args)
        {
            var entry = new LogEntry(level, text, args);
            Console.WriteLine("{0:HH:mm:ss.fff} [{1}] {2}", DateTime.Now, entry.LevelCharacter, entry.Text);
        }
    }
}
