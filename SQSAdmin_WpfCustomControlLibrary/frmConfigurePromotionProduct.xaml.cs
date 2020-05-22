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
    /// Interaction logic for frmConfigurePromotionProduct.xaml
    /// </summary>
    public partial class frmConfigurePromotionProduct : Window
    {
        private SQSAdminServiceClient client;
        private int loginstateid, promotionid, promotiontypeid;
        private CommonResource cr;
        private BackgroundWorker backgroundWorker1;
        public frmConfigurePromotionProduct(int stateid, int multiplepromotionid)
        {
            loginstateid = stateid;
            promotionid = multiplepromotionid;
            cr = new CommonResource(stateid, multiplepromotionid);
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
            cr.LoadPromotionType();
            cr.LoadAllAreaForAvailableProduct();
            cr.LoadAllAreaForExistingPromotion();
            cr.LoadAllGroupForExistingPromotion();
            DataSet ds = client.SQSAdmin_Promotion_GetPromotionDetails(promotionid);
            promotiontypeid = int.Parse(ds.Tables[0].Rows[0]["fkidpromotiontypeid"].ToString());
            textBlock1.Text = @"Configure promotion products for promotion -- " + ds.Tables[0].Rows[0]["promotionname"].ToString() + @".         Type: " + ds.Tables[0].Rows[0]["promotiontype"].ToString() + @"            Base Product: " + ds.Tables[0].Rows[0]["baseproductid"].ToString();
            //SearchAvailableProducts(null,null, int.Parse(cmbArea.SelectedValue.ToString()), int.Parse(cmbGroup.SelectedValue.ToString()),txtProductID.Text, txtPAGID.Text);
 
            this.loadingimage.Image = global::SQSAdmin_WpfCustomControlLibrary.Properties.Resources.processingImage;
            SearchAvailableProducts_MultiThread();
            SearchExistingProducts();
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
                SearchExistingProducts();
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

        //private string SearchAvailableProducts(System.ComponentModel.BackgroundWorker worker, System.ComponentModel.DoWorkEventArgs e, int areaid, int groupid, string productstring, string pagid)
        private void SearchAvailableProducts_MultiThread()
        {
            CallParameter cp = new CallParameter();
            cp.promotionid = promotionid;
            cp.areaid = int.Parse(cmbArea.SelectedValue.ToString());
            cp.groupid = int.Parse(cmbGroup.SelectedValue.ToString());
            cp.productstring = txtProductID.Text;
            cp.pagid = txtPAGID.Text;

            imagebox.Visibility = Visibility.Visible;
            backgroundWorker1.RunWorkerAsync(cp);

            //DataSet ds = client.SQSAdmin_Promotion_GetAvailableProductsForPromotion(promotionid, areaid, groupid, productstring, pagid);

            //dataGrid1.DataContext = ds.Tables[0];
        }
        private void SearchAvailableProducts()
        {
            DataSet ds = client.SQSAdmin_Promotion_GetAvailableProductsForPromotion(promotionid, int.Parse(cmbArea2.SelectedValue.ToString()), int.Parse(cmbGroup2.SelectedValue.ToString()), txtProductID.Text, txtPAGID.Text);
            dataGrid1.DataContext = ds.Tables[0];
        }
        private void btnSearch2_Click(object sender, RoutedEventArgs e)
        {
            SearchExistingProducts();
        }

        private void SearchExistingProducts()
        {
            int areaid = int.Parse(cmbArea2.SelectedValue.ToString());
            int groupid = int.Parse(cmbGroup2.SelectedValue.ToString());
            string productstring = txtProductID2.Text;
            string pagid = txtPAGID2.Text;

            DataSet ds = client.SQSAdmin_Promotion_GetExistingProductsForPromotion(promotionid, areaid, groupid, productstring, pagid);

            dataGrid2.DataContext = ds.Tables[0];

            if (promotiontypeid != 11 && promotiontypeid != 10)
            {
                dataGrid2.Columns[6].Visibility = Visibility.Collapsed;
                dataGrid2.Columns[7].Visibility = Visibility.Collapsed;
            }
            else if (promotiontypeid != 11)
            {
                dataGrid2.Columns[7].Visibility = Visibility.Collapsed;
            }
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
                SearchExistingProducts();
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
            e.Result = SearchAvailableProducts1(cp.promotionid, cp.areaid, cp.groupid, cp.productstring, cp.pagid);
        }
        private DataSet SearchAvailableProducts1(int multiplepromotionid,int areaid, int groupid, string productstring, string pagid)
        {

            DataSet ds = client.SQSAdmin_Promotion_GetAvailableProductsForPromotion(multiplepromotionid,areaid, groupid, productstring, pagid);
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



    }
}
