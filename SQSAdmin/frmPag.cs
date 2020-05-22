using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SQSAdmin.Common;

namespace SQSAdmin
{
	public partial class frmPag : Form, IInputForm
	{

		DataSet dsAllAreas;
		DataSet dsAllGroups;
		DataSet dsPAG;

		private string productID;
		private string productName;
		public string SelectedProductName
		{
			get { return productName; }
			set { productName = value; }
		}
		public string ProductID
		{
			get { return productID; }
			set { productID = value; }
		}


		private InputFormMode mode;

		public frmPag()
		{
			InitializeComponent();
			
			
		}
		private void btnSave_Click(object sender, EventArgs e)
		{
			this.Close();
		}
		private void frmPag_Load(object sender, EventArgs e)
		{
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            this.txtProductID.Text = productID;
			this.txtProductName.Text = SelectedProductName;
			
			//Load Areas
			if (dsAllAreas == null)
				dsAllAreas = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select areaid, areaname from area order by areaname");


			
			dropArea.DataSource = dsAllAreas.Tables[0];
			dropArea.DisplayMember = "areaname";
			


			//Load Groups
			if (dsAllGroups == null)
				dsAllGroups = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select groupid, groupname from [group] order by groupname");
			dropGroup.DataSource = dsAllGroups.Tables[0];
			dropGroup.DisplayMember = "groupname";

			LoadPAG();
			ViewRead();
            alertPanel.Visible = false;

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
		private void LoadPAG()
		{
            dsPAG = MetriconCommon.DatabaseManager.ExecuteSQLQuery(@"SELECT ProductAreaGroup.GroupID,ProductAreaGroup.AreaID, ProductAreaGroup.ProductAreaGroupID,ProductAreaGroup.ProductID,Area.AreaName, [Group].GroupName, ProductAreaGroup.Active , ProductAreaGroup.issitework  
																	FROM ProductAreaGroup INNER JOIN
																	Area ON ProductAreaGroup.AreaID = Area.AreaID INNER JOIN
																	[Group] ON ProductAreaGroup.GroupID = [Group].GroupID
																	WHERE (ProductAreaGroup.ProductID = N'" + ProductID + "')");

            bindingData(dsPAG);  

		}

        private void bindingData(DataSet dsTemp)
        {
            dataGridView1.DataSource = dsTemp.Tables[0];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;

            dataGridView1.Columns[2].HeaderText = "PAG ID";
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[2].DisplayIndex = 1;

            dataGridView1.Columns[3].Visible = false;

            dataGridView1.Columns[4].HeaderText = "Area";
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[4].DisplayIndex = 2;
            dataGridView1.Columns[4].Width = 210;

            dataGridView1.Columns[5].HeaderText = "Group";
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[5].DisplayIndex = 3;
            dataGridView1.Columns[5].Width = 210;
            dataGridView1.Columns[6].HeaderText = "Active";
            dataGridView1.Columns[6].ReadOnly = false;
            dataGridView1.Columns[6].DisplayIndex = 4;
            dataGridView1.Columns[6].Width = 60;

            dataGridView1.Columns[7].HeaderText = "Site Work";
            dataGridView1.Columns[7].ReadOnly = false;
            dataGridView1.Columns[7].DisplayIndex = 5;
            dataGridView1.Columns[7].Width = 80;
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            int areaID, groupID,active,pagID,sitework;

            areaID = Int32.Parse(dropArea.SelectedValue.ToString());
            groupID = Int32.Parse(dropGroup.SelectedValue.ToString());

            try
            {
                pagID = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["productareagroupid"].Value.ToString());
                if ((bool)(dataGridView1.Rows[e.RowIndex].Cells["active"].Value))
                {
                    active = 1;
                }
                else
                {
                    active =0;
                }

                if ((bool)(dataGridView1.Rows[e.RowIndex].Cells["issitework"].Value))
                {
                    sitework = 1;
                }
                else
                {
                    sitework = 0;
                }

                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminEditPAG]", new System.Data.SqlClient.SqlParameter[6]
                            {
					            new System.Data.SqlClient.SqlParameter("@ProductAreaGroupID",pagID)
					            ,new System.Data.SqlClient.SqlParameter("@AreaID",areaID)
					            ,new System.Data.SqlClient.SqlParameter("@GroupID", groupID)
					            ,new System.Data.SqlClient.SqlParameter("@Active",active)
                                ,new System.Data.SqlClient.SqlParameter("@issitework",sitework)
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
		private void lstGroups_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
					
			ListViewItem lvi = e.Item;
			DataRow row = (DataRow)lvi.Tag;
			if (lvi.Checked)
			{

				lvi.BackColor = Color.Lavender;
			}
			else
			{
				lvi.BackColor = Color.White;

			}
		}
		private void lstAreas_ItemChecked(object sender, ItemCheckedEventArgs e)
		{

			ListViewItem lvi = e.Item;
			if (lvi.Checked)
			{
				lvi.BackColor = Color.Lavender;

			}
			else
			{
				lvi.BackColor = Color.White;
			}
		}

		private void btnNewData_Click(object sender, EventArgs e)
		{

		}
		private void btnSaveData_Click(object sender, EventArgs e)
		{
			if (mode == InputFormMode.New)
			{

				if ((dropArea.SelectedIndex != -1) && (dropGroup.SelectedIndex != -1))
				{

                    //bool areaRow = false;//listArea.Contains(dropArea.SelectedValue.ToString());
                    //bool groupRow = false; //listGroup.Contains(dropGroup.SelectedValue.ToString());

                    bool areaRow = false;
                    bool groupRow = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[4].Value.ToString().Equals(dropArea.Text) && row.Cells[5].Value.ToString().Equals(dropGroup.Text))
                        {
                            areaRow = true;
                            groupRow = true;
                            break;
                        }
                    }

					//if either the area or group is null then allow the insert of a new pag
					if ((areaRow == false) || (groupRow == false))
					{
						MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminAddPAG", new System.Data.SqlClient.SqlParameter[6]{
					 new System.Data.SqlClient.SqlParameter("@ProductID",this.txtProductID.Text)
                    ,new System.Data.SqlClient.SqlParameter("@AreaID",dropArea.SelectedValue)
                    ,new System.Data.SqlClient.SqlParameter("@GroupID",dropGroup.SelectedValue)
					,new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode)
					,new System.Data.SqlClient.SqlParameter("@Active",chkActive.Checked)
					, new System.Data.SqlClient.SqlParameter("@issitework",chksitework.Checked)});
					}

					else
					{
						MessageBox.Show("PAG for product " + this.txtProductID.Text + " in same area and group already exists.");
					}

				}
			}
			else
			{
				if ((dropArea.SelectedIndex != -1) && (dropGroup.SelectedIndex != -1))
				{

					MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminEditPAG", new System.Data.SqlClient.SqlParameter[6]{
					new System.Data.SqlClient.SqlParameter("@ProductAreaGroupID",this.txtPAGID.Text)
					,new System.Data.SqlClient.SqlParameter("@AreaID",this.dropArea.SelectedValue)
					,new System.Data.SqlClient.SqlParameter("@GroupID", this.dropGroup.SelectedValue)
					,new System.Data.SqlClient.SqlParameter("@Active",chkActive.Checked)
                    , new System.Data.SqlClient.SqlParameter("@issitework",chksitework.Checked)
					, new System.Data.SqlClient.SqlParameter("@ModifiedBy",MetriconCommon.UserCode)});
				}		
			}
			ViewRead();
			LoadPAG();
		}
		public void ViewNew()
		{
			mode = InputFormMode.New;
			ClearForm();
			this.dropArea.Enabled = true;
			this.dropGroup.Enabled = true;
			this.chkActive.Enabled = true;
            this.chksitework.Enabled = true;


			btnNew.Enabled = false;
			btnEdit.Enabled = false;
			btnCancelData.Enabled = true;
			btnSaveData.Enabled = true;

		}
		public void ViewEdit()
		{
			mode = InputFormMode.Edit;
			this.dropArea.Enabled = true;
			this.dropGroup.Enabled = true;
			this.chkActive.Enabled = true;
            this.chksitework.Enabled = true;


			btnNew.Enabled = false;
			btnEdit.Enabled = false;
			btnCancelData.Enabled = true;
			btnSaveData.Enabled = true;
		}
		public void ViewRead()
		{
			mode = InputFormMode.Read;
			this.dropArea.Enabled = false;
			this.dropGroup.Enabled = false;
			this.chkActive.Enabled = false;
            this.chksitework.Enabled = false;

			btnNew.Enabled = true;
			btnEdit.Enabled = true;
			btnCancelData.Enabled = false;
			btnSaveData.Enabled = false;
		}
		public void SaveForm()
		{
			ViewRead();
		}
		public void ClearForm()
		{
			this.txtPAGID.Text = "";
			this.dropArea.SelectedIndex = -1;
			this.dropGroup.SelectedIndex = -1;
			this.chkActive.Checked = false;
            this.chksitework.Checked = false;
		}
		private void btnCancelData_Click(object sender, EventArgs e)
		{
			ViewRead();
		}
		private void btnEdit_Click(object sender, EventArgs e)
		{
            checkPAGPromotionProduct();

			if (!(string.IsNullOrEmpty(this.txtPAGID.Text)))
				ViewEdit();
		}
		private void btnNew_Click(object sender, EventArgs e)
		{
			ViewNew();
			this.chkActive.Checked = true;
		}

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtPAGID.Text = dataGridView1.Rows[e.RowIndex].Cells["productareagroupid"].Value.ToString();
                dropArea.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["areaid"].Value.ToString();
                dropGroup.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells["groupid"].Value.ToString();
                this.chkActive.Checked = bool.Parse(dataGridView1.Rows[e.RowIndex].Cells["active"].Value.ToString());
                this.chksitework.Checked = bool.Parse(dataGridView1.Rows[e.RowIndex].Cells["issitework"].Value.ToString());

                ViewRead();
                checkPAGPromotionProduct();
            }
            else
            {
                ClearForm();
                ViewRead();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void checkPAGPromotionProduct()
        {

            int pagID;
            if (txtPAGID.Text.Trim() != "")
            {
                try
                {
                    pagID = Int32.Parse(txtPAGID.Text.Trim());

                    DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_checkPAGPromotionProduct]", new System.Data.SqlClient.SqlParameter[1]
                            {
					            new System.Data.SqlClient.SqlParameter("@pagID",pagID)
                            });
                    if (dsTemp.Tables[0].Rows[0]["result"].ToString() == "1")
                    {
                        alertPanel.Visible = true;
                    }
                    else
                    {
                        alertPanel.Visible = false;
                    }

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

        }


	}
}