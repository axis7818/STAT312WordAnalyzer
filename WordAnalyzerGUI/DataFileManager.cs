using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using STAT312WordAnalyzer;

//TODO: make the sample file save between sessions

namespace WordAnalyzerGUI
{
    public static class DataFileManager
    {
        public const string fileName = "WordResultData.txt";

        private static readonly string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), fileName);

        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(List<Word>));

        public static void CopyFileToDesktop()
        {
            //TODO: implement
        }

        public static void SaveFile(List<Word> words)
        {
            if (words == null || words.Any(w => w == null))
                throw new ArgumentNullException("words or a Word in words is equal to null");

            using(StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, words);
            }
        }

        public static List<Word> ReadFile()
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath + " was not found.");

            using (StreamReader reader = new StreamReader(filePath))
            {
                return serializer.Deserialize(reader) as List<Word>;
            }
        }
    }
}
