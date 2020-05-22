using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SQSAdmin.Common;

namespace SQSAdmin
{
    public partial class frmSelectHouseModel : Form
    {
        DataSet dsBrand;
        bool finishedLoadingBrand = false;
        string housemodel;
        string state;
        public string House
        {
            get { return housemodel; }
            set { housemodel = value; }
        }
        public string HouseState
        {
            get { return state; }
            set { state = value; }
        }
        public frmSelectHouseModel()
        {
            InitializeComponent();
        }

        private void frmSelectHouseModel_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            this.House = "";
            LoadStateDropDown();
            LoadBrandList();
            LoadHomeList();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void LoadStateDropDown()
        {
            DataSet ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });

            dropdownState.DataSource = ds.Tables[0];
            dropdownState.ValueMember = "stateid";
            dropdownState.DisplayMember = "stateAbbreviation";
            dropdownState.SelectedValue = MetriconCommon.UserState;

        }

        private void LoadBrandList()
        {
            DataSet ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetBrand]", new System.Data.SqlClient.SqlParameter[1] { new SqlParameter("@stateID", dropdownState.SelectedValue.ToString()) } );

            dropBrand.DataSource = ds.Tables[0];
            dropBrand.ValueMember = "brandid";
            dropBrand.DisplayMember = "brandname";
            dropBrand.SelectedIndex = 0;

            finishedLoadingBrand = true;
        }
        private void LoadHomeList()
        {
            int brandID = Int32.Parse(dropBrand.SelectedValue.ToString());
            DataSet dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminGetHomeModel", new SqlParameter[1] { new SqlParameter("@brandID", brandID) });
            bindingData(dsHome);
            this.House = dsHome.Tables[0].Rows[0]["homemodel"].ToString();
            this.HouseState = dsHome.Tables[0].Rows[0]["fkStateid"].ToString();

        }
        private void bindingData(DataSet dsTemp)
        {
            dataGridView1.DataSource = dsTemp.Tables[0];

            dataGridView1.Columns[0].HeaderText = "Home Model";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[0].DisplayIndex = 0;
            dataGridView1.Columns[0].Width = 200;

            dataGridView1.Columns[1].Visible = false;


        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow row = ((DataRowView)this.dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
            this.House = row[0].ToString();
            this.HouseState = row[1].ToString();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow row = ((DataRowView)this.dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
            this.House = row[0].ToString();
            this.HouseState = row[1].ToString();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dropdownState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadBrandList();
        }

        private void dropBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropBrand.SelectedIndex != -1 && this.finishedLoadingBrand)
            {
                LoadHomeList();
            }
        }
    }
}