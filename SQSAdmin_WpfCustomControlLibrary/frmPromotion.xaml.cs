using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class frmPromotion : Window
    {
        private SQSAdminServiceClient client;
        private int loginstateid;
        private CommonResource cr;
        public frmPromotion(int stateid)
        {
            loginstateid = stateid;
            cr = new CommonResource(stateid,0);
            InitializeComponent();
            cr.LoadProductDisplayCodeForPromo();
            this.DataContext = cr;
            //client = new SQSAdminServiceClient();
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //LoadStateDropDown();
            //loadStoreyDropdown();
            //loadStatusDropdown();
            SearchPromotion();
        }
        private void LoadStateDropDown()
        {
            //DataSet ds = client.SQSAdmin_Generic_GetState();
            //cmbState.DataContext = ds.Tables[0];
            //cmbState.DisplayMemberPath = "stateAbbreviation";
            //cmbState.SelectedValuePath = "stateid";
            //cmbState.SelectedValue = loginstateid;
        }

        private void LoadBrandDropDown()
        {
            //DataSet ds = client.SQSAdmin_Generic_GetBrandByState(int.Parse(cmbState.SelectedValue.ToString()));
            //cmbBrand.DataContext = ds.Tables[0];
            //cmbBrand.DisplayMemberPath = "BrandName";   // this name is case sensitive
            //cmbBrand.SelectedValuePath = "BrandID";
            //cmbBrand.SelectedValue = 0;

        }

        private void cmbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //LoadBrandDropDown();
            int pstateid;
            if (cmbState.SelectedValue != null)
            {
                pstateid=int.Parse(cmbState.SelectedValue.ToString());
            }
            else
            {
                pstateid = loginstateid;
            }
            cr.LoadBrand(pstateid);
            cmbBrand.SelectedIndex = 0;
        }

    
        private void bthSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchPromotion();
        }

        private void SearchPromotion()
        {
            int stateid = int.Parse(cmbState.SelectedValue.ToString());
            int brandid = int.Parse(cmbBrand.SelectedValue.ToString());
            int storey = int.Parse(cmbStorey.SelectedValue.ToString());
            int active = int.Parse(cmbStatus.SelectedValue.ToString());
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_Promotion_GetPromotionList(stateid, brandid, storey, active);
            client.Close();

            
            cr.LoadFullPromotion(ds);
            //dataGrid1.DataContext = cr.SQSFullMultiplePromotion;

        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            frmNewPromotion win = new frmNewPromotion(loginstateid);
            win.ShowDialog();
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            string multiplepromotionid=((Button)e.OriginalSource).CommandParameter.ToString();
            frmConfigurePromotionProduct win = new frmConfigurePromotionProduct(loginstateid, int.Parse(multiplepromotionid));
            win.ShowDialog();

        }

        private void dataGrid1_RowEditEnding(object sender, Microsoft.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            int forregional = 0;
            int active = 0;
            decimal promotioncost = 0;
            decimal capevalue = 0;
            int  multiplepromotionid;
            bool ok = true;
            string baseproduct = "";
            string message = "";

            CommonResource.FullMultiplePromotion dv = (CommonResource.FullMultiplePromotion)e.Row.Item;
            //DataRowView dv = (DataRowView)e.Row.Item;
            if (dv != null)
            {
                if (dv.BaseProduct != null)
                {
                    baseproduct = dv.BaseProduct;
                }

                try
                {
                    promotioncost = decimal.Parse(dv.PromotionCost.ToString());
                }
                catch (Exception ex)
                {
                    message = "Please enter a valid promotion cost.";
                    ok = false;
                }
                try
                {
                    capevalue = decimal.Parse(dv.CapeVale.ToString());
                }
                catch (Exception ex)
                {
                    message = "Please enter a valid cape value.";
                    ok = false;
                }
                multiplepromotionid = int.Parse(dv.MultiplePromotionID.ToString());
                if (dv.ForRegional)
                {
                        forregional = 1;
                }

                if (dv.Active)
                {
                        active = 1;
                }

                if (ok)
                {
                    client = new SQSAdminServiceClient();
                    client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
                    client.SQSAdmin_Promotion_UpdatePromotion(multiplepromotionid, baseproduct, promotioncost, capevalue, forregional, active, dv.DisplayCodeID, dv.PromotionName);
                    client.Close();
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            string multiplepromotionid = ((Button)e.OriginalSource).CommandParameter.ToString();
            frmPromotionCopy win = new frmPromotionCopy(  loginstateid,int.Parse(multiplepromotionid));
            win.ShowDialog();
        }

        private void txtpromoname_LostFocus(object sender, RoutedEventArgs e)
        {

            CommonResource.FullMultiplePromotion fm = ((TextBox)e.OriginalSource).DataContext as CommonResource.FullMultiplePromotion;
            cr.UpdateMultilpePromotionName(fm.MultiplePromotionID.ToString(), fm.PromotionName);
      
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            int forregional = 0;
            int active = 0;
            decimal promotioncost = 0;
            decimal capevalue = 0;
            int multiplepromotionid;
            bool ok = true;
            string baseproduct = "";
            string message = "";
           

            Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));
            CommonResource.FullMultiplePromotion dv = (CommonResource.FullMultiplePromotion)row.Item;

            if (dv != null)
            {
                if(dv.PromotionName.Trim()=="")
                {
                    message = "Please enter promotion name.";
                    ok = false;
                }
                if (dv.BaseProduct != null)
                {
                    baseproduct = dv.BaseProduct;
                }
                else
                {
                    message = "Please enter base product.";
                    ok = false;
                }

                if(dv.DisplayCodeID<1)
                {
                    message = "Please select a price display code.";
                    ok = false;
                }

                try
                {
                    promotioncost = decimal.Parse(dv.PromotionCost.ToString());
                }
                catch (Exception ex)
                {
                    message = "Please enter a valid promotion cost.";
                    ok = false;
                }
                try
                {
                    capevalue = decimal.Parse(dv.CapeVale.ToString());
                }
                catch (Exception ex)
                {
                    message = "Please enter a valid cape value.";
                    ok = false;
                }
                multiplepromotionid = int.Parse(dv.MultiplePromotionID.ToString());
                if (dv.ForRegional)
                {
                    forregional = 1;
                }

                if (dv.Active)
                {
                    active = 1;
                }

                if (ok)
                {
                    client = new SQSAdminServiceClient();
                    client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
                    client.SQSAdmin_Promotion_UpdatePromotion(multiplepromotionid, baseproduct, promotioncost, capevalue, forregional, active, dv.DisplayCodeID, dv.PromotionName);
                    client.Close();

                    SearchPromotion();
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));

                // change the details visibility
                if (row.DetailsVisibility == Visibility.Collapsed)
                {
                    row.DetailsVisibility = Visibility.Visible;
                }
                else
                {
                    row.DetailsVisibility = Visibility.Collapsed;
                }
                SearchPromotion();
            }
            catch (System.Exception)
            {
            }

        }

        private void RowDoubleClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Windows.Controls.DataGridRow row1 = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));
            if (row1.DetailsVisibility == Visibility.Visible)
            {
                row1.DetailsVisibility = Visibility.Collapsed;
            }
            else
            {
                row1.DetailsVisibility = Visibility.Visible;
            }
            
        }
    }
}
