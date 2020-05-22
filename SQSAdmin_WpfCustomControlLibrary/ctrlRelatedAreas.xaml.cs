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
    /// Interaction logic for ctrlRelatedAreas.xaml
    /// </summary>
    public partial class ctrlRelatedAreas : Window
    {
        public int stateid = 0;
        public string loginuser = "";
        private string currentselectedproductid = "";
        private string currentselectedproductname = "";
        private string selectedareaidstring = "";
        private string removedareaidstring = "";
        private RelatedAreasSource rs;
        public ctrlRelatedAreas(int pstateid, string pusercode)
        {
            loginuser = pusercode;
            stateid = pstateid;
            rs = new RelatedAreasSource(stateid, loginuser);
            InitializeComponent();
            this.DataContext = rs;
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            rs.LoadProduct(stateid, txtproductid.Text, txtproductname.Text);
        }
        //private void btnResetAvailable_Click(object sender, RoutedEventArgs e)
        //{
        //    rs.LoadAvailableAreasForProduct(currentselectedproductid, txtAvailableAreaName.Text);
        //}

        //private void btnSearchAvailable_Click(object sender, RoutedEventArgs e)
        //{
        //    rs.LoadAvailableAreasForProduct(currentselectedproductid, txtAvailableAreaName.Text);
        //}

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            rs.LoadProduct(int.Parse(cmbState.SelectedValue.ToString()), txtproductid.Text.Trim(), txtproductname.Text.Trim());
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            cmbState.SelectedValue = stateid;
            txtproductid.Text="";
            txtproductname.Text = "";
            rs.LoadProduct(int.Parse(cmbState.SelectedValue.ToString()), txtproductid.Text.Trim(), txtproductname.Text.Trim());
        }

        //private void btnSaveSelectedArea_Click(object sender, RoutedEventArgs e)
        //{
        //    selectedareaidstring = "";
        //    if (currentselectedproductid == "")
        //    {
        //        MessageBox.Show("Please select a product from left grid.");
        //    }
        //    else
        //    {
        //        var itemsSource = dataGrid3.ItemsSource as IEnumerable;
        //        if (itemsSource != null)
        //        {
        //            foreach (var item in itemsSource)
        //            {
        //                var row = dataGrid3.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;

        //                ContentPresenter contentPresenter = dataGrid3.Columns[0].GetCellContent(item) as ContentPresenter;
        //                if (contentPresenter != null)
        //                {
        //                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
        //                    CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
        //                    TextBlock tx = dataGrid3.Columns[1].GetCellContent(row) as TextBlock;
        //                    if ((bool)chk.IsChecked)
        //                    {
        //                        if (selectedareaidstring.Length == 0)
        //                        {
        //                            selectedareaidstring = tx.Text;
        //                        }
        //                        else
        //                        {
        //                            selectedareaidstring = selectedareaidstring + "," + tx.Text;
        //                        }
        //                    }
        //                }
        //            }
        //            if (selectedareaidstring != "")
        //            {
        //                SaveData(currentselectedproductid, selectedareaidstring, loginuser);
        //            }
        //            else
        //            {
        //                MessageBox.Show("Please select at least one group.");
        //            }
        //        }
        //    }
        //}

        //private void SaveData(string productid, string selectedareaid, string usercode)
        //{
        //    rs.SaveRelatedAreaToProduct(productid, selectedareaid, usercode, callfrom);
        //    //btnSearchAvailable_Click(null, null);
        //    rs.LoadExistingAreasForProduct(productid,1);
        //}

        private void RemoveArea(string productid, string selectedareaid, string callfrom)
        {
            rs.RemoveRelatedAreaFromProduct(productid, selectedareaid, callfrom);
            //btnSearchAvailable_Click(null, null);
            rs.LoadExistingAreasForProduct(productid, 1);
        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = dataGrid2.ItemsSource as IEnumerable;
            removedareaidstring = "";
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = dataGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;

                    ContentPresenter contentPresenter = dataGrid2.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected3", contentPresenter) as CheckBox;
                        TextBlock tx = dataGrid2.Columns[1].GetCellContent(row) as TextBlock;
                        if ((bool)chk.IsChecked)
                        {
                            if (removedareaidstring.Length == 0)
                            {
                                removedareaidstring = tx.Text;
                            }
                            else
                            {
                                removedareaidstring = removedareaidstring + "," + tx.Text;
                            }
                        }
                    }
                }
                if (removedareaidstring != "")
                {
                    RemoveArea(currentselectedproductid, removedareaidstring, "RELATED");
                }
                else
                {
                    MessageBox.Show("Please select at least one area to remove.");
                }
            }
        }
        public void RefreshGrids()
        {
            rs.LoadExistingAreasForProduct(currentselectedproductid, 1);
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product p = (Product)dataGrid1.SelectedItem;
            if (p != null && p.ProductID != "")
            {
                rs.SelectedProductID = p.ProductID;
                btnRemove.IsEnabled = true;
                btnAddAreaToExcludedArea.IsEnabled = true;
                btnAddArea.IsEnabled = true;
                btnRemoveFromExcluded.IsEnabled = true;
                currentselectedproductid = p.ProductID;

                rs.LoadExistingAreasForProduct(currentselectedproductid,1);
                //btnSearchAvailable_Click(null, null);
                groupBox3.Header = "Excluded Areas for Product " + currentselectedproductid;
                gboxupgrade.Header = "Related Areas of product " + p.ProductID;
            }
            else
            {
                groupBox3.Header = "Excluded Areas";
                gboxupgrade.Header = "Configured Areas";
                btnRemove.IsEnabled = false;
                btnAddAreaToExcludedArea.IsEnabled = false;
                btnAddArea.IsEnabled = false;
                btnRemoveFromExcluded.IsEnabled = false;
                currentselectedproductid = "";
                //rs.LoadAvailableAreasForProduct(currentselectedproductid, txtAvailableAreaName.Text);
                //btnSearchAvailable_Click(null, null);
            }
            

        }

        private void btnRemoveFromExcluded_Click(object sender, RoutedEventArgs e)
        {
            var itemsSource = dataGrid3.ItemsSource as IEnumerable;
            removedareaidstring = "";
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = dataGrid3.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;

                    ContentPresenter contentPresenter = dataGrid3.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                        TextBlock tx = dataGrid3.Columns[1].GetCellContent(row) as TextBlock;
                        if ((bool)chk.IsChecked)
                        {
                            if (removedareaidstring.Length == 0)
                            {
                                removedareaidstring = tx.Text;
                            }
                            else
                            {
                                removedareaidstring = removedareaidstring + "," + tx.Text;
                            }
                        }
                    }
                }
                if (removedareaidstring != "")
                {
                    RemoveArea(currentselectedproductid, removedareaidstring, "EXCLUDED");
                }
                else
                {
                    MessageBox.Show("Please select at least one area to remove.");
                }
            }
        }

        private void btnAddArea_Click(object sender, RoutedEventArgs e)
        {
            Product p = (Product)dataGrid1.SelectedItem;
            if (p != null)
            {
                SQSAdmin_WpfCustomControlLibrary.ctrlAvailableAreas win = new SQSAdmin_WpfCustomControlLibrary.ctrlAvailableAreas(stateid, loginuser, p.ProductID, "RELATED");
                win.Owner = this;
                win.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an product!");
            }
        }

        private void btnAddAreaToExcludedArea_Click(object sender, RoutedEventArgs e)
        {
            Product p = (Product)dataGrid1.SelectedItem;
            if (p != null)
            {
                SQSAdmin_WpfCustomControlLibrary.ctrlAvailableAreas win = new SQSAdmin_WpfCustomControlLibrary.ctrlAvailableAreas(stateid, loginuser, p.ProductID, "EXCLUDED");
                win.Owner = this;
                win.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select an product!");
            }
        }

        private void Window_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            rs.LoadExistingAreasForProduct(currentselectedproductid, 1);
        }


    }
}
