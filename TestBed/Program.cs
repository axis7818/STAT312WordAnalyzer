using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STAT312WordAnalyzer;

namespace TestBed
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\tWord Analyzer Test Bed");
            Console.WriteLine();

            Console.Write("Enter a word: ");
            string testString = Console.ReadLine();

            Word testWord = new Word(testString);
            Console.WriteLine("word: \t\t\t" + testWord);
            Console.WriteLine("length: \t\t" + testWord.Length);
            Console.WriteLine("unique characters:\t" + testWord.UniqueChars);
            Console.WriteLine("complexity:\t\t" + WordAnalyzer.WordComplexity(testWord));

            Console.WriteLine();
            Console.Write("Press any key to quit...");
            Console.ReadKey();
        }
    }
}
