using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STAT312WordAnalyzer;

namespace MinitabDataGenerationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // input
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(args[0]);
            }
            catch (IndexOutOfRangeException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No file to read from. Please give the file as a command line argument.");
                Console.ResetColor();
                goto END;
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("File " + args[0] + " was not found.");
                Console.ResetColor();
                goto END;
            }

            // output
            string resultFilePath = "./Results.txt";
            if (!File.Exists(resultFilePath))
                File.Create(resultFilePath);
            StreamWriter writer;
            try
            {
                writer = new StreamWriter(resultFilePath);
            }
            catch (IOException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There was an error creating the Results.txt file: " + e.Message);
                Console.ResetColor();
                goto END;
            }

            // make output
            Console.WriteLine("Generating output...");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int wordCount = 0;
            writer.WriteLine("word\tcomplexity");
            while (!reader.EndOfStream)
            {
                string[] words = reader.ReadLine().Split(' ');
                foreach(string s in words)
                {
                    Word w = new Word(s);
                    if (!string.IsNullOrWhiteSpace(w.ToString()))
                    {
                        float complexity = WordAnalyzer.WordComplexity(w);
                        writer.WriteLine(w.ToString() + "\t" + complexity.ToString());
                        wordCount++;
                    }                    
                }
            }
            writer.Close();
            sw.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Output successfully generated.");
            Console.WriteLine("Output took: " + sw.Elapsed.ToString() + " to analyze " + wordCount.ToString() + " words.");
            Console.ResetColor();
            try
            {
                Process.Start(new ProcessStartInfo(".\\Results.txt"));
            }
            catch(Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("There was an error starting Results.txt: " + e.Message);
                Console.ResetColor();
            }

            // quit
            END:
            Console.Write("Press any key to quit...");
            Console.ReadKey();
        }
    }
}
