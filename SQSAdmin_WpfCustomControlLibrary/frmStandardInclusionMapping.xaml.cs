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
    /// Interaction logic for frmStandardInclusionMapping.xaml
    /// </summary>
    public partial class frmStandardInclusionMapping : Window
    {
        private StandardInclusionResource sr;
        private int loginstateid;
        private string loginuser;
        public frmStandardInclusionMapping(int stateid, string usercode)
        {
            loginstateid = stateid;
            loginuser = usercode;
            sr = new StandardInclusionResource(loginstateid);
            InitializeComponent();
            this.DataContext = sr;
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnSearch_Click(null, null);
            btnSearchAvailable_Click(null, null);

            btnPromoAdd.Visibility = Visibility.Collapsed;
            textBlockEffectiveDate.Visibility = Visibility.Collapsed;
            datePickerEffectiveDate.Visibility = Visibility.Collapsed;
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtProductID.Text = "";
            txtProductname.Text = "";
        }

        private void btnSearchAvailable_Click(object sender, RoutedEventArgs e)
        {
            string brandidselected = string.Empty;
            string stdinclusionproductidselected = string.Empty;
            StandardInclusionResource.GenericProduct p = (StandardInclusionResource.GenericProduct)dataGrid1.SelectedItem;
            if (p != null)
            {
                brandidselected = p.BrandID.ToString();
                stdinclusionproductidselected = p.ProductID;
            }
            sr.GetAvailableUpgradeOptionProducts(txtAvailableProductID.Text, txtAvailableProductName.Text, brandidselected, loginstateid, stdinclusionproductidselected);
        }

        private void btnResetAvailable_Click(object sender, RoutedEventArgs e)
        {
            txtAvailableProductID.Text = "";
            txtAvailableProductName.Text = "";
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            sr.GetStandardInclusion(txtProductID.Text, txtProductname.Text, loginstateid, getBrandIDsSelected());
        }

        private string getBrandIDsSelected()
        {
            string return_value = string.Empty;

            List<string> brands = new List<string>();
            foreach (KeyValuePair<string, object> brand in cmbBrand.SelectedItems)
            {
                brands.Add(brand.Value.ToString());
            }

            return string.Join(",", brands);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StandardInclusionResource.GenericProduct p = (StandardInclusionResource.GenericProduct)dataGrid1.SelectedItem;
            if (p != null)
            {
                tabItemUpgrade.Header = "Upgrade Option for " + p.ProductID;
                tabItemPromo.Header = "Promotion Product for " + p.ProductID;

                // The check is required to prevent cursor flickering
                if (Mouse.OverrideCursor != Cursors.Wait)
                    Mouse.OverrideCursor = Cursors.Wait;

                sr.GetUpgradeForStandardInclusion(p.ProductID, getBrandIDsSelected());

                btnSearchAvailable_Click(sender, e);

                Mouse.OverrideCursor = null;

                btnPromoAdd.Visibility = Visibility.Visible;
            }
            else
            {
                tabItemUpgrade.Header = "Upgrade Options";

                btnPromoAdd.Visibility = Visibility.Collapsed;
                textBlockEffectiveDate.Visibility = Visibility.Collapsed;
                datePickerEffectiveDate.Visibility = Visibility.Collapsed;
            }
        }

        private void cmbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StandardInclusionResource.GenericProduct p = (StandardInclusionResource.GenericProduct)dataGrid1.SelectedItem;
            if (p != null)
            {
                tabItemUpgrade.Header = "Upgrade Option for " + p.ProductID;
                tabItemPromo.Header = "Promotion Product for " + p.ProductID;
                sr.GetUpgradeForStandardInclusion(p.ProductID, getBrandIDsSelected());
            }
        }

        private void btnSaveUpgradesPromotions_Click(object sender, RoutedEventArgs e)
        {
            if (tabItemUpgrade.IsSelected)
            { 
                saveSelectedProducts(false);
            }
            else if (tabItemPromo.IsSelected)
            { 
                if (!datePickerEffectiveDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select an effective date.");
                    datePickerEffectiveDate.Focus();
                }
                else
                {
                    saveSelectedProducts(true, datePickerEffectiveDate.SelectedDate);
                }
            }
        }

        private void saveSelectedProducts(bool promotion, DateTime? effectivedate = null)
        {
            try
            {
                // The check is required to prevent cursor flickering
                if (Mouse.OverrideCursor != Cursors.Wait)
                    Mouse.OverrideCursor = Cursors.Wait;
                string selectedproductids = string.Empty;
                StandardInclusionResource.GenericProduct p = (StandardInclusionResource.GenericProduct)dataGrid1.SelectedItem;
                if (p != null)
                {
                    var itemsSelectedCount = 0;
                    var itemsSource = dataGrid3.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {

                        foreach (StandardInclusionResource.GenericProduct item in itemsSource)
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
                                    if (promotion && !validateEffectiveDate(item.ProductID))
                                    {
                                        selectedproductids = string.Empty;
                                        break;
                                    }
                                    if (selectedproductids.Length == 0)
                                    {
                                        selectedproductids = tx.Text;
                                    }
                                    else
                                    {
                                        selectedproductids = selectedproductids + "," + tx.Text;
                                    }
                                    itemsSelectedCount += 1;
                                }
                            }
                        }
                        if (!string.IsNullOrWhiteSpace(selectedproductids) && (itemsSelectedCount <= 20 || MessageBox.Show("You have selected " + itemsSelectedCount.ToString() + " products" + Environment.NewLine + "Would you like to proceed?", "Confirm Add Products", MessageBoxButton.OKCancel)==MessageBoxResult.OK))
                        {
                            string brandids = getBrandIDsSelected();
                            if (string.IsNullOrWhiteSpace(brandids))
                            {
                                MessageBox.Show("Please select one or more brands to add products.");
                            }
                            else
                            {
                                SaveData(p.ProductID, selectedproductids, brandids, loginuser, promotion, effectivedate ?? (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue);
                                MessageBox.Show(selectedproductids.Split(',').Length.ToString() + " Products added.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a standard inclusion product first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        private bool validateEffectiveDate(string productID)
        {
            bool return_value = true;
            DateTime effectiveDateCurrent = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            DateTime effectiveDatenew = datePickerEffectiveDate.SelectedDate ?? DateTime.Now;

            var itemsSource = dataGridPromotionProducts.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (StandardInclusionResource.GenericProduct item in itemsSource)
                {
                    if (item.Active && item.ProductID == productID && item.EffectiveDate >= effectiveDatenew)
                    {
                        MessageBox.Show("Effective date selected is invalid for this product : " + item.ProductName + " since it is conflicting with another promotion");
                        return_value = false;
                        break;
                    }
                }
            }

            return return_value;
        }

        private void SaveData(string standardinclusionproductid, string upgradeoptionproductids, string brandids, string usercode, bool promotion, DateTime effectivedate)
        {
            sr.SaveValidationRule(standardinclusionproductid, upgradeoptionproductids, brandids, usercode, promotion, effectivedate);
            sr.GetUpgradeForStandardInclusion(standardinclusionproductid, brandids);
            btnSearchAvailable_Click(null, null);
        }

        //private void btnDeletePromotions_Click(object sender, RoutedEventArgs e)
        //{
        //    DeleteSelectedProduct(sender, true);
        //}

        private void btnDeleteUpgrades_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedProduct(sender, false);
        }

        private void DeleteSelectedProduct(object sender, bool promotion)
        {
            MyDialog dialog = new MyDialog();
            if (dialog.ShowDialog() == false)
            {
                if (dialog.ResponseText == "Y")
                {
                    StandardInclusionResource.GenericProduct p = ((FrameworkElement)sender).DataContext as StandardInclusionResource.GenericProduct;
                    sr.RemoveValidationRule(p, promotion);
                }
            }
        }

        private void chkActive_Click(object sender, RoutedEventArgs e)
        {
            StandardInclusionResource.GenericProduct p = ((FrameworkElement)sender).DataContext as StandardInclusionResource.GenericProduct;
            MyDialog dialog = new MyDialog("Do you want to " + (p.Active ? "enable" : "disable") + " this promotion?");
            if (dialog.ShowDialog() == false)
            {
                if (dialog.ResponseText == "Y")
                {
                    sr.RemoveValidationRule(p, true, true, p.Active);
                }
                else
                {
                    // revert to previous state
                    p.Active = !p.Active;
                    ((CheckBox)sender).IsChecked = p.Active;
                }
            }
        }

        private void tabUpgradePromo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            showUpgradePromoActions();
        }

        private void showUpgradePromoActions()
        {
            textBlockEffectiveDate.Visibility = Visibility.Collapsed;
            datePickerEffectiveDate.Visibility = Visibility.Collapsed;
            if (tabItemPromo.IsSelected)
            {
                textBlockEffectiveDate.Visibility = Visibility.Visible;
                datePickerEffectiveDate.Visibility = Visibility.Visible;
            }
        }

        private void buttonSelectAll_Click(object sender, RoutedEventArgs e)
        {
            availableProductsSelectAll(true);
        }

        private void buttonUnSelectAll_Click(object sender, RoutedEventArgs e)
        {
            availableProductsSelectAll(false);
        }

        private void availableProductsSelectAll(bool selectall)
        {
            try
            {
                // The check is required to prevent cursor flickering
                if (Mouse.OverrideCursor != Cursors.Wait)
                    Mouse.OverrideCursor = Cursors.Wait;
                string selectedproductids = string.Empty;
                StandardInclusionResource.GenericProduct p = (StandardInclusionResource.GenericProduct)dataGrid1.SelectedItem;
                if (p != null)
                {

                    var itemsSource = dataGrid3.ItemsSource as IEnumerable;
                    if (itemsSource != null)
                    {

                        foreach (StandardInclusionResource.GenericProduct item in itemsSource)
                        {
                            var row = dataGrid3.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;

                            ContentPresenter contentPresenter = dataGrid3.Columns[0].GetCellContent(item) as ContentPresenter;
                            if (contentPresenter != null)
                            {
                                DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                                CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                                TextBlock tx = dataGrid3.Columns[1].GetCellContent(row) as TextBlock;
                                if (chk != null)
                                {
                                    chk.IsChecked = selectall;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a standard inclusion product first.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

    }
}
