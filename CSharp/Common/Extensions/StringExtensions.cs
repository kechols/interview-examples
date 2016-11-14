using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Kevins.Examples.Common.Extensions
{
    public static class StringExtensions
    {

        /*
         * Perhaps good code snippet
         
         How can I discover the “path” of an embedded resource?
        
         http://stackoverflow.com/questions/27757/how-can-i-discover-the-path-of-an-embedded-resource

         * 
         */


        public static readonly IEnumerable<string> Titles = new List<string>(){"Dr.",
            "Dr",
            "Miss",
            "Mrs.",
            "Mrs",
            "\"\"",
            "Reverend",
            "Mr.",
            "Mr",
            "Ms.",
            "Ms",
            "Chaplain"
        };


        public static readonly IEnumerable<string> Suffixes = new List<string>(){
            "Jr",
            "-",
            "M.D./Ph.D.",
            "M.D.",
            "`",
            "Ph.D.",
            "\"\"",
            "C.M.D.",
            "N.P.",
            "III",
            "MD, PhD"
        };
        /// <summary>
        /// Extends the string object to convert "yes" and "no" to true or false. 
        /// </summary>
        
        public static bool ToBoolean(this string s)
        {
            var one = "1";
            var zero = "0";
            var yes = new List<string> { "YES", "Y" };
            var no = new List<string> { "NO", "N" };
            var _true = new List<string> { "TRUE", "T" };
            var _false = new List<string> { "FALSE", "F" };
            if (string.IsNullOrEmpty(s.Trim()) || no.Contains(s.ToUpper()) || zero.Equals(s.Trim()) || _false.Contains(s.ToUpper()))
            {
                return false;
            }
            if (yes.Contains(s.ToUpper()) || _true.Contains((s.ToUpper())) || one.Equals(s.Trim()))
            {
                return true;
            }

            throw new ArgumentException("Must be Yes or No value | True False | 1 or 0 ");
        }


        
        public static string ReplaceAll(this string stringToOperateOn, string toBeReplaced, string replacementString)
        {
            return Regex.Replace(stringToOperateOn, toBeReplaced, replacementString, RegexOptions.IgnoreCase);
        }


        
        public static string ReplaceSuffix(this string stringToOperateOn, string toBeReplaced, string replace)
        {
            if (stringToOperateOn.EndsWith(toBeReplaced, true, CultureInfo.InvariantCulture))
            {
                var start = stringToOperateOn.Length - toBeReplaced.Length;
                stringToOperateOn = stringToOperateOn.Remove(start, toBeReplaced.Length).Insert(start, replace).Trim();
            }
            return stringToOperateOn;
        }


        
        public static string ReplaceTitle(this string stringToOperateOn, string toBeReplaced, string replace)
        {
            if (stringToOperateOn.StartsWith(toBeReplaced, true, CultureInfo.InvariantCulture))
            {
                stringToOperateOn = stringToOperateOn.Remove(0, toBeReplaced.Length).Insert(0, replace).Trim();
            }
            return stringToOperateOn;
        }


        
        public static string AsFirstAndLastName(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            var stringValues = Regex.Split(name, ",");
            return (stringValues.Length > 1 ? (stringValues[1].Trim() + " " + stringValues[0].Trim()) : stringValues[0].Trim());
        }


        
        public static string FirstName(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            var stringValues = Regex.Split(name, ",");
            return (stringValues.Length > 1 ? (stringValues[1].Trim()) : string.Empty);
        }


        
        public static string LastName(this string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            var stringValues = Regex.Split(name, ",");
            return (stringValues.Length > 0 ? (stringValues[0].Trim()) : string.Empty);
        }


        
        public static int AsInt(this string value)
        {
            return Convert.ToInt32(value);
        }

        
        public static string AsYesNo(this bool b)
        {
            return b ? "Yes" : "No";
        }

        
        public static bool AreValidEmails(this IEnumerable<string> emails)
        {
            foreach (var email in emails)
            {
                if (!email.IsValidEmail())
                {
                    return false;
                }
            }
            return true;
        }


        
        public static string CamelCaseAsWords(this string s)
        {
            return string.Join(" ", Regex.Split(s, "(?<!^)(?=[A-Z])"));
        }


        
        public static string CommaDelimited(this IEnumerable<string> list)
        {
            return string.Join(", ", list);
        }



        public static MatchCollection FindTag(this string htmlString, string tag)
        {
            var regex = new Regex(string.Format(@"<{0}[^>]*>(.+?)</{0}>", tag.Trim()), RegexOptions.IgnoreCase);
            // var regex = new Regex(@"(<" + tag.Trim() + @".+?>.+?(?:</" + tag.Trim() + @">){2,})", RegexOptions.IgnoreCase); Nested Tags
            return regex.Matches(htmlString);
        }


        
        public static bool IsValidEmail(this string emailString)
        {
            return emailString != null && Regex.IsMatch(emailString, @"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$", RegexOptions.IgnoreCase);
        }


        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            maxLength = Math.Abs(maxLength);

            return (value.Length <= maxLength
                   ? value
                   : value.Substring(0, maxLength)
                   );
        }


        
        public static string NameFromVerboseUserName(this string verboseUserName)
        {
            var userName = Titles.Aggregate(verboseUserName, (current, title) => current.ReplaceTitle(title, string.Empty)).Trim();
            userName = Suffixes.Aggregate(userName, (current, suffix) => current.ReplaceSuffix(suffix, string.Empty.Trim())).Trim();
            if (string.IsNullOrEmpty(userName))
            {
                return string.Empty;
            }
            var stringValues = Regex.Split(userName, " ");
            if (stringValues.Length == 1)
            {
                return stringValues[0].Trim();
            }

            if (stringValues.Length == 2)
            {
                return (stringValues[1] + ", " + stringValues[0]).Trim();
            }

            if (stringValues.Length > 2)
            {
                return (stringValues[2] + ", " +
                        (string.IsNullOrEmpty(stringValues[0]) ? string.Empty : stringValues[0] + " ") +
                        stringValues[1]);
            }
            return string.Empty;
        }


        
        public static string RemoveStyle(this string stringWithStyles, string style)
        {
            var styleRegExpression = new Regex(@"(?i)" + style + @"\s*:[^;]*;");
            return styleRegExpression.Replace(stringWithStyles, "");
        }


        
        public static string WithoutCarriageReturns(this string s)
        {
            return s.WithoutCarriageReturns(null);
        }

        
        public static string WithoutCarriageReturns(this string s, string replacementDelimiter)
        {
            replacementDelimiter = replacementDelimiter ?? " ";
            return string.Join(replacementDelimiter, Regex.Split(s, "\r\n"));
        }
    }
}
