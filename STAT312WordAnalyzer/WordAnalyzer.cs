using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace STAT312WordAnalyzer
{
    public static class WordAnalyzer
    {
        #region Fields
        private static readonly Dictionary<char, int> LetterValues = new Dictionary<char, int>()
        {
            {'a', 1 }, {'b', 3 }, {'c', 3 }, {'d', 2 }, {'e', 1 }, {'f', 4 }, {'g', 2 }, {'h', 4 }, {'i', 1 },
            { 'j', 1 }, {'k', 5 }, {'l', 1 }, {'m', 3 }, {'n', 1 }, {'o', 1 }, {'p', 3 }, {'q', 10 }, { 'r', 1 },
            { 's', 1 }, {'t', 1 }, {'u', 1 }, {'v', 4 }, {'w', 4 }, {'x', 8 }, {'y', 4 }, {'z', 10 }
        };

        private const string VowelString = "aeiou";

        private static readonly HashSet<char> Vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
        
        private static readonly Regex YVowelCheck = new Regex("(?<![" + VowelString + "])[y](?![" + VowelString + "])");
        
        private static readonly Regex AlphaCheck = new Regex("[A-Za-z]+");
        #endregion Fields

        #region Methods
        public static string FormatWord(Word word)
        {
            string result = "";
            foreach (Match m in AlphaCheck.Matches(word.ToString().ToLower().Trim()))
                result += m.Value;
            return result;
        }

        public static int ScrabbleScore(Word word)
        {
            int count = 0;
            foreach(char c in FormatWord(word))
            {
                // add the scores of each letter
                count += LetterValues[c];
            }
            return count;
        }
        
        //TODO: see if we can make this work
        public static int SyllableCount(Word word)
        {
            return 0;
        }
        
        public static int VowelCount(Word word)
        {
            // simplify the format of the word
            string formattedWord = FormatWord(word);

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

        public static float WordComplexity(Word word)
        {
            return UniquenessFactor(word) * ScrabbleScore(word);
        }

        public static float UniquenessFactor(Word word)
        {
            return (float)word.UniqueChars / (float)word.Length;
        }
        #endregion Methods
    }
}
