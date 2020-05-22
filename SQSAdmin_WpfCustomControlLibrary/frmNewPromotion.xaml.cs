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
    /// Interaction logic for frmNewPromotion.xaml
    /// </summary>
    public partial class frmNewPromotion : Window
    {
        private CommonResource cr;
        private SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService.SQSAdminServiceClient client;

        public frmNewPromotion(int loginstateid)
        {
            InitializeComponent();
            cr = new CommonResource(loginstateid,0);
            this.DataContext = cr;
            client = new SQSAdminWCFService.SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cr.LoadPromotionType();
            cr.LoadProductDisplayCodeForPromo();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cr.LoadBrand(int.Parse(cmbState.SelectedValue.ToString()));
            cmbBrand.SelectedIndex = 1;
        }

        private void cmbPromotionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPromotionType.SelectedValue.ToString() == "7")
            {
                txtPromotionCape.IsEnabled = false;
            }
            else
            {
                txtPromotionCape.IsEnabled = true;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            decimal cap=0;
            int regional=0;
            int active=0;
            if (FormValid())
            {
                if (txtPromotionCape.IsEnabled)
                    cap=decimal.Parse(txtPromotionCape.Text);
                if ((bool)chkRegional.IsChecked)
                    regional=1;
                if ((bool)chkActive.IsChecked)
                    active=1;

                client.SQSAdmin_Promotion_SavePromotion(
                    int.Parse(cmbState.SelectedValue.ToString()),
                    int.Parse(cmbBrand.SelectedValue.ToString()),
                    int.Parse(cmbStorey.SelectedValue.ToString()),
                    int.Parse(cmbPromotionType.SelectedValue.ToString()),
                    txtProductID.Text,
                    txtPromotionName.Text,
                    decimal.Parse(txtPromotionCost.Text),
                    cap,
                    regional,
                    active,
                    int.Parse(cmbDisplayCode.SelectedValue.ToString())
                    );

                this.Close();
            }
        }

        private bool FormValid()
        {
            bool result = true;
            decimal temp;
            if (cmbState.SelectedValue.ToString() == "0")
            {
                tbkErrorMessage.Text = "Please select a state.";
                return false;
            }
            if (cmbBrand.SelectedValue.ToString() == "0")
            {
                tbkErrorMessage.Text = "Please select a brand.";
                return false;
            }

            //if (cmbStorey.SelectedValue.ToString() == "0")
            //{
            //    tbkErrorMessage.Text = "Please select a storey.";
            //    return false;
            //}
            if (txtProductID.Text == "")
            {
                tbkErrorMessage.Text = "Please enter the master product ID.";
                return false;
            }
            else
            {
                if (!client.SQSAdmin_Promotion_CheckMasterPromotionProductExist(int.Parse(cmbState.SelectedValue.ToString()), txtProductID.Text))
                {
                    tbkErrorMessage.Text = "This product ID does NOT exist in SQS. Please change and try again.";
                    return false;
                }
                else if (client.SQSAdmin_Promotion_CheckMultiplePromotionExist(int.Parse(cmbBrand.SelectedValue.ToString()), int.Parse(cmbStorey.SelectedValue.ToString()), txtProductID.Text))
                {
                    tbkErrorMessage.Text = "Potential duplication! A promotion with same product ID and stories exists\r\n for this brand in SQS.";
                    return false;
                }

            }
            if ( cmbPromotionType.SelectedValue.ToString() != "7")
            {
                if (txtPromotionCape.Text == "")
                {
                    tbkErrorMessage.Text = "This type of promotion must have a cape value.";
                    return false;
                }
                else
                {
                    try
                    {
                        temp = decimal.Parse(txtPromotionCape.Text);
                    }
                    catch (Exception ex)
                    {
                        tbkErrorMessage.Text = "Please enter a valid cape value for this promotion.";
                        return false;
                    }
                }
            }

            if (txtPromotionName.Text == "")
            {
                tbkErrorMessage.Text = "Please enter a promotion name.";
                return false;
            }

            if (txtPromotionCost.Text=="")
            {
                tbkErrorMessage.Text = "Please enter a promotion cost. If there is no cost, enter 0.";
                return false;
            }
            else
            {
                try
                {
                    temp = decimal.Parse(txtPromotionCost.Text);
                }
                catch (Exception ex)
                {
                    tbkErrorMessage.Text = "Please enter a valid promotion cost.";
                    return false;
                }
            }



            if (result) tbkErrorMessage.Text = "";
            return result;
        }
    }
}
