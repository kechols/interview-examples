using Sunrise.Radiology.Messenger.Database;

namespace Kevins.Examples.Database.Mapping
{
    public class WebServerMap : BaseEntityMap<WebServer>
    {
        public WebServerMap()
        {
            Table("WebSrvrs");

            Id(o => o.Id, "WebServerNo");
            Map(o => o.ExternalUrl, "ExternalUrl");
            Map(o => o.InternalUrl, "InternalUrl");
            Map(o => o.UrlForMonitoring, "UrlForMonitoring");
            Map(o => o.Status, "Status");
        }

    }
}
