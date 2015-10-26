using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace STAT312WordAnalyzer
{
    public static class WordAnalyzer
    {
        #region Fields

        private const string VowelString = "aeiou";

        private static readonly Regex AlphaCheck = new Regex("[A-Za-z]+");

        private static readonly Regex CharRepeatFinder = new Regex("(\\w)(\\1+)");

        private static readonly Dictionary<char, int> LetterValues = new Dictionary<char, int>()
        {
            {'a', 1 }, {'b', 3 }, {'c', 3 }, {'d', 2 }, {'e', 1 }, {'f', 4 }, {'g', 2 }, {'h', 4 }, {'i', 1 },
            { 'j', 1 }, {'k', 5 }, {'l', 1 }, {'m', 3 }, {'n', 1 }, {'o', 1 }, {'p', 3 }, {'q', 10 }, { 'r', 1 },
            { 's', 1 }, {'t', 1 }, {'u', 1 }, {'v', 4 }, {'w', 4 }, {'x', 8 }, {'y', 4 }, {'z', 10 }
        };

        private static readonly Regex SequenceRepeatFinder = new Regex("(\\w{2,}?)(\\1+)");
        private static readonly HashSet<char> Vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };

        private static readonly Regex YVowelCheck = new Regex("(?<![" + VowelString + "])[y](?![" + VowelString + "])");

        #endregion Fields

        #region Methods

        public static int ConsonantCount(Word word)
        {
            // return the number of letters - the number that are vowels
            return word.Length - VowelCount(word);
        }

        public static string FormatWord(Word word)
        {
            string result = "";
            foreach (Match m in AlphaCheck.Matches(word.ToString().ToLower().Trim()))
                result += m.Value;
            return result;
        }

        public static string FormatWord(string s)
        {
            string result = "";
            foreach (Match m in AlphaCheck.Matches(s.ToLower().Trim()))
                result += m.Value;
            return result;
        }

        public static int ScrabbleScore(Word word)
        {
            int count = 0;
            foreach (char c in FormatWord(word))
            {
                // add the scores of each letter
                count += LetterValues[c];
            }
            return count;
        }

        public static int SequenceRepeats(Word word)
        {
            int count = 0;
            foreach (Match m in SequenceRepeatFinder.Matches(FormatWord(word)))
            {
                count += m.Groups[2].Length / m.Groups[1].Length;
            }
            return count >= 0 ? count : 0;
        }

        public static int SequentialCharRepeats(Word word)
        {
            int count = 0;
            foreach (Match m in CharRepeatFinder.Matches(FormatWord(word)))
            {
                count += m.Groups[2].Length;
            }
            return count >= 0 ? count : 0;
        }

        //TODO: see if we can make this work
        public static int SyllableCount(Word word)
        {
            return 0;
        }

        public static float UniquenessFactor(Word word)
        {
            return (float)word.UniqueChars / word.Length;
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

        public static float WordComplexity(Word word)
        {
            return UniquenessFactor(word) * (ScrabbleScore(word) + (3.0f * (float)word.Length) - SequentialCharRepeats(word)) / (SequenceRepeats(word) + 1.0f);
        }

        #endregion Methods
    }
}