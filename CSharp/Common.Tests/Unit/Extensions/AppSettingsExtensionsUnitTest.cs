using System;
using System.Collections.Generic;
using Kevins.Examples.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Common.Tests.Unit.Extensions
{
    [TestClass]
    public class AppSettingsExtensionsUnitTest
    {

        [TestMethod]
        [DeploymentItem("log4net.config")]
        public void ShouldChangeAppSetting()
        {
            var key = "TimerInterval";
            var originalValue = "TimerInterval".RequiredSetting();
            var expectedNewValue = "1000";
            // Assert that this test put things back from the previous test run.
            Assert.AreNotEqual(originalValue, expectedNewValue);

            new KeyValuePair<string, string>(key, expectedNewValue).UpdateAppSettings();
            // Assert Changed
            Assert.AreEqual(int.Parse(expectedNewValue), "TimerInterval".RequiredSetting().AsInt());

            new KeyValuePair<string, string>(key, originalValue).UpdateAppSettings();
            // Assert Changed back
            Assert.AreEqual(int.Parse(originalValue), "TimerInterval".RequiredSetting().AsInt());
        }

        [TestMethod]
        public void ShouldChangeMailPickupDirectorySetting()
        {
            var key = AppSettingsExtensions.PickupDirectoryLocation;
            var originalDirectory = AppSettingsExtensions.MailSettings.Smtp.SpecifiedPickupDirectory.PickupDirectoryLocation;
            var newDirectory = "c:\\test";
            // Assert that this test put things back from the previous test run.
            Assert.AreNotEqual(originalDirectory, newDirectory);

            new KeyValuePair<string, string>(key, newDirectory).UpdateAppSettings();
            // Assert Changed
            Assert.AreEqual(newDirectory, AppSettingsExtensions.MailSettings.Smtp.SpecifiedPickupDirectory.PickupDirectoryLocation);

            new KeyValuePair<string, string>(key, originalDirectory).UpdateAppSettings();
            // Assert Changed back
            Assert.AreEqual(originalDirectory, AppSettingsExtensions.MailSettings.Smtp.SpecifiedPickupDirectory.PickupDirectoryLocation);
        }

        [TestMethod]
        [DeploymentItem("log4net.config")]
        [ExpectedException(typeof(ArgumentException))]
        public void RequiredSettingShouldThrowsException()
        {
            "Bogus".RequiredSetting();
        }


        [TestMethod]
        [DeploymentItem("log4net.config")]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptySettingShouldThrowsException()
        {
            string.Empty.RequiredSetting();
        }


        [TestMethod]
        public void ShouldGetHostname()
        {
            Assert.IsFalse(string.IsNullOrEmpty(AppSettingsExtensions.Hostname));
        }
    }
}
