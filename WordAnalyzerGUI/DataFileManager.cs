using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using STAT312WordAnalyzer;

namespace WordAnalyzerGUI
{
    public static class DataFileManager
    {
        public const string fileName = "WordResultData.txt";

        private const string minitabFileHeader = "Word\tSource\tDate\tComplexity";

        public static readonly string localFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), fileName);

        public static readonly string desktopFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);

        private static string MinitabFileLine(Word word)
        {
            return word.ToString() + "\t" + word.Source + "\t" + word.SourceDateString + "\t" + WordAnalyzer.WordComplexity(word);
        }

        private static Word MinitabFileLine(string line)
        {
            try
            {
                string[] tokens = line.Split('\t');
                return new Word(tokens[0], tokens[1], DateTime.Parse(tokens[2]));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void CopyFileToDesktop(bool force = false)
        {
            if (!File.Exists(localFilePath))
                throw new FileNotFoundException("could not find the local file");

            if (force)
            {
                File.Copy(localFilePath, desktopFilePath, true);
            }
            else
            {
                if (File.Exists(desktopFilePath))
                    throw new FileOverwriteException("Desktop file already exists.");

                File.Copy(localFilePath, desktopFilePath);
            }
        }

        public static void WriteFile(List<Word> words)
        {
            if (words == null || words.Any(w => w == null))
                throw new ArgumentNullException("words or a Word in words is equal to null");

            using (StreamWriter writer = new StreamWriter(localFilePath))
            {
                writer.WriteLine(minitabFileHeader);
                foreach(Word w in words)
                {
                    writer.WriteLine(MinitabFileLine(w));
                }
            }
        }

        public static List<Word> ReadFile()
        {
            if (!File.Exists(localFilePath))
                throw new FileNotFoundException(localFilePath + " was not found.");

            List<Word> result = new List<Word>();
            using (StreamReader reader = new StreamReader(localFilePath))
            {
                while (!reader.EndOfStream)
                {
                    Word next = MinitabFileLine(reader.ReadLine());
                    if (next != null)
                        result.Add(next);
                }
            }
            return result;
        }
    }
}
