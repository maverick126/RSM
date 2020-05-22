using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;

namespace SQSAdmin_WpfCustomControlLibrary
{

    public partial class frmBasePriceHoldingDaysManagement : Window
    {
        private SQSAdminServiceClient client;
        private int loginstateid;
        private CommonResource cr;
        private string usercode;
        public frmBasePriceHoldingDaysManagement(int stateid, string pusercode)
        {
            loginstateid = stateid;
            usercode = pusercode;
            cr = new CommonResource(stateid, 0);
            InitializeComponent();
            this.DataContext = cr;
            dteEffectiveDate.SelectedDate = DateTime.Now;
        }

        private void cmbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int pstateid;
            if (cmbState.SelectedValue != null)
            {
                pstateid = int.Parse(cmbState.SelectedValue.ToString());
            }
            else
            {
                pstateid = loginstateid;
            }
            cr.LoadRegion(pstateid);
            cr.LoadBrand(pstateid);
            cr.LoadHomesPlans(pstateid, null, "", 1); // return all homes in the state
            cmbBrand.Text = string.Empty;
            cmbRegion.Text = string.Empty;

        }

        private void bthSearch_Click(object sender, RoutedEventArgs e)
        {
            string pstateid = "0";
            string pregionids = "0";
            string pbrandids = "0";
            string active = "2";

            if (cmbActive.Text.ToUpper() == "ACTIVE")
                active = "1";
            else if (cmbActive.Text.ToUpper() == "INACTIVE")
                active = "0";

            DateTime effectivedate = dteEffectiveDate.SelectedDate ?? DateTime.Now;
            if (cmbState.SelectedValue != null)
            {
                pstateid = cmbState.SelectedValue.ToString();
            }
            else
            {
                pstateid = loginstateid.ToString();
            }
            if (cmbRegion.SelectedItems != null)
            {
                pregionids = cr.getIDsSelected(cmbRegion.SelectedItems);
            }

            if (cmbBrand.SelectedItems != null)
            {
                pbrandids = cr.getIDsSelected(cmbBrand.SelectedItems);
            }
            cr.LoadBasePriceHoldingDays(pstateid, pregionids, pbrandids, effectivedate, active);
        }
        //private void checkBoxActive_Click(object sender, RoutedEventArgs e)
        //{
        //    CommonResource.BasePriceHoldingDays itemSelected = (CommonResource.BasePriceHoldingDays)((System.Windows.FrameworkElement)sender).DataContext;

        //    try
        //    {
        //        if (itemSelected != null)
        //        {
        //            var confirmResult = MessageBox.Show("Are you sure to mark this item " + (itemSelected.Active ? "active" : "inactive") + " ?",
        //                                                 "Confirm Change!!", MessageBoxButton.YesNo);
        //            if (confirmResult == MessageBoxResult.Yes)
        //            {
        //                // save changes
        //                //cr.;
        //            }
        //            else
        //            {
        //                itemSelected.Active = !itemSelected.Active;
        //                ((System.Windows.Controls.Primitives.ToggleButton)sender).IsChecked = itemSelected.Active;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            string pstateid = "0";
            int istateid = loginstateid;
            if (cmbState.SelectedValue != null)
            {
                pstateid = cmbState.SelectedValue.ToString();
                int.TryParse(pstateid, out istateid);
            }
            frmNewBasePriceHoldingDays win = new frmNewBasePriceHoldingDays(istateid, usercode);
            win.ShowDialog();

            bthSearch_Click(sender, e);
        }


        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            CommonResource.BasePriceHoldingDays itemSelected = (CommonResource.BasePriceHoldingDays)((System.Windows.FrameworkElement)sender).DataContext;
            string active = "1";
            try
            {
                if (itemSelected != null)
                {
                    // save changes
                    if (!itemSelected.Active)
                        active = "0";

                    cr.UpdateBasePriceHoldingDays(itemSelected.ID.ToString(), itemSelected.DaysFrom.ToString(), itemSelected.DaysTo.ToString(), itemSelected.EffectiveDate, active, itemSelected.DepositAmount, usercode, itemSelected.CMAPercent, itemSelected.SurchargePercent, itemSelected.BTPSingleStoreyPercent, itemSelected.BTPDoubleStoreyPercent, itemSelected.BTPSingleStoreyDiscount, itemSelected.BTPDoubleStoreyDiscount, itemSelected.BTPSingleStoreySiteOtherCosts, itemSelected.BTPDoubleStoreySiteOtherCosts);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
