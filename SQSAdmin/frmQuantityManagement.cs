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
    public partial class frmQuantityManagement : Form
    {
        private DataSet dsAllAreas, dsAllGroup;
        //private System.Collections.Hashtable IDList;

        public frmQuantityManagement()
        {
            InitializeComponent();
        }

        private void frmQuantityManagement_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            loadStateDropdown();
            loadBrand();
            loadArea();
            loadGroup();
            //loadHomeList();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
        private void loadStateDropdown()
        {
            DataSet dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });
            dropdownState.DataSource = dsState.Tables[0];
            dropdownState.DisplayMember = "stateAbbreviation";
            dropdownState.ValueMember = "stateID";
            dropdownState.SelectedValue=MetriconCommon.UserState;

        }

        private void loadBrand()
        {

            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spw_GetBrandPlusAllBrandByState]", new System.Data.SqlClient.SqlParameter[1]
                            {
                                new System.Data.SqlClient.SqlParameter("@stateID", dropdownState.SelectedValue.ToString())
                            }

               );
            cmbBrand.DataSource = dsTemp.Tables[0];
            cmbBrand.DisplayMember = "BrandName";
            cmbBrand.ValueMember = "brandid";
            cmbBrand.SelectedValue = "0";

        }

        private void loadArea()
        {
            if (dsAllAreas == null)
                dsAllAreas = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select areaid, areaname from area order by areaname");

            dropArea.ValueMember = "areaid";
            dropArea.DisplayMember = "areaname";
            dropArea.DataSource = dsAllAreas.Tables[0];
            dropArea.SelectedIndex = 0;

        }
        private void loadGroup()
        {
            if (dsAllGroup == null)
                dsAllGroup = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select groupid, groupname from [group] where active=1 order by groupname");

            dropGroup.ValueMember = "groupid";
            dropGroup.DisplayMember = "groupname";
            dropGroup.DataSource = dsAllGroup.Tables[0];
            dropGroup.SelectedIndex = 0;
        }
        private void loadHomeList()
        {

            int areaID, groupID,stateID;
            areaID = Int32.Parse(dropArea.SelectedValue.ToString());
            groupID = Int32.Parse(dropGroup.SelectedValue.ToString());
            stateID = Int32.Parse(dropdownState.SelectedValue.ToString());

            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminGetHomeModel]", new System.Data.SqlClient.SqlParameter[4]
                            {
                                new System.Data.SqlClient.SqlParameter("@stateID", stateID),
                                new System.Data.SqlClient.SqlParameter("@areaID", areaID),
                                new System.Data.SqlClient.SqlParameter("@groupID", groupID),
                                new System.Data.SqlClient.SqlParameter("@brandID", cmbBrand.SelectedValue.ToString()),
                            }

                   );

            bindingData(dsTemp);

        }
        private void bindingData(DataSet dsTemp)
        {
            dataGridView1.DataSource = dsTemp.Tables[0];
            dataGridView1.Columns[0].Visible = false;

            dataGridView1.Columns[1].HeaderText = "Home Model";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].DisplayIndex = 1;

            dataGridView1.Columns[2].HeaderText = "Quantity";
            dataGridView1.Columns[2].ReadOnly = false;
            dataGridView1.Columns[2].DisplayIndex = 2;
            dataGridView1.Columns[2].DefaultCellStyle.Format = "n2";

            dataGridView1.Columns[3].HeaderText = "Display1";
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[3].DisplayIndex = 3;

            dataGridView1.Columns[4].HeaderText = "Display2";
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[4].DisplayIndex = 4;

            dataGridView1.Columns[5].HeaderText = "Display3";
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[5].DisplayIndex = 5;

            dataGridView1.Columns[6].HeaderText = "Display4";
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[6].DisplayIndex = 6;

            dataGridView1.Columns[7].HeaderText = "Display5";
            dataGridView1.Columns[7].ReadOnly = true;
            dataGridView1.Columns[7].DisplayIndex = 7;

            dataGridView1.Columns[8].HeaderText = "Display6";
            dataGridView1.Columns[8].ReadOnly = true;
            dataGridView1.Columns[8].DisplayIndex = 8;

            dataGridView1.Columns[9].HeaderText = "Display7";
            dataGridView1.Columns[9].ReadOnly = true;
            dataGridView1.Columns[9].DisplayIndex = 9;
        }

        private void btnSearch_Click(object sender, EventArgs e)    
        {
            loadHomeList();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int areaID, groupID,stateID;
            double qty;
            string homemodel,createdBy;

            areaID = Int32.Parse(dropArea.SelectedValue.ToString());
            groupID = Int32.Parse(dropGroup.SelectedValue.ToString());
            stateID = Int32.Parse(dropdownState.SelectedValue.ToString());

            try
            {
                DataRow modifiedRow = ((DataRowView)this.dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;

                homemodel=modifiedRow["homemodel"].ToString();
                qty =double.Parse(modifiedRow["quantity"].ToString());
                createdBy=MetriconCommon.UserCode;

                if (modifiedRow.RowState == DataRowState.Unchanged)
                    modifiedRow.SetModified();



                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminChangeQtyOfAreaGroupForAllHome]", new System.Data.SqlClient.SqlParameter[7]
                            {
                                new System.Data.SqlClient.SqlParameter("@homemodel", homemodel),
                                new System.Data.SqlClient.SqlParameter("@stateID", stateID),
                                new System.Data.SqlClient.SqlParameter("@areaID", areaID),
                                new System.Data.SqlClient.SqlParameter("@groupID", groupID),
                                new System.Data.SqlClient.SqlParameter("@qty", qty),
                                new System.Data.SqlClient.SqlParameter("@createdBy", createdBy),
                                new System.Data.SqlClient.SqlParameter("@brandid", cmbBrand.SelectedValue.ToString())
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshQuantity();
            loadHomeList();
        }

        private void RefreshQuantity()
        {
            int areaID, groupID, stateID;
            areaID = Int32.Parse(dropArea.SelectedValue.ToString());
            groupID = Int32.Parse(dropGroup.SelectedValue.ToString());
            stateID = Int32.Parse(dropdownState.SelectedValue.ToString());

            try
            {
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminRefreshAndUpdateQuantity]", new System.Data.SqlClient.SqlParameter[4]
                            {
                                new System.Data.SqlClient.SqlParameter("@stateID", stateID),
                                new System.Data.SqlClient.SqlParameter("@areaID", areaID),
                                new System.Data.SqlClient.SqlParameter("@groupID", groupID),
                                new System.Data.SqlClient.SqlParameter("@usercode", MetriconCommon.UserCode)
                            });

                MessageBox.Show("Quantity has been updated successfully!");
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message.ToString());
            }
        }

 

        private void dropdownState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadBrand();
        }
    }
}