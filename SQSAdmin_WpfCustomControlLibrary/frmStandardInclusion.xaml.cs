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
    /// Interaction logic for frmStandardInclusion.xaml
    /// </summary>
    public partial class frmStandardInclusion : Window
    {
        public int loginstateid = 0;
        private StandardInclusionResource sr;
        private string SelectedProductID = "";
        public decimal SelectedQty = 0;
        public bool SelectedActive = false;

        public frmStandardInclusion(int pstateid)
        {
            loginstateid = pstateid;
            sr = new StandardInclusionResource(loginstateid);
            InitializeComponent();
            this.DataContext = sr;
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sr.LoadRegionGroupFromState(loginstateid);
            sr.LoadAllStandardInclusions(0, 0, 0, txtproductid.Text, txtproductname.Text);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            frmStandardInclusionPAGConfigure sipag = new frmStandardInclusionPAGConfigure(loginstateid);
            sipag.ShowDialog();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            StandardInclusionResource.StandardInclusionPAG pag = ((FrameworkElement)sender).DataContext as StandardInclusionResource.StandardInclusionPAG;

            sr.RemoveStandardInclusion(pag);
            btnSearch_Click(null, null);
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
                btnSearch_Click(null, null);

            }
            catch (System.Exception)
            {
            }

        }


        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            sr.LoadAllStandardInclusions(int.Parse(cmbbrand.SelectedValue.ToString()), int.Parse(cmbregiongroup.SelectedValue.ToString()), 0, txtproductid.Text, txtproductname.Text);
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));
            StandardInclusionResource.StandardInclusionPAG s = (StandardInclusionResource.StandardInclusionPAG)row.Item;
            bool exists = false;
            if (s.PAGID != 0 && s.SIBrandID != 0 && s.SIRegionGroupID != 0)
            {
                try
                {
                    sr.SaveStandardInclusions(s);
                    if (row.DetailsVisibility == Visibility.Visible)
                    {
                        row.DetailsVisibility = Visibility.Collapsed;
                    }
                    btnSearch_Click(null, null);
                }
                catch (Exception ex)
                {
                }

            }
            else
            {
                if (s.PAGID == 0)
                {
                    MessageBox.Show("Please select a PAG.");
                }
                else if (s.SIRegionGroupID == 0)
                {
                    MessageBox.Show("Please select a region group.");
                }
                else if (s.SIBrandID == 0)
                {
                    MessageBox.Show("Please select a Brand.");
                }

            }

        }
        private bool StandardInclusionExists(StandardInclusionResource.StandardInclusionPAG s)
        {
            return sr.StandardInclusionExists(s.PAGID, s.SIBrandID, s.SIRegionGroupID, s.StandardInclusionID);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtproductid.Text = "";
            txtproductname.Text = "";
            cmbbrand.SelectedValue = 0;
            cmbregiongroup.SelectedValue = 0;
            btnSearch_Click(null, null);
        }

        private void chkActive2_Click(object sender, RoutedEventArgs e)
        {
            btnSave_Click(sender, e);
        }
    }
}
