using System;
using System.Linq;
using Kevins.Examples.Database.Common;

namespace Kevins.Examples.Database.Extensions
{
    public static class WebServerExtensions
    {
        public static WebServer GetWebServer(this IDatabaseRepository repository)
        {
            var webServers = repository.GetAll<WebServer>();
            if (webServers.Any())
            {
                var webServerWithHighestStatusLookup = (from server in webServers
                    group server by new { server.Status, server.Id }
                    into i
                    select new { i.Key.Id, i.Key.Status  })
                    .Where(i => i.Status.ToUpper().Trim().Equals("A"))
                    .OrderByDescending(i => i.Status)
                    .FirstOrDefault();
                return repository.Get<WebServer>(webServerWithHighestStatusLookup.Id);
            }
            return new WebServer { InternalUrl = String.Empty };
        }
    }
}
