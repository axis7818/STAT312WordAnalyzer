using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace STAT312WordAnalyzer
{
    public class Word : IEnumerable
    {
        private string _word;
        
        private string _source;
        
        private DateTime? _sourceDate;
        
        private HashSet<char> Chars = new HashSet<char>();

        public Word() { }

        public Word(string word, string source = null, DateTime? sourceDate = null)
        {
            _word = WordAnalyzer.FormatWord(word);
            Source = source;
            SourceDate = sourceDate;

            // add the characters to the Chars HashSet
            for (int i = 0; i < _word.Length; i++)
            {
                Chars.Add(_word[i]);
            }
        }

        [XmlIgnore()]
        public string SourceDateString
        {
            get
            {
                if (SourceDate != null)
                {
                    return SourceDate.ToString().Split(' ')[0];
                }
                else
                    return "";
            }
        }

        [XmlAttribute()]
        public DateTime? SourceDate
        {
            get
            {
                return _sourceDate;
            }
            set
            {
                _sourceDate = value;
            }
        }

        [XmlAttribute()]
        public string Source
        {
            get
            {
                return _source;
            }
            set
            {
                _source = value;
            }
        }

        [XmlAttribute()]
        public string Text
        {
            get
            {
                return _word;
            }
            set
            {
                _word = value;
            }
        }

        [XmlIgnore()]
        public int Length
        {
            get
            {
                return _word.Length;
            }
        }

        [XmlIgnore()]
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
