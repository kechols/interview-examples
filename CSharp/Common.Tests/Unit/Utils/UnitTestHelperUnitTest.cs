using Kevins.Examples.Common.Tests.Unit.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sunrise.Radiology.Messenger.Common.Utils;

namespace Kevins.Examples.Common.Tests.Unit.Utils
{
    [TestClass]
    public class ProjectHelperUnitTest
    {
        [TestMethod]
        // [Ignore]
        public void ShouldGetCorrectAssemblyDirectoryPath()
        {
            // Note: This test is britle bcause the output changes based on where the project is setup
            // So for the above reason this test is ignored. However I, (kechols), feel that
            // that this test should exist since the property is heavly used.
            Assert.AreEqual("C:\\Users\\KEchols\\Source\\Repos\\Messenger\\Common.Tests\\bin\\Debug", ProjectHelper.AssemblyDirectory);
        }


        [TestMethod]
        // [Ignore]
        public void ShouldGetCorrectProjectDirectoryPath()
        {
            // Note: This test is britle bcause the output changes based on where the project is setup
            // So for the above reason this test is ignored. However I, (kechols), feel that
            // that this test should exist since the property is heavly used.
            Assert.AreEqual("C:\\Users\\KEchols\\Source\\Repos\\Messenger", ProjectHelper.ProjectDirectory);
        }


        [TestMethod]
        // [Ignore]
        public void ShouldDoSomething()
        {
            Assert.AreEqual(string.Empty, @"kevins_hello_world.html".GetEmbeddeResoueLocation(@"Sunrise.Radiology.Messenger.Message.Tests.Resources"));
        }
    }
}
