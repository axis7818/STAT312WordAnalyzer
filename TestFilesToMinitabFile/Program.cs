using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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

        static void Main(string[] args)
        {
            /* Introduction Message */
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine("This program will process text files in the directory: " + textFilesDirectory);
            Console.WriteLine("Place all .txt files to be processed in that directory and run this program.");
            Console.WriteLine("All files must have the date as their title. If an invalid date format is found, " +
                "the user will be asked for another format.");
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.WriteLine();

            /* Get the sample size from the user */
            Console.Write("Enter the sample size to collect from each file: ");
            string userInput = Console.ReadLine();
            if(!int.TryParse(userInput, out SampleSize))
            {
                WriteWithColor("Invalid Sample Size. Ending Program...", ConsoleColor.Red);
                return;
            }
            Console.WriteLine("The sample size: " + SampleSize + " will be used.\n");

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
                    }
                    

                }
                else
                {
                    WriteWithColor(fileName + " was skipped because it is not a text file.", ConsoleColor.Yellow);   
                }
            }

            /* Summary */
            Console.WriteLine(ProcessedFiles + " files were processed.");
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
