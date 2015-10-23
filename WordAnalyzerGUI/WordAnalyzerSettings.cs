using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WordAnalyzerGUI
{
    [Serializable]
    public class WordAnalyzerSettings
    {
        [XmlAttribute()]
        public string Name;

        [XmlAttribute()]
        public int SampleSize;
        
        public WordAnalyzerSettings() { }

        public WordAnalyzerSettings(string name, int sampleSize)
        {
            Name = name;
            SampleSize = sampleSize;
        }

        [XmlIgnore()]
        public static WordAnalyzerSettings DefaultSettings
        {
            get
            {
                return new WordAnalyzerSettings("WordAnalyzerSettings", 4);
            }
        }

        [XmlIgnore()]
        private static XmlSerializer serializer = new XmlSerializer(typeof(WordAnalyzerSettings));

        public static void WriteFile(WordAnalyzerSettings settings)
        {
            // make sure that we are not writting a null object
            if (settings == null)
                throw new ArgumentNullException("settings");

            // get the file path
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), settings.Name + ".xml");

            // serialize the object to a file
            using(StreamWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, settings);
            }
        }

        public static WordAnalyzerSettings ReadFile(string fileName)
        {
            // get the file path
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), fileName);

            // check if the file path exists
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath + " could not be found.");
            
            // deserialize the file and return it
            WordAnalyzerSettings result = null;
            using (StreamReader reader = new StreamReader(filePath))
            {
                result = serializer.Deserialize(reader) as WordAnalyzerSettings;
            }
            return result;
        }
    }
}
