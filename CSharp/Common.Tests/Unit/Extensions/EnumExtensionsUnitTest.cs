using Kevins.Examples.Common.Enums;
using Kevins.Examples.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Common.Tests.Unit.Extensions
{
    [TestClass]
    public class EnumExtensionsUnitTest
    {

        [TestMethod]
        public void ShouldCovertEnumToInt()
        {
            Assert.AreEqual(2000, DeliveryQueueType.ExamReport.AsInt());
        }


        [TestMethod]
        public void ShouldCovertEnumToEnumNameAsAWord()
        {
            Assert.AreEqual("ExamReport", DeliveryQueueType.ExamReport.Name());
        }


        [TestMethod]
        public void ShouldCovertEnumToEnumNameAsWords()
        {
            Assert.AreEqual("Exam Report", DeliveryQueueType.ExamReport.AsWords());
        }


        [TestMethod]
        public void ShouldCovertEnumNamesToEnum()
        {
            Assert.IsInstanceOfType("ExamReport".GetEnum<DeliveryQueueType>(), typeof(DeliveryQueueType));
            Assert.AreEqual(DeliveryQueueType.ExamReport, "ExamReport".GetEnum<DeliveryQueueType>());
        }


        [TestMethod]
        public void ShouldNotCovertEnumNameToEnum()
        {
            Assert.IsInstanceOfType("blah".GetEnum<DeliveryQueueType>(), typeof(DeliveryQueueType));
            Assert.AreEqual(DeliveryQueueType.Undefined, "blah".GetEnum<DeliveryQueueType>());
        }


        [TestMethod]
        public void ShouldCovertEnumNameAsWordsToEnum()
        {
            Assert.IsInstanceOfType("Exam Report".GetEnumFromWords<DeliveryQueueType>(), typeof(DeliveryQueueType));
            Assert.AreEqual(DeliveryQueueType.ExamReport, "Exam Report".GetEnumFromWords<DeliveryQueueType>());
        }


        [TestMethod]
        public void ShouldNotCovertEnumNameAsWordsToEnum()
        {
            Assert.IsInstanceOfType("blah".GetEnumFromWords<DeliveryQueueType>(), typeof(DeliveryQueueType));
            Assert.AreEqual(DeliveryQueueType.Undefined, "blah".GetEnumFromWords<DeliveryQueueType>());
        }


        [TestMethod]
        public void ShouldDetermineIfAnHardCopyDeliveryMethod()
        {
            Assert.IsTrue(DeliveryMethod.BatchPrint.IsHardCopyDeliveryMethod());
            Assert.IsFalse(DeliveryMethod.Email.IsHardCopyDeliveryMethod());
            Assert.IsTrue(DeliveryMethod.Fax.IsHardCopyDeliveryMethod());
            Assert.IsTrue(DeliveryMethod.Print.IsHardCopyDeliveryMethod());
            Assert.IsTrue(DeliveryMethod.ServerRequest.IsHardCopyDeliveryMethod());
        }


        [TestMethod]
        public void ShouldDetermineIfAnFaxDeliveryMethod()
        {
            Assert.IsTrue(DeliveryMethod.Fax.IsFax());
            Assert.IsFalse(DeliveryMethod.BatchPrint.IsFax());
            Assert.IsFalse(DeliveryMethod.Email.IsFax());
            Assert.IsFalse(DeliveryMethod.Undefined.IsFax());
            Assert.IsFalse(DeliveryMethod.Print.IsFax());
            Assert.IsFalse(DeliveryMethod.ServerRequest.IsFax());
        }


        [TestMethod]
        public void ShouldDetermineIfAnPrintDeliveryMethod()
        {
            Assert.IsTrue(DeliveryMethod.Print.IsPrint());
            Assert.IsTrue(DeliveryMethod.BatchPrint.IsPrint());
            Assert.IsTrue(DeliveryMethod.ServerRequest.IsPrint());
            Assert.IsFalse(DeliveryMethod.Email.IsPrint());
            Assert.IsFalse(DeliveryMethod.Fax.IsPrint());
            Assert.IsFalse(DeliveryMethod.Undefined.IsPrint());
        }
    }
}
