using SQSAdmin.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQSAdmin
{
    public partial class frmCopyPAG : Form
    {
        private int selectedrow = 0;
        public frmCopyPAG()
        {
            InitializeComponent();
        }

        private void frmCopyPAG_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            loadStateDropdown();
            LoadBrands();
            LoadAreas();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void loadStateDropdown()
        {
            DataSet dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });

            ComboBox cb = (ComboBox)toolStripCBState.Control;

            cb.DataSource = dsState.Tables[0];
            cb.DisplayMember = "stateAbbreviation";
            cb.ValueMember = "stateID";
            cb.SelectedValue = MetriconCommon.UserState;

            cb.SelectionChangeCommitted += new System.EventHandler(this.cb_SelectionChangeCommitted);
        }

        private void cb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadBrands();
        }

        private void LoadBrands()
        {
            ComboBox cb = (ComboBox)toolStripCBBrand.Control;
            LoadBrandList(cb);
            LoadBrandList(cbDestBrand);
        }

        private void LoadBrandList(ComboBox cb)
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetBrand]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@stateID", ((ComboBox)toolStripCBState.Control).SelectedValue.ToString() )
                            });

            DataRow myDataRow = dsTemp.Tables[0].NewRow();
            myDataRow["brandid"] = 99;
            myDataRow["brandname"] = "All";
            dsTemp.Tables[0].Rows.Add(myDataRow);
            dsTemp.Tables[0].DefaultView.Sort = "brandname";

            cb.DataSource = dsTemp.Tables[0];
            cb.DisplayMember = "brandname";
            cb.ValueMember = "brandid";
            cb.SelectedValue = 99;
        }

        private void toolStripBtnSearch_Click(object sender, EventArgs e)
        {
            int brandID, active;
            string homename;
            string stateID = ((ComboBox)toolStripCBState.Control).SelectedValue.ToString();

            try
            {
                brandID = Int32.Parse(((ComboBox)toolStripCBBrand.Control).SelectedValue.ToString());
                active = 1; // All active and inactive

                homename=((TextBox)toolStripTextBox1.Control).Text.ToString();

                DataSet dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminFindHome]", new System.Data.SqlClient.SqlParameter[7]
                            {
                                new System.Data.SqlClient.SqlParameter("@stateID", stateID),
                                new System.Data.SqlClient.SqlParameter("@brandID", brandID),
                                new System.Data.SqlClient.SqlParameter("@active", active),
                                new System.Data.SqlClient.SqlParameter("@homename", homename),
                                new System.Data.SqlClient.SqlParameter("@showSummary", 1),
                                new System.Data.SqlClient.SqlParameter("@viewmode", "homefacade"), //source home always go to facade level.
                                new System.Data.SqlClient.SqlParameter("@showonpricelist", 2 )
                            }
                       );
                PopulateHomeGrid(gridSourceHome, dsHome);

                // Populate destination details
                loadHomeList();
                //PopulateHomeGrid(gridDestinationHome, dsHome);

                cbDestBrand.SelectedValue = brandID;
                txtDestHome.Text = homename;
                
            }
            catch (SqlException ex2)
            {
                MessageBox.Show(ex2.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void PopulateHomeGrid(DataGridView grid, DataSet ds)
        {
            grid.DataSource = ds.Tables[0];
            grid.Columns["HomeID"].HeaderText = "Home ID";
            grid.Columns["HomeID"].ReadOnly = true;
            grid.Columns["HomeID"].Width = 80;

            grid.Columns["HomeName"].HeaderText = "Home Name";
            grid.Columns["HomeName"].ReadOnly = true;
            grid.Columns["HomeName"].Width = 250;

            grid.Columns["StateAbbreviation"].HeaderText = "State";
            grid.Columns["StateAbbreviation"].ReadOnly = true;
            grid.Columns["StateAbbreviation"].Width = 50;

            grid.Columns["ParentHomeID"].Visible = false;

            grid.Columns["BrandName"].HeaderText = "Brand";
            grid.Columns["BrandName"].ReadOnly = true;
            grid.Columns["BrandName"].Width = 120;

            grid.Columns["Active"].ReadOnly = true;
            grid.Columns["Active"].Width = 50;

            grid.Columns["Brandid"].ReadOnly = true;
            grid.Columns["Brandid"].Width = 0;
            grid.Columns["Brandid"].Visible = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadHomeList();
        }

        private void loadHomeList()
        {
            string viewmode = "homeplan";
            if (radHomePlan.Checked)
            {
                viewmode = "homeplan";
            }
            else
            {
                viewmode = "homefacade";
            }
            ComboBox cb = (ComboBox)toolStripCBState.Control;

            DataSet dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminFindHome", new System.Data.SqlClient.SqlParameter[7]
                            {
                                new System.Data.SqlClient.SqlParameter("@stateID", cb.SelectedValue.ToString()),
                                new System.Data.SqlClient.SqlParameter("@brandID", cbDestBrand.SelectedValue.ToString()),
                                new System.Data.SqlClient.SqlParameter("@active", 1),
                                new System.Data.SqlClient.SqlParameter("@homename", txtDestHome.Text),
                                new System.Data.SqlClient.SqlParameter("@showSummary", 1),
                                new System.Data.SqlClient.SqlParameter("@viewmode", viewmode),
                                new System.Data.SqlClient.SqlParameter("@showonpricelist", 2 )
                            }
                       );
            PopulateHomeGrid(gridDestinationHome, dsHome);
        }


        private void LoadAreas()
        {
            LoadAreaList(cbSourceArea);
            LoadAreaList(cbDestinationArea);
        }

        private void LoadAreaList(ComboBox cb)
        {
            DataSet ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminGetAllAreas", new System.Data.SqlClient.SqlParameter[0] );
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                cb.DataSource = ds.Tables[0];
                cb.DisplayMember = "areaname";
                cb.ValueMember = "areaid";
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            string sourceHomeId = "", sourceAreaId = "";
            string destHomeId = "", destHomeBrandId = "", destAreaId = "";
            string mode="homeplan";
            if (radHomePlan.Checked)
            {
                mode = "homeplan";
            }
            else
            {
                mode = "homefacade";
            }
            if (IsDataValid(ref sourceHomeId, ref sourceAreaId, ref destHomeId, ref destAreaId, ref destHomeBrandId))
            {
                // Start copy
                DataSet dsCopy = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_Admin_CopyConfigForSpecificArea", new System.Data.SqlClient.SqlParameter[7]
                            {
                                new System.Data.SqlClient.SqlParameter("@srcHomeId", sourceHomeId),
                                new System.Data.SqlClient.SqlParameter("@srcAreaID", sourceAreaId),
                                new System.Data.SqlClient.SqlParameter("@destHomeId", destHomeId),
                                new System.Data.SqlClient.SqlParameter("@destHomeBrandId", destHomeBrandId),
                                new System.Data.SqlClient.SqlParameter("@destAreaID", destAreaId),
                                new System.Data.SqlClient.SqlParameter("@userCode", MetriconCommon.UserCode),
                                new System.Data.SqlClient.SqlParameter("@mode",mode)
                            }
                );
                // Dispaly result
                if (dsCopy != null && dsCopy.Tables[0].Rows.Count > 0)
                {
                    string error = dsCopy.Tables[0].Rows[0]["Error"].ToString();
                    string msg = dsCopy.Tables[0].Rows[0]["OutputMessage"].ToString();
                    msg = msg.Replace("<br>", "\n");
                    if (error == "0")
                        MessageBox.Show(msg, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool IsDataValid(ref string sourceHomeId, ref string sourceAreaId, ref string destHomeId, ref string destAreaId, ref string destHomeBrandId)
        {
            bool result = true;

            // Get source data, should be only home selected
            
            foreach (DataGridViewRow row in gridSourceHome.Rows)
            {
                if (row.Cells["SourceSelected"].Value != null && bool.Parse(row.Cells["SourceSelected"].Value.ToString()))
                {
                    // check if more than one source home selected
                    if (sourceHomeId == "")
                        sourceHomeId = row.Cells["HomeID"].Value.ToString();
                    else
                    {
                        // More than one home selected
                        MessageBox.Show("Only one source home can be selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                }
            }
            if (sourceHomeId == "")
            {
                MessageBox.Show("Please select a \n source home!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            sourceAreaId = cbSourceArea.SelectedValue.ToString();

            // get destination data
            destAreaId = cbDestinationArea.SelectedValue.ToString();
            destHomeId = "";
            foreach (DataGridViewRow row in gridDestinationHome.Rows)
            {
                if (row.Cells["DestinationSelected"].Value != null && bool.Parse(row.Cells["DestinationSelected"].Value.ToString()))
                {
                    // check if more than one source home selected
                    if (destHomeId == "")
                    {
                        if (radHomePlan.Checked)
                        {
                            destHomeId = row.Cells["HomeName"].Value.ToString();
                            destHomeBrandId = row.Cells["Brandid"].Value.ToString();
                        }
                        else
                        {
                            destHomeId = row.Cells["HomeID"].Value.ToString();
                            destHomeBrandId = row.Cells["Brandid"].Value.ToString();
                        }
                    }
                    else
                    {

                        if (radHomePlan.Checked)
                        {
                            destHomeId =destHomeId+","+ row.Cells["HomeName"].Value.ToString();
                            destHomeBrandId = destHomeBrandId + "," + row.Cells["Brandid"].Value.ToString();
                        }
                        else
                        {
                            destHomeId = destHomeId + "," + row.Cells["HomeID"].Value.ToString();
                            destHomeBrandId = destHomeBrandId + "," + row.Cells["Brandid"].Value.ToString();
                        }
                        //// More than one home selected
                        //MessageBox.Show("Only one destination home can be selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //return false;
                    }
                }
            }
            if (destHomeId == "")
            {
                MessageBox.Show("Please select a destination home!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            // check if source and destination home and area are the same.
            if (destHomeId == sourceHomeId && destAreaId == sourceAreaId)
            {
                MessageBox.Show("Destination home and area cannot be the same as the source home!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            return result;
        }

        private void chbDeleteExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (chbDeleteExisting.Checked == false)
            {
                if (MessageBox.Show("Configuration can be doubled up if deletion does not run first before copy.\n\n Do you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    chbDeleteExisting.Checked = true;
            }
        }

        private void chbCreateNewPAG_CheckedChanged(object sender, EventArgs e)
        {
            if (chbCreateNewPAG.Checked == true)
            {
                if (MessageBox.Show("All none existing PAG's will be created automatically.\n\n Do you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    chbCreateNewPAG.Checked = false;
            }
        }

        private void gridSourceHome_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int currentrow = e.RowIndex;
            bool selected = false;
            if (gridSourceHome.Rows[e.RowIndex].Cells["SourceSelected"].Value != null)
            {
                selected = bool.Parse(gridSourceHome.Rows[e.RowIndex].Cells["SourceSelected"].Value.ToString());
            }

            if (selected)
            {
                foreach (DataGridViewRow row in gridSourceHome.Rows)
                {
                    if (row.Index == selectedrow)
                    {
                        row.Cells["SourceSelected"].Value = false;
                    }
                }
                selectedrow = currentrow;
            }

        }



        private void gridSourceHome_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //int currentrow = e.RowIndex;
            //bool selected = false;
            //if (gridSourceHome.Rows[e.RowIndex].Cells["SourceSelected"].Value != null)
            //{
            //    selected = bool.Parse(gridSourceHome.Rows[e.RowIndex].Cells["SourceSelected"].Value.ToString());
            //}
            //if (selected)
            //{
            //    foreach (DataGridViewRow row in gridSourceHome.Rows)
            //    {
            //        if (row.Index == selectedrow)
            //        {
            //            row.Cells["SourceSelected"].Value = false;
            //        }
            //    }
            //    selectedrow = currentrow;
            //}
        }

        private void gridSourceHome_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            DataGridViewCell cell = dgv.CurrentCell;
            if (cell.RowIndex >= 0 && cell.ColumnIndex == 0) // My checkbox column
            {
                // If checkbox checked, copy value from col 1 to col 2
                if (dgv.Rows[cell.RowIndex].Cells[cell.ColumnIndex].EditedFormattedValue != null && dgv.Rows[cell.RowIndex].Cells[cell.ColumnIndex].EditedFormattedValue.Equals(true))
                {
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (row.Index != cell.RowIndex)
                        {
                            row.Cells["SourceSelected"].Value = false;
                        }
                    }
                }
            }
        }

        private void radHomePlan_CheckedChanged(object sender, EventArgs e)
        {
            loadHomeList();
        }

        private void radHomeFacade_CheckedChanged(object sender, EventArgs e)
        {
            loadHomeList();
        }


    }
}