using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace STAT312WordAnalyzer
{
    public static class WordAnalyzer
    {
        private static readonly Dictionary<char, int> LetterValues = new Dictionary<char, int>()
        {
            {'a', 1 }, {'b', 3 }, {'c', 3 }, {'d', 2 }, {'e', 1 }, {'f', 4 }, {'g', 2 }, {'h', 4 },
            {'i', 1 }, {'j', 1 }, {'k', 5 }, {'l', 1 }, {'m', 3 }, {'n', 1 }, {'o', 1 }, {'p', 3 },
            {'q', 10 }, {'r', 1 }, {'s', 1 }, {'t', 1 }, {'u', 1 }, {'v', 4 }, {'w', 4 }, {'x', 8 },
            {'y', 4 }, {'z', 10 }
        };

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
