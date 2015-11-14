using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace STAT312WordAnalyzer
{
    public static class DataFileManager
    {
        public const string wordsFileName = "WordResultData.txt";

        public const string sourceTextFileName = "SourceText.txt";

        private const string minitabFileHeader = "Word\tSource\tDate\tTopic\tComplexity\tLogComplexity\tLength\tUniquenessFactor\tUniqueChars\tVowels\tVowelProportion\tConsonants\tConsonantProportion\tFirstLetter\tStartsWithVowel";

        public static readonly string localWordsFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), wordsFileName);

        public static readonly string localSourceTextFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), sourceTextFileName);

        public static void WriteMinitabFile(string outputPath, List<Word> words)
        {
            if (words == null || words.Any(w => w == null))
                throw new ArgumentNullException("words or a Word in words is equal to null");

            using (StreamWriter writer = new StreamWriter(outputPath))
            {
                writer.WriteLine(minitabFileHeader);
                foreach (Word w in words)
                {
                    writer.WriteLine(MinitabFileLine(w));
                }
            }
        }

        private static string MinitabFileLine(Word word)
        {
            int length = word.Length;
            float complexity = WordAnalyzer.WordComplexity(word);
            int vowelCount = WordAnalyzer.VowelCount(word);
            int consonantCount = length - vowelCount;
            char? firstChar;
            try
            {
                firstChar = word.ToString()[0];
            }
            catch (IndexOutOfRangeException) { firstChar = null; }

            string result = word.ToString() + "\t" + word.Source + "\t" + word.SourceDateString + "\t" + word.Topic + "\t" + complexity + "\t" + Math.Log10(complexity) + "\t" + length +
                "\t" + WordAnalyzer.UniquenessFactor(word) + "\t" + word.UniqueChars + "\t" + vowelCount + "\t" + (vowelCount / (float)length) + "\t" + consonantCount +
                "\t" + (consonantCount / (float)length) + "\t" + (firstChar.ToString() ?? "") + "\t" + (WordAnalyzer.StartsWithVowel(word) ? "yes" : "no");
            return result;
        }

        private static Word MinitabFileLine(string line)
        {
            string[] tokens = line.Split('\t');
            DateTime? dateTime = null;
            try
            {
                dateTime = DateTime.Parse(tokens[2]);
            }
            catch (FormatException) { }

            return new Word(tokens[0], tokens[1], dateTime, tokens[3]);
        }

        public static void CopyFileToDesktop(string fileName, bool force = false)
        {
            if (!File.Exists(localWordsFilePath))
                throw new FileNotFoundException("could not find the local file");

            string desktopWordsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName + ".txt");
            if (force)
            {
                File.Copy(localWordsFilePath, desktopWordsFilePath, true);
            }
            else
            {
                if (File.Exists(desktopWordsFilePath))
                    throw new FileOverwriteException("Desktop file already exists.");

                File.Copy(localWordsFilePath, desktopWordsFilePath);
            }
        }

        public static void DeleteWordFile()
        {
            if (File.Exists(localWordsFilePath))
                File.Delete(localWordsFilePath);
        }

        public static void WriteWordFile(List<Word> words)
        {
            if (words == null || words.Any(w => w == null))
                throw new ArgumentNullException("words or a Word in words is equal to null");

            using (StreamWriter writer = new StreamWriter(localWordsFilePath))
            {
                writer.WriteLine(minitabFileHeader);
                foreach(Word w in words)
                {
                    writer.WriteLine(MinitabFileLine(w));
                }
            }
        }
        
        public static List<Word> ReadWordFile()
        {
            if (!File.Exists(localWordsFilePath))
                throw new FileNotFoundException(localWordsFilePath + " was not found.");

            List<Word> result = new List<Word>();

            using (StreamReader reader = new StreamReader(localWordsFilePath))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    try
                    {
                        Word next = MinitabFileLine(reader.ReadLine());
                        if (next != null)
                            result.Add(next);
                    }
                    catch(Exception e)
                    {
                        throw new SessionFileReadException("Session file could not be read.", e);
                    }
                }
            }

            return result;
        }

        public static void WriteSourceTextFile(string sourceText)
        {
            using (StreamWriter writer = new StreamWriter(localSourceTextFilePath))
            {
                writer.WriteLine(sourceText);
            }
        }

        public static string ReadSourceTextFile()
        {
            if (!File.Exists(localSourceTextFilePath))
                throw new FileNotFoundException(localSourceTextFilePath + " was not found.");

            string result = "";
            using (StreamReader reader = new StreamReader(localSourceTextFilePath))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public static void DeleteLocalSourceTextFile()
        {
            if (File.Exists(localSourceTextFilePath))
                File.Delete(localSourceTextFilePath);
        }
    }
}
