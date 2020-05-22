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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class frmStudioM : Window
    {
        public frmStudioM(string productid, string productname, int stateid, bool isstudiom)
        {

            InitializeComponent();
            ctrlStudioMAttributes c1 = new ctrlStudioMAttributes(productid, productname, stateid, isstudiom);
            this.maincontainer.Children.Clear();
            this.maincontainer.Children.Add(c1);
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }
    }
}
