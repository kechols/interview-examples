using Kevins.DSolutions.Algorithm.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.DSolutions.Algorithm.Tests.Unit.Extensions
{
    [TestClass]
    public class StringExtensionsUnitTest
    {
        [TestMethod]
        public void ShouldFindFirstStringPairAtTheEnds()
        {
            Assert.AreEqual("Kevin", "KevinEcholsEcholsKevin".FindFirstStringPair(5));
        }


        [TestMethod]
        public void ShouldFindFirstStringPairInTheMiddle()
        {
            Assert.AreEqual("Echols", "KevinEcholsEcholsKevin".FindFirstStringPair(6));
        }


        [TestMethod]
        public void ShouldNotIfMatchLengthIsZero()
        {
            Assert.AreEqual(string.Empty, "Kevin".FindFirstStringPair(0));
        }


        [TestMethod]
        public void ShouldNotFindIfSearchStringIsEmpty()
        {
            Assert.AreEqual(string.Empty, string.Empty.FindFirstStringPair(10));
        }
    }
}
