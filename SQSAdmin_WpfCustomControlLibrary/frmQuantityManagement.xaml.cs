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
using System.Windows.Threading;
using System.Threading;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for frmQuantityManagement.xaml
    /// </summary>
    public partial class frmQuantityManagement : Window
    {
        private SQSAdminServiceClient client;
        private int loginstateid;
        private string usercode;
        private CommonResource cr;
        public CheckBox allcheckbox;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        public frmQuantityManagement(int pstate, string pusercode)
        {
            
            loginstateid = pstate;
            usercode = pusercode;
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker2 = new BackgroundWorker();

            cr = new CommonResource(pstate, 0);
            InitializeComponent();

            InitializeBackgroundWorker();

            this.DataContext = cr;

            cr.LoadAllAreaForAvailableProduct();
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
            //dataGrid1.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        }

        //void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        //{
        //    if (dataGrid1.ItemContainerGenerator.Status == System.Windows.Controls.Primitives.GeneratorStatus.ContainersGenerated)
        //    {
        //        dataGrid1.ItemContainerGenerator.StatusChanged -= ItemContainerGenerator_StatusChanged;
        //        imagebox.Visibility = Visibility.Collapsed;                
        //    }
        //}

        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);


            backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);
            //backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }
        private void cmbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //LoadBrandDropDown();
            int pstateid;
            if (cmbState.SelectedValue != null)
            {
                pstateid = int.Parse(cmbState.SelectedValue.ToString());
            }
            else
            {
                pstateid = loginstateid;
            }
            cr.LoadBrand(pstateid);
            cmbBrand.SelectedIndex = 0;
        }

        private void bthSearch_Click(object sender, RoutedEventArgs e)
        {
            //SearchQuantiy();
            Search();
        }
        public void SearchQuantiy()
        {
            int stateid = int.Parse(cmbState.SelectedValue.ToString());
            int brandid = int.Parse(cmbBrand.SelectedValue.ToString());
            int areaid = int.Parse(cmbArea.SelectedValue.ToString());
            int groupid = int.Parse(cmbGroup.SelectedValue.ToString());

            cr.LoadHomeModelQuantity(stateid.ToString(), areaid.ToString(), groupid.ToString(), brandid.ToString());
            dataGrid1.DataContext = cr.HomeModelQty;

        }

        private void bthRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (cmbArea.SelectedValue.ToString() == "0" || cmbGroup.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please select area and group");
            }
            else
            {
                frmRefreshQtyWarning win = new frmRefreshQtyWarning(this);
                win.ShowDialog();
            }
        }

        public void RefreshQuantity()
        {
            cr.RefreshQuantity(loginstateid.ToString(), cmbArea.SelectedValue.ToString(), cmbGroup.SelectedValue.ToString(), usercode);
            SearchQuantiy();
        }
        private void chkSelectedall_Click(object sender, RoutedEventArgs e)
        {
            CheckBox ckall = (CheckBox)e.OriginalSource;
            allcheckbox = ckall;
            var itemsSource = dataGrid1.ItemsSource as IEnumerable;
            if ((bool)ckall.IsChecked)
            {

                if (itemsSource != null)
                {

                    foreach (var item in itemsSource)
                    {
                        var row = dataGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                        ContentPresenter contentPresenter = dataGrid1.Columns[0].GetCellContent(item) as ContentPresenter;
                        if (contentPresenter != null)
                        {
                            DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                            CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                            chk.IsChecked = true;
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
                        var row = dataGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                        ContentPresenter contentPresenter = dataGrid1.Columns[0].GetCellContent(item) as ContentPresenter;
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

        private void bthUpdateQty_Click(object sender, RoutedEventArgs e)
        {
            string homestring = "";
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
                        TextBlock tx = dataGrid1.Columns[1].GetCellContent(row) as TextBlock;
                        CheckBox chk = editingTemplate.FindName("chkSelected2", contentPresenter) as CheckBox;
                        if ((bool)chk.IsChecked)
                        {
                            if (homestring.Trim() == "")
                            {
                                homestring = tx.Text;
                            }
                            else
                            {
                                homestring = homestring + "," + tx.Text;
                            }
                        }
                    }
                }

                if (homestring.Trim() != "")
                {
                    frmBulkUpdateHomeModelQuantity win = new frmBulkUpdateHomeModelQuantity(cmbBrand.SelectedValue.ToString(), homestring, cmbArea.SelectedValue.ToString(), cmbArea.Text, cmbGroup.SelectedValue.ToString(), cmbGroup.Text, usercode, loginstateid, this);
                    win.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please select at least one home model to update quantity!");
                }
            }
        }


        private void txtQty1_LostFocus(object sender, RoutedEventArgs e)
        {
            decimal quantity = 1;
            bool ok = true;
            string message = "";

            CommonResource.HomeModelAndQuantity hq = ((TextBox)e.OriginalSource).DataContext as CommonResource.HomeModelAndQuantity;
          
            if (hq != null)
            {

                try
                {
                    quantity = decimal.Parse(hq.UpdatedQuantity.ToString());
                }
                catch (Exception ex)
                {
                    message = "Please enter a valid quantity!";
                    ok = false;
                }


                if (ok)
                {
                    if (hq.UpdatedQuantity != hq.Quantity)
                    {
                        cr.UpdateQuantityAtHomeModel(loginstateid.ToString(), cmbArea.SelectedValue.ToString(), cmbGroup.SelectedValue.ToString(), cmbBrand.SelectedValue.ToString(), hq.HomeModel, quantity.ToString(), usercode);
                        foreach (CommonResource.HomeAndQuantity haq in hq.HomeList)
                        {
                            haq.Quantity = quantity;
                            haq.UpdatedQuantity = quantity;
                        }
                        SearchQuantiy();
                    }
                }
                else
                {
                    MessageBox.Show(message);
                }
            }
        }

        //private void txtQty2_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    decimal quantity = 1;
        //    bool ok = true;
        //    string message = "";

        //    CommonResource.HomeAndQuantity hq = ((TextBox)e.OriginalSource).DataContext as CommonResource.HomeAndQuantity;
        //    if (hq != null)
        //    {

        //        try
        //        {
        //            quantity = decimal.Parse(hq.UpdatedQuantity.ToString());
        //        }
        //        catch (Exception ex)
        //        {
        //            message = "Please enter a valid quantity!";
        //            ok = false;
        //        }


        //        if (ok)
        //        {
        //            if (hq.UpdatedQuantity != hq.Quantity)
        //            {
        //                cr.UpdateQuantityAtHomeFacade(cmbArea.SelectedValue.ToString(), cmbGroup.SelectedValue.ToString(), hq.HomeID.ToString(), quantity.ToString(), usercode);
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show(message);
        //        }
        //    }
        //}

        #region multiple thread code to show loading image
        public void Search()
        {
            CallParameter cp = new CallParameter();

            cp.stateid = cr.LoginState;
            cp.brandid = int.Parse(cmbBrand.SelectedValue.ToString());
            cp.areaid = int.Parse(cmbArea.SelectedValue.ToString());
            cp.groupid = int.Parse(cmbGroup.SelectedValue.ToString());

            this.loadingimage.Image = global::SQSAdmin_WpfCustomControlLibrary.Properties.Resources.processingImage;
            imagebox.Visibility = Visibility.Visible;
            backgroundWorker1.RunWorkerAsync(cp);
        }


        public void Refresh()
        {
            this.loadingimage.Image = global::SQSAdmin_WpfCustomControlLibrary.Properties.Resources.processingImage;
            CallParameter cp = new CallParameter();

            cp.stateid = cr.LoginState;
            cp.brandid = int.Parse(cmbBrand.SelectedValue.ToString());
            cp.areaid = int.Parse(cmbArea.SelectedValue.ToString());
            cp.groupid = int.Parse(cmbGroup.SelectedValue.ToString());

            imagebox.Visibility = Visibility.Visible;
            backgroundWorker2.RunWorkerAsync(cp);
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            CallParameter cp = (CallParameter)e.Argument;

            e.Result=cr.LoadHomeModelQuantity_Multithread(cp.stateid.ToString(), cp.areaid.ToString(), cp.groupid.ToString(), cp.brandid.ToString());
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                CommonResource.retrunValue rv = (CommonResource.retrunValue)e.Result;
                DataSet ds = rv.HomeModelSet;
                DataSet facadeds = rv.FacadeSet;
                List<CommonResource.HomeModelAndQuantity> HomeModelQty = new List<CommonResource.HomeModelAndQuantity>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    List<CommonResource.HomeAndQuantity> hl = new List<CommonResource.HomeAndQuantity>();
                    CommonResource.HomeModelAndQuantity hq = new CommonResource.HomeModelAndQuantity();
                    hq.HomeModel = dr["homemodel"].ToString();
                    hq.Quantity = decimal.Parse(dr["quantity"].ToString());
                    hq.UpdatedQuantity = decimal.Parse(dr["quantity"].ToString());
                    if ((dr["multipleqty"].ToString().ToUpper() == "1" || dr["multipleqty"].ToString().ToUpper() == "TRUE"))
                    {
                        hq.MultipleQuantity = true;
                    }
                    else
                    {
                        hq.MultipleQuantity = false;
                    }

                    hq.Display1 = dr["Display1"].ToString();
                    hq.Display2 = dr["Display2"].ToString();
                    hq.Display3 = dr["Display3"].ToString();
                    hq.Display4 = dr["Display4"].ToString();
                    hq.Display5 = dr["Display5"].ToString();
                    hq.Display6 = dr["Display6"].ToString();
                    hq.BMPImage = dr["warningimage"].ToString();

                    foreach (DataRow dr2 in facadeds.Tables[0].Rows)
                    {
                        if (dr2["homemodel"].ToString() == hq.HomeModel)
                        {
                            CommonResource.HomeAndQuantity hh = new CommonResource.HomeAndQuantity();
                            hh.HomeID = int.Parse(dr2["homeid"].ToString());
                            hh.HomeName = dr2["homemodel"].ToString() + " " + dr2["facade"].ToString();
                            hh.Quantity = decimal.Parse(dr2["quantity"].ToString());
                            hh.UpdatedQuantity = decimal.Parse(dr2["quantity"].ToString());
                            //if (dr2["allowedit"].ToString() == "1" || dr2["allowedit"].ToString().ToUpper() == "TURE")
                            //{
                            //    hh.Enabled = true;
                            //}
                            //else
                            //{
                            //    hh.Enabled = false;
                            //}

                            hl.Add(hh);
                        }
                    }
                    hq.HomeList = hl;
                    HomeModelQty.Add(hq);

                }
                dataGrid1.DataContext = HomeModelQty;
            }
             imagebox.Visibility = Visibility.Collapsed;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            CallParameter cp = (CallParameter)e.Argument;

            cr.RefreshQuantity(cp.stateid.ToString(), cp.areaid.ToString(), cp.groupid.ToString(), usercode);
            e.Result = cr.LoadHomeModelQuantity_Multithread(cp.stateid.ToString(), cp.areaid.ToString(), cp.groupid.ToString(), cp.brandid.ToString());
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                CommonResource.retrunValue rv = (CommonResource.retrunValue)e.Result;
                DataSet ds = rv.HomeModelSet;
                DataSet facadeds = rv.FacadeSet;
                List<CommonResource.HomeModelAndQuantity> HomeModelQty = new List<CommonResource.HomeModelAndQuantity>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    List<CommonResource.HomeAndQuantity> hl = new List<CommonResource.HomeAndQuantity>();
                    CommonResource.HomeModelAndQuantity hq = new CommonResource.HomeModelAndQuantity();
                    hq.HomeModel = dr["homemodel"].ToString();
                    hq.Quantity = decimal.Parse(dr["quantity"].ToString());
                    hq.UpdatedQuantity = decimal.Parse(dr["quantity"].ToString());
                    if ((dr["multipleqty"].ToString().ToUpper() == "1" || dr["multipleqty"].ToString().ToUpper() == "TRUE"))
                    {
                        hq.MultipleQuantity = true;
                    }
                    else
                    {
                        hq.MultipleQuantity = false;
                    }

                    hq.Display1 = dr["Display1"].ToString();
                    hq.Display2 = dr["Display2"].ToString();
                    hq.Display3 = dr["Display3"].ToString();
                    hq.Display4 = dr["Display4"].ToString();
                    hq.Display5 = dr["Display5"].ToString();
                    hq.Display6 = dr["Display6"].ToString();
                    hq.BMPImage = dr["warningimage"].ToString();

                    foreach (DataRow dr2 in facadeds.Tables[0].Rows)
                    {
                        if (dr2["homemodel"].ToString() == hq.HomeModel)
                        {
                            CommonResource.HomeAndQuantity hh = new CommonResource.HomeAndQuantity();
                            hh.HomeID = int.Parse(dr2["homeid"].ToString());
                            hh.HomeName = dr2["homemodel"].ToString() + " " + dr2["facade"].ToString();
                            hh.Quantity = decimal.Parse(dr2["quantity"].ToString());
                            hh.UpdatedQuantity = decimal.Parse(dr2["quantity"].ToString());
                            //if (dr2["allowedit"].ToString() == "1" || dr2["allowedit"].ToString().ToUpper() == "TURE")
                            //{
                            //    hh.Enabled = true;
                            //}
                            //else
                            //{
                            //    hh.Enabled = false;
                            //}

                            hl.Add(hh);
                        }
                    }
                    hq.HomeList = hl;
                    HomeModelQty.Add(hq);

                }
                dataGrid1.DataContext = HomeModelQty;
            }
            imagebox.Visibility = Visibility.Collapsed;
        }
        //void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    pbStatus.Value = e.ProgressPercentage;
        //}

        public class CallParameter
        {
            public int stateid { get; set; }
            public int brandid { get; set; }
            public int areaid { get; set; }
            public int groupid { get; set; }
            
        }

        #endregion

        private void cmbArea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cr.LoadFilteredGroupWithoutAllForAvailableProduct(int.Parse(cmbArea.SelectedValue.ToString()));
            cmbGroup.SelectedIndex = 0;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void dataGrid2_CellEditEnding(object sender, Microsoft.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            if (e.Row.Item != null)
            {
                CommonResource.HomeAndQuantity result = e.Row.Item as CommonResource.HomeAndQuantity;
                TextBox t = e.EditingElement as TextBox;
                decimal quantity = 1;
                bool ok = true;
                string message = "";

                try
                {
                    quantity = decimal.Parse(t.Text);
                }
                catch (Exception ex)
                {
                    message = "Please enter a valid quantity!";
                    ok = false;
                }


                if (ok)
                {
                    if (quantity != result.Quantity)
                    {
                        cr.UpdateQuantityAtHomeFacade(cmbArea.SelectedValue.ToString(), cmbGroup.SelectedValue.ToString(), result.HomeID.ToString(), quantity.ToString(), usercode);
                    }
                }
                else
                {
                    MessageBox.Show(message);
                }
            } 
        }
    }
}
