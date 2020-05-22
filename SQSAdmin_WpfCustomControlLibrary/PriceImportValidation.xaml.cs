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

    public partial class PriceImportValidation : Window
    {
        private CommonResource cr;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private int stateid;
        private string usercode;
        private string filename;
        private System.Data.DataTable ExcelTable;
        public PriceImportValidation(int pstateid, string pusercode)
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
                List<PriceErrorList> lst = (List<PriceErrorList>)e.Result;
                if (lst.Count > 0)
                {
                    var newlist = lst.OrderBy(x => x.ProductID);
                    ErrorGrid.DataContext = newlist;
                    btnContinue.IsEnabled = false;
                    warningmessage.Text = "There are some invalid value in excel. Please fix all error display below, then import again.";

                }
                else
                {
                    btnContinue.IsEnabled = true;
                    warningmessage.Text = "Import file provided is valid, to import product click on “process” button.";
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            List<PriceErrorList> list = new List<PriceErrorList>();
            SQSAdminServiceClient client = new SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            try
            {

                CallParameter cp3 = (CallParameter)e.Argument;
                ExcelTable = cr.readExcelFileToDataTable(cp3.FileName, "PriceSheet$");

                if (ExcelTable != null && (ExcelTable.Columns.Count == 8 || ExcelTable.Columns.Count == 68 || ExcelTable.Columns.Count == 16))
                {
                    if (ExcelTable.Columns.Count == 8)// vertical format
                    {
                        if (ExcelTable.Columns[0].ColumnName.Trim().ToUpper() != "PRODUCTID" ||
                            ExcelTable.Columns[1].ColumnName.Trim().ToUpper() != "REGIONNAME" ||
                            ExcelTable.Columns[2].ColumnName.Trim().ToUpper() != "EFFECTIVEDATE" ||
                            ExcelTable.Columns[3].ColumnName.Trim().ToUpper() != "DIRECTBUILDCOST" ||
                            ExcelTable.Columns[4].ColumnName.Trim().ToUpper() != "BUILDINGTRANSFERCOST" ||
                            ExcelTable.Columns[5].ColumnName.Trim().ToUpper() != "SELLPRICE" ||
                            ExcelTable.Columns[6].ColumnName.Trim().ToUpper() != "TARGETMARGINPERCENT" ||
                            ExcelTable.Columns[7].ColumnName.Trim().ToUpper() != "DERIVEDCOST"
                                )
                        {
                            PriceErrorList l = new PriceErrorList();
                            l.MessageText = "Column name or number does not match standard format. Please follow the sample data sheet format.";
                            list.Add(l);
                            e.Result = list;
                        }
                        else
                        {
                            e.Result = IsSourceValidate(ExcelTable);
                        }
                    }
                    else if ((ExcelTable.Columns.Count == 16 && stateid == 2) || (ExcelTable.Columns.Count == 68 && stateid == 3))// NSW/QLD horizontal format 
                    {
                        DataSet dsTemp = client.SQSAdmin_Generic_GetRegionList(stateid);
                        string[] colnamearray = new string[dsTemp.Tables[0].Rows.Count * 4 + 4];
                        int idx = 3;
                        foreach (DataRow dr in dsTemp.Tables[0].Rows)
                        {
                            colnamearray[idx] = dr["BCCode"].ToString().TrimStart().TrimEnd() + "_DIRECTBUILDCOST";
                            colnamearray[idx + dsTemp.Tables[0].Rows.Count] = dr["BCCode"].ToString().TrimStart().TrimEnd() + "_BUILDINGTRANSFERCOST";
                            colnamearray[idx + dsTemp.Tables[0].Rows.Count * 2] = dr["BCCode"].ToString().TrimStart().TrimEnd() + "_TARGETMARGINPERCENT";
                            colnamearray[idx + dsTemp.Tables[0].Rows.Count * 3] = dr["BCCode"].ToString().TrimStart().TrimEnd() + "_SELLPRICE";
                            idx++;
                        }


                        if (ExcelTable.Columns[0].ColumnName.Trim().ToUpper() != "PRODUCTID" ||
                        ExcelTable.Columns[1].ColumnName.Trim().ToUpper() != "HOUSETYPE" ||
                        ExcelTable.Columns[2].ColumnName.Trim().ToUpper() != "EFFECTIVEDATE" ||
                        ExcelTable.Columns[ExcelTable.Columns.Count - 1].ColumnName.Trim().ToUpper() != "DERIVEDCOST")
                        {
                            PriceErrorList l = new PriceErrorList();
                            l.MessageText = "Column name or number does not match standard format.";
                            list.Add(l);
                            e.Result = list;
                        }
                        else
                        {
                            for (int i = 3; i < ExcelTable.Columns.Count - 2; i++)
                            {
                                if (ExcelTable.Columns[i].ColumnName.Trim().ToUpper() != colnamearray[i])
                                {
                                    PriceErrorList l = new PriceErrorList();
                                    l.MessageText = "Column name or number does not match standard format.";
                                    list.Add(l);
                                    e.Result = list;

                                    break;
                                }
                            }
                        }

                        e.Result = IsHorizontalSourceValidate(ExcelTable);

                    }
                    else
                    {
                        PriceErrorList l = new PriceErrorList();
                        l.MessageText = "Column name or number does not match standard format.";
                        list.Add(l);
                        e.Result = list;
                    }
                }
                else
                {
                    PriceErrorList l = new PriceErrorList();
                    l.MessageText = "Excel provided does not match the standard format.";
                    list.Add(l);
                    e.Result = list;
                }

            }
            catch (Exception ex)
            {
                PriceErrorList l = new PriceErrorList();
                l.MessageText = ex.Message;
                list.Add(l);
                e.Result = list;
            }

        }

        private List<PriceErrorList> IsSourceValidate(System.Data.DataTable dtable)
        {
            List<PriceErrorList> listerror = new List<PriceErrorList>();
            // step 1 validate product id duplication

            if (dtable.Rows.Count > 0)
            {

                // step 1 validate region value
                var regionList = from c in cr.SQSRegion
                                 select c.RegionName;


                var sourceregionlist = from dr in dtable.AsEnumerable()
                                       select dr.Field<string>("regionname");

                var nonexist = sourceregionlist.Except(regionList);

                var ps = from dr in dtable.AsEnumerable()
                         join nx in nonexist on dr["regionname"] equals nx.ToString()
                         select new
                         {
                             productid = dr["productid"],
                             region = dr["regionname"],
                             effectivedate = dr["effectivedate"]
                         };

                foreach (var row in ps)
                {
                    PriceErrorList em = new PriceErrorList();

                    em.ProductID = row.productid.ToString();
                    em.Region = row.region.ToString();

                    DateTime tempdt;
                    try
                    {
                        tempdt = DateTime.Parse(row.effectivedate.ToString());
                        em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                    }
                    catch (Exception exx2)
                    {
                        tempdt = DateTime.MinValue;
                        em.EffectiveDateString = "";
                    }

                    em.EffectiveDate = tempdt;
                    em.EffectiveDateString = (DateTime.Parse(row.effectivedate.ToString())).ToString("dd/MM/yyyy");
                    em.MessageText = row.region.ToString() + @" is an invalid price region name. Please check sample import file for valid value.";
                    listerror.Add(em);
                }

                // step 2 validate costprice


                foreach (DataRow dr in dtable.Rows)
                {

                    if (dr["productid"] == null || dr["productid"].ToString() == "" ||
                        dr["regionname"] == null || dr["regionname"].ToString() == "" ||
                        dr["DerivedCost"] == null || dr["DerivedCost"].ToString() == ""
                        )
                    {
                        PriceErrorList em = new PriceErrorList();

                        DateTime tempdt;
                        try
                        {
                            tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                            em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                        }
                        catch (Exception exx2)
                        {
                            tempdt = DateTime.MinValue;
                            em.EffectiveDateString = "";
                        }
                        em.ProductID = dr["productid"].ToString();
                        em.EffectiveDate = tempdt;

                        em.MessageText = @"There are some missing value for this product.";
                        listerror.Add(em);
                    }

                    try
                    {
                        double cost = double.Parse(dr["DirectBuildCost"].ToString().Replace("$", "").Replace(",", ""));
                    }
                    catch (Exception ex)
                    {
                        PriceErrorList em = new PriceErrorList();
                        em.ProductID = dr["productid"].ToString();
                        em.Region = dr["regionname"].ToString();

                        DateTime tempdt;
                        try
                        {
                            tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                            em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                        }
                        catch (Exception exx2)
                        {
                            tempdt = DateTime.MinValue;
                            em.EffectiveDateString = "";
                        }
                        em.EffectiveDate = tempdt;

                        em.MessageText = @"Direct Build Cost " + dr["DirectBuildCost"].ToString() + @" is invalid. Please enter a number.";
                        listerror.Add(em);
                    }

                }

                // step 3 validate sell price

                foreach (DataRow dr in dtable.Rows)
                {

                    try
                    {
                        double sellprice = double.Parse(dr["SellPrice"].ToString().Replace("$", "").Replace(",", ""));
                    }
                    catch (Exception ex)
                    {
                        PriceErrorList em = new PriceErrorList();
                        em.ProductID = dr["productid"].ToString();
                        em.Region = dr["regionname"].ToString();

                        DateTime tempdt;
                        try
                        {
                            tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                            em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                        }
                        catch (Exception exx2)
                        {
                            tempdt = DateTime.MinValue;
                            em.EffectiveDateString = "";
                        }

                        em.EffectiveDate = tempdt;
                        em.MessageText = @"Sell price " + dr["sellprice"].ToString() + @" is invalid. Please enter a number.";
                        listerror.Add(em);
                    }
                }

                // step 4 validate derived cost

                foreach (DataRow dr in dtable.Rows)
                {

                    if (dr["derivedcost"].ToString().ToUpper() != "Y" && dr["derivedcost"].ToString().ToUpper() != "N")
                    {
                        PriceErrorList em = new PriceErrorList();
                        em.ProductID = dr["productid"].ToString();
                        em.Region = dr["regionname"].ToString();

                        DateTime tempdt;
                        try
                        {
                            tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                            em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                        }
                        catch (Exception exx2)
                        {
                            tempdt = DateTime.MinValue;
                            em.EffectiveDateString = "";
                        }

                        em.EffectiveDate = tempdt;

                        em.MessageText = @"Derived cost " + dr["derivedcost"].ToString() + @" is invalid. it should be Y or N.";
                        listerror.Add(em);
                    }

                }


                // step 5 validate effective date

                foreach (DataRow dr in dtable.Rows)
                {

                    try
                    {
                        DateTime eff = DateTime.Parse(dr["effectivedate"].ToString());
                    }
                    catch (Exception ex)
                    {
                        PriceErrorList em = new PriceErrorList();
                        em.ProductID = dr["productid"].ToString();
                        em.Region = dr["regionname"].ToString();
                        DateTime tempdt;
                        try
                        {
                            tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                            em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                        }
                        catch (Exception exx2)
                        {
                            tempdt = DateTime.MinValue;
                            em.EffectiveDateString = "";
                        }
                        em.EffectiveDate = tempdt;
                        em.MessageText = "Effective date is invalid.";
                        listerror.Add(em);
                    }

                }

                // step 5.5 validate btp margin
                foreach (DataRow dr in dtable.Rows)
                {

                    if (dr["productid"] == null || dr["productid"].ToString() == "" ||
                        dr["DerivedCost"] == null || dr["DerivedCost"].ToString() == ""
                        )
                    {
                        PriceErrorList em = new PriceErrorList();

                        DateTime tempdt;
                        try
                        {
                            tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                            em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                        }
                        catch (Exception exx2)
                        {
                            tempdt = DateTime.MinValue;
                            em.EffectiveDateString = "";
                        }
                        em.ProductID = dr["productid"].ToString();
                        em.EffectiveDate = tempdt;

                        em.MessageText = @"There are some missing value for this product.";
                        listerror.Add(em);
                    }

                    try
                    {
                        double cost = double.Parse(dr["BuildingTransferCost"].ToString().Replace("$", "").Replace(",", ""));
                    }
                    catch (Exception ex)
                    {
                        PriceErrorList em = new PriceErrorList();
                        em.ProductID = dr["productid"].ToString();
                        em.Region = dr["regionname"].ToString();

                        DateTime tempdt;
                        try
                        {
                            tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                            em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                        }
                        catch (Exception exx2)
                        {
                            tempdt = DateTime.MinValue;
                            em.EffectiveDateString = "";
                        }
                        em.EffectiveDate = tempdt;

                        em.MessageText = @"Building Transfer Cost " + dr["BuildingTransferCost"].ToString() + @" is invalid. Please enter a number.";
                        listerror.Add(em);
                    }

                }
                // step 5.5 validate target margin
                foreach (DataRow dr in dtable.Rows)
                {
                    try
                    {
                        double targetmarginpercent = double.Parse(dr["TargetMarginPercent"].ToString());
                    }
                    catch (Exception ex)
                    {
                        PriceErrorList em = new PriceErrorList();
                        em.ProductID = dr["productid"].ToString();
                        em.Region = dr["regionname"].ToString();

                        DateTime tempdt;
                        try
                        {
                            tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                            em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                        }
                        catch (Exception ex5)
                        {
                            tempdt = DateTime.MinValue;
                            em.EffectiveDateString = "";
                        }
                        em.EffectiveDate = tempdt;

                        em.MessageText = @"TargetMarginPercent " + dr["TargetMarginPercent"].ToString() + @" is invalid. Please enter a number.";
                        listerror.Add(em);
                    }

                }
                //step 6 validate duplication based on productid, effectivedate and regionid on excel

                var ss = from c in dtable.AsEnumerable()
                         group c by new
                         {
                             productid = c["productid"],
                             regionname = c["regionname"],
                             effectivedate = c["effectivedate"]
                         } into newdr
                         where newdr.Count() > 1
                         select new
                         {
                             productid = newdr.Key.productid,
                             regionname = newdr.Key.regionname,
                             effectivedate = newdr.Key.effectivedate,
                             totalcout = newdr.Count()
                         };



                foreach (var row in ss)
                {
                    PriceErrorList em = new PriceErrorList();
                    em.ProductID = row.productid.ToString();

                    DateTime tempdt;
                    try
                    {
                        tempdt = (DateTime)row.effectivedate;
                        em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                    }
                    catch (Exception exx2)
                    {
                        tempdt = DateTime.MinValue;
                        em.EffectiveDateString = "";
                    }
                    em.EffectiveDate = tempdt;
                    em.Region = row.regionname.ToString();
                    em.MessageText = row.totalcout.ToString() + " Duplicate records found.";
                    listerror.Add(em);
                }

                // step 7 validate against database to check duplication

                if (listerror.Count == 0)
                {
                    DataSet ds = ValidatePriceSheetWithDatabase(dtable);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            PriceErrorList em = new PriceErrorList();
                            em.ProductID = dr["productid"].ToString();
                            em.Region = dr["regionname"].ToString();

                            DateTime tempdt;
                            try
                            {
                                tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                                em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                            }
                            catch (Exception exx2)
                            {
                                tempdt = DateTime.MinValue;
                                em.EffectiveDateString = "";
                            }

                            em.EffectiveDate = tempdt;
                            em.MessageText = "The price exists based on product, region and effective date. Please either deactivate current price or remove this price from import file and try again.";
                            listerror.Add(em);
                        }
                    }
                }

            }
            else
            {
                PriceErrorList em = new PriceErrorList();

                em.ProductID = "";
                em.MessageText = @"The excel file is blank.";
                listerror.Add(em);
            }

            return listerror;
        }
        private void btnQuestion_Click(object sender, RoutedEventArgs e)
        {
            string message = @"
1. Download price import format that is available from system. 

2. Prepare data that you wish to import in the system using the downloadable excel. [Critical that you do not change format of excel, neither alter column position in excel]

3. Click on the “Import price” button.

4. System is now performing necessary checks for integrity of the excel document and data. Import will also perform necessary checks for any errors or warning, if there are any they will be displayed on the screen.

5. Price import can only be used to create new price.

6. Product import cannot be used to update an existing price or make a specific price inactive.

7. “Process” button will be enabled if excel can be imported";

            frmPopUpInformation pop = new frmPopUpInformation(message);
            pop.Show();
        }
        private DataSet ValidatePriceSheetWithDatabase(DataTable dt)
        {
            DataSet ds;
            string direction = "";
            if (dt != null && dt.Columns.Count == 8)
            {
                direction = "VERTICAL";
            }
            else
            {
                direction = "HORIZONTAL";
            }
            try
            {
                ds = cr.ValidatePrice(dt, usercode, direction);
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private List<PriceErrorList> IsHorizontalSourceValidate(System.Data.DataTable dtable)
        {
            List<PriceErrorList> listerror = new List<PriceErrorList>();
            // step 1 validate product id duplication

            if (dtable.Rows.Count > 0)
            {

                if (stateid == 3 || stateid == 2)
                {

                    foreach (DataRow dr in dtable.Rows)
                    {
                        #region validate productid
                        if (dr["productid"] == null || dr["productid"].ToString() == "")
                        {
                            PriceErrorList em = new PriceErrorList();
                            em.ProductID = "";
                            em.MessageText = @"ProductID is missing.";
                            DateTime tempdt;
                            try
                            {
                                tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                                em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                            }
                            catch (Exception exx2)
                            {
                                tempdt = DateTime.MinValue;
                                em.EffectiveDateString = "";
                            }
                            em.ProductID = dr["productid"].ToString();
                            em.EffectiveDate = tempdt;
                            em.EffectiveDate = tempdt;
                            listerror.Add(em);
                        }
                        if (dr["productid"] != null && dr["productid"].ToString() != "" && dr["productid"].ToString().Length > 15)
                        {
                            PriceErrorList em = new PriceErrorList();
                            em.ProductID = dr["productid"].ToString();
                            em.MessageText = @"Product ID length over 15 characters.";
                            DateTime tempdt;
                            try
                            {
                                tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                                em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                            }
                            catch (Exception exx2)
                            {
                                tempdt = DateTime.MinValue;
                                em.EffectiveDateString = "";
                            }
                            em.EffectiveDate = tempdt;
                            listerror.Add(em);
                        }
                        #endregion
                        #region validate effective date
                        try
                        {
                            DateTime eff = DateTime.Parse(dr["effectivedate"].ToString());
                        }
                        catch (Exception ex)
                        {
                            PriceErrorList em = new PriceErrorList();
                            em.ProductID = dr["productid"].ToString();
                            em.Region = "";
                            DateTime tempdt;
                            try
                            {
                                tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                                em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                            }
                            catch (Exception exx2)
                            {
                                tempdt = DateTime.MinValue;
                                em.EffectiveDateString = "";
                            }
                            em.EffectiveDate = tempdt;

                            em.MessageText = "Effective date is invalid.";
                            listerror.Add(em);
                        }
                        #endregion
                        #region validate derivedcost value
                        if (dr["derivedcost"].ToString().ToUpper() != "Y" && dr["derivedcost"].ToString().ToUpper() != "N")
                        {
                            PriceErrorList em = new PriceErrorList();
                            em.ProductID = dr["productid"].ToString();
                            em.Region = "";
                            DateTime tempdt;
                            try
                            {
                                tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                                em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                            }
                            catch (Exception exx2)
                            {
                                tempdt = DateTime.MinValue;
                                em.EffectiveDateString = "";
                            }
                            em.ProductID = dr["productid"].ToString();
                            em.EffectiveDate = tempdt;
                            em.MessageText = "Derived cost '" + dr["derivedcost"].ToString() + "' is invalid. The value should be Y or N.";
                            listerror.Add(em);
                        }
                        #endregion

                    }

                    #region validate productid
                    string tempname = "";
                    foreach (DataRow dr in dtable.Rows)
                    {
                        #endregion
                        foreach (CommonResource.Region s in cr.SQSRegion)
                        {
                            #region validate direct build cost
                            tempname = s.RegionCode.TrimEnd().TrimStart() + "_DirectBuildCost";
                            try
                            {
                                double cost = double.Parse(dr[tempname].ToString().Replace("$", "").Replace(",", "").TrimEnd().TrimStart());
                            }
                            catch (Exception ex)
                            {
                                PriceErrorList em = new PriceErrorList();
                                em.ProductID = dr["productid"].ToString();
                                em.Region = s.RegionName;
                                DateTime tempdt;
                                try
                                {
                                    tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                                    em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                                }
                                catch (Exception exx2)
                                {
                                    tempdt = DateTime.MinValue;
                                    em.EffectiveDateString = "";
                                }
                                em.ProductID = dr["productid"].ToString();
                                em.EffectiveDate = tempdt;

                                em.EffectiveDate = tempdt;
                                em.MessageText = @"Direct Build Cost " + dr[tempname].ToString() + @" is invalid. Please enter a number.";
                                listerror.Add(em);
                            }
                            #endregion
                            #region validate BTP cost
                            //tempname = s.RegionCode.TrimEnd().TrimStart() + "_CMAPercent";
                            //try
                            //{
                            //    double cmapercent = double.Parse(dr[tempname].ToString().TrimEnd().TrimStart());
                            //}
                            //catch (Exception ex)
                            //{
                            //    PriceErrorList em = new PriceErrorList();
                            //    em.ProductID = dr["productid"].ToString();
                            //    em.Region = s.RegionName;
                            //    DateTime tempdt;
                            //    try
                            //    {
                            //        tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                            //        em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                            //    }
                            //    catch (Exception exx2)
                            //    {
                            //        tempdt = DateTime.MinValue;
                            //        em.EffectiveDateString = "";
                            //    }
                            //    em.ProductID = dr["productid"].ToString();
                            //    em.EffectiveDate = tempdt;

                            //    em.EffectiveDate = tempdt;
                            //    em.MessageText = @"CMA percent " + dr[tempname].ToString() + @" is invalid. Please enter a number.";
                            //    listerror.Add(em);
                            //}
                            tempname = s.RegionCode.TrimEnd().TrimStart() + "_BuildingTransferCost";
                            try
                            {
                                double cost = double.Parse(dr[tempname].ToString().Replace("$", "").Replace(",", "").TrimEnd().TrimStart());
                            }
                            catch (Exception ex)
                            {
                                PriceErrorList em = new PriceErrorList();
                                em.ProductID = dr["productid"].ToString();
                                em.Region = s.RegionName;
                                DateTime tempdt;
                                try
                                {
                                    tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                                    em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                                }
                                catch (Exception exx2)
                                {
                                    tempdt = DateTime.MinValue;
                                    em.EffectiveDateString = "";
                                }
                                em.ProductID = dr["productid"].ToString();
                                em.EffectiveDate = tempdt;

                                em.EffectiveDate = tempdt;
                                em.MessageText = @"Building Transfer Cost " + dr[tempname].ToString() + @" is invalid. Please enter a number.";
                                listerror.Add(em);
                            }
                            #endregion
                            #region validate target margin percent
                            tempname = s.RegionCode.TrimEnd().TrimStart() + "_TargetMarginPercent";
                            try
                            {
                                double targetmarginpercent = double.Parse(dr[tempname].ToString().TrimEnd().TrimStart());
                            }
                            catch (Exception ex)
                            {
                                PriceErrorList em = new PriceErrorList();
                                em.ProductID = dr["productid"].ToString();
                                em.Region = s.RegionName;
                                DateTime tempdt;
                                try
                                {
                                    tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                                    em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                                }
                                catch (Exception exx2)
                                {
                                    tempdt = DateTime.MinValue;
                                    em.EffectiveDateString = "";
                                }
                                em.ProductID = dr["productid"].ToString();
                                em.EffectiveDate = tempdt;

                                em.EffectiveDate = tempdt;
                                em.MessageText = @"Target Margin Percent " + dr[tempname].ToString() + @" is invalid. Please enter a number.";
                                listerror.Add(em);
                            }
                            #endregion
                            #region validate sell price
                            tempname = s.RegionCode.TrimEnd().TrimStart() + "_SellPrice";
                            try
                            {
                                double sell = double.Parse(dr[tempname].ToString().Replace("$", "").Replace(",", "").TrimEnd().TrimStart());
                            }
                            catch (Exception ex)
                            {
                                PriceErrorList em = new PriceErrorList();
                                em.ProductID = dr["productid"].ToString();
                                em.Region = s.RegionName;
                                DateTime tempdt;
                                try
                                {
                                    tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                                    em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                                }
                                catch (Exception exx2)
                                {
                                    tempdt = DateTime.MinValue;
                                    em.EffectiveDateString = "";
                                }
                                em.ProductID = dr["productid"].ToString();
                                em.EffectiveDate = tempdt;

                                em.EffectiveDate = tempdt;
                                em.MessageText = @"Sell price " + dr[tempname].ToString() + @" is invalid. Please enter a number.";
                                listerror.Add(em);
                            }
                            #endregion
                        }

                    }

                    #region  validate duplication based on productid, effectivedate and regionid

                    var ss = from c in dtable.AsEnumerable()
                             group c by new
                             {
                                 productid = c["productid"],
                                 effectivedate = c["effectivedate"]
                             } into newdr
                             where newdr.Count() > 1
                             select new
                             {
                                 productid = newdr.Key.productid,
                                 effectivedate = newdr.Key.effectivedate,
                                 totalcout = newdr.Count()
                             };



                    foreach (var row in ss)
                    {
                        PriceErrorList em = new PriceErrorList();
                        em.ProductID = row.productid.ToString();
                        DateTime tempdt;
                        try
                        {
                            tempdt = (DateTime)row.effectivedate;
                            em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                        }
                        catch (Exception exx2)
                        {
                            tempdt = DateTime.MinValue;
                            em.EffectiveDateString = "";
                        }

                        em.EffectiveDate = tempdt;
                        em.MessageText = row.totalcout.ToString() + " Duplicate records found.";
                        listerror.Add(em);
                    }

                    #endregion
                    #region  validate against database to check duplication

                    if (listerror.Count == 0)
                    {
                        DataTable newformat = cr.ConvertToImportFormatTable(dtable);
                        DataSet ds = ValidatePriceSheetWithDatabase(newformat);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                PriceErrorList em = new PriceErrorList();
                                em.ProductID = dr["productid"].ToString();
                                em.Region = dr["regionname"].ToString();
                                DateTime tempdt;
                                try
                                {
                                    tempdt = DateTime.Parse(dr["effectivedate"].ToString());
                                    em.EffectiveDateString = tempdt.ToString("dd/MM/yyyy");
                                }
                                catch (Exception exx2)
                                {
                                    tempdt = DateTime.MinValue;
                                    em.EffectiveDateString = "";
                                }

                                em.EffectiveDate = tempdt;
                                em.MessageText = "The price exists based on product, region and effective date. Please either deactivate current price or remove this price from import file and try again.";
                                listerror.Add(em);
                            }
                        }
                        //else
                        //{
                        //    PriceErrorList em = new PriceErrorList();

                        //    em.ProductID = "";
                        //    em.MessageText = @"Please log on as QLD or NSW user to import this file.";
                        //    listerror.Add(em);
                        //}
                    }
                    #endregion
                }
                else
                {
                    PriceErrorList em = new PriceErrorList();

                    em.ProductID = "";
                    em.MessageText = @"The excel file is blank.";
                    listerror.Add(em);
                }

            }
            return listerror;
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            string direction = "";
            if (ExcelTable != null && ExcelTable.Columns.Count == 8)
            {
                direction = "VERTICAL";
            }
            else
            {
                direction = "HORIZONTAL";
            }
            try
            {
                DataSet ds = cr.ImportPrice(ExcelTable, usercode, direction);
                MessageBox.Show(ds.Tables[0].Rows[0]["result"].ToString().Replace("<br>", "\n"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            btnContinue.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnContinue.IsEnabled = false;
            if (stateid == 3 || stateid == 2)
            {
                btnDownloadHorizontal.Visibility = Visibility.Visible;
            }
            else
            {
                btnDownloadHorizontal.Visibility = Visibility.Collapsed;
                //btnDownload.Content = "Download Vertical Format";
            }
            //imagebox.Visibility = Visibility.Visible;
            //this.loadingimage.Image = global::SQSAdmin_WpfCustomControlLibrary.Properties.Resources.processingImage;

            //try
            //{

            //        CallParameter cp = new CallParameter();
            //        cp.State = stateid.ToString();
            //        cp.UserCode = usercode;
            //        cp.FileName = filename;
            //        //cp.Data = ExcelTable;

            //        if (!backgroundWorker1.IsBusy)
            //        {
            //            backgroundWorker1.RunWorkerAsync(cp);
            //        }

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    btnContinue.IsEnabled = false;
            //}
        }

        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            cr.GenerateExcel("Price", "VERTICAL");
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

        private void btnDownloadHorizontal_Click(object sender, RoutedEventArgs e)
        {
            cr.GenerateExcel("Price", "HORIZONTAL");
        }
    }

    public class PriceErrorList
    {
        public string ProductID { get; set; }
        public string Region { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string EffectiveDateString { get; set; }
        public string MessageText { get; set; }
        public string ProductAreaGroupID { get; set; }
        public bool goAhead { get; set; }
    }
}
