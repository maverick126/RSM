using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;
using System.ComponentModel;
using System.Xml;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for ctrlGroupList.xaml
    /// </summary>
    public partial class ctrlGroupList : UserControl
    {
        private RetailClusterSource rs;
        private int loginstateid;
        private string loginuser;
        private string configType = "";
        public ctrlGroupList(int pstateid, string pusercode)
        {
            loginstateid = pstateid;
            loginuser = pusercode;
            rs = new RetailClusterSource(loginstateid, pusercode);
            InitializeComponent();
            this.DataContext = rs;
            btnSearch_Click(null, null);
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            RetailClusterSource.Group group = (RetailClusterSource.Group)(((Button)e.OriginalSource).DataContext);
            frmConfigureSupplierQAToCluster win = new frmConfigureSupplierQAToCluster(loginstateid, group, loginuser, "GROUP");
            win.ShowDialog();

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtgroupname.Text = "";
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            rs.LoadSQSGroups(txtgroupname.Text);
        }
    }
}
