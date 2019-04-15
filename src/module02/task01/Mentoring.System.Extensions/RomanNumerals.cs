using System;
using System.Collections.Generic;
using System.Linq;

namespace Mentoring.System.Extensions
{
    public static class IntExtensions
    {
        private static readonly IReadOnlyDictionary<int, string> ArabicToRomanMapping = new Dictionary<int, string>
        {
            {1, "I"},
            {4, "IV"},
            {5, "V"},
            {9, "IX"},
            {10, "X"},
            {40, "XL"},
            {50, "L"},
            {90, "XC"},
            {100, "C"},
            {400, "CD"},
            {500, "D"},
            {900, "CM"},
            {1000, "M"},
        };

        private static int GetClosest(int number) => ArabicToRomanMapping.Keys.Where(i => i <= number).Max();


        public static string ToRoman(this int number)
        {
            if (number <= 0 || number >= 4000)
            {
                throw new ArgumentOutOfRangeException(nameof(number), number,
                    "The number must be between 1 and 3999.");
            }

            var closestToInput = GetClosest(number);

            return number == closestToInput
                ? ArabicToRomanMapping[number]
                : ArabicToRomanMapping[closestToInput] + ToRoman(number - closestToInput);
        }
    }
}
