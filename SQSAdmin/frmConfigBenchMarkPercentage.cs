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
    public partial class frmConfigBenchMarkPercentage : Form
    {
        public frmConfigBenchMarkPercentage()
        {
            InitializeComponent();
        }
        private void frmConfigBenchMarkPercentage_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            tabControl1_SelectedIndexChanged(sender, e);

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Text == "Edit Benchmark")
            {
                loadStateDropDown(this.dropdownState);
                loadRegionDropdown();
                searchBenchmark();
            }
            else
            {
                loadStateDropDown(this.dropdownState2);
                newBenchmark();
                //disableGridView();
            }
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            disableGridView();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            disableSetAll();
        }

        private void disableGridView()
        {
            dropdownState2.Enabled = false;
            dataGridView2.Enabled = false;
            btnSaveConfig.Enabled = false;
            chkSelectAll.Enabled = false;

            btnSave.Enabled = true;
            txtEffective.Enabled = true;
            txtPercent.Enabled = true;
            txtEffective.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
        }
        private void disableSetAll()
        {
            dropdownState2.Enabled = true;
            dataGridView2.Enabled = true;
            btnSaveConfig.Enabled = true;
            chkSelectAll.Enabled = true;

            btnSave.Enabled = false;
            txtEffective.Enabled = false;
            txtPercent.Enabled = false;
            txtEffective.Text = "";
        }


        private void loadStateDropDown(ComboBox drop)
        {
            DataSet dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });
            drop.DataSource = dsState.Tables[0];
            drop.DisplayMember = "stateAbbreviation";
            drop.ValueMember = "stateID";
            drop.SelectedValue=MetriconCommon.UserState;
        }

        private void newBenchmark()
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
                    lvi.Cells["effectivedate"].Value = DateTime.Now.AddDays(1);
                }
            }
        }
        private void bindGrid1Data2(DataSet dsTemp)
        {

            DataColumn dc = new DataColumn();
            dc.ColumnName = "percentage";
            dsTemp.Tables[0].Columns.Add(dc);

            dataGridView2.DataSource = dsTemp.Tables[0];

            dataGridView2.Columns["regionid"].Visible = false;
            dataGridView2.Columns["percentage"].HeaderText = "Percentage";

            if (!dataGridView2.Columns.Contains("effectivedate"))
            {
                CalendarColumn col = new CalendarColumn();
                col.HeaderText = "Effective Date";
                col.Name = "effectivedate";
                col.Width = 120;
                col.ValueType = typeof(DateTime);

                dataGridView2.Columns.Add(col);
            }

        }

        private void dropdownState2_SelectionChangeCommitted(object sender, EventArgs e)
        {
            newBenchmark();
        }
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow lvi in dataGridView2.Rows)
            {
                lvi.Cells[0].Value = chkSelectAll.Checked;
            }
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            string regexp = @"^((\d?)|(([-+]?\d+\.?\d*)|([-+]?\d*\.?\d+))|(([-+]?\d+\.?\d*\,\ ?)*([-+]?\d+\.?\d*))|(([-+]?\d*\.?\d+\,\ ?)*([-+]?\d*\.?\d+))|(([-+]?\d+\.?\d*\,\ ?)*([-+]?\d*\.?\d+))|(([-+]?\d*\.?\d+\,\ ?)*([-+]?\d+\.?\d*)))$";
            string regionStr = "";
            string amountStr = "";
            string effStr = "";
            bool anyrowselected = false;
            bool error = false;

            dataGridView2.EndEdit();


            foreach (DataGridViewRow lvi in dataGridView2.Rows)
            {

                if (lvi.Cells["selected"].Value != null && bool.Parse(lvi.Cells["selected"].Value.ToString()))  //if the check box is ticked for this row
                {
                    anyrowselected = true;
                    //validate the amount or percentage based on the drop down value by amount or by percentage.
                    if (lvi.Cells["percentage"].Value != null && lvi.Cells["percentage"].Value.ToString() != "")
                    {

                        if (Regex.IsMatch(lvi.Cells["percentage"].Value.ToString(), regexp) && DateTime.Parse(lvi.Cells["effectivedate"].Value.ToString()) > DateTime.Now && (Decimal.Parse(lvi.Cells["percentage"].Value.ToString()) <= 100 && Decimal.Parse(lvi.Cells["percentage"].Value.ToString()) > 0))
                        {
                            if (regionStr == "")
                            {
                                regionStr = lvi.Cells["regionid"].Value.ToString();
                                effStr = DateTime.Parse(lvi.Cells["effectivedate"].Value.ToString()).ToString("dd/MMM/yyyy");
                                amountStr = lvi.Cells["percentage"].Value.ToString();
                            }
                            else
                            {
                                regionStr = regionStr + "," + lvi.Cells["regionid"].Value.ToString();
                                effStr = effStr + "," + DateTime.Parse(lvi.Cells["effectivedate"].Value.ToString()).ToString("dd/MMM/yyyy");
                                amountStr = amountStr + "," + lvi.Cells["percentage"].Value.ToString();
                            }

                        }
                        else
                        {
                            if (DateTime.Parse(lvi.Cells["effectivedate"].Value.ToString()) <= DateTime.Now)
                            {
                                MessageBox.Show("Please select a future date as effective date!");
                            }
                            else if ((Double.Parse(lvi.Cells["percentage"].Value.ToString()) > 100) && (Double.Parse(lvi.Cells["percentage"].Value.ToString()) <= 0.0))
                            {
                                MessageBox.Show("Please enter a percentage between 0 to 100!");
                            }
                            else
                            {
                                MessageBox.Show("Data type error! Please enter a number for percentage column!");
                            }
                            error = true;
                            break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Data type error! Percentage column can not be empty!");
                        error = true;
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
                    saveData(regionStr, amountStr, effStr);
                }
            }
        }

        private void saveData(string region, string amount, string date)
        {
            try
            {

                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_adminSaveBenchmarkConfig]", new System.Data.SqlClient.SqlParameter[4]
                            {
                                new System.Data.SqlClient.SqlParameter("@region", region ),
                                new System.Data.SqlClient.SqlParameter("@amount", amount ),
                                new System.Data.SqlClient.SqlParameter("@effdate", date ),
                                new System.Data.SqlClient.SqlParameter("@usercode", MetriconCommon.UserCode )
                            });

                newBenchmark();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchBenchmark();
        }
        private void searchBenchmark()
        {
            string date = "";
            try
            {
                if (txtDate.Text != "") date = DateTime.Parse(txtDate.Text).ToString("dd/MMM/yyyy");
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetBenchmark]", new System.Data.SqlClient.SqlParameter[3]
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
        private void bindGrid1Data(DataSet dsTemp)
        {
            dataGridView1.DataSource = dsTemp.Tables[0];

            dataGridView1.Columns["idbenchmarkpercentage"].Visible = false;
            dataGridView1.Columns["fkidregion"].Visible=false;

            dataGridView1.Columns["regionname"].HeaderText = "Region";
            dataGridView1.Columns["regionname"].ReadOnly = true;
            dataGridView1.Columns["regionname"].Width = 120;

            dataGridView1.Columns["percentage"].HeaderText = "Percentage";

            dataGridView1.Columns["effectivedate"].HeaderText = "Effective Date";
            dataGridView1.Columns["effectivedate"].Width = 100;

            dataGridView1.Columns["active"].HeaderText = "Active";
            dataGridView1.Columns["active"].Width = 40;

        }

        private void dropdownState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            loadRegionDropdown();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int priceID, active;
            decimal percentage;

            try
            {
                priceID = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["idbenchmarkpercentage"].Value.ToString());
                percentage = Decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells["percentage"].Value.ToString());
                if ((bool)(dataGridView1.Rows[e.RowIndex].Cells["active"].Value))
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }

                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminUpdateBenchmarkPercentage]", new System.Data.SqlClient.SqlParameter[4]
                            {
                                new System.Data.SqlClient.SqlParameter("@benchmarkID",priceID)
                                ,new System.Data.SqlClient.SqlParameter("@percentage",percentage)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            string regexp=@"\d+(\.\d{1,2})?";

            if (txtPercent.Text!="" && txtEffective.Text!="")
            {
                try
                {
                    DateTime d = DateTime.Parse(txtEffective.Text);
                    if (Regex.IsMatch(txtPercent.Text, regexp))
                    {
                        saveAll();
                    }
                    else
                    {
                        MessageBox.Show("Please enter a percentage in number!");
                    }
                }
                catch (FormatException fe)
                {
                    MessageBox.Show("Please enter a valid effective date!");
                }

            }
            else
            {
                if (txtPercent.Text == "")
                {
                    MessageBox.Show("Please enter a percentage in number!");
                }
                else
                {
                    MessageBox.Show("Please enter a valid effective date!");
                }
            }

        }
        private void saveAll()
        {
            try
            {
                string date=DateTime.Parse(txtEffective.Text).ToString("dd/MMM/yyyy");
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_adminSaveBenchmarkConfigForAllRegion]", new System.Data.SqlClient.SqlParameter[3]
                            {
                                new System.Data.SqlClient.SqlParameter("@amount", txtPercent.Text ),
                                new System.Data.SqlClient.SqlParameter("@effdate", date),
                                new System.Data.SqlClient.SqlParameter("@usercode", MetriconCommon.UserCode )
                            });

                MessageBox.Show("Configuration is saved successfully!");
                txtPercent.Text = "";
                txtEffective.Text = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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