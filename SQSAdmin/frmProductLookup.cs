using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.IsolatedStorage;
using System.IO;
using System.Xml.Serialization;
using SQSAdmin.Common;

namespace SQSAdmin
{
	public partial class frmProductLookup : Form
       // public partial class frmProductLookup : SQSAdmin.frmLookup
	{

        DataSet dsReg;
        DataSet dsState;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        DataTable dtbl;
        public frmProductLookup()
		{
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker2 = new BackgroundWorker();
			InitializeComponent();
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            InitializeBackgroundWorker();
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
            BackgroundWorker worker = sender as BackgroundWorker;
            CallParameter cp = (CallParameter)e.Argument;

            e.Result = searchproduct(cp.ProductID,cp.ProductName,cp.ProductDescription,cp.regionID,cp.active,cp.minibillstart,cp.minibillcomplete,cp.stateID,cp.studioMproduct,cp.standardinclusion,cp.additionalnote, cp.operationsonly);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                bindingData((DataSet)e.Result);
            }
            imagebox.Visible = false;

            SetDisplayOrder();
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            CallParameter cp = (CallParameter)e.Argument;

            GenerateExcel(cp.ProductID, cp.ProductName, cp.ProductDescription, cp.regionID, cp.active, cp.minibillstart, cp.minibillcomplete, cp.stateID, cp.studioMproduct, cp.standardinclusion, cp.additionalnote, cp.operationsonly);
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            imagebox.Visible = false;
        }
		private DataRow selectedRow;

		public DataRow SelectedRow
		{
			get { return selectedRow; }
			set { selectedRow = value; }
		}

		private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			this.Close();
		}

		private void frmProductLookup_Load(object sender, EventArgs e)
		{
           
            imagebox.BackColor = Color.Transparent;
            imagebox.Parent = dataGridView1;
            CenterImageBox();

            loadStateDropdown();
            loadRegionDropdown();
            loadStatusDropdown();
            if (label6.Text == "")
            {
                dropMiniStart.SelectedIndex = 0;
            }
            else
            {
                dropMiniStart.SelectedIndex = Int32.Parse(label6.Text);
            }
            if (label7.Text == "")
            {
                dropMiniCompleted.SelectedIndex = 0;
            }
            else
            {
                dropMiniCompleted.SelectedIndex = Int32.Parse(label7.Text);
            }


           fillToolStripButton_Click(sender, e);

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);

        }

        private void CenterImageBox()
        {
            imagebox.Location = new Point((imagebox.Parent.ClientSize.Width / 2) - (imagebox.Width / 2), (imagebox.Parent.ClientSize.Height / 2) - (imagebox.Height / 2));
            imagebox.Refresh();
        }
		private void listView1_DoubleClick(object sender, EventArgs e)
		{
			this.Close();
		}

        private void loadRegionDropdown()
        {
           
                dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetRegion]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@stateID", dropdownState.SelectedValue.ToString() )
                            });
                if (dsReg.Tables[0].Rows.Count > 0)
                {
                    dropRegion.DataSource = dsReg.Tables[0];
                    dropRegion.DisplayMember = "regionName";
                    dropRegion.ValueMember = "regionID";
                    if (label9.Text.Trim() != "")
                        dropRegion.SelectedValue = label9.Text;
                    else
                        dropRegion.SelectedIndex = 0;
                }
                else
                {
                    dropRegion.DataSource = null;
                    dropRegion.Items.Clear();
                }

        }

        private void loadStateDropdown()
        {
                dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0]{});
                dropdownState.DataSource = dsState.Tables[0];
                dropdownState.DisplayMember = "stateAbbreviation";
                dropdownState.ValueMember = "stateID";
                dropdownState.SelectedValue = MetriconCommon.State;
                //dropdownState.SelectedIndex = 0;
                //if (label5.Text != "")
                //{
                //    dropdownState.SelectedValue = label5.Text.Trim();
                //}
        }

        private void loadStatusDropdown()
       {
           if (dtbl == null)
           {
               dtbl = CreateStatusTable();

               dropStatus.DataSource = dtbl;
               dropStatus.DisplayMember = "status";
               dropStatus.ValueMember = "id";
               dropStatus.SelectedValue = 1;
           }
        }
        private DataTable CreateStatusTable()
        {
            DataTable myTab = new DataTable("statusTab");
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
            dtColumn.ColumnName = "status";
            // Add Name column to the table.
            myTab.Columns.Add(dtColumn);

            DataRow myDataRow = myTab.NewRow();
            myDataRow["id"] = 2;
            myDataRow["status"] = "All";
            myTab.Rows.Add(myDataRow);

            myDataRow = myTab.NewRow();
            myDataRow["id"] = 1;
            myDataRow["status"] = "Active";
            myTab.Rows.Add(myDataRow);

            myDataRow = myTab.NewRow();
            myDataRow["id"] = 0;
            myDataRow["status"] = "Inactive";
            myTab.Rows.Add(myDataRow);

            return myTab;
        }

        public DataSet searchproduct(
            string productid,
            string productname,
            string productdesc,
            string regionid,
            string active,
            string minibillstart,
            string minibillcomplete,
            string stateid,
            string studiomproduct,
            string standardinclusion,
            string additionalnote,
            string operationsonly
                                    )
        {



            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminSearchProduct]", new System.Data.SqlClient.SqlParameter[12]
                            {
					            new System.Data.SqlClient.SqlParameter("@ProductID",productid)
					            ,new System.Data.SqlClient.SqlParameter("@ProductName",productname)
					            ,new System.Data.SqlClient.SqlParameter("@ProductDescription", productdesc)
					            ,new System.Data.SqlClient.SqlParameter("@regionID",regionid)
					            , new System.Data.SqlClient.SqlParameter("@active",active)
                                , new System.Data.SqlClient.SqlParameter("@minibillstart",minibillstart)
                                , new System.Data.SqlClient.SqlParameter("@minibillcomplete", minibillcomplete)
                                , new System.Data.SqlClient.SqlParameter("@stateID", stateid)
                                , new System.Data.SqlClient.SqlParameter("@studioMproduct", studiomproduct)
                                , new System.Data.SqlClient.SqlParameter("@standardinclusion", standardinclusion)
                                , new System.Data.SqlClient.SqlParameter("@additionalnote", additionalnote)
                                , new System.Data.SqlClient.SqlParameter("@operationsonly", operationsonly)
                            });

            return dsTemp;
        }
        private void search_multiThread()
        {
            CallParameter cp = new CallParameter();
            int active, regionID, stateID, studiomproduct, standardinclusion, operationsonly;
            active = Int32.Parse(dropStatus.SelectedValue.ToString());
            studiomproduct = 0;
            standardinclusion = 0;
            operationsonly = 0;
            if (dropRegion.SelectedValue != null && dropRegion.SelectedValue.ToString() != "")
            {
                regionID = Int32.Parse(dropRegion.SelectedValue.ToString());
            }
            else
            {
                regionID = 9;
            }
            if (chkstudiomproduct.Checked)
            {
                studiomproduct = 1;
            }
            if (chkStandardInclusion.Checked)
            {
                standardinclusion = 1;
            }
            if (chkOperationsOnly.Checked)
            {
                operationsonly = 1;
            }
            stateID = Int32.Parse(dropdownState.SelectedValue.ToString());

            cp.ProductID = productIDToolStripTextBox.Text.ToString();
            cp.ProductName = productNameToolStripTextBox.Text.ToString();
            cp.ProductDescription = productDescriptionToolStripTextBox.Text.ToString();
            cp.regionID = regionID.ToString();
            cp.active = active.ToString();
            cp.minibillstart = dropMiniStart.SelectedIndex.ToString();
            cp.minibillcomplete = dropMiniCompleted.SelectedIndex.ToString();
            cp.stateID = stateID.ToString();
            cp.studioMproduct = studiomproduct.ToString();
            cp.standardinclusion = standardinclusion.ToString();
            cp.additionalnote = txtAdditionalnote.Text.Trim();
            cp.operationsonly = operationsonly.ToString();

            imagebox.BackColor = Color.Transparent;
            imagebox.Parent = dataGridView1;
            imagebox.Visible = true;

            backgroundWorker1.RunWorkerAsync(cp);
        }
		private void fillToolStripButton_Click(object sender, EventArgs e)
		{
            search_multiThread();          
		}
        private void bindingData(DataSet dsTemp)
        {
            dataGridView1.Columns.Clear();

            DataGridViewImageColumn ic = new DataGridViewImageColumn();
            ic.Name = "image";
            dataGridView1.Columns.Add(ic);

            dataGridView1.DataSource = dsTemp.Tables[0];

            dataGridView1.Columns["image"].DisplayIndex = 3;
            dataGridView1.Columns["image"].HeaderText = "";
            dataGridView1.Columns["image"].Width = 20;

            dataGridView1.Columns[1].HeaderText = "Product ID";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].Width=150;

            dataGridView1.Columns[2].HeaderText = "Product Name";
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[2].Width = 200;

            dataGridView1.Columns[3].HeaderText = "Product Description";
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[3].Width = 600;

            dataGridView1.Columns[4].HeaderText = "Sell Price (Inc GST)";
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[4].DefaultCellStyle.Format = "c";
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns[5].HeaderText = "Region";
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns[6].HeaderText = "Active";
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[6].Width = 40;

            dataGridView1.Columns[7].Visible = true;
            dataGridView1.Columns[7].HeaderText = "UOM";
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.Columns[7].Width = 40;

            dataGridView1.Columns[8].Visible = true;
            dataGridView1.Columns[8].HeaderText = "Sort Order";
            dataGridView1.Columns[8].ReadOnly = true;
            dataGridView1.Columns[8].Width = 40;

            dataGridView1.Columns[9].Visible = true;
            dataGridView1.Columns[9].HeaderText = "Effective Date";
            dataGridView1.Columns[9].ReadOnly = true;
            dataGridView1.Columns[9].Width = 70;

            //dataGridView1.Columns[10].Visible = false;
            //dataGridView1.Columns[11].Visible = false;
            //dataGridView1.Columns[12].Visible = false;
            //dataGridView1.Columns[13].Visible = false;
            //dataGridView1.Columns[14].Visible = false;
            
            //dataGridView1.Columns[15].Visible = true;
            //dataGridView1.Columns[15].HeaderText = "Derived Cost";
            //dataGridView1.Columns[15].Width = 60;
            //dataGridView1.Columns[15].ReadOnly = true;

            dataGridView1.Columns[15].Visible = true;
            dataGridView1.Columns[15].HeaderText = "Mini Bill Complete";
            dataGridView1.Columns[15].Width = 60;
            dataGridView1.Columns[15].ReadOnly = true;

            //dataGridView1.Columns[16].Visible = false;
            //dataGridView1.Columns[17].Visible = false;

            dataGridView1.Columns["costprice"].HeaderText = "BTP Cost (Ex GST)";
            dataGridView1.Columns["costprice"].Width = 80;
            dataGridView1.Columns["costprice"].DisplayIndex=4;
            dataGridView1.Columns["costprice"].DefaultCellStyle.Format = "c";
            dataGridView1.Columns["costprice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns["dbccost"].HeaderText = "DBC Cost (Ex GST)";
            dataGridView1.Columns["dbccost"].Width = 80;
            dataGridView1.Columns["dbccost"].DisplayIndex = 5;
            dataGridView1.Columns["dbccost"].DefaultCellStyle.Format = "c";
            dataGridView1.Columns["dbccost"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns["targetmargin"].HeaderText = "Target Margin %";
            dataGridView1.Columns["targetmargin"].Width = 50;
            dataGridView1.Columns["targetmargin"].DisplayIndex = 6;
            dataGridView1.Columns["targetmargin"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["targetmargin"].DefaultCellStyle.Format = "#.00\\%"; ;

            dataGridView1.Columns["sellpriceexgst"].HeaderText = "Sell Price (Ex GST)";
            dataGridView1.Columns["sellpriceexgst"].Width = 80;
            dataGridView1.Columns["sellpriceexgst"].DisplayIndex = 7;
            dataGridView1.Columns["sellpriceexgst"].DefaultCellStyle.Format = "c";
            dataGridView1.Columns["sellpriceexgst"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns["benchmark"].Visible = false;
            dataGridView1.Columns["percentage"].HeaderText = "Benchmark %";
            dataGridView1.Columns["percentage"].Width = 70;

            dataGridView1.Columns["packageflyerpromo"].Visible = false;
            dataGridView1.Columns["standardinclusion"].Visible = false;
            dataGridView1.Columns["studiomqanda"].Visible = false;
            dataGridView1.Columns["imagecount"].Visible = false;
            dataGridView1.Columns["productcategory"].Visible = false;
            dataGridView1.Columns["productcode"].Visible = false;
            dataGridView1.Columns["pricedisplaycode"].Visible = false;
            dataGridView1.Columns["costcentrecode"].Visible = false;
            dataGridView1.Columns["fkstateid"].Visible = false;

            dataGridView1.Columns["BCActive"].Visible = false;
            dataGridView1.Columns["fkproductcategoryid"].Visible = false;
            dataGridView1.Columns["fkproductcodeid"].Visible = false;
            dataGridView1.Columns["fkpricedisplaycodeid"].Visible = false;
            dataGridView1.Columns["fkcostcentrecodeid"].Visible = false;
            dataGridView1.Columns["minibillcomplete"].Visible = false;

            dataGridView1.Columns["defaultQty"].Visible = false;
            dataGridView1.Columns["defaultareaid"].Visible = false;
            dataGridView1.Columns["defaultgroupid"].Visible = false;


            dataGridView1.Columns["marginamount"].HeaderText = "Margin $";
            dataGridView1.Columns["marginamount"].Width = 80;
            dataGridView1.Columns["marginamount"].DisplayIndex = 8;
            dataGridView1.Columns["marginamount"].DefaultCellStyle.Format = "c";
            dataGridView1.Columns["marginamount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns["marginpercent"].HeaderText = "Margin%";
            dataGridView1.Columns["marginpercent"].Width = 50;
            dataGridView1.Columns["marginpercent"].DisplayIndex = 9;
            dataGridView1.Columns["marginpercent"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns["marginpercent"].DefaultCellStyle.Format = "#.00\\%"; ;

        }
		private void fillToolStripButton1_Click(object sender, EventArgs e)
		{
			try
			{
				this.adminSearchProductTableAdapter.Fill(this.pMO006STGDataSet.AdminSearchProduct, this.productIDToolStripTextBox.Text, productNameToolStripTextBox.Text, productDescriptionToolStripTextBox.Text);
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}

		}


		private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex != -1)
			{
				DataRow row = ((DataRowView)this.dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
				this.selectedRow = row;
				this.Close();
			}
			
		}


		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}


        protected  void btnSave_Click(object sender, EventArgs e)
        {
            DataRow row = ((DataRowView)this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].DataBoundItem).Row;
            this.selectedRow = row;
            this.Close();
        }

        private void toolStripbtnClear_Click(object sender, EventArgs e)
        {
            dropRegion.SelectedValue = 1;
            dropStatus.SelectedValue = 2;
            productIDToolStripTextBox.Text = "";
            productNameToolStripTextBox.Text = "";
            productDescriptionToolStripTextBox.Text = "";
            dropMiniStart.SelectedIndex = 0;
            dropMiniCompleted.SelectedIndex = 0 ;
            txtAdditionalnote.Text = "";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            chkprintname.Visible = true;
            chkprintdesc.Visible = true;
            chkprintregionprice.Visible = true;
            chkprintsortorder.Visible = true;
            chkprinteffectivedate.Visible = true;
            chkstatus.Visible = true;
            chkuom.Visible = true;
            chkMiniBill.Visible = true;
            btnExport.Visible = true;
            btncloseprint.Visible = true;
            chkAdditional.Visible=true;
            label8.Visible = true;
            chkCost.Visible = true;
            chkmarginpercentage.Visible = true;
            chkMargin.Visible = true;
            chkInternalNote.Visible = true;
            chkAsBulkimport.Visible = true;
        }

        private void btncloseprint_Click(object sender, EventArgs e)
        {
            chkprintname.Visible = false;
            chkprintdesc.Visible = false;
            chkprintregionprice.Visible = false;
            chkprintsortorder.Visible = false;
            chkprinteffectivedate.Visible = false;
            chkstatus.Visible = false;
            chkuom.Visible = false;
            chkMiniBill.Visible = false;
            btnExport.Visible = false;
            btncloseprint.Visible = false;
            chkAdditional.Visible = false;
            label8.Visible = false;
            chkCost.Visible = false;
            chkmarginpercentage.Visible = false;
            chkMargin.Visible = false;
            chkInternalNote.Visible = false;
            chkAsBulkimport.Visible = false;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            CallParameter cp = new CallParameter();
            int active, regionID, stateID, studiomproduct, standardinclusion, operationsonly;
            active = Int32.Parse(dropStatus.SelectedValue.ToString());
            studiomproduct = 0;
            standardinclusion = 0;
            operationsonly = 0;
            if (dropRegion.SelectedValue != null && dropRegion.SelectedValue.ToString() != "")
            {
                regionID = Int32.Parse(dropRegion.SelectedValue.ToString());
            }
            else
            {
                regionID = 9;
            }
            if (chkstudiomproduct.Checked)
            {
                studiomproduct = 1;
            }
            if (chkStandardInclusion.Checked)
            {
                standardinclusion = 1;
            }

            if (chkOperationsOnly.Checked)
            {
                operationsonly = 1;
            }
            stateID = Int32.Parse(dropdownState.SelectedValue.ToString());

            cp.ProductID = productIDToolStripTextBox.Text.ToString();
            cp.ProductName = productNameToolStripTextBox.Text.ToString();
            cp.ProductDescription = productDescriptionToolStripTextBox.Text.ToString();
            cp.regionID = regionID.ToString();
            cp.active = active.ToString();
            cp.minibillstart = dropMiniStart.SelectedIndex.ToString();
            cp.minibillcomplete = dropMiniCompleted.SelectedIndex.ToString();
            cp.stateID = stateID.ToString();
            cp.studioMproduct = studiomproduct.ToString();
            cp.standardinclusion = standardinclusion.ToString();
            cp.additionalnote = txtAdditionalnote.Text.Trim();
            cp.operationsonly = operationsonly.ToString();

            imagebox.BackColor = Color.Transparent;
            imagebox.Parent = dataGridView1;
            imagebox.Visible = true;

            backgroundWorker2.RunWorkerAsync(cp);          
        }

        private void GenerateExcel(
            string productid,
            string productname,
            string productdesc,
            string regionid,
            string active,
            string minibillstart,
            string minibillcomplete,
            string stateid,
            string studiomproduct,
            string standardinclusion,
            string additionalnote,
            string operationsonly)
        {
            /*
             int ridx;
             Microsoft.Office.Interop.Excel.Application wapp=new Microsoft.Office.Interop.Excel.Application(); 
             Microsoft.Office.Interop.Excel.Worksheet wsheet=new Microsoft.Office.Interop.Excel.Worksheet();
             Microsoft.Office.Interop.Excel.Workbook wbook=new Microsoft.Office.Interop.Excel.Workbook();
             object template;
            */
            try
            {


                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminSearchProduct]", new System.Data.SqlClient.SqlParameter[12]
                            {
					             new System.Data.SqlClient.SqlParameter("@ProductID",productid)
					            ,new System.Data.SqlClient.SqlParameter("@ProductName",productname)
					            ,new System.Data.SqlClient.SqlParameter("@ProductDescription", productdesc)
					            ,new System.Data.SqlClient.SqlParameter("@regionID",regionid)
					            ,new System.Data.SqlClient.SqlParameter("@active",active)
                                ,new System.Data.SqlClient.SqlParameter("@minibillstart",minibillstart)
                                ,new System.Data.SqlClient.SqlParameter("@minibillcomplete", minibillcomplete)
                                ,new System.Data.SqlClient.SqlParameter("@stateID", stateid)
                                ,new System.Data.SqlClient.SqlParameter("@studioMproduct", studiomproduct)
                                , new System.Data.SqlClient.SqlParameter("@standardinclusion", standardinclusion)
                                , new System.Data.SqlClient.SqlParameter("@additionalnote", additionalnote)
                                , new System.Data.SqlClient.SqlParameter("@operationsonly", operationsonly)
                            });


                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();

                excel.Application.Workbooks.Add(true);
                DataTable table = dsTemp.Tables[0];
                int ColumnIndex = 2;
                Microsoft.Office.Interop.Excel.Range rng;

                // build the column header text;
                if (!chkAsBulkimport.Checked)
                {
                    #region normal user selected export
                    excel.Cells[1, 1] = "Product ID";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 1];
                    rng.Font.Bold = true;


                    if (chkprintname.Checked)
                    {
                        excel.Cells[1, 2] = "Product Name";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 2];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }
                    if (chkprintdesc.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "Product Description";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }
                    if (chkAdditional.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "Additonal Note";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }
                    if (chkInternalNote.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "Internal Note";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }
                    if (chkprintregionprice.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "Region";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                        excel.Cells[1, ColumnIndex] = "Price";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }
                    if (chkprintsortorder.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "Sort Order";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }
                    if (chkprinteffectivedate.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "Effective Date";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }
                    if (chkuom.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "UOM";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }
                    if (chkstatus.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "Status";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }
                    if (chkMiniBill.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "Derived Cost";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;

                        excel.Cells[1, ColumnIndex] = "Mini Bill Complete";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }

                    if (chkCost.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "Cost";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }

                    if (chkMargin.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "Margin $";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }

                    if (chkmarginpercentage.Checked)
                    {
                        excel.Cells[1, ColumnIndex] = "Margin %";
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, ColumnIndex];
                        rng.Font.Bold = true;
                        ColumnIndex++;
                    }

                    int rowIndex = 0;
                    string temp = "";

                    foreach (DataRow row in table.Rows)
                    {
                        rowIndex++;
                        ColumnIndex = 2;

                        excel.Cells[rowIndex + 1, 1] = row["productID"].ToString();

                        if (chkprintname.Checked)
                        {
                            excel.Cells[rowIndex + 1, 2] = row["productName"].ToString();
                            ColumnIndex++;
                        }
                        if (chkprintdesc.Checked)
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = row["Productdescription"].ToString();
                            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex + 1, ColumnIndex];
                            //rng.WrapText = true;
                            ColumnIndex++;
                        }
                        if (chkAdditional.Checked)
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = row["Additionalinfo"].ToString();
                            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex + 1, ColumnIndex];
                            //rng.WrapText = true;
                            ColumnIndex++;
                        }
                        if (chkInternalNote.Checked)
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = row["internaldescription"].ToString();
                            rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex + 1, ColumnIndex];
                            //rng.WrapText = true;
                            ColumnIndex++;
                        }
                        if (chkprintregionprice.Checked)
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = row["regionName"].ToString();
                            ColumnIndex++;
                            temp = String.Format("{0:F}", double.Parse(row["sellprice"].ToString()));
                            excel.Cells[rowIndex + 1, ColumnIndex] = "$" + temp;
                            ColumnIndex++;
                        }
                        if (chkprintsortorder.Checked)
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = row["sortorder"].ToString();
                            ColumnIndex++;
                        }
                        if (chkprinteffectivedate.Checked)
                        {
                            if (row["maxeffdate"].ToString() != "")
                            {
                                excel.Cells[rowIndex + 1, ColumnIndex] = DateTime.Parse(row["maxeffdate"].ToString(), new System.Globalization.CultureInfo("en-au"));

                            }
                            else
                            {
                                excel.Cells[rowIndex + 1, ColumnIndex] = "";
                            }
                            ColumnIndex++;
                        }
                        if (chkuom.Checked)
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = row["uom"].ToString();
                            ColumnIndex++;
                        }
                        if (chkstatus.Checked)
                        {
                            if ((bool)row["active"])
                            {
                                excel.Cells[rowIndex + 1, ColumnIndex] = "Active";
                            }
                            else
                            {
                                excel.Cells[rowIndex + 1, ColumnIndex] = "Inactive";
                            }
                            ColumnIndex++;
                        }

                        if (chkMiniBill.Checked)
                        {
                            if ((bool)row["minibillstart"])
                            {
                                excel.Cells[rowIndex + 1, ColumnIndex] = "Yes";
                            }
                            else
                            {
                                excel.Cells[rowIndex + 1, ColumnIndex] = "No";
                            }
                            ColumnIndex++;
                            if ((bool)row["minibillcomplete"])
                            {
                                excel.Cells[rowIndex + 1, ColumnIndex] = "Yes";
                            }
                            else
                            {
                                excel.Cells[rowIndex + 1, ColumnIndex] = "No";
                            }

                        }
                        // cost

                        if (chkCost.Checked)
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "$" + String.Format("{0:C}", row["costprice"].ToString());
                            ColumnIndex++;
                        }

                        if (chkMargin.Checked)
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "$" + String.Format("{0:C}", row["marginamount"].ToString());
                            ColumnIndex++;
                        }

                        if (chkmarginpercentage.Checked)
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = row["marginpercent"].ToString() + "%";
                            ColumnIndex++;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region force to export as per bulk import

                    excel.Cells[1, 1] = "Product ID";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 1];
                    rng.Font.Bold = true;
 
                    excel.Cells[1, 2] = "Product Name";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 2];
                    rng.Font.Bold = true;
 

                    excel.Cells[1, 3] = "Product Description";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 3];
                    rng.Font.Bold = true;
     
                    excel.Cells[1, 4] = "UOM";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 4];
                    rng.Font.Bold = true;

                    excel.Cells[1, 5] = "Sort Order";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 5];
                    rng.Font.Bold = true;

                    excel.Cells[1, 6] = "Active";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 6];
                    rng.Font.Bold = true;

                    excel.Cells[1, 7] = "BC Active";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 7];
                    rng.Font.Bold = true;

                    excel.Cells[1, 8] = "Product Category";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 8];
                    rng.Font.Bold = true;

                    excel.Cells[1, 9] = "Product Code";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 9];
                    rng.Font.Bold = true;

                    excel.Cells[1, 10] = "Price Display Code";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 10];
                    rng.Font.Bold = true;

                    excel.Cells[1, 11] = "Cost Centre Code";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 11];
                    rng.Font.Bold = true;

                    excel.Cells[1, 12] = "Mini Bill Completed";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 12];
                    rng.Font.Bold = true;

                    excel.Cells[1, 13] = "State";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 13];
                    rng.Font.Bold = true;


                    excel.Cells[1, 14] = "IsStudioMProduct";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 14];
                    rng.Font.Bold = true;

                    excel.Cells[1, 15] = "Internal Description";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 15];
                    rng.Font.Bold = true;

                    excel.Cells[1, 16] = "Additional Notes";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 16];
                    rng.Font.Bold = true;

                    excel.Cells[1, 17] = "Operation Only";
                    rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[1, 17];
                    rng.Font.Bold = true;

                    int rowIndex = 0;
                    string temp = "";

                    foreach (DataRow row in table.Rows)
                    {
                        rowIndex++;
                        ColumnIndex = 2;

                        excel.Cells[rowIndex + 1, 1] = row["productID"].ToString();
                        excel.Cells[rowIndex + 1, 2] = row["productName"].ToString();
                        ColumnIndex++;

                        excel.Cells[rowIndex + 1, ColumnIndex] = row["Productdescription"].ToString();
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex + 1, ColumnIndex];
                        ColumnIndex++;

                        excel.Cells[rowIndex + 1, ColumnIndex] = row["uom"].ToString();
                        ColumnIndex++;

                        excel.Cells[rowIndex + 1, ColumnIndex] = row["sortorder"].ToString();
                        ColumnIndex++;
 
                        if ((bool)row["active"])
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "Y";
                        }
                        else
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "N";
                        }
                        ColumnIndex++;

                        if ((bool)row["BCActive"])
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "Y";
                        }
                        else
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "N";
                        }
                        ColumnIndex++;

                        excel.Cells[rowIndex + 1, ColumnIndex] = row["ProductCategory"].ToString();
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex + 1, ColumnIndex];
                        ColumnIndex++;

                        excel.Cells[rowIndex + 1, ColumnIndex] = row["ProductCode"].ToString();
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex + 1, ColumnIndex];
                        ColumnIndex++;

                        excel.Cells[rowIndex + 1, ColumnIndex] = row["PriceDisplayCode"].ToString();
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex + 1, ColumnIndex];
                        ColumnIndex++;

                        excel.Cells[rowIndex + 1, ColumnIndex] = row["CostCentreCode"].ToString();
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex + 1, ColumnIndex];
                        ColumnIndex++;

                        if ((bool)row["minibillcomplete"])
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "Y";
                        }
                        else
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "N";
                        }
                        ColumnIndex++;


                        excel.Cells[rowIndex + 1, ColumnIndex] = row["state"].ToString();
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex + 1, ColumnIndex];
                        ColumnIndex++;

                        if ((bool)row["isStudioMProduct"])
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "Y";
                        }
                        else
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "N";
                        }
                        ColumnIndex++;

                        excel.Cells[rowIndex + 1, ColumnIndex] = row["internaldescription"].ToString();
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex + 1, ColumnIndex];
                        ColumnIndex++;

                        excel.Cells[rowIndex + 1, ColumnIndex] = row["Additionalinfo"].ToString();
                        rng = (Microsoft.Office.Interop.Excel.Range)excel.Cells[rowIndex + 1, ColumnIndex];
                        ColumnIndex++;


                        if ((bool)row["operationsonly"])
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "Y";
                        }
                        else
                        {
                            excel.Cells[rowIndex + 1, ColumnIndex] = "N";
                        }

                    }
  
                    #endregion
                }

                excel.Visible = true;
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
                worksheet.Activate();
            }
            catch (SqlException sqlex)
            {
                MessageBox.Show(sqlex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dropdownState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadRegionDropdown();
            label5.Text = dropdownState.SelectedValue.ToString();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Bitmap b1 = Properties.Resources.green;
            Bitmap b2 = Properties.Resources.red;
            Bitmap b0 = Properties.Resources.add;
            foreach (DataGridViewRow lvi in dataGridView1.Rows)
            {
                if (lvi.Cells["benchmark"].Value.ToString() == "0") // no cost price
                {
                    lvi.Cells["image"].Value = b0;
                }
                else if (lvi.Cells["benchmark"].Value.ToString() == "1")
                {
                    lvi.Cells["image"].Value = b1;
                }
                else if (lvi.Cells["benchmark"].Value.ToString() == "2")
                {
                    lvi.Cells["image"].Value = b2;
                }
            }
        }

        private void dropMiniStart_SelectionChangeCommitted(object sender, EventArgs e)
        {
            label6.Text = dropMiniStart.SelectedIndex.ToString();
        }

        private void dropMiniCompleted_SelectionChangeCommitted(object sender, EventArgs e)
        {
            label7.Text = dropMiniCompleted.SelectedIndex.ToString();
        }

        public class CallParameter
        {
            public string ProductID { get; set; }
            public string ProductName { get; set; }
            public string ProductDescription { get; set; }
            public string regionID { get; set; }
            public string active { get; set; }
            public string minibillstart { get; set; }
            public string minibillcomplete { get; set; }
            public string stateID { get; set; }
            public string studioMproduct { get; set; }
            public string standardinclusion { get; set; }
            public string additionalnote { get; set; }
            public string operationsonly { get; set; }
        }

        private void frmProductLookup_SizeChanged(object sender, EventArgs e)
        {
            CenterImageBox();
        }

        private void frmProductLookup_FormClosing(object sender, FormClosingEventArgs e)
        {
            CacheDisplayOrder();
        }

        private void CacheDisplayOrder()
        {
           IsolatedStorageFile isoFile =
              IsolatedStorageFile.GetUserStoreForAssembly();
           using (IsolatedStorageFileStream isoStream = new
              IsolatedStorageFileStream("DisplayCache", FileMode.Create,
                 isoFile))
           {
              List<metgrid> metgriditems = new List<metgrid>();
              metgrid metgriditem;
              for (int i = 0; i < dataGridView1.ColumnCount; i++)
              {
                  metgriditem = new metgrid(dataGridView1.Columns[i].DisplayIndex, dataGridView1.Columns[i].Width);
                  metgriditems.Add(metgriditem);
              }
              XmlSerializer ser = new XmlSerializer(typeof(List<metgrid>));
              ser.Serialize(isoStream, metgriditems);
           }
        }

        private void SetDisplayOrder()
        {
           IsolatedStorageFile isoFile =
              IsolatedStorageFile.GetUserStoreForAssembly();
           string[] fileNames = isoFile.GetFileNames("*");
           bool found = false;
           foreach (string fileName in fileNames)
           {
              if (fileName == "DisplayCache")
                 found = true;
           }
           if (!found)
              return;
           using (IsolatedStorageFileStream isoStream = new
              IsolatedStorageFileStream("DisplayCache", FileMode.Open,
                 isoFile))
           {
              try
              {
                  XmlSerializer ser = new XmlSerializer(typeof(List<metgrid>));
                  List<metgrid> metgriditems = (List<metgrid>)ser.Deserialize(isoStream);
                    int colIndex = 0;
                    foreach(metgrid item in metgriditems)
                    {

                        dataGridView1.Columns[colIndex].DisplayIndex = item.DisplayIndex;
                        dataGridView1.Columns[colIndex++].Width = item.Width;

                    }
              }
              catch { }
            }
        }

        private void dropRegion_SelectionChangeCommitted(object sender, EventArgs e)
        {
            label9.Text = dropRegion.SelectedValue.ToString();
        }

        private void chkAsBulkimport_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAsBulkimport.Checked)
            {
                chkAdditional.Checked = false;
                chkCost.Checked = false;
                chkMargin.Checked = false;
                chkmarginpercentage.Checked = false;
                chkMiniBill.Checked = false;
                chkprintdesc.Checked = false;
                chkprinteffectivedate.Checked = false;
                chkprintsortorder.Checked = false;
                chkuom.Checked = false;
                chkprintname.Checked = false;
                chkprintregionprice.Checked = false;
                chkMargin.Checked = false;
                chkInternalNote.Checked = false;
                chkstatus.Checked = false;

                chkAdditional.Enabled = false;
                chkCost.Enabled = false;
                chkMargin.Enabled = false;
                chkmarginpercentage.Enabled = false;
                chkMiniBill.Enabled = false;
                chkprintdesc.Enabled = false;
                chkprinteffectivedate.Enabled = false;
                chkprintsortorder.Enabled = false;
                chkuom.Enabled = false;
                chkprintname.Enabled = false;
                chkprintregionprice.Enabled = false;
                chkMargin.Enabled = false;
                chkInternalNote.Enabled = false;
                chkstatus.Enabled = false;

            }
            else
            {
                chkAdditional.Enabled = true;
                chkCost.Enabled = true;
                chkMargin.Enabled = true;
                chkmarginpercentage.Enabled = true;
                chkMiniBill.Enabled = true;
                chkprintdesc.Enabled = true;
                chkprinteffectivedate.Enabled = true;
                chkprintsortorder.Enabled = true;
                chkuom.Enabled = true;
                chkprintname.Enabled = true;
                chkprintregionprice.Enabled = true;
                chkMargin.Enabled = true;
                chkInternalNote.Enabled = true;
                chkstatus.Enabled = true;
            }
        }
    }

    public class metgrid
    {
        public int DisplayIndex;
        public int Width;
        public metgrid() { }
        public metgrid(int displayIndex, int width)
        {
            DisplayIndex = displayIndex;
            Width = width;
        }
    }

}

