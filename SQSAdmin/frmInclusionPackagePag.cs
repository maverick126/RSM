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
    public partial class frmInclusionPackagePag : Form
    {
        frmInclusionPackageLookUp frminclusionpackagelookup;
        string idregiongroup, idbrand, idinclusionpackage;
        public frmInclusionPackagePag()
        {
            InitializeComponent();
        }

        private void frmInclusionPackagePag_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            if (frminclusionpackagelookup == null)
                frminclusionpackagelookup = new frmInclusionPackageLookUp();
            frminclusionpackagelookup.ShowDialog();

            if (frminclusionpackagelookup.SelectedPackage != null && (frminclusionpackagelookup.SelectedPackage.RowState == DataRowState.Unchanged))
            {
                DataRow row = frminclusionpackagelookup.SelectedPackage;
                label1.Text = "Configure PAGs to " + row["regiongroupname"].ToString() + " " + row["brandname"].ToString() + " " + row["inclusionname"].ToString();
                idregiongroup=row["fkidregiongroup"].ToString();
                idbrand=row["fkidbrand"].ToString();
                idinclusionpackage = row["idinclusionpackage"].ToString();

                loadArea();
                loadGroup();
                searchInclusionPAG();
                searchExistingInclusionPAG();
            }

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void loadArea()
        {

            DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAllAreas]", new System.Data.SqlClient.SqlParameter[0] 
                            {
                            });
            DataSet dsReg2 = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAllAreas]", new System.Data.SqlClient.SqlParameter[0] 
                            {
                            });
            if (dsReg.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsReg.Tables[0].NewRow();
                DataRow dr2 = dsReg2.Tables[0].NewRow();

                dr["areaid"] = "0";
                dr["areaname"] = "All";
                dr2["areaid"] = "0";
                dr2["areaname"] = "All";

                dsReg.Tables[0].Rows.InsertAt(dr, 0);
                dsReg2.Tables[0].Rows.InsertAt(dr2, 0);

                dropArea.DataSource = dsReg.Tables[0];
                dropArea.DisplayMember = "areaname";
                dropArea.ValueMember = "areaid";
                dropArea.SelectedValue = 1;

                dropArea2.DataSource = dsReg2.Tables[0];
                dropArea2.DisplayMember = "areaname";
                dropArea2.ValueMember = "areaid";
                dropArea2.SelectedValue = 1;


            }
            else
            {
                dropArea.DataSource = null;
                dropArea.Items.Clear();
                dropArea2.DataSource = null;
                dropArea2.Items.Clear();
            }
        }
        private void loadGroup()
        {

            DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAllGroups]", new System.Data.SqlClient.SqlParameter[0] 
                            {
                            });
            DataSet dsReg2 = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAllGroups]", new System.Data.SqlClient.SqlParameter[0] 
                            {
                            });

            if (dsReg.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsReg.Tables[0].NewRow();
                DataRow dr2 = dsReg2.Tables[0].NewRow();

                dr["groupid"] = "0";
                dr["groupname"] = "All";
                dr2["groupid"] = "0";
                dr2["groupname"] = "All";

                dsReg.Tables[0].Rows.InsertAt(dr, 0);
                dsReg2.Tables[0].Rows.InsertAt(dr2, 0);

                dropGroup.DataSource = dsReg.Tables[0];
                dropGroup.DisplayMember = "groupname";
                dropGroup.ValueMember = "groupid";
                dropGroup.SelectedValue = 1;


                dropGroup2.DataSource = dsReg2.Tables[0];
                dropGroup2.DisplayMember = "groupname";
                dropGroup2.ValueMember = "groupid";
                dropGroup2.SelectedValue = 1;
            }
            else
            {
                dropGroup.DataSource = null;
                dropGroup.Items.Clear();

                dropGroup2.DataSource = null;
                dropGroup2.Items.Clear();
            }
        }

        private void searchInclusionPAG()
        {

            try
            {
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminSearchAllInclusionPags]", new System.Data.SqlClient.SqlParameter[7] 
                            {
                                new System.Data.SqlClient.SqlParameter("@idregiongroup", idregiongroup),
                                new System.Data.SqlClient.SqlParameter("@idbrand", idbrand),
                                new System.Data.SqlClient.SqlParameter("@areaid", dropArea.SelectedValue.ToString()),
                                new System.Data.SqlClient.SqlParameter("@groupid", dropGroup.SelectedValue.ToString()),
                                new System.Data.SqlClient.SqlParameter("@productID", txtProductID.Text),
                                new System.Data.SqlClient.SqlParameter("@productname", txtProductName.Text),
                                new System.Data.SqlClient.SqlParameter("@idinclusionpackage", idinclusionpackage)
                            });
                if (dsReg.Tables[0].Rows.Count >= 0)
                {
                    bindGrid1Data(dsReg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bindGrid1Data(DataSet dsTemp)
        {
            dataGridView1.DataSource = dsTemp.Tables[0];

            dataGridView1.Columns["areaid"].Visible = false;
            dataGridView1.Columns["groupid"].Visible = false;

            dataGridView1.Columns["productareagroupid"].HeaderText = "PAG ID";
            dataGridView1.Columns["productareagroupid"].DisplayIndex = 1;
            dataGridView1.Columns["productareagroupid"].ReadOnly = true;
            dataGridView1.Columns["productareagroupid"].Width = 70;

            dataGridView1.Columns["productid"].HeaderText = "ProductID";
            dataGridView1.Columns["productid"].DisplayIndex = 2;
            dataGridView1.Columns["productid"].ReadOnly = true;
            dataGridView1.Columns["productid"].Width = 120;

            dataGridView1.Columns["productname"].HeaderText = "Product Name";
            dataGridView1.Columns["productname"].DisplayIndex = 3;
            dataGridView1.Columns["productname"].ReadOnly = true;
            dataGridView1.Columns["productname"].Width = 200;

            dataGridView1.Columns["areaname"].HeaderText = "Area";
            dataGridView1.Columns["areaname"].DisplayIndex = 4;
            dataGridView1.Columns["areaname"].ReadOnly = true;
            dataGridView1.Columns["areaname"].Width = 130;

            dataGridView1.Columns["groupname"].HeaderText = "Group";
            dataGridView1.Columns["groupname"].DisplayIndex = 5;
            dataGridView1.Columns["groupname"].ReadOnly = true;
            dataGridView1.Columns["groupname"].Width = 130;


            dataGridView1.Columns["select"].Width = 20;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchInclusionPAG();
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            searchExistingInclusionPAG();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder pagIDs = new StringBuilder();

            foreach (DataGridViewRow lvi in dataGridView2.Rows)
            {
                if (lvi.Cells["select2"].Value != null && (bool)(lvi.Cells["select2"].Value))
                {
                    pagIDs.Append(lvi.Cells["fkidproductareagroup"].Value.ToString());
                    pagIDs.Append(",");
                }
            }

            if (pagIDs.Length > 0)
            {
                pagIDs.Remove(pagIDs.Length - 1, 1);
            }

            try
            {
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminRemovePagsFromInclusionPackage", new System.Data.SqlClient.SqlParameter[2] 
				{
					new System.Data.SqlClient.SqlParameter("@pagIDs", pagIDs.ToString())
					, new System.Data.SqlClient.SqlParameter("@fkidinclusionpackage", idinclusionpackage)
				});
                searchInclusionPAG();
                searchExistingInclusionPAG();

                MessageBox.Show("PAG successfully removed!", "Message");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
			
        }

        private void searchExistingInclusionPAG()
        {

            try
            {
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminSearchExistingInclusionPags]", new System.Data.SqlClient.SqlParameter[5] 
                            {
                                new System.Data.SqlClient.SqlParameter("@idinclusionpackage", idinclusionpackage),
                                new System.Data.SqlClient.SqlParameter("@areaid", dropArea2.SelectedValue.ToString()),
                                new System.Data.SqlClient.SqlParameter("@groupid", dropGroup2.SelectedValue.ToString()),
                                new System.Data.SqlClient.SqlParameter("@productID", txtProductID2.Text),
                                new System.Data.SqlClient.SqlParameter("@productname", txtProductName2.Text)
                            });
                if (dsReg.Tables[0].Rows.Count >= 0)
                {
                    bindGrid1Data2(dsReg);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bindGrid1Data2(DataSet dsTemp)
        {
            dataGridView2.DataSource = dsTemp.Tables[0];

            dataGridView2.Columns["areaid"].Visible = false;
            dataGridView2.Columns["groupid"].Visible = false;

            dataGridView2.Columns["fkidproductareagroup"].HeaderText = "PAG ID";
            dataGridView2.Columns["fkidproductareagroup"].DisplayIndex = 1;
            dataGridView2.Columns["fkidproductareagroup"].ReadOnly = true;
            dataGridView2.Columns["fkidproductareagroup"].Width = 60;

            dataGridView2.Columns["productid"].HeaderText = "ProductID";
            dataGridView2.Columns["productid"].DisplayIndex = 2;
            dataGridView2.Columns["productid"].ReadOnly = true;
            dataGridView2.Columns["productid"].Width = 120;

            dataGridView2.Columns["productname"].HeaderText = "Product Name";
            dataGridView2.Columns["productname"].DisplayIndex = 3;
            dataGridView2.Columns["productname"].ReadOnly = true;
            dataGridView2.Columns["productname"].Width = 200;

            dataGridView2.Columns["areaname"].HeaderText = "Area";
            dataGridView2.Columns["areaname"].DisplayIndex = 4;
            dataGridView2.Columns["areaname"].ReadOnly = true;
            dataGridView2.Columns["areaname"].Width = 130;

            dataGridView2.Columns["groupname"].HeaderText = "Group";
            dataGridView2.Columns["groupname"].DisplayIndex = 5;
            dataGridView2.Columns["groupname"].ReadOnly = true;
            dataGridView2.Columns["groupname"].Width = 130;


            dataGridView2.Columns[0].Width = 20;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder pagIDs = new StringBuilder();

            foreach (DataGridViewRow lvi in dataGridView1.Rows)
            {
                if (lvi.Cells["select"].Value!=null && (bool)(lvi.Cells["select"].Value))
                {
                    pagIDs.Append(lvi.Cells["productareagroupid"].Value.ToString());
                    pagIDs.Append(",");
                }
            }

            if (pagIDs.Length > 0)
            {
                pagIDs.Remove(pagIDs.Length - 1, 1);
            }

            try
            {
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminAddPagsToInclusionPackage", new System.Data.SqlClient.SqlParameter[3] 
				{
					new System.Data.SqlClient.SqlParameter("@pagIDs", pagIDs.ToString())
					, new System.Data.SqlClient.SqlParameter("@fkidinclusionpackage", idinclusionpackage)
                    , new System.Data.SqlClient.SqlParameter("@usercode", MetriconCommon.UserCode)
				});
                searchInclusionPAG();
                searchExistingInclusionPAG();

                MessageBox.Show("PAG successfully configured", "Message");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
			
        }

    }
}