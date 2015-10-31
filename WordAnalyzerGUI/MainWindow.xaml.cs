using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using STAT312WordAnalyzer;
using System.Threading.Tasks;

namespace WordAnalyzerGUI
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private static readonly Regex Alphafier = new Regex("[A-Za-z]+");

        private static readonly Regex Numberfier = new Regex("[\\d]+");

        private static readonly Regex Tokenizer = new Regex("\\S+");

        private WordAnalyzerSettings _settings;

        private List<Word> Words = new List<Word>();

        public MainWindow()
        {
            // get the settings
            try
            {
                Settings = WordAnalyzerSettings.ReadFile("WordAnalyzerSettings.xml");
            }
            catch (FileNotFoundException)
            {
                Settings = WordAnalyzerSettings.DefaultSettings;
                WordAnalyzerSettings.WriteFile(Settings);
                SourceText = Settings.Source;
            }

            // initialize the GUI
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string SessionWords
        {
            get
            {
                string result = "";
                foreach(Word w in Words)
                {
                    result += w.ToString() + "\n";
                }
                return result;
            }
            set { }
        }

        public string SourceText
        {
            get
            {
                return Settings.Source;
            }
            private set
            {
                Settings.Source = value;
                OnPropertyChanged("SourceText");
            }
        }

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
        
        private List<string> GetSample(List<string> source, int size)
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

        private void EnableLoadingFilm(string message)
        {
            TB_LoadingMessage.Text = message;
            G_LoadingFilm.Visibility = Visibility.Visible;
        }
        
        private void DisableLoadingFilm()
        {
            G_LoadingFilm.Visibility = Visibility.Collapsed;
        }

        private void SetSampleLoadingFilmVisibility(bool visible)
        {
            G_SampleLoadingFilm.Visibility = visible ? Visibility.Visible : Visibility.Collapsed;
        }

        public static List<string> Tokenize(string input)
        {
            List<string> result = new List<string>();

            foreach (Match m1 in Tokenizer.Matches(input))
            {
                string token = "";

                foreach (Match m2 in Alphafier.Matches(m1.Value))
                {
                    token += m2.Value;
                }

                if (!string.IsNullOrWhiteSpace(token))
                    result.Add(token);
            }

            return result;
        }

        private void BTN_ClearSessionData_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult check = MessageBox.Show("Are you sure you want to delete this session's data?", "Delete Data?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (check == MessageBoxResult.Yes)
            {
                Words.Clear();
                TB_SourceText.Text = TB_RandomSample.Text = "";
                SourceText = "";
                DP_Date.SelectedDate = null;
                CB_UseDate.IsChecked = false;
                DataFileManager.DeleteWordFile();
                DataFileManager.DeleteLocalSourceTextFile();
                OnPropertyChanged("SessionWords");
            }
        }

        private void BTN_ExportToDesktop_Click(object sender, RoutedEventArgs e)
        {
            // create the local file
            DataFileManager.WriteWordFile(Words/*.Distinct().ToList()*/);   // uncomment to filter for unique words

            StringGetter stringGetter = new StringGetter("Enter a name for the file.");
            stringGetter.Owner = this;
            stringGetter.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            stringGetter.ShowDialog();
            string name = stringGetter.Result;
            Regex txtChecker = new Regex("^.*\\.txt$");
            if (stringGetter.GotString && (string.IsNullOrWhiteSpace(name) || txtChecker.IsMatch(name)))
            {
                MessageBox.Show("Invalid name.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (stringGetter.GotString == false)
                return;

            try
            {
                DataFileManager.CopyFileToDesktop(name);
            }
            catch (FileOverwriteException)
            {
                MessageBoxResult check = MessageBox.Show("The file [" + name + ".txt] already exists on the desktop. \nDo you want to overwrite it?", "File Overwrite", MessageBoxButton.YesNo, MessageBoxImage.None);
                if(check == MessageBoxResult.Yes)
                {
                    DataFileManager.CopyFileToDesktop(name, true);
                }
                else
                {
                    MessageBox.Show("The file was not sent to the desktop.", "File Not Overwritten", MessageBoxButton.OK, MessageBoxImage.None);
                    return;
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Local file was not found.");
                return;
            }

            MessageBox.Show("The file was sent to the desktop.", "File Sent", MessageBoxButton.OK);
        }

        private async void BTN_GetSample_Click(object sender, RoutedEventArgs e)
        {
            SetSampleLoadingFilmVisibility(true);
            string result = "";

            List<string> words = null;
            Dispatcher.Invoke(() =>
            {
                words = Tokenize(TB_SourceText.Text);
            });
            

            await Task.Run(() =>
            {
                try
                {
                    foreach (string word in GetSample(words, SampleSize))
                    {
                        result += word + "\n";
                    }
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });

            TB_RandomSample.Text = result;
            SetSampleLoadingFilmVisibility(false);
        }

        private async void BTN_SaveSample_Click(object sender, RoutedEventArgs e)
        {
            EnableLoadingFilm("Processing . . .");
            string sampleText = TB_RandomSample.Text;

            // get source and add the source to the Sources list if it doesn't exist
            string source = SourceText;
            if (string.IsNullOrWhiteSpace(source))
            {
                MessageBox.Show("Please enter a source.", "No Source", MessageBoxButton.OK);
                DisableLoadingFilm();
                return;
            }

            // get the date (if it exists)
            DateTime? date = null;
            if (DP_Date.IsEnabled)
            {
                date = DP_Date.DisplayDate;
            }

            await Task.Run(() => 
            {
                // get the sample of words
                List<string> sample = Tokenize(sampleText);
                if (sample.Count <= 0)
                {
                    MessageBox.Show("No Data To Save", "No Data", MessageBoxButton.OK);
                    DisableLoadingFilm();
                    return;
                }

                // save the data
                foreach(string word in sample)
                {
                    Word w = new Word(word, source, date);
                    Words.Add(w);
                    
                }

                // sort the data
                Words = Words.OrderBy(w => w.ToString()).ToList();

                OnPropertyChanged("SessionWords");
                //MessageBox.Show("Sample data was saved!", "Sample Saved", MessageBoxButton.OK, MessageBoxImage.None);
            });

            DisableLoadingFilm();
        }

        private static string ToTextList(List<Word> words)
        {
            string result = "";
            foreach(Word w in words)
            {
                result += w.ToString() + "\n";
            }
            result.TrimEnd('\n');
            return result;
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                if (propertyName.Equals("Settings"))
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SampleSize"));
                    PropertyChanged(this, new PropertyChangedEventArgs("SourceText"));
                }
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void TB_SampleSize_LostFocus(object sender, RoutedEventArgs e)
        {
            string number = "";
            foreach (Match m in Numberfier.Matches(TB_SampleSize.Text))
            {
                number += m.Value;
            }
            if (!string.IsNullOrWhiteSpace(number))
                SampleSize = int.Parse(number);
            else
                SampleSize = 0;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // save the settings on exit
            if (Settings != null)
                WordAnalyzerSettings.WriteFile(Settings);

            // save the Current Session data
            try
            {
                DataFileManager.WriteWordFile(Words);
            }
            catch (ArgumentNullException)
            {
                MessageBoxResult check = MessageBox.Show("Session data could not be saved. Exit anyways?", "Could not save session data.", MessageBoxButton.YesNo, MessageBoxImage.None);
                if (check == MessageBoxResult.No)
                    e.Cancel = true;
            }

            // save the current source text
            if(!string.IsNullOrWhiteSpace(TB_SourceText.Text))
                DataFileManager.WriteSourceTextFile(TB_SourceText.Text);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableLoadingFilm("Loading Previous Session Data . . .");
            string sourceText = "";

            await Task.Run(() =>
            {
                // load an existing Session file
                if (File.Exists(DataFileManager.localWordsFilePath))
                {
                    try
                    {
                        Words = DataFileManager.ReadWordFile();
                    }
                    catch (SessionFileReadException)
                    {
                        MessageBox.Show("There was an error reading the last session's file.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                if (File.Exists(DataFileManager.localSourceTextFilePath))
                {
                    sourceText = DataFileManager.ReadSourceTextFile();
                }
                OnPropertyChanged("SessionWords");
            });

            TB_SourceText.Text = sourceText;
            DisableLoadingFilm();
        }

        private void TB_Source_LostFocus(object sender, RoutedEventArgs e)
        {
            string source = TB_Source.Text;
            SourceText = source;   
        }
    }
}