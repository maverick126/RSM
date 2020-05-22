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
using System.Windows.Shapes;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmNewBasePriceHoldingDays.xaml
    /// </summary>
    public partial class frmNewBasePriceHoldingDays : Window
    {
        private SQSAdminServiceClient client;
        private int stateidselected;
        private CommonResource cr;
        private string usercode;
        public frmNewBasePriceHoldingDays(int stateid, string pusercode)
        {
            stateidselected = stateid;
            usercode = pusercode;
            cr = new CommonResource(stateid, 0);
            InitializeComponent();
            this.DataContext = cr;
             this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string pregionids = "0";
            string pbrandids="0";
            string active="1";
            string depositamount = "0";
            int daysfrom;
            int daysto;
            DateTime effectivedate ;

            try
            {
                effectivedate = DateTime.Parse(dteEffectiveDate.SelectedDate.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter a valid effective date.");
                return;
            }

            try
            {
                daysfrom = int.Parse(txtDaysFrom.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter a valid number for day from.");
                return;
            }

            try
            {
                daysto = int.Parse(txtDaysTo.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter a valid number for day to.");
                return;
            }

            if ((bool)chkActive.IsChecked)
                active = "1";
            else
                active = "0";

            if (cmbRegion.SelectedItems != null)
            {
                pregionids = cr.getIDsSelected(cmbRegion.SelectedItems);
            }

            if (cmbBrand.SelectedItems != null)
            {
                pbrandids = cr.getIDsSelected(cmbBrand.SelectedItems);
            }
            depositamount = txtDepositAmount.Text;
            cr.NewBasePriceHoldingDays(stateidselected, pregionids, pbrandids,daysfrom.ToString(), daysto.ToString(),effectivedate,active,depositamount,usercode, txtCMAPercent.Text, txtSurchargePercent.Text, txtRegionalSurchargeSSPercent.Text, txtRegionalSurchargeSDPercent.Text, txtBTPSingleStoryDiscount.Text, txtBTPDoubleStoryDiscount.Text, textBoxBTPSingleStoryCostSiteOther.Text, textBoxBTPDoubleStoryCostSiteOther.Text);
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
