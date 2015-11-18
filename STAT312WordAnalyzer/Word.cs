using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace STAT312WordAnalyzer
{
    public class Word : IEnumerable
    {
        private string _word;        
        private string _source;        
        private DateTime? _sourceDate;
        private string _topic;  
        private HashSet<char> Chars = new HashSet<char>();

        public Word() { }
        public Word(string word, string source = null, DateTime? sourceDate = null, string topic = "")
        {
            _word = WordAnalyzer.FormatWord(word);
            Source = source;
            SourceDate = sourceDate;
            Topic = topic;

            // add the characters to the Chars HashSet
            for (int i = 0; i < _word.Length; i++)
            {
                Chars.Add(_word[i]);
            }
        }

        [XmlIgnore()]
        public string DateCategory
        {
            get
            {
                if (_sourceDate == null)
                    return DateCategories.NONE;

                if (_sourceDate < DateCategories._1850)
                    return DateCategories.NONE;
                else if (_sourceDate < DateCategories._1900)
                    return DateCategories.EARLY;
                else if (_sourceDate < DateCategories._1980)
                    return DateCategories.MIDDLE;
                else if (_sourceDate < DateTime.Now)
                    return DateCategories.LATE;
                else
                    return DateCategories.NONE;
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
        public string Topic
        {
            get
            {
                return _topic;
            }
            set
            {
                _topic = value;
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

        public override bool Equals(object obj)
        {
            if(obj is Word)
            {
                return _word.Equals((obj as Word)._word);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return _word.GetHashCode();
        }
    }
}
