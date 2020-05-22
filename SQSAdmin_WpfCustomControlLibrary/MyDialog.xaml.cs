using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for MyDialog.xaml
    /// </summary>
    public partial class MyDialog : Window
    {
        private string _response;
        public MyDialog(string messageText = "")
        {
            InitializeComponent();
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
            if (!string.IsNullOrWhiteSpace(messageText))
            {
                textBlockMessage.Text = messageText;
            }
        }
        public string ResponseText
        {
            get { return _response; }
            set { _response = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ResponseText = "Y";
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.ResponseText = "N";
            this.Close();
        }


    }
}
