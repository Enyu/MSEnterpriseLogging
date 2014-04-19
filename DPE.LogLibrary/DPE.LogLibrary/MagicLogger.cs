using System.Collections.Generic;
using System.Diagnostics;
using DPE.LogLibrary.Listeners;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace DPE.LogLibrary
{
    public class MagicLogger
    {
        public MagicLogger()
        {
            var config = new LoggingConfiguration();
            config.AddLogSource("Error", SourceLevels.Error, true).AddTraceListener(new FlatFileListener("error.xml"));
            config.AddLogSource("Information", SourceLevels.Information, true).AddTraceListener(new FlatFileListener("information.xml"));
            var loggerWriter = new LogWriter(config);
            Logger.SetLogWriter(loggerWriter);
        }

        public void WriteError(string message, IDictionary<string, object> dict)
        {
            var logEntry = new LogEntry { ExtendedProperties = dict, Message = message, Severity = TraceEventType.Error };
            logEntry.Categories.Add("Error");

            Logger.Writer.Write(logEntry);
        }

        public void WriteInfomation(string message, IDictionary<string, object> dict)
        {
            var logEntry = new LogEntry { ExtendedProperties = dict, Message = message, Severity = TraceEventType.Information };
            logEntry.Categories.Add("Information");

            Logger.Writer.Write(logEntry);
        }
    }
}
