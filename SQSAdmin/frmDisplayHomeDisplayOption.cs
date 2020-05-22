using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Xml;
using System.Diagnostics;
using SQSAdmin.Common;

namespace SQSAdmin
{
    public partial class frmDisplayHomeDisplayOption : Form
    {
        private frmHomeDisplayLookup homeDisplayLookupForm;
        private DataSet dsAllAreas;
        private DataSet dsAllGroup;
        private System.Collections.Hashtable IDList;
        private string regionid = "1";
        private string state = "Vic";
        private frmDOLPDF frmDOL;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private int highestPercentageReached = 0;
        public string dolpath = "";
        public string displaystate = "";
        public string displayregionid = "";

        public frmDisplayHomeDisplayOption()
        {
            backgroundWorker1 = new BackgroundWorker();
            this.FormClosing += frmDisplayHomeDisplayOption_FormClosing;
            InitializeComponent();
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            InitializeBackgroundWorker();
            IDList = null;
        }
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        private void frmDisplayHomeDisplayOption_Load(object sender, EventArgs e)
        {
            
            this.adminSearchHomeDisplayOptionTableAdapter.Connection.ConnectionString = MetriconCommon.getConnectionString();

            btnSearchDisHomeID_Click(sender, e);
            loadArea();
            loadGroup();
            if (txtDisHomeID.Text.ToString() != "")
            {
                loadPAGList();
                loadPAGList2();
                LoadDisplayHomeDetails();
            }


            this.listView2.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView2_ItemChecked);

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
        private void loadArea()
        {
            if (dsAllAreas == null)
            {
                dsAllAreas = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAllAreas]", new System.Data.SqlClient.SqlParameter[0] { });
            }

            dropArea.ValueMember = "areaid";
            dropArea.DisplayMember = "areaname";
            dropArea.DataSource = dsAllAreas.Tables[0];
            dropArea.SelectedIndex = -1;

            dropArea2.ValueMember = "areaid";
            dropArea2.DisplayMember = "areaname";
            dropArea2.DataSource = dsAllAreas.Tables[0].Copy();
            dropArea2.SelectedIndex = -1;
        }
        private void loadGroup()
        {
            if (dsAllGroup == null)
                dsAllGroup = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAllGroups]", new System.Data.SqlClient.SqlParameter[0] { });

            dropGroup.ValueMember = "groupid";
            dropGroup.DisplayMember = "groupname";
            dropGroup.DataSource = dsAllGroup.Tables[0];
            dropGroup.SelectedIndex = -1;

            dropGroup3.ValueMember = "groupid";
            dropGroup3.DisplayMember = "groupname";
            dropGroup3.DataSource = dsAllGroup.Tables[0].Copy();
            dropGroup3.SelectedIndex = -1;
        }

        private void btnSearchDisHomeID_Click(object sender, EventArgs e)
        {
            if (homeDisplayLookupForm == null)
                homeDisplayLookupForm = new frmHomeDisplayLookup();
            homeDisplayLookupForm.ShowDialog();

            if (homeDisplayLookupForm.SelectedHomeDisplay != null && (homeDisplayLookupForm.SelectedHomeDisplay.RowState == DataRowState.Unchanged))
            {
                DataRow row = homeDisplayLookupForm.SelectedHomeDisplay;
                txtDisHomeID.Text = row["HomeID"].ToString();
                txtDisHomeID2.Text = row["HomeID"].ToString();
                txtDisHomeName.Text = row["HomeName"].ToString();
                label3.Text = row["suburb"].ToString();
                label5.Text = row["lotaddress"].ToString();
                label6.Text = row["brandname"].ToString();
            }
        }
        private void LoadDisplayHomeDetails()
        {
            int disHomeID; 
            if (txtDisHomeID.Text.ToString() == "")
            {
                disHomeID = 0;
            }
            else
            {
                disHomeID = Int32.Parse(txtDisHomeID.Text.ToString());
            }
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_Generic_GetDisplayHomeDetails]", new System.Data.SqlClient.SqlParameter[1]
                            {
                                new System.Data.SqlClient.SqlParameter("@disHomeID", disHomeID),
                            }

                          );

            displaystate = dsTemp.Tables[0].Rows[0]["StateAbbreviation"].ToString();
            displayregionid = dsTemp.Tables[0].Rows[0]["regionid"].ToString();
        }

        private void loadPAGList()
        {

            int areaID, groupID, disHomeID, active;

            dataGridView1.ClearSelection();
            if (txtDisHomeID.Text.ToString() == "")
            {
                disHomeID = 0;
            }
            else
            {
                disHomeID = Int32.Parse(txtDisHomeID.Text.ToString());
            }
            if (dropArea.SelectedIndex == -1)
            {
                areaID = 0;
            }
            else
            {
                areaID = Int32.Parse(dropArea.SelectedValue.ToString());
            }
            if (dropGroup.SelectedIndex == -1)
            {
                groupID = 0;
            }
            else
            {
                groupID = Int32.Parse(dropGroup.SelectedValue.ToString());
            }

            if (chkActive.Checked)
            {
                active = 1;
            }
            else
            {
                active = 0;
            }

            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminSearchExistingPAGForDisHome]", new System.Data.SqlClient.SqlParameter[4]
                            {
                                new System.Data.SqlClient.SqlParameter("@disHomeID", disHomeID),
                                new System.Data.SqlClient.SqlParameter("@areaID", areaID),
                                new System.Data.SqlClient.SqlParameter("@groupID", groupID),
                                new System.Data.SqlClient.SqlParameter("@active", active)
                            }

                   );

            bindingData(dsTemp);

        }

        private void bindingData(DataSet dsTemp)
        {
                dataGridView1.DataSource = dsTemp.Tables[0];
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;

                dataGridView1.Columns[2].HeaderText = "PAG ID";
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[2].DisplayIndex = 1;

                dataGridView1.Columns[3].HeaderText = "S-Option";
                dataGridView1.Columns[3].DisplayIndex = 16;
                dataGridView1.Columns[3].ReadOnly = true;

                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;

                dataGridView1.Columns[6].HeaderText = "Quantity";
                dataGridView1.Columns[6].DisplayIndex = 7;
                dataGridView1.Columns[6].DefaultCellStyle.Format = "#####.##";

                dataGridView1.Columns[7].Visible = false;
                dataGridView1.Columns[8].Visible = false;
                dataGridView1.Columns[9].Visible = false;
                dataGridView1.Columns[10].Visible = false;

                dataGridView1.Columns[11].HeaderText = "Active";
                dataGridView1.Columns[11].DisplayIndex = 12;

                dataGridView1.Columns[12].Visible = false;

                dataGridView1.Columns[13].HeaderText = "Change Qty";
                dataGridView1.Columns[13].DisplayIndex = 17;
                dataGridView1.Columns[13].ReadOnly = true;

                dataGridView1.Columns[14].HeaderText = "Add Desc";
                dataGridView1.Columns[14].DisplayIndex = 18;
                dataGridView1.Columns[14].ReadOnly = true;

                dataGridView1.Columns[15].HeaderText = "Extra Desc";
                dataGridView1.Columns[15].DisplayIndex = 15;

                dataGridView1.Columns[16].HeaderText = "Change Price";
                dataGridView1.Columns[16].DisplayIndex = 19;
                dataGridView1.Columns[16].ReadOnly = true;

                dataGridView1.Columns[17].HeaderText = "Area";
                dataGridView1.Columns[17].ReadOnly = true;
                dataGridView1.Columns[17].DisplayIndex = 2;
                dataGridView1.Columns[18].HeaderText = "Group";
                dataGridView1.Columns[18].ReadOnly = true;
                dataGridView1.Columns[18].DisplayIndex = 3;
                dataGridView1.Columns[19].HeaderText = "ProductID";
                dataGridView1.Columns[19].ReadOnly = true;
                dataGridView1.Columns[19].DisplayIndex = 4;
                dataGridView1.Columns[20].HeaderText = "Product Name";
                dataGridView1.Columns[20].ReadOnly = true;
                dataGridView1.Columns[20].DisplayIndex = 5;

                dataGridView1.Columns[21].Visible = false;
                dataGridView1.Columns[22].Visible = false;
                dataGridView1.Columns[23].Visible = false;

                dataGridView1.Columns[24].HeaderText = "Mini Bill Start";
                dataGridView1.Columns[24].ReadOnly = true;
                dataGridView1.Columns[24].DisplayIndex = 24;

                dataGridView1.Columns[25].HeaderText = "Mini Bill Complete";
                dataGridView1.Columns[25].ReadOnly = true;
                dataGridView1.Columns[25].DisplayIndex = 25;

                dataGridView1.Columns[26].HeaderText = "UOM";
                dataGridView1.Columns[26].ReadOnly = true;
                dataGridView1.Columns[26].DisplayIndex = 26;
                dataGridView1.Columns[26].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns[26].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }
        private void loadPAGList2()
        {

            int areaID, groupID, disHomeID;

            if (dropArea2.SelectedIndex == -1)
            {
                areaID = 0;
            }
            else
            {
                areaID = Int32.Parse(dropArea2.SelectedValue.ToString());
            }
            if (dropGroup3.SelectedIndex == -1)
            {
                groupID = 0;
            }
            else
            {
                groupID = Int32.Parse(dropGroup3.SelectedValue.ToString());
            }
            if (txtDisHomeID.Text.ToString() != "")
            {
                disHomeID = Int32.Parse(txtDisHomeID.Text.ToString());
            }
            else
            {
                disHomeID = 0;
            }
            try
            {

                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminSearchPAGForDisHome]", new System.Data.SqlClient.SqlParameter[4]
                            {
                                new System.Data.SqlClient.SqlParameter("@disHomeID", disHomeID),
                                new System.Data.SqlClient.SqlParameter("@productname", txtPagID.Text.ToString()),
                                new System.Data.SqlClient.SqlParameter("@areaID", areaID),
                                new System.Data.SqlClient.SqlParameter("@groupID", groupID)
                            }

                       );
                listView2.Items.Clear();

                ListViewItem lvi;

                foreach (DataRow row in dsTemp.Tables[0].Rows)
                {
                    lvi = new ListViewItem(row["ProductAreaGroupID"].ToString());
                    if (IDList != null)
                        lvi.Checked = IDList.ContainsKey(row["ProductAreaGroupID"].ToString()) ? true : false;

                    lvi.SubItems.Add(row["AreaName"].ToString());
                    lvi.SubItems.Add(row["GroupName"].ToString());
                    lvi.SubItems.Add(row["ProductID"].ToString());
                    lvi.SubItems.Add(row["ProductName"].ToString());
                    lvi.SubItems.Add(row["ProductDescription"].ToString());
                    lvi.SubItems.Add(row["Active"].ToString());
                    lvi.SubItems.Add(row["MiniBillStart"].ToString());
                    lvi.SubItems.Add(row["MiniBillComplete"].ToString());
                    lvi.SubItems.Add(row["UOM"].ToString());
                    lvi.Tag = row;
                    this.listView2.Items.Add(lvi);

                }
            }
            catch (SqlException ex2)
            {
                MessageBox.Show(ex2.ToString());
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Please enter a valid PAG ID " + ex1.ToString());
            }




        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtDisHomeID.Text.ToString() != "")
            {
                loadPAGList();
            }
            else
            {
                MessageBox.Show("Please select a display home!", "Message");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dropDownChange(object sender, EventArgs e)
        {
            loadPAGList2();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {

            foreach (ListViewItem lvi in this.listView2.Items)
            {
                lvi.Checked = chkSelectAll.Checked;
            }

        }

        private void btnSaveToHome_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.dropArea.SelectedIndex = -1;
            this.dropGroup.SelectedIndex = -1;
        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            this.dropArea2.SelectedIndex = -1;
            this.dropGroup3.SelectedIndex = -1;
            this.txtPagID.Text = "";
        }

        private void btnSaveChange_Click(object sender, EventArgs e)
        {

        }

        private void groupAdding_Enter(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            string enterdesc = "";
            int active, active2 ;
            decimal qty;
            qty = 0;
            bool so, si, changeqty, changeprice, addextradesc;


                try
                {
                    if (dataGridView1.Rows.Count != 0)
                    {
                        this.adminSearchHomeDisplayOptionTableAdapter.Connection.ConnectionString = MetriconCommon.getConnectionString();

                        DataRow modifiedRow = ((DataRowView)this.dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;

                        DataSet dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select active from homedisplayoption_Staging where productareagroupid=" + modifiedRow["productareagroupid"].ToString() + " and homeid=" + modifiedRow["homeid"].ToString() + " and homedisplayid=" + modifiedRow["homedisplayid"].ToString());
                        if (dsHome.Tables[0].Rows[0][0].ToString() == "False")
                        {
                            active = 0;
                        }
                        else
                        {
                            active = 1;
                        }
                        if (modifiedRow["active"].ToString() == "True")
                        {
                            active2 = 1;
                        }
                        else
                        {
                            active2 = 0;
                        }
                        //modifiedRow["EnterDesc"] = "Note: Stained finish to KDHW components on this stair are non-standard and cannot be offered." + DateTime.Now.ToLongTimeString();
                        if (modifiedRow["EnterDesc"] != null && modifiedRow["EnterDesc"].ToString() != "")
                        {
                            enterdesc = modifiedRow["EnterDesc"].ToString();
                        }
                        if (modifiedRow["quantity"] != null && modifiedRow["quantity"].ToString() != "")
                        {
                            try
                            {
                                qty = decimal.Parse(modifiedRow["quantity"].ToString());
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Quantity should be a number.");
                                return;
                            }
                        }

                        if (modifiedRow["standardoption"] != null && modifiedRow["standardoption"].ToString().ToUpper() == "TRUE")
                        {
                            so = true;
                        }
                        else
                        {
                            so = false;
                        }
                        if (modifiedRow["standardinclusion"] != null && modifiedRow["standardinclusion"].ToString().ToUpper() == "TRUE")
                        {
                            si = true;
                        }
                        else
                        {
                            si = false;
                        }

                        if (modifiedRow["changeqty"] != null && modifiedRow["changeqty"].ToString().ToUpper() == "TRUE")
                        {
                            changeqty = true;
                        }
                        else
                        {
                            changeqty = false;
                        }

                        if (modifiedRow["addextradesc"] != null && modifiedRow["addextradesc"].ToString().ToUpper() == "TRUE")
                        {
                            addextradesc = true;
                        }
                        else
                        {
                            addextradesc = false;
                        }

                        if (modifiedRow["changeprice"] != null && modifiedRow["changeprice"].ToString().ToUpper() == "TRUE")
                        {
                            changeprice = true;
                        }
                        else
                        {
                            changeprice = false;
                        }
                        if (active != active2)
                        {

                            var confirmResult = MessageBox.Show("Are you sure to delete this item ?",
                                                                 "Confirm Delete!!",
                                                                 MessageBoxButtons.YesNo);
                            if (confirmResult == DialogResult.Yes)
                            {
                                dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminEditHDO]", new System.Data.SqlClient.SqlParameter[12]
                            {
                                new System.Data.SqlClient.SqlParameter("@StandardOption", so),
                                new System.Data.SqlClient.SqlParameter("@GeneralOption",false ),
                                new System.Data.SqlClient.SqlParameter("@StandardInclusion", si),
                                new System.Data.SqlClient.SqlParameter("@Quantity",qty),
                                new System.Data.SqlClient.SqlParameter("@ModifiedDate", DateTime.Now),
                                new System.Data.SqlClient.SqlParameter("@ModifiedBy",MetriconCommon.UserCode),
                                new System.Data.SqlClient.SqlParameter("@Active", active2),
                                new System.Data.SqlClient.SqlParameter("@ChangeQty",changeqty ),
                                new System.Data.SqlClient.SqlParameter("@AddExtraDesc", addextradesc),
                                new System.Data.SqlClient.SqlParameter("@EnterDesc",enterdesc ),
                                new System.Data.SqlClient.SqlParameter("@ChangePrice", changeprice),
                                new System.Data.SqlClient.SqlParameter("@OptionID",Int32.Parse(modifiedRow["optionID"].ToString())),
                            }

                                       );
                                dataGridView1.Rows.RemoveAt(e.RowIndex);
                            }
                            else
                            {
                                modifiedRow["active"] = "True";
                                return;
                            }

                        }
                        else
                        {

                            dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminEditHDO]", new System.Data.SqlClient.SqlParameter[12]
                            {
                                new System.Data.SqlClient.SqlParameter("@StandardOption", so),
                                new System.Data.SqlClient.SqlParameter("@GeneralOption",false ),
                                new System.Data.SqlClient.SqlParameter("@StandardInclusion", si),
                                new System.Data.SqlClient.SqlParameter("@Quantity",qty),
                                new System.Data.SqlClient.SqlParameter("@ModifiedDate", DateTime.Now),
                                new System.Data.SqlClient.SqlParameter("@ModifiedBy",MetriconCommon.UserCode),
                                new System.Data.SqlClient.SqlParameter("@Active", active2),
                                new System.Data.SqlClient.SqlParameter("@ChangeQty",changeqty ),
                                new System.Data.SqlClient.SqlParameter("@AddExtraDesc", addextradesc),
                                new System.Data.SqlClient.SqlParameter("@EnterDesc",enterdesc ),
                                new System.Data.SqlClient.SqlParameter("@ChangePrice", changeprice),
                                new System.Data.SqlClient.SqlParameter("@OptionID",Int32.Parse(modifiedRow["optionID"].ToString())),
                            }

                                   );
                        }

                    }
                }
                catch (IndexOutOfRangeException ex1)
                {
                    //MessageBox.Show(ex1.Message.ToString());
                }
                catch (Exception ex2)
                {
                    MessageBox.Show(ex2.Message.ToString());
                }
        }



        private void listView2_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            string pagID, disHomeID;
            ListView.CheckedListViewItemCollection checkedItems;
            ListViewItem lvi = e.Item;
            if (lvi.Checked)
            {
                checkedItems = listView2.CheckedItems;
                foreach (ListViewItem item in checkedItems)
                {
                    if (!item.Equals(lvi))
                        item.Checked = false;
                }

                DataRow row = (DataRow)lvi.Tag;
                pagID = row["productAreaGroupID"].ToString();
                disHomeID = this.txtDisHomeID.Text.ToString();
                getQuantity(pagID, disHomeID);
            }


        }
        private void getQuantity(string pid, string disID)
        {
            DataSet ds;

            ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select quantity from homedisplayoption_Staging where homeID in ( select parenthomeID from home where homeID=" + disID + ") and homedisplayid is null and productareagroupid=" + pid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.txtQuantity.Text = ds.Tables[0].Rows[0][0].ToString();
                ds.Dispose();
            }
            else
            {
                this.txtQuantity.Text = "1";
            }
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            loadPAGList2();
        }

        private void btnAddtoDisplay_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder pagIDArrayString = new StringBuilder();


            foreach (ListViewItem lvi in this.listView2.Items)
            {
                if (lvi.Checked)
                {
                    DataRow row = (DataRow)lvi.Tag;
                    pagIDArrayString.Append(row["ProductAreaGroupID"].ToString());
                    pagIDArrayString.Append(",");
                }
            }
            if (pagIDArrayString.Length > 0)
            {
                pagIDArrayString.Remove(pagIDArrayString.Length - 1, 1);

                MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminAddOptionsToDisHome", new SqlParameter[11] 
                {
                      new SqlParameter("@disHomeID", Int32.Parse(txtDisHomeID.Text.ToString()))
                    , new SqlParameter("@StandardInclusion", this.radStandardInclusion.Checked)
                    , new SqlParameter("@StandardOption", this.radStandardOption.Checked)
                    , new SqlParameter("@GeneralOption", this.radGeneralOption.Checked)
                    , new SqlParameter("@Quantity", txtQuantity.Text)
                    , new SqlParameter("@EnterDesc", txtEnterDescription.Text)
                    , new SqlParameter("@ChangeQty", chkChangeQuantity.Checked)
                    , new SqlParameter("@AddExtraDesc", chkAddExtraDescription.Checked)
                    , new SqlParameter("@ChangePrice", chkChangePrice.Checked)
                    , new SqlParameter("@CreatedBy", MetriconCommon.UserCode)
                    , new SqlParameter("@pagIDArrayString", pagIDArrayString.ToString())
                });
                loadPAGList();
                loadPAGList2();
            }

        }

        private void btnPrintDOL_Click(object sender, EventArgs e)
        {
            //pictureBox1.BackColor = Color.Transparent;
            //pictureBox1.Parent = dataGridView1;
            //pictureBox1.Visible = true;
            
            //backgroundWorker1.RunWorkerAsync();
            string urlstring = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
            XmlNodeList nodeList = doc.SelectNodes("connectionStrings/conString");
            foreach (XmlNode node in nodeList)
            {
                if (node.SelectSingleNode("server").InnerText == MetriconCommon.Environment)
                {
                    urlstring = node.SelectSingleNode("DOLProntLink").InnerText;
                }
            }

            urlstring=urlstring+@"?DOLHOMEID="+txtDisHomeID.Text+@"&Regionid="+ displayregionid +@"&statecode="+displaystate;
            ProcessStartInfo sInfo = new ProcessStartInfo(urlstring);
            Process.Start(sInfo);

        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string pdfname = "";
            this.pictureBox1.Visible = false;
            pdfname = e.Result.ToString();

            if (File.Exists(pdfname))
            {
                if (frmDOL != null)
                {
                    frmDOL = null;
                }
                frmDOL = new frmDOLPDF(pdfname);
                frmDOL.ShowDialog();
            }
            else
            {
                MessageBox.Show("DOL is NOT found!");
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            e.Result = CreatePDF(worker, e);
        }
        private string CreatePDF(BackgroundWorker worker, DoWorkEventArgs e)
        {
            int disHomeID;
            string pdfname = "";
            try
            {
                if (txtDisHomeID.Text.ToString() == "")
                {
                    disHomeID = 0;
                }
                else
                {
                    disHomeID = Int32.Parse(txtDisHomeID.Text.ToString());
                }

            }
            catch (SqlException sqlex)
            {
                MessageBox.Show(sqlex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return pdfname;
        }

        public DataSet GetDOLBrandRegionAndHomeNameForPrint(int dolhomeid)
        {
            DataSet DOLHPBHDS = MetriconCommon.DatabaseManager.ExecuteSQLQuery("sp_GetDOLBrandRegionAndHomeNameForPrint", new SqlParameter[1] 
                {
                      new SqlParameter("@DOLHomeid", dolhomeid)
                });

            return DOLHPBHDS;
        }
        public string PDFHeader(int DOLHomeID)
        {
            string HouseName = ""; string LotAddress = ""; string estate = "";
            string Brand = "";
            string path = "";

            XmlDocument doc = new XmlDocument();
            doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
            XmlNodeList nodeList = doc.SelectNodes("connectionStrings/PDFImagePath");
            foreach (XmlNode node in nodeList)
            {
                path = @"" + node.SelectSingleNode("path").InnerText;
            }

            XmlNodeList nodeList2 = doc.SelectNodes("connectionStrings/DOLPDFPath");
            foreach (XmlNode node in nodeList2)
            {
                dolpath = @"" + node.SelectSingleNode("path").InnerText;
            }

            DataSet DOLDS = GetDOLBrandRegionAndHomeNameForPrint(DOLHomeID);
            if (DOLDS != null)
            {
                if (DOLDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DOLDR = DOLDS.Tables[0].Rows[0];
                    Brand = DOLDR["brandname"].ToString();
                    HouseName = DOLDR["HomeName"].ToString();
                    LotAddress = DOLDR["address"].ToString();
                    estate = DOLDR["estatename"].ToString();
                    regionid = DOLDR["regionid"].ToString();
                    state = DOLDR["state"].ToString();

                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<html><head><style type='text/css'>
            th {
                font-weight: bold; 
                font-size: 13px; 
                color: #000000;
                font-family: arial; 
                padding-left:2px; 
                padding-right:10px
            }
            td {
                font-family: arial; 
                font-size: 11px;
                padding: 3px;
            }
            </style>");
            sb.Append(@"</head>
            <body style='margin:0px' valign='top'>
            <table border='0' width='100%' cellspacing=0 cellpadding=0>
            <tr bgcolor='#76bdeb' valign='top'>
                <td>
                    <table border='0' style='margin-top:10px; margin-left:10px; color:white'>
                    <tr>
                        <td valign='top' height='190px' style='font-size:18pt' colspan='2'>Displayed Option List</td>
                    </tr>
                    <tr>
                        <td style='font-size:11pt'><b>Catalogue:</b></td><td style='font-size:11pt'>" + Brand + @"</td>
                    </tr>
                    <tr>
                        <td style='font-size:11pt'><b>House Name:</b></td><td style='font-size:11pt'>" + HouseName + @"</td>
                    </tr>
                    <tr>
                        <td style='font-size:11pt'><b>Estate:</b></td><td style='font-size:11pt'>" + estate + @"</td>
                    </tr>
                    <tr>
                        <td style='font-size:11pt'><b>Address:</b></td><td style='font-size:11pt'>" + LotAddress + @"</td>
                    </tr>
                    </table>
                </td>
                <td align=right style='padding:0px'>
                    <img src='" + path + @"' />
                </td>
            </tr>
            </table>");
            return sb.ToString();
        }


        public DataSet GetDOLOptionForHome(int HOME_ID, string Regionid)
        {
            //changing to sp from Inline sql
            DataSet DOLDS = MetriconCommon.DatabaseManager.ExecuteSQLQuery("sp_GetDolOptionStagingForHome", new SqlParameter[2] 
                {
                      new SqlParameter("@homeid", HOME_ID),
                      new SqlParameter("@regionid", Regionid)
                });
            return DOLDS;
        }

        public string ReplaceCRLFByLineBreak(string text)
        {
            StringBuilder newText = new StringBuilder();

            try
            {
                newText.Append(text);

                newText.Replace("\r\n", @"<br />");
                newText.Replace("\n", @"<br />");
            }
            catch
            {
                // Nothing.
            }

            return newText.ToString();
        }
        public string getDOLDisclaimer(string state)
        {
            string result = "";

            DataSet ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spw_getDisclaimerForDOL", new SqlParameter[1] 
                {
                      new SqlParameter("@state", state)
                });

            if (ds.Tables[0].Rows.Count == 1)
            {
                result = ds.Tables[0].Rows[0]["agreement"].ToString();
            }

            return result;
        }

        private void frmDisplayHomeDisplayOption_Deactivate(object sender, EventArgs e)
        {
           
        }


        private void frmDisplayHomeDisplayOption_FormClosing(object sender, FormClosingEventArgs e) 
        {
            btnSearch_Click(sender, e);
        }

    }
}