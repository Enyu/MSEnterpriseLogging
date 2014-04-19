using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;

namespace DPE.LogLibrary.Formatters
{
    [ConfigurationElementType(typeof(CustomFormatterData))]
    public class XmlFormatter : ILogFormatter
    {
        public XmlFormatter(NameValueCollection attributes)
        {
        }

        public string Format(LogEntry log)
        {
            var xmlBody = new StringBuilder()
                .AppendLine("<AppLog>")
                .AppendFormat("<Message>{0}</Message>", log.Message).AppendLine()
                .AppendFormat("<ControllerName>{0}</ControllerName>", log.ExtendedProperties["controllerName"]).AppendLine()
                .AppendFormat("<ActionName>{0}</ActionName>", log.ExtendedProperties["actionName"]).AppendLine()
                .AppendFormat("<category>{0}</category>", log.Categories.First())
                .AppendLine("</AppLog>");

            return xmlBody.ToString();
        }
    }
}