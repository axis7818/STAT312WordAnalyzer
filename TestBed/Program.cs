using System;
using STAT312WordAnalyzer;

namespace TestBed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\tWord Analyzer Test Bed");
            Console.WriteLine();

            bool isRunning = true;

            while (isRunning)
            {
                Console.Write("Enter a word ('q' to quit): ");
                string testString = Console.ReadLine();
                if (testString.Equals("q"))
                {
                    isRunning = false;
                }
                else
                {
                    Word testWord = new Word(testString);
                    Console.WriteLine("word: \t\t\t" + testWord);
                    Console.WriteLine("length: \t\t" + testWord.Length);
                    Console.WriteLine("unique characters:\t" + testWord.UniqueChars);
                    Console.WriteLine("complexity:\t\t" + WordAnalyzer.WordComplexity(testWord));
                }

                Console.WriteLine();
            }

            Console.WriteLine("Quitting...");
        }
    }
}
