using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Threading;
using System.Threading;
using SQSAdmin.Common;

namespace SQSAdmin
{
    public partial class frmPAGWizard : Form
    {
        DataSet dsBrand;
        bool finishedLoadingBrand = false;
        private System.Collections.Hashtable homeIDList;
        string stateID;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private int pagID;
        CallParameter cp;
        ManualResetEvent wh = new ManualResetEvent(false);
        DataTable dtbl;

        public System.Collections.Hashtable HomeIDList
        {
            get { return homeIDList; }
            set { homeIDList = value; }
        }



        public frmPAGWizard()
        {

            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker2 = new BackgroundWorker();
            backgroundWorker2.WorkerSupportsCancellation = true;
            InitializeComponent();
            this.Text = this.Text +  " - " + MetriconCommon.WindowTitleInfo;
            InitializeBackgroundWorker();
            cp = new CallParameter();
        }

        private void frmPAGWizard_Load(object sender, EventArgs e)
        {
            
            LoadPAGDetails();
            LoadBrandList();
            loadStories();
            //LoadHomeList();
            dropBrand_SelectionChangeCommitted(null, null);
            //dropBrand_SelectedIndexChanged(null,null);

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void loadStories()
        {
            if (dtbl == null)
            {
                dtbl = CreateStoriesTable();

                dropstorey.DataSource = dtbl;
                dropstorey.DisplayMember = "storey";
                dropstorey.ValueMember = "id";
                dropstorey.SelectedValue = 0;
            }
        }
        private DataTable CreateStoriesTable()
        {
            DataTable myTab = new DataTable("storeyTab");
            DataColumn dtColumn;

            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Int32");
            dtColumn.ColumnName = "id";
            // Add id Column to the DataColumnCollection.
            myTab.Columns.Add(dtColumn);

            // Create Name column.
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "storey";
            // Add Name column to the table.
            myTab.Columns.Add(dtColumn);

            DataRow myDataRow = myTab.NewRow();
            myDataRow["id"] = 0;
            myDataRow["storey"] = "ALL";
            myTab.Rows.Add(myDataRow);

            myDataRow = myTab.NewRow();
            myDataRow["id"] = 1;
            myDataRow["storey"] = "Single";
            myTab.Rows.Add(myDataRow);

            myDataRow = myTab.NewRow();
            myDataRow["id"] = 2;
            myDataRow["storey"] = "Double";
            myTab.Rows.Add(myDataRow);

            return myTab;
        }
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);

            backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            //BackgroundWorker worker = sender as BackgroundWorker;
            CallParameter cp2 = (CallParameter)e.Argument;

            Save(cp2);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            imagebox.Visible = false;
            MessageBox.Show("PAG successfully configured", "Message");
        }


        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            CallParameter cp3 = (CallParameter)e.Argument;
            //this.imagebox.BeginInvoke((Action)(() =>
            //    {
            //        LoadHomeList_MultiThread(cp2.brandid, cp2.pagid);
            //    }));
            e.Result = LoadHomeList_MultiThread(cp3.brandid, cp3.pagid, cp3.storey);
            backgroundWorker2.CancelAsync();

        }
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                bindingData((DataSet)e.Result);
            }
            imagebox.Visible = false;
        }

        private void LoadBrandList()
        {

            //dsBrand = MetriconCommon.DatabaseManager.ExecuteSQLQuery("SELECT [BrandID], [BrandName] from [Brand] where active=1 order by [BrandName]");
            dsBrand = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetBrand]", new System.Data.SqlClient.SqlParameter[1]
                            {
                                new System.Data.SqlClient.SqlParameter("@stateID",stateID)

                             });
            DataRow dr = dsBrand.Tables[0].NewRow();
            dr["brandid"] = "0";
            dr["brandname"] = "ALL";

            DataRow dr2 = dsBrand.Tables[0].NewRow();
            dr2["brandid"] = "99999";
            dr2["brandname"] = "Select Brand";

            //dsBrand.Tables[0].Rows.Add(dr);
            //dsBrand.Tables[0].Rows.Add(dr2);
            dsBrand.Tables[0].Rows.InsertAt(dr, 0);
            dsBrand.Tables[0].Rows.InsertAt(dr2, 0);

            //dsBrand.Tables[0].DefaultView.Sort = "brandname";


            dropBrand.DataSource = dsBrand.Tables[0].DefaultView;
            dropBrand.DisplayMember = "BrandName";
            dropBrand.ValueMember = "BrandID";
            dropBrand.SelectedValue = "99999"; ;

            finishedLoadingBrand = true;
        }


        private void LoadPAGDetails()
        {
            //DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery(@"AdminGetPagDetails " + this.pagID);
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminGetPagDetails", new SqlParameter[1] { new SqlParameter("@pagID", this.pagID) });
            if (dsTemp.Tables[0].Rows.Count == 1)
            {
                DataRow row = dsTemp.Tables[0].Rows[0];
                this.txtProductID.Text = row["ProductID"].ToString();
                this.txtArea.Text = row["AreaName"].ToString();
                this.txtGroup.Text = row["GroupName"].ToString();
                this.txtPAGID.Text = row["ProductAreaGroupID"].ToString();
                this.txtEnterDescription.Text = row["enterDesc"].ToString();
                this.txtInternalDesc.Text = row["internaldescription"].ToString();
                this.txtQuantity.Text = row["quantity"].ToString();
                stateID = row["fkstateID"].ToString();

                if ((bool)row["StandardOption"])
                {
                    this.radStandardOption.Checked = true;
                }
                else
                {
                    this.radStandardOption.Checked = false;
                }

                if ((bool)row["StandardInclusion"])
                {
                    this.radStandardInclusion.Checked = true;
                }
                else
                {
                    this.radStandardInclusion.Checked = false;
                }

                if ((bool)row["ChangeQty"])
                {
                    this.chkChangeQuantity.Checked = true;
                }
                else
                {
                    this.chkChangeQuantity.Checked = false;
                }
                if ((bool)row["AddExtraDesc"])
                {
                    this.chkAddExtraDescription.Checked = true;
                }
                else
                {
                    this.chkAddExtraDescription.Checked = false;
                }
                if ((bool)row["ChangePrice"])
                {
                    this.chkChangePrice.Checked = true;
                }
                else
                {
                    this.chkChangePrice.Checked = false;
                }
            }
        }

        public int PagID
        {
            get { return pagID; }
            set { pagID = value; }
        }
        private void LoadHomeList()
        {
            int brandID = Int32.Parse(dropBrand.SelectedValue.ToString());
            DataSet dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminGetHomeModelAndQuantity", new SqlParameter[2] { new SqlParameter("@pagID", pagID), new SqlParameter("@brandID", brandID) });
            bindingData(dsHome);

        }

        private DataSet LoadHomeList_MultiThread(string brandID, string pagID, string storey)
        {
            //int brandID = Int32.Parse(dropBrand.SelectedValue.ToString());
            DataSet dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminGetHomeModelAndQuantity", new SqlParameter[2] { new SqlParameter("@pagID", pagID), new SqlParameter("@brandID", brandID) });
            return dsHome;
            //bindingData(dsHome);

        }
        private void bindingData(DataSet dsTemp)
        {
            if (dropstorey.SelectedValue.ToString() != "0")
            {
                dsTemp.Tables[0].DefaultView.RowFilter = " stories=" + dropstorey.SelectedValue;
            }
            dataGridView1.DataSource = dsTemp.Tables[0].DefaultView;
            dataGridView1.Columns[0].HeaderText = "";
            dataGridView1.Columns[0].ReadOnly = false;
            dataGridView1.Columns[0].DisplayIndex = 0;
            dataGridView1.Columns[0].Width = 20;

            dataGridView1.Columns[1].HeaderText = "Home Model";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].DisplayIndex = 1;

            dataGridView1.Columns[2].HeaderText = "Quantity";
            dataGridView1.Columns[2].ReadOnly = false;
            dataGridView1.Columns[2].DisplayIndex = 2;

            dataGridView1.Columns[3].HeaderText = "Storey";
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[3].DisplayIndex = 3;

            dataGridView1.Columns[4].HeaderText = "Brand Name";
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[4].DisplayIndex = 4;

            dataGridView1.Columns[5].HeaderText = "BrandID";
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[5].DisplayIndex = 5;

            dataGridView1.Columns[6].HeaderText = "Home Size (sq)";
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[6].DisplayIndex = 6;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {

            foreach (DataGridViewRow lvi in dataGridView1.Rows)
            {
                lvi.Cells[0].Value = chkSelectAll.Checked;
            }
        }


        private void btnSavePAG_Click(object sender, EventArgs e)
        {
            if (dropBrand.SelectedValue.ToString() == "99999")
            {
                MessageBox.Show("Please select brand and homes.");
            }
            else
            {
                GetAllInfo();
                imagebox.Visible = true;
                backgroundWorker1.RunWorkerAsync(cp);
            }
        }
        private void GetAllInfo()
        {
            int chkchangeqty, chkExtraDesc, chkChangPri, chkDisplay;
            int brandID = Int32.Parse(dropBrand.SelectedValue.ToString());

            if (chkChangeQuantity.Checked)
            {
                chkchangeqty = 1;
            }
            else
            {
                chkchangeqty = 0;
            }
            if (chkAddExtraDescription.Checked)
            {
                chkExtraDesc = 1;
            }
            else
            {
                chkExtraDesc = 0;
            }
            if (chkChangePrice.Checked)
            {
                chkChangPri = 1;
            }
            else
            {
                chkChangPri = 0;
            }
            if (chkdisplayhome.Checked)
            {
                chkDisplay = 1;
            }
            else
            {
                chkDisplay = 0;
            }
            //Construct the HomeIDArrayString

            System.Text.StringBuilder homeIDArrayString = new StringBuilder();
            System.Text.StringBuilder qtyArrayString = new StringBuilder();
            System.Text.StringBuilder brandidArrayString = new StringBuilder();
            System.Text.StringBuilder actionString = new StringBuilder();

            foreach (DataGridViewRow lvi in dataGridView1.Rows)
            {
                homeIDArrayString.Append(lvi.Cells[1].Value.ToString());
                homeIDArrayString.Append(",");

                qtyArrayString.Append(lvi.Cells[2].Value.ToString());
                qtyArrayString.Append(",");

                brandidArrayString.Append(lvi.Cells[5].Value.ToString());
                brandidArrayString.Append(",");

                if ((bool)(lvi.Cells[0].Value))
                {
                    actionString.Append("1,");
                }
                else
                {
                    actionString.Append("0,");
                }
            }


            homeIDArrayString.Remove(homeIDArrayString.Length - 1, 1);
            qtyArrayString.Remove(qtyArrayString.Length - 1, 1);
            brandidArrayString.Remove(brandidArrayString.Length - 1, 1);
            actionString.Remove(actionString.Length - 1, 1);
                //MessageBox.Show(homeIDArrayString.ToString());


            string homeid = homeIDArrayString.ToString();
            string qty = qtyArrayString.ToString();
            string brandid = brandidArrayString.ToString();
            string action = actionString.ToString();

            cp.AddExtraDesc = chkExtraDesc.ToString();
            cp.EnterDesc = txtEnterDescription.Text;
            cp.brandid = brandid;
            cp.ChangePrice = chkChangPri.ToString();
            cp.changeqty = chkchangeqty.ToString();
            cp.homeid = homeid;
            cp.pagid = txtPAGID.Text;
            cp.quantity = qty;
            cp.UserCode = MetriconCommon.UserCode;
            cp.action = action;

        }

        private void Save(CallParameter cpp)
        {

            MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminAddHDO2", new SqlParameter[15] 
				{
                   

					new SqlParameter("@ProductAreaGroupID", cpp.pagid)
					, new SqlParameter("@StandardInclusion", 0)
					, new SqlParameter("@StandardOption", 1)
					, new SqlParameter("@GeneralOption", 0)
					, new SqlParameter("@qtyArrayString", cpp.quantity)
					, new SqlParameter("@EnterDesc", cpp.EnterDesc)
					, new SqlParameter("@ChangeQty", cpp.changeqty)
					, new SqlParameter("@AddExtraDesc", cpp.AddExtraDesc)
					, new SqlParameter("@ChangePrice", cpp.ChangePrice)
					, new SqlParameter("@CreatedBy", cpp.UserCode)
					, new SqlParameter("@HomeIDArrayString", cpp.homeid)
                    , new SqlParameter("@applychangetodisplay","0")
                    , new SqlParameter("@brandID",cpp.brandid)
                    , new SqlParameter("@internalDesc","")
                    , new SqlParameter("@action",cpp.action)
				});

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lstHome_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            MetriconCommon.ToDouble(ref txtQuantity);
        }

        private void txtQuantity_Enter(object sender, EventArgs e)
        {
            MetriconCommon.ToDouble(ref txtQuantity);
        }

        private void btndefault_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text.ToString() != "")
            {
                foreach (DataGridViewRow lvi in dataGridView1.Rows)
                {
                    lvi.Cells[2].Value = txtQuantity.Text.ToString();
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.");
            }
        }


        public class CallParameter
        {
            public string pagid { get; set; }
            public string brandid { get; set; }
            public string quantity { get; set; }
            public string action { get; set; }
            public string EnterDesc { get; set; }
            public string changeqty { get; set; }
            public string AddExtraDesc { get; set; }
            public string ChangePrice { get; set; }
            public string UserCode { get; set; }
            public string homeid { get; set; }
            public string storey { get; set; }

        }

        private void dropBrand_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dropBrand.SelectedIndex != -1 && finishedLoadingBrand)
            {

                //LoadHomeList();
                CallParameter cp = new CallParameter();
                cp.brandid = dropBrand.SelectedValue.ToString();
                cp.pagid = pagID.ToString();
                cp.storey = dropstorey.SelectedValue.ToString();
                this.chkSelectAll.Checked = false;

                imagebox.BackColor = Color.Transparent;
                imagebox.Parent = dataGridView1;
                imagebox.Visible = true;

                if (!backgroundWorker2.IsBusy)
                {
                    backgroundWorker2.RunWorkerAsync(cp);
                }
            }
        }

        private void dropstorey_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (dropstorey.SelectedIndex != -1)
            {

                //LoadHomeList();
                CallParameter cp = new CallParameter();
                cp.brandid = dropBrand.SelectedValue.ToString();
                cp.pagid = pagID.ToString();
                cp.storey = dropstorey.SelectedValue.ToString();
                this.chkSelectAll.Checked = false;

                imagebox.BackColor = Color.Transparent;
                imagebox.Parent = dataGridView1;
                imagebox.Visible = true;

                if (!backgroundWorker2.IsBusy)
                {
                    backgroundWorker2.RunWorkerAsync(cp);
                }
            }
        }

    }
}