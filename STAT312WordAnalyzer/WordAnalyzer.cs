using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace STAT312WordAnalyzer
{
    public static class WordAnalyzer
    {
        private const string VowelString = "aeiou";

        private static readonly HashSet<char> Vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };

        // Matches a 'y' that is not surrounded by other 'non-y' vowels
        private static readonly Regex YVowelCheck = new Regex("(?<![" + VowelString + "])[y](?![" + VowelString + "])");
        
        public static int VowelCount(Word word)
        {
            // simplify the format of the word
            string formattedWord = word.ToString().ToLower().Trim();

            // the number of vowels that are in the word
            int count = 0;

            // go through each letter and check for the simple vowels
            foreach (char c in formattedWord)
            {
                if (Vowels.Contains(c))
                    count++;
            }

            // check for 'y' vowels
            count += YVowelCheck.Matches(formattedWord).Count;

            // return the count
            return count;
        }

        public static int ConsonantCount(Word word)
        {
            // return the number of letters - the number that are vowels
            return word.Length - VowelCount(word);
        }

        public static int SyllableCount(Word word)
        {
            // get a lowercase/trimmed version of the string
            string formattedWord = word.ToString().ToLower().Trim();

            // the number of syllables in the word
            int count = 0;



            // return the result
            return count;
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
