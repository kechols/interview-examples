using System.IO;
using System.Linq;
using Kevins.Examples.Common.Io;
using Kevins.Examples.Common.Io.Web.Extensions;
using Kevins.Examples.Common.Tests.Unit.Helpers;
using Kevins.Examples.Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Common.Tests.Unit.Io.Web.Extensions
{
    [TestClass]
    public class RemoveFieldsWhenPrintingExtensionsUnitTest
    {
        private static readonly string TestResources = ProjectHelper.AssemblyDirectory +
                                                       @"\Resources\Io\Web\Extensions\RemoveFieldsWhenPrintingExtensionsUnitTest\";


        [TestMethod]
        public void ShouldRemoveFieldsMatchingTagUsingAttribute()
        {
            var inputHtmlFile = TestResources + @"removeFieldsMatchingTagUsingAttribute_TestFile.html";
            var expectedHtml = TestResources + @"removeFieldsMatchingTagUsingAttribute_ExpectedOutput.html";


            var result = File.ReadAllText(inputHtmlFile).RemoveFields(
                string.Format(RemoveFieldsWhenPrintingExtensions.FindHtmlTagWithAttributeClause,
                @"div", "class", "shouldGetRemoved1")
            );

            Assert.IsTrue(result.RemovedFields);

            result = result.Html.RemoveFields(
                string.Format(RemoveFieldsWhenPrintingExtensions.FindHtmlTagWithAttributeClause,
                @"div", "class", "shouldGetRemoved2",
                result.RemovedFields)
            );

            Assert.IsTrue(result.RemovedFields);
            var linesThatAreDifferent = expectedHtml.IsFileContentsEqual(result.Html);
            Assert.AreEqual(linesThatAreDifferent.Count(), 0, linesThatAreDifferent.GetLineDifferenceFailureMessage());
        }


        [TestMethod]
        public void ShouldRemoveFieldsMatchingTagUsingFieldEnum()
        {
            var inputHtmlFile = TestResources + @"removeFieldsMatchingTagUsingFieldEnum_TestFile.html";
            var expectedHtml = TestResources + @"removeFieldsMatchingTagUsingFieldEnum_ExpectedOutput.html";

            var result = File.ReadAllText(inputHtmlFile).RemoveFieldsMatchingTag(Fields.Select);

            Assert.IsTrue(result.RemovedFields);
            var linesThatAreDifferent = expectedHtml.IsFileContentsEqual(result.Html);
            Assert.AreEqual(linesThatAreDifferent.Count(), 0, linesThatAreDifferent.GetLineDifferenceFailureMessage());
        }
    }
}
