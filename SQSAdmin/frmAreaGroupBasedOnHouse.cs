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
    public partial class frmAreaGroupBasedOnHouse : Form
    {
        private int areaID, groupID;
        private string areaName, groupName;
        private System.Collections.Hashtable homeNameList;

        public System.Collections.Hashtable pHomeNameList
        {
            get { return homeNameList; }
            set { homeNameList = value; }
        }

        public int pAreaID
        {
            get { return areaID; }
            set { areaID=value; }
        }
        public int pGroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public string pAreaName
        {
            get { return areaName; }
            set { areaName = value; }
        }
        public string pGroupName
        {
            get { return groupName; }
            set { groupName = value; }
        }

        public frmAreaGroupBasedOnHouse()
        {
            InitializeComponent();
        }

        private void frmAreaGroupBasedOnHouse_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            if (areaID >0)
            {
                labelAreaName.Text ="Area: " + areaName.ToString();
            }
            else
            {
                labelAreaName.Text = "Group: "+ groupName.ToString();
            }
            chkSelectAll.Checked = false;
            LoadStateDropDown();
            LoadHomeList();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void LoadHomeList()
        {
            DataSet dsHome = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminGetHomeModelForAreaGroup]", new System.Data.SqlClient.SqlParameter[3]
                            {
                                new System.Data.SqlClient.SqlParameter("@stateID", Int32.Parse(dropdownState.SelectedValue.ToString())),
                                new System.Data.SqlClient.SqlParameter("@areaID", areaID),
                                new System.Data.SqlClient.SqlParameter("@groupID", groupID),
                            }

                   ); 

            ListViewItem lvi;
            this.lstHome.Items.Clear();
            if (dsHome.Tables[0].Rows.Count >= 1)
            {
                foreach (DataRow row in dsHome.Tables[0].Rows)
                {
                    lvi = new ListViewItem(row["Homemodel"].ToString());

                    if (Int32.Parse(row["active"].ToString()) == 1)
                    {
                        lvi.Checked = true;
                    }
                    else
                    {
                        lvi.Checked = false;
                    }
                    lvi.SubItems.Add(row["Homemodel"].ToString());
                    lvi.Tag = row;
                    this.lstHome.Items.Add(lvi);
                }
            }
        }

        private void LoadStateDropDown()
        {
            DataSet ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });

            dropdownState.DataSource = ds.Tables[0];
            dropdownState.ValueMember = "stateid";
            dropdownState.DisplayMember = "stateAbbreviation";
            dropdownState.SelectedValue = MetriconCommon.UserState;

        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in this.lstHome.Items)
            {
                lvi.Checked = chkSelectAll.Checked;
            }
        }

        private void saveData()
        {


                System.Text.StringBuilder homeString = new StringBuilder();
                System.Text.StringBuilder activeString = new StringBuilder();

                foreach (ListViewItem lvi in this.lstHome.Items)
                {
                    //if (lvi.Checked)
                    //{
                        DataRow row = (DataRow)lvi.Tag;
                        homeString.Append(row["Homemodel"].ToString());
                        homeString.Append(",");
                        if ((lvi.Checked)) activeString.Append("1");
                        else activeString.Append("0");

                        activeString.Append(",");
                    //}
                }
                if (homeString.Length >= 1)
                {
                    homeString.Remove(homeString.Length - 1, 1);
                    activeString.Remove(activeString.Length - 1, 1);
                }
                try
                {
                    MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminUpdateAreaGroupByHomeModel", new SqlParameter[6] 
			        {
				          new SqlParameter("@areaID", areaID)
				        , new SqlParameter("@groupID", groupID)
                        , new SqlParameter("@homeString", homeString.ToString())
                        , new SqlParameter("@activeString", activeString.ToString())
				        , new SqlParameter("@CreatedBy", MetriconCommon.UserCode)
                        , new SqlParameter("@stateID", dropdownState.SelectedValue.ToString())
			        });
                    LoadHomeList();
                    MessageBox.Show("Changes are successfully saved.");

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTurnOn_Click(object sender, EventArgs e)
        {
            saveData();
        }

        private void dropdownState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadHomeList();
        }
    }
}