using System.Collections.Generic;

namespace Kevins.Examples.Common.Utils.Strings
{
    /// <summary>
    /// This class helps reduce string duplication in custom validation attribute parameters. For example, the attribute
    ///   [Schema("Name of Schema in database where this application's tables are located")]
    /// can be used on many model properties, but the actual Invalid Date Format error message can be changed in just one place: here.
    /// 
    /// These messages are also used in some places on .aspx pages and to pull descriptions of things to show to the user or log. 
    /// 
    /// StringProvider (a singleton) is used to provide an interface to deliver these strings.
    /// </summary>
    public class MessagesLoader : IStringLoader
    {
        private readonly Dictionary<string, string> messages;

        private static readonly string CONFIG_INSTRUCTIONS = "Please alter it in the web.config or app.config by adjusting or adding: <add key=\"{0}\" value=\"{1}\" /> in the appSettings section";

        public MessagesLoader()
        {
            messages = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Load()
        {
            messages.Add("AdobeTimeToPrintBeforeClosing", string.Format(CONFIG_INSTRUCTIONS, "AdobeTimeToPrintBeforeClosing", "Time to check for the print job in the printer buffer before closing adobe reader"));
            messages.Add("Database", string.Format(CONFIG_INSTRUCTIONS, "Database", "The database name used for this application"));
            messages.Add("EnableBatchPrint", string.Format(CONFIG_INSTRUCTIONS, "EnableBatchPrint", "True|False|1|0"));
            messages.Add("EnableEmail", string.Format(CONFIG_INSTRUCTIONS, "EnableEmail", "True|False|1|0"));
            messages.Add("EnableEmailNotices", string.Format(CONFIG_INSTRUCTIONS, "EnableEmailNotices", "True|False|1|0"));
            messages.Add("EnableFax", string.Format(CONFIG_INSTRUCTIONS, "EnableFax", "True|False|1|0"));
            messages.Add("EnablePrint", string.Format(CONFIG_INSTRUCTIONS, "EnablePrint", "True|False|1|0"));
            messages.Add("EnableServerRequest", string.Format(CONFIG_INSTRUCTIONS, "EnableServerRequest", "True|False|1|0"));
            messages.Add("FaxMessagePriority", string.Format(CONFIG_INSTRUCTIONS, "FaxMessagePriority", "1"));
            messages.Add("FaxServer", string.Format(CONFIG_INSTRUCTIONS, "FaxServer", "Where facsys Sever is installed"));
            messages.Add("FaxServerPassword", string.Format(CONFIG_INSTRUCTIONS, "FaxServerPassword", "the password for the facsys server"));
            messages.Add("FaxServerUsername", string.Format(CONFIG_INSTRUCTIONS, "FaxServerUsername", "the username for the facsys server"));
            messages.Add("MaxConcurrentDeliveryQueueEntries", string.Format(CONFIG_INSTRUCTIONS, "MaxConcurrentJobs", "10"));
            messages.Add("MessengerEmailAddress", string.Format(CONFIG_INSTRUCTIONS, "MessengerEmailAddress", "noname@noname.com"));
            messages.Add("NumberDaysToKeepLogs", string.Format(CONFIG_INSTRUCTIONS, "NumberDaysToKeepLogs", "10"));
            messages.Add("Password", string.Format(CONFIG_INSTRUCTIONS, "Password", "A password for the database used for this applicatoin"));
            messages.Add("ProductName", string.Format(CONFIG_INSTRUCTIONS, "ProductName", "ReportDelivery"));
            messages.Add("QueryTimeOut", string.Format(CONFIG_INSTRUCTIONS, "QueryTimeOut", "The max time in seconds before a database query timesout"));
            messages.Add("Server", string.Format(CONFIG_INSTRUCTIONS, "Server", "The database server used for this application"));
            messages.Add("Schema", string.Format(CONFIG_INSTRUCTIONS, "Schema", "The database schema used for this application"));
            messages.Add("SysAdminEmailAddress", string.Format(CONFIG_INSTRUCTIONS, "SysAdminEmailAddress", "noname@noname.com"));
            messages.Add("BatchPrintTimerInterval", string.Format(CONFIG_INSTRUCTIONS, "BatchPrintTimerInterval", "60"));
            messages.Add("TimerInterval", string.Format(CONFIG_INSTRUCTIONS, "TimerInterval", "10"));

            return messages;
        }
    }
}