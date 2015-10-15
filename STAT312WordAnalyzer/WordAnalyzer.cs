using System.Collections.Generic;
using System.Linq;

namespace STAT312WordAnalyzer
{
    public static class WordAnalyzer
    {
        private static readonly HashSet<char> vowels = new HashSet<char>() { 'A', 'E', 'I', 'O', 'U', 'a', 'e', 'i', 'o', 'u' };

        public static int VowelCount(Word word)
        {
            int count = 0;
            foreach (char c in word)
            {
                if (vowels.Contains(c))
                    count++;
            }
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
