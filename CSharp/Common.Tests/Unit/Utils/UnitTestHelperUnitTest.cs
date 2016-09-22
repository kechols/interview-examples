using Kevins.Examples.Common.Tests.Unit.Helpers;
using Kevins.Examples.Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Common.Tests.Unit.Utils
{
    [TestClass]
    public class ProjectHelperUnitTest
    {
        [TestMethod]
        // [Ignore]
        public void ShouldGetCorrectAssemblyDirectoryPath() { 
            // Note: This test is britle bcause the output changes based on where the project is setup
            // So for the above reason this test is ignored. However I, (kechols), feel that
            // that this test should exist since the property is heavly used.
            Assert.AreEqual(@"C:\Users\Kevin\Source\Repos\interview-examples\CSharp\Common.Tests\bin\Debug", ProjectHelper.AssemblyDirectory);
        }


        [TestMethod]
        // [Ignore]
        public void ShouldGetCorrectProjectDirectoryPath()
        {
            // Note: This test is britle bcause the output changes based on where the project is setup
            // So for the above reason this test is ignored. However I, (kechols), feel that
            // that this test should exist since the property is heavly used.
            Assert.AreEqual(@"C:\Users", ProjectHelper.ProjectDirectory);
        }


        [TestMethod]
        // [Ignore]
        public void ShouldDoSomething()
        {
            Assert.AreEqual(string.Empty, @"kevins_hello_world.html".GetEmbeddeResoueLocation(@"Kevins.Examples.Message.Tests.Resources"));
        }
    }
}
