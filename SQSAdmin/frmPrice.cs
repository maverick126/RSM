using SQSAdmin.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQSAdmin
{
	public partial class frmPrice : SQSAdmin.frmLookup, IInputForm
	{

		private InputFormMode mode;

		DataSet dsRegion;

		private string productID;
		private string selectedProductName;
        private string stateID;

		public string SelectedProductName
		{
			get { return selectedProductName; }
			set { selectedProductName = value; }
		}

		public string ProductID
		{
			get { return productID; }
			set { productID = value; }
		}

        public string StateID
        {
            get { return stateID; }
            set { stateID = value; }
        }

		public frmPrice()
		{
			InitializeComponent();
		}

		private void frmPrice_Load(object sender, EventArgs e)
		{
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            this.txtProductID.Text = this.productID;
			this.txtProductName.Text = this.selectedProductName;

			LoadRegion();
			LoadPriceList();
            LoadRegionGroup();
			ViewRead();
            loadConfigurationData();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

		private void LoadRegion()
		{


            dsRegion = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetRegion]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@stateID", stateID )
                            });

			dropRegion.DataSource = null;
			dropRegion.Items.Clear();
            if (dsRegion.Tables[0].Rows.Count > 0)
            {
                dropRegion.ValueMember = "RegionID";
                dropRegion.DisplayMember = "RegionName";
                dropRegion.DataSource = dsRegion.Tables[0];
            }
		}

        private void LoadRegionGroup()
        {


            DataSet ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spw_GetRegionGroupByState]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@stateID", stateID )
                            });

            DataRow myDataRow = ds.Tables[0].NewRow();
            myDataRow["idRegionGroup"] = 0;
            myDataRow["RegionGroupName"] = "Please Select...";
            ds.Tables[0].Rows.Add(myDataRow);
            ds.Tables[0].DefaultView.Sort = "idregiongroup";

            dropregiongroup.DataSource = null;
            dropregiongroup.Items.Clear();
            if (ds.Tables[0].Rows.Count > 0)
            {
                dropregiongroup.ValueMember = "idregiongroup";
                dropregiongroup.DisplayMember = "RegionGroupName";
                dropregiongroup.DataSource = ds.Tables[0];
            }
            dropregiongroup.SelectedValue = "0";
        }
		private void LoadPriceList()
		{
            
			DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminSearchPrice", new System.Data.SqlClient.SqlParameter[1] { new System.Data.SqlClient.SqlParameter("@ProductID", this.productID)});
            bindDataToGrid(dsTemp);
		}

        private void bindDataToGrid(DataSet dsTemp)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = dsTemp.Tables[0];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[1].HeaderText = "Effective Date";
            dataGridView1.Columns[1].DataPropertyName = "effectiveDate";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].DisplayIndex = 1;
            dataGridView1.Columns[2].Visible = false;
            
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[3].ReadOnly = false;
            dataGridView1.Columns[3].HeaderText = "DBC Cost";
            dataGridView1.Columns[3].DataPropertyName = "dbccost";
            dataGridView1.Columns[3].DisplayIndex = 3;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "c";
            dataGridView1.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns[4].Visible = true;
            dataGridView1.Columns[4].ReadOnly = false;
            dataGridView1.Columns[4].HeaderText = "BTP Cost";
            dataGridView1.Columns[4].DataPropertyName = "costprice";
            dataGridView1.Columns[4].DisplayIndex = 4;
            dataGridView1.Columns[4].DefaultCellStyle.Format = "c";
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns[6].Visible = true;
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[6].HeaderText = "Sell Price";
            dataGridView1.Columns[6].DataPropertyName = "sellprice";
            dataGridView1.Columns[6].DisplayIndex = 5;
            dataGridView1.Columns[6].DefaultCellStyle.Format = "c";
            dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns[5].Visible = true;
            dataGridView1.Columns[5].ReadOnly = false;
            dataGridView1.Columns[5].HeaderText = "Target Margin %";
            dataGridView1.Columns[5].DataPropertyName = "targetmarginpercent";
            dataGridView1.Columns[5].DisplayIndex = 6;
            dataGridView1.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView1.Columns[5].Width = 110;


            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;

            dataGridView1.Columns[12].HeaderText = "Active";
            dataGridView1.Columns[12].ReadOnly = false;
            dataGridView1.Columns[12].DataPropertyName = "active";
            dataGridView1.Columns[12].DisplayIndex = 12;

            dataGridView1.Columns[13].HeaderText = "Derived Cost";
            dataGridView1.Columns[13].ReadOnly = false;
            dataGridView1.Columns[13].DataPropertyName = "derivedcost";
            dataGridView1.Columns[13].DisplayIndex = 13;

            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible = false;
            dataGridView1.Columns[17].Visible = false;
            dataGridView1.Columns[18].Visible = false;
            dataGridView1.Columns[19].Visible = false;
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[21].Visible = true;
            dataGridView1.Columns[21].HeaderText = "Region";
            dataGridView1.Columns[21].ReadOnly = true;
            dataGridView1.Columns[21].DataPropertyName = "regionname";
            dataGridView1.Columns[21].DisplayIndex = 21;

            dataGridView1.Columns[22].Visible = false;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            int priceID,active, derivedcost;
            decimal costprice, dbccost, targetmargin;

            try
            {
                    priceID = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["priceID"].Value.ToString());
                if (dataGridView1.Rows[e.RowIndex].Cells["active"].Value != null && (bool)(dataGridView1.Rows[e.RowIndex].Cells["active"].Value))
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["derivedcost"].Value != null && (bool)(dataGridView1.Rows[e.RowIndex].Cells["derivedcost"].Value))
                {
                    derivedcost = 1;
                }
                else
                {
                    derivedcost = 0;
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["costprice"].Value!=null && dataGridView1.Rows[e.RowIndex].Cells["costprice"].Value.ToString()!="")
                {
                    try
                    {
                        costprice = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells["costprice"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please enter a valid cost price!");
                        return;
                    }
                }
                else
                {
                    costprice = 0;
                }

                if (dataGridView1.Rows[e.RowIndex].Cells["dbccost"].Value != null && dataGridView1.Rows[e.RowIndex].Cells["dbccost"].Value.ToString() != "")
                {
                    try
                    {
                        dbccost = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells["dbccost"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please enter a valid cost price!");
                        return;
                    }
                }
                else
                {
                    dbccost = 0;
                }
                if (dataGridView1.Rows[e.RowIndex].Cells["targetmarginpercent"].Value != null && dataGridView1.Rows[e.RowIndex].Cells["targetmarginpercent"].Value.ToString() != "")
                {
                    try
                    {
                        targetmargin = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells["targetmarginpercent"].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Please enter a valid cost price!");
                        return;
                    }
                }
                else
                {
                    targetmargin = 0;
                }
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminChangePriceStatus]", new System.Data.SqlClient.SqlParameter[7]
                            {
					            new System.Data.SqlClient.SqlParameter("@priceID",priceID)
					            ,new System.Data.SqlClient.SqlParameter("@Active",active)
                                ,new System.Data.SqlClient.SqlParameter("@derivedcost",derivedcost)
                                ,new System.Data.SqlClient.SqlParameter("@costprice",costprice)
                                ,new System.Data.SqlClient.SqlParameter("@dbccost",dbccost)
                                ,new System.Data.SqlClient.SqlParameter("@targetmargin",targetmargin)
                                , new System.Data.SqlClient.SqlParameter("@ModifiedBy",MetriconCommon.UserCode)
                            });

            }
            catch (IndexOutOfRangeException ex1)
            {
                MessageBox.Show(ex1.Message.ToString());
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message.ToString());
            }

        }

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
            /*
			if (listView1.SelectedItems.Count > 0)
			{
				DataRow row = (DataRow)listView1.SelectedItems[0].Tag;
				txtEffectiveDate.Text = DateTime.Parse(row["EffectiveDate"].ToString()).ToString("dd/MM/yyyy");
				txtCostPrice.Text = row["CostPrice"].ToString();
				txtSellPrice.Text = row["SellPrice"].ToString();
				MetriconCommon.ToCurrency(ref txtCostPrice);
				MetriconCommon.ToCurrency(ref txtSellPrice);

				foreach (DataRowView drv in dropRegion.Items)
				{
					if (row["RegionName"].ToString().Equals(drv["RegionName"].ToString()))
					{
						dropRegion.SelectedItem = drv;
					}
				}
				

				chkActive.Checked = bool.Parse(row["Active"].ToString());
				txtPriceID.Text = row["PriceID"].ToString();
				ViewRead();
			}
			else
			{
				ClearForm();
				ViewRead();
			}


            */

		}

		private void button1_Click(object sender, EventArgs e)
		{
			
		}

		private void textBoxBTPCost_Enter(object sender, EventArgs e)
		{
			//MetriconCommon.ToDouble(ref textBoxBTPCost);
		}

		private void textBoxBTPCost_TextChanged(object sender, EventArgs e)
		{

		}

		private void textBoxBTPCost_Leave(object sender, EventArgs e)
		{
			//MetriconCommon.ToCurrency(ref textBoxBTPCost);

		}

		public void ClearForm()
		{
			txtPriceID.Text = "";

            if (txtEffectiveDate.Text == "  /  /")
            {
                txtEffectiveDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                textBoxBTPCost.Text = "0.00";
                txtSellPrice.Text = "0.00";
            }

		    if (dropRegion.SelectedValue!=null)
			dropRegion.SelectedIndex = 0;
			//dropRegion.Text = "Metro";
			chkActive.Checked = true;
            chkDerivedCost.Checked = false;
			txtPriceID.Text = "";
			chkAllRegions.Checked = true;
            chkSamePrice.Checked = false;
			dropRegion.Enabled = false;


			
		}

		public void ViewNew()
		{
			mode = InputFormMode.New;
			ClearForm();
			txtEffectiveDate.ReadOnly = false;
			// textBoxBTPCost.ReadOnly = false;
            textBoxDBCCost.ReadOnly = false;
            textBoxTargetMargin.ReadOnly = false;
            txtSellPrice.ReadOnly = false;
			dropRegion.Enabled = false;
			chkActive.Enabled = true;
            chkDerivedCost.Enabled = true;
			chkAllRegions.Enabled = true;
            chkSamePrice.Enabled = true;

			btnNew.Enabled = false;
			btnEdit.Enabled = false;
			btnCancelData.Enabled = true;
			btnSaveData.Enabled = true;
            radioregiongroup.Enabled = true;
            radioregion.Enabled = true;
            if (radioregiongroup.Checked)
            {
                dropregiongroup.Enabled = true;
                chkAllRegions.Enabled = false;
                chkSamePrice.Enabled = false;
            }
            else
            {
                dropregiongroup.Enabled = false;
            }
		}
		public void ViewEdit()
		{
			mode = InputFormMode.Edit;
			txtEffectiveDate.ReadOnly = false;
			// textBoxBTPCost.ReadOnly = false;
            textBoxDBCCost.ReadOnly = false;
            textBoxTargetMargin.ReadOnly = false;
            txtSellPrice.ReadOnly = false;
			dropRegion.Enabled = true;
			chkActive.Enabled = true;
            chkDerivedCost.Enabled = true;
			chkAllRegions.Enabled = false;
            chkSamePrice.Enabled = false;

			btnNew.Enabled = false;
			btnEdit.Enabled = false;
			btnCancelData.Enabled = true;
			btnSaveData.Enabled = true;
            radioregiongroup.Enabled = true;
            radioregion.Enabled = true;
            dropregiongroup.Enabled = true;

		}
		public void ViewRead()
		{
			mode = InputFormMode.Read;
			txtEffectiveDate.ReadOnly = true;
			textBoxBTPCost.ReadOnly = true;
            textBoxDBCCost.ReadOnly = true;
            textBoxTargetMargin.ReadOnly = true;
            txtSellPrice.ReadOnly = true;
			dropRegion.Enabled = false;
			chkActive.Enabled = false;
			chkAllRegions.Enabled = false;
            chkSamePrice.Enabled = false;
            chkDerivedCost.Enabled = false;

			btnNew.Enabled = true;
			btnEdit.Enabled = true;
			btnCancelData.Enabled = false;
			btnSaveData.Enabled = false;
            radioregiongroup.Enabled = false;
            radioregion.Enabled = false;
            dropregiongroup.Enabled = false;

		}
		public void SaveForm()
		{

			if (mode == InputFormMode.New)
			{
                if (radioregiongroup.Checked)
                {
                    if (dropregiongroup.SelectedValue.ToString() == "0")
                    {
                        MessageBox.Show("Please select a region group!");
                    }
                    else
                    {
                        System.Data.SqlClient.SqlParameter[] myParams = new System.Data.SqlClient.SqlParameter[11];
                        myParams[0] = new System.Data.SqlClient.SqlParameter("@EffectiveDate", DateTime.Parse(txtEffectiveDate.Text).ToString("dd/MMM/yyyy"));
                        myParams[1] = new System.Data.SqlClient.SqlParameter("@ProductID", productID);
                        myParams[2] = new System.Data.SqlClient.SqlParameter("@CostPrice", textBoxBTPCost.Text);
                        myParams[3] = new System.Data.SqlClient.SqlParameter("@DBCCost", textBoxDBCCost.Text);
                        myParams[4] = new System.Data.SqlClient.SqlParameter("@TargetMargin", textBoxTargetMargin.Text);
                        myParams[5] = new System.Data.SqlClient.SqlParameter("@SellPrice", txtSellPrice.Text);
                        myParams[6] = new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode);
                        myParams[7] = new System.Data.SqlClient.SqlParameter("@Active", chkActive.Checked);
                        myParams[8] = new System.Data.SqlClient.SqlParameter("@stateID", stateID);
                        myParams[9] = new System.Data.SqlClient.SqlParameter("@DerivedCost", chkDerivedCost.Checked);
                        myParams[10] = new System.Data.SqlClient.SqlParameter("@regiongroupid", dropregiongroup.SelectedValue.ToString());
                        MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_Admin_AddPriceToRegionGroup", myParams);

                        LoadPriceList();
                    }
                }
                else
                {
                    if (chkAllRegions.Checked)
                    {
                        System.Data.SqlClient.SqlParameter[] myParams = new System.Data.SqlClient.SqlParameter[11];
                        myParams[0] = new System.Data.SqlClient.SqlParameter("@EffectiveDate", DateTime.Parse(txtEffectiveDate.Text).ToString("dd/MMM/yyyy"));
                        myParams[1] = new System.Data.SqlClient.SqlParameter("@ProductID", productID);
                        myParams[2] = new System.Data.SqlClient.SqlParameter("@CostPrice", textBoxBTPCost.Text);
                        myParams[3] = new System.Data.SqlClient.SqlParameter("@DBCCost", textBoxDBCCost.Text);
                        myParams[4] = new System.Data.SqlClient.SqlParameter("@TargetMargin", textBoxTargetMargin.Text);
                        myParams[5] = new System.Data.SqlClient.SqlParameter("@SellPrice", txtSellPrice.Text);
                        myParams[6] = new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode);
                        myParams[7] = new System.Data.SqlClient.SqlParameter("@Active", chkActive.Checked);
                        myParams[8] = new System.Data.SqlClient.SqlParameter("@stateID", stateID);
                        myParams[9] = new System.Data.SqlClient.SqlParameter("@DerivedCost", chkDerivedCost.Checked);
                        myParams[10] = new System.Data.SqlClient.SqlParameter("@samepricetoallregion", chkSamePrice.Checked);
                        MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminAddPriceAllRegion", myParams);
                    }
                    else
                    {
                        System.Data.SqlClient.SqlParameter[] myParams = new System.Data.SqlClient.SqlParameter[10];
                        myParams[0] = new System.Data.SqlClient.SqlParameter("@EffectiveDate", DateTime.Parse(txtEffectiveDate.Text).ToString("dd/MMM/yyyy"));
                        myParams[1] = new System.Data.SqlClient.SqlParameter("@ProductID", productID);
                        myParams[2] = new System.Data.SqlClient.SqlParameter("@CostPrice", textBoxBTPCost.Text);
                        myParams[3] = new System.Data.SqlClient.SqlParameter("@DBCCost", textBoxDBCCost.Text);
                        myParams[4] = new System.Data.SqlClient.SqlParameter("@TargetMargin", textBoxTargetMargin.Text);
                        myParams[5] = new System.Data.SqlClient.SqlParameter("@SellPrice", txtSellPrice.Text);
                        myParams[6] = new System.Data.SqlClient.SqlParameter("@RegionID", dropRegion.SelectedValue);
                        myParams[7] = new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode);
                        myParams[8] = new System.Data.SqlClient.SqlParameter("@Active", chkActive.Checked);
                        myParams[9] = new System.Data.SqlClient.SqlParameter("@DerivedCost", chkDerivedCost.Checked);
                        MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminAddPrice", myParams);
                    }
                    LoadPriceList();
                }

			}


			if (mode == InputFormMode.Edit)
			{

					System.Data.SqlClient.SqlParameter[] myParams = new System.Data.SqlClient.SqlParameter[10];
					myParams[0] = new System.Data.SqlClient.SqlParameter("@EffectiveDate", DateTime.Parse(txtEffectiveDate.Text).ToString("dd/MMM/yyyy"));
					myParams[1] = new System.Data.SqlClient.SqlParameter("@ProductID", productID);
					myParams[2] = new System.Data.SqlClient.SqlParameter("@CostPrice", textBoxBTPCost.Text);
                    myParams[3] = new System.Data.SqlClient.SqlParameter("@DBCCost", textBoxDBCCost.Text);
                    myParams[4] = new System.Data.SqlClient.SqlParameter("@TargetMargin", textBoxTargetMargin.Text);
                    myParams[5] = new System.Data.SqlClient.SqlParameter("@SellPrice", txtSellPrice.Text);
					myParams[6] = new System.Data.SqlClient.SqlParameter("@RegionID", dropRegion.SelectedValue);
					myParams[7] = new System.Data.SqlClient.SqlParameter("@ModifiedBy", MetriconCommon.UserCode);
					myParams[8] = new System.Data.SqlClient.SqlParameter("@Active", chkActive.Checked);
					myParams[9] = new System.Data.SqlClient.SqlParameter("@PriceID", txtPriceID.Text);
					MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminEditPrice", myParams);
                    LoadPriceList();

			}

            if ((radioregiongroup.Checked && dropregiongroup.SelectedValue.ToString() != "0") || radioregion.Checked)
            {
                txtEffectiveDate.ReadOnly = true;
                textBoxBTPCost.ReadOnly = true;
                textBoxDBCCost.ReadOnly = true;
                textBoxTargetMargin.ReadOnly = true;
                txtSellPrice.ReadOnly = true;
                dropRegion.Enabled = false;
                chkActive.Enabled = false;
                chkDerivedCost.Enabled = false;
                ViewRead();
            }
            
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			ViewNew();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtPriceID.Text))
				ViewEdit();
		}

		private void btnSaveData_Click(object sender, EventArgs e)
		{
			SaveForm();
		}

		private void btnCancelData_Click(object sender, EventArgs e)
		{
			ViewRead();
		}

		private void txtSellPrice_Leave(object sender, EventArgs e)
		{
			MetriconCommon.ToCurrency(ref txtSellPrice);

		}

		private void txtSellPrice_Enter(object sender, EventArgs e)
		{
			MetriconCommon.ToDouble(ref txtSellPrice);
		}

		private void chkAllRegions_CheckedChanged(object sender, EventArgs e)
		{
            if (chkAllRegions.Checked)
            {
                dropRegion.Enabled = false;
                chkSamePrice.Enabled = true;
                chkSamePrice.Checked = false;
                
            }
            else
            {
                dropRegion.Enabled = true;               
                chkSamePrice.Checked = false;
                chkSamePrice.Enabled = false;
            }

		}

        protected override void btnSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listView1_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            /*
            this.listView1.ListViewItemSorter = new ListViewItemComparer(e.Column, listView1.Sorting);
            listView1.Sort();
            if (listView1.Sorting == SortOrder.Ascending)
            {
                listView1.Sorting = SortOrder.Descending;
            }
            else
            {
                listView1.Sorting = SortOrder.Ascending;
            }
            */
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].ErrorText = "";
            decimal newInteger;

            // Don't try to validate the 'new row' until finished  
            // editing since there 
            // is not any point in validating its initial value. 
            if (dataGridView1.Rows[e.RowIndex].IsNewRow) { return; }
            if (e.ColumnIndex == 3)
            {
                if (!decimal.TryParse(e.FormattedValue.ToString().Replace("$", ""), out newInteger))
                {
                    e.Cancel = true;
                    dataGridView1.Rows[e.RowIndex].ErrorText = "Please enter a valid cost price!";
                }
            }
        }

        private void radioregiongroup_CheckedChanged(object sender, EventArgs e)
        {
            if (radioregiongroup.Checked)
            {
                dropregiongroup.Enabled = true;
            }
            else
            {
                dropregiongroup.Enabled = false;
            }
        }

        private void radioregion_CheckedChanged(object sender, EventArgs e)
        {
            if (radioregion.Checked)
            {
                if (chkAllRegions.Checked)
                {
                    dropRegion.Enabled = false ;
                    chkSamePrice.Enabled = true;
                }
                else
                {
                    dropRegion.Enabled = true;
                    chkSamePrice.Enabled = false;
                }
                  chkAllRegions.Enabled = true;
                  
            }
            else
            {
                dropRegion.Enabled = false;
                chkAllRegions.Enabled = false;
                chkSamePrice.Enabled = false;
            }
        }

        private void checkBoxApplyCMA_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCMAPercent.Visible = checkBoxApplyCMA.Checked;
            labelCMAPercent.Visible = textBoxCMAPercent.Visible;
            ApplyCMA();
        }

        private void textBoxDBCCost_TextChanged(object sender, EventArgs e)
        {
            ApplyCMA();
        }

        private void ApplyCMA()
        {
            double dbcCost = 0;
            double btpCost = 0;
            double sellPrice = 0;
            double gst = 1.1;

            double.TryParse(textBoxDBCCost.Text.ToString().Replace("$", ""), out dbcCost);
            if (checkBoxApplyCMA.Checked)
            {
                double cmaPercent = 0;
                double.TryParse(textBoxCMAPercent.Text.ToString(), out cmaPercent);

                btpCost = dbcCost + dbcCost * (cmaPercent / 100);
            }
            else
            {
                btpCost = dbcCost;
            }
            double.TryParse(txtSellPrice.Text.ToString().Replace("$", ""), out sellPrice);
            textBoxBTPCost.Text = btpCost.ToString();
            if (sellPrice > 0)
            {
                textBoxBTPTargetMargin.Text = Math.Round(100 * ((sellPrice / gst) - btpCost) / (sellPrice / gst), 2).ToString();
            }
            MetriconCommon.ToCurrency(ref textBoxBTPCost);
        }

        private void textBoxDBCCost_Enter(object sender, EventArgs e)
        {
            MetriconCommon.ToDouble(ref textBoxDBCCost);
        }

        private void textBoxDBCCost_Leave(object sender, EventArgs e)
        {
            ApplyCMA();
            MetriconCommon.ToCurrency(ref textBoxDBCCost);
        }

        private void textBoxTargetMargin_Enter(object sender, EventArgs e)
        {
            MetriconCommon.ToDouble(ref textBoxTargetMargin);
        }

        private void textBoxTargetMargin_Leave(object sender, EventArgs e)
        {
            MetriconCommon.ToDouble(ref textBoxTargetMargin);
        }

        private void loadConfigurationData(int regionID = 0)
        {
            DataSet dsRegion = MetriconCommon.DatabaseManager.ExecuteSQLQuery("Select CMAPercent from [tblDisclaimerDaysAndAmount] where fkidregion=" + regionID.ToString() + " and active = 1 order by Effectivedate desc ");
            if (dsRegion.Tables[0].Rows.Count > 0)
            {
                textBoxCMAPercent.Text = dsRegion.Tables[0].Rows[0][0].ToString();
            }
            dsRegion.Dispose();
        }
    }
}

