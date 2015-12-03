using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHunspell;
using System.Reflection;
using STAT312WordAnalyzer;

namespace WordVerifier
{
    class Program
    {
        private static readonly string RESULT_FILE = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "results.txt");

        static void Main(string[] args)
        {
            #region OpenFiles
            // get the file reader
            StreamReader reader;
            try
            {
                reader = new StreamReader(args[0]);
            }   
            catch(Exception e)
            {
                Console.WriteLine("an error occurred while opening the file: " + e.Message);
                return;
            }

            // get the file writer
            if (!File.Exists(RESULT_FILE))
                File.Create(RESULT_FILE);
            StreamWriter writer = new StreamWriter(RESULT_FILE);
            writer.WriteLine(DataFileManager.minitabFileHeader);
            #endregion

            Word word;
            int numWords = 0;
            HashSet<string> okWords = new HashSet<string>();
            HashSet<string> ignoreWords = new HashSet<string>();

            Console.WriteLine("Going through file...\n");

            using (Hunspell checker = new Hunspell("en_us.aff", "en_us.dic"))
            {
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    word = DataFileManager.MinitabFileLine(reader.ReadLine());
                    if (checker.Spell(word.ToString()) || okWords.Contains(word.ToString()))
                    {
                        writer.WriteLine(DataFileManager.MinitabFileLine(word));
                    }
                    else if(!ignoreWords.Contains(word.ToString()))
                    {
                        ColorWrite("Word #: " + numWords, ConsoleColor.Green);
                        Console.Write(word.Source + ": '" + word.ToString() + "'" + " was not recognized as a word. Do you want to keep it? (y/n): ");
                        if (Console.ReadKey().Key == ConsoleKey.Y)
                        {
                            writer.WriteLine(DataFileManager.MinitabFileLine(word));
                            okWords.Add(word.ToString());
                        }
                        else
                        {
                            Console.Write("\nDo you want to offer an alternate spelling? (y/n): ");
                            if (Console.ReadKey().Key == ConsoleKey.Y)
                            {
                                Console.Write("\nspelling: ");
                                string input = Console.ReadLine();
                                foreach(string s in Sampler.Tokenize(input))
                                {
                                    Word newWord = new Word(s, word.Source, word.SourceDate, word.Topic);
                                    writer.WriteLine(DataFileManager.MinitabFileLine(newWord));
                                    okWords.Add(newWord.ToString());
                                }
                            }
                            else
                            {
                                ColorWrite("\nThe word will be ignored.", ConsoleColor.Red);
                                ignoreWords.Add(word.ToString());
                            }
                        }
                        Console.WriteLine("\n");
                    }

                    numWords++;
                }
            }

            // End the program
            ColorWrite(numWords + " words processed.", ConsoleColor.Green);
            reader.Close();
            writer.Close();
        }

        private static void ColorWrite(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
