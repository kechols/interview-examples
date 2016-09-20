using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using log4net;
using Sunrise.Radiology.Messenger.Common.Utils.Strings;

namespace Kevins.Examples.Common.Extensions
{
    public static class AppSettingsExtensions
    {
        // Specifies the location to drop the emails when app.config mailSettings smtp deliveryMethod="SpecifiedPickupDirectory" for test
        public static string PickupDirectoryLocation = "PickupDirectoryLocation";

        private static readonly ILog Log = LogManager.GetLogger(typeof(AppSettingsExtensions));

        private static readonly DefaultsProvider defaultsProvider = DefaultsProvider.Instance;
        private static readonly MessagesProvider messagesProvider = MessagesProvider.Instance;

        public static void UpdateAppSettings(this KeyValuePair<string, string> keyValue)
        {
            if (ConfigurationManager.AppSettings.AllKeys.ToList().Contains(keyValue.Key))
            {
                Configuration configuration;
                if (System.Web.HttpContext.Current != null &&
                    !System.Web.HttpContext.Current.Request.PhysicalPath.Equals(string.Empty))
                {
                    configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                    configuration.AppSettings.Settings[keyValue.Key].Value = keyValue.Value;
                    configuration.Save(ConfigurationSaveMode.Modified);
                    var test = configuration.AppSettings.Settings[keyValue.Key].Value;

                }
                else
                {
                    configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    configuration.AppSettings.Settings[keyValue.Key].Value = keyValue.Value;
                    configuration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }


            if (System.Web.HttpContext.Current != null &&
                !System.Web.HttpContext.Current.Request.PhysicalPath.Equals(string.Empty))
            {
                var configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                if (PickupDirectoryLocation.Equals(keyValue.Key))
                {
                    var mailSettings =
                        configuration.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
                    mailSettings.Smtp.SpecifiedPickupDirectory.PickupDirectoryLocation = keyValue.Value;
                    configuration.Save(ConfigurationSaveMode.Modified);
                }
            }
            else
            {
                var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                if (PickupDirectoryLocation.Equals(keyValue.Key))
                {
                    var mailSettings =
                        configuration.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
                    mailSettings.Smtp.SpecifiedPickupDirectory.PickupDirectoryLocation = keyValue.Value;
                    configuration.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("system.net");
                }
            }
        }


        public static string Setting(this string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                var exception = new ArgumentException("The key for the setting must not be null and not empty");
                Log.Error("It is impossible to get a setting with no key", exception);
                throw exception;
            }

            string setting;
            if (System.Web.HttpContext.Current != null)
            {
                setting = System.Web.Configuration.WebConfigurationManager.AppSettings[key];
            }
            else
            {
                setting = ConfigurationManager.AppSettings[key];
            }


            if (string.IsNullOrEmpty(setting))
            {
                setting = defaultsProvider[key];
            }
            return setting;
        }


        public static string RequiredSetting(this string key)
        {
            var setting = key.Setting();

            if (string.IsNullOrEmpty(setting))
            {
                var exception = new ArgumentException(string.Format("The setting: {0} must not be null or empty. {1}", key, messagesProvider[key]));
                Log.Error("Failed to get the required setting for " + key, exception);
                throw exception;
            }

            return setting;
        }


        public static string Hostname
        {
            get
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                return ipHostInfo.HostName;
            }
        }


        public static MailSettingsSectionGroup MailSettings
        {
            get
            {
                return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
                        .GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
            }
        }

    }
}
