using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;

namespace DPE.LogLibrary.Formatters
{
    [ConfigurationElementType(typeof(CustomFormatterData))]
    public class TxtFormatter : ILogFormatter
    {
        public TxtFormatter(NameValueCollection attributes)
        {
        }

        public string Format(LogEntry log)
        {
            var logBody = new StringBuilder();
            foreach (var property in log.ExtendedProperties)
            {
                if (property.Value == null || string.IsNullOrEmpty(property.Value.ToString()))
                {
                    logBody.AppendLine(string.Format("{0} : null", property.Key));
                    continue;
                }
                logBody.AppendLine(string.Format("{0} : {1}", property.Key, property.Value));
            }
            logBody.Append(string.Format("Message : {0}", log.Message));
            return logBody.ToString();
        }
    }
}