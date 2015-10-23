using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.IO;
using System.Collections.Specialized;

namespace WordAnalyzerGUI
{
    public partial class MainWindow : Window
    {
        private WordAnalyzerSettings Settings;

        private static readonly Regex Tokenizer = new Regex("\\S+");

        private static readonly Regex Alphafier = new Regex("[A-Za-z]+");
        
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                Settings = WordAnalyzerSettings.ReadFile("WordAnalyzerSettings.xml");
            }
            catch (FileNotFoundException)
            {
                Settings = WordAnalyzerSettings.DefaultSettings;
                WordAnalyzerSettings.WriteFile(Settings);
            }
        }

        private void BTN_GetSample_Click(object sender, RoutedEventArgs e)
        {
            TB_RandomSample.Text = "";
            List<string> words = Tokenize(TB_SourceText.Text);
            try
            {
                foreach(string word in GetSample(words, Settings.SampleSize))
                {
                    TB_RandomSample.Text += word + "\n";
                }
            }    
            catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }   
        }

        private void MI_Options_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Not yet implemented!", "Not Implemented", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void MI_OpenResultsFile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Not yet implemented!", "Not Implemented", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private static List<string> Tokenize(string input)
        {
            List<string> result = new List<string>();

            foreach (Match m1 in Tokenizer.Matches(input))
            {
                string token = "";

                foreach(Match m2 in Alphafier.Matches(m1.Value))
                {
                    token += m2.Value;  
                }

                result.Add(token);
            }

            return result;
        }

        private static List<string> GetSample(List<string> source, int size)
        {
            if (size > source.Count)
                throw new ArgumentException("Sample size must be larger than the source count.");

            List<string> input = new List<string>(source);
            List<string> result = new List<string>();
            Random rand = new Random(DateTime.Now.Millisecond);

            for(int i = 0; i < size; i++)
            {
                int index = rand.Next() % input.Count;
                result.Add(input[index]);
                input.RemoveAt(index);
            }

            return result;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // save the settings on exit
            if(Settings != null)
                WordAnalyzerSettings.WriteFile(Settings);
        }
    }
}
