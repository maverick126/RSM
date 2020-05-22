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
    /// Interaction logic for frmConfigureSIToBrand.xaml
    /// </summary>
    public partial class frmConfigureSIToBrand : Window
    {
        public int loginstateid = 0;
        public string SelectedProductID = "";
        private StandardInclusionResource sr;
        public decimal SelectedQty = 0;
        public bool SelectedActive = false;
        public frmConfigureSIToBrand(string productid, int pstateid)
        {
            loginstateid = pstateid;
            SelectedProductID = productid;
            sr = new StandardInclusionResource(loginstateid);
            InitializeComponent();
            this.DataContext = sr;
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));
            StandardInclusionResource.StandardInclusionPAG s = (StandardInclusionResource.StandardInclusionPAG)row.Item;
            bool exists = false;
            if (s.PAGID != 0 && s.SIBrandID != 0 && s.SIRegionGroupID != 0)
            {
                if (s.StandardInclusionID == 0)
                {
                    if (!StandardInclusionExists(s))
                    {
                        exists = false;
                    }
                    else
                    {
                        exists = true;
                    }
                }

                if (exists)
                {
                    MessageBox.Show("This inclusion PAG for this brand and region group already exists.");
                }
                else
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //sr.LoadPAGsFromProduct(SelectedProductID);
            sr.LoadRegionGroupFromState(loginstateid);
            sr.LoadAvailableStandardInclusionPAGs(SelectedProductID, "", 0, 0, 0, 0);
            sr.LoadAllStandardInclusions(0, 0, 0, SelectedProductID,"");
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            StandardInclusionResource.StandardInclusionPAG s = new StandardInclusionResource.StandardInclusionPAG();
            s.PAGID = 0;
            s.Active = true;
            s.StandardInclusionID = 0;
            s.Quantity = 1;
            sr.SQSAllStandardInclusions.Add(s);
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
            sr.LoadAllStandardInclusions(0, 0, 0, SelectedProductID, "");
        }

        private void cmbpag_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = (ComboBox)e.OriginalSource;
            
            int pid = int.Parse(cmb.SelectedValue.ToString());
            foreach (StandardInclusionResource.ProductAreaGroup pag in sr.SQSProductAreaGroup)
            {
                if (pag.ProductAreaGroupID == pid)
                {
                    sr.SelectedPAG = pag;
                    break;
                }
            }

        }

        private void dataGrid1_RowDetailsVisibilityChanged(object sender, Microsoft.Windows.Controls.DataGridRowDetailsEventArgs e)
        {
            sr.SelectedPAG = new StandardInclusionResource.ProductAreaGroup();
        }
    }
}
