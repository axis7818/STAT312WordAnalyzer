using System;
using STAT312WordAnalyzer;

namespace TestBed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\tWord Analyzer Test Bed");
            Console.WriteLine("\tEnter a word to get information.");
            Console.WriteLine("\tPress 'q' to quit.");
            Separator(true);
            Console.WriteLine();

            bool isRunning = true;

            while (isRunning)
            {
                Console.Write("> ");
                string testString = Console.ReadLine();
                if (testString.Equals("q"))
                {
                    isRunning = false;
                }
                else
                {
                    Word testWord = new Word(testString);
                    Separator();
                    Console.WriteLine("\tlength: \t\t" + testWord.Length);
                    Console.WriteLine("\tunique characters:\t" + testWord.UniqueChars);
                    Separator();
                    Console.WriteLine("\tvowels:\t\t\t" + WordAnalyzer.VowelCount(testWord));
                    Console.WriteLine("\tconsonants:\t\t" + WordAnalyzer.ConsonantCount(testWord));
                    Separator();
                    Console.WriteLine("\tcomplexity:\t\t" + WordAnalyzer.WordComplexity(testWord));
                    Separator();
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine("Quitting...");
        }

        static void Separator(bool bold = false)
        {
            if (bold)
            {
                Console.WriteLine("//////////////////////////////////////////////////");
            }
            else
            {
                Console.WriteLine("--------------------------------------------------");
            }            
        }
    }
}
