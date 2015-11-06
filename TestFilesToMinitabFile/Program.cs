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
        private static readonly string resultFilePath = Path.Combine(Assembly.GetEntryAssembly().Location, resultFileName);
        private static readonly string textFilesDirectory = Path.Combine(Assembly.GetEntryAssembly().Location, textFileFolderName);
        private static readonly Regex textFileChecker = new Regex("^.*\\.txt$");
        
        static void Main(string[] args)
        {
            /* Start the program */
            Console.WriteLine("Going through files...");

            /* Make sure the input directory exists */
            if (!Directory.Exists(textFilesDirectory))
            {
                Directory.CreateDirectory(textFilesDirectory);
                Console.WriteLine("input directory [" + textFilesDirectory + "] was not found, so it was created. Start the program again with files in that directory.");
                return;
            }

            /* iterate through the files in the directory */
            foreach (string fileName in Directory.EnumerateFiles(textFilesDirectory))
            {
                if (textFileChecker.IsMatch(fileName))
                {
                    /* PUT CODE HERE TO PROCESS THE FILES */
                }
                else
                {
                    WriteWithColor(fileName + " was skipped because it is not a text file.", ConsoleColor.Yellow);   
                }
            }
        }

        private static void WriteWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
