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
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmStudioMFunctions.xaml
    /// </summary>
    public partial class frmStudioMFunctions : Window
    {
        private int loginstate;
        private string loginuser;
        private SQSAdminServiceClient client;
        public frmStudioMFunctions(int stateid, string usercode)
        {
            loginstate=stateid;
            loginuser = usercode;
            InitializeComponent();
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = @"Studio M Admin " ;
            LoadIntroduction();
            //btnSupplier_Click(null, null);
        }

        private void btnSupplier_Click(object sender, RoutedEventArgs e)
        {
            ctrlSupplier c1 = new ctrlSupplier(loginstate);
            this.maincontainer.Children.Clear();
            this.maincontainer.Children.Add(c1);
        }

        private void btnBrand_Click(object sender, RoutedEventArgs e)
        {
            ctrlBrands b1 = new ctrlBrands(loginstate);
            this.maincontainer.Children.Clear();
            this.maincontainer.Children.Add(b1);
        }
        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            ctrlQuestion c1 = new ctrlQuestion(loginstate);
            this.maincontainer.Children.Clear();
            this.maincontainer.Children.Add(c1);
        }

        private void btnAnswer_Click(object sender, RoutedEventArgs e)
        {
            ctrlAnswer c1 = new ctrlAnswer(loginstate);
            this.maincontainer.Children.Clear();
            this.maincontainer.Children.Add(c1);
        }

        private void btnRetailCluster_Click(object sender, RoutedEventArgs e)
        {
            ctrlRetailCluster rc1 = new ctrlRetailCluster(loginstate, loginuser);
            this.maincontainer.Children.Clear();
            this.maincontainer.Children.Add(rc1);
        }


        private void btnConfigure_Click(object sender, RoutedEventArgs e)
        {
            ctrlConfigureQAToCluster config = new ctrlConfigureQAToCluster(loginstate, loginuser, "RETAILCLUSTER");
            this.maincontainer.Children.Clear();
            this.maincontainer.Children.Add(config);
        }
        private void LoadIntroduction()
        {
            ctrlIntroduction c1 = new ctrlIntroduction(loginstate);
            this.maincontainer.Children.Clear();
            this.maincontainer.Children.Add(c1);
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            frmSearchProduct c1 = new frmSearchProduct(loginstate);
            c1.ShowDialog();
            string productid = c1.ProductID;
            string proudctname = c1.ProductName;
            bool isstudiomproduct = c1.isStudioMProduct;
            if (isstudiomproduct)
            {
                if (productid != null && productid.Trim() != "")
                {
                    ctrlStudioMAttributes c2 = new ctrlStudioMAttributes(productid, proudctname, loginstate, true);
                    this.maincontainer.Children.Clear();
                    this.maincontainer.Children.Add(c2);
                }
                else
                {
                    //MessageBox.Show("Please select a product to view StudioM attributes.");
                }
            }
            else
            {
                MessageBox.Show("This product is not a StudioM product.");
            }
        }

        private void btnConfigGroup_Click(object sender, RoutedEventArgs e)
        {
            ctrlGroupList g1 = new ctrlGroupList(loginstate, loginuser);
            this.maincontainer.Children.Clear();
            this.maincontainer.Children.Add(g1);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);


            client.SQSAdmin_StudioM_UploadImageToSharepointImageLibrary("SG-BRI-CA1-140", 0);
        }

        private void btnBulkconfiguration_Click(object sender, RoutedEventArgs e)
        {
            frmStudioMBulkConfigure c1 = new frmStudioMBulkConfigure(loginstate, loginuser);
            c1.ShowDialog();
        }


    }
}
