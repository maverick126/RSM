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
    /// Interaction logic for frmCopyProductConfiguration.xaml
    /// </summary>
    public partial class frmCopyProductConfiguration : Window
    {
        private int stateid;
        private string usercode;
        private CommonResource cr;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public frmCopyProductConfiguration(int pstateid, string pusercode)
        {
            stateid = pstateid;
            usercode = pusercode;
            cr = new CommonResource(stateid, 0);

            backgroundWorker1 = new BackgroundWorker();
            InitializeBackgroundWorker();  
        
            InitializeComponent();
            this.DataContext = cr;
            cmbState.SelectedValue = stateid;
            cmbState2.SelectedValue = stateid;
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void btnClear2_Click(object sender, RoutedEventArgs e)
        {
            cmbState.SelectedValue = stateid;
            txtProductID.Text = "";
            txtKeyword.Text = "";
        }

        private void btnClear1_Click(object sender, RoutedEventArgs e)
        {
            cmbState2.SelectedValue = stateid;
            txtProductID2.Text = "";
            txtKeyword2.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SearchSourceProducts();
            SearchTargetProducts();
            this.loadingimage.Image = global::SQSAdmin_WpfCustomControlLibrary.Properties.Resources.processingImage; 
        }
        private void SearchSourceProducts()
        {
            cr.LoadSourceProducts(int.Parse(cmbState.SelectedValue.ToString()), txtProductID.Text, txtKeyword.Text );
        }
        private void SearchTargetProducts()
        {
            cr.LoadTargetProducts(int.Parse(cmbState2.SelectedValue.ToString()), txtProductID2.Text, txtKeyword2.Text);
        }

        private void btnSearch1_Click(object sender, RoutedEventArgs e)
        {
            SearchTargetProducts();
        }

        private void btnSearch2_Click(object sender, RoutedEventArgs e)
        {
            SearchSourceProducts();
        }

        private void chkSelected2_Checked(object sender, RoutedEventArgs e)
        {
             var itemsSource = dataGrid2.ItemsSource as IEnumerable;
             int index = dataGrid2.SelectedIndex;
             if (itemsSource != null)
             {

                 foreach (var item in itemsSource)
                 {
                     var row = dataGrid2.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                     if (row.GetIndex() != index)
                     {
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

        private void chkSelected_Checked(object sender, RoutedEventArgs e)
        {
            var itemsSource = dataGrid1.ItemsSource as IEnumerable;
            int index = dataGrid1.SelectedIndex;
            int totalselected = 0;
            if (itemsSource != null)
            {

                foreach (var item in itemsSource)
                {
                    //var row = dataGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGrid1.Columns[0].GetCellContent(item) as ContentPresenter;
                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                        if ((bool)chk.IsChecked)
                        {
                            totalselected = totalselected + 1;
                        }
                    }
                    //if (row.GetIndex() != index)
                    //{
                    //    ContentPresenter contentPresenter = dataGrid1.Columns[0].GetCellContent(item) as ContentPresenter;
                    //    if (contentPresenter != null)
                    //    {
                    //        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                    //        CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                    //        chk.IsChecked = false;
                    //    }
                    //}
                }
                if (totalselected > 10)
                {
                   CheckBox chk=  (CheckBox)e.OriginalSource;
                   chk.IsChecked = false;
                   MessageBox.Show("WARNING: You cannot copy select source product to more than 10 target products.");
                }
            }
        }

        private void btnCopy_Click(object sender, RoutedEventArgs e)
        {
            string sourceproductid = "";
            string targetproductid = "";



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
                        if ((bool)chk.IsChecked)
                        {
                            TextBlock tx = dataGrid2.Columns[1].GetCellContent(row) as TextBlock;
                            sourceproductid = tx.Text;
                            break;
                        }
                        
                    }
                }
            }

            // get target product
            var itemsSource1 = dataGrid1.ItemsSource as IEnumerable;
            if (itemsSource1 != null)
            {

                foreach (var item in itemsSource1)
                {
                    var row = dataGrid1.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;
                    ContentPresenter contentPresenter = dataGrid1.Columns[0].GetCellContent(item) as ContentPresenter;

                    if (contentPresenter != null)
                    {
                        DataTemplate editingTemplate = contentPresenter.ContentTemplate;
                        CheckBox chk = editingTemplate.FindName("chkSelected", contentPresenter) as CheckBox;
                        if ((bool)chk.IsChecked)
                        {
                            TextBlock tx = dataGrid1.Columns[1].GetCellContent(row) as TextBlock;
                            if (targetproductid == "")
                            {
                                targetproductid = tx.Text;
                            }
                            else
                            {
                                targetproductid = targetproductid+","+tx.Text;
                            }

                        }

                    }
                }
            }

            if (sourceproductid == targetproductid)
            {
                MessageBox.Show("Target product is same as source product. No configuration copied.");
            }
            else if (sourceproductid != "" && targetproductid != "")
            {
                CallParameter cp = new CallParameter();
                cp.sourceproductid = sourceproductid;
                cp.targetproductid = targetproductid;
                cp.usercode = usercode;
                imagebox.Opacity = 0.7;
                imagebox.Visibility = Visibility.Visible;
                
                backgroundWorker1.RunWorkerAsync(cp);
                //string result = cr.CopyProductConfiguration(sourceproductid, targetproductid, usercode);
                //MessageBox.Show(result.Replace(@"\r\n", System.Environment.NewLine));
            }
            else
            {               
                MessageBox.Show("Wrong source or target product ID. please check.");
            }
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            //backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }
        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{

        //}
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            imagebox.Opacity = 1;
            imagebox.Visibility = Visibility.Collapsed;
            
            if (e.Result != null)
            {
                string  res = ((string)e.Result);        
                MessageBox.Show(res.Replace(@"\r\n", System.Environment.NewLine));
            }
           
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            CallParameter cp = (CallParameter)e.Argument;
            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            e.Result = cr.CopyProductConfiguration(cp.sourceproductid, cp.targetproductid, cp.usercode);
        }

        public class CallParameter
        {
            public string sourceproductid { get; set; }
            public string targetproductid { get; set; }
            public string usercode { get; set; }

        }
    }
}
