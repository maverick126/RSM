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
    public partial class frmInclusionPackageLookUp : Form
    {
        private DataRow selectedPackage;

        public DataRow SelectedPackage
        {
            get { return selectedPackage; }
            set { selectedPackage = value; }
        }
        public frmInclusionPackageLookUp()
        {
            InitializeComponent();
        }

        private void frmInclusionPackageLookUp_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            loadRegionGroup();
            loadBrand();
            loadType();

            searchInclusionPackage();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
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
            }
            else
            {
                dropRegionGroup.DataSource = null;
                dropRegionGroup.Items.Clear();
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
            }
            else
            {
                dropBrand.DataSource = null;
                dropBrand.Items.Clear();
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
            }
            else
            {
                dropType.DataSource = null;
                dropType.Items.Clear();
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataRow row = ((DataRowView)this.dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
                this.SelectedPackage = row;
                this.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchInclusionPackage();
        }
    }
}