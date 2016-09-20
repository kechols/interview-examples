using System;
using System.Collections.Generic;
using Kevins.Examples.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Common.Tests.Unit.Extensions
{
    [TestClass]
    public class StringExtensionsUnitTest
    {
        [TestMethod]
        public void ShouldCovertToFirstAndLastName()
        {
            Assert.AreEqual("Kevin Echols", " Echols , Kevin ".AsFirstAndLastName());
        }

        [TestMethod]
        public void ShouldCovertVerboseTotName()
        {
            Assert.AreEqual("Echols, Kevin", "Kevin Echols".NameFromVerboseUserName());
            Assert.AreEqual("Echols, Kevin", "Dr. Kevin Echols".NameFromVerboseUserName());
            Assert.AreEqual("Scarfe, Andrew", "Andrew Scarfe M.D.".NameFromVerboseUserName());
            Assert.AreEqual("Turner, A Robert", "Dr A Robert Turner M.D.".NameFromVerboseUserName());
            Assert.AreEqual("Nixon, Anne", "Mrs. Anne Nixon".NameFromVerboseUserName());
            Assert.AreEqual("Kolb, Kevin", "Mr Kevin Kolb".NameFromVerboseUserName());
        }

        [TestMethod]
        public void ShouldCovertEnumToTrimedName()
        {
            Assert.AreEqual("Echols", " Echols ".AsFirstAndLastName());
        }

        [TestMethod]
        public void ShouldCovertCommaDelimitedNameToFirstName()
        {
            Assert.AreEqual("Kevin", "Echols,Kevin".FirstName());
            Assert.AreEqual("Kevin", "Echols, Kevin ".FirstName());
            Assert.AreEqual(string.Empty, "Echols".FirstName());
            Assert.AreEqual(string.Empty, "".FirstName());
        }

        [TestMethod]
        public void ShouldCovertCommaDelimitedNameToLastName()
        {
            Assert.AreEqual("Echols", "Echols,Kevin".LastName());
            Assert.AreEqual("Echols", "Echols, Kevin ".LastName());
            Assert.AreEqual("Echols", "Echols".LastName());
            Assert.AreEqual(string.Empty, "".LastName());
        }

        [TestMethod]
        public void ShouldCovertStringToInt()
        {
            Assert.AreEqual(1, "1".AsInt());
        }

        [TestMethod]
        public void ShouldCovertTrueToYes()
        {
            Assert.AreEqual("Yes", true.AsYesNo());
        }


        [TestMethod]
        public void ShouldCovertFalseToNo()
        {
            Assert.AreEqual("No", false.AsYesNo());
        }


        [TestMethod]
        public void UsingToBooleanTheStringsShouldBeTrue()
        {
            var testStrings = new List<string> { "1", "YES", "Y", "y", "YeS", "yes", "true", "True", "TRUE" };
            foreach (var testString in testStrings)
            {
                Assert.IsTrue(testString.ToBoolean());
            }
        }


        [TestMethod]
        public void ShouldReplaceAll()
        {
            Assert.AreEqual("Hello kevin kevin", "Hello WoRlD World".ReplaceAll("world", "kevin"));
        }


        [TestMethod]
        public void ShouldReturnCorrectStringFromLeft()
        {
            Assert.AreEqual("k", @"kevin".Left(1));

            Assert.AreEqual("k", @"kevin".Left(-1)); // returns k
        }


        [TestMethod]
        public void ShouldReturnEmptyStringFromLeft()
        {
            Assert.AreEqual(string.Empty, @"kevin".Left(0));

            Assert.AreEqual(string.Empty, string.Empty.Left(1));

            Assert.AreEqual(string.Empty, string.Empty.Left(0));

            Assert.AreEqual(string.Empty, string.Empty.Left(-1));

            Assert.AreEqual(string.Empty, string.Empty.Left(1));
        }


        [TestMethod]
        public void UsingToBooleanTheStringsShouldBeFalse()
        {
            var testStrings = new List<string> { String.Empty, "  ", "0", "NO", "N", "n", "No", "no", "false", "False", "FALSE" };
            foreach (var testString in testStrings)
            {
                Assert.IsFalse(testString.ToBoolean());
            }
        }


        [TestMethod]
        public void ShouldCreateSeparatedWordsFromCamelcaseString()
        {
            Assert.AreEqual("Big Bad Wolf", "BigBadWolf".CamelCaseAsWords());
            Assert.AreEqual("Car", "Car".CamelCaseAsWords());
        }

        [TestMethod]
        public void ShouldDetermineValidEmail()
        {
            Assert.IsFalse("BigBadWolf".IsValidEmail());
            Assert.IsTrue("john.doe@gmail.com".IsValidEmail());

            Assert.IsFalse((new List<string> { "BigBadWolf", "john.doe@gmail.com" }).AreValidEmails());
            Assert.IsTrue((new List<string> { "mike.jones@gmail.com", "john.doe@gmail.com" }).AreValidEmails());
        }


        [TestMethod]
        public void ShouldReplaceSuffixOnly()
        {
            Assert.AreEqual("Jrat", "Jrat Jr".ReplaceSuffix("Jr", string.Empty));
        }


        [TestMethod]
        public void ShouldReplaceTitleOnly()
        {
            Assert.AreEqual("Mrat", "Mr Mrat".ReplaceTitle("Mr", string.Empty));
        }


        [TestMethod]
        public void ShouldReplaceStyle()
        {
            var orginalStyles = "color: brown; Background-Color: white;";
            var expectedStyles = "color: brown; ";
            Assert.AreEqual(expectedStyles, orginalStyles.RemoveStyle("background-color"));

            orginalStyles = "color: brown; background-color: white;";
            Assert.AreEqual(expectedStyles, orginalStyles.RemoveStyle("background-color"));
        }


        [TestMethod]
        public void ShouldFindDivs()
        {
            const string div1 = "<div style=\"margin: 0in 0in 10pt\"><span style=\"line-height: 115%; font-size: 10pt\">This letter is in reference to the exams that you underwent recently:</span></div>";
            const string div2 = "<div style=\"margin: 0in 0in 10pt\"><span style=\"line-height: 115%; font-size: 10pt\">XRAY - Spine Total 2v performed on Mar 18 2014 </span></div>";
            const string html = div1 + div2;

            var divTags = html.FindTag("div");

            Assert.AreEqual(2, divTags.Count);
            Assert.AreEqual(div1, divTags[0].Value);
            Assert.AreEqual(div2, divTags[1].Value);
        }


        [TestMethod]
        [Ignore]
        public void ShouldFindNestedDivs()
        {
            const string div1 = "<div style=\"margin: 0in 0in 10pt\"><div style=\"line-height: 115%; font-size: 10pt\">This letter is in reference to the exams that you underwent recently:</div></div>";
            const string html = div1;


            var divTags = html.FindTag("div");

            Assert.AreEqual(1, divTags.Count);
            Assert.AreEqual(div1, divTags[0].Value);
        }


        [TestMethod]
        public void ShouldFindPtags()
        {
            const string paragraph = "<p><span style=\"line-height: 115%; font-size: 10pt\">Dear Kevin Echols, </span></p>";
            const string html = paragraph;

            var pTags = html.FindTag("p");

            Assert.AreEqual(1, pTags.Count);
            Assert.AreEqual(paragraph, pTags[0].Value);
        }
    }
}
