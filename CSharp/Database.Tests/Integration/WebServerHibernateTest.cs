using System;
using Kevins.Examples.Database.Common;
using Kevins.Examples.Database.Tests.Integration.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Database.Tests.Integration
{
    [TestClass]
    public class WebServerHibernateTest : HibernateTestBase
    {
        [TestMethod]
        public void ShouldCorrectlyMapWebServer()
        {
            WithSession(session => {
                var repository = new DatabaseRepository(session);
                WebServer retrievedWebServer = null;
                var expectedId = -1;
                try
                {
                    const string expectedExternalUrl = "http://External";
                    const string expectedInternalUrl = "http://Internal";
                    const string expectedUrlForMonitoring = "http://ForMonitoring";
                    const string expectedStatus = "A";

                    var webServer = new WebServer()
                    {
                        ExternalUrl = expectedExternalUrl,
                        InternalUrl = expectedInternalUrl,
                        UrlForMonitoring = expectedUrlForMonitoring,
                        Status = expectedStatus
                    };

                    // Very new unsaved webserver has no Id
                    Assert.AreEqual(0, webServer.Id);

                    // Save the webserver and then verify that it has an Id
                    repository.SaveOrUpdate(webServer);
                    Assert.IsTrue(webServer.Id > 0);
                    expectedId = webServer.Id;
                    repository.Clear();

                    retrievedWebServer = repository.Get<WebServer>(webServer.Id);
                    Assert.IsTrue(retrievedWebServer.Id > 0);
                    Assert.AreEqual(expectedId, retrievedWebServer.Id);
                    Assert.AreEqual(expectedExternalUrl, retrievedWebServer.ExternalUrl);
                    Assert.AreEqual(expectedInternalUrl, retrievedWebServer.InternalUrl);
                    Assert.AreEqual(expectedUrlForMonitoring, retrievedWebServer.UrlForMonitoring);
                    Assert.AreEqual(expectedStatus, retrievedWebServer.Status);
                }
                catch (Exception e)
                {
                    // Here to debug exceptions if necessary
                    throw e;
                }
                finally
                {
                    retrievedWebServer = repository.Get<WebServer>(expectedId);
                    repository.Clear();
                    repository.Delete(retrievedWebServer);
                    Assert.IsNull(repository.Get<WebServer>(retrievedWebServer.Id));
                }
            });
        }
    }
}
