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
using System.Xml;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmRefreshQtyWarning.xaml
    /// </summary>
    public partial class frmRefreshQtyWarning : Window
    {
        public frmQuantityManagement parent;
        public frmRefreshQtyWarning(frmQuantityManagement pparent)
        {
            parent = pparent;
            InitializeComponent();
            //string message= "Warning: \r\nRefresh quantity will overwrite the quantity for all items configured to the certain area and group for all homes in the selected brand. The quantity is get from the oldest home configurations.\r\n \r\n";
            //message=message+"For example: In a brand, home A (five facades) has 30 as quantity cross all facades in area Structural and group Eave-first floor only. Home B(four facades) in the same area group has 29 as quantity in three facades and X facade has the quantity 40.\r\n \r\n";
            //message = message + "If there is a new item in area Structural and group Eave-first floor only has been configured to these two homes, the default quantity is 1 for these configurations.\r\n \r\n";
            //message = message + "Refresh function will use 30 to overwrite 1 as quantity for home A; use 29 to overwrite 1 as quantity for home B in 3 facades and use 40 to overwrite 1 as quantity for facade X.\r\n\r\n ";
            //message = message + "Do you want to process refresh the quantity?";

            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
            string value = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
            XmlNodeList nodeList = doc.SelectNodes("connectionStrings/QuantityRefreshWarning");
            foreach (XmlNode node in nodeList)
            {
                value = @"" + node.SelectSingleNode("Text").InnerText;
            }

            txtWarning.Text = value.Replace("<br><br>","\r\n");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //this.parent.RefreshQuantity();
            this.parent.Refresh();
            //this.parent.Search();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
