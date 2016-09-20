using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Common.Tests.Unit.Helpers
{
    [TestClass]
    public class UnitTestHelperUnitTest
    {
        [TestMethod]
        // [Ignore]
        public void ShouldDoSomething()
        {
            Assert.AreEqual(string.Empty, @"kevins_hello_world.html".GetEmbeddeResoueLocation(@"Sunrise.Radiology.Messenger.Message.Tests.Resources"));
        }
    }
}
