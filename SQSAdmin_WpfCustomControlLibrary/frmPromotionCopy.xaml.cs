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
    /// Interaction logic for frmPromotionCopy.xaml
    /// </summary>
    public partial class frmPromotionCopy : Window
    {
        private SQSAdminServiceClient client;
        private CommonResource cr;
        private int loginstateid, multiplepromotionid;
        public frmPromotionCopy(int pstateid, int idmultiplepromotion)
        {
            loginstateid=pstateid;
            multiplepromotionid = idmultiplepromotion;
            cr = new CommonResource(loginstateid, 0);
            InitializeComponent();
            this.DataContext = cr;
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            bool result=cr.CopyPromotionProductFromSourceToTarget(multiplepromotionid, int.Parse(cmbPromotion.SelectedValue.ToString()));
            if (result)
            {
                MessageBox.Show("Promotion products have been successfully copied.");
            }
            else
            {
                MessageBox.Show("Copy failed.");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cr.LoadSourcePromotionForCopy(multiplepromotionid);
        }
    }
}
