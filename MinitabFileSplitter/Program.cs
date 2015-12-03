using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STAT312WordAnalyzer;

namespace MinitabFileSplitter
{
    class Program
    {
        private static readonly string TopicResultsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "TopicResults.txt");
        private static readonly string DateResultsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "DateResults.txt");
        private const string Wikipedia = "Wikipedia";
        private const string Scholarly_Article = "Scholarly Article";
        private const string NYT_Article = "NYT Article";
        private const string Novel = "Novel";

        static void Main(string[] args)
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(args[0]);
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occurred opening the file: " + e.Message);
                return;
            }
            reader.ReadLine();

            StreamWriter topicWriter = null, dateWriter = null;
            topicWriter = new StreamWriter(TopicResultsPath);
            dateWriter = new StreamWriter(DateResultsPath);
            topicWriter.WriteLine(DataFileManager.minitabFileHeader);
            dateWriter.WriteLine(DataFileManager.minitabFileHeader);

            Console.WriteLine("Going through file...");

            Word word;
            while (!reader.EndOfStream)
            {
                word = DataFileManager.MinitabFileLine(reader.ReadLine());
                if(word.Source.Equals(Wikipedia) || word.Source.Equals(Scholarly_Article))
                {
                    topicWriter.WriteLine(DataFileManager.MinitabFileLine(word));
                }
                else if(word.Source.Equals(NYT_Article) || word.Source.Equals(Novel))
                {
                    dateWriter.WriteLine(DataFileManager.MinitabFileLine(word));
                }
            }

            reader.Close();
            topicWriter.Close();
            dateWriter.Close();
            Console.WriteLine("Done!");
        }
    }
}
