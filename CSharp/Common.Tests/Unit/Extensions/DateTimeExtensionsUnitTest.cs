using System;
using System.Globalization;
using Kevins.Examples.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Common.Tests.Unit.Extensions
{
    [TestClass]
    public class DateTimeExtensionsUnitTest
    {
        private static readonly string DateFormat = "M/d/yyyy HH:mm";

        [TestMethod]
        public void YearsShouldBeYearsFromReferenceDate()
        {
            // DateTime.ParseExact("25/12/2008 13:00:00", "dd/MM/yyyy HH:mm:ss", new CultureInfo("en-US"));
            var referenceDate = DateTime.ParseExact("9/1/2009 12:00", DateFormat, new CultureInfo("en-US"));
            var date = DateTime.ParseExact("9/1/2008 12:00", DateFormat, new CultureInfo("en-US"));
            Assert.AreEqual(-1, date.YearsAfterReferenceDate(referenceDate));

            referenceDate = DateTime.ParseExact("9/1/2008 12:00", DateFormat, new CultureInfo("en-US"));
            date = DateTime.ParseExact("9/1/2009 11:50", DateFormat, new CultureInfo("en-US"));
            Assert.AreEqual(0, date.YearsAfterReferenceDate(referenceDate));

            referenceDate = DateTime.ParseExact("9/1/2008 12:00", DateFormat, new CultureInfo("en-US"));
            date = DateTime.ParseExact("9/2/2009 12:00", DateFormat, new CultureInfo("en-US"));
            Assert.AreEqual(1, date.YearsAfterReferenceDate(referenceDate));
        }


        [TestMethod]
        public void NonNullDateShouldReturnMonthDayYearFormatString()
        {

            DateTime? date = DateTime.ParseExact("9/1/2008 12:00", DateFormat, new CultureInfo("en-US"));
            var expectedDate = ((DateTime)date).ToString(DateTimeExtensions.MonthDayYearFormat);
            Assert.AreEqual(expectedDate, date.NullableDateToString());
            Assert.AreEqual("Sep 1 2008", date.NullableDateToString());
        }


        [TestMethod]
        public void NonNullDateShouldReturnFullMonthDayYearFormatString()
        {

            DateTime? date = DateTime.ParseExact("9/1/2008 12:00", DateFormat, new CultureInfo("en-US"));
            var expectedDate = ((DateTime)date).ToString(DateTimeExtensions.FullMonthDayYearFormat);
            Assert.AreEqual(expectedDate, date.NullableDateToString(DateTimeExtensions.FullMonthDayYearFormat));
            Assert.AreEqual("September 1 2008", date.NullableDateToString(DateTimeExtensions.FullMonthDayYearFormat));
        }


        [TestMethod]
        public void NullDateShouldReturnEmptyString()
        {
            DateTime? date = null;
            Assert.AreEqual(String.Empty, date.NullableDateToString());
        }


        [TestMethod]
        public void ShouldCovertToAmAndPm()
        {
            Assert.AreEqual("1:00 AM", "01:00".ConvertTimeToAmPm());
            Assert.AreEqual("1:00 AM", "1:00".ConvertTimeToAmPm());
            Assert.AreEqual("1:00 PM", "13:00".ConvertTimeToAmPm());
            Assert.AreEqual("12:01 PM", "12:01".ConvertTimeToAmPm());
        }
    }
}
