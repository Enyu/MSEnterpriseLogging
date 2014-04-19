using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using DPE.LogLibrary.Formatters;
using DPE.LogLibrary.Listeners;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace DPE.LogLibrary
{
    public class MagicLogger
    {
        public MagicLogger()
        {
            var config = new LoggingConfiguration();
            var txtFormatter = new TxtFormatter(new NameValueCollection());
            config.AddLogSource("Error", SourceLevels.Error, true).AddTraceListener(new FlatFileListener("error.txt", formatter: txtFormatter));
            config.AddLogSource("Information", SourceLevels.Information, true).AddTraceListener(new FlatFileListener("information.txt", formatter: txtFormatter));
            var loggerWriter = new LogWriter(config);
            Logger.SetLogWriter(loggerWriter, false);
        }


        /// <summary>
        /// Writing a error log
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="dict">Properties you want to log.
        /// Example: When input Key is Controller and Value is ExampleController in dict.
        /// Then log information will be 'Controller : ExampleController'.
        /// </param>
        public void WriteError(string message, IDictionary<string, object> dict)
        {
            var logEntry = new LogEntry { ExtendedProperties = dict, Message = message, Severity = TraceEventType.Error };
            logEntry.Categories.Add("Error");

            Logger.Writer.Write(logEntry);
            Logger.Writer.Dispose();
        }

        /// <summary>
        /// Writing a information log
        /// </summary>
        /// <param name="message">information message</param>
        /// <param name="dict">Properties you want to log.
        /// Example: When input Key is Controller and Value is ExampleController in dict.
        /// Then log information will be 'Controller : ExampleController'.
        /// </param>
        public void WriteInfomation(string message, IDictionary<string, object> dict)
        {
            var logEntry = new LogEntry { ExtendedProperties = dict, Message = message, Severity = TraceEventType.Information };
            logEntry.Categories.Add("Information");

            Logger.Writer.Write(logEntry);
            Logger.Writer.Dispose();
        }
    }
}
