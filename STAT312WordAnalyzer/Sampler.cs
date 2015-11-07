using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace STAT312WordAnalyzer
{
    public static class Sampler
    {
        private static readonly Regex Alphafier = new Regex("[A-Za-z]+");

        private static readonly Regex Numberfier = new Regex("[\\d]+");

        private static readonly Regex Tokenizer = new Regex("\\S+");

        public static List<string> Tokenize(string input)
        {
            List<string> result = new List<string>();

            foreach (Match m1 in Tokenizer.Matches(input))
            {
                string token = "";

                foreach (Match m2 in Alphafier.Matches(m1.Value))
                {
                    token += m2.Value;
                }

                if (!string.IsNullOrWhiteSpace(token))
                    result.Add(token);
            }

            return result;
        }

        public static List<string> GetSample(List<string> source, int size, bool useProportion)
        {
            if (size < 0)
                size = 0;

            if (useProportion)
            {
                if (size > 100)
                    size = 100;
                float proportion = size / 100.0f;
                float sizeFloat = source.Count * proportion;
                size = (int)sizeFloat;
            }

            return GetSampleWithSize(source, size);
        }

        private static List<string> GetSampleWithSize(List<string> source, int size)
        {
            if (size > source.Count)
                throw new ArgumentException("Sample size (" + size.ToString() + ") must be smaller than the source count.");

            List<string> input = new List<string>(source);
            List<string> result = new List<string>();
            Random rand = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < size; i++)
            {
                int index = rand.Next() % input.Count;
                result.Add(input[index]);
                input.RemoveAt(index);
            }
            return result;
        }
    }
}
