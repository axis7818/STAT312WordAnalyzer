﻿using System;
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
                    Console.WriteLine("\t\t(" + WordAnalyzer.FormatWord(testWord) + ")");
                    Separator();
                    Console.WriteLine("\tlength: \t\t" + testWord.Length);
                    Console.WriteLine("\tunique characters:\t" + testWord.UniqueChars);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\tconsecutive repeats:\t" + WordAnalyzer.SequentialCharRepeats(testWord));
                    Console.WriteLine("\tconsecutive sequences:\t" + WordAnalyzer.SequenceRepeats(testWord));
                    Console.ResetColor();
                    Console.WriteLine("\tscrabble score:\t\t" + WordAnalyzer.ScrabbleScore(testWord));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\tfrequency score:\t" + WordAnalyzer.LetterFrequencyScore(testWord));
                    Console.WriteLine("\taverage letter freq:\t" + WordAnalyzer.AverageLetterFrequency(testWord));
                    Console.ResetColor();
                    Separator();
                    WriteLineWithColor("\tStarts with Vowel:\t" + (WordAnalyzer.StartsWithVowel(testWord) ? "yes" : "no"), ConsoleColor.Green);
                    Console.WriteLine("\tvowels:\t\t\t" + WordAnalyzer.VowelCount(testWord));
                    Console.WriteLine("\tconsonants:\t\t" + WordAnalyzer.ConsonantCount(testWord));
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\tsyllables:\t\t" + WordAnalyzer.SyllableCount(testWord));
                    Console.ResetColor();
                    Separator();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\tcomplexity:\t\t" + WordAnalyzer.WordComplexity(testWord));
                    Console.ResetColor();
                    Separator();
                }
                Console.WriteLine("\n");
            }

            Console.WriteLine("Quitting...");
        }

        private static void WriteLineWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
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
