using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Kevins.Examples.Common.Extensions
{
    public static class DateTimeExtensions
    {

        public static readonly string MonthDayYearFormat = "MMM d yyyy";
        public static readonly string MonthDayYearTimeFormat = "MMM d yyyy HH:mm:ss";
        public static readonly string MonthDayYearTimeNoSecondsFormat = "MMM d yyyy h:mm tt";
        public static readonly string FullMonthDayYearFormat = "MMMM d yyyy";
        public static readonly string TimeFormat = "HH:mm";

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static int YearsAfterReferenceDate(this DateTime date, DateTime referenceDate)
        {
            var age = date.Year - referenceDate.Year;
            if (referenceDate > date.AddYears(-age))
            {
                age--;
            }
            return age;
        }



        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string NullableDateToString(this DateTime? date)
        {
            return date.NullableDateToString(MonthDayYearFormat);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static DateTime? DateFromTime(this DateTime? date, string time)
        {
            if (date == null || string.IsNullOrEmpty(time))
            {
                return date;
            }
            
            date = ((DateTime)date).AddHours(time.Substring(0, 2).AsInt());
            date = ((DateTime)date).AddMinutes(time.Substring(2, 2).AsInt());
            return date;
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string NullableDateToString(this DateTime? date, string format)
        {
            if (date == null)
            {
                return String.Empty;
            }
            return ((DateTime)date).ToString(format);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string ConvertTimeToAmPm(this string time)
        {
            var regex = new Regex(@"\d{1,2}");
            var matches = regex.Matches(time);
            if (matches.Count < 2)
            {
                return time;
            }
            var hours = matches[0].ToString();
            hours = hours.Length > 1 ? hours : 0 + hours;
            var minutes = matches[1].ToString();

            return DateTime.ParseExact(hours + minutes, "HHmm", CultureInfo.CurrentCulture).ToString("h:mm tt");
        }
    }
}
