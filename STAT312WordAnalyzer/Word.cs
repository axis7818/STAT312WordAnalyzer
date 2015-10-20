using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace STAT312WordAnalyzer
{
    public class Word : IEnumerable
    {
        private string _word;

        private HashSet<char> Chars = new HashSet<char>();
        
        public Word(string word)
        {
            _word = word;

            // add the characters to the Chars HashSet
            for(int i = 0; i < _word.Length; i++)
            {
                Chars.Add(_word[i]);
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
                return Chars.Count;
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

        public IEnumerator GetEnumerator()
        {
            foreach (char c in _word)
                yield return c;
        }
    }
}
