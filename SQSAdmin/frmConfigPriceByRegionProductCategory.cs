using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using SQSAdmin.Common;

namespace SQSAdmin
{
    public partial class frmConfigPriceByRegionProductCategory : Form
    {
        public string callFrom;
        public string CallFrom
        {
            get { return callFrom; }
            set { callFrom = value; }
		}

        public frmConfigPriceByRegionProductCategory()
        {
            InitializeComponent();
        }


        private void frmConfigPriceByRegionProductCategory_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            tabControl1_SelectedIndexChanged(sender,e);

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void loadStateDropDown(ComboBox drop)
        {
            DataSet dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });
            drop.DataSource = dsState.Tables[0];
            drop.DisplayMember = "stateAbbreviation";
            drop.ValueMember = "stateID";
            drop.SelectedValue= MetriconCommon.UserState;
        }


        private void loadRegionDropdown()
        {

            DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetRegion]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@stateID", dropdownState.SelectedValue.ToString() )
                            });
            if (dsReg.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsReg.Tables[0].NewRow();
                dr["regionid"] = 0;
                dr["Regionname"] = "All";
                dsReg.Tables[0].Rows.Add(dr);

                dropRegion.DataSource = dsReg.Tables[0];
                dropRegion.DisplayMember = "regionName";
                dropRegion.ValueMember = "regionID";

                dropRegion.SelectedValue = 0;
            }
            else
            {
                dropRegion.DataSource = null;
                dropRegion.Items.Clear();
            }

        }

        private void dropdownState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadRegionDropdown();
        }

        private void searchConfiguration()
        {
            string date = "";
            try
            {
                if (txtDate.Text!="") date = DateTime.Parse(txtDate.Text).ToString("dd/MMM/yyyy");
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetPriceConfiguration]", new System.Data.SqlClient.SqlParameter[3]
                    {
                        new System.Data.SqlClient.SqlParameter("@stateid", dropdownState.SelectedValue.ToString() ),
                        new System.Data.SqlClient.SqlParameter("@regionid", dropRegion.SelectedValue.ToString() ),
                        new System.Data.SqlClient.SqlParameter("@effectiveDate",date)
                    });


                if (dsReg.Tables[0].Rows.Count >= 0)
                {
                    bindGrid1Data(dsReg);
                }
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Please enter correct date (dd/mm/yyyy)!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }




        }

        private void newConfiguration()
        {

            DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetRegion]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@stateID", dropdownState2.SelectedValue.ToString() )
                            });

            if (dsReg.Tables[0].Rows.Count >= 0)
            {
                bindGrid1Data2(dsReg);
                // set default value for the dropdown
                foreach (DataGridViewRow lvi in dataGridView2.Rows)
                {
                    lvi.Cells["bytype"].Value = "By Percent";
                    lvi.Cells["effectivedate"].Value = DateTime.Now.AddDays(1);
                    lvi.Cells["productcat"].Value = 0;
                }
            }



        }


        private void bindGrid1Data(DataSet dsTemp)
        {
            dataGridView1.DataSource = dsTemp.Tables[0];

            dataGridView1.Columns[0].Visible = false;

            dataGridView1.Columns[1].HeaderText = "Region";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].Width = 120;

            dataGridView1.Columns[2].HeaderText = "Type";
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[2].Width = 100;

            dataGridView1.Columns[3].HeaderText = "Product Category";
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[3].Width=140;

            dataGridView1.Columns[4].HeaderText = "Amount/Percent";
            dataGridView1.Columns[4].ReadOnly = true;

            dataGridView1.Columns[5].HeaderText = "Effective Date";
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[5].ReadOnly = true;

            dataGridView1.Columns[6].HeaderText = "Active";
            dataGridView1.Columns[6].Width = 40;

        }

        private void bindGrid1Data2(DataSet dsTemp)
        {

            DataColumn dc = new DataColumn();
            dc.ColumnName = "valueamount";
            dsTemp.Tables[0].Columns.Add(dc);

            dataGridView2.DataSource = dsTemp.Tables[0];

            dataGridView2.Columns["regionid"].Visible = false;
            dataGridView2.Columns["valueamount"].HeaderText = "Value/Amount";
            dataGridView2.Columns["bytype"].DisplayIndex=3 ;

            if (!dataGridView2.Columns.Contains("effectivedate"))
            {
                CalendarColumn col = new CalendarColumn();
                col.HeaderText = "Effective Date";
                col.Name = "effectivedate";
                col.Width = 120;
                col.ValueType = typeof(DateTime);
 
                dataGridView2.Columns.Add(col);
            }
            if (!dataGridView2.Columns.Contains("productcat"))
            {
                DataGridViewComboBoxColumn comboboxColumn = new DataGridViewComboBoxColumn();
                comboboxColumn.HeaderText = "Product Category";
                comboboxColumn.Name = "productcat";
                comboboxColumn.Width = 250;
                comboboxColumn.FlatStyle = FlatStyle.Flat;
                comboboxColumn = populateData(comboboxColumn);
            
                dataGridView2.Columns.Add(comboboxColumn);
                dataGridView2.Columns["productcat"].DisplayIndex=4;
            }

        }

        public DataGridViewComboBoxColumn populateData(DataGridViewComboBoxColumn col)
        {
            try
            {

                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetProductCategoryCode]", new System.Data.SqlClient.SqlParameter[0]
                            {
                            });
                if (dsReg.Tables[0].Rows.Count > 0)
                {
                    col.DataSource = dsReg.Tables[0];
                    col.DisplayMember = "productcat";
                    col.ValueMember = "productcategoryID";
                }
                return col;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Text == "Edit Configuration")
            {
                loadStateDropDown(this.dropdownState);
                loadRegionDropdown();
                searchConfiguration();
            }
            else
            {
                dropProductCat.Visible = false;
                txtProductCat.Visible = false;

                loadStateDropDown(this.dropdownState2);
                newConfiguration();
            }
        }

        private void dropdownState2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            newConfiguration();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow lvi in dataGridView2.Rows)
            {
                lvi.Cells[0].Value = chkSelectAll.Checked;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string regexp = @"^((\d?)|(([-+]?\d+\.?\d*)|([-+]?\d*\.?\d+))|(([-+]?\d+\.?\d*\,\ ?)*([-+]?\d+\.?\d*))|(([-+]?\d*\.?\d+\,\ ?)*([-+]?\d*\.?\d+))|(([-+]?\d+\.?\d*\,\ ?)*([-+]?\d*\.?\d+))|(([-+]?\d*\.?\d+\,\ ?)*([-+]?\d+\.?\d*)))$";
            string regionStr="";
            string typeStr = "";
            string amountStr = "";
            string effStr = "";
            string productcatStr = "";
            bool anyrowselected = false;
            bool error=false;

            dataGridView2.EndEdit();


            foreach (DataGridViewRow lvi in dataGridView2.Rows)
            {

                if (lvi.Cells["selected"].Value != null && bool.Parse(lvi.Cells["selected"].Value.ToString()))  //if the check box is ticked for this row
                {
                    anyrowselected = true;
                    //validate the amount or percentage based on the drop down value by amount or by percentage.
                    if (lvi.Cells["valueamount"].Value != null && lvi.Cells["valueamount"].Value.ToString() != "")
                    {

                        if (Regex.IsMatch(lvi.Cells["valueamount"].Value.ToString(), regexp) && DateTime.Parse(lvi.Cells["effectivedate"].Value.ToString()) > DateTime.Now)
                        {
                            if (regionStr == "")
                            {
                                regionStr = lvi.Cells["regionid"].Value.ToString();
                                effStr = DateTime.Parse(lvi.Cells["effectivedate"].Value.ToString()).ToString("dd/MMM/yyyy");
                                productcatStr = lvi.Cells["productcat"].Value.ToString();
                                amountStr = lvi.Cells["valueamount"].Value.ToString();
                            }
                            else
                            {
                                regionStr = regionStr + "," + lvi.Cells["regionid"].Value.ToString();
                                effStr = effStr + "," + DateTime.Parse(lvi.Cells["effectivedate"].Value.ToString()).ToString("dd/MMM/yyyy");
                                amountStr = amountStr + "," + lvi.Cells["valueamount"].Value.ToString();
                                productcatStr = productcatStr + "," + lvi.Cells["productcat"].Value.ToString();
                            }
                            if (lvi.Cells["bytype"].Value.ToString().ToUpper() == "BY PERCENT")
                            {
                                if (typeStr == "")
                                {
                                    typeStr = "1";
                                }
                                else
                                {
                                    typeStr = typeStr + ",1";
                                }
                            }
                            else
                            {
                                if (typeStr == "")
                                {
                                    typeStr = "2";
                                }
                                else
                                {
                                    typeStr = typeStr + ",2";
                                }
                            }
                        }
                        else
                        {
                            if (DateTime.Parse(lvi.Cells["effectivedate"].Value.ToString()) <= DateTime.Now)
                            {
                                MessageBox.Show("Please select a future date as effective date!");
                            }
                            else
                            {
                                MessageBox.Show("Data type error! Please enter a number for Amount/Percent column!");
                            }
                            error = true;
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data type error! Amount/Percent column can not be empty!");
                        error=true;
                        break;
                    }

                }
            }

            if (!anyrowselected)
            {
                MessageBox.Show("Please select at least one region!");
            }
            else
            {
                if (!error)
                {
                    saveData(regionStr, typeStr, amountStr, effStr,productcatStr);
                }
            }
        }
        private void saveData(string region,string type,string amount,string date,string productcat)
        {
            try
            {

                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_adminSavePriceConfig]", new System.Data.SqlClient.SqlParameter[6]
                            {
                                new System.Data.SqlClient.SqlParameter("@region", region ),
                                new System.Data.SqlClient.SqlParameter("@type", type ),
                                new System.Data.SqlClient.SqlParameter("@amount", amount ),
                                new System.Data.SqlClient.SqlParameter("@effdate", date ),
                                new System.Data.SqlClient.SqlParameter("@productcat", productcat ),
                                new System.Data.SqlClient.SqlParameter("@usercode", MetriconCommon.UserCode )
                            });

                newConfiguration();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchConfiguration();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int priceID, active;

            try
            {
                priceID = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["idSQSAdminPriceConfig"].Value.ToString());
                if ((bool)(dataGridView1.Rows[e.RowIndex].Cells["active"].Value))
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }

                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminChangePriceConfigStatus]", new System.Data.SqlClient.SqlParameter[3]
                            {
                                new System.Data.SqlClient.SqlParameter("@priceconfigID",priceID)
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

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow lvi in dataGridView1.Rows)
            {
                if (DateTime.Parse(lvi.Cells["effectivedate"].Value.ToString()) <= DateTime.Now)
                {
                    lvi.Cells["active"].ReadOnly = true;
                    lvi.DefaultCellStyle.BackColor = Color.LightGray;
                }

            }
        }
    }
}