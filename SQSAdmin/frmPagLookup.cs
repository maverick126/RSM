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
	public partial class frmPagLookup : Form
	{

		private string selectedPAGID;

		private frmProductLookup productLookupForm;



		public string SelectedPAGID
		{
			get { return selectedPAGID; }
			set { selectedPAGID = value; }
		}


		public frmPagLookup()
		{
			InitializeComponent();
		}

		private void LoadPAGList()
		{
            DataSet dsPag = MetriconCommon.DatabaseManager.ExecuteSQLQuery(@"SELECT ProductAreaGroup.ProductAreaGroupID, Area.AreaName, [Group].GroupName, ProductAreaGroup.ProductID,ProductAreaGroup.active
																			FROM ProductAreaGroup INNER JOIN
																			Area ON ProductAreaGroup.AreaID = Area.AreaID INNER JOIN
																			[Group] ON ProductAreaGroup.GroupID = [Group].GroupID
																			WHERE (ProductAreaGroup.ProductID = N'" + txtProductID.Text + "')");

			lstPAG.Items.Clear();
			ListViewItem lvi;
			foreach (DataRow row in dsPag.Tables[0].Rows)
			{

				lvi = new ListViewItem(row["productareagroupid"].ToString());
				lvi.Tag = row;
				lvi.SubItems.Add(row["areaname"].ToString());
				lvi.SubItems.Add(row["groupname"].ToString());
                lvi.SubItems.Add(row["active"].ToString());
     			lstPAG.Items.Add(lvi);


			}
		}

		private void btnLookupPAG_Click(object sender, EventArgs e)
		{
			if (productLookupForm == null)
				productLookupForm = new frmProductLookup();
			productLookupForm.ShowDialog();

			if (productLookupForm.SelectedRow != null && (!(productLookupForm.SelectedRow.RowState == DataRowState.Detached)))
			{
				txtProductID.Text = productLookupForm.SelectedRow["ProductID"].ToString();
				LoadPAGList();
			}
		}

		private void frmPagLookup_Load(object sender, EventArgs e)
		{
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            if (productLookupForm == null)
				productLookupForm = new frmProductLookup();
			productLookupForm.ShowDialog();

			if (productLookupForm.SelectedRow != null && (!(productLookupForm.SelectedRow.RowState == DataRowState.Detached)))
			{
				txtProductID.Text = productLookupForm.SelectedRow["ProductID"].ToString();
				LoadPAGList();
			}

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

		private void btnSave_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void lstPAG_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lstPAG.SelectedIndices.Count > 0)
			{
				DataRow row = (DataRow)lstPAG.SelectedItems[0].Tag;
				this.selectedPAGID = row["productareagroupid"].ToString();

			}

		}

		private void lstPAG_DoubleClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}