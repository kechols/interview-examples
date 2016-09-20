using System;
using Kevins.Examples.Database.Common;
using Kevins.Examples.Database.Extensions;
using Kevins.Examples.Database.Tests.Integration.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sunrise.Radiology.Messenger.Common.Utils.Patterns;
using Sunrise.Radiology.Messenger.Database.Common;

namespace Kevins.Examples.Database.Tests.Integration.Extensions
{
    [TestClass]
    public class WebServerExtensionsTest : HibernateTestBase
    {

        [TestMethod]
        public void ShouldGetHighestRankedWebServer()
        {
            WithSession(session => {
                var repository = new MessengerRepository(session);
                var expectedId = -1;
                var previousHighistRankedWebserver = repository.GetWebServer();
                repository.Clear();
                WebServer retrievedWebServer = null;
                try
                {
                    var webServer = new WebServer()
                    {
                        ExternalUrl = "http://External",
                        InternalUrl = "http://Internal",
                        UrlForMonitoring = "http://ForMonitoring",
                        Status = "W"
                    };
                    Assert.AreEqual(0, webServer.Id);
                    // Save the webserver and then verify that it has an Id
                    repository.SaveOrUpdate(webServer);
                    Assert.IsTrue(webServer.Id > 0);
                    expectedId = webServer.Id;
                    repository.Clear();

                    // Now Veirfy that the newest Webserver with status W has the highest ranking
                    retrievedWebServer = repository.GetWebServer();
                    Assert.IsTrue(retrievedWebServer.Id > 0);
                    Assert.AreEqual((object)previousHighistRankedWebserver.Id, retrievedWebServer.Id);
                    // Assert.AreNotEqual(expectedId, retrievedWebServer.Id);

                    // Now ddlete all webservers and assert that the highest ranking webserver is a stub
                    repository.Clear();
                    foreach (var webserver in repository.GetAll<WebServer>())
                    {
                        repository.Delete(webserver);
                    }
                    var operation = new WebServerRetryable();
                    var retryer = new Retryer(operation);
                    try
                    {
                        retryer.Perform(2, 500);
                    }
                    catch (RetryFailedException)
                    {
                    }
                    Assert.AreEqual(0, repository.GetWebServer().Id);
                }
                catch (Exception e)
                {
                    // Here to debug exceptions if necessary
                    throw e;
                }
                finally
                {
                    repository.Clear();
                    // Putback the original highest ranked webserver
                    previousHighistRankedWebserver.Id = 0;
                    repository.SaveOrUpdate(previousHighistRankedWebserver);
                    Assert.IsNotNull(repository.Get<WebServer>(previousHighistRankedWebserver.Id));
                }
            });
        }
    }


    class WebServerRetryable : IRetryable
    {
        public bool Attempt()
        {
            return (new MessengerRepository(MessengerRepositorySessionFactory.Instance.OpenSession()).GetWebServer().Id == 0);
        }


        public void Recover() { }
    }
}
