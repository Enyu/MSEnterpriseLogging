using System;
using System.Configuration;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace DPE.LogLibrary.Listeners
{
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class FlatFileListener : RollingFlatFileTraceListener
    {
        public FlatFileListener(string fileName,
            string header = "",
            string footer = "",
            ILogFormatter formatter = null,
            int rollSizeKb = 20000,
            string timeStampPattern = "yyyy-MM-dd hh:mm:ss",
            RollFileExistsBehavior rollFileExistsBehavior = RollFileExistsBehavior.Increment,
            RollInterval rollInterval = RollInterval.Day,
            int maxArchivedFiles = 0) :
                base(
                Path.Combine(ResolveLogPath(), fileName), header, footer, new XmlLogFormatter(), rollSizeKb, timeStampPattern,
                rollFileExistsBehavior, rollInterval, maxArchivedFiles)
        {
        }

        private static string ResolveLogPath()
        {
            try
            {
                return ConfigurationManager.AppSettings["logPath"];
            }
            catch (Exception exception)
            {
                throw new ConfigurationErrorsException("Please add log path root in your config file first.", exception);
            }
        }
    }
}