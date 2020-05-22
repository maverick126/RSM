using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
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
namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmDOLProductReplacement.xaml
    /// </summary>
    public partial class frmDOLProductReplacement : Window
    {
        private SQSAdminServiceClient client;
        private int loginstateid, promotionid, promotiontypeid;
        private string userCode;
        private CommonResource cr;
        private BackgroundWorker backgroundWorker1;

        public frmDOLProductReplacement(int stateid, string usercode)
        {
            loginstateid = stateid;
            userCode = usercode;
            cr = new CommonResource(stateid, 0);
            backgroundWorker1 = new BackgroundWorker();

            InitializeComponent();

            InitializeBackgroundWorker();
            this.DataContext = cr;
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

       private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            //backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            cr.LoadAllAreaForAvailableProduct();
            cr.LoadAllGroupForAvailableProduct();

            textBlock1.Text = @"Replace a product in selected display homes.";
 
            this.loadingimage.Image = global::SQSAdmin_WpfCustomControlLibrary.Properties.Resources.processingImage;
            SearchAvailableProducts_MultiThread();
            SearchNewProducts();
        }
        private void btnSearch1_Click(object sender, RoutedEventArgs e)
        {
            SearchAvailableProducts_MultiThread();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string selectedpagids = "";
            var itemsSource = dataGrid1.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                
                foreach (var item in itemsSource)
                {
                    var row = dataGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGrid1.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                        TextBlock tx = dataGrid1.Columns[1].GetCellContent(row) as TextBlock;
                        if ((bool)chk.IsChecked)
                        {
                            if (selectedpagids.Length == 0)
                            {
                                selectedpagids = tx.Text;
                            }
                            else
                            {
                                selectedpagids = selectedpagids + "," + tx.Text;
                            }
                        }
                    }
                }
            }

            if (selectedpagids.Length != 0)
            {
                AddProductsToPromotion(selectedpagids);
                //SearchAvailableProducts(null, null, int.Parse(cmbArea.SelectedValue.ToString()), int.Parse(cmbGroup.SelectedValue.ToString()), txtProductID.Text, txtPAGID.Text);
                cr.LoadAllAreaForExistingPromotion();
                cmbArea2.SelectedValue = 0;
                cr.LoadAllGroupForExistingPromotion();
                cmbGroup2.SelectedIndex = 0;
                SearchAvailableProducts();
                SearchNewProducts();
            }


        }

        private void AddProductsToPromotion(string selectedid)
        {
            client.SQSAdmin_Promotion_AddProductsToPromotion(promotionid, selectedid);
        }

        private void cmbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                cr.LoadFilteredGroupForAvailableProduct(int.Parse(cmbArea.SelectedValue.ToString()));
                cmbGroup.SelectedIndex = 0;
        }
        private void cmbArea2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cr.LoadFilteredGroupForAvailableProduct2(int.Parse(cmbArea2.SelectedValue.ToString()));
            cmbGroup2.SelectedIndex = 0;
        }
        //private string SearchAvailableProducts(System.ComponentModel.BackgroundWorker worker, System.ComponentModel.DoWorkEventArgs e, int areaid, int groupid, string productstring, string pagid)
        private void SearchAvailableProducts_MultiThread()
        {
            CallParameter cp = new CallParameter();
            cp.areaid = int.Parse(cmbArea.SelectedValue.ToString());
            cp.groupid = int.Parse(cmbGroup.SelectedValue.ToString());
            cp.productstring = txtProductID.Text;


            try
            {
                int aa = int.Parse(txtPAGID.Text.ToString());
                cp.pagid = aa.ToString();
            }
            catch (Exception ex)
            {
                cp.pagid = "0";
            }

            imagebox.Visibility = Visibility.Visible;
            backgroundWorker1.RunWorkerAsync(cp);

            //DataSet ds = client.SQSAdmin_Promotion_GetAvailableProductsForPromotion(promotionid, areaid, groupid, productstring, pagid);

            //dataGrid1.DataContext = ds.Tables[0];
        }
        private void SearchAvailableProducts()
        {
            DataSet ds = client.SQSAdmin_Generic_GetAvailablePAGByState(loginstateid.ToString(), cmbArea.SelectedValue.ToString(), cmbGroup.SelectedValue.ToString(), txtProductID.Text, txtPAGID.Text);
            dataGrid1.DataContext = ds.Tables[0];
        }
        private void btnSearch2_Click(object sender, RoutedEventArgs e)
        {
            SearchNewProducts();
        }

        private void SearchNewProducts()
        {
            string areaid = cmbArea2.SelectedValue.ToString();
            string groupid = cmbGroup2.SelectedValue.ToString();
            string productstring = txtProductID2.Text;
            string pagid = "0";

            try
            {
                int aa = int.Parse(txtPAGID2.Text.ToString());
                pagid = aa.ToString();
            }
            catch (Exception ex)
            {
                pagid = "0";
            }

            DataSet ds = client.SQSAdmin_Generic_GetAvailablePAGByState(loginstateid.ToString(), areaid, groupid, productstring, pagid);

            dataGrid2.DataContext = ds.Tables[0];

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string removepagids = "";
            var itemsSource = dataGrid2.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = dataGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGrid2.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                    TextBlock tx = dataGrid2.Columns[1].GetCellContent(row) as TextBlock;
                    if ((bool)chk.IsChecked)
                    {
                        if (removepagids.Length == 0)
                        {
                            removepagids = tx.Text;
                        }
                        else
                        {
                            removepagids = removepagids + "," + tx.Text;
                        }
                    }
                }
            }

            if (removepagids.Length != 0)
            {
                RemoveProductsFromPromotion(removepagids);
                //SearchAvailableProducts(null, null, int.Parse(cmbArea.SelectedValue.ToString()), int.Parse(cmbGroup.SelectedValue.ToString()), txtProductID.Text, txtPAGID.Text);
                cr.LoadAllAreaForExistingPromotion();
                cmbArea2.SelectedValue = 0;
                cr.LoadAllGroupForExistingPromotion();
                cmbGroup2.SelectedIndex = 0;
                SearchAvailableProducts();
                SearchNewProducts();
            }
        }

        private void RemoveProductsFromPromotion(string selectedid)
        {
            client.SQSAdmin_Promotion_RemoveProductsFromPromotion(promotionid, selectedid);
        }


        private void dataGrid2_RowEditEnding(object sender, Microsoft.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            int defaultselected = 0;
            decimal  discount=0;
            int pagid = 0;
            bool ok = true;
            string message = "";

            DataRowView dv = (DataRowView)e.Row.Item;
            if (dv != null)
            {
                if (dv[7] != null)
                {
                    if (dv[7].ToString().ToUpper() == "TRUE")
                    {
                        defaultselected = 1;
                    }
                    else
                    {
                        defaultselected = 0;
                    }

                }

                try
                {
                    discount = decimal.Parse(dv[8].ToString());
                    if (discount > 1 || discount < 0)
                    {
                        ok = false;
                        message="The discount percentage should be between 0 and 1.";
                    }
                }
                catch (Exception ex)
                {
                    message="Please enter a valid percentage.";
                    ok = false;
                }

                pagid = int.Parse(dv[0].ToString());

                if (ok)
                {
                    client.SQSAdmin_Promotion_UpdateProductForPromotion(promotionid, pagid, defaultselected, discount);
                }
                else
                {
                    MessageBox.Show(message);
                }
            }

        }

        //private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        //{

        //}
        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                dataGrid1.DataContext = ((DataSet)e.Result).Tables[0].DefaultView;               
            }
            imagebox.Visibility = Visibility.Collapsed;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            CallParameter cp = (CallParameter)e.Argument;
            e.Result = SearchAvailableProducts1(loginstateid.ToString(), cp.areaid.ToString(), cp.groupid.ToString(), cp.productstring, cp.pagid);
        }
        private DataSet SearchAvailableProducts1(string stateid, string areaid, string groupid, string keyword, string pagid)
        {

            DataSet ds = client.SQSAdmin_Generic_GetAvailablePAGByState(stateid,areaid, groupid, keyword, pagid);
            return ds;

        }
        public class CallParameter
        {
            public int promotionid { get; set; }                
            public int areaid { get; set; }
            public int groupid { get; set; }
            public string productstring { get; set; }
            public string pagid { get; set; }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            if (client != null && client.State == System.ServiceModel.CommunicationState.Opened)
            {
                client.Close();
            }
        }

        private void btnSearchDisplayHome_Click(object sender, RoutedEventArgs e)
        {
            string tempid = "0";
            var itemsSource = dataGrid1.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = dataGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGrid1.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;                    
                    if ((bool)chk.IsChecked)
                    {
                        TextBlock tx = dataGrid1.Columns[1].GetCellContent(row) as TextBlock;
                        tempid = tx.Text;
                        break;
                    }
                }
            }
            cr.LoadDisplayHome(loginstateid.ToString(), cmbBrand.SelectedValue.ToString(), tempid, txtHomeName.Text, txtSuburb.Text);
            if (cr.SQSDisplayHome.Count == 0)
            {
                txtNoRecord.Visibility = Visibility.Visible;
            }
            else
            {
                txtNoRecord.Visibility = Visibility.Collapsed;
            }
        }

        private void cmbBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void chkSelected_Checked(object sender, RoutedEventArgs e)
        {
            DataRowView dataGridRow = (DataRowView)dataGrid1.CurrentItem;
            string tempid = dataGridRow[0].ToString();

            var itemsSource = dataGrid1.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = dataGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGrid1.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                    TextBlock tx = dataGrid1.Columns[1].GetCellContent(row) as TextBlock;
                    if ((bool)chk.IsChecked && tempid!=tx.Text.Trim() )
                    {
                        chk.IsChecked = false;
                    }
                }
            }
        }

        private void chkSelected2_Checked(object sender, RoutedEventArgs e)
        {
            DataRowView dataGridRow = (DataRowView)dataGrid2.CurrentItem;
            string tempid = dataGridRow[0].ToString();

            var itemsSource = dataGrid2.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = dataGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGrid2.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                    TextBlock tx = dataGrid2.Columns[1].GetCellContent(row) as TextBlock;
                    if ((bool)chk.IsChecked && tempid != tx.Text.Trim())
                    {
                        chk.IsChecked = false;
                    }
                }
            }
        }

        private void btnReplacePAG_Click(object sender, RoutedEventArgs e)
        {
            string oldpagid = GetOldProduct();
            string newpagid = GetNewProduct();
            string displayhomeid = GetDisplayHomes();
            string changeqty, changeprice, allowdesc, qty, desc, newextradesc;

            if ((bool)chkchangeprice.IsChecked)
            {
                changeprice = "1";
            }
            else
            {
                changeprice = "0";
            }

            if ((bool)chkchangeqty.IsChecked)
            {
                changeqty = "1";
            }
            else
            {
                changeqty = "0";
            }

            if ((bool)chkenterdesc.IsChecked)
            {
                allowdesc = "1";
            }
            else
            {
                allowdesc = "0";
            }

            if ((bool)chkqty.IsChecked)
            {
                qty = "1";
            }
            else
            {
                qty = "0";
            }

            if ((bool)chkdesc.IsChecked)
            {
                desc = "1";
                newextradesc = string.Empty;
            }
            else
            {
                desc = "0";
                newextradesc = textBoxExtraDesc.Text.ToString();
            }

            if (oldpagid.Trim() == "")
            {
                MessageBox.Show("Please select an existing product.");
                return;
            }

            if (newpagid.Trim() == "")
            {
                MessageBox.Show("Please select a new product to replace selected product.");
                return;
            }

            if (newpagid != "" && oldpagid != "" && oldpagid == newpagid)
            {
                MessageBox.Show("Selectd new product is the same as old one, no need to replace.");
                return;
            }

            if (displayhomeid.Trim()=="")
            {
                MessageBox.Show("Please select at least one display home.");
                return;
            }

            string result= cr.ReplacePAGInDisplayhomes(oldpagid, newpagid, displayhomeid,userCode, qty, changeqty,changeprice,allowdesc,desc,newextradesc);
            if (result != "")
            {
                MessageBox.Show(result.Replace("<br>", "\n"));
                btnSearchDisplayHome_Click(null, null);
            }

        }



        private string GetOldProduct()
        {
            string tempid = "";
            var itemsSource = dataGrid1.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = dataGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGrid1.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                    TextBlock tx = dataGrid1.Columns[1].GetCellContent(row) as TextBlock;
                    if ((bool)chk.IsChecked)
                    {
                        tempid = tx.Text;
                        break;
                    }
                }
            }
            return tempid;
        }

        private void chkdesc_Click(object sender, RoutedEventArgs e)
        {
            textBlockExtraDesc.Visibility = (chkdesc.IsChecked ?? false) ? Visibility.Collapsed : Visibility.Visible;
            textBoxExtraDesc.Visibility = textBlockExtraDesc.Visibility;
        }

        private string GetNewProduct()
        {
            string tempid = "";

            var itemsSource = dataGrid2.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = dataGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGrid2.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                    TextBlock tx = dataGrid2.Columns[1].GetCellContent(row) as TextBlock;
                    if ((bool)chk.IsChecked )
                    {
                       tempid = tx.Text.Trim();
                       break;
                    }
                }
            }
            return tempid;
        }

        private string GetDisplayHomes()
        {
            string tempid = "";

            var itemsSource = dataGridHome.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = dataGridHome.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGridHome.Columns[0].GetCellContent(row) as ContentPresenter;
                    DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    CheckBox chk = editingTemplate.FindName("chkSelected3", contentPresenter) as CheckBox;
                    TextBlock tx = dataGridHome.Columns[1].GetCellContent(row) as TextBlock;
                    if ((bool)chk.IsChecked)
                    {
                        if (tempid == "")
                        {
                            tempid = tx.Text.Trim();
                        }
                        else
                        {
                            tempid = tempid+","+ tx.Text.Trim();
                        }
                    }
                }
            }
            return tempid;
        }
    }
}
