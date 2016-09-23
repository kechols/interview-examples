using System.IO;
using Kevins.Examples.Common.Io.Pdf;
using Kevins.Examples.Common.Io.Pdf.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Common.Tests.Unit.Io.Pdf.Extensions
{
    [TestClass]
    public class PdfExtensionsUnitTest
    {

        private PdfFileObject pdfResult = null;


        [TestMethod]
        public void ShouldConvertGoogleUrlToPdfFile()
        {
            bool removedFieldsToHideWhenPriniting;
            var url = @"https://www.google.com/";

            pdfResult = url.UrlToPdf(
                "google_web_url_test",
                1,
                out removedFieldsToHideWhenPriniting
            );

            Assert.IsTrue(pdfResult.GeneratedSuccessfully);
            Assert.IsFalse(removedFieldsToHideWhenPriniting);
        }


        [TestMethod]
        [DeploymentItem("log4net.config")]
        public void ShouldConvertWebUrlToPdfFile()
        {
            bool removedFieldsToHideWhenPriniting;
            var url = @"https://docs.oracle.com/javase/tutorial/getStarted/cupojava/";

            pdfResult = url.UrlToPdf(
                "oracle_documentation_hello_world_web_url_test",
                1,
                out removedFieldsToHideWhenPriniting
            );

            Assert.IsTrue(pdfResult.GeneratedSuccessfully);
            Assert.IsFalse(removedFieldsToHideWhenPriniting);
            // Assert.IsTrue(UnitTestHelper.FilesAreEqual(expectedContentFile, pdfResult));
        }


        [TestMethod]
        // Ther next line is not needed because of settings in .runsettings file 
        // which is specified as a test running setup file
        // [DeploymentItem("log4net.config")]  
        public void ShouldNotConvertBadUrlToPdf()
        {
            bool removedFieldsToHideWhenPrinitng;
            var url = @"http://blah.blah.com/blah.html";

            pdfResult = url.UrlToPdf(
                "bad_url_test",
                1,
                out removedFieldsToHideWhenPrinitng
            );

            Assert.IsFalse(pdfResult.GeneratedSuccessfully);
            Assert.IsFalse(removedFieldsToHideWhenPrinitng);
        }


        [TestCleanup]
        public void TearDown()
        {
            if (pdfResult.GeneratedSuccessfully)
            {
                if (!File.Exists(pdfResult.FilePath))
                {
                    Assert.Fail(
                        "The teardown failed because the pdfResult does not exist and cannot be cleaned up as expected.");
                }
                File.Delete(pdfResult.FilePath);
            }
        }
    }
}
