using Kevins.Examples.Common.Enums;
using Kevins.Examples.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Database.Tests.Unit.Extensions
{
    [TestClass]
    public class EnumExtensionsUnitTest
    {

        
        [TestMethod]
        public void ShouldCovertEnumOpinionToWord()
        {
            Assert.AreEqual("Email", DeliveryMethod.Email.AsWords());
        }


        [TestMethod]
        public void ShouldCovertEnumOpinionToWords()
        {
            Assert.AreEqual("Auto Appointment Confirmation", DeliveryQueueType.AutoAppointmentConfirmation.AsWords());
        }


        [TestMethod]
        public void ShouldCovertEnumOpinionToInt()
        {
            Assert.AreEqual(3, OpinionType.Disagree.AsInt());
        }
    }
}
