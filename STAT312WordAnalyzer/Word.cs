using System.Collections.Generic;

namespace STAT312WordAnalyzer
{
    public class Word
    {
        private string _word;

        private HashSet<char> chars = new HashSet<char>();

        public Word(string word)
        {
            _word = word;
            for(int i = 0; i < word.Length; i++)
            {
                chars.Add(_word[i]);
            }
        }

        public int Length
        {
            get
            {
                return _word.Length;
            }
        }

        public int UniqueChars
        {
            get
            {
                return chars.Count;
            }
        }

        public static implicit operator string(Word word)
        {
            return word._word;
        }

        public override string ToString()
        {
            return _word;
        }
    }
}
