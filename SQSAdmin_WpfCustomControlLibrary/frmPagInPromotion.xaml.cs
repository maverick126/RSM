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
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmPagInPromotion.xaml
    /// </summary>
    public partial class frmPagInPromotion : Window
    {
        public int userstate;
        private CommonResource cr;
        public frmPagInPromotion(int pstate)
        {
            userstate = pstate;
            cr = new CommonResource(userstate, 0);
            InitializeComponent();
            this.DataContext = cr;
            txtrecordcount.Text = "0";
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int pagid=0;
            string productid=txtproductid.Text;
            if (txtpagid.Text!="")
            {
                try 
                {
                    pagid=int.Parse(txtpagid.Text);
                }
                catch (Exception ex)
                {
                    pagid=0;
                }
            }
            cr.LoadPromotionProduct(pagid, productid);
            txtrecordcount.Text = cr.SQSPromotionProduct.Count.ToString();
        }
    }
}
