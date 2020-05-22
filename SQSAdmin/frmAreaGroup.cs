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
    public partial class frmAreaGroup : Form
    {

        DataSet dsArea, dsGroup,dsProduct;
        frmAreaGroupBasedOnHouse areaGroupByHouse;
        private int selectedAreaID, selectedGroupID;
        private string selectedAreaname, selectedGroupName;

        public frmAreaGroup()
        {
            InitializeComponent();
        }
        private void frmAreaGroup_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            panel1.Visible = false;
            panel2.Visible = false;
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            loadAreas();
            loadGroups();
            
            // set default area group selection
            selectedGroupID = Int32.Parse(dataGridView2.Rows[0].Cells["groupid"].Value.ToString());
            selectedGroupName = dataGridView2.Rows[0].Cells["groupName"].Value.ToString();
            selectedAreaID = Int32.Parse(dataGridView1.Rows[0].Cells["areaid"].Value.ToString());
            selectedAreaname = dataGridView1.Rows[0].Cells["areaName"].Value.ToString();
            loadProduct();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
        private void loadAreas()
        {
            string sqlStr;
            if (this.txtAreaName.Text.ToString() == "")
            {
                sqlStr = @"SELECT areaID,areaName,sortOrder,sortOrderDouble,active from area order by sortOrder,sortOrderDouble";
            }
            else
            {
                sqlStr = @"SELECT areaID,areaName,sortOrder,sortOrderDouble,active from area where areaName like '%" + txtAreaName.Text.ToString() + "%'  order by sortOrder,sortOrderDouble";
            }
            dsArea = MetriconCommon.DatabaseManager.ExecuteSQLQuery(sqlStr);
            bindingData(dsArea);  
        }
        private void bindingData(DataSet dsTemp)
        {
            dataGridView1.DataSource = dsTemp.Tables[0];
            dataGridView1.Columns[0].Visible = true;
            dataGridView1.Columns[0].HeaderText = "AreaID";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[0].Width = 30;
            dataGridView1.Columns[0].DisplayIndex = 0;

            dataGridView1.Columns[1].HeaderText = "Area Name";
            dataGridView1.Columns[1].ReadOnly = false;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[1].DisplayIndex = 1;

            dataGridView1.Columns[2].HeaderText = "Sort Order(Single)";
            dataGridView1.Columns[2].ReadOnly = false;
            dataGridView1.Columns[2].DisplayIndex = 2;
            dataGridView1.Columns[2].Width = 80;

            dataGridView1.Columns[3].HeaderText = "Sort Order(Double)";
            dataGridView1.Columns[3].ReadOnly = false;
            dataGridView1.Columns[3].DisplayIndex = 3;
            dataGridView1.Columns[3].Width = 80;

            dataGridView1.Columns[4].HeaderText = "Active";
            dataGridView1.Columns[4].ReadOnly = false;
            dataGridView1.Columns[4].DisplayIndex = 4;
            dataGridView1.Columns[4].Width = 60;

        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
                int areaID, active, sortOrder1,sortOrder2;
                string areaName;


                try
                {
                    areaID = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["areaid"].Value.ToString());
                    sortOrder1 = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["sortorder"].Value.ToString());
                    if (dataGridView1.Rows[e.RowIndex].Cells["sortorderDouble"].Value.ToString() != "")
                    {
                        sortOrder2 = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["sortorderDouble"].Value.ToString());
                    }
                    else
                    {
                        sortOrder2 = 0;
                    }
                    if ((bool)(dataGridView1.Rows[e.RowIndex].Cells["active"].Value))
                    {
                        active = 1;
                    }
                    else
                    {
                        active = 0;
                    }

                    areaName = dataGridView1.Rows[e.RowIndex].Cells["areaname"].Value.ToString();
                    if (areaName != "")
                    {
                        DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminAddEditArea]", new System.Data.SqlClient.SqlParameter[7]
                                {
					                  new System.Data.SqlClient.SqlParameter("@areaID", areaID)
                                    , new System.Data.SqlClient.SqlParameter("@areaName", areaName)
                                    , new System.Data.SqlClient.SqlParameter("@sortOrder", sortOrder1)
                                    , new System.Data.SqlClient.SqlParameter("@sortOrderDouble", sortOrder2)
					                , new System.Data.SqlClient.SqlParameter("@active", active)
                                    , new System.Data.SqlClient.SqlParameter("@action", "EDIT")
					                , new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode)
                                 });
                    }
                    else
                    {
                        MessageBox.Show("Please enter area name.");
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

        private void btnNewArea_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;

        }

        private void btnSaveNewArea_Click(object sender, EventArgs e)
        {
            
            int sortOrder,active,sortOrder2;

            if (txtNewAreaName.Text.ToString() != "")
            {

                sortOrder = MetriconCommon.initIntVariables(txtAreaSortOrder.Text.ToString());
                sortOrder2 = MetriconCommon.initIntVariables(txtDoubleSortOrder.Text.ToString());

                if (chkActive.Checked)
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }
                try
                {
                    MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminAddEditArea", new System.Data.SqlClient.SqlParameter[7] {
					  new System.Data.SqlClient.SqlParameter("@areaID", -1)
                    , new System.Data.SqlClient.SqlParameter("@areaName", txtNewAreaName.Text)
                    , new System.Data.SqlClient.SqlParameter("@sortOrder", sortOrder)
                    , new System.Data.SqlClient.SqlParameter("@sortOrderDouble", sortOrder2)
					, new System.Data.SqlClient.SqlParameter("@active", active)
                    , new System.Data.SqlClient.SqlParameter("@action", "NEW")
					, new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode)});
                }
                catch (SqlException ex1)
                {
                    MessageBox.Show(ex1.Message.ToString());
                }
                panel1.Visible = false;
                loadAreas();
            }
            else
            {
                MessageBox.Show("Please enter area name.");
            }
            
        }

        private void btnSearchArea_Click(object sender, EventArgs e)
        {
            loadAreas();
        }
        private void btnClearArea_Click(object sender, EventArgs e)
        {
            txtAreaName.Text = "";
            loadAreas();
        }
/*
 * *************************************************************************************
 *  groups
 * *************************************************************************************
 */


        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void loadGroups()
        {
            string sqlStr;
            if (this.txtGroupName.Text.ToString() == "")
            {
                sqlStr = @"SELECT groupID,groupName,sortOrder,active, studiomsortorder from [group] order by sortOrder";
            }
            else
            {
                sqlStr = @"SELECT groupID,groupName,sortOrder,active, studiomsortorder from [group]  where groupName like '%" + txtGroupName.Text.ToString() + "%' order by sortOrder";
            }
            dsGroup = MetriconCommon.DatabaseManager.ExecuteSQLQuery(sqlStr);
            bindingData2(dsGroup);
        }
        private void bindingData2(DataSet dsTemp)
        {
            dataGridView2.DataSource = dsTemp.Tables[0];
            dataGridView2.Columns[0].Visible = true;
            dataGridView2.Columns[0].HeaderText = "GroupID";
            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[0].Width = 30;
            dataGridView2.Columns[0].DisplayIndex = 0;

            dataGridView2.Columns[1].HeaderText = "Group Name";
            dataGridView2.Columns[1].ReadOnly = false;
            dataGridView2.Columns[1].Width = 180;
            dataGridView2.Columns[1].DisplayIndex = 1;
            

            dataGridView2.Columns[2].HeaderText = "Sort Order";
            dataGridView2.Columns[2].ReadOnly = false;
            dataGridView2.Columns[2].DisplayIndex = 2;
            dataGridView2.Columns[2].Width = 80;

            dataGridView2.Columns[3].HeaderText = "Active";
            dataGridView2.Columns[3].ReadOnly = false;
            dataGridView2.Columns[3].DisplayIndex = 3;
            dataGridView2.Columns[3].Width = 30;

            dataGridView2.Columns[4].HeaderText = "Studio M Order";
            dataGridView2.Columns[4].ReadOnly = false;
            dataGridView2.Columns[4].DisplayIndex = 4;
            dataGridView2.Columns[4].Width = 80;
            //dataGridView2.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void btnGroupSearch_Click(object sender, EventArgs e)
        {
            loadGroups();
        }

        private void btnGroupClear_Click(object sender, EventArgs e)
        {
            txtGroupName.Text = "";
            loadGroups();
        }

        private void btnSaveNewGroup_Click(object sender, EventArgs e)
        {
            int sortOrder, active, studiomsortorder;

            if (txtNewGroupName.Text.ToString() != "")
            {

                sortOrder = MetriconCommon.initIntVariables(txtNewGroupSortOrder.Text.ToString());
                studiomsortorder = MetriconCommon.initIntVariables(txtStudioMsortorder.Text.ToString());
                if (chkActive2.Checked)
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }
                try
                {
                    MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminAddEditGroup", new System.Data.SqlClient.SqlParameter[7] {
					  new System.Data.SqlClient.SqlParameter("@groupID", -1)
                    , new System.Data.SqlClient.SqlParameter("@groupName", txtNewGroupName.Text)
                    , new System.Data.SqlClient.SqlParameter("@sortOrder", txtNewGroupSortOrder.Text)
					, new System.Data.SqlClient.SqlParameter("@active", active)
                    , new System.Data.SqlClient.SqlParameter("@action", "NEW")
					, new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode)
                    , new System.Data.SqlClient.SqlParameter("@studiomsortorder", studiomsortorder)
                    });
                }
                catch (SqlException ex1)
                {
                    MessageBox.Show(ex1.Message.ToString());
                }
                panel2.Visible = false;
                loadGroups();
            }
            else
            {
                MessageBox.Show("Please enter group name.");
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int groupID, active, sortOrder2, studiomsortorder;
            string groupName;


            try
            {
                groupID = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells["groupid"].Value.ToString());
                sortOrder2 = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells["sortorder"].Value.ToString());
                studiomsortorder = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells["studiomsortorder"].Value.ToString());
                if ((bool)(dataGridView2.Rows[e.RowIndex].Cells["active"].Value))
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }

                groupName = dataGridView2.Rows[e.RowIndex].Cells["groupName"].Value.ToString();
                if (groupName != "")
                {
                    DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminAddEditGroup]", new System.Data.SqlClient.SqlParameter[7]
                                {
					                  new System.Data.SqlClient.SqlParameter("@groupID", groupID)
                                    , new System.Data.SqlClient.SqlParameter("@groupName", groupName)
                                    , new System.Data.SqlClient.SqlParameter("@sortOrder", sortOrder2)
					                , new System.Data.SqlClient.SqlParameter("@active", active)
                                    , new System.Data.SqlClient.SqlParameter("@action", "EDIT")
					                , new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode)
                                    , new System.Data.SqlClient.SqlParameter("@studiomsortorder", studiomsortorder)
                                 });
                }
                else
                {
                    MessageBox.Show("Please enter group name.");
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

        private void btnAreaCancel_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void btnGroupCancel_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void btnChangeByHouse_Click(object sender, EventArgs e)
        {
            if (areaGroupByHouse == null)
                areaGroupByHouse = new frmAreaGroupBasedOnHouse();

            areaGroupByHouse.pAreaID = selectedAreaID;
            areaGroupByHouse.pAreaName = selectedAreaname;
            areaGroupByHouse.pGroupID = 0;
            areaGroupByHouse.pGroupName = "";

            areaGroupByHouse.ShowDialog();

        }

        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    selectedGroupID = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells["groupid"].Value.ToString());
                    selectedGroupName = dataGridView2.Rows[e.RowIndex].Cells["groupName"].Value.ToString();
                    loadProduct();
                }
            }
            catch
            {
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    selectedAreaID = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["areaid"].Value.ToString());
                    selectedAreaname = dataGridView1.Rows[e.RowIndex].Cells["areaName"].Value.ToString();
                    loadProduct();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    selectedAreaID = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["areaid"].Value.ToString());
                    selectedAreaname = dataGridView1.Rows[e.RowIndex].Cells["areaName"].Value.ToString();
                    loadProduct();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int tempgroupID = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells["groupid"].Value.ToString());
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAreaFromGroup]", new System.Data.SqlClient.SqlParameter[1]
                                {
					                  new System.Data.SqlClient.SqlParameter("@groupID", tempgroupID)

                                 });
                bindingData(dsTemp);

            }
            catch
            {
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int tempareaID = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["areaid"].Value.ToString());
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetGroupFromArea]", new System.Data.SqlClient.SqlParameter[1]
                                {
					                  new System.Data.SqlClient.SqlParameter("@areaID", tempareaID)

                                 });
                bindingData2(dsTemp);

            }
            catch
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadAreas();
        }

        private void btnshowallgroup_Click(object sender, EventArgs e)
        {
            loadGroups();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    selectedGroupID = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells["groupid"].Value.ToString());
                    selectedGroupName = dataGridView2.Rows[e.RowIndex].Cells["groupName"].Value.ToString();
                }
                loadProduct();
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (areaGroupByHouse == null)
                areaGroupByHouse = new frmAreaGroupBasedOnHouse();

            areaGroupByHouse.pGroupID = selectedGroupID;
            areaGroupByHouse.pGroupName = selectedGroupName;
            areaGroupByHouse.pAreaID = 0;
            areaGroupByHouse.pAreaName = "";

            areaGroupByHouse.ShowDialog();
        }

///////////////////////////////////////////////////////////////////
////    product
////
///////////////////////////////////////////////////////////////////////

        private void loadProduct()
        {
            string sqlStr;
            if (this.txtProductName.Text.ToString() == "" && this.txtProductID.Text.ToString() == "")
            {
                sqlStr = @"SELECT distinct p.productID,productName,sortOrder,p.active,uom,productDescription from product p "+
                         @" inner join productAreaGroup pag on p.productID=pag.productID "+
                         @" where p.active=1 and p.fkstateid="+MetriconCommon.UserState+" and areaID=" + selectedAreaID.ToString() + @" and groupID=" + selectedGroupID.ToString() + @" order by sortOrder";
            }
            else
            {
                if (this.txtProductName.Text.ToString() == "" && this.txtProductID.Text.ToString() != "")
                    sqlStr = @"SELECT distinct p.productID,productName,sortOrder,p.active,uom,productDescription from product p " +
                         @" inner join productAreaGroup pag on p.productID=pag.productID "+
                         @" where  p.active=1 and p.fkstateid=" + MetriconCommon.UserState + " and areaID=" + selectedAreaID.ToString() + @" and groupID=" + selectedGroupID.ToString() +
                         @" and p.productID like '%" + txtProductID.Text.ToString() + "%'"+
                         @" order by sortOrder";

                    
                else if (this.txtProductName.Text.ToString() != "" && this.txtProductID.Text.ToString() == "")

                    sqlStr = @"SELECT distinct p.productID,productName,sortOrder,p.active,uom,productDescription from product p " +
                         @" inner join productAreaGroup pag on p.productID=pag.productID "+
                         @" where p.active=1 and p.fkstateid=" + MetriconCommon.UserState + " and areaID=" + selectedAreaID.ToString() + @" and groupID=" + selectedGroupID.ToString() +
                         @" and productName like '%" + txtProductName.Text.ToString() + "%'"+
                         @" order by sortOrder";

                    //sqlStr = @"SELECT productID,productName,sortOrder,active,uom,productDescription from product where productName like '%" + txtProductName.Text.ToString() + "%'  order by productName";
                else
                    sqlStr = @"SELECT distinct p.productID,productName,sortOrder,p.active,uom,productDescription from product p " +
                         @" inner join productAreaGroup pag on p.productID=pag.productID " +
                         @" where p.active=1 and p.fkstateid=" + MetriconCommon.UserState + " and areaID=" + selectedAreaID.ToString() + @" and groupID=" + selectedGroupID.ToString() +
                         @" and p.productID like '%" + txtProductID.Text.ToString() + "%' and productName like '%" + txtProductName.Text.ToString() + "%'" +
                         @" order by sortOrder";
                   // sqlStr = @"SELECT productID,productName,sortOrder,active,uom,productDescription from product where productID like '%" + txtProductID.Text.ToString() + "%' and productName like '%" + txtProductName.Text.ToString() + "%' order by productName";
            }
            dsProduct = MetriconCommon.DatabaseManager.ExecuteSQLQuery(sqlStr);
            if (dsProduct.Tables[0].Rows.Count == 0)
            {
                dataGridView3.DataSource = null;
            }
            else
            {
                bindingDataProduct(dsProduct);
            }
        }
        private void bindingDataProduct(DataSet dsTemp)
        {
            dataGridView3.DataSource = dsTemp.Tables[0];
            dataGridView3.Columns[0].Visible = true;
            dataGridView3.Columns[0].HeaderText = "ProductID";
            dataGridView3.Columns[0].ReadOnly = true;
            dataGridView3.Columns[0].Width = 80;
            dataGridView3.Columns[0].DisplayIndex = 0;

            dataGridView3.Columns[1].HeaderText = "Product Name";
            dataGridView3.Columns[1].ReadOnly = false;
            dataGridView3.Columns[1].Width = 180;
            dataGridView3.Columns[1].DisplayIndex = 1;

            dataGridView3.Columns[2].HeaderText = "Sort Order";
            dataGridView3.Columns[2].ReadOnly = false;
            dataGridView3.Columns[2].DisplayIndex = 2;
            dataGridView3.Columns[2].Width = 80;

            dataGridView3.Columns[3].HeaderText = "Active";
            dataGridView3.Columns[3].ReadOnly = false;
            dataGridView3.Columns[3].DisplayIndex = 3;
            dataGridView3.Columns[3].Width = 30;

            dataGridView3.Columns[4].Visible = false;
            dataGridView3.Columns[5].Visible = false;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            txtProductName.Text = "";
            txtProductID.Text = "";
            loadProduct();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            loadProduct();
        }
        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int  active, sortOrder3;
            string productName,productID,productDesc,uom;


            try
            {
                productID = dataGridView3.Rows[e.RowIndex].Cells["productid"].Value.ToString();
                sortOrder3 = Int32.Parse(dataGridView3.Rows[e.RowIndex].Cells["sortorder"].Value.ToString());
                productDesc = dataGridView3.Rows[e.RowIndex].Cells["productDescription"].Value.ToString();
                uom = dataGridView3.Rows[e.RowIndex].Cells["uom"].Value.ToString();
                if ((bool)(dataGridView3.Rows[e.RowIndex].Cells["active"].Value))
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }

                productName = dataGridView3.Rows[e.RowIndex].Cells["productName"].Value.ToString();
                if (productName != "")
                {
                    DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminEditProductSortOrderAndStatus]", new System.Data.SqlClient.SqlParameter[7]
                                {
					                  new System.Data.SqlClient.SqlParameter("@ProductID", productID)
                                    , new System.Data.SqlClient.SqlParameter("@productName", productName)
                                    , new System.Data.SqlClient.SqlParameter("@ProductDescription", productDesc)
                                    , new System.Data.SqlClient.SqlParameter("@uom", uom)
                                    , new System.Data.SqlClient.SqlParameter("@sortOrder", sortOrder3)
                                     , new System.Data.SqlClient.SqlParameter("@ModifiedBy", MetriconCommon.UserCode)
					                , new System.Data.SqlClient.SqlParameter("@active", active)
                                 });
                }
                else
                {
                    MessageBox.Show("Please enter product name.");
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