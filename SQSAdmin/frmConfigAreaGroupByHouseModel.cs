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
    public partial class frmConfigAreaGroupByHouseModel : Form
    {
        private frmSelectHouseModel homeLookupForm;
        private int selectedAreaID, selectedGroupID;
        private string selectedAreaname, selectedGroupName;
        DataSet dsArea, dsGroup, dsProduct;

        public frmConfigAreaGroupByHouseModel()
        {
            InitializeComponent();
        }

        private void frmConfigAreaGroupByHouseModel_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            if (homeLookupForm == null)
                homeLookupForm = new frmSelectHouseModel();
            homeLookupForm.ShowDialog();

            label4.Text = "Areas and Groups Configuration For " + homeLookupForm.House.ToString();

            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();
            loadAreas();
            loadGroups();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
        private void loadAreas()
        {
            dsArea = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_GetAreaByHouse]", new System.Data.SqlClient.SqlParameter[3]
                                {
                                      new System.Data.SqlClient.SqlParameter("@stateid", homeLookupForm.HouseState.ToString())
					                , new System.Data.SqlClient.SqlParameter("@homemodel", homeLookupForm.House.ToString())
                                    , new System.Data.SqlClient.SqlParameter("@pname", txtAreaName.Text)
                                 });
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
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].Width = 180;
            dataGridView1.Columns[1].DisplayIndex = 1;

            dataGridView1.Columns[2].HeaderText = "Sort Order(Single)";
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[2].DisplayIndex = 2;
            dataGridView1.Columns[2].Width = 80;

            dataGridView1.Columns[3].HeaderText = "Sort Order(Double)";
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[3].DisplayIndex = 3;
            dataGridView1.Columns[3].Width = 80;

            dataGridView1.Columns[4].HeaderText = "Active";
            dataGridView1.Columns[4].ReadOnly = false;
            dataGridView1.Columns[4].DisplayIndex = 4;
            dataGridView1.Columns[4].Width = 60;

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int areaID, active, sortOrder1, sortOrder2;
            string areaName;

            if (((DataRowView)this.dataGridView1.Rows[e.RowIndex].DataBoundItem).Row.RowState != DataRowState.Unchanged)
            {

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
                        DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminAddEditAreaByHomeModel]", new System.Data.SqlClient.SqlParameter[9]
                                {
                                    new System.Data.SqlClient.SqlParameter("@stateID", homeLookupForm.HouseState.ToString())
					                , new System.Data.SqlClient.SqlParameter("@areaID", areaID)
                                    , new System.Data.SqlClient.SqlParameter("@areaName", areaName)
                                    , new System.Data.SqlClient.SqlParameter("@sortOrder", sortOrder1)
                                    , new System.Data.SqlClient.SqlParameter("@sortOrderDouble", sortOrder2)
					                , new System.Data.SqlClient.SqlParameter("@active", active)
                                    , new System.Data.SqlClient.SqlParameter("@action", "EDIT")
					                , new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode)
                                    , new System.Data.SqlClient.SqlParameter("@homemodel",homeLookupForm.House.ToString())
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
        private void button1_Click(object sender, EventArgs e)
        {
            loadAreas();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex > 0)
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

                    // Populate PAG for a selected area
                    if (e.ColumnIndex >= 0 && e.ColumnIndex <= 1)
                    {
                        bool active = ((bool)(dataGridView1.Rows[e.RowIndex].Cells["active"].Value));
                        DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetPAGByAreaHomeModelActive]", new System.Data.SqlClient.SqlParameter[4]
                                {
                                    new System.Data.SqlClient.SqlParameter("@stateid", homeLookupForm.HouseState.ToString()),
                                    new System.Data.SqlClient.SqlParameter("@homemodel", homeLookupForm.House.ToString()),
                                    new System.Data.SqlClient.SqlParameter("@areaID", selectedAreaID),
                                    new System.Data.SqlClient.SqlParameter("@hdoactive", active)
                                });
                        label2.Text = "PAG details for selected area - " + selectedAreaname;
                        bindingPAGDetailGrid(dsTemp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int selectedAreaID = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["areaid"].Value.ToString());
                
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetGroupFromAreaForOneHouseModel]", new System.Data.SqlClient.SqlParameter[2]
                                {
					                  new System.Data.SqlClient.SqlParameter("@areaID", selectedAreaID),
                                      new System.Data.SqlClient.SqlParameter("@homemodel", homeLookupForm.House.ToString())
                                 });
                
                bindingData2(dsTemp);

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void bindingPAGDetailGrid(DataSet dsTemp)
        {
            dataGridView4.DataSource = dsTemp.Tables[0];
            dataGridView4.Columns[0].HeaderText = "PAG ID";
            dataGridView4.Columns[0].ReadOnly = true;
            dataGridView4.Columns[0].Width = 80;
            dataGridView4.Columns[0].DisplayIndex = 0;

            dataGridView4.Columns[1].HeaderText = "ProductID";
            dataGridView4.Columns[1].ReadOnly = true;
            dataGridView4.Columns[1].Width = 120;
            dataGridView4.Columns[1].DisplayIndex = 1;

            dataGridView4.Columns[2].Visible = false;

            dataGridView4.Columns[3].HeaderText = "Area";
            dataGridView4.Columns[3].ReadOnly = true;
            dataGridView4.Columns[3].Width = 120;
            dataGridView4.Columns[3].DisplayIndex = 2;

            dataGridView4.Columns[4].Visible = false;

            dataGridView4.Columns[5].HeaderText = "Group";
            dataGridView4.Columns[5].ReadOnly = true;
            dataGridView4.Columns[5].Width = 150;
            dataGridView4.Columns[5].DisplayIndex = 3;

            dataGridView4.Columns[6].HeaderText = "HDO Active";
            dataGridView4.Columns[6].ReadOnly = false;
            dataGridView4.Columns[6].Width = 80;
            dataGridView4.Columns[6].DisplayIndex = 4;

            dataGridView4.Columns[7].Visible = false;
            dataGridView4.Columns[8].Visible = false;

            dataGridView4.Columns[9].HeaderText = "Home";
            dataGridView4.Columns[9].ReadOnly = false;
            dataGridView4.Columns[9].Width = 180;
            dataGridView4.Columns[9].DisplayIndex = 5;


        }
        //==========================================================================
        //     group
        //
        //==========================================================================
        private void loadGroups()
        {
            dsGroup = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_GetGroupByHouse]", new System.Data.SqlClient.SqlParameter[3]
                                {
                                       new System.Data.SqlClient.SqlParameter("@stateid", homeLookupForm.HouseState.ToString())
					                ,  new System.Data.SqlClient.SqlParameter("@homemodel", homeLookupForm.House.ToString())
                                    , new System.Data.SqlClient.SqlParameter("@pname", txtGroupName.Text)
                                 });
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

        }
        private void btnshowallgroup_Click(object sender, EventArgs e)
        {
            loadGroups();
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
        private void dataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                int tempgroupID = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells["groupid"].Value.ToString());
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAreaFromGroupForOneHomeModel]", new System.Data.SqlClient.SqlParameter[2]
                                {
					                  new System.Data.SqlClient.SqlParameter("@groupID", tempgroupID),
                                      new System.Data.SqlClient.SqlParameter("@homemodel", homeLookupForm.House.ToString())

                                 });
                bindingData(dsTemp);

            }
            catch
            {
            }
        }
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int groupID, active, sortOrder2;
            string groupName;
            //if (((DataRowView)this.dataGridView2.Rows[e.RowIndex].DataBoundItem).Row.RowState != DataRowState.Unchanged)
            //{

                try
                {
                    groupID = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells["groupid"].Value.ToString());  
                    sortOrder2 = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells["sortorder"].Value.ToString());

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
                        DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminAddEditGroupByHomeModel]", new System.Data.SqlClient.SqlParameter[9]
                                {
					                  new System.Data.SqlClient.SqlParameter("@stateid", homeLookupForm.HouseState.ToString())
                                    , new System.Data.SqlClient.SqlParameter("@groupID", groupID)
                                    , new System.Data.SqlClient.SqlParameter("@groupName", groupName)
                                    , new System.Data.SqlClient.SqlParameter("@sortOrder", sortOrder2)
					                , new System.Data.SqlClient.SqlParameter("@active", active)
                                    , new System.Data.SqlClient.SqlParameter("@action", "EDIT")
					                , new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode)
                                    , new System.Data.SqlClient.SqlParameter("@homemodel", homeLookupForm.House.ToString())
                                    , new System.Data.SqlClient.SqlParameter("@areaID", selectedAreaID)
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
            //}

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    selectedGroupID = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells["groupid"].Value.ToString());
                    selectedGroupName = dataGridView2.Rows[e.RowIndex].Cells["groupName"].Value.ToString();
                    loadProduct();

                    // Populate PAG for a selected area
                    if (e.ColumnIndex >= 0 && e.ColumnIndex <= 1)
                    {
                        bool active = ((bool)(dataGridView2.Rows[e.RowIndex].Cells["active"].Value));
                        DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetPAGByGroupHomeModelActive]", new System.Data.SqlClient.SqlParameter[5]
                                {
                                    new System.Data.SqlClient.SqlParameter("@stateid", homeLookupForm.HouseState.ToString()),
                                    new System.Data.SqlClient.SqlParameter("@homemodel", homeLookupForm.House.ToString()),
                                    new System.Data.SqlClient.SqlParameter("@areaID", selectedAreaID),
                                    new System.Data.SqlClient.SqlParameter("@groupID", selectedGroupID),
                                    new System.Data.SqlClient.SqlParameter("@hdoactive", active)
                                });
                        label2.Text = "PAG details for selected area - "+selectedAreaname+" and group - " + selectedGroupName;
                        bindingPAGDetailGrid(dsTemp);
                    }
                }
            }
            catch
            {
            }
        }

///////////////////////////////////////////////////////////////////
////    product
////
///////////////////////////////////////////////////////////////////////

        private void loadProduct()
        {
            string sqlStr;


            //if (this.txtProductName.Text.ToString() == "" && this.txtProductID.Text.ToString() == "")
            //{
            //    sqlStr = @"SELECT distinct p.productID,productName,sortOrder,hdo.active,uom,productDescription from homedisplayoption hdo "+
            //             @" inner join productAreaGroup pag on hdo.productAreaGroupID=pag.productAreaGroupID and  homeid in (select homeid from home where fkstateid=" + homeLookupForm.HouseState.ToString() + " and homename like '%" + homeLookupForm.House.ToString() + "%' and active=1 and parenthomeid is null) and areaID=" + selectedAreaID.ToString() + @" and groupID=" + selectedGroupID.ToString() +
            //             @" inner join product p on p.productID=pag.productID where p.fkstateid="+MetriconCommon.UserState+" and  homedisplayid is null " +
            //             @" order by sortOrder";
            //}
            //else
            //{
            //    if (this.txtProductName.Text.ToString() == "" && this.txtProductID.Text.ToString() != "")
            //    sqlStr = @"SELECT distinct p.productID,productName,sortOrder,hdo.active,uom,productDescription from homedisplayoption hdo "+
            //             @" inner join productAreaGroup pag on hdo.productAreaGroupID=pag.productAreaGroupID and  homeid in (select homeid from home where fkstateid=" + homeLookupForm.HouseState.ToString() + " and  homename like '%" + homeLookupForm.House.ToString() + "%' and active=1 and parenthomeid is null) and areaID=" + selectedAreaID.ToString() + @" and groupID=" + selectedGroupID.ToString() +
            //             @" inner join product p on p.productID=pag.productID " +
            //             @" where  p.fkstateid=" + MetriconCommon.UserState + " and productID like '%" + txtProductID.Text.ToString() + "%'  and homedisplayid is null " +
            //             @" order by sortOrder";

                    
            //    else if (this.txtProductName.Text.ToString() != "" && this.txtProductID.Text.ToString() == "")

            //    sqlStr = @"SELECT distinct p.productID,productName,sortOrder,hdo.active,uom,productDescription from homedisplayoption hdo " +
            //             @" inner join productAreaGroup pag on hdo.productAreaGroupID=pag.productAreaGroupID and  homeid in (select homeid from home where fkstateid=" + homeLookupForm.HouseState.ToString() + " and  homename like '%" + homeLookupForm.House.ToString() + "%' and active=1 and parenthomeid is null) and areaID=" + selectedAreaID.ToString() + @" and groupID=" + selectedGroupID.ToString() +
            //             @" inner join product p on p.productID=pag.productID " +
            //             @" where  p.fkstateid=" + MetriconCommon.UserState + " and productName like '%" + txtProductName.Text.ToString() + "%' and homedisplayid is null " +
            //             @" order by sortOrder";

            //        //sqlStr = @"SELECT productID,productName,sortOrder,active,uom,productDescription from product where productName like '%" + txtProductName.Text.ToString() + "%'  order by productName";
            //    else
            //    sqlStr = @"SELECT distinct p.productID,productName,sortOrder,hdo.active,uom,productDescription from homedisplayoption hdo " +
            //             @" inner join productAreaGroup pag on hdo.productAreaGroupID=pag.productAreaGroupID and  homeid in (select homeid from home where fkstateid=" + homeLookupForm.HouseState.ToString() + " and  homename like '%" + homeLookupForm.House.ToString() + "%' and active=1 and parenthomeid is null) and areaID=" + selectedAreaID.ToString() + @" and groupID=" + selectedGroupID.ToString() +
            //             @" inner join product p on p.productID=pag.productID " +
            //             @" where  p.fkstateid=" + MetriconCommon.UserState + " and productID like '%" + txtProductID.Text.ToString() + "%' and productName like '%" + txtProductName.Text.ToString() + "%' and homedisplayid is null " +
            //             @" order by sortOrder";
            //       // sqlStr = @"SELECT productID,productName,sortOrder,active,uom,productDescription from product where productID like '%" + txtProductID.Text.ToString() + "%' and productName like '%" + txtProductName.Text.ToString() + "%' order by productName";
            //}
            //dsProduct = MetriconCommon.DatabaseManager.ExecuteSQLQuery(sqlStr);
            DataSet dsProduct = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetProductByHomeAreaGroup]", new System.Data.SqlClient.SqlParameter[6]
                                {
                                    new System.Data.SqlClient.SqlParameter("@homename", homeLookupForm.House.ToString()),
                                    new System.Data.SqlClient.SqlParameter("@areaID", selectedAreaID),
                                    new System.Data.SqlClient.SqlParameter("@groupID", selectedGroupID),
                                    new System.Data.SqlClient.SqlParameter("@productname", txtProductName.Text),
                                    new System.Data.SqlClient.SqlParameter("@productid", txtProductID.Text),
                                    new System.Data.SqlClient.SqlParameter("@stateid", MetriconCommon.UserState)
                                });
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
            dataGridView3.Columns[1].ReadOnly = true;
            dataGridView3.Columns[1].Width = 180;
            dataGridView3.Columns[1].DisplayIndex = 1;

            dataGridView3.Columns[2].HeaderText = "Sort Order";
            dataGridView3.Columns[2].ReadOnly = true;
            dataGridView3.Columns[2].DisplayIndex = 2;
            dataGridView3.Columns[2].Width = 80;

            dataGridView3.Columns[3].HeaderText = "Active";
            dataGridView3.Columns[3].ReadOnly = true;
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
                    DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_EditProductByHouse]", new System.Data.SqlClient.SqlParameter[10]
                                {
					                  new System.Data.SqlClient.SqlParameter("@ProductID", productID)
                                    , new System.Data.SqlClient.SqlParameter("@productName", productName)
                                    , new System.Data.SqlClient.SqlParameter("@ProductDescription", productDesc)
                                    , new System.Data.SqlClient.SqlParameter("@uom", uom)
                                    , new System.Data.SqlClient.SqlParameter("@sortOrder", sortOrder3)
                                     , new System.Data.SqlClient.SqlParameter("@ModifiedBy", MetriconCommon.UserCode)
					                , new System.Data.SqlClient.SqlParameter("@active", active)
                                    , new System.Data.SqlClient.SqlParameter("@areaid", selectedAreaID)
                                    , new System.Data.SqlClient.SqlParameter("@groupid", selectedGroupID)
                                    , new System.Data.SqlClient.SqlParameter("@homemodel", homeLookupForm.House.ToString())
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

        private void button9_Click_1(object sender, EventArgs e)
        {
            loadProduct();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            txtProductID.Text = "";
            txtProductName.Text = "";
            loadProduct();
        }

        private void dataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int active, pagID,homeID;

            try
            {
                pagID = Int32.Parse(dataGridView4.Rows[e.RowIndex].Cells["productareagroupid"].Value.ToString());
                homeID = Int32.Parse(dataGridView4.Rows[e.RowIndex].Cells["homeid"].Value.ToString());
                if ((bool)(dataGridView4.Rows[e.RowIndex].Cells["hdo active"].Value))
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }


                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_EditPAGByHouse]", new System.Data.SqlClient.SqlParameter[4]
                            {
				                  new System.Data.SqlClient.SqlParameter("@pagID", pagID)
                                , new System.Data.SqlClient.SqlParameter("@homeID", homeID)
                                , new System.Data.SqlClient.SqlParameter("@active", active)
                                , new System.Data.SqlClient.SqlParameter("@ModifiedBy", MetriconCommon.UserCode)
				                
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