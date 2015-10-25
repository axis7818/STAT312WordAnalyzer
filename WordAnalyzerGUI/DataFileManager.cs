using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using STAT312WordAnalyzer;

//TODO: make the sample file save between sessions

namespace WordAnalyzerGUI
{
    public static class DataFileManager
    {
        public const string fileName = "WordResultData.txt";

        private static readonly string localFilePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), fileName);

        private static readonly string desktopFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
        
        public static void CopyFileToDesktop(bool force = false)
        {
            if (!File.Exists(localFilePath))
                throw new FileNotFoundException("could not find the local file");

            if (force)
            {
                File.Copy(localFilePath, desktopFilePath, true);
            }
            else
            {
                if (File.Exists(desktopFilePath))
                    throw new FileOverwriteException("Desktop file already exists.");

                File.Copy(localFilePath, desktopFilePath);
            }
        }

        public static void SaveFile(List<Word> words)
        {
            if (words == null || words.Any(w => w == null))
                throw new ArgumentNullException("words or a Word in words is equal to null");

            using (StreamWriter writer = new StreamWriter(localFilePath))
            {
                //TODO: write the file in a minitab readable format
            }
        }

        public static List<Word> ReadFile()
        {
            if (!File.Exists(localFilePath))
                throw new FileNotFoundException(localFilePath + " was not found.");

            using (StreamReader reader = new StreamReader(localFilePath))
            {
                //TODO: read the file from a minitab readable format
                return null;
            }
        }
    }
}
