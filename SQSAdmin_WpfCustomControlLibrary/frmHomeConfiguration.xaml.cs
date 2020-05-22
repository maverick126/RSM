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
    /// Interaction logic for frmHomeConfiguration.xaml
    /// </summary>
    public partial class frmHomeConfiguration : Window
    {
        public int homeid;
        private int loginstateid, promotionid, promotiontypeid;
        private string usercode, homename;
        private CommonResource cr;
        public CheckBox allcheckbox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public frmHomeConfiguration(int phomeid, string phomename, int pstateid, string pusercode)
        {
            homeid = phomeid;
            usercode=pusercode;
            loginstateid = pstateid;
            homename = phomename;
            backgroundWorker1 = new BackgroundWorker();

            cr = new CommonResource(loginstateid, 0);
            InitializeComponent();
            InitializeBackgroundWorker();
            //Binding.AddSourceUpdatedHandler(dataGrid2,OnDataGridSourceUpdated);
            this.DataContext = cr;
            label1.Content = label1.Content + " " + phomename;
            cr.LoadHomeConfigurationAreas(loginstateid);
            cr.LoadHomeConfigurationGroups(loginstateid);
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;

        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            CallParameter cp = (CallParameter)e.Argument;
            e.Result = SearchExistingProducts_multiThread(cp.homeid, cp.areaid, cp.groupid, cp.productid);
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            List<CommonResource.HomeConfigurationItem> HomeExistingPAG = new List<CommonResource.HomeConfigurationItem>();
            if (e.Result != null)
            {
                DataSet ds = (DataSet)e.Result;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    CommonResource.HomeConfigurationItem s = new CommonResource.HomeConfigurationItem();
                    s.HomeID = homeid;
                    s.PagID = int.Parse(dr["productareagroupid"].ToString());
                    s.AreaID = int.Parse(dr["areaid"].ToString());
                    s.AreaName = dr["areaname"].ToString();
                    s.GroupID = int.Parse(dr["groupid"].ToString());
                    s.GroupName = dr["groupname"].ToString();
                    s.ProductID = dr["productid"].ToString();
                    s.ProductName = dr["productname"].ToString();
                    s.ProductDescription = dr["productdescription"].ToString();
                    s.Quantity = decimal.Parse(dr["quantity"].ToString());
                    s.UnitPrice = decimal.Parse(dr["unitprice"].ToString());
                    if (dr["ChangePrice"].ToString().ToUpper() == "TRUE" || dr["ChangePrice"].ToString() == "1")
                    {
                        s.ChangePrice = true;
                    }
                    else
                    {
                        s.ChangePrice = false;
                    }
                    if (dr["ChangeQty"].ToString().ToUpper() == "TRUE" || dr["ChangeQty"].ToString() == "1")
                    {
                        s.ChangeQuantity = true;
                    }
                    else
                    {
                        s.ChangeQuantity = false;
                    }
                    if (dr["HomeDisplayID"].ToString() == "0")
                    {
                        s.IsDisplayHomeItem = false;
                    }
                    else
                    {
                        s.IsDisplayHomeItem = true;
                    }
                    if (dr["addextradesc"].ToString().ToUpper() == "TRUE" || dr["addextradesc"].ToString() == "1")
                    {
                        s.AddExtraDesc = true;
                    }
                    else
                    {
                        s.AddExtraDesc = false;
                    }
                    HomeExistingPAG.Add(s);
                }

                dataGrid2.DataContext = HomeExistingPAG;
                txttotal.Text = HomeExistingPAG.Count.ToString();
                imagebox.Visibility = Visibility.Collapsed;
            }
            
        }
        //private void OnDataGridSourceUpdated(object sender, DataTransferEventArgs e)
        //{
        //    int pagid = 0;
        //    decimal quantity = 0;
        //    int changeqty=0;
        //    int changeprice = 0;
        //    int addextradesc=0;
        //    DependencyObject dep = (DependencyObject)e.OriginalSource;
 
        //    while ((dep != null))
        //    {
        //        if (dep is Microsoft.Windows.Controls.DataGridCell)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            dep = VisualTreeHelper.GetParent(dep);
        //        }
        //    }
        //    if (dep is Microsoft.Windows.Controls.DataGridCell)
        //    {
        //        while (dep != null && !(dep is Microsoft.Windows.Controls.DataGridRow))
        //        {
        //            dep = VisualTreeHelper.GetParent(dep); 
        //        }
        //        Microsoft.Windows.Controls.DataGridRow row = dep as Microsoft.Windows.Controls.DataGridRow;
        //        TextBlock tx = dataGrid2.Columns[1].GetCellContent(row) as TextBlock;
        //        pagid = int.Parse(tx.Text);

        //        ContentPresenter cp1 = dataGrid1.Columns[7].GetCellContent(row) as ContentPresenter;
        //        DataTemplate et1 = cp1.ContentTemplate;
        //        TextBox tx2 = et1.FindName("txtQty1", cp1) as TextBox;
        //        try
        //        {
        //            quantity = decimal.Parse(tx2.Text);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Please enter a valid quantity for PAG "+pagid.ToString()+" !");
        //            return;
        //        }
        //        ContentPresenter cp2 = dataGrid1.Columns[8].GetCellContent(row) as ContentPresenter;
        //        DataTemplate et2 = cp2.ContentTemplate;
        //        CheckBox chkqty2 = et2.FindName("chkchangequantity", cp2) as CheckBox;
        //        if ((bool)chkqty2.IsChecked)
        //        {
        //            changeqty = 1;
        //        }
        //        else
        //        {
        //            changeqty = 0;
        //        }

        //        ContentPresenter cp3 = dataGrid1.Columns[9].GetCellContent(row) as ContentPresenter;
        //        DataTemplate et3 = cp3.ContentTemplate;
        //        CheckBox chkprice2 = et3.FindName("chkchangeprice", cp3) as CheckBox;
        //        if ((bool)chkprice2.IsChecked)
        //        {
        //            changeprice = 1;
        //        }
        //        else
        //        {
        //            changeprice = 0;
        //        }

        //        ContentPresenter cp4 = dataGrid1.Columns[10].GetCellContent(row) as ContentPresenter;
        //        DataTemplate et4 = cp4.ContentTemplate;
        //        CheckBox extra2 = et4.FindName("chkextra", cp4) as CheckBox;
        //        if ((bool)extra2.IsChecked)
        //        {
        //            addextradesc = 1;
        //        }
        //        else
        //        {
        //            addextradesc = 0;
        //        }
                

        //    }
        //    cr.UpdatePagForHome(homeid, pagid, quantity, changeqty, changeprice, addextradesc, usercode);

        //}
        //static T FindVisualParent<T>(UIElement element) where T : UIElement
        //{
        //    UIElement parent = element;
        //    while (parent != null)
        //    {
        //        T correctlyTyped = parent as T;
        //        if (correctlyTyped != null)
        //        {
        //            return correctlyTyped;
        //        }
        //        parent = VisualTreeHelper.GetParent(parent) as UIElement;
        //    }
        //    return null;

        //}
        private void btnSearch2_Click(object sender, RoutedEventArgs e)
        {
            SearchExistingProducts();
        }
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string selectedpagids = "";
            var itemsSource = dataGrid2.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    var row = dataGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGrid2.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
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
                cr.RemoveProductsFromHome(homeid, selectedpagids, usercode);
                SearchAvailableProducts();
                SearchExistingProducts();
            }
            else
            {
                MessageBox.Show("Please select at leat one product to remove from the home.");
            }
        }

        public void SearchExistingProducts()
        {
            CallParameter cp = new CallParameter();

            cp.homeid = homeid;
            cp.productid = txtProductID2.Text;
            cp.areaid = int.Parse(cmbArea2.SelectedValue.ToString());
            cp.groupid = int.Parse(cmbGroup2.SelectedValue.ToString());

            this.loadingimage.Image = global::SQSAdmin_WpfCustomControlLibrary.Properties.Resources.processingImage;
            imagebox.Visibility = Visibility.Visible;
            backgroundWorker1.RunWorkerAsync(cp);
            //cr.LoadHomeExistingPAG(homeid, int.Parse(cmbArea2.SelectedValue.ToString()), int.Parse(cmbGroup2.SelectedValue.ToString()), txtProductID2.Text);
        }

        public DataSet SearchExistingProducts_multiThread(int homeid, int areaid, int groupid, string productid)
        {
            return cr.LoadHomeExistingPAGAsDataSet(homeid, areaid, groupid, productid);
        }
        private void SearchAvailableProducts()
        {
            cr.LoadHomeAvailablePAG(homeid, int.Parse(cmbArea.SelectedValue.ToString()), int.Parse(cmbGroup.SelectedValue.ToString()), txtProductID.Text);
        }

        private void cmbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cmbArea2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string selectedpagids = "";
            string qtystring = "";
            string changeqtystring = "";
            string changepricestring = "";
            string extrastring="";
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
                            ContentPresenter cp2 = dataGrid1.Columns[8].GetCellContent(item) as ContentPresenter;
                            DataTemplate et2 = cp2.ContentTemplate;
                            CheckBox chkqty2 = et2.FindName("chkchangequantity2", cp2) as CheckBox;

                            ContentPresenter cp3 = dataGrid1.Columns[9].GetCellContent(item) as ContentPresenter;
                            DataTemplate et3 = cp3.ContentTemplate;
                            CheckBox chkprice2 = et3.FindName("chkchangeprice2", cp3) as CheckBox;

                            ContentPresenter cp4 = dataGrid1.Columns[10].GetCellContent(item) as ContentPresenter;
                            DataTemplate et4 = cp4.ContentTemplate;
                            CheckBox extra2 = et4.FindName("chkextra2", cp4) as CheckBox;

                            ContentPresenter cp1 = dataGrid1.Columns[7].GetCellContent(item) as ContentPresenter;
                            DataTemplate et1 = cp1.ContentTemplate;
                            TextBox tx2 = et1.FindName("txtQty", cp1) as TextBox;

                            try
                            {
                                decimal a = decimal.Parse(tx2.Text);
                            }
                            catch (Exception ex)
                            {
                                TextBlock tx3 = dataGrid1.Columns[4].GetCellContent(row) as TextBlock;
                                MessageBox.Show("Please enter a valid quantity for item "+ tx3.Text+"!");
                                return;
                            }
                            
                            if (selectedpagids.Length == 0)
                            {
                                selectedpagids = tx.Text;
                                qtystring = tx2.Text;
                                if ((bool)chkqty2.IsChecked)
                                {
                                    changeqtystring = "1";
                                }
                                else
                                {
                                    changeqtystring = "0";
                                }
                                if ((bool)chkprice2.IsChecked)
                                {
                                    changepricestring = "1";
                                }
                                else
                                {
                                    changepricestring = "0";
                                }
                                if ((bool)extra2.IsChecked)
                                {
                                    extrastring = "1";
                                }
                                else
                                {
                                    extrastring = "0";
                                }
                            }
                            else
                            {
                                selectedpagids = selectedpagids + "," + tx.Text;
                                qtystring = qtystring + "," + tx2.Text;
                                if ((bool)chkqty2.IsChecked)
                                {
                                    changeqtystring =changeqtystring+ ",1";
                                }
                                else
                                {
                                    changeqtystring = changeqtystring+",0";
                                }
                                if ((bool)chkprice2.IsChecked)
                                {
                                    changepricestring = changepricestring+",1";
                                }
                                else
                                {
                                    changepricestring = changepricestring+",0";
                                }
                                if ((bool)extra2.IsChecked)
                                {
                                    extrastring = extrastring+",1";
                                }
                                else
                                {
                                    extrastring = extrastring+",0";
                                }
                            }
                           
                        }
                    }
                }
            }

            if (selectedpagids.Length != 0)
            {
                cr.AddProductsToHome(homeid, selectedpagids, qtystring, changeqtystring, changepricestring, extrastring, usercode);
                //SearchAvailableProducts(null, null, int.Parse(cmbArea.SelectedValue.ToString()), int.Parse(cmbGroup.SelectedValue.ToString()), txtProductID.Text, txtPAGID.Text);
                SearchAvailableProducts();
                SearchExistingProducts();
            }
            else
            {
                MessageBox.Show("Please select at leat one product to configure.");
            }
        }

        private void btnSearch1_Click(object sender, RoutedEventArgs e)
        {
            SearchAvailableProducts();
        }

        private void btnClear2_Click(object sender, RoutedEventArgs e)
        {
            txtProductID2.Text = "";
            cmbArea2.SelectedIndex = 0;
            cmbGroup2.SelectedIndex = 0;
        }

        private void btnClear1_Click(object sender, RoutedEventArgs e)
        {
            txtProductID.Text = "";
            cmbArea.SelectedIndex = 0;
            cmbGroup.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SearchExistingProducts();
            SearchAvailableProducts();     
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int pagid = 0;
            decimal quantity = 0;
            int changeqty = 0;
            int changeprice = 0;
            int addextradesc = 0;
            CommonResource.HomeConfigurationItem item = ((Button)e.OriginalSource).DataContext as CommonResource.HomeConfigurationItem;
                
                pagid = item.PagID;
                quantity = item.Quantity;

                if (item.ChangeQuantity)
                {
                    changeqty = 1;
                }
                else
                {
                    changeqty = 0;
                }
                if (item.ChangePrice)
                {
                    changeprice = 1;
                }
                else
                {
                    changeprice = 0;
                }

                if (item.AddExtraDesc)
                {
                    addextradesc = 1;
                }
                else
                {
                    addextradesc = 0;
                }
                
            cr.UpdatePagForHome(homeid, pagid, quantity, changeqty, changeprice, addextradesc, usercode);

        }

        private void chkSelectedall_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ckall =(CheckBox)e.OriginalSource;
            allcheckbox = ckall;
            var itemsSource = dataGrid2.ItemsSource as IEnumerable;
            if ((bool)ckall.IsChecked)
            {
                
                if (itemsSource != null)
                {

                    foreach (var item in itemsSource)
                    {
                        var row = dataGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                        ContentPresenter contentPresenter = dataGrid2.Columns[0].GetCellContent(item) as ContentPresenter;
                        if (contentPresenter != null)
                        {
                            DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                            CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                            chk.IsChecked=true;
                        }
                    }
                }
            }
            else
            {
                if (itemsSource != null)
                {

                    foreach (var item in itemsSource)
                    {
                        var row = dataGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                        ContentPresenter contentPresenter = dataGrid2.Columns[0].GetCellContent(item) as ContentPresenter;
                        if (contentPresenter != null)
                        {
                            DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                            CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                            chk.IsChecked = false;
                        }
                    }
                }
            }
        }

        private void btnBulkQty_Click(object sender, RoutedEventArgs e)
        {
            string pagstring = "";
            var itemsSource = dataGrid2.ItemsSource as IEnumerable;
            if (itemsSource != null)
            {
                foreach (var item in itemsSource)
                {
                    var row = dataGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGrid2.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        TextBlock tx = dataGrid2.Columns[1].GetCellContent(row) as TextBlock;
                        CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                        if ((bool)chk.IsChecked)
                        {
                            if (pagstring.Trim() == "")
                            {
                                pagstring = tx.Text;
                            }
                            else
                            {
                                pagstring = pagstring+","+ tx.Text;
                            }
                        }
                    }
                }

                if (pagstring.Trim()!="")
                {
                    frmBulkUpdateQuantity win = new frmBulkUpdateQuantity(pagstring, homeid.ToString(), homename, cmbArea2.SelectedValue.ToString(), cmbArea2.Text, cmbGroup2.SelectedValue.ToString(), cmbGroup2.Text, usercode, loginstateid, txtProductID2.Text, this);
                    win.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select at least one item from the home configuration to update quantity!");
                }
            }
        }

        public class CallParameter
        {
            public int homeid { get; set; }
            public int areaid { get; set; }
            public int groupid { get; set; }
            public string productid { get; set; }

        }

    }
}
