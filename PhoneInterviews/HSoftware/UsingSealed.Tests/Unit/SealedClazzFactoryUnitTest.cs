using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.HSoftware.UsingSealed.Tests.Unit
{
    [TestClass]
    public class SealedClazzFactoryUnitTest
    {
        [TestMethod]
        public void ShouldAllowPartiallyInitializedObjectWhenNotSealed()
        {
            var partiallyInitializedObject = false.SealedInstance(null);

            Assert.IsNull(partiallyInitializedObject.Value);
            Assert.IsFalse(partiallyInitializedObject.Sealed);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ShouldThrowExceptinWhenPartiallyInitialingWhileSealed()
        {            
            true.SealedInstance(null);
        }


        [TestMethod]
        public void ShouldPermitInitializedObjectWhenSealed()
        {
            var expectedValue = @"successfully initialized";
            var initializedObject = true.SealedInstance(expectedValue);

            Assert.AreEqual(expectedValue, initializedObject.Value);
            Assert.IsTrue(initializedObject.Sealed);
        }
    }
}
