using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kevins.Examples.Common.Tests.Unit.Helpers
{
    public static class UnitTestHelper
    {
        public static string GetLineDifferenceFailureMessage(this IEnumerable<string> linesThatAreDifferent)
        {
            return "The following lines are different: " + string.Join(", ", linesThatAreDifferent);
        }


        public static bool DictionariesAreEqual(this Dictionary<string, string> hashtable1, ConcurrentDictionary<string, string> hashtable2)
        {
            return hashtable1.Keys.Count == hashtable2.Keys.Count && 
                hashtable1.Keys.All(
                    key => hashtable1[key].ToString().Trim() == hashtable2[key].ToString().Trim()
                );
        }


        public static IEnumerable<string> IsFileContentsEqual (this string filename, string expectedFileContents)
        {
            return AreEqual(File.ReadAllLines(filename), expectedFileContents.AsLines());
        }


        private static IEnumerable<string> AreEqual(string[] expected, string[] actual)
        {
            return  actual.Except(expected).
                Where(s => !string.IsNullOrWhiteSpace(s));
        }


        private static string[] AsLines(this string stringValue)
        {
            return stringValue.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
