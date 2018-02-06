using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace FpElectionCalculator.Domain.Utils
{
    public static class ChoiceParser
    {
        public static int[] Parse(string str, int range)
        {
            int[] answer = null;
            if (Regex.IsMatch(str, @"^[\d, ]*$"))
            {
                string[] splitted = str.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                answer = new int[splitted.Length];
                for (int i = 0; i < splitted.Length; i++)
                {
                    answer[i] = int.Parse(splitted[i].Replace(" ", string.Empty));
                    if (answer[i] > range || answer[i] < 1)
                    {
                        answer = null;
                        break;
                    }
                }

                if (answer != null && answer.Length > 1)
                {
                    answer = answer.Distinct().ToArray();
                    Array.Sort(answer, (i1, i2) => i2.CompareTo(i1));
                }
            }

            return answer;
        }
    }
}