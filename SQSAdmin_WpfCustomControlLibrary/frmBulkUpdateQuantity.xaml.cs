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
    /// Interaction logic for frmBulkUpdateQuantity.xaml
    /// </summary>
    public partial class frmBulkUpdateQuantity : Window
    {
        public string homename="";
        public string homeid = "";
        public string pagidstring = "";
        public string areaname = "";
        public string areaid = "";
        public string groupname = "";
        public string groupid = "";
        public string usercode = "";
        public string productidtext = "";
        private frmHomeConfiguration parent;
        private CommonResource cr;

        public frmBulkUpdateQuantity(string ppagidstring,string phomeid, string phomename, string pareaid, string pareaname, string pgroupid, string pgroupname, string pusercode, int loginstateid, string pproductidtext, frmHomeConfiguration pparent)
        {
            cr = new CommonResource(loginstateid, 0);
            pagidstring = ppagidstring;
            homeid = phomeid;
            homename = phomename;
            areaid = pareaid;
            areaname = pareaname;
            groupid = pgroupid;
            groupname = pgroupname;
            usercode = pusercode;
            productidtext=pproductidtext;
            this.parent = pparent;

            InitializeComponent();

            LoadDetails();
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;

        }
        public void LoadDetails()
        {
            textBlock1_1.Text = homename;
            textBlock2_1.Text = areaname;
            textBlock3_1.Text = groupname;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            decimal qty = 0;
            try
            {
                qty = decimal.Parse(txtQty.Text);
                cr.BulkUpdateQuantity(homeid, pagidstring, qty.ToString(), usercode);
                this.parent.SearchExistingProducts();
                if (this.parent.allcheckbox != null)
                {
                    this.parent.allcheckbox.IsChecked = false;
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter a valid quantity!");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
    }
}
