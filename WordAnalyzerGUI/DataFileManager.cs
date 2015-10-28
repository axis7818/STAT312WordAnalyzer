﻿using System;
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
        public const string wordsFileName = "WordResultData.txt";

        public const string sourceTextFileName = "SourceText.txt";

        private const string minitabFileHeader = "Word\tSource\tDate\tComplexity";

        public static readonly string localWordsFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), wordsFileName);

        public static readonly string localSourceTextFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), sourceTextFileName);

        public static readonly string desktopWordsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), wordsFileName);

        private static string MinitabFileLine(Word word)
        {
            return word.ToString() + "\t" + word.Source + "\t" + word.SourceDateString + "\t" + WordAnalyzer.WordComplexity(word);
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

            return new Word(tokens[0], tokens[1], dateTime);
        }

        public static void CopyFileToDesktop(bool force = false)
        {
            if (!File.Exists(localWordsFilePath))
                throw new FileNotFoundException("could not find the local file");

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

        public static void WriteFile(List<Word> words)
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
        
        public static List<Word> ReadFile()
        {
            if (!File.Exists(localWordsFilePath))
                throw new FileNotFoundException(localWordsFilePath + " was not found.");

            List<Word> result = new List<Word>();

            using (StreamReader reader = new StreamReader(localWordsFilePath))
            {
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
    }
}
