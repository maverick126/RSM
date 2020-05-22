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
	public partial class frmHomeLookup : Form
	{

		private DataRow selectedHome;

		public DataRow SelectedHome
		{
			get { return selectedHome; }
			set { selectedHome = value; }
		}


		public frmHomeLookup()
		{
			InitializeComponent();
		}

		private void frmHomeLookup_Load(object sender, EventArgs e)
		{
            // TODO: This line of code loads data into the 'pMO006STGDataSet.Home' table. You can move, or remove it, as needed.
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            this.homeTableAdapter.Connection.ConnectionString = MetriconCommon.getConnectionString();


			//this.homeTableAdapter.Fill(this.pMO006STGDataSet.Home);
            loadStateDropdown();
			LoadBrandList();
            loadStatusDropdown();
            loadShowOnPriceListDropdown();
            fillByToolStripButton_Click(sender,e);

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
        private void loadStateDropdown()
        {
            DataSet dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });

            ComboBox cb = (ComboBox)toolStripComboBox3.Control;

            cb.DataSource = dsState.Tables[0];
            cb.DisplayMember = "stateAbbreviation";
            cb.ValueMember = "stateID";
            cb.SelectedValue=MetriconCommon.UserState;

            cb.SelectionChangeCommitted += new System.EventHandler(this.cb_SelectionChangeCommitted);

        }
        private void loadShowOnPriceListDropdown()
        {
            DataTable tempTable = new DataTable();
            tempTable.TableName = "type";

            tempTable.Columns.Add("typeid");
            tempTable.Columns.Add("typename");
            DataRow dr = tempTable.NewRow();
            dr["typeid"] = "2";
            dr["typename"] = "All";
            tempTable.Rows.Add(dr);

            DataRow dr2 = tempTable.NewRow();
            dr2["typeid"] = "1";
            dr2["typename"] = "Yes";
            tempTable.Rows.Add(dr2);

            DataRow dr3 = tempTable.NewRow();
            dr3["typeid"] = "0";
            dr3["typename"] = "No";
            tempTable.Rows.Add(dr3);

            ComboBox cb = (ComboBox)toolStripComboBox4.Control;

            cb.DataSource = tempTable;
            cb.DisplayMember = "typename";
            cb.ValueMember = "typeid";

            if (label2.Text != "")
                cb.SelectedValue = label2.Text;
            else
                cb.SelectedValue = 2;

        }
        private void cb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadBrandList();
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

            //toolStripComboBox1.ComboBox.DataSource= dsTemp.Tables[0];
            //toolStripComboBox1.ComboBox.DisplayMember = "brandname";
            //toolStripComboBox1.ComboBox.ValueMember = "brandid";

            ComboBox cb = (ComboBox)toolStripComboBox1.Control;

            cb.DataSource = dsTemp.Tables[0];
            cb.DisplayMember = "brandname";
            cb.ValueMember = "brandid";
            cb.SelectedValue = 999 ;

            if (label1.Text != "")
                cb.SelectedValue = label1.Text;
            else
                cb.SelectedValue = 999;
        }




        private void fillByToolStripButton_Click(object sender, EventArgs e)
		{
            /*
			try
			{
				this.homeTableAdapter.FillBy(this.pMO006STGDataSet.Home, new System.Nullable<long>(((long)(System.Convert.ChangeType(((DropDownItems)this.toolStripComboBox1.SelectedItem).DisplayValue, typeof(long))))));
				
			}
			catch (System.Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
			}*/
            int brandID, active;
            string homename;
            string stateID = ((ComboBox)toolStripComboBox3.Control).SelectedValue.ToString();
            label1.Text = ((ComboBox)toolStripComboBox1.Control).SelectedValue.ToString();
            label2.Text = ((ComboBox)toolStripComboBox4.Control).SelectedValue.ToString();
            string showonpricelist = ((ComboBox)toolStripComboBox4.Control).SelectedValue.ToString();

            try
            {
                //brandID = Int32.Parse(((ComboBox)toolStripComboBox1.Control).SelectedValue.ToString());
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
                homename=((TextBox)toolStripTextBox1.Control).Text.ToString();

                DataSet dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminFindHome]", new System.Data.SqlClient.SqlParameter[7]
                            {
                                new System.Data.SqlClient.SqlParameter("@stateID", stateID),
                                new System.Data.SqlClient.SqlParameter("@brandID", brandID),
                                new System.Data.SqlClient.SqlParameter("@active",active ),
                                new System.Data.SqlClient.SqlParameter("@homename",homename ),
                                new System.Data.SqlClient.SqlParameter("@showSummary",0),
                                new System.Data.SqlClient.SqlParameter("@viewmode","homefacade" ),
                                new System.Data.SqlClient.SqlParameter("@showonpricelist", showonpricelist )
                            }

                       );
                //dataGridView1.DataSource = dsHome.Tables[0];
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
        private void bindingData(DataSet dsTemp)
        {
            dataGridView1.DataSource = dsTemp.Tables[0];


            dataGridView1.Columns["homeid"].HeaderText = "Home ID";
            dataGridView1.Columns["homeid"].ReadOnly = true;
            dataGridView1.Columns["homeid"].DisplayIndex = 0;

            dataGridView1.Columns["homename"].HeaderText = "Home Name";
            dataGridView1.Columns["homename"].ReadOnly = true;
            dataGridView1.Columns["homename"].DisplayIndex = 1;

            dataGridView1.Columns["brandname"].HeaderText = "Brand Name";
            dataGridView1.Columns["brandname"].ReadOnly = true;
            dataGridView1.Columns["brandname"].DisplayIndex = 2;

            dataGridView1.Columns["productid"].HeaderText = "Product ID";
            dataGridView1.Columns["productid"].ReadOnly = true;
            dataGridView1.Columns["productid"].DisplayIndex = 3;

            dataGridView1.Columns["active"].HeaderText = "Active";
            dataGridView1.Columns["active"].ReadOnly = false;
            dataGridView1.Columns["active"].DisplayIndex = 4;

            dataGridView1.Columns["showonpricelist"].HeaderText = "Show On Price List";
            dataGridView1.Columns["showonpricelist"].ReadOnly = false;
            dataGridView1.Columns["showonpricelist"].DisplayIndex = 5;


            dataGridView1.Columns["housearea"].HeaderText = "House Area";
            dataGridView1.Columns["housearea"].ReadOnly = true;
            dataGridView1.Columns["housearea"].DisplayIndex = 6;

            dataGridView1.Columns["houseareasquares"].HeaderText = "House Area Squares";
            dataGridView1.Columns["houseareasquares"].ReadOnly = true;
            dataGridView1.Columns["houseareasquares"].DisplayIndex = 7;

            dataGridView1.Columns["alfrescoarea"].HeaderText = "Alfresco Area";
            dataGridView1.Columns["alfrescoarea"].ReadOnly = true;
            dataGridView1.Columns["alfrescoarea"].DisplayIndex = 8;

            dataGridView1.Columns["alfrescoareasquares"].HeaderText = "Alfresco Area Squares";
            dataGridView1.Columns["alfrescoareasquares"].ReadOnly = true;
            dataGridView1.Columns["alfrescoareasquares"].DisplayIndex = 9;

            dataGridView1.Columns["garagearea"].HeaderText = "Garage Area";
            dataGridView1.Columns["garagearea"].ReadOnly = true;
            dataGridView1.Columns["garagearea"].DisplayIndex = 10;

            dataGridView1.Columns["garageareasquares"].HeaderText = "Garage Area Squares";
            dataGridView1.Columns["garageareasquares"].ReadOnly = true;
            dataGridView1.Columns["garageareasquares"].DisplayIndex = 11;

            dataGridView1.Columns["totalarea"].HeaderText = "Total Area";
            dataGridView1.Columns["totalarea"].ReadOnly = true;
            dataGridView1.Columns["totalarea"].DisplayIndex = 12;

            dataGridView1.Columns["totalareasquares"].HeaderText = "Total Area Squares";
            dataGridView1.Columns["totalareasquares"].ReadOnly = true;
            dataGridView1.Columns["totalareasquares"].DisplayIndex = 13;


            dataGridView1.Columns["min_block_width"].HeaderText = "Min Block Width";
            dataGridView1.Columns["min_block_width"].ReadOnly = true;
            dataGridView1.Columns["min_block_width"].DisplayIndex = 14;

            dataGridView1.Columns["houselength"].HeaderText = "House Length";
            dataGridView1.Columns["houselength"].ReadOnly = true;
            dataGridView1.Columns["houselength"].DisplayIndex = 15;

            dataGridView1.Columns["housewidth"].HeaderText = "House Width";
            dataGridView1.Columns["housewidth"].ReadOnly = true;
            dataGridView1.Columns["housewidth"].DisplayIndex = 16;

            dataGridView1.Columns["Bedrooms"].HeaderText = "Bedrooms";
            dataGridView1.Columns["Bedrooms"].ReadOnly = true;
            dataGridView1.Columns["Bedrooms"].DisplayIndex = 17;

            dataGridView1.Columns["bathrooms"].HeaderText = "Bathrooms";
            dataGridView1.Columns["bathrooms"].ReadOnly = true;
            dataGridView1.Columns["bathrooms"].DisplayIndex = 18;

            dataGridView1.Columns["garagebays"].HeaderText = "Garages";
            dataGridView1.Columns["garagebays"].ReadOnly = true;
            dataGridView1.Columns["garagebays"].DisplayIndex = 19;

            dataGridView1.Columns["stories"].HeaderText = "Stories";
            dataGridView1.Columns["stories"].ReadOnly = true;
            dataGridView1.Columns["stories"].DisplayIndex = 20;

            dataGridView1.Columns["livingarea"].HeaderText = "Living Area";
            dataGridView1.Columns["livingarea"].ReadOnly = true;
            dataGridView1.Columns["livingarea"].DisplayIndex = 21;

            dataGridView1.Columns["draft"].HeaderText = "Draft";
            dataGridView1.Columns["draft"].ReadOnly = true;
            dataGridView1.Columns["draft"].DisplayIndex = 22;

            dataGridView1.Columns["brandid"].Visible = false;
            dataGridView1.Columns["fkstateid"].Visible = false;
            dataGridView1.Columns["desc"].Visible = false;
            dataGridView1.Columns["cstctr"].Visible = false;
            dataGridView1.Columns["suofm"].Visible = false;
            dataGridView1.Columns["puofm"].Visible = false;
            dataGridView1.Columns["prdcat"].Visible = false;
            dataGridView1.Columns["taxcd"].Visible = false;
            dataGridView1.Columns["prdsop"].Visible = false;
            dataGridView1.Columns["usrdef"].Visible = false;
            dataGridView1.Columns["ldesc"].Visible = false;
            dataGridView1.Columns["brand"].Visible = false;
            dataGridView1.Columns["specyear"].Visible = false;
            dataGridView1.Columns["housecode"].Visible = false;
            dataGridView1.Columns["housesize"].Visible = false;
            dataGridView1.Columns["housefacade"].Visible = false;
            dataGridView1.Columns["storey"].Visible = false;
            dataGridView1.Columns["mark"].Visible = false;

            dataGridView1.Columns["sortorder"].Visible = false;
            dataGridView1.Columns["createddate"].Visible = false;
            dataGridView1.Columns["createdby"].Visible = false;
            dataGridView1.Columns["modifieddate"].Visible = false;
            dataGridView1.Columns["modifiedby"].Visible = false;
            dataGridView1.Columns["parenthomeid"].Visible = false;
            dataGridView1.Columns["min_area"].Visible = false;
            dataGridView1.Columns["homeplan"].Visible = false;
            dataGridView1.Columns["facade"].Visible = false;
            dataGridView1.Columns["edesc"].Visible = false;
        }
        private void toolStripComboBox1_DropDownClosed(object sender, EventArgs e)
		{
			fillByToolStripButton_Click(sender, e);
		}


		private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
            if (e.RowIndex != -1)
            {
                DataRow row = ((DataRowView)this.dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
                this.selectedHome = row;
                this.Close();
            }
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			DataRow row = ((DataRowView)this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].DataBoundItem).Row;
			this.selectedHome = row;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

        private void homeBindingSource_CurrentChanged(object sender, EventArgs e)
        {

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

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DataRow row = ((DataRowView)this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].DataBoundItem).Row;
            this.selectedHome = row;
            this.Close();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string homeid = dataGridView1.Rows[e.RowIndex].Cells["homeid"].Value.ToString();
                string active;
                if ((bool)(dataGridView1.Rows[e.RowIndex].Cells["active"].Value))
                {
                    active = "1";
                }
                else
                {
                    active = "0";
                }
                string showonpricelist;
                if ((bool)(dataGridView1.Rows[e.RowIndex].Cells["showonpricelist"].Value))
                {
                    showonpricelist = "1";
                }
                else
                {
                    showonpricelist = "0";
                }
                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminUpdateHomeFlag]", new System.Data.SqlClient.SqlParameter[4]
                            {
                                      new System.Data.SqlClient.SqlParameter("@homeID", homeid)
                                    , new System.Data.SqlClient.SqlParameter("@active", active)
                                    , new System.Data.SqlClient.SqlParameter("@showonpricelist", showonpricelist)
                                    , new System.Data.SqlClient.SqlParameter("@modifiedBy", MetriconCommon.UserCode)
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



		public override string  ToString()
		{
			return displayText.ToString();
		}

	}

}