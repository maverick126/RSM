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
    public partial class frmPromotion : Form
    {
        //private InputFormMode mode;
        private frmPromotionLookup promotionLookupForm;
        private frmProductLookup productLookupForm;
        private frmAddPAGtoPromotion addPAGtoPromotionForm;
        private DataSet dsPromType;
        private DataSet dsBrand;

        //private DataSet dsRegion;

        public frmPromotion()
        {
            InitializeComponent();
        }

        private void frmPromotion_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            clearForm();
            btnSearchProm_Click(sender, e);
            loadPromTpye();
            loadBrand();
            //loadRegion();
            if (this.txtPromID.Text != "")
            {
                this.btnAddPAG.Enabled = true;
                this.labelPublished.Visible = true;
                this.chkPublished.Visible = true;
            }
            else
            {
                this.btnAddPAG.Enabled = false;
                this.labelPublished.Visible = false;
                this.chkPublished.Visible = false;
            }

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void btnSearchProm_Click(object sender, EventArgs e)
        {
            if (promotionLookupForm == null)
                promotionLookupForm = new frmPromotionLookup();
            promotionLookupForm.ShowDialog();

            if (promotionLookupForm.SelectedPromotion != null && (promotionLookupForm.SelectedPromotion.RowState == DataRowState.Unchanged))
            {
                DataRow row = promotionLookupForm.SelectedPromotion;

                this.txtPromID.Text = row["promotionID"].ToString();
                this.txtPromName.Text = row["promotionName"].ToString();
                this.txtPromTypeID.Text = row["promotionTypeID"].ToString();
                this.txtBaseProductID.Text = row["baseProductID"].ToString();
                this.txtBudgetProductID.Text = row["budgetProductID"].ToString();
                //this.txtBudgetProductID.Text = row["active"].ToString();
                this.txtPromCost.Text = row["promotionCost"].ToString();
                this.txtPromBudget.Text = row["promotionBudget"].ToString();
                this.txtSortOrder.Text = row["sortOrder"].ToString();
                this.txtStories.Text = row["stories"].ToString();
                this.txtBrandID.Text = row["brandID"].ToString();
                //this.txtRegionID.Text = row["regionID"].ToString();
                try
                {
                    DateTime temp = DateTime.Parse(row["effectiveDate"].ToString());
                    this.txtEffectiveDate.Text = temp.ToString("dd/MM/yyyy");
                }
                catch ( FormatException fe)
                {
                    this.txtEffectiveDate.Text = "";
                    MessageBox.Show(fe.ToString());
                }
                if (row["active"].ToString() == "True")
                {
                    this.chkActive.Checked = true;
                }
                else
                {
                    this.chkActive.Checked = false;
                }
                if (row["published"].ToString() == "True")
                {
                    this.chkPublished.Checked = true;
                    this.labelEfDate.Visible = true;
                    this.txtEffectiveDate.Visible = true;
                }
                else
                {
                    this.chkPublished.Checked =false;
                    this.labelEfDate.Visible = false;
                    this.txtEffectiveDate.Visible = false;
                }
            }
        }
        private void loadPromTpye()
        {
            if (dsPromType == null)
                dsPromType = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select promotionTypeID,promotionDescription from PromotionType");

            dropPromType.ValueMember = "promotionTypeID";
            dropPromType.DisplayMember = "promotionDescription";
            dropPromType.DataSource = dsPromType.Tables[0];
            if (this.txtPromTypeID.Text.ToString() == "")
            {
                dropPromType.SelectedIndex = -1;
            }
            else
            {
                dropPromType.SelectedValue = Int32.Parse(txtPromTypeID.Text.ToString());
            }
        }
        private void loadBrand()
        {
            if (dsBrand == null)
                dsBrand = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select brandID,brandName from brand order by brandname");

            dropBrand.ValueMember = "brandID";
            dropBrand.DisplayMember = "brandName";
            dropBrand.DataSource = dsBrand.Tables[0];
            if (this.txtBrandID.Text.ToString() == "")
            {
                dropBrand.SelectedIndex = -1;
            }
            else
            {
                dropBrand.SelectedValue = Int32.Parse(txtBrandID.Text.ToString());
            }
        }
        /*
        private void loadRegion()
        {
            if (dsRegion== null)
                dsRegion = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select regionID,regionName from region order by regionName");

            dropRegion.ValueMember = "regionID";
            dropRegion.DisplayMember = "regionName";
            dropRegion.DataSource = dsRegion.Tables[0];
            if (this.txtRegionID.Text.ToString() == "" || this.txtRegionID.Text.ToString()=="0")
            {
                dropRegion.SelectedIndex = -1;
            }
            else
            {
                dropRegion.SelectedValue = Int32.Parse(txtRegionID.Text.ToString());
            }
        }
         * */
        private void clearForm()
        {
            this.txtPromID.Text = "";
            this.txtPromName.Text = "";
            this.txtPromTypeID.Text = "";


            this.txtBaseProductID.Text = "";
            this.txtBudgetProductID.Text = "";
            this.txtPromCost.Text = "";
            this.txtPromBudget.Text = "";
            //this.txtRegionID.Text = "";
            this.txtBrandID.Text = "";
            this.txtSortOrder.Text = "";
            this.txtStories.Text = "";
            this.chkActive.Checked = false;
            this.chkPublished.Checked = false;
            this.txtEffectiveDate.Text = "";

            this.txtPromTypeID.Text = "";

            this.dropPromType.SelectedIndex = -1;
            this.dropBrand.SelectedIndex = -1;
           // this.dropRegion.SelectedIndex = -1;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            clearForm();
            this.btnAddPAG.Enabled = false;
            this.labelPublished.Visible = false;
            this.chkPublished.Visible = false;
            this.chkPublished.Checked = false;
            this.txtEffectiveDate.Text = "";
            this.txtEffectiveDate.Visible = false;

        }


        private void chkPublished_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPublished.Checked)
            {
                this.labelEfDate.Visible = true;
                this.txtEffectiveDate.Visible = true;
            }
            else
            {
                this.labelEfDate.Visible = false;
                this.txtEffectiveDate.Visible = false;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            this.txtPromID.Text = "";
            this.txtPromName.Text = this.txtPromName.Text+"- New";
            this.btnAddPAG.Enabled = false;
            if (this.chkPublished.Checked)
            {
                this.chkPublished.Checked = false;
                this.txtEffectiveDate.Text = "";
                this.txtEffectiveDate.Visible = false;
            }
        }

        private void btnSelectProduct_Click(object sender, EventArgs e)
        {
            if (productLookupForm == null)
                productLookupForm = new frmProductLookup();
            productLookupForm.ShowDialog();

            DataRow row = productLookupForm.SelectedRow;

            try
            {
                this.txtBaseProductID.Text = row["productID"].ToString();
            }
            catch (RowNotInTableException ep)          
            {
                this.txtBaseProductID.Text = "";
                MessageBox.Show(ep.ToString());
            }
            catch (NullReferenceException ep2)
            {
                this.txtBaseProductID.Text = "";
                MessageBox.Show(ep2.ToString());
            }
        }

        private void btnSaveProm_Click(object sender, EventArgs e)
        {
            savePromotion();
        }
        private void savePromotion()
        {
            int promID, promTypeID,active,published,stories,sortorder,brandID, goAhead;;
            double promCost, promBudget;
            string tempdate,action;

            tempdate="";
            goAhead = 1;

            if (txtBaseProductID.Text.ToString() != "")
            {
                promID     = MetriconCommon.initIntVariables(txtPromID.Text.ToString());
                promTypeID = MetriconCommon.initIntVariables(dropPromType.SelectedValue.ToString());

                brandID    = MetriconCommon.initIntVariables(dropBrand.SelectedValue.ToString());
                stories    = MetriconCommon.initIntVariables(txtStories.Text.ToString());
                sortorder  = MetriconCommon.initIntVariables(txtSortOrder.Text.ToString());

                if (this.chkActive.Checked)
                {
                    active = 1;
                }
                else
                {
                    active = 0;
                }
                if (this.chkPublished.Checked)
                {
                        goAhead = checkPAG();
                        published = 1;
                        if (txtEffectiveDate.Text == "")
                        {
                            MessageBox.Show("Please enter an effective date!");
                        }
                        else
                        {
                            try
                            {
                                DateTime tempdate3 = DateTime.Parse(txtEffectiveDate.Text.ToString());
                                tempdate = tempdate3.ToString("dd/MMM/yyyy");

                            }
                            catch (FormatException ex)
                            {
                                MessageBox.Show("Please enter a valid effective date! "+ex.ToString());
                            }
                        }

                }
                else
                {
                    published = 0;
                }

                if (goAhead == 1)
                {
                    promCost = MetriconCommon.initDoubleVariables(txtPromCost.Text.ToString());
                    promBudget = MetriconCommon.initDoubleVariables(txtPromBudget.Text.ToString());

                    if (txtPromID.Text == "")
                    {
                        action = "NEW";
                    }
                    else
                    {
                        action = "EDIT";
                    }

                    // insert data to tabel
                    MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminAddEditPromotion", new System.Data.SqlClient.SqlParameter[15] {
                                         new System.Data.SqlClient.SqlParameter("@promID",promID)
					                    , new System.Data.SqlClient.SqlParameter("@promTypeID",promTypeID)
					                    , new System.Data.SqlClient.SqlParameter("@promname", txtPromName.Text)
					                    , new System.Data.SqlClient.SqlParameter("@baseProductID", txtBaseProductID.Text)
					                    , new System.Data.SqlClient.SqlParameter("@budgetProductID", txtBudgetProductID.Text)
					                    , new System.Data.SqlClient.SqlParameter("@promCost", promCost)
					                    , new System.Data.SqlClient.SqlParameter("@promBudget", promBudget)
					                    , new System.Data.SqlClient.SqlParameter("@published", published)
					                    , new System.Data.SqlClient.SqlParameter("@sortorder", sortorder)
					                    , new System.Data.SqlClient.SqlParameter("@brandID", brandID)
					                    , new System.Data.SqlClient.SqlParameter("@stories", stories)
                                        , new System.Data.SqlClient.SqlParameter("@effectiveDate", tempdate )
					                    , new System.Data.SqlClient.SqlParameter("@active", active)
					                    , new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode)
                                        , new System.Data.SqlClient.SqlParameter("@action",action)}
                         );

                    // get new promotion ID and show on screen
                    if (action == "NEW")
                    {
                        DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select max(promotionID) as promID from promotion");
                        this.txtPromID.Text = dsTemp.Tables[0].Rows.Count == 1 ? dsTemp.Tables[0].Rows[0]["promID"].ToString() : "";

                        this.btnAddPAG.Enabled = true;
                    }
                    MessageBox.Show("Data saved successfully!");
                }
                else
                {
                    MessageBox.Show("No PAG in current promotion! Please add PAG to this promotion and publish again!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a base product ID!");
            }
        }

        private void btnAddPAG_Click(object sender, EventArgs e)
        {
            if (addPAGtoPromotionForm == null)
            {
                addPAGtoPromotionForm = new frmAddPAGtoPromotion();
            }
            addPAGtoPromotionForm.PromotionID = this.txtPromID.Text.ToString();
            addPAGtoPromotionForm.PromotionName = this.txtPromName.Text.ToString();
            addPAGtoPromotionForm.ShowDialog();
        }
        private int checkPAG()
        {
            DataSet ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select * from PromotionProduct where promotionID=" + this.txtPromID.Text.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}