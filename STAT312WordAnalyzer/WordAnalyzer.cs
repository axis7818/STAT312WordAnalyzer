using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace STAT312WordAnalyzer
{
    public static class WordAnalyzer
    {
        private static readonly string VowelString = "AEIOUaeiou";

        private static readonly HashSet<char> Vowels = new HashSet<char>() { 'A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u' };

        private static readonly Regex YVowelCheck = new Regex("(?<![" + VowelString + "Yy])[Yy](?![" + VowelString + "])");

        public static int VowelCount(Word word)
        {
            // the number of vowels that are in the word
            int count = 0;

            // go through each letter and check for the simple vowels
            foreach (char c in word)
            {
                if (Vowels.Contains(c))
                    count++;
            }

            // check for 'y' vowels
            MatchCollection yVowels = YVowelCheck.Matches(word);
            count += yVowels.Count;

            // return the count
            return count;
        }

        public static int ConsonantCount(Word word)
        {
            return word.Length - VowelCount(word);
        }

        public static float WordComplexity(Word word)
        {
            return UniquenessFactor(word) * word.Length;
        }

        public static float UniquenessFactor(Word word)
        {
            return (float)word.UniqueChars / (float)word.Length;
        }
    }
}
