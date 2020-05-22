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
    /// Interaction logic for frmSearchProduct.xaml
    /// </summary>
    public partial class frmSearchProduct : Window
    {
        private SQSAdminServiceClient client;
        private CommonResource cr;
        private int loginstateid;
        public string ProductID;
        public string ProductName;
        public bool isStudioMProduct = true;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public frmSearchProduct(int stateid)
        { 
            loginstateid = stateid;
            backgroundWorker1 = new BackgroundWorker();
            cr = new CommonResource(stateid, 0);

            InitializeComponent();

            InitializeBackgroundWorker();
            this.DataContext = cr;
            //client = new SQSAdminServiceClient();
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            imagebox.Visibility = Visibility.Visible;
            this.loadingimage.Image = global::SQSAdmin_WpfCustomControlLibrary.Properties.Resources.processingImage;
            if (MySetting.Default.SearchProductID.ToString() != "")
            {
                txtProductID.Text = MySetting.Default.SearchProductID.ToString();
            }
            if (MySetting.Default.SearchProductName.ToString() != "")
            {
                txtProductName.Text = MySetting.Default.SearchProductName.ToString();
            }
            if (MySetting.Default.SearchProsuctDesc.ToString() != "")
            {
                txtProductDesc.Text = MySetting.Default.SearchProsuctDesc.ToString();
            }
            Search();
        }

        private void SearchProductPrice()
        {
            //int stateid = cr.LoginState; //int.Parse(cmbState.SelectedValue.ToString());
            //int regionid, studiomproduct;
            //if (cmbRegion.SelectedValue != null)
            //{
            //    regionid = int.Parse(cmbRegion.SelectedValue.ToString());
            //}
            //else
            //{
            //    regionid = 1;
            //}
            //int status = int.Parse(cmbStatus.SelectedValue.ToString());
            //if ((bool)chkStudioM.IsChecked)
            //{
            //    studiomproduct = 1;
            //}
            //else
            //{
            //    studiomproduct = 0;
            //}

            //DataSet ds = client.SQSAdmin_StudioM_GetProductPrice(stateid, regionid, txtProductID.Text, txtProductName.Text, txtProductDesc.Text, status, studiomproduct);

            //dataGrid1.DataContext = ds.Tables[0];

        }

        private void Search()
        {
            int studiomproduct = 0;
            CallParameter cp = new CallParameter();
            if ((bool)chkStudioM.IsChecked)
            {
                studiomproduct = 1;
            }

            cp.stateid = cr.LoginState;
            cp.regionid = int.Parse(cmbRegion.SelectedValue.ToString());
            cp.status = int.Parse(cmbStatus.SelectedValue.ToString());
            cp.productid = txtProductID.Text;
            cp.productname = txtProductName.Text;
            cp.productdesc = txtProductDesc.Text;
            cp.studiomproduct = studiomproduct;
            MySetting.Default.SearchProductID = txtProductID.Text;
            MySetting.Default.SearchProductName = txtProductName.Text;
            MySetting.Default.SearchProsuctDesc = txtProductDesc.Text;

            imagebox.Visibility = Visibility.Visible;
            backgroundWorker1.RunWorkerAsync(cp);
        }

        private DataSet SearchProductPrice1(int stateid, int regionid,int status, string productid, string productname, string productdesc, int studiomproduct)
        {
            client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet ds = client.SQSAdmin_StudioM_GetProductPrice(stateid, regionid, productid, productname, productdesc, status, studiomproduct);
            client.Close();
            return ds;

        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //SearchProductPrice();
            Search();
        }
        private void cmbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cr.LoadRegion(cr.DefaultStateID);
            cmbRegion.SelectedIndex = 0;
        }

        private void RowDoubleClick(object sender, RoutedEventArgs e)
        {
            Microsoft.Windows.Controls.DataGridRow row1 = (Microsoft.Windows.Controls.DataGridRow)(dataGrid1.ItemContainerGenerator.ContainerFromItem(dataGrid1.SelectedItem));
            DataRowView dr = (DataRowView)row1.Item;
            ProductID = dr["productid"].ToString();
            ProductName = dr["productname"].ToString();
            isStudioMProduct = bool.Parse(dr["isstudiomproduct"].ToString());
            this.Close();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            txtProductDesc.Text = "";
            cmbState.SelectedValue = loginstateid;
            cmbStatus.SelectedValue = 1;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //DataTable dt;
            string plantext = "";
            if (e.Result != null)
            {
                //dt = ((DataSet)e.Result).Tables[0];
                //if ((bool)chkStudioM.IsChecked)
                //{
                //    dt.DefaultView.RowFilter = "isStudioMProduct='True'";
                //}
                DataTable dt = ((DataSet)e.Result).Tables[0];
                dt.Columns.Add("PlanTextQandA", typeof(String));
                foreach (DataRow dr in dt.Rows)
                {
                    plantext = "";
                    XmlDocument doc = new XmlDocument();
                    if (dr["studiomqanda"] != null && dr["studiomqanda"].ToString() != "")
                    {
                        try
                        {
                            doc.LoadXml(dr["studiomqanda"].ToString());

                            XmlNodeList supplierList = doc.SelectNodes("/Brands/Brand");
                            foreach (XmlNode xd in supplierList)
                            {
                                
                                XmlNode idnode = xd.Attributes["id"];
                                XmlNode namenode = xd.Attributes["name"];
                                if (plantext == "")
                                {
                                    plantext = namenode.Value;
                                }
                                else
                                {
                                    plantext = plantext + System.Environment.NewLine + System.Environment.NewLine + namenode.Value;
                                }

                                if (idnode != null && idnode.Value != "")
                                {
                                    XmlNodeList questionList = doc.SelectNodes("/Brands/Brand[@id='" + idnode.Value + "']/Questions/Question");
                                    foreach (XmlNode xd2 in questionList)
                                    {
                                        XmlNode idnode2 = xd2.Attributes["id"];
                                        XmlNode textnode2 = xd2.Attributes["text"];
                                        XmlNode atypenode = xd2.Attributes["type"];
                                        plantext = plantext + System.Environment.NewLine + "    "+textnode2.Value ;
                                        if (idnode2 != null && idnode2.Value != "")
                                        {
                                            XmlNodeList answerList = doc.SelectNodes("/Brands/Brand[@id='" + idnode.Value + "']/Questions/Question[@id='" + idnode2.Value + "']/Answers/Answer");
                                            foreach (XmlNode xd3 in answerList)
                                            {
                                                XmlNode idnode3 = xd3.Attributes["id"];
                                                XmlNode textnode3 = xd3.Attributes["text"];
                                                plantext = plantext + System.Environment.NewLine + "        " + textnode3.Value;
                                            }
                                        }
                                    }
                                }
                            }
                            dr["PlanTextQandA"] = plantext;
                        }
                        catch
                        {
                            dr["PlanTextQandA"] = "";
                        }
                        
                    }
                }

                dataGrid1.DataContext = dt.DefaultView;
                txtrecordcount.Text = dt.Rows.Count.ToString();
                imagebox.Visibility = Visibility.Collapsed;
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
            e.Result = SearchProductPrice1(cp.stateid, cp.regionid, cp.status,cp.productid, cp.productname, cp.productdesc, cp.studiomproduct);
        }

        public class CallParameter
        {
            public int stateid { get; set; }
            public int regionid { get; set; }
            public int status { get; set; }
            public string productid { get; set; }
            public string productname { get; set; }
            public string productdesc { get; set; }
            public int studiomproduct { get; set; }
        }
    }
}
