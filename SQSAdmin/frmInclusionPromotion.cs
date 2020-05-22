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
    public partial class frmInclusionPromotion : Form
    {
        DataSet dsState;
        public frmInclusionPromotion()
        {
            InitializeComponent();
        }
        private void frmInclusionPromotion_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            loadStateDropdown();
            LoadBrandList(dropdownState.SelectedValue.ToString());
            loadInclusionPromotion();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
        private void loadStateDropdown()
        {
            dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });
            dropdownState.DataSource = dsState.Tables[0];
            dropdownState.DisplayMember = "stateAbbreviation";
            dropdownState.ValueMember = "stateID";
            dropdownState.SelectedIndex = 0;

            dropState2.DataSource = dsState.Tables[0];
            dropState2.DisplayMember = "stateAbbreviation";
            dropState2.ValueMember = "stateID";
            dropState2.SelectedIndex = 0;

            if (label1.Text != "")
            {
                dropdownState.SelectedValue = label1.Text.Trim();
            }

            if (label1.Text != "")
            {
                dropdownState.SelectedValue = label1.Text.Trim();
            }

        }
        private void LoadBrandList(string stateid)
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetBrand]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@stateID",  stateid )
                            });

            this.dropBrand.DataSource = dsTemp.Tables[0];
            this.dropBrand.ValueMember = "brandid";
            this.dropBrand.DisplayMember = "brandname";
            this.dropBrand.SelectedIndex = 0;


            this.dropBrand2.DataSource = dsTemp.Tables[0];
            this.dropBrand2.ValueMember = "brandid";
            this.dropBrand2.DisplayMember = "brandname";
            this.dropBrand2.SelectedIndex = 0;
            //this.dropBrand.Text = "Not Allocated";

        }
        private void dropdownState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadBrandList(dropdownState.SelectedValue.ToString());
            label1.Text = dropdownState.SelectedValue.ToString();
        }

        private void loadInclusionPromotion()
        {
            try
            {
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetInclusionPromotion]", new System.Data.SqlClient.SqlParameter[2]
                    {
                        new System.Data.SqlClient.SqlParameter("@fkstateid", dropdownState.SelectedValue.ToString() ),
                        new System.Data.SqlClient.SqlParameter("@fkidbrand", dropBrand.SelectedValue.ToString() )
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
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = dsTemp.Tables[0];

            dataGridView1.Columns["idinclusionpromotion"].Visible = false;

            dataGridView1.Columns["promotionname"].HeaderText = "Promotion Name";
            dataGridView1.Columns["promotionname"].DisplayIndex = 0;
            dataGridView1.Columns["promotionname"].ReadOnly = true;
            dataGridView1.Columns["promotionname"].Width = 500;

            dataGridView1.Columns["stateabbreviation"].HeaderText = "State";
            dataGridView1.Columns["stateabbreviation"].DisplayIndex = 1;
            dataGridView1.Columns["stateabbreviation"].ReadOnly = true;

            dataGridView1.Columns["brandname"].HeaderText = "Brand";
            dataGridView1.Columns["brandname"].DisplayIndex = 2;
            dataGridView1.Columns["brandname"].ReadOnly = true;

            dataGridView1.Columns["fkidbaseproduct"].HeaderText = "Product ID";
            dataGridView1.Columns["fkidbaseproduct"].DisplayIndex = 3;
            dataGridView1.Columns["fkidbaseproduct"].ReadOnly = true;

            dataGridView1.Columns["active"].DisplayIndex = 4;
            dataGridView1.Columns["active"].HeaderText = "Active";

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadInclusionPromotion();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int active;
            if (dataGridView1.Rows[e.RowIndex].Cells["active"].Value.ToString().ToUpper() == "TRUE")
            {
                active = 1;
            }
            else
            {
                active = 0;
            }
            int idinclusionpromotion = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["idinclusionpromotion"].Value.ToString());
            try
            {
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminEditInclusionPromotion]", new System.Data.SqlClient.SqlParameter[3] 
                            {
                                new System.Data.SqlClient.SqlParameter("@idinclusionpromotion", idinclusionpromotion),
                                new System.Data.SqlClient.SqlParameter("@active", active),
                                new System.Data.SqlClient.SqlParameter("@usercode", MetriconCommon.UserCode )
                            });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dropState2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadBrandList(dropState2.SelectedValue.ToString());
            label1.Text = dropState2.SelectedValue.ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Text == "Edit Inclusion Promotion")
            {
                loadInclusionPromotion();
            }
            else
            {
                loadPromotionProducts();
            }
        }
        private void loadPromotionProducts()
        {
            try
            {
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetPromotionListForPackage]", new System.Data.SqlClient.SqlParameter[3]
                    {
                        new System.Data.SqlClient.SqlParameter("@productid", txtProductID.Text ),
                        new System.Data.SqlClient.SqlParameter("@productname", txtProductName.Text ),
                        new System.Data.SqlClient.SqlParameter("@stateid", dropState2.SelectedValue.ToString() )
                    });


                if (dsReg.Tables[0].Rows.Count >= 0)
                {
                    bindGrid2Data(dsReg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void bindGrid2Data(DataSet dsTemp)
        {
            //dataGridView2.Columns.Clear();
            dataGridView2.DataSource = dsTemp.Tables[0];

            dataGridView2.Columns["productareagroupid"].Visible = false;


            dataGridView2.Columns["productid"].HeaderText = "Product ID";
            dataGridView2.Columns["productid"].DisplayIndex = 1;
            dataGridView2.Columns["productid"].ReadOnly = true;
            dataGridView2.Columns["productid"].Width = 120;

            dataGridView2.Columns["productname"].HeaderText = "Product Name";
            dataGridView2.Columns["productname"].DisplayIndex = 2;
            dataGridView2.Columns["productname"].ReadOnly = true;
            dataGridView2.Columns["productname"].Width = 500;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadPromotionProducts();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtProductName.Text = "";
            txtProductID.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string pagid = "";
            string productid = "";
            bool anyrowselected = false;
            bool error = false;
            foreach (DataGridViewRow lvi in dataGridView2.Rows)
            {

                if (lvi.Cells["selected"].Value != null && bool.Parse(lvi.Cells["selected"].Value.ToString()))  //if the check box is ticked for this row
                {
                    anyrowselected = true;
                    if (pagid == "")
                    {
                        pagid = lvi.Cells["productareagroupid"].Value.ToString();
                        productid = lvi.Cells["productid"].Value.ToString();
                    }
                    else
                    {
                        pagid =pagid+","+lvi.Cells["productareagroupid"].Value.ToString();
                        productid =productid+","+ lvi.Cells["productid"].Value.ToString();
                    }

                    
                }
            }

            if (!anyrowselected)
            {
                MessageBox.Show("Please select at least one product!");
            }
            else
            {
                if (!error)
                {
                    try
                    {
                        DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminCreateInclusionPromotion]", new System.Data.SqlClient.SqlParameter[5]
                        {
                            new System.Data.SqlClient.SqlParameter("@productid", productid ),
                            new System.Data.SqlClient.SqlParameter("@fkstateid", dropState2.SelectedValue.ToString() ),
                            new System.Data.SqlClient.SqlParameter("@fkidbrand", dropBrand2.SelectedValue.ToString() ),
                            new System.Data.SqlClient.SqlParameter("@pagid", pagid ),
                            new System.Data.SqlClient.SqlParameter("@usercode", MetriconCommon.UserCode )
                        });

                        MessageBox.Show("Inclusion promotion created successfully!");
                        loadPromotionProducts();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                  
                }
            }
        }
    }
}