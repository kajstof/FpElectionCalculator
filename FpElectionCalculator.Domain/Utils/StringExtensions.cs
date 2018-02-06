using System;
using System.Linq;

namespace FpElectionCalculator.Domain.Utils
{
    public static class StringExtensions
    {
        public static string CapitalizeAllWords(this string str)
        {
            var splittedText = str.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", splittedText.Select(s => s.UppercaseFirstLetters()));
        }

        public static string UppercaseFirstLetters(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
    }
}