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
using SQSAdmin_WpfCustomControlLibrary.Common;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmBulkUpdateHomeModelQuantity.xaml
    /// </summary>
    public partial class frmBulkUpdateHomeModelQuantity : Window
    {
        public string homename = "";
        public string brandid = "";
        public string pagidstring = "";
        public string areaname = "";
        public string areaid = "";
        public string groupname = "";
        public string groupid = "";
        public string usercode = "";
        public string productidtext = "";
        private frmQuantityManagement parent;
        private CommonResource cr;
        public frmBulkUpdateHomeModelQuantity(string pbrandid, string phomestring, string pareaid, string pareaname, string pgroupid, string pgroupname, string pusercode, int loginstateid,  frmQuantityManagement pparent)
        {
            cr = new CommonResource(loginstateid, 0);
            homename = phomestring;
            brandid = pbrandid;
            areaid = pareaid;
            areaname = pareaname;
            groupid = pgroupid;
            groupname = pgroupname;
            usercode = pusercode;
            this.parent = pparent;

            InitializeComponent();

            LoadDetails();
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        public void LoadDetails()
        {
            txthome.Text = homename.Replace(",", "\r\n");
            textBlock2_1.Text = areaname;
            textBlock3_1.Text = groupname;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            decimal qty = 0;
            try
            {
                qty = decimal.Parse(txtQty.Text);
                textBlock6.Text = "";
                cr.BulkUpdateHomeModelQuantity(brandid, areaid, groupid, homename, qty.ToString(), usercode);
                this.parent.SearchQuantiy();
                if (this.parent.allcheckbox != null)
                {
                    this.parent.allcheckbox.IsChecked = false;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Please enter a valid quantity!");
                textBlock6.Text = "Please enter a valid quantity!";
            }
        }
    }
}
