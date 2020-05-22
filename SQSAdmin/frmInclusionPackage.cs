using SQSAdmin.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQSAdmin
{
    public partial class frmInclusionPackage : Form
    {
        public frmInclusionPackage()
        {
            InitializeComponent();
        }

        private void frmInclusionPackage_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            loadRegionGroup();
            loadBrand();
            loadType();

            searchInclusionPackage();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void loadRegionGroup()
        {

            DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetRegionGroup]", new System.Data.SqlClient.SqlParameter[1]
                            {
                                new System.Data.SqlClient.SqlParameter("@stateid", MetriconCommon.State )
                            });
            if (dsReg.Tables[0].Rows.Count > 0)
            {
                //DataRow dr = dsReg.Tables[0].NewRow();

                //dr["idregiongroup"] = "0";
                //dr["regiongroupname"] = "All";

                //dsReg.Tables[0].Rows.InsertAt(dr, 0);

                dropRegionGroup.DataSource = dsReg.Tables[0];
                dropRegionGroup.DisplayMember = "regionGroupName";
                dropRegionGroup.ValueMember = "idRegionGroup";
                dropRegionGroup.SelectedIndex = 0;

                dropNewRegionGroup.DataSource = dsReg.Tables[0];
                dropNewRegionGroup.DisplayMember = "regionGroupName";
                dropNewRegionGroup.ValueMember = "idRegionGroup";
                dropNewRegionGroup.SelectedIndex = 0;
            }
            else
            {
                dropRegionGroup.DataSource = null;
                dropRegionGroup.Items.Clear();

                dropNewRegionGroup.DataSource = null;
                dropNewRegionGroup.Items.Clear();
            }

        }

        private void loadBrand()
        {

            DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetBrand]", new System.Data.SqlClient.SqlParameter[1] 
                            {
                                new System.Data.SqlClient.SqlParameter("@stateid", MetriconCommon.State )
                            });
            if (dsReg.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsReg.Tables[0].NewRow();

                dr["brandid"] = "0";
                dr["brandname"] = "All";

                dsReg.Tables[0].Rows.InsertAt(dr, 0);

                dropBrand.DataSource = dsReg.Tables[0];
                dropBrand.DisplayMember = "brandname";
                dropBrand.ValueMember = "brandid";
                dropBrand.SelectedValue = 0;

                dropNewBrand.DataSource = dsReg.Tables[0];
                dropNewBrand.DisplayMember = "brandname";
                dropNewBrand.ValueMember = "brandid";
                dropNewBrand.SelectedValue = 0;
            }
            else
            {
                dropBrand.DataSource = null;
                dropBrand.Items.Clear();

                dropNewBrand.DataSource = null;
                dropNewBrand.Items.Clear();
            }
        }

        private void loadType()
        {

            DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetInclusionType]", new System.Data.SqlClient.SqlParameter[0] 
                            {
                            });
            if (dsReg.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dsReg.Tables[0].NewRow();

                dr["idInclusionType"] = "0";
                dr["Inclusionname"] = "All";

                dsReg.Tables[0].Rows.InsertAt(dr, 0);

                dropType.DataSource = dsReg.Tables[0];
                dropType.DisplayMember = "Inclusionname";
                dropType.ValueMember = "idInclusionType";
                dropType.SelectedValue = 0;

                dropNewType.DataSource = dsReg.Tables[0];
                dropNewType.DisplayMember = "Inclusionname";
                dropNewType.ValueMember = "idInclusionType";
                dropNewType.SelectedValue = 0;
            }
            else
            {
                dropType.DataSource = null;
                dropType.Items.Clear();

                dropNewType.DataSource = null;
                dropNewType.Items.Clear();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dropNewBrand.SelectedValue.ToString() != "0" && dropNewRegionGroup.SelectedValue.ToString() != "0" && dropNewType.SelectedValue.ToString() != "0")
            {
                if (!inclusionPackageExists("0", dropNewRegionGroup.SelectedValue.ToString(), dropNewBrand.SelectedValue.ToString(), dropNewType.SelectedValue.ToString()))
                {
                    saveDetails();
                    dropNewBrand.SelectedValue = "0";
                    dropNewRegionGroup.SelectedValue = "0";
                    dropNewType.SelectedValue = "0";
                    chkActive.Checked = true;
                    searchInclusionPackage();
                }
                else
                {
                    MessageBox.Show("The inclusion package exist! Can't make the change!");
                }

            }
            else
            {
                if (dropNewBrand.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("Please select a brand!");
                }
                else if (dropNewRegionGroup.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("Please select a region group!");
                }
                else if (dropNewType.SelectedValue.ToString() == "0")
                {
                    MessageBox.Show("Please select a inclusion type!");
                }
                
            }
        }
        private void saveDetails()
        {
            try
            {

                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminAddInclusionPackage]", new System.Data.SqlClient.SqlParameter[5] 
                            {
                                new System.Data.SqlClient.SqlParameter("@fkidregiongroup", dropNewRegionGroup.SelectedValue.ToString()),
                                new System.Data.SqlClient.SqlParameter("@fkidbrand",dropNewBrand.SelectedValue.ToString() ),
                                new System.Data.SqlClient.SqlParameter("@fkidInclusionType", dropNewType.SelectedValue.ToString()),
                                new System.Data.SqlClient.SqlParameter("@active", chkActive.Checked),
                                new System.Data.SqlClient.SqlParameter("@usercode", MetriconCommon.UserCode )
                            });

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void searchInclusionPackage()
        {
            try
            {
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetInclusionPackage]", new System.Data.SqlClient.SqlParameter[3]
                    {
                        new System.Data.SqlClient.SqlParameter("@fkidregiongroup", dropRegionGroup.SelectedValue.ToString() ),
                        new System.Data.SqlClient.SqlParameter("@fkidbrand", dropBrand.SelectedValue.ToString() ),
                        new System.Data.SqlClient.SqlParameter("@fkidInclusionType",dropType.SelectedValue.ToString())
                    });


                if (dsReg.Tables[0].Rows.Count >= 0)
                {
                    bindGrid1Data(dsReg);
                    //foreach (DataGridViewRow lvi in dataGridView1.Rows)
                    //{
                    //    lvi.Cells["dropregiongroup"].Value = Int32.Parse(lvi.Cells["fkidregiongroup"].Value.ToString());
                    //    lvi.Cells["dropbrand"].Value = Int32.Parse(lvi.Cells["fkidbrand"].Value.ToString());
                    //    lvi.Cells["droptype"].Value = Int32.Parse(lvi.Cells["fkidInclusionType"].Value.ToString());
                    //}
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

            dataGridView1.Columns["idinclusionpackage"].Visible = false;
            dataGridView1.Columns["fkidregiongroup"].Visible = false;
            dataGridView1.Columns["fkidbrand"].Visible = false;
            dataGridView1.Columns["fkidInclusionType"].Visible = false;
            //dataGridView1.Columns["regiongroupname"].Visible = false;
            //dataGridView1.Columns["brandname"].Visible = false;
            //dataGridView1.Columns["inclusionname"].Visible = false;


            //if (!dataGridView1.Columns.Contains("dropregiongroup"))
            //{
            //    DataGridViewComboBoxColumn comboboxColumn = new DataGridViewComboBoxColumn();
            //    comboboxColumn.HeaderText = "Region Group";
            //    comboboxColumn.Name = "dropregiongroup";
            //    comboboxColumn.Width = 200;
            //    comboboxColumn.FlatStyle = FlatStyle.Flat;
            //    comboboxColumn = populateRegionGroup(comboboxColumn);

            //    dataGridView1.Columns.Add(comboboxColumn);
            //    dataGridView1.Columns["dropregiongroup"].DisplayIndex = 0;
            //}
            //if (!dataGridView1.Columns.Contains("dropbrand"))
            //{
            //    DataGridViewComboBoxColumn comboboxColumn = new DataGridViewComboBoxColumn();
            //    comboboxColumn.HeaderText = "Brand";
            //    comboboxColumn.Name = "dropbrand";
            //    comboboxColumn.Width = 120;
            //    comboboxColumn.FlatStyle = FlatStyle.Flat;
            //    comboboxColumn = populateBrand(comboboxColumn);

            //    dataGridView1.Columns.Add(comboboxColumn);
            //    dataGridView1.Columns["dropbrand"].DisplayIndex = 1;
            //}
            //if (!dataGridView1.Columns.Contains("droptype"))
            //{
            //    DataGridViewComboBoxColumn comboboxColumn = new DataGridViewComboBoxColumn();
            //    comboboxColumn.HeaderText = "Inclusion Type";
            //    comboboxColumn.Name = "droptype";
            //    comboboxColumn.Width = 120;
            //    comboboxColumn.FlatStyle = FlatStyle.Flat;
            //    comboboxColumn = populateType(comboboxColumn);

            //    dataGridView1.Columns.Add(comboboxColumn);
            //    dataGridView1.Columns["droptype"].DisplayIndex = 2;
            //}
            dataGridView1.Columns["regiongroupname"].HeaderText = "Region Group";
            dataGridView1.Columns["regiongroupname"].DisplayIndex = 0;
            dataGridView1.Columns["regiongroupname"].ReadOnly = true;

            dataGridView1.Columns["brandname"].HeaderText = "Brand";
            dataGridView1.Columns["brandname"].DisplayIndex = 1;
            dataGridView1.Columns["brandname"].ReadOnly = true;

            dataGridView1.Columns["inclusionname"].HeaderText = "Inclusion Type";
            dataGridView1.Columns["inclusionname"].DisplayIndex = 2;
            dataGridView1.Columns["inclusionname"].ReadOnly = true;

            dataGridView1.Columns["active"].DisplayIndex = 3;
            dataGridView1.Columns["active"].HeaderText = "Active";

        }
        public DataGridViewComboBoxColumn populateRegionGroup(DataGridViewComboBoxColumn col)
        {
            try
            {

                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetRegionGroup]", new System.Data.SqlClient.SqlParameter[0]
                            {
                            });
                if (dsReg.Tables[0].Rows.Count > 0)
                {
                    col.DataSource = dsReg.Tables[0];
                    col.DisplayMember = "regiongroupname";
                    col.ValueMember = "idregiongroup";
                }
                return col;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        public DataGridViewComboBoxColumn populateBrand(DataGridViewComboBoxColumn col)
        {
            try
            {

                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetBrand]", new System.Data.SqlClient.SqlParameter[1] 
                            {
                                new System.Data.SqlClient.SqlParameter("@stateid", "1" )
                            });
                if (dsReg.Tables[0].Rows.Count > 0)
                {
                    col.DataSource = dsReg.Tables[0];
                    col.DisplayMember = "brandname";
                    col.ValueMember = "brandid";
                }
                return col;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
        public DataGridViewComboBoxColumn populateType(DataGridViewComboBoxColumn col)
        {
            try
            {

                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetInclusionType]", new System.Data.SqlClient.SqlParameter[0] 
                            {
                            });
                if (dsReg.Tables[0].Rows.Count > 0)
                {
                    col.DataSource = dsReg.Tables[0];
                    col.DisplayMember = "inclusionname";
                    col.ValueMember = "idinclusiontype";
                }
                return col;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchInclusionPackage();
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
            int idinclusionpackage = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["idinclusionpackage"].Value.ToString());
            try
            {
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminEditInclusionPackage]", new System.Data.SqlClient.SqlParameter[3] 
                            {
                                new System.Data.SqlClient.SqlParameter("@idinclusionpackage", idinclusionpackage),
                                new System.Data.SqlClient.SqlParameter("@active", active),
                                new System.Data.SqlClient.SqlParameter("@usercode", MetriconCommon.UserCode )
                            });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //if (!inclusionPackageExists(dataGridView1.Rows[e.RowIndex].Cells["idinclusionpackage"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["dropregiongroup"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["dropbrand"].Value.ToString(), dataGridView1.Rows[e.RowIndex].Cells["droptype"].Value.ToString()))
            //{
            //    MessageBox.Show("changed!");
            //}
            //else
            //{
            //    MessageBox.Show("The inclusion package exist! Can't make the change!");
            //}
        }
        private bool inclusionPackageExists(string packageid, string regiongroupid, string brandid, string typeid)
        {
            bool result = true;
            try
            {
                DataSet dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminCheckInclusionPackageExists]", new System.Data.SqlClient.SqlParameter[4] 
                            {
                                new System.Data.SqlClient.SqlParameter("@idinclusionpackage", packageid),
                                new System.Data.SqlClient.SqlParameter("@fkidregiongroup", regiongroupid),
                                new System.Data.SqlClient.SqlParameter("@fkidbrand", brandid),
                                new System.Data.SqlClient.SqlParameter("@fkidinclusiontype", typeid)
                            });
                if (dsReg.Tables[0].Rows[0]["result"].ToString() == "1")
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(ex.Message);
            }
            return result;
        }


    }
}