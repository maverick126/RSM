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
    public partial class frmHomeDisplayLookup : Form
    {

        private DataRow selectedhomedisplay;
        private int newdis;

        public DataRow SelectedHomeDisplay
        {
            get { return selectedhomedisplay; }
            set { selectedhomedisplay = value; }
        }
        public int NewDis
        {
            get { return newdis; }
            set { newdis = value; }
        }
        public frmHomeDisplayLookup()
        {
            InitializeComponent();
        }

        private void frmHomeDisplayLookup_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            this.homeTableAdapter.Connection.ConnectionString = MetriconCommon.getConnectionString();

            //when dataAdapter fill the dataset, query was written in PMO006STGDataSet.xsd file.
            //this.homeTableAdapter.FillHomeDisplayData(this.pMO006STGDataSet.Home);
            loadStateDropdown();
            LoadBrandList();
            loadStatusDropdown();
            fillByToolStripButton_Click(sender, e);

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void LoadBrandList()
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetBrand]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@stateID", ((ComboBox)toolStripComboBox3.Control).SelectedValue.ToString() )
                            });

            DataRow myDataRow = dsTemp.Tables[0].NewRow();
            myDataRow["brandid"] = 999;
            myDataRow["brandname"] = "All";
            dsTemp.Tables[0].Rows.Add(myDataRow);
            dsTemp.Tables[0].DefaultView.Sort = "brandname";

            ComboBox cb = (ComboBox)toolStripComboBox1.Control;

            cb.DataSource = dsTemp.Tables[0];
            cb.DisplayMember = "brandname";
            cb.ValueMember = "brandid";
            cb.SelectedValue = 999;

            if (label2.Text != "")
                cb.SelectedValue = label2.Text;
            else
                cb.SelectedValue = 999;

        }

        private void loadStateDropdown()
        {
            DataSet dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });

            ComboBox cb = (ComboBox)toolStripComboBox3.Control;

            cb.DataSource = dsState.Tables[0];
            cb.DisplayMember = "stateAbbreviation";
            cb.ValueMember = "stateID";
            cb.SelectedValue=MetriconCommon.UserState;
            if (label1.Text != "")
            {
                cb.SelectedValue = label1.Text.Trim();
            }
            cb.SelectionChangeCommitted += new System.EventHandler(this.cb_SelectionChangeCommitted);

        }

        private void cb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadBrandList();
            ComboBox cb = (ComboBox)toolStripComboBox3.Control;
            label1.Text = cb.SelectedValue.ToString();
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            NewDis = 0;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataRow row = ((DataRowView)this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].DataBoundItem).Row;
            this.selectedhomedisplay = row;
            this.NewDis = 0;
            this.Close();
        }

        private void toolStripComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            fillByToolStripButton_Click(sender, e);
        }
        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            int brandID, active;
            string homename,suburb;
            string stateID = ((ComboBox)toolStripComboBox3.Control).SelectedValue.ToString();
            label2.Text = ((ComboBox)toolStripComboBox1.Control).SelectedValue.ToString();
            try
            {
                int brandID2 = toolStripComboBox1.SelectedIndex;
                if (brandID2 == -1)
                {
                    brandID = -1;
                }
                else
                {
                    brandID = Int32.Parse(((ComboBox)toolStripComboBox1.Control).SelectedValue.ToString());
                }
                active = Int32.Parse(((ComboBox)toolStripComboBox2.Control).SelectedValue.ToString());
                homename = ((TextBox)toolStripTextBox1.Control).Text.ToString();
                suburb = ((TextBox)toolStripTextBox2.Control).Text.ToString();

                DataSet dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminFindDisplayHome]", new System.Data.SqlClient.SqlParameter[5]
                            {
                                new System.Data.SqlClient.SqlParameter("@stateID", stateID),
                                new System.Data.SqlClient.SqlParameter("@brandID", brandID),
                                new System.Data.SqlClient.SqlParameter("@active",active ),
                                new System.Data.SqlClient.SqlParameter("@homename",homename ),
                                new System.Data.SqlClient.SqlParameter("@suburb",suburb ),
                            }

                       );

                bindingData(dsHome);

            }
            catch (SqlException ex2)
            {
                MessageBox.Show(ex2.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public struct DropDownItems
        {

            private string displayText;
            private object displayValue;

            public object DisplayValue
            {
                get { return displayValue; }
                set { displayValue = value; }
            }
            public DropDownItems(string displayText, object displayValue)
            {
                this.displayText = displayText;
                this.displayValue = displayValue;

            }
            public override string ToString()
            {
                return displayText.ToString();
            }

        }



        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataRow row = ((DataRowView)this.dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
                this.SelectedHomeDisplay = row;
                this.NewDis = 0;
                this.Close();
            }
        }

        private void btnNew2_Click(object sender, EventArgs e)
        {
            NewDis = 1;
            this.Close();
        }
        private void loadStatusDropdown()
        {
            DataTable dtbl = CreateStatusTable();
            ComboBox cb = (ComboBox)toolStripComboBox2.Control;
            cb.DataSource = dtbl;
            cb.DisplayMember = "status";
            cb.ValueMember = "id";
            cb.SelectedValue = 1;
        }
        private DataTable CreateStatusTable()
        {
            DataTable myTab = new DataTable("statusTab");
            DataColumn dtColumn;

            // Create id Column
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.Int32");
            dtColumn.ColumnName = "id";
            // Add id Column to the DataColumnCollection.
            myTab.Columns.Add(dtColumn);

            // Create Name column.
            dtColumn = new DataColumn();
            dtColumn.DataType = System.Type.GetType("System.String");
            dtColumn.ColumnName = "status";
            // Add Name column to the table.
            myTab.Columns.Add(dtColumn);

            DataRow myDataRow = myTab.NewRow();
            myDataRow["id"] = 2;
            myDataRow["status"] = "All";
            myTab.Rows.Add(myDataRow);

            myDataRow = myTab.NewRow();
            myDataRow["id"] = 1;
            myDataRow["status"] = "Active";
            myTab.Rows.Add(myDataRow);

            myDataRow = myTab.NewRow();
            myDataRow["id"] = 0;
            myDataRow["status"] = "Inactive";
            myTab.Rows.Add(myDataRow);

            return myTab;
        }

        private void bindingData(DataSet dsTemp)
        {
            dataGridView1.DataSource = dsTemp.Tables[0];

            dataGridView1.Columns[0].HeaderText = "Home ID";
            dataGridView1.Columns[0].ReadOnly = true;
            dataGridView1.Columns[0].Width = 80;

            dataGridView1.Columns[1].HeaderText = "Home Name";
            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.Columns[1].Width = 150;

            dataGridView1.Columns[2].HeaderText = "Brand";
            dataGridView1.Columns[2].ReadOnly = true;
            dataGridView1.Columns[2].Width = 100;

            dataGridView1.Columns[3].HeaderText = "ProductID";
            dataGridView1.Columns[3].ReadOnly = true;
            dataGridView1.Columns[3].Width = 80;

            dataGridView1.Columns[4].Visible = false;

            dataGridView1.Columns[5].HeaderText = "Suburb";
            dataGridView1.Columns[5].ReadOnly = true;
            dataGridView1.Columns[5].Width = 100;

            dataGridView1.Columns[6].HeaderText = "Lot Address";
            dataGridView1.Columns[6].ReadOnly = true;
            dataGridView1.Columns[6].Width = 200;

            dataGridView1.Columns[7].Visible = true;
            dataGridView1.Columns[7].HeaderText = "Estate Name";
            dataGridView1.Columns[7].ReadOnly = true;

            dataGridView1.Columns[8].HeaderText = "Active";
            dataGridView1.Columns[8].ReadOnly = true;

            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible = false;
            dataGridView1.Columns[17].Visible = false;
            dataGridView1.Columns[18].Visible = false;
            dataGridView1.Columns[19].Visible = false;
            dataGridView1.Columns[20].Visible = false;
            dataGridView1.Columns[21].Visible = false;
            dataGridView1.Columns[22].Visible = false;
            dataGridView1.Columns[23].Visible = false;
            dataGridView1.Columns[24].Visible = false;
            dataGridView1.Columns[25].Visible = false;
            dataGridView1.Columns[26].Visible = false;
            dataGridView1.Columns[27].Visible = false;
            dataGridView1.Columns[28].Visible = false;
            dataGridView1.Columns[29].Visible = false;
            dataGridView1.Columns[30].Visible = false;
            dataGridView1.Columns[31].Visible = false;
            dataGridView1.Columns[32].Visible = false;
            dataGridView1.Columns[33].Visible = false;
            dataGridView1.Columns[34].Visible = false;
            dataGridView1.Columns[35].Visible = false;
            dataGridView1.Columns[36].Visible = false;
            dataGridView1.Columns[37].Visible = false;
            dataGridView1.Columns[38].Visible = false;
            dataGridView1.Columns[39].Visible = false;
            dataGridView1.Columns[40].Visible = false;
            dataGridView1.Columns[41].Visible = false;
            dataGridView1.Columns[42].Visible = false;
            dataGridView1.Columns[43].Visible = false;
            dataGridView1.Columns[44].Visible = false;
            dataGridView1.Columns[45].Visible = false;
            dataGridView1.Columns[46].Visible = false;
            dataGridView1.Columns[47].Visible = false;
            dataGridView1.Columns[48].Visible = false;
            dataGridView1.Columns[49].Visible = false;
            dataGridView1.Columns[50].Visible = false;
            dataGridView1.Columns[54].Visible = false;
            dataGridView1.Columns[55].Visible = false;


            dataGridView1.Columns[51].Visible = true;
            dataGridView1.Columns[51].HeaderText = "UnderReview";
            dataGridView1.Columns[51].ReadOnly = true;

        }

    }
}