using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using STAT312WordAnalyzer;

namespace TextFilesToMinitabFile
{
    class Program
    {
        private const string textFileFolderName = "TextFiles";
        private const string resultFileName = "Results.txt";
        private static readonly string resultFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), resultFileName);
        private static readonly string textFilesDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), textFileFolderName);
        private static readonly Regex textFileChecker = new Regex("^.*\\.txt$");
        private static int SampleSize = 0;
        private static int ProcessedFiles = 0;
        private static string Source = null;
        private static string Topic = null;
        private static bool AllText = false;

        static void Main(string[] args)
        {
            /* Find out if all words should be used */
            if(args.Length > 0)
            {
                if (args.Contains("full") || args.Contains("all"))
                    AllText = true;
            }

            /* Introduction Message */
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("This program will process text files in the directory: " + textFilesDirectory);
            Console.WriteLine("Place all .txt files to be processed in that directory and run this program.");
            Console.WriteLine("All files must have the date as their title. If an invalid date format is found, " +
                "the user will be asked for another format.");
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine();

            /* Get the source from the user */
            Console.Write("Enter the source that the files were pulled from: ");
            Source = Console.ReadLine();

            /* Get the topic of the word */
            Console.Write("Enter the topic of the text (Enter for no topic): ");
            Topic = Console.ReadLine();


            /* Get the sample size from the user */
            if (!AllText)
            {
                Console.Write("Enter the sample size to collect from each file: ");
                string userInput = Console.ReadLine();
                if(!int.TryParse(userInput, out SampleSize))
                {
                    WriteWithColor("Invalid Sample Size. Ending Program...", ConsoleColor.Red);
                    return;
                }
                Console.WriteLine("The sample size [" + SampleSize + "] will be used to get words from [" + Source + "].\n");
            }
            else
            {
                SampleSize = 100; // set to 100% if getting all text
            }

            /* Start the program */
            Console.WriteLine("Processing files...\n");

            /* Make sure the input directory exists */
            if (!Directory.Exists(textFilesDirectory))
            {
                Directory.CreateDirectory(textFilesDirectory);
                Console.WriteLine("input directory [" + textFilesDirectory + "] was not found, so it was created. " + 
                    "Start the program again with files in that directory.");
                return;
            }

            /* the sample words */
            List<Word> sampleWords = new List<Word>();

            /* iterate through the files in the directory */
            foreach (string fileName in Directory.EnumerateFiles(textFilesDirectory))
            {
                /* Check if the file is a .txt file */
                if (textFileChecker.IsMatch(fileName))
                {
                    /* Get the file path of the file and the name of the file without the extension */
                    string path = Path.Combine(textFilesDirectory, fileName);
                    string date = Path.GetFileNameWithoutExtension(path);

                    /* Get the date */
                    DateTime dateTime;
                    while(!DateTime.TryParse(date, out dateTime))
                    {
                        Console.WriteLine("Failed to convert: " + date);
                        Console.Write("Enter another value: ");
                        date = Console.ReadLine();
                        Console.WriteLine();
                    }

                    /* read the contents of the file */
                    List<string> sourceWords;
                    using (StreamReader reader = new StreamReader(path))
                    {
                        sourceWords = Sampler.Tokenize(reader.ReadToEnd());
                    }

                    /* Get a sample of the words */
                    foreach(string word in Sampler.GetSample(sourceWords, SampleSize, AllText))
                    {
                        sampleWords.Add(new Word(word, Source, dateTime, Topic));
                    }

                    /* increment the # of processed files */
                    ProcessedFiles++;
                }
                else
                {
                    WriteWithColor(fileName + " was skipped because it is not a text file.", ConsoleColor.Yellow);   
                }
            }

            /* Write the results file */
            sampleWords = sampleWords.OrderBy(w => w.ToString()).ToList();
            DataFileManager.WriteMinitabFile(resultFilePath, sampleWords);
            
            /* Summary */
            WriteWithColor(ProcessedFiles + " files were processed.", ConsoleColor.Green);
            WriteWithColor(sampleWords.Count + " words were grabbed.", ConsoleColor.Green);
            Console.WriteLine();
            Console.WriteLine("The resulting file: " + resultFilePath);
        } 

        private static void WriteWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
