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
    public partial class frmPromotionLookup : Form
    {

        private DataRow selectedprom;
        public DataRow SelectedPromotion
        {
            get { return selectedprom;}
            set { selectedprom = value; }
        }
        
        public frmPromotionLookup()
        {
            InitializeComponent();
        }

        private void frmPromotionLookup_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pMO006STGDataSet.promotion' table. You can move, or remove it, as needed.
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            this.promotionTableAdapter.Connection.ConnectionString = MetriconCommon.getConnectionString();

            //when dataAdapter fill the dataset, query was written in PMO006STGDataSet.xsd file.
            this.promotionTableAdapter.Fill(this.pMO006STGDataSet.promotion);

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            int published, promID;
            string promName;
            if (txtPromID.Text.ToString() != "")
            {
                promID = Int32.Parse(txtPromID.Text.ToString());
            }
            else
            {
                promID = 0;
            }
            promName = txtPromName.Text.ToString();
            if (chkActive.Checked)
            {
                published = 1;
            }
            else
            {
                published = 0;
            }

           this.promotionTableAdapter.Connection.ConnectionString = MetriconCommon.getConnectionString();

            //when dataAdapter fill the dataset, query was written in PMO006STGDataSet.xsd file.
            this.promotionTableAdapter.FillBy(this.pMO006STGDataSet.promotion, promID,promName,published);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataRow row = ((DataRowView)this.dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
            this.SelectedPromotion = row;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DataRow row = ((DataRowView)this.dataGridView1.Rows[this.dataGridView1.SelectedCells[0].RowIndex].DataBoundItem).Row;
            this.selectedprom = row;
            this.Close();
        }
    }
}