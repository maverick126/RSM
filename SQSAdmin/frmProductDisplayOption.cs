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
    public partial class frmProductDisplayOption : Form
    {
        frmProductLookup productLookupForm;
        string productID = "";
        DataRow row;
        string displayid = "";
        public frmProductDisplayOption()
        {
            InitializeComponent();
        }

        private void frmProductDisplayOption_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            ClearForm();
            if (productLookupForm == null)
                productLookupForm = new frmProductLookup();
            productLookupForm.ShowDialog();

            row = productLookupForm.SelectedRow;

            if (row != null)
            {
                productID = row["ProductID"].ToString();
                loadArea();
                loadGroup();
                getStatistics();
                LoadProductDetails();
                LoadPriceList();
                loadPAGList();
                loadDisplayList();
            }
            else
            {
                this.Close();
            }

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
        private void LoadProductDetails()
        {
            try
            {
                
                txtProductID.Text = productID;
                txtProductName.Text = row["ProductName"].ToString();

                chkActive.Checked = bool.Parse(row["Active"].ToString());
                chkBCActive.Checked = bool.Parse(row["BCActive"].ToString());
                chkminiStart.Checked = bool.Parse(row["minibillStart"].ToString());
                chkminiComplete.Checked = bool.Parse(row["minibillComplete"].ToString());
                if (row["packageflyerpromo"].ToString() == "1")
                {
                    chkFlyerPromo.Checked = true;
                }
                else
                {
                    chkFlyerPromo.Checked = false;
                }


                //if (row["fkProductCategoryID"].ToString() != "")
                //{
                //    dropProductCategory.SelectedValue = row["fkProductCategoryID"];
                //}
                //if (row["fkProductCodeID"].ToString() != "")
                //{
                //    dropProductCode.SelectedValue = row["fkProductCodeID"];
                //}
                //if (row["fkCostCentreCodeID"].ToString() != "")
                //{
                //    dropCostCentre.SelectedValue = row["fkCostCentreCodeID"];
                //}
                //if (row["fkPriceDisplayCodeID"].ToString() != "")
                //{
                //    dropPriceDisplayCode.SelectedValue = row["fkPriceDisplayCodeID"];
                //}
                //if (row["uom"].ToString() != "")
                //{
                //    dropUOM.SelectedValue = row["uom"];
                //}

                //dropdownState.SelectedValue = row["fkStateID"];

                if (row["defaultQty"].ToString() != "")
                {
                    txtDefaultQty.Text = row["defaultQty"].ToString();
                }
                if (row["defaultareaid"].ToString() != "")
                {
                    dropArea.SelectedValue = row["defaultareaid"].ToString();
                }
                if (row["defaultgroupid"].ToString() != "")
                {
                    dropGroup.SelectedValue = row["defaultgroupid"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void ClearForm()
        {
            txtProductID.Text = "";
            //txtProductName.Text = "";
            //txtProductDescription.Text = "";
            //dropUOM.Text = "";
            //txtSortOrder.Text = "";
            //chkActive.Checked = true;
            //chkBCActive.Checked = true;
            //chkminiStart.Checked = false;
            //chkminiComplete.Checked = false;
            //chkFlyerPromo.Checked = false;
            //dropArea.SelectedIndex = -1;
            //dropGroup.SelectedIndex = -1;
            //txtDefaultQty.Text = "";
            //dropProductCode.SelectedIndex = 1;
            //dropProductCategory.SelectedIndex = 1;
            //dropPriceDisplayCode.SelectedIndex = 1;
            //dropCostCentre.SelectedIndex = 1;
        }


        public void getStatistics()
        {
            try
            {
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetProductConfigureStatisticsByProductID]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@productID", productID  )
                            });
                txtHomeCount.Text = dsTemp.Tables[0].Rows[0]["vanillahomecount"].ToString();
                txtDisplayCount.Text = dsTemp.Tables[0].Rows[0]["displayhomecount"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void loadArea()
        {
            DataSet dsAllAreas = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAllAreas]", new System.Data.SqlClient.SqlParameter[0] { });
            dropArea.ValueMember = "areaid";
            dropArea.DisplayMember = "areaname";
            dropArea.DataSource = dsAllAreas.Tables[0];
            dropArea.SelectedIndex = -1;
        }

        private void loadGroup()
        {
            DataSet dsAllGroup = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAllGroups]", new System.Data.SqlClient.SqlParameter[0] { });
            dropGroup.ValueMember = "groupid";
            dropGroup.DisplayMember = "groupname";
            dropGroup.DataSource = dsAllGroup.Tables[0];
            dropGroup.SelectedIndex = -1;
        }

        private void btnSaveBasic_Click(object sender, EventArgs e)
        {
            string defaultareaid, defaultgroupid;
            double defaultqty;
            int bcactive, packagepromo, minibillstart, minibillcomplete;

            try
            {
                if (txtDefaultQty.Text != "") defaultqty = Double.Parse(txtDefaultQty.Text); else defaultqty = 0.0;
                if (dropArea.SelectedIndex != -1)
                {
                    defaultareaid = dropArea.SelectedValue.ToString();
                }
                else
                {
                    defaultareaid = "0";
                }

                if (dropGroup.SelectedIndex != -1)
                {
                    defaultgroupid = dropGroup.SelectedValue.ToString();
                }
                else
                {
                    defaultgroupid = "0";
                }

                if (chkBCActive.Checked) bcactive = 1; else bcactive = 0;
                if (chkFlyerPromo.Checked) packagepromo = 1; else packagepromo = 0;
                if (chkminiStart.Checked) minibillstart = 1; else minibillstart = 0;
                if (chkminiComplete.Checked) minibillcomplete = 1; else minibillcomplete = 0;

                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminUpdateProductBasicAttributes",
                                    new System.Data.SqlClient.SqlParameter[8] 
                                {   new System.Data.SqlClient.SqlParameter("@ProductID", this.productID) ,
                                    new System.Data.SqlClient.SqlParameter("@bcactive", bcactive),
                                    new System.Data.SqlClient.SqlParameter("@packagepromo", packagepromo),
                                    new System.Data.SqlClient.SqlParameter("@minibillstart", minibillstart),
                                    new System.Data.SqlClient.SqlParameter("@minibillcomplete", minibillcomplete),
                                    new System.Data.SqlClient.SqlParameter("@defaultareaid", defaultareaid),
                                    new System.Data.SqlClient.SqlParameter("@defaultgroupid", defaultgroupid),
                                    new System.Data.SqlClient.SqlParameter("@defaultqty", defaultqty)

                                }
                                    );

                MessageBox.Show("Attributes of this product has been updated!");
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Please enter a number in default qty!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadPriceList()
        {

            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminSearchPrice", new System.Data.SqlClient.SqlParameter[1] { new System.Data.SqlClient.SqlParameter("@ProductID", this.productID) });
            bindDataToGrid(dsTemp);
        }

        private void bindDataToGrid(DataSet dsTemp)
        {
            dataGridView1.DataSource = dsTemp.Tables[0];
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[0].Width = 0;
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[1].HeaderText = "Effective Date";
            dataGridView1.Columns[1].DataPropertyName = "effectiveDate";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[1].DisplayIndex = 1;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;


            dataGridView1.Columns[4].Visible = true;
            dataGridView1.Columns[4].ReadOnly = true;
            dataGridView1.Columns[4].HeaderText = "Sell Price";
            dataGridView1.Columns[4].Width = 80;
            dataGridView1.Columns[4].DataPropertyName = "sellprice";
            dataGridView1.Columns[4].DisplayIndex = 3;
            dataGridView1.Columns[4].DefaultCellStyle.Format = "c";
            dataGridView1.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;

            dataGridView1.Columns[10].HeaderText = "Active";
            dataGridView1.Columns[10].ReadOnly = false;
            dataGridView1.Columns[10].DataPropertyName = "active";
            dataGridView1.Columns[10].Width = 60;
            dataGridView1.Columns[10].DisplayIndex = 5;

            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible = false;
            dataGridView1.Columns[17].Visible = false;
            dataGridView1.Columns[18].Visible = true;
            dataGridView1.Columns[18].HeaderText = "Region";
            dataGridView1.Columns[18].ReadOnly = true;
            dataGridView1.Columns[18].DataPropertyName = "regionname";
            dataGridView1.Columns[18].DisplayIndex = 4;

            dataGridView1.Columns[19].Visible = false;
        }

        private void loadPAGList()
        {

            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminGetAllPAGsByProductID", new System.Data.SqlClient.SqlParameter[1] { new System.Data.SqlClient.SqlParameter("@ProductID", this.productID) });
            bindDataToPAGGrid(dsTemp);
        }

        private void bindDataToPAGGrid(DataSet dsTemp)
        {
            dataGridView2.DataSource = dsTemp.Tables[0];
            dataGridView2.Columns[0].Visible = false;
            dataGridView2.Columns[0].Width = 0;
            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].Visible = true;
            dataGridView2.Columns[2].HeaderText = "Area Name";
            dataGridView2.Columns[2].DataPropertyName = "areaname";
            dataGridView2.Columns[2].ReadOnly = true;
            dataGridView2.Columns[2].Width = 100;
            dataGridView2.Columns[2].DisplayIndex = 1;
            
            dataGridView2.Columns[3].Visible = false;


            dataGridView2.Columns[4].Visible = true;
            dataGridView2.Columns[4].ReadOnly = true;
            dataGridView2.Columns[4].HeaderText = "Group";
            dataGridView2.Columns[4].Width = 80;
            dataGridView2.Columns[4].DataPropertyName = "groupname";
            dataGridView2.Columns[4].DisplayIndex = 2;


            dataGridView2.Columns[5].HeaderText = "Promo";
            dataGridView2.Columns[5].ReadOnly = true;
            dataGridView2.Columns[5].DataPropertyName = "promo";
            dataGridView2.Columns[5].Width = 40;
            dataGridView2.Columns[5].DisplayIndex =3;

            dataGridView2.Columns[6].HeaderText = "Active";
            dataGridView2.Columns[6].ReadOnly = false;
            dataGridView2.Columns[6].DataPropertyName = "active";
            dataGridView2.Columns[6].Width = 40;
            dataGridView2.Columns[6].DisplayIndex = 4;

       }

       private void loadDisplayList()
       {

           DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminGetDisplayHomeByProduct", new System.Data.SqlClient.SqlParameter[1] { new System.Data.SqlClient.SqlParameter("@ProductID", this.productID) });
           bindDataToDisplayGrid(dsTemp);
       }

        private void bindDataToDisplayGrid(DataSet dsTemp)
       {
           dataGridView3.DataSource = dsTemp.Tables[0];
           dataGridView3.Columns[0].Visible = true;
           dataGridView3.Columns[0].Width = 90;
           dataGridView3.Columns[0].HeaderText = "Home ID";
           dataGridView3.Columns[0].ReadOnly = true;
           dataGridView3.Columns[0].DisplayIndex = 1;

           dataGridView3.Columns[1].Visible = true;
           dataGridView3.Columns[1].HeaderText = "Home Name";
           dataGridView3.Columns[1].DataPropertyName = "homename";
           dataGridView3.Columns[1].ReadOnly = true;
           dataGridView3.Columns[1].Width = 240;
           dataGridView3.Columns[1].DisplayIndex = 2;




           dataGridView3.Columns[2].Visible = true;
           dataGridView3.Columns[2].ReadOnly = true;
           dataGridView3.Columns[2].HeaderText = "Suburb";
           dataGridView3.Columns[2].Width = 160;
           dataGridView3.Columns[2].DataPropertyName = "suburb";
           dataGridView3.Columns[2].DisplayIndex = 3;

           dataGridView3.Columns[3].Visible = false;
           dataGridView3.Columns[4].Visible = false;


       }


        private void loadDisplayPAGList()
        {

            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminGetAllPAGByProductAndDisplayhome", 
                                new System.Data.SqlClient.SqlParameter[2] 
                                { new System.Data.SqlClient.SqlParameter("@ProductID", this.productID) ,
                                  new System.Data.SqlClient.SqlParameter("@displayhomeid", this.displayid)
                                }
                                );
            bindDataToDisplayPAGGrid(dsTemp);
        }

        private void bindDataToDisplayPAGGrid(DataSet dsTemp)
        {
            dataGridView4.DataSource = dsTemp.Tables[0];
            dataGridView4.Columns[0].Visible = true;
            dataGridView4.Columns[0].Width = 60;
            dataGridView4.Columns[0].HeaderText = "PAG";
            dataGridView4.Columns[0].ReadOnly = true;

            dataGridView4.Columns[1].Visible = true;
            dataGridView4.Columns[1].HeaderText = "Area Name";
            dataGridView4.Columns[1].DataPropertyName = "areaname";
            dataGridView4.Columns[1].ReadOnly = true;
            dataGridView4.Columns[1].Width = 100;


            dataGridView4.Columns[2].Visible = true;
            dataGridView4.Columns[2].ReadOnly = true;
            dataGridView4.Columns[2].HeaderText = "Group Name";
            dataGridView4.Columns[2].Width = 120;
            dataGridView4.Columns[2].DataPropertyName = "groupname";


            dataGridView4.Columns[3].Visible = true;
            dataGridView4.Columns[3].ReadOnly = false;
            dataGridView4.Columns[3].HeaderText = "Qty";
            dataGridView4.Columns[3].Width = 50;
            dataGridView4.Columns[3].DataPropertyName = "quantity";

            dataGridView4.Columns[4].Visible = true;
            dataGridView4.Columns[4].ReadOnly = false;
            dataGridView4.Columns[4].HeaderText = "Active";
            dataGridView4.Columns[4].Width = 60;
            dataGridView4.Columns[4].DataPropertyName = "active";


        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataRow row = ((DataRowView)this.dataGridView3.Rows[e.RowIndex].DataBoundItem).Row;
                displayid = row["homeid"].ToString();

                label9.Text = row["homename"].ToString() + " -- " + row["suburb"].ToString();
                loadDisplayPAGList();
            }
        }

        private void dataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int active=1;
            try
            {
                DataRow modifiedRow = ((DataRowView)this.dataGridView4.Rows[e.RowIndex].DataBoundItem).Row;
                if (bool.Parse(modifiedRow["active"].ToString()))
                {
                    active=1;
                }
                else
                {
                    active=0;
                }
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminUpdatePAGStatusAndAuantityByDisplayHome",
                                  new System.Data.SqlClient.SqlParameter[5] 
                                { new System.Data.SqlClient.SqlParameter("@pagID",modifiedRow["productareagroupid"].ToString()) ,
                                  new System.Data.SqlClient.SqlParameter("@displayhomeid", this.displayid),
                                  new System.Data.SqlClient.SqlParameter("@qty", modifiedRow["quantity"].ToString()),
                                  new System.Data.SqlClient.SqlParameter("@active", active),
                                  new System.Data.SqlClient.SqlParameter("@modifiedby", MetriconCommon.UserCode)
                                }
                                    );
                getStatistics();
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

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int active = 1;
            try
            {
                DataRow modifiedRow = ((DataRowView)this.dataGridView2.Rows[e.RowIndex].DataBoundItem).Row;
                if (bool.Parse(modifiedRow["active"].ToString()))
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminEditPAG]", new System.Data.SqlClient.SqlParameter[5]
                            {
					            new System.Data.SqlClient.SqlParameter("@ProductAreaGroupID", modifiedRow["productareagroupid"].ToString())
					            ,new System.Data.SqlClient.SqlParameter("@AreaID",modifiedRow["areaID"].ToString())
					            ,new System.Data.SqlClient.SqlParameter("@GroupID", modifiedRow["groupID"].ToString())
					            ,new System.Data.SqlClient.SqlParameter("@Active",active)
					            , new System.Data.SqlClient.SqlParameter("@ModifiedBy",MetriconCommon.UserCode)
                            });
                getStatistics();
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

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            int priceID, active;

            try
            {
                priceID = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["priceID"].Value.ToString());
                if ((bool)(dataGridView1.Rows[e.RowIndex].Cells["active"].Value))
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }

                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminChangePriceStatus]", new System.Data.SqlClient.SqlParameter[3]
                            {
					            new System.Data.SqlClient.SqlParameter("@priceID",priceID)
					            ,new System.Data.SqlClient.SqlParameter("@Active",active)
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

    }
}