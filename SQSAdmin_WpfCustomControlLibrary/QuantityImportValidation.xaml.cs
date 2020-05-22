using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data.OleDb;
using System.Data;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.ServiceModel;
using SQSAdmin_WpfCustomControlLibrary.SQSAdminWCFService;
using SQSAdmin_WpfCustomControlLibrary.Common;
using System.Windows.Forms.Integration;
using System.ComponentModel;

namespace SQSAdmin_WpfCustomControlLibrary
{
    /// <summary>
    /// Interaction logic for QuantityImportValidation.xaml
    /// </summary>
    public partial class QuantityImportValidation : Window
    {
        private CommonResource cr;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private int stateid;
        private string usercode;
        private string filename;
        private System.Data.DataTable ExcelTable;

        public QuantityImportValidation(int pstateid, string pusercode)
        {
            stateid = pstateid;
            usercode = pusercode;
            backgroundWorker1 = new BackgroundWorker();
            InitializeComponent();
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
            cr = new CommonResource(stateid, 0);
            InitializeBackgroundWorker();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnContinue.IsEnabled = false;
            //imagebox.Visibility = Visibility.Visible;
            //this.loadingimage.Image = global::SQSAdmin_WpfCustomControlLibrary.Properties.Resources.processingImage;

            //try
            //{

            //    CallParameter cp = new CallParameter();
            //    cp.State = stateid.ToString();
            //    cp.UserCode = usercode;
            //    cp.FileName = filename;

            //    if (!backgroundWorker1.IsBusy)
            //    {
            //        backgroundWorker1.RunWorkerAsync(cp);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    btnContinue.IsEnabled = false;
            //}
        }

        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<PriceErrorList> list = new List<PriceErrorList>();
            try
            {
                CallParameter cp3 = (CallParameter)e.Argument;
                ExcelTable = cr.readExcelFileToDataTable(cp3.FileName, "QuantitySheet$");

                if (ExcelTable != null && ExcelTable.Columns.Count == 3 )
                {
                    if (ExcelTable.Columns[0].ColumnName.Trim().ToUpper() != "HOMEPRODUCTID" ||
                                           ExcelTable.Columns[1].ColumnName.Trim().ToUpper() != "PRODUCTAREAGROUPID" ||
                                           ExcelTable.Columns[2].ColumnName.Trim().ToUpper() != "QUANTITY"
                                           )
                    {
                        PriceErrorList l = new PriceErrorList();
                        l.MessageText = "Column name or number does not match standard format.";
                        list.Add(l);
                        e.Result = list;
                    }
                    else
                    {
                        e.Result = IsSourceValidate(ExcelTable);
                    }
                }
                else
                {
                    imagebox.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Can not find correct format sheet to import.");
                    btnContinue.IsEnabled = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                btnContinue.IsEnabled = false;
            }

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                imagebox.Visibility = Visibility.Collapsed;
                List<PriceErrorList> lst = (List<PriceErrorList>)e.Result;
                if (lst.Count > 0)
                {
                    btnContinue.IsEnabled = true;
                    var newlist = lst.OrderBy(x => x.ProductID);
                    ErrorGrid.DataContext = newlist;
                    foreach (PriceErrorList l in lst)
                    {
                        if (!l.goAhead)
                        {
                            btnContinue.IsEnabled = false;
                            break;
                        }
                    }
                    
                    warningmessage.Text = "There are some invalid value in excel. Please fix all error display above, then import again.";

                }
                else
                {
                    btnContinue.IsEnabled = true;
                    warningmessage.Text = "Import file provided is valid, to import product click on “process” button.";
                }
            }
        }

        private List<PriceErrorList> IsSourceValidate(System.Data.DataTable dtable)
        {
            List<PriceErrorList> listerror = new List<PriceErrorList>();

            // step 1 validate product id duplication

            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow dr in dtable.Rows)
                {

                    if (dr["HomeProductID"] == null || dr["HomeProductID"].ToString() == "" 
                        //dr["ProductAreaGroupID"] == null || dr["ProductAreaGroupID"].ToString() == "" ||
                        //dr["Quantity"] == null || dr["Quantity"].ToString() == "" 
                        )
                    {
                        PriceErrorList em = new PriceErrorList();

                        em.ProductID = dr["HomeProductID"].ToString();
                        em.MessageText = @"There are some missing value for this home.";
                        em.goAhead = false;
                        listerror.Add(em);
                    }

                    try
                    {
                        int eff = int.Parse(dr["productareagroupid"].ToString());
                    }
                    catch (Exception ex)
                    {
                        PriceErrorList em = new PriceErrorList();
                        em.ProductID = dr["HomeProductID"].ToString();
                        em.ProductAreaGroupID = dr["productareagroupid"].ToString();
                        em.goAhead = false;
                        em.MessageText = dr["productareagroupid"].ToString() + @" is an invalid PAG. It should be an integer.";
                        listerror.Add(em);
                    }

                    try
                    {
                        double eff2 = double.Parse(dr["Quantity"].ToString());
                    }
                    catch (Exception ex)
                    {
                        PriceErrorList em = new PriceErrorList();
                        em.ProductID = dr["HomeProductID"].ToString();
                        em.ProductAreaGroupID = dr["productareagroupid"].ToString();
                        em.MessageText =dr["Quantity"].ToString()+@" is an invalid quantity. It should be a decimal.";
                        em.goAhead = false;
                        listerror.Add(em);
                    }
                }

                DataSet ds = cr.ValidateQuantity(dtable, usercode);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        PriceErrorList em = new PriceErrorList();
                        em.ProductID = dr["HomeProductID"].ToString();
                        em.ProductAreaGroupID = dr["productareagroupid"].ToString();
                        em.goAhead = true;
                        em.MessageText = "This product (" + dr["ProductID"].ToString() + ") was not configured to home. If you process it will be ignored.";
                        listerror.Add(em);
                    }
                }
            }
            else
            {
                PriceErrorList em = new PriceErrorList();

                em.ProductID = "";
                em.MessageText = @"The excel file is blank.";
                em.goAhead = false;
                listerror.Add(em);
            }

            return listerror;
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataSet ds = cr.ImportQuantity(ExcelTable, usercode);
                MessageBox.Show(ds.Tables[0].Rows[0]["result"].ToString().Replace("<br>", "\n"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            btnContinue.IsEnabled = false;
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            cr.GenerateExcel("Quantity", "");
        }
        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            string message = @"
1. Download quantity import format that is available from system. 

2. Prepare data that you wish to import in the system using the downloadable excel. [Critical that you do not change format of excel, neither alter column position in excel]

3. Click on the “Import quantity” button.

4. System is now performing necessary checks for integrity of the excel document and data. Import will also perform necessary checks for any errors or warning, if there are any they will be displayed on the screen.

5. Quantity import can only be used to update quantity of a product that is already configured to homes.

6. Quantity import cannot be used to configure quantity of a product that is not already configured to homes or make a specific quantity configuration inactive.

7. “Process” button will be enabled if excel can be imported.";

            frmPopUpInformation pop = new frmPopUpInformation(message);
            pop.Show();
        }
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = false;

            System.Windows.Forms.DialogResult result = openFileDialog1.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                if (filename != "")
                {
                    imagebox.Visibility = Visibility.Visible;
                    this.loadingimage.Image = global::SQSAdmin_WpfCustomControlLibrary.Properties.Resources.processingImage;

                    try
                    {

                        CallParameter cp = new CallParameter();
                        cp.State = stateid.ToString();
                        cp.UserCode = usercode;
                        cp.FileName = filename;

                        if (!backgroundWorker1.IsBusy)
                        {
                            backgroundWorker1.RunWorkerAsync(cp);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        } 
    }

}
