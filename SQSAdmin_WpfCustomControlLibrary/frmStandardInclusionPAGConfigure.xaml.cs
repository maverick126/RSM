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
using System.Globalization;
using System.Threading;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmStandardInclusionPAGConfigure.xaml
    /// </summary>
    public partial class frmStandardInclusionPAGConfigure : Window
    {
        public int loginstateid = 0;
        private StandardInclusionResource sr;
        public decimal SelectedQty = 0;
        public bool SelectedActive = false;
        public frmStandardInclusionPAGConfigure(int pstateid)
        {
            loginstateid = pstateid;
            sr = new StandardInclusionResource(loginstateid);
            InitializeComponent();
            this.DataContext = sr;
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CultureInfo ci = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            datePickerEffectiveDate.SelectedDate = DateTime.Today;

            sr.LoadArea();
            sr.LoadGroup();
            sr.LoadRegionGroupFromState(loginstateid, true);
            btnSearchAvailable_Click(null, null);
        }

        private void btnSearchAvailable_Click(object sender, RoutedEventArgs e)
        {
            sr.LoadAvailableStandardInclusionPAGsFromMasterList(txtAvailableProductID.Text, txtAvailableProductName.Text, int.Parse(cmbArea.SelectedValue.ToString()), int.Parse(cmbGroup.SelectedValue.ToString()), loginstateid);
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

        private void btnResetAvailable_Click(object sender, RoutedEventArgs e)
        {
            txtAvailableProductID.Text = "";
            txtAvailableProductName.Text = "";
            cmbArea.SelectedIndex = 0;
            cmbGroup.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
                string selectedproductids="";
                var itemsSource = dataGrid3.ItemsSource as IEnumerable;

                if (!datePickerEffectiveDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Please select an effective date.");
                    datePickerEffectiveDate.Focus();
                }
                else if (itemsSource != null)
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
                                if (selectedproductids.Length == 0)
                                {
                                    selectedproductids = tx.Text;
                                }
                                else
                                {
                                    selectedproductids = selectedproductids + "," + tx.Text;
                                }
                            }
                        }
                    }
                    if (selectedproductids != "")
                    {
                        string brandids = getBrandIDsSelected();
                        if (string.IsNullOrWhiteSpace(brandids))
                        {
                            MessageBox.Show("Please select one or more brands to add products.");
                        }
                        else
                        { 
                            SaveData(selectedproductids, brandids, int.Parse(cmbregiongroup.SelectedValue.ToString()), CommonVariables.UserCode, datePickerEffectiveDate.SelectedDate ?? DateTime.Today);
                            MessageBox.Show(selectedproductids.Split(',').Length.ToString() + " Products added.");
                        }
                    }
                }

                else
                {
                    MessageBox.Show("Please select at least one PAG from the grid.");
                }
        }

        private void SaveData(string selectedids, string brandids, int regiongroupid, string usercode, DateTime effectivedate)
        {
            sr.SaveStandardInclusionToBrand(selectedids, brandids, regiongroupid, usercode, effectivedate);
            btnSearchAvailable_Click(null, null);
        }

    }
}
