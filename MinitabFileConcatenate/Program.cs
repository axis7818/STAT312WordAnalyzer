using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using STAT312WordAnalyzer;

namespace MinitabFileConcatenate
{
    class Program
    {
        private static readonly string resultsLocation = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "results.txt");
        private static readonly string inputDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "input");
        
        static void Main(string[] args)
        {
            Console.WriteLine("Merging Files...");

            if (!Directory.Exists(inputDirectory))
            {
                Console.WriteLine("No input directory, creating one. Try again.");
                Directory.CreateDirectory(inputDirectory);
                return;
            }

            StreamWriter writer = new StreamWriter(resultsLocation);
            StreamReader reader;

            writer.WriteLine(DataFileManager.minitabFileHeader);

            foreach(string file in Directory.EnumerateFiles(inputDirectory))
            {
                using (reader = new StreamReader(file))
                {
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        writer.WriteLine(reader.ReadLine());
                    }
                }
            }
            writer.Close();

            Console.WriteLine("Done!");
        }
    }
}
