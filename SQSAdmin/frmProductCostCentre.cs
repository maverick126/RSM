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
    public partial class frmProductCostCentre : Form
    {
        public frmProductCostCentre()
        {
            InitializeComponent();
        }


        private void loadList()
        {
            int active = 0;

            if (chkSearchActive.Checked)
            {
                active = 1;
            }
            try
            {
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetProductCostCentreCode]", new System.Data.SqlClient.SqlParameter[3]
                                {
                                      new System.Data.SqlClient.SqlParameter("@code", txtSearchCode.Text)
                                    , new System.Data.SqlClient.SqlParameter("@desc", txtSearchDesc.Text)
                                    , new System.Data.SqlClient.SqlParameter("@active", active)

                                 });
                bindData(dsTemp);
            }
            catch (SqlException sqlex)
            {
                MessageBox.Show(sqlex.ToString());
            }


        }
        private void bindData(DataSet ds)
        {
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[0].HeaderText = "CodeID";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].DisplayIndex = 0;

            dataGridView1.Columns[1].HeaderText = "Code";
            dataGridView1.Columns[1].ReadOnly = false;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[1].DisplayIndex = 1;

            dataGridView1.Columns[2].HeaderText = "Description";
            dataGridView1.Columns[2].ReadOnly = false;
            dataGridView1.Columns[2].DisplayIndex = 2;
            dataGridView1.Columns[2].Width = 280;

            dataGridView1.Columns[3].HeaderText = "Active";
            dataGridView1.Columns[3].ReadOnly = false;
            dataGridView1.Columns[3].DisplayIndex = 3;
            dataGridView1.Columns[3].Width = 60;

            dataGridView1.Columns[4].Visible = false;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            int active = 0;
            if (txtCode.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid product cost centre code!");
            }
            else
            {
                if (chkActive.Checked)
                {
                    active = 1;
                }
                try
                {
                    DataSet dsTemp1 = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select * from tblCostCentreCode where CostCentreCode='" + txtCode.Text.ToString().Trim() + "'");
                    if (dsTemp1.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("This cost centre code already exists !");
                    }
                    else
                    {
                        DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminAddEditProductCostCentreCode]", new System.Data.SqlClient.SqlParameter[6]
                                {
					                  new System.Data.SqlClient.SqlParameter("@codeID", -1)
                                    , new System.Data.SqlClient.SqlParameter("@code", txtCode.Text)
                                    , new System.Data.SqlClient.SqlParameter("@desc", txtDesc.Text)
                                    , new System.Data.SqlClient.SqlParameter("@active", active)
                                    , new System.Data.SqlClient.SqlParameter("@action", "NEW")
					                , new System.Data.SqlClient.SqlParameter("@userCode", MetriconCommon.UserCode)
                                 });

                        clearText();
                        loadList();
                    }
                }
                catch (SqlException sqlex)
                {
                    MessageBox.Show(sqlex.ToString());
                }
            }
        }
        private void clearText()
        {
            txtCode.Text = "";
            txtDesc.Text = "";
            chkActive.Checked = true;
        }

        private void frmProductCostCentre_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            loadList();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int codeID, active;
            string code, desc;


            try
            {
                codeID = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["CostCentreCodeID"].Value.ToString());
                if ((bool)(dataGridView1.Rows[e.RowIndex].Cells["Active"].Value))
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }
                code = dataGridView1.Rows[e.RowIndex].Cells["CostCentreCode"].Value.ToString();
                desc = dataGridView1.Rows[e.RowIndex].Cells["CostCentreCodeDesc"].Value.ToString();


                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminAddEditProductCostCentreCode]", new System.Data.SqlClient.SqlParameter[6]
                            {
				                  new System.Data.SqlClient.SqlParameter("@codeID", codeID)
                                , new System.Data.SqlClient.SqlParameter("@code", code)
                                , new System.Data.SqlClient.SqlParameter("@desc", desc)
                                , new System.Data.SqlClient.SqlParameter("@active", active)
                                , new System.Data.SqlClient.SqlParameter("@action", "EDIT")
				                , new System.Data.SqlClient.SqlParameter("@userCode", MetriconCommon.UserCode)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadList();
        }



    }
}