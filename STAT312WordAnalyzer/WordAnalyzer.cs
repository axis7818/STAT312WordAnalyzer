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
        private static readonly Regex SequenceRepeatFinder = new Regex("(\\w{2,}?)(\\1+)");
        private static readonly HashSet<char> Vowels = new HashSet<char>() { 'a', 'e', 'i', 'o', 'u' };
        private static readonly Regex YVowelCheck = new Regex("(?<![" + VowelString + "])[y](?![" + VowelString + "])");
        private static readonly Regex StartsWithVowelCheck = new Regex(@"^(?=[" + VowelString + "])|(?=y[^" + VowelString + "])");

        public static readonly Dictionary<char, int> LetterValues = new Dictionary<char, int>()
        {
            {'a', 1 }, {'b', 3 }, {'c', 3 }, {'d', 2 }, {'e', 1 }, {'f', 4 }, {'g', 2 }, {'h', 4 }, {'i', 1 },
            { 'j', 1 }, {'k', 5 }, {'l', 1 }, {'m', 3 }, {'n', 1 }, {'o', 1 }, {'p', 3 }, {'q', 10 }, { 'r', 1 },
            { 's', 1 }, {'t', 1 }, {'u', 1 }, {'v', 4 }, {'w', 4 }, {'x', 8 }, {'y', 4 }, {'z', 10 }
        };
        public static readonly Dictionary<char, float> LetterFrequency = new Dictionary<char, float>()
        {
            {'a', 0.08167f }, {'b', 0.01492f }, {'c', 0.02782f }, {'d', 0.04253f }, {'e', 0.12702f }, {'f', 0.02228f }, {'g', 0.02015f }, {'h', 0.06094f }, {'i', 0.06966f },
            {'j', 0.00153f }, {'k', 0.00772f }, {'l', 0.04025f }, {'m', 0.02406f }, {'n', 0.06749f }, {'o', 0.07507f }, {'p', 0.01929f }, {'q', 0.00095f }, {'r', 0.05987f },
            {'s', 0.06327f }, {'t', 0.09056f }, {'u', 0.02758f }, {'v', 0.00978f }, {'w', 0.02361f }, {'x', 0.00150f }, {'y', 0.01974f }, {'z', 0.00074f }
        };
        public static readonly Dictionary<char, float> FirstLetterFrequency = new Dictionary<char, float>()
        {
            {'a', 0.11602f }, {'b', 0.04702f }, {'c', 0.03511f }, {'d', 0.02670f }, {'e', 0.02007f }, {'f', 0.03779f }, {'g', 0.01950f }, {'h', 0.07232f }, {'i', 0.06286f },
            {'j', 0.00597f }, {'k', 0.00590f }, {'l', 0.02705f }, {'m', 0.04383f }, {'n', 0.02365f }, {'o', 0.06264f }, {'p', 0.02545f }, {'q', 0.00173f }, {'r', 0.01653f },
            {'s', 0.07755f }, {'t', 0.16671f }, {'u', 0.01487f }, {'v', 0.00649f }, {'w', 0.06753f }, {'x', 0.00017f }, {'y', 0.01620f }, {'z', 0.00034f }
        };

        #endregion Fields

        #region Methods

        public static bool StartsWithVowel(Word word)
        {
            return StartsWithVowelCheck.IsMatch(FormatWord(word));
        }

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
            float letterScore = UniquenessFactor(word) * (word.Length + LetterFrequencyScore(word));
            float repeatScore = (SequenceRepeats(word) + SequentialCharRepeats(word)) / word.Length;
            return 10 * AverageLetterFrequency(word) * (letterScore - repeatScore)  ;
        }

        public static float AverageLetterFrequency(Word word)
        {
            float result = 0;
            foreach (char c in word)
                result += LetterFrequency[c];
            result /= word.Length;
            return result;
        }

        public static float LetterFrequencyScore(Word word)
        {
            float result = 0;

            foreach(char c in word)
            {
                result += 1 - LetterFrequency[c];
            }

            result -= FirstLetterFrequency[word.ToString()[0]];

            return result;
        }

        #endregion Methods
    }
}