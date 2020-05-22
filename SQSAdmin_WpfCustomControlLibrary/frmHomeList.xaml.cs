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
    /// Interaction logic for frmHomeList.xaml
    /// </summary>
    public partial class frmHomeList : Window
    {
        private SQSAdminServiceClient client;
        private int loginstateid;
        private CommonResource cr;
        private string usercode;
        public frmHomeList(int stateid, string pusercode)
        {
            loginstateid = stateid;
            usercode = pusercode;

            cr = new CommonResource(stateid, 0);
            InitializeComponent();
            this.DataContext = cr;
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void bthSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchHome();
        }

        private void cmbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //LoadBrandDropDown();
            int pstateid;
            if (cmbState.SelectedValue != null)
            {
                pstateid = int.Parse(cmbState.SelectedValue.ToString());
            }
            else
            {
                pstateid = loginstateid;
            }
            cr.LoadBrand(pstateid);
            cmbBrand.SelectedIndex = 0;
        }

        private void SearchHome()
        {
            int stateid = int.Parse(cmbState.SelectedValue.ToString());
            int brandid = int.Parse(cmbBrand.SelectedValue.ToString());
            int active = 1;

            if (cmbStatus.Text.ToUpper() == "ALL")
            {
                active = 2;
            }
            else if (cmbStatus.Text.ToUpper() == "ACTIVE")
            {
                active = 1;
            }
            else
            {
                active = 0;
            }

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            cr.LoadHomes(stateid, brandid, txtHomeName.Text, active);
            client.Close();

        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            CommonResource.Home home = ((FrameworkElement)sender).DataContext as CommonResource.Home;
            frmHomeConfiguration win2 = new frmHomeConfiguration(home.HomeID, home.HomeName, loginstateid, usercode);
            win2.ShowDialog();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SearchHome();
        }
    }
}
