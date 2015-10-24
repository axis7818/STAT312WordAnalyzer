using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace WordAnalyzerGUI
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private static readonly Regex Alphafier = new Regex("[A-Za-z]+");
        private static readonly Regex Numberfier = new Regex("[\\d]+");
        private static readonly Regex Tokenizer = new Regex("\\S+");
        private WordAnalyzerSettings _settings;

        public MainWindow()
        {
            try
            {
                Settings = WordAnalyzerSettings.ReadFile("WordAnalyzerSettings.xml");
            }
            catch (FileNotFoundException)
            {
                Settings = WordAnalyzerSettings.DefaultSettings;
                WordAnalyzerSettings.WriteFile(Settings);
            }
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public int SampleSize
        {
            get
            {
                return Settings.SampleSize;
            }
            private set
            {
                Settings.SampleSize = value;
                OnPropertyChanged("SampleSize");
            }
        }

        public WordAnalyzerSettings Settings
        {
            get
            {
                return _settings;
            }
            private set
            {
                _settings = value;
                OnPropertyChanged("Settings");
            }
        }

        private static List<string> GetSample(List<string> source, int size)
        {
            if (size > source.Count)
                throw new ArgumentException("Sample size (" + size.ToString() + ") must be smaller than the source count.");

            List<string> input = new List<string>(source);
            List<string> result = new List<string>();
            Random rand = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < size; i++)
            {
                int index = rand.Next() % input.Count;
                result.Add(input[index]);
                input.RemoveAt(index);
            }

            return result;
        }

        private static List<string> Tokenize(string input)
        {
            List<string> result = new List<string>();

            foreach (Match m1 in Tokenizer.Matches(input))
            {
                string token = "";

                foreach (Match m2 in Alphafier.Matches(m1.Value))
                {
                    token += m2.Value;
                }

                if(!string.IsNullOrWhiteSpace(token))
                    result.Add(token);
            }

            return result;
        }

        private void BTN_GetSample_Click(object sender, RoutedEventArgs e)
        {
            TB_RandomSample.Text = "";
            List<string> words = Tokenize(TB_SourceText.Text);
            try
            {
                foreach (string word in GetSample(words, SampleSize))
                {
                    TB_RandomSample.Text += word + "\n";
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                if (propertyName.Equals("Settings"))
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SampleSize"));
                }
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // save the settings on exit
            if (Settings != null)
                WordAnalyzerSettings.WriteFile(Settings);
        }

        private void TB_SampleSize_LostFocus(object sender, RoutedEventArgs e)
        {
            string number = "";
            foreach(Match m in Numberfier.Matches(TB_SampleSize.Text))
            {
                number += m.Value;
            }
            if (!string.IsNullOrWhiteSpace(number))
                SampleSize = int.Parse(number);
            else
                SampleSize = 0;
        }
    }
}