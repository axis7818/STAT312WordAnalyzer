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
            Word testWord = new Word("apple");
            Console.WriteLine(testWord);
            Console.WriteLine(testWord.Length);
            Console.WriteLine(testWord.UniqueChars);

            Console.Write("Press any key to quit...");
            Console.ReadKey();
        }
    }
}
