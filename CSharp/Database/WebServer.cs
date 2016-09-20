namespace Kevins.Examples.Database
{
    public class WebServer : BaseEntity<WebServer>
    {
        public virtual string ExternalUrl { get; set; }
        public virtual string InternalUrl { get; set; }
        public virtual string UrlForMonitoring { get; set; }
        public virtual string Status { get; set; }
    }
}
