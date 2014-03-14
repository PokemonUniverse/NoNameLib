using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NoNameLib.Enums;

namespace NoNameLib.Logging
{
    public class AsyncLoggingProvider : LoggingProviderBase, IDisposable
    {
        private DateTime lastHistoryCleanup = DateTime.MinValue;
        private readonly string logPath;
        private readonly string logFilenamePrefix;
        private readonly string applicationName;
        private readonly int daysOfHistory;

        private readonly BlockingCollection<LogEntry> logQueue = new BlockingCollection<LogEntry>();
        readonly CancellationTokenSource stopRequested = new CancellationTokenSource();

        public AsyncLoggingProvider(string logPath, string logFilenamePrefix, int daysOfHistory)
        {
            if (daysOfHistory < 1)
                throw new InvalidDataException("Days of history must be at least 1");

            this.logPath = logPath;
            this.logFilenamePrefix = logFilenamePrefix;
            this.daysOfHistory = daysOfHistory;
            this.applicationName = Global.ApplicationInfo.ApplicationName;

            // Init consumer thread            
            Task.Factory.StartNew(() => this.ProcessQueue(stopRequested.Token));
        }

        public override void Log(LoggingLevel level, string text, params object[] args)
        {
            var entry = new LogEntry(level, text, args);
            this.logQueue.Add(entry);
        }

        private void ProcessQueue(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    // Take will wait until there's something to do
                    var entry = this.logQueue.Take(cancellationToken);

                    // Write to log
                    this.PersistToLog(entry);

                    // Clean up history once an hour
                    if ((DateTime.Now - this.lastHistoryCleanup).TotalMinutes > 60)
                    {
                        this.CleanupHistory();
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\r" + ex.StackTrace;
                PersistToLog(new LogEntry(LoggingLevel.Error, message));
            }
        }

        private void PersistToLog(LogEntry entry)
        {
            string filepath = Path.Combine(this.logPath, this.GetFilename());

            using (StreamWriter writer = File.AppendText(filepath))
            {
                writer.Write(string.Format("{0:MM-dd HH:mm:ss.fff}: {1}/{2}({3}): {4}", DateTime.Now, entry.LevelCharacter, this.applicationName, System.Diagnostics.Process.GetCurrentProcess().Id, entry.Text));
                writer.Write("\n");
                writer.Flush();
            }
        }

        private void CleanupHistory()
        {
            // Clean up
            var files = Directory.GetFiles(this.logPath, this.logFilenamePrefix + "-*");
            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                if ((DateTime.Now - fi.LastWriteTime).Days > this.daysOfHistory)
                    File.Delete(file);
            }

            this.lastHistoryCleanup = DateTime.Now;
        }

        /// <summary>
        /// Gets the filename.
        /// </summary>
        /// <returns></returns>
        private string GetFilename()
        {
            return string.Format("{0}-{1:yyyyMMdd}.log", this.logFilenamePrefix, DateTime.Now);
        }

        public void Dispose()
        {
            this.stopRequested.Cancel(true);
        }
    }
}
