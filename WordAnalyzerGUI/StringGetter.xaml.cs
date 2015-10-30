using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WordAnalyzerGUI
{
    /// <summary>
    /// Interaction logic for StringGetter.xaml
    /// </summary>
    public partial class StringGetter : Window
    {
        private string result = null;

        private bool gotString = false;

        public StringGetter(string message)
        {
            InitializeComponent();

            TB_Message.Text = message;
            Keyboard.Focus(TB_Result);
        }

        public string Result
        {
            get
            {
                return result;
            }
        }

        public bool GotString
        {
            get
            {
                return gotString;
            }
        }

        private void BTN_Submit_Click(object sender, RoutedEventArgs e)
        {
            result = TB_Result.Text;
            gotString = true;
            Close();
        }
    }
}
