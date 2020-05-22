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
    public partial class frmProductCodeDiscounts : Form
    {
        public frmProductCodeDiscounts()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int budgeted = 0;
            int active = 0;
            if (txtCode.Text.ToString() == "")
            {
                MessageBox.Show("Please enter a valid product code!");
            }
            else
            {
                if (checkBoxBudgeted.Checked)
                {
                    budgeted = 1;
                }
                if (checkBoxActive.Checked)
                {
                    active = 1;
                }
                try
                {
                        DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminAddEditProductCodeDiscounted]", new System.Data.SqlClient.SqlParameter[4]
                        {
					        new System.Data.SqlClient.SqlParameter("@productid", txtCode.Text)
                            , new System.Data.SqlClient.SqlParameter("@budgeted", budgeted)
                            , new System.Data.SqlClient.SqlParameter("@active", active)
                            , new System.Data.SqlClient.SqlParameter("@userCode", MetriconCommon.UserCode)
                            });
                        clearText();
                        loadList();
                }
                catch (SqlException sqlex)
                {
                    MessageBox.Show(sqlex.ToString());
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadList();
        }
        private void clearText()
        {
            txtCode.Text = "";
            txtDesc.Text = "";
            checkBoxActive.Checked = true;
        }

        private void loadList()
        {
            int budgeted = 0;
            int active = 0;

            if (checkBoxSearchBudgeted.Checked)
            {
                budgeted = 1;
            }
            if (checkBoxSearchActive.Checked)
            {
                active = 1;
            }
            try
            {
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetDiscountedProductsList]", new System.Data.SqlClient.SqlParameter[4]
                                {
                                      new System.Data.SqlClient.SqlParameter("@productid", txtSearchCode.Text)
                                    , new System.Data.SqlClient.SqlParameter("@desc", txtSearchDesc.Text)
                                    , new System.Data.SqlClient.SqlParameter("@budgeted", budgeted)
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
            dataGridView1.Columns[0].HeaderText = "IDProductDiscounted";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[0].Width = 50;
            dataGridView1.Columns[0].DisplayIndex = 0;
            dataGridView1.Columns[0].Visible = false;

            dataGridView1.Columns[1].HeaderText = "ProductID";
            dataGridView1.Columns[1].ReadOnly = false;
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[1].DisplayIndex = 1;

            dataGridView1.Columns[2].HeaderText = "ProductName";
            dataGridView1.Columns[2].ReadOnly = false;
            dataGridView1.Columns[2].DisplayIndex = 2;
            dataGridView1.Columns[2].Width = 200;

            dataGridView1.Columns[3].HeaderText = "Budgeted";
            dataGridView1.Columns[3].ReadOnly = false;
            dataGridView1.Columns[3].DisplayIndex = 3;
            dataGridView1.Columns[3].Width = 70;

            dataGridView1.Columns[4].HeaderText = "Active";
            dataGridView1.Columns[4].ReadOnly = false;
            dataGridView1.Columns[4].DisplayIndex = 4;
            dataGridView1.Columns[4].Width = 60;

            //dataGridView1.Columns[4].Visible = false;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int budgeted, active;
            string productid;


            try
            {
                productid = dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                if ((bool)(dataGridView1.Rows[e.RowIndex].Cells["budgeted"].Value))
                {
                    budgeted = 1;
                }
                else
                {
                    budgeted = 0;
                }
                if ((bool)(dataGridView1.Rows[e.RowIndex].Cells["Active"].Value))
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }


                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminAddEditProductCodeDiscounted]", new System.Data.SqlClient.SqlParameter[4]
                                {
                                    new System.Data.SqlClient.SqlParameter("@productid", productid)
                                    , new System.Data.SqlClient.SqlParameter("@budgeted", budgeted)
                                    , new System.Data.SqlClient.SqlParameter("@active", active)
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

        private void frmProductCode_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            loadList();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            txtDesc.Text = "";
            btnSave.Enabled = false;
        }

        private void buttonAddProductSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsTemp1 = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select ProductDescription from product where ProductID='" + txtCode.Text.ToString().Trim() + "'");
                if (dsTemp1.Tables[0].Rows.Count > 0)
                {
                    txtDesc.Text = dsTemp1.Tables[0].Rows[0]["ProductDescription"].ToString();
                    btnSave.Enabled = true;
                }
                else
                {
                    MessageBox.Show("This product does not exists!, please add it in the new product screen first in order to set it as discounted product.");
                    labelProductNotFound.Visible = true;
                }
            }
            catch (SqlException sqlex)
            {
                MessageBox.Show(sqlex.ToString());
            }
        }
    }
}