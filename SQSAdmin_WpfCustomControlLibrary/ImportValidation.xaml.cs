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

    public partial class ImportValidation : System.Windows.Window
    {
        private CommonResource cr;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private int stateid;
        private bool goahead = false;
        private string usercode;
        private string filename;
        private System.Data.DataTable ExcelTable;
        private System.Data.DataTable ExcelTableFinal;

        public ImportValidation(int pstateid, string pusercode)
        {
            stateid = pstateid;
            usercode = pusercode;

            backgroundWorker1 = new BackgroundWorker();
            InitializeComponent();
            this.Title = this.Title + " - " + CommonVariables.WindowTitleInfo;
            cr = new CommonResource(stateid, 0);
            InitializeBackgroundWorker();
            
        }
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                imagebox.Visibility = Visibility.Collapsed;
                List<ErrorList> lst = (List<ErrorList>)e.Result;
                if (lst.Count > 0)
                {
                    var newlist = lst.OrderBy(x => x.ProductID);
                    ErrorGrid.DataContext = newlist;
                    if (goahead)
                    {
                        btnContinue.IsEnabled = true;
                        warningmessage.Text = "Import file provided is valid, to import product click on “process” button";
                    }
                    else
                    {
                        btnContinue.IsEnabled = false;
                        warningmessage.Text = "There are some invalid value in excel. Please fix all error display below, then import again.";
                    }
                    

                }
                else
                {
                    btnContinue.IsEnabled = true;
                    warningmessage.Text = "Import file provided is valid, to import product click on “process” button";
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<ErrorList> list = new List<ErrorList>();
            ExcelTableFinal = new DataTable();
            try
            {
                CallParameter cp3 = (CallParameter)e.Argument;
                ExcelTable = cr.readExcelFileToDataTable(cp3.FileName, "ProductSheet$");
                while (ExcelTable.Columns.Count > 17)
                {
                    ExcelTable.Columns.RemoveAt(17);
                }


                ExcelTableFinal = ExcelTable.Clone();
                ExcelTable.AsEnumerable().Where(row => row["product id"] != null && row["product id"].ToString()!="").ToList().ForEach(row => ExcelTableFinal.ImportRow(row));

                if (ExcelTableFinal != null)
                {
                    if (ExcelTable.Columns[0].ColumnName.Trim().ToUpper() != "PRODUCT ID" ||
                        ExcelTable.Columns[1].ColumnName.Trim().ToUpper() != "PRODUCT NAME" ||
                        ExcelTable.Columns[2].ColumnName.Trim().ToUpper() != "PRODUCT DESCRIPTION" ||
                        ExcelTable.Columns[3].ColumnName.Trim().ToUpper() != "UOM" ||
                        ExcelTable.Columns[4].ColumnName.Trim().ToUpper() != "SORT ORDER" ||
                        ExcelTable.Columns[5].ColumnName.Trim().ToUpper() != "ACTIVE" ||
                        ExcelTable.Columns[6].ColumnName.Trim().ToUpper() != "BC ACTIVE" ||
                        ExcelTable.Columns[7].ColumnName.Trim().ToUpper() != "PRODUCT CATEGORY" ||
                        ExcelTable.Columns[8].ColumnName.Trim().ToUpper() != "PRODUCT CODE" ||
                        ExcelTable.Columns[9].ColumnName.Trim().ToUpper() != "PRICE DISPLAY CODE" ||
                        ExcelTable.Columns[10].ColumnName.Trim().ToUpper() != "COST CENTRE CODE" ||
                        ExcelTable.Columns[11].ColumnName.Trim().ToUpper() != "MINI BILL COMPLETED" ||
                        ExcelTable.Columns[12].ColumnName.Trim().ToUpper() != "STATE" ||
                        ExcelTable.Columns[13].ColumnName.Trim().ToUpper() != "ISSTUDIOMPRODUCT" ||
                        ExcelTable.Columns[14].ColumnName.Trim().ToUpper() != "INTERNAL DESCRIPTION" ||
                        ExcelTable.Columns[15].ColumnName.Trim().ToUpper() != "ADDITIONAL NOTES" ||
                        ExcelTable.Columns[16].ColumnName.Trim().ToUpper() != "OPERATION ONLY"
                        )
                    {
                        ErrorList l = new ErrorList();
                        l.MessageText = "Column name or number does not match standard format.";
                        list.Add(l);
                        e.Result = list;
                    }
                    else
                    {
                        e.Result = IsSourceValidate(ExcelTableFinal);
                    }
                }
                else
                {
 
                    ErrorList l = new ErrorList();
                    l.MessageText = "The excel is empty.";
                    list.Add(l);
                    e.Result = list;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
       

        private List<ErrorList> IsSourceValidate(System.Data.DataTable dtable)
        {
            bool result = true;
            List<ErrorList> listerror = new List<ErrorList>();

            // step 1 validate product id duplication

            if (dtable.Rows.Count > 0)
            {
                foreach (DataRow dr in dtable.Rows)
                {
                    if (dr["product id"] == null || dr["product id"].ToString() == "" ||
                        dr["uom"] == null || dr["uom"].ToString() == "" ||
                        dr["Product Category"] == null || dr["Product Category"].ToString() == "" ||
                        dr["Product Code"] == null || dr["Product Code"].ToString() == "" ||
                        dr["Price Display Code"] == null || dr["Price Display Code"].ToString() == "" ||
                        dr["Cost Centre Code"] == null || dr["Cost Centre Code"].ToString() == "" ||
                        dr["State"] == null || dr["State"].ToString() == "" ||
                        dr["active"] == null || dr["active"].ToString() == "" ||
                        dr["BC active"] == null || dr["BC active"].ToString() == "" ||
                        dr["Mini Bill Completed"] == null || dr["Mini Bill Completed"].ToString() == "" ||
                        dr["IsStudioMProduct"] == null || dr["IsStudioMProduct"].ToString() == "" ||
                        dr["Sort Order"] == null || dr["Sort Order"].ToString() == "" ||
                        dr["product name"] == null || dr["product name"].ToString() == "" ||
                        dr["product description"] == null || dr["product description"].ToString() == "" ||
                        dr["Operation Only"] == null || dr["Operation Only"].ToString() == ""
                        )
                    {
                        ErrorList em = new ErrorList();

                        em.ProductID = dr["product id"].ToString();
                        em.MessageText =   @"There are some missing value for this product.";
                        listerror.Add(em);
                    }

                    if ((dr["product id"] != null && dr["product id"].ToString() != "") && dr["product id"].ToString().Length > 15)
                    {
                        ErrorList em = new ErrorList();

                        em.ProductID = dr["product id"].ToString();
                        em.MessageText = @"The product ID is over 15 characters in length.";
                        listerror.Add(em);
                    }
                }
                var ss = from dr in dtable.AsEnumerable()
                         group dr by dr["product id"] into newdr
                         where newdr.Count() > 1 
                         select newdr.Key;
                foreach (var row in ss)
                {
                    ErrorList em = new ErrorList();
                    em.ProductID = row.ToString();
                    em.MessageText = "Product ID duplication. Product ID must be unique.";
                    listerror.Add(em);
                }

                // step 2 validate UOM value

                DataSet puom = cr.LoadProductUOM();
                var uomList = from c in puom.Tables[0].AsEnumerable()
                              select c.Field<string>("dropdown");


                var sourceuomlist = from dr in dtable.AsEnumerable()
                                    where dr.Field<string>("uom") != null && dr.Field<string>("uom") != ""
                                    select dr.Field<string>("uom");

                var nonexist = sourceuomlist.Except(uomList);

                var ps = from dr in dtable.AsEnumerable()
                         join nx in nonexist on dr["UOM"] equals nx.ToString()
                         select new
                             {
                                 productid = dr["product id"],
                                 uom = dr["uom"]
                             };

                foreach (var row in ps)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = row.productid.ToString();
                    em.MessageText = @"'" + row.uom + @"' is an invalid value for UOM. Please check sample import file for valid value.";
                    listerror.Add(em);
                }
         

                // step 3 validate product category code

                DataSet dsproductcategoy = cr.LoadProductCategory();
                var pcatList = from c in dsproductcategoy.Tables[0].AsEnumerable()
                               select c.Field<string>("dropdown");


                var sourcepcatlist = from dr in dtable.AsEnumerable()
                                     where dr.Field<string>("Product Category") != null && dr.Field<string>("Product Category") != ""
                                     select dr.Field<string>("Product Category");

                nonexist = sourcepcatlist.Except(pcatList);

                var ps2 = from dr in dtable.AsEnumerable()
                          join nx in nonexist on dr["Product Category"] equals nx.ToString()
                          select new
                          {
                              productid = dr["product id"],
                              pcat = dr["Product Category"]
                          };

                foreach (var row in ps2)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = row.productid.ToString();
                    em.MessageText = @"'" + row.pcat + @"' is an invalid value for product category. Please check sample import file for valid value.";
                    listerror.Add(em);
                }
  
                // step 4 validate product category code

                DataSet dsproductcode = cr.LoadProductCode();
                var pcodeList = from c in dsproductcode.Tables[0].AsEnumerable()
                                select c.Field<string>("dropdown");


                var sourcepcodelist = from dr in dtable.AsEnumerable()
                                      where dr.Field<string>("Product Code") != null && dr.Field<string>("Product Code") != ""
                                      select dr.Field<string>("Product Code");

                nonexist = sourcepcodelist.Except(pcodeList);

                var ps3 = from dr in dtable.AsEnumerable()
                          join nx in nonexist on dr["Product Code"] equals nx.ToString()
                          select new
                          {
                              productid = dr["product id"],
                              pcode = dr["Product Code"]
                          };
 
                foreach (var row in ps3)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = row.productid.ToString();
                    em.MessageText = @"'" + row.pcode + @"' is an invalid value for product code. Please check sample import file for valid value.";
                    listerror.Add(em);
                }
        
                // step 5 validate product category code

                DataSet dsproductdiscode = cr.LoadProductDisplayCode();
                var pdiscodeList = from c in dsproductdiscode.Tables[0].AsEnumerable()
                                   select c.Field<string>("dropdown");


                var sourcepdiscodelist = from dr in dtable.AsEnumerable()
                                         where dr.Field<string>("Price Display Code") != null && dr.Field<string>("Price Display Code") != ""
                                         select dr.Field<string>("Price Display Code");

                nonexist = sourcepdiscodelist.Except(pdiscodeList);

                var ps4 = from dr in dtable.AsEnumerable()
                          join nx in nonexist on dr["Price Display Code"] equals nx.ToString()
                          select new
                          {
                              productid = dr["product id"],
                              pdiscode = dr["Price Display Code"]
                          };
                foreach (var row in ps4)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = row.productid.ToString();
                    em.MessageText = @"'" + row.pdiscode + @"' is an invalid value for product display code. Please check sample import file for valid value.";
                    listerror.Add(em);
                }


                // step 6 validate cost center code

                DataSet dscostcenter = cr.LoadProductCostCenterCode();
                var costcenterList = from c in dscostcenter.Tables[0].AsEnumerable()
                                     select c.Field<string>("dropdown");


                var sourcecostcenterlist = from dr in dtable.AsEnumerable()
                                           where dr.Field<string>("Cost Centre Code") != null && dr.Field<string>("Cost Centre Code") != ""
                                           select dr.Field<string>("Cost Centre Code");

                nonexist = sourcecostcenterlist.Except(costcenterList);

                var ps5 = from dr in dtable.AsEnumerable()
                          join nx in nonexist on dr["Cost Centre Code"] equals nx.ToString()
                          select new
                          {
                              productid = dr["product id"],
                              costcenter = dr["Cost Centre Code"]
                          };

                foreach (var row in ps5)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = row.productid.ToString();
                    em.MessageText = @"'" + row.costcenter + @"' is an invalid value for cost center code. Please check sample import file for valid value.";
                    listerror.Add(em);
                }
   
                // step 7 validate state value

                var stateList = from c in cr.SQSState
                                select c.StateAbbreviation;

                var sourcestatelist = from dr in dtable.AsEnumerable()
                                      where dr.Field<string>("State") != null && dr.Field<string>("State") != ""
                                      select dr.Field<string>("State");

                nonexist = sourcestatelist.Except(stateList);

                var ps6 = from dr in dtable.AsEnumerable()
                          join nx in nonexist on dr["state"] equals nx.ToString()
                          select new
                          {
                              productid = dr["product id"],
                              state = dr["state"]
                          };

                foreach (var row in ps6)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = row.productid.ToString();
                    em.MessageText = @"'" + row.state + @"' is an invalid value for state. Please check sample import file for valid value.";
                    listerror.Add(em);
                }

                // step 8 validate state value

                var sourceactivelist = from dr in dtable.AsEnumerable()
                                       where  dr.Field<string>("active")!=null && dr.Field<string>("active") != "Y" && dr.Field<string>("active") != "N"
                                       select new
                                       {
                                           productid = dr["product id"],
                                           active = dr["active"]
                                       };

                foreach (var row in sourceactivelist)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = row.productid.ToString();
                    em.MessageText = @"'" + row.active + @"' is an invalid value for Active. it should be 'Y' or 'N'.";
                    listerror.Add(em);
                }

                // step 9 validate state value

                var sourcebcactivelist = from dr in dtable.AsEnumerable()
                                         where dr.Field<string>("bc active")!=null && dr.Field<string>("bc active") != "Y" && dr.Field<string>("bc active") != "N"
                                         select new
                                         {
                                             productid = dr["product id"],
                                             bcactive = dr["bc active"]
                                         };

                foreach (var row in sourcebcactivelist)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = row.productid.ToString();
                    em.MessageText = @"'" + row.bcactive + @"' is an invalid value for BCActive. it should be 'Y' or 'N'.";
                    listerror.Add(em);
                }

                // step 10 validate mini bill start value

                var sourceminibilllist = from dr in dtable.AsEnumerable()
                                         where dr.Field<string>("Mini Bill Completed") != null && dr.Field<string>("Mini Bill Completed") != "Y" && dr.Field<string>("Mini Bill Completed") != "N"
                                         select new
                                         {
                                             productid = dr["product id"],
                                             minibill = dr["Mini Bill Completed"]
                                         };

                foreach (var row in sourceminibilllist)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = row.productid.ToString();
                    em.MessageText = @"'" + row.minibill + @"' is an invalid value for Mini Bill Completed. it should be 'Y' or 'N'.";
                    listerror.Add(em);
                }

                // step 10 validate is studio product value

                var sourcestudiomlist = from dr in dtable.AsEnumerable()
                                        where dr.Field<string>("IsStudioMProduct")!=null && dr.Field<string>("IsStudioMProduct") != "Y" && dr.Field<string>("IsStudioMProduct") != "N"
                                        select new
                                        {
                                            productid = dr["product id"],
                                            studiom = dr["IsStudioMProduct"]
                                        };

                foreach (var row in sourcestudiomlist)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = row.productid.ToString();
                    em.MessageText = @"'" + row.studiom + @"' is an invalid value for IsStudioMProduct. it should be 'Y' or 'N'.";
                    listerror.Add(em);
                }

                // step 11 validate operation only value
                var sourceoperationonlylist = from dr in dtable.AsEnumerable()
                                        where dr.Field<string>("Operation Only") != null && dr.Field<string>("Operation Only") != "Y" && dr.Field<string>("Operation Only") != "N"
                                        select new
                                        {
                                            productid = dr["product id"],
                                            operationonly = dr["Operation Only"]
                                        };

                foreach (var row in sourceoperationonlylist)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = row.productid.ToString();
                    em.MessageText = @"'" + row.operationonly + @"' is an invalid value for Operation Only. it should be 'Y' or 'N'.";
                    listerror.Add(em);
                }

                foreach (DataRow drr in dtable.Rows)
                {
                    if (drr["product id"] != null && drr["product id"].ToString() != "")
                    {
                        try
                        {
                            int aa = int.Parse(drr["Sort Order"].ToString());
                        }
                        catch (Exception ex)
                        {
                            ErrorList em = new ErrorList();

                            em.ProductID = drr["Product id"].ToString();
                            em.MessageText = @"Sort order should be a number.";
                            listerror.Add(em);
                        }
                    }
                }

            }
            else
            {
                ErrorList em = new ErrorList();

                em.ProductID = "";
                em.MessageText = @"The excel file is blank.";
                listerror.Add(em);
            }

            //if (listerror.Count > 0)
            //{
            //    var newlist = listerror.OrderBy(x => x.ProductID);
            //    ErrorGrid.DataContext = newlist;
            //    btnContinue.IsEnabled = false;
            //    warningmessage.Text = "There are some invalid value in excel. Please fix all error display above, then import again.";
            //    result = false;
            //}

            //if product exists then give warning only still allow to process

            DataSet ds = ValidateProductSheetWithDatabase(dtable);

            if (ds.Tables[0].Rows.Count > 0)
            {
                goahead = true;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ErrorList em = new ErrorList();

                    em.ProductID = dr["Productid"].ToString();
                    em.MessageText = @"Product exists. If process will overwrite with the data in this spread sheet. If want to keep the current attributes, please remove this product from import sheet.";
                    listerror.Add(em);

                }
            }

            return listerror;
        }

        private DataSet ValidateProductSheetWithDatabase(DataTable dt)
        {
            DataSet ds;
            try
            {
                ds = cr.ValidateProduct(dt);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            DataSet ds = cr.ImportProduct(ExcelTableFinal, usercode);
            MessageBox.Show(ds.Tables[0].Rows[0]["result"].ToString().Replace("<br>", "\n"));
            btnContinue.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnContinue.IsEnabled = false;
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            cr.GenerateExcel("Product", "");
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            openFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog1.FilterIndex = 1;
            ErrorGrid.DataContext = null;

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

        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            string message = @"
1. Download product import format that is available from system.

2. Prepare data that you wish to import in the system using the downloadable excel. [Critical that you do not change format of excel, neither alter column position in excel]

3. Click on the “Import product” button.

4. System is now performing necessary checks for integrity of the excel document and data. Import will also perform necessary checks for any errors or warning, if there are any they will be displayed on the screen.

5. Product import updates information about product if it finds a match and creates a new product if it does not find a matching product. [matching is performed based on Product ID]

6. Product import cannot be used to delete or make a product inactive.

7. “Process” button will be enabled if excel can be imported.";
            
            frmPopUpInformation pop = new frmPopUpInformation(message);
            pop.Show();
        }

    }

    public class ErrorList
    {
        public string ProductID { get; set; }
        public string MessageText { get; set; }
    }
    public class ImportProduct
    {
        public string ProductID { get; set; }
    }
    public class CallParameter
    {
        public string UserCode { get; set; }
        public string State { get; set; }
        public string FileName { get; set; }
        public System.Data.DataTable Data { get; set; }
    }
}
