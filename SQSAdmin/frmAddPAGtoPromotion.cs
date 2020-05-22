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
    public partial class frmAddPAGtoPromotion : Form
    {
        //private frmPromotion promForm;
        private frmPromotionLookup promForm;
        private string promotionid, promotionname;
        private DataSet dsAllAreas, dsAllGroup;
        private System.Collections.Hashtable IDList;

        public string PromotionID
        {
            get { return promotionid; }
            set { promotionid = value; }
        }
        public string PromotionName
        {
            get { return promotionname; }
            set { promotionname = value; }
        } 
       

        public frmAddPAGtoPromotion()
        {
            InitializeComponent();
            IDList = null;
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
        }
        private void btnSearchPromID_Click(object sender, EventArgs e)
        {
            if (promForm == null)
                promForm = new frmPromotionLookup();
            promForm.ShowDialog();

            if (promForm.SelectedPromotion != null && (promForm.SelectedPromotion.RowState == DataRowState.Unchanged))
            {
                DataRow row = promForm.SelectedPromotion;
                txtPromID.Text = row["promotionID"].ToString();
                txtPromID2.Text = row["promotionID"].ToString();
                txtPromName.Text = row["promotionName"].ToString();
            }
        }

        private void frmAddPAGtoPromotion_Load(object sender, EventArgs e)
        {
            setValue();
            loadArea();
            loadGroup();
            loadPAGList();
            loadPAGList2();

            // // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
        private void setValue()
        {
            this.txtPromID.Text = promotionid;
            this.txtPromID2.Text = promotionid;
            this.txtPromName.Text = promotionname;
        }
        private void loadArea()
        {
            if (dsAllAreas == null)
                dsAllAreas = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select areaid, areaname from area order by areaname");

            dropArea.ValueMember = "areaid";
            dropArea.DisplayMember = "areaname";
            dropArea.DataSource = dsAllAreas.Tables[0];
            dropArea.SelectedIndex = -1;

            dropArea2.ValueMember = "areaid";
            dropArea2.DisplayMember = "areaname";
            dropArea2.DataSource = dsAllAreas.Tables[0].Copy();
            dropArea2.SelectedIndex = -1;
        }
        private void loadGroup()
        {
            if (dsAllGroup == null)
                dsAllGroup = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select groupid, groupname from [group] where active=1 order by groupname");

            dropGroup.ValueMember = "groupid";
            dropGroup.DisplayMember = "groupname";
            dropGroup.DataSource = dsAllGroup.Tables[0];
            dropGroup.SelectedIndex = -1;

            dropGroup3.ValueMember = "groupid";
            dropGroup3.DisplayMember = "groupname";

            dropGroup3.DataSource = dsAllGroup.Tables[0].Copy();

            dropGroup3.SelectedIndex = -1;
        }
        private void loadPAGList()
        {

            int areaID, groupID, promID, active;


            if (txtPromID.Text.ToString() == "")
            {
                promID = 0;
            }
            else
            {
                promID = Int32.Parse(txtPromID.Text.ToString());
            }
            if (dropArea.SelectedIndex == -1)
            {
                areaID = 0;
            }
            else
            {
                areaID = Int32.Parse(dropArea.SelectedValue.ToString());
            }
            if (dropGroup.SelectedIndex == -1)
            {
                groupID = 0;
            }
            else
            {
                groupID = Int32.Parse(dropGroup.SelectedValue.ToString());
            }

            if (chkActive.Checked)
            {
                active = 1;
            }
            else
            {
                active = 0;
            }

            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminSearchExistingPAGForPromotion]", new System.Data.SqlClient.SqlParameter[4]
                            {
                                new System.Data.SqlClient.SqlParameter("@promID", promID),
                                new System.Data.SqlClient.SqlParameter("@areaID", areaID),
                                new System.Data.SqlClient.SqlParameter("@groupID", groupID),
                                new System.Data.SqlClient.SqlParameter("@active", active)
                            }

                   );

            listView1.Items.Clear();

            ListViewItem lvi;
            foreach (DataRow row in dsTemp.Tables[0].Rows)
            {
                lvi = new ListViewItem();
                lvi.Tag = row;
                if (IDList != null)
                    lvi.Checked = IDList.ContainsKey(row["ProductAreaGroupID"].ToString()) ? true : false;
                lvi.Text = row["ProductAreaGroupID"].ToString();
                lvi.SubItems.Add(row["AreaName"].ToString());
                lvi.SubItems.Add(row["GroupName"].ToString());
                lvi.SubItems.Add(row["ProductID"].ToString());
                lvi.SubItems.Add(row["ProductName"].ToString());
                lvi.SubItems.Add(row["Active"].ToString());
                lvi.SubItems.Add(row["qty"].ToString());
                this.listView1.Items.Add(lvi);

            }

        }

        private void loadPAGList2()
        {

            int areaID, groupID, promID;

            if (dropArea2.SelectedIndex == -1)
            {
                areaID = 0;
            }
            else
            {
                areaID = Int32.Parse(dropArea2.SelectedValue.ToString());
            }
            if (dropGroup3.SelectedIndex == -1)
            {
                groupID = 0;
            }
            else
            {
                groupID = Int32.Parse(dropGroup3.SelectedValue.ToString());
            }
            if (txtPromID.Text.ToString() != "")
            {
                promID = Int32.Parse(txtPromID.Text.ToString());
            }
            else
            {
                promID = 0;
            }

            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[AdminSearchPAGForPromotion]", new System.Data.SqlClient.SqlParameter[3]
                            {
                                new System.Data.SqlClient.SqlParameter("@promID", promID),
                                new System.Data.SqlClient.SqlParameter("@areaID", areaID),
                                new System.Data.SqlClient.SqlParameter("@groupID", groupID)
                            }

                   );

            listView2.Items.Clear();

            ListViewItem lvi;

            foreach (DataRow row in dsTemp.Tables[0].Rows)
            {
                lvi = new ListViewItem(row["ProductAreaGroupID"].ToString());
                if (IDList != null)
                    lvi.Checked = IDList.ContainsKey(row["ProductAreaGroupID"].ToString()) ? true : false;

                lvi.SubItems.Add(row["AreaName"].ToString());
                lvi.SubItems.Add(row["GroupName"].ToString());
                lvi.SubItems.Add(row["ProductID"].ToString());
                lvi.SubItems.Add(row["ProductName"].ToString());
                lvi.SubItems.Add(row["Active"].ToString());
                lvi.Tag = row;
                this.listView2.Items.Add(lvi);

            }

        }

        private void btnClear2_Click(object sender, EventArgs e)
        {
            this.dropArea2.SelectedIndex = -1;
            this.dropGroup3.SelectedIndex = -1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.dropArea.SelectedIndex = -1;
            this.dropGroup.SelectedIndex = -1;
        }
        private void dropDownChange(object sender, EventArgs e)
        {
            loadPAGList2();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in this.listView2.Items)
            {
                lvi.Checked = chkSelectAll.Checked;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtPromID.Text.ToString() != "")
            {
                loadPAGList();
            }
            else
            {
                MessageBox.Show("Please select a promotion.", "Message");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveToHome_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder pagIDArrayString = new StringBuilder();


            foreach (ListViewItem lvi in this.listView2.Items)
            {
                if (lvi.Checked)
                {
                    DataRow row = (DataRow)lvi.Tag;
                    pagIDArrayString.Append(row["ProductAreaGroupID"].ToString());
                    pagIDArrayString.Append(",");
                }
            }
            if (pagIDArrayString.Length > 0)
            {
                pagIDArrayString.Remove(pagIDArrayString.Length - 1, 1);

                MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminAddPAGToPromotion", new SqlParameter[3] 
                {
                      new SqlParameter("@promID", Int32.Parse(txtPromID.Text.ToString()))
                    , new SqlParameter("@Quantity", txtQuantity.Text)
                    , new SqlParameter("@pagIDArrayString", pagIDArrayString.ToString())
                });
                loadPAGList();
                loadPAGList2();
            }
        }

        private void btnchangeStatus_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder pagIDArrayString = new StringBuilder();


            foreach (ListViewItem lvi in this.listView1.Items)
            {
                if (lvi.Checked)
                {
                    DataRow row = (DataRow)lvi.Tag;
                    pagIDArrayString.Append(row["ProductAreaGroupID"].ToString());
                    pagIDArrayString.Append(",");
                }
            }
            if (pagIDArrayString.Length > 0)
            {
                pagIDArrayString.Remove(pagIDArrayString.Length - 1, 1);

                MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminChangePAGStatusToPromotion", new SqlParameter[2] 
                {
                      new SqlParameter("@promID", Int32.Parse(txtPromID.Text.ToString()))
                    , new SqlParameter("@pagIDArrayString", pagIDArrayString.ToString())
                });
                loadPAGList();
                loadPAGList2();
            }
            else
            {
                MessageBox.Show("Please select PAG to remove.");
            }
        }
    }
}