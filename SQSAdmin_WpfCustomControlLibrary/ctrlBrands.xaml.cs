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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.ServiceModel;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;
using System.Windows.Controls.Primitives;
namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for ctrlBrands.xaml
    /// </summary>
    public partial class ctrlBrands : UserControl
    {
        private SupplierBrandResource br;
        private int loginstate;
        public ctrlBrands(int pstateid)
        {
            loginstate = pstateid;
            br = new SupplierBrandResource(loginstate);
            InitializeComponent();
            this.DataContext = br;
        }
       private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));
            SupplierBrandResource.SupplierBrand s = (SupplierBrandResource.SupplierBrand)row.Item;
            bool exists = false;

            if (s.SupplierBrandName != null && s.SupplierBrandName.Trim() != "")
            {
                if (s.SupplierBrandID == 0)
                {
                    if (!SupplierBrandExists(s))
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
                    MessageBox.Show("This brand already exists.");
                }
                else
                {
                    try
                    {
                        br.SaveSupplierBrand(s);
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
                MessageBox.Show("Please enter a question text.");
            }
        }

        private bool SupplierBrandExists(SupplierBrandResource.SupplierBrand s)
        {
            return br.SupplierBrandExists(s.SupplierBrandName, s.BrandStateID);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            SupplierBrandResource.SupplierBrand s = new SupplierBrandResource.SupplierBrand();
            s.SupplierBrandID = 0;
            s.BrandStateID = loginstate;
            s.Active = true;
            br.SQSSupplierBrand.Add(s);
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            br.LoadSupplierBrand(txtBrand.Text, int.Parse(cmbState.SelectedValue.ToString()), int.Parse(cmbActive.SelectedValue.ToString()));
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

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtBrand.Text = "";
            cmbActive.SelectedValue = 1;
            cmbState.SelectedValue = loginstate;
            btnSearch_Click(null, null);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            btnSearch_Click(null, null);
        }

    }
}
