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
    /// Interaction logic for ctrlSupplier.xaml
    /// </summary>
    public partial class ctrlSupplier : UserControl
    {
        private ManagementResource mr;
        private int loginstate;
        public ctrlSupplier(int pstateid)
        {
            loginstate = pstateid;
            mr = new ManagementResource(loginstate);
            InitializeComponent();
            this.DataContext = mr;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            mr.LoadSuppliers(int.Parse(cmbState.SelectedValue.ToString()), txtSupplierName.Text, int.Parse(cmbActive.SelectedValue.ToString()));
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            mr.LoadSuppliers(int.Parse(cmbState.SelectedValue.ToString()), txtSupplierName.Text, int.Parse(cmbActive.SelectedValue.ToString()));
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ManagementResource.Supplier s = new ManagementResource.Supplier();
            s.StateID = 1;
            s.SupplierID = 0;
            mr.StudioMSupplier.Add(s);
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Windows.Controls.DataGridRow row = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));
            ManagementResource.Supplier s = (ManagementResource.Supplier)row.Item;
            bool exists=false;

            if (s.SupplierName!=null && s.SupplierName.Trim() != "")
            {
                if (s.SupplierID == 0)
                {
                    if (!SupplierExists(s))
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
                    MessageBox.Show("This supplier already exists.");
                }
                else
                {
                    try
                    {
                        mr.SaveSupplier(s);
                        if (row.DetailsVisibility == Visibility.Visible)
                        {
                            row.DetailsVisibility = Visibility.Collapsed;
                        }
                        btnSearch_Click(null,null);
                    }
                    catch (Exception ex)
                    {
                    }

                }
            }
            else
            {
                MessageBox.Show("Please enter a supplier name.");
            }
        }
        private bool SupplierExists(ManagementResource.Supplier s)
        {
            return mr.SupplierExists(s.StateID, s.SupplierName);
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

        private void btnNextpage_Click(object sender, RoutedEventArgs e)
        {
            mr.StudioMSupplier2.MoveToNextPage();
        }

        private void btnPrevpage_Click(object sender, RoutedEventArgs e)
        {
            mr.StudioMSupplier2.MoveToPreviousPage();
        }
    }
}
