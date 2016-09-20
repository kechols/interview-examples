using System.Collections.Generic;

namespace Sunrise.Radiology.Messenger.Common.Utils.Strings
{
    /// <summary>
    /// This class helps reduce string duplication in defaults. For example, the attribute
    ///   [Schema("RLogic")]
    /// can be used on many model properties, but the actual defauls can be changed in just one place: here.
    /// 
    /// DefaultProvider (a singleton) is used to provide an interface to deliver these strings.
    /// </summary>
    public class DefaultsLoader : IStringLoader
    {
        private readonly Dictionary<string, string> defaults;

        public DefaultsLoader()
        {
            defaults = new Dictionary<string, string>();
        }

        public Dictionary<string, string> Load()
        {
            defaults.Add("AdobeReaderLocation", @"C:\Program Files(x86)\Adobe\Acrobat Reader DC\Reader\AcroRd32.exe");
            defaults.Add("AdobeTimeToPrintBeforeClosing", "5");
            defaults.Add("EnableBatchPrint", "True");
            defaults.Add("EnableEmail", "True");
            defaults.Add("EnableEmailNotices", "False");
            defaults.Add("EnableFax", "True");
            defaults.Add("EnablePrint", "True");
            defaults.Add("EnableServerRequest", "True");
            defaults.Add("FaxMessagePriority", "1");
            defaults.Add("MaxConcurrentDeliveryQueueEntries", "10");
            defaults.Add("MessengerEmailAddress", "noname@noname.com");
            defaults.Add("NumberDaysToKeepLogs", "10");
            defaults.Add("ProductName", "RIS");
            defaults.Add("SysAdminEmailAddress", "noname@noname.com");
            defaults.Add("TimerInterval", "10");
            defaults.Add("BatchPrintTimerInterval", "60");
            defaults.Add("QueryTimeOut", "90");

            return defaults;
        }
    }
}
