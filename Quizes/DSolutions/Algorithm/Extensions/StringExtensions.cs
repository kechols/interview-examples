using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Kevins.DSolutions.Algorithm.Extensions
{
    public static class StringExtensions
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string FindFirstStringPair(this string searchString, int matchLength)
        {
            int [] firstOccurnceInPairIndexes = new [] {0, 0};
            var firstStringPair = string.Empty;

            if (matchLength < 1 || string.IsNullOrEmpty(searchString))
            {
                return firstStringPair;
            }

            for (var startingIndex = 0; (startingIndex < (searchString.Length - matchLength)); startingIndex++)
            {
                var pattern = searchString.Substring(startingIndex, matchLength);
                var patternIndexes = GetPatternIndexes(searchString, pattern);
                if (patternIndexes.Length > 1)
                {
                    if (string.IsNullOrEmpty(firstStringPair) ||
                        (firstOccurnceInPairIndexes[1] < patternIndexes[1])
                        )
                    {
                        firstStringPair = pattern;
                        firstOccurnceInPairIndexes = patternIndexes;
                    }
                }
            }

            return firstStringPair;
        }



        private static int[] GetPatternIndexes(this string searchString, string pattern)
        {
            return Regex.Matches(searchString, pattern).Cast<Match>().Select(m => m.Index).ToArray();
        }
    }
}
