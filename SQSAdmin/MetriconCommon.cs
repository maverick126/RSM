using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Configuration.Assemblies;

using System.Diagnostics;
using System.IO;
using System.Xml;
using Microsoft.Office.Interop.Excel;


/// <summary>
/// Summary description for MetriconCommon
/// </summary>
public static class MetriconCommon
{

    public static string UserName = "";
    public static string UserCode = "";
    public static string UserState = "1";
    public static string UserStateName = "";
    public static string _windowtitle = "";

#if DEBUG
    private static string sEnvironment = "DEV";
#else
    private static string sEnvironment = "LIVE";
#endif




    public static string Environment
    {
        get { return MetriconCommon.sEnvironment; }
        set
        {
            MetriconCommon.sEnvironment = value;

        }
    }

    public static string State
    {
        get { return MetriconCommon.UserState; }
        set
        {
            MetriconCommon.UserState = value;

        }
    }
    public static string StateName
    {
        get { return MetriconCommon.UserStateName; }
        set
        {
            MetriconCommon.UserStateName = value;

        }
    }

    public static string WindowTitleInfo
    {
        get { return MetriconCommon._windowtitle; }
        set
        {
            MetriconCommon._windowtitle = value;

        }
    }

    private static ServerManager databaseManager;

    public static ServerManager DatabaseManager
    {
        get
        {
            if (databaseManager == null)
                databaseManager = new ServerManager();

            return databaseManager;
        }
        set { MetriconCommon.databaseManager = value; }
    }





    public static string WebDirectory = "";
    public static string GetSettings(string key)
    {
        try
        {

            return ConfigurationSettings.AppSettings.Get(key);

        }
        catch
        {
            return "";
        }

    }
    public static void SetSettings(string key, string value)
    {
        try
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            if (settings[key] == null)
            {
                settings.Add(key, value);
            }
            else
            {
                settings[key].Value = value;
            }
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
        catch (ConfigurationErrorsException)
        {
            Console.WriteLine("Error writing app settings");
        }
    }
    private static void LogError(string source, string message)
    {
        System.Diagnostics.EventLog.WriteEntry(source, message);
    }
    public static void LogToFile(string sIP, string user, string method, string message)
    {
        string source = "IP=" + sIP + "\tADUser=" + user + "\tMethod=" + method;

        string logfolder = GetSettings("LOG_FILE_DIRECTORY").ToString();
        string logFile = logfolder + @"\\" + GetSettings("LOG_FILE").ToString().Replace(".", DateTime.Now.ToString("yyyyMMdd") + ".");


        if (!System.IO.Directory.Exists(logfolder))
        {
            System.IO.Directory.CreateDirectory(logfolder);
        }

        System.IO.TextWriter tw;
        if (System.IO.File.Exists(logFile))
        {
            tw = System.IO.File.AppendText(logFile);
        }
        else
        {
            tw = System.IO.File.CreateText(logFile);
        }

        try
        {

            DateTime dt = DateTime.Now;
            tw.WriteLine(dt.ToString(GetSettings("LOG_DATE_FORMAT")) + "\t" + source + "\t\tMessage=" + message);
            tw.Close();
            tw.Dispose();

            if (Boolean.Parse(GetSettings("LOG_TO_EVENT")))
                LogError(source, message);

        }
        catch (Exception)
        {

            throw;
        }







    }
    public static string CleanField(string aField)
    {
        string newField = "";
        if (aField == string.Empty)
        {
            return "";
        }
        else
        {
            newField = aField.Trim();
            newField = newField.Replace("SELECT='", "");
            newField = newField.Replace("',", "");
            newField = newField.Replace("þ", "");

        }
        return newField;


    }
    public static string CleanEmail(string s)
    {
        if (s.Length > 0)
        {
            string result;
            result = NullToString(s.Substring(0, s.IndexOf('þ')));
            result.Replace("ÿ", "");
            return result;
        }
        else
        {
            return "";
        }
    }
    public static string NullToString(object o)
    {
        if (o == null)
            return "";
        else
            return o.ToString();
    }
    public static string GetLocalPath()
    {
        return AppDomain.CurrentDomain.BaseDirectory;
    }

    public static string DateToString(DateTime? dt)
    {
        if (dt == null)
        {
            return "NULL";
        }
        return "'" + dt.Value.ToString("yyyy-MMM-dd") + "'";


    }

    public static void ToCurrency(ref System.Windows.Forms.TextBox tempTextBox)
    {

        string sCurrencyString = tempTextBox.Text;

        if (string.IsNullOrEmpty(sCurrencyString))
            sCurrencyString = "0";

        double temp = 0;
        try
        {
            temp = double.Parse(sCurrencyString.Replace("$",string.Empty));
        }
        catch
        {
            System.Windows.Forms.MessageBox.Show("Please enter a valid currency.");
            tempTextBox.Focus();
        }
        tempTextBox.Text = string.Format("{0:c}", temp);

    }

    public static void ToDouble(ref System.Windows.Forms.TextBox tempTextBox)
    {

        string sDoubleString = tempTextBox.Text;

        if (string.IsNullOrEmpty(sDoubleString))
            sDoubleString = "0";

        double temp = 0;

        try
        {
            temp = Double.Parse(sDoubleString, System.Globalization.NumberStyles.Currency);

        }
        catch
        {

            System.Windows.Forms.MessageBox.Show("Please enter a valid decimal number.");
            tempTextBox.Focus();


        }
        tempTextBox.Text = temp.ToString("#,###.00");
    }
    /// <summary>
    /// This function was created by Frank Zhao on 29/05/2007.
    /// validate the textbox as an integer
    /// </summary>
    /// <param name="tempTextBox"></param>
    public static void ToInteger(ref System.Windows.Forms.TextBox tempTextBox)
    {

        string sIntegerString = tempTextBox.Text;

        if (string.IsNullOrEmpty(sIntegerString))
            sIntegerString = "0";

        Int32 temp = 0;

        try
        {
            temp = Int32.Parse(sIntegerString, System.Globalization.NumberStyles.Integer);

        }
        catch
        {

            System.Windows.Forms.MessageBox.Show("Please enter a valid integer.");
            tempTextBox.Focus();


        }
        tempTextBox.Text = temp.ToString("#,##0");
    }
    public static int initIntVariables(string inputStr)
    {

        Int32 temp;
        if (inputStr == "")
        {
            temp = 0;
        }
        else
        {
            temp = Int32.Parse(inputStr, System.Globalization.NumberStyles.Integer);
        }
        return temp;
    }
    public static Double initDoubleVariables(string inputStr)
    {

        Double temp;
        if (inputStr == "")
        {
            temp = 0;
        }
        else
        {
            temp = Double.Parse(inputStr, System.Globalization.NumberStyles.Currency);
        }
        return temp;
    }
    public static string getConnectionString()
    {
        string constring = "";

        XmlDocument doc = new XmlDocument();
        doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
        XmlNodeList nodeList = doc.SelectNodes("connectionStrings/conString");

        foreach (XmlNode node in nodeList)
        {
            if (node.SelectSingleNode("server").InnerText == MetriconCommon.Environment)
            {
                constring = node.SelectSingleNode("value").InnerText;
            }
        }
        //string constring = "Data Source=pm-sqlclus05;Initial Catalog=PMO006;Persist Security Info=True;User ID=sqs;Password=password;Connection Timeout=2400;";
        return constring;
    }

    public static string getWcfEndpoint()
    {
        string endpoint = "";

        XmlDocument doc = new XmlDocument();
        doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
        XmlNodeList nodeList = doc.SelectNodes("connectionStrings/conString");

        foreach (XmlNode node in nodeList)
        {
            if (node.SelectSingleNode("server").InnerText == MetriconCommon.Environment)
            {
                endpoint = node.SelectSingleNode("endpoint").InnerText;
            }
        }

        //string endpoint = "http://localhost:51809/SQSAdminService.svc";

        return endpoint;
    }

    public static void GenerateExcel(string formname, string formdirection)
    {
        string flatlistYN = "Y,N";
        string flatliststate = "";
        string flatlistproductcategory = "";
        string flatlistproductcode = "";
        string flatlistdisplaycode = "";
        string flatlistcostcenter = "";
        string flatlistuom = "";
        string flatregion = "";
        string tempname = formname.Substring(0, 1).ToUpper() + formname.Substring(1, formname.Length - 1).ToLower() + "Sheet";
        try
        {
            Application excel = new Application();
            Workbook oWB = excel.Application.Workbooks.Add(true);
            Worksheet oSheet = oWB.ActiveSheet as Worksheet;
            oSheet.Name = tempname;

            //Worksheet oSheet2 = oWB.Sheets.Add(Type.Missing, Type.Missing, 1, Type.Missing) as Worksheet;
            //oSheet2.Name = "sheet2";
            //oSheet2.Select();
            int idxuom = 1;
            int idxpcat = 1;
            int idxpcode = 1;
            int idxdcode = 1;
            int idxccode = 1;
#region product sample sheet
            if (formname.ToUpper() == "PRODUCT")
            {

                // get state 
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });
                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    if (flatliststate == "")
                    {
                        flatliststate = dr["stateabbreviation"].ToString();
                    }
                    else
                    {
                        flatliststate = flatliststate + "," + dr["stateabbreviation"].ToString();
                    }
                }

                // get product category
                dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetProductCategory]", new System.Data.SqlClient.SqlParameter[3]
                            {
					             new System.Data.SqlClient.SqlParameter("@code","")
					            ,new System.Data.SqlClient.SqlParameter("@desc","")
					            ,new System.Data.SqlClient.SqlParameter("@active", 1)
                                 
                            });
 

                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    if (flatlistproductcategory == "")
                    {
                        flatlistproductcategory = dr["dropdown"].ToString();
                    }
                    else
                    {
                        flatlistproductcategory = flatlistproductcategory + "," + dr["dropdown"].ToString();
                    }
                    oSheet.Cells[idxpcat, 27] = dr["dropdown"].ToString();
                    idxpcat++;

                }

                // get product code
                dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetProductCode]", new System.Data.SqlClient.SqlParameter[3]
                            {
					             new System.Data.SqlClient.SqlParameter("@code","")
					            ,new System.Data.SqlClient.SqlParameter("@desc","")
					            ,new System.Data.SqlClient.SqlParameter("@active", 1)
                                 
                            });
   
                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    if (flatlistproductcode == "")
                    {
                        flatlistproductcode = dr["dropdown"].ToString();
                    }
                    else
                    {
                        flatlistproductcode = flatlistproductcode + "," + dr["dropdown"].ToString();
                    }
                    oSheet.Cells[idxpcode, 28] = dr["dropdown"].ToString();
                    idxpcode++;
                }

                // get price display code
                dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetPriceDisplayCode]", new System.Data.SqlClient.SqlParameter[4]
                            {
					             new System.Data.SqlClient.SqlParameter("@code","")
					            ,new System.Data.SqlClient.SqlParameter("@desc","")
					            ,new System.Data.SqlClient.SqlParameter("@active", 1)
                                ,new System.Data.SqlClient.SqlParameter("@forpromotion", "0")
                            });
                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    if (flatlistdisplaycode == "")
                    {
                        flatlistdisplaycode = dr["dropdown"].ToString();
                    }
                    else
                    {
                        flatlistdisplaycode = flatlistdisplaycode + "," + dr["dropdown"].ToString();
                    }

                    oSheet.Cells[idxdcode, 29] = dr["dropdown"].ToString();
                    idxdcode++;
                }

                // get cost center
                dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetProductCostCentreCode]", new System.Data.SqlClient.SqlParameter[3]
                            {
					             new System.Data.SqlClient.SqlParameter("@code","")
					            ,new System.Data.SqlClient.SqlParameter("@desc","")
					            ,new System.Data.SqlClient.SqlParameter("@active", 1)
                                 
                            });

                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    if (flatlistcostcenter == "")
                    {
                        flatlistcostcenter = dr["dropdown"].ToString();
                    }
                    else
                    {
                        flatlistcostcenter = flatlistcostcenter + "," + dr["dropdown"].ToString();
                    }
                    oSheet.Cells[idxccode, 30] = dr["dropdown"].ToString();
                    idxccode++;
                }


                // get UOM
                dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetUOMCode]", new System.Data.SqlClient.SqlParameter[3]
                            {
					             new System.Data.SqlClient.SqlParameter("@code","")
					            ,new System.Data.SqlClient.SqlParameter("@desc","")
					            ,new System.Data.SqlClient.SqlParameter("@active", 1)
                                 
                            });
                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    if (flatlistuom == "")
                    {
                        flatlistuom = dr["dropdown"].ToString();
                    }
                    else
                    {
                        flatlistuom = flatlistuom + "," + dr["dropdown"].ToString();
                    }
                    oSheet.Cells[idxuom, 31] = dr["dropdown"].ToString();
                    idxuom++;
                }

                oSheet.Select();

                int ColumnIndex = 1;
                Range rng;

                // build the column header text;

                excel.Cells[1, ColumnIndex] = "Product ID";
                rng = (Range)excel.Cells[1, 1];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "Product Name";
                rng = (Range)excel.Cells[1, 2];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "Product Description";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;


                excel.Cells[1, ColumnIndex] = "UOM";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;


                excel.Cells[1, ColumnIndex] = "Sort Order";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "Active";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "BC Active";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "Product Category";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "Product Code";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "Price Display Code";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "Cost Centre Code";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "Mini Bill Completed";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "State";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "IsStudioMProduct";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "Internal Description";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

                excel.Cells[1, ColumnIndex] = "Additional Notes";
                rng = (Range)excel.Cells[1, ColumnIndex];
                rng.Font.Bold = true;
                ColumnIndex++;

    
                int rowIndex = 2;

 

                        string forumula = "='" + tempname + "'!AE1:AE" + (idxuom - 1).ToString();

                        var cell6 = (Range)excel.Columns[4];
                        cell6.Validation.Delete();
                        //cell6.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistuom, Type.Missing);
                        cell6.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, forumula, Type.Missing);
                        cell6.Validation.IgnoreBlank = true;
                        cell6.Validation.InCellDropdown = true;

                        ((Range)excel.Cells[1, 4]).Validation.Delete();


                        var cell7 = (Range)excel.Columns[6];
                        cell7.Validation.Delete();
                        cell7.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                        cell7.Validation.IgnoreBlank = true;
                        cell7.Validation.InCellDropdown = true;
                        ((Range)excel.Cells[1, 6]).Validation.Delete();

                        var cell8 = (Range)excel.Columns[7];
                        cell8.Validation.Delete();
                        cell8.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                        cell8.Validation.IgnoreBlank = true;
                        cell8.Validation.InCellDropdown = true;
                        ((Range)excel.Cells[1, 7]).Validation.Delete();


                        forumula = "='" + tempname + "'!AA1:AA" + (idxpcat - 1).ToString();
                        var cell2 = (Range)excel.Columns[8];
                        cell2.Validation.Delete();
                        //cell2.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistproductcategory, Type.Missing);
                        cell2.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, forumula, Type.Missing);
                        cell2.Validation.IgnoreBlank = true;
                        cell2.Validation.InCellDropdown = true;
                        ((Range)excel.Cells[1, 8]).Validation.Delete();

                        forumula = "='" + tempname + "'!AB1:AB" + (idxpcode - 1).ToString();
                        var cell3 = (Range)excel.Columns[9];
                        cell3.Validation.Delete();
                        //cell3.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistproductcode, Type.Missing);
                        cell3.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, forumula, Type.Missing);
                        cell3.Validation.IgnoreBlank = true;
                        cell3.Validation.InCellDropdown = true;
                        ((Range)excel.Cells[1, 9]).Validation.Delete();

                        forumula = "='" + tempname + "'!AC1:AC" + (idxdcode - 1).ToString();
                        var cell4 = (Range)excel.Columns[10];
                        cell4.Validation.Delete();
                        //cell4.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistdisplaycode, Type.Missing);
                        cell4.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, forumula, Type.Missing);
                        cell4.Validation.IgnoreBlank = true;
                        cell4.Validation.InCellDropdown = true;
                        ((Range)excel.Cells[1, 10]).Validation.Delete();

                        forumula = "='" + tempname + "'!AD1:AD" + (idxccode - 1).ToString();
                        var cell5 = (Range)excel.Columns[11];
                        cell5.Validation.Delete();
                        //cell5.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistcostcenter, Type.Missing);
                        cell5.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, forumula, Type.Missing);
                        cell5.Validation.IgnoreBlank = true;
                        cell5.Validation.InCellDropdown = true;

                        ((Range)excel.Cells[1, 11]).Validation.Delete();
        
                        var cell9 = (Range)excel.Columns[12];
                        cell9.Validation.Delete();
                        cell9.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                        cell9.Validation.IgnoreBlank = true;
                        cell9.Validation.InCellDropdown = true;
                        ((Range)excel.Cells[1, 12]).Validation.Delete();

                        var cell = (Range)excel.Columns[13];
                        cell.Validation.Delete();
                        cell.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatliststate, Type.Missing);
                        cell.Validation.IgnoreBlank = true;
                        cell.Validation.InCellDropdown = true;
                        ((Range)excel.Cells[1, 13]).Validation.Delete();

                        var cell10 = (Range)excel.Columns[14];
                        cell10.Validation.Delete();
                        cell10.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                        cell10.Validation.IgnoreBlank = true;
                        cell10.Validation.InCellDropdown = true;
                        ((Range)excel.Cells[1, 14]).Validation.Delete();
                           
            }
#endregion


#region price sample sheet vertical format
            if (formname.ToUpper() == "PRICE" && formdirection.ToUpper() == "VERTICAL")
            {
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_GetRegionForPriceImport]", new System.Data.SqlClient.SqlParameter[1]
                            {
					             new System.Data.SqlClient.SqlParameter("@stateid",MetriconCommon.UserState)                                 
                            });

                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    if (flatregion == "")
                    {
                        flatregion = dr["regionname"].ToString();
                    }
                    else
                    {
                        flatregion = flatregion + "," + dr["regionname"].ToString();
                    }
                }

                int ColumnIndex2 = 1;
                Range rng;

                // build the column header text;

                excel.Cells[1, ColumnIndex2] = "ProductID";
                rng = (Range)excel.Cells[1, ColumnIndex2];
                rng.Font.Bold = true;
                ColumnIndex2++;

                excel.Cells[1, ColumnIndex2] = "RegionName";
                rng = (Range)excel.Cells[1, ColumnIndex2];
                rng.Font.Bold = true;
                ColumnIndex2++;

                excel.Cells[1, ColumnIndex2] = "EffectiveDate";
                rng = (Range)excel.Cells[1, ColumnIndex2];
                rng.Font.Bold = true;
                ColumnIndex2++;


                excel.Cells[1, ColumnIndex2] = "Costprice";
                rng = (Range)excel.Cells[1, ColumnIndex2];
                rng.Font.Bold = true;
                ColumnIndex2++;

                excel.Cells[1, ColumnIndex2] = "SellPrice";
                rng = (Range)excel.Cells[1, ColumnIndex2];
                rng.Font.Bold = true;
                ColumnIndex2++;

                excel.Cells[1, ColumnIndex2] = "DerivedCost";
                rng = (Range)excel.Cells[1, ColumnIndex2];
                rng.Font.Bold = true;
                ColumnIndex2++;

                int rowIndex2 = 2;

                //excel.Cells[rowIndex2, 1] = "TT-XXX-YYY-000";


                //var cell2 = (Range)excel.Cells[rowIndex2, 2];
                var cell2 = (Range)excel.Columns[2];
                cell2.Validation.Delete();
                cell2.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatregion, Type.Missing);
                cell2.Validation.IgnoreBlank = true;
                cell2.Validation.InCellDropdown = true;
                ((Range)excel.Cells[1, 2]).Validation.Delete();

                //excel.Cells[rowIndex2, 3] = "13/05/2015";
                //excel.Cells[rowIndex2, 4] = "130.05";
                //excel.Cells[rowIndex2, 5] = "195.45";

                //var cell7 = (Range)excel.Cells[rowIndex2, 6];
                var cell7 = (Range)excel.Columns[6];
                cell7.Validation.Delete();
                cell7.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                cell7.Validation.IgnoreBlank = true;
                cell7.Validation.InCellDropdown = true;
                ((Range)excel.Cells[1, 6]).Validation.Delete();

                //excel.Cells[rowIndex2, 7] = "Please delete this sample row which contains the validation of region after complete the form.";
            }
#endregion

#region horizontal format for price
            if (formname.ToUpper() == "PRICE" && formdirection.ToUpper() == "HORIZONTAL")
            {
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_GetRegionForPriceImport]", new System.Data.SqlClient.SqlParameter[1]
                            {
					             new System.Data.SqlClient.SqlParameter("@stateid",MetriconCommon.UserState)                                 
                            });


                int ColumnIndex2 = 1;
                Range rng;

                // build the column header text;

                excel.Cells[1, ColumnIndex2] = "ProductID";
                rng = (Range)excel.Cells[1, ColumnIndex2];
                rng.Font.Bold = true;
                ColumnIndex2++;

                excel.Cells[1, ColumnIndex2] = "HouseType";
                rng = (Range)excel.Cells[1, ColumnIndex2];
                rng.Font.Bold = true;
                ColumnIndex2++;

                excel.Cells[1, ColumnIndex2] = "EffectiveDate";
                rng = (Range)excel.Cells[1, ColumnIndex2];
                rng.Font.Bold = true;
                ColumnIndex2++;


                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    excel.Cells[1, ColumnIndex2] = dr["BCCode"].ToString().TrimStart().TrimEnd() + "_CostPrice";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;
                }

                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    excel.Cells[1, ColumnIndex2] = dr["BCCode"].ToString().TrimStart().TrimEnd() + "_SellPrice";
                    rng = (Range)excel.Cells[1, ColumnIndex2];
                    rng.Font.Bold = true;
                    ColumnIndex2++;
                }

                excel.Cells[1, ColumnIndex2] = "DerivedCost";
                rng = (Range)excel.Cells[1, ColumnIndex2];
                rng.Font.Bold = true;
                ColumnIndex2++;

                int rowIndex2 = 2;
                excel.Cells[rowIndex2, 1] = "TT-XXX-YYY-000";
                excel.Cells[rowIndex2, 2] = "Leave blank if no code";
                excel.Cells[rowIndex2, 3] = "13/05/2015";

                ColumnIndex2 = 4;
                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    excel.Cells[rowIndex2, ColumnIndex2] = "12345.67";
                    ColumnIndex2++;
                }

                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    excel.Cells[rowIndex2, ColumnIndex2] = "154567.99";
                    ColumnIndex2++;
                }

                var cell7 = (Range)excel.Cells[rowIndex2, ColumnIndex2];
                cell7.Validation.Delete();
                cell7.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertInformation, XlFormatConditionOperator.xlBetween, flatlistYN, Type.Missing);
                cell7.Validation.IgnoreBlank = true;
                cell7.Validation.InCellDropdown = true;
                ColumnIndex2++;

                excel.Cells[rowIndex2, ColumnIndex2] = "Please delete this sample row after complete the form.";
            }
#endregion


#region quantity sample sheet
            if (formname.ToUpper() == "QUANTITY")
            {

                Range rng;

                // build the column header text;

                excel.Cells[1, 1] = "HomeProductID";
                rng = (Range)excel.Cells[1, 1];
                rng.Font.Bold = true;

                excel.Cells[1, 2] = "ProductAreaGroupID";
                rng = (Range)excel.Cells[1, 2];
                rng.Font.Bold = true;

                excel.Cells[1, 3] = "Quantity";
                rng = (Range)excel.Cells[1, 3];
                rng.Font.Bold = true;

                int rowIndex = 2;

                excel.Cells[rowIndex, 1] = "QM9ADE33TRAS1";
                excel.Cells[rowIndex, 2] = "12345";
                excel.Cells[rowIndex, 3] = "108.50";
                excel.Cells[rowIndex, 4] = "Sample data row. please delete this row after complete the excel.";
                rng = (Range)excel.Cells[rowIndex, 3];
                rng.NumberFormat = "0.00";

                rowIndex++;

                excel.Cells[rowIndex, 1] = "QM9ADE33TRAS1";
                excel.Cells[rowIndex, 2] = "67890";
                excel.Cells[rowIndex, 3] = "45.00";
                excel.Cells[rowIndex, 4] = "Sample data row. please delete this row after complete the excel.";
                rng = (Range)excel.Cells[rowIndex, 3];
                rng.NumberFormat = "0";



            }
#endregion


            excel.Visible = true;
            oSheet.Columns.AutoFit();
            oSheet.Activate();
            oSheet.get_Range("AA:AA", Type.Missing).EntireColumn.Hidden = true;
            oSheet.get_Range("AB:AB", Type.Missing).EntireColumn.Hidden = true;
            oSheet.get_Range("AC:AC", Type.Missing).EntireColumn.Hidden = true;
            oSheet.get_Range("AD:AD", Type.Missing).EntireColumn.Hidden = true;
            oSheet.get_Range("AE:AE", Type.Missing).EntireColumn.Hidden = true;
            //worksheet.Protect(Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


        }
        catch (Exception ex)
        {
           
        }
    }



}


