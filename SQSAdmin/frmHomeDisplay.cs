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
    public partial class frmHomeDisplay : Form
    {
        private InputFormMode mode;
        private frmHomeLookup homeLookupForm;
        private frmHomeDisplayLookup homeDisplayLookupForm;
        private frmProductLookup productLookupForm;
        private int oldBrandID;
        private string oldProductID;
        private int success=1;

        public frmHomeDisplay()
        {
            InitializeComponent();
        }

        private void frmHomeDisplay_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            ClearForm();
            loadStateDropdown();
            LoadBrandList();
            //LoadRegion();
            //LoadSuburb();
            //LoadEstate();
            btnLookupHomeDisplay_Click(sender, e);
            if (homeDisplayLookupForm.NewDis == 1)
            {

                btnNew_Click(sender, e);
                mode = InputFormMode.New;
            }
            else
            {
                ViewRead();
            }
            //dropdownState_SelectionChangeCommitted(sender,e);
            //setbuttons1();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
        public void ViewRead()
        {
            //mode = InputFormMode.Read;
            this.txtHomeID.ReadOnly = true;
            this.txtHomePlan.ReadOnly = true;
            this.dropBrand.Enabled = false;
            this.dropdownState.Enabled = false;
            this.txtProductID.ReadOnly = true;
            this.txtHouseArea.ReadOnly = true;
            this.txtHouseAreaSquares.ReadOnly = true;
            this.txtAlfrescoArea.ReadOnly = true;
            this.txtAlfrescoAreaSquares.ReadOnly = true;
            this.txtGarageArea.ReadOnly = true;
            this.txtGarageAreaSquares.ReadOnly = true;
            this.txtTotalArea.ReadOnly = true;
            this.txtTotalAreaSquares.ReadOnly = true;
            this.txtMinimumBlockWidth.ReadOnly = true;
            this.txtHouseLength.ReadOnly = true;
            this.txtHouseWidth.ReadOnly = true;
            this.txtGarageBays.ReadOnly = true;
            this.dropBedrooms.Enabled = false;
            this.dropBathrooms.Enabled = false;
            this.dropStoreys.Enabled = false;
            this.txtSortOrder.ReadOnly = true;
            this.chkActive.Enabled = false;
            this.chkDraft.Enabled = false;
            this.txtDesc.ReadOnly = true;
            this.txtEdesc.ReadOnly = true;
            this.txtCstctr.ReadOnly = true;
            this.txtSuofm.ReadOnly = true;
            this.txtPuofm.ReadOnly = true;
            this.txtPrdcat.ReadOnly = true;
            this.txtTaxcd.ReadOnly = true;
            this.txtPrdsop.ReadOnly = true;
            this.txtUsrdef.ReadOnly = true;
            this.txtLdesc.ReadOnly = true;
            this.txtBrand.ReadOnly = true;
            this.txtSpecyear.ReadOnly = true;
            this.txtHousecode.ReadOnly = true;
            this.txtHousesize.ReadOnly = true;
            this.txtHousefacade.ReadOnly = true;
            this.txtStorey.ReadOnly = true;
            this.txtMark.ReadOnly = true;
            this.btnLookupProduct.Enabled = false;
            this.btnLookupHomeDisplay.Enabled = false;
            this.btnLookUpParentHome.Enabled = false;
            this.txtclosedate.Enabled = false;

            this.txtLotAddress.ReadOnly = true ;
            //this.txtSuburb.Enabled=false;
            this.dropSuburb.Enabled = false;
            //this.radioButton1.Enabled = false;
            //this.radioButton2.Enabled = false;
            this.txtBlockSize.ReadOnly = true ;
            this.dropEstate.Enabled = false;
            //this.dropRegion.Enabled=false;
            this.dateTimePicker1.Enabled = false;
            btnNew.Enabled = true;
            if (txtHomeID.Text.ToString() != "")
            {
                btnEdit.Enabled = true;
            }
           
            btnCancelData.Enabled = false;
            btnSaveData.Enabled = false;

            if (!(string.IsNullOrEmpty(txtHomeID.Text)))
                btnCopy.Enabled = true;
            else
                btnCopy.Enabled = false;


        }
        public void ViewNew()
        {
            this.txtHomePlan.ReadOnly = false;
            this.dropBrand.Enabled = true;
            this.txtProductID.ReadOnly = false;
            this.txtHouseArea.ReadOnly = false;
            this.txtHouseAreaSquares.ReadOnly = false;
            this.txtAlfrescoArea.ReadOnly = false;
            this.txtAlfrescoAreaSquares.ReadOnly = false;
            this.txtGarageArea.ReadOnly = false;
            this.txtGarageAreaSquares.ReadOnly = false;
            this.txtTotalArea.ReadOnly = false;
            this.txtTotalAreaSquares.ReadOnly = false;
            this.txtMinimumBlockWidth.ReadOnly = false;
            this.txtHouseLength.ReadOnly = false;
            this.txtHouseWidth.ReadOnly = false;
            this.txtGarageBays.ReadOnly = false;
            this.dropBedrooms.Enabled = true;
            this.dropBathrooms.Enabled = true;
            this.dropStoreys.Enabled = true;
            this.txtSortOrder.ReadOnly = false;
            this.chkActive.Enabled = true;
            this.chkDraft.Enabled = true;
            this.txtDesc.ReadOnly = false;
            this.txtEdesc.ReadOnly = false;
            this.txtCstctr.ReadOnly = false;
            this.txtSuofm.ReadOnly = false;
            this.txtPuofm.ReadOnly = false;
            this.txtPrdcat.ReadOnly = false;
            this.txtTaxcd.ReadOnly = false;
            this.txtPrdsop.ReadOnly = false;
            this.txtUsrdef.ReadOnly = false;
            this.txtLdesc.ReadOnly = false;
            this.txtBrand.ReadOnly = false;
            this.txtSpecyear.ReadOnly = false;
            this.txtHousecode.ReadOnly = false;
            this.txtHousesize.ReadOnly = false;
            this.txtHousefacade.ReadOnly = false;
            this.txtStorey.ReadOnly = false;
            this.txtMark.ReadOnly = false;
            this.btnLookupProduct.Enabled = true;
            this.btnLookupHomeDisplay.Enabled = true;
            this.btnLookUpParentHome.Enabled = true;
            this.dateTimePicker1.Enabled = true;
            this.txtclosedate.Enabled = true;

            this.txtLotAddress.ReadOnly = false;
            //this.txtSuburb.ReadOnly = false;
            //this.txtSuburb.Enabled = false;
            //this.radioButton1.Enabled = true;
            //this.radioButton2.Enabled = true;
            this.txtBlockSize.ReadOnly = false;
            this.dropEstate.Enabled = true;
            this.dropSuburb.Enabled = true;
            //this.dropRegion.Enabled = true;

            btnNew.Enabled = false;
            btnEdit.Enabled = false;
            btnCancelData.Enabled = true;
            btnSaveData.Enabled = true;
            //setbuttons1();

            this.btnCopy.Enabled = false;
        }
        public void ClearForm()
        {
            this.txtHomeID.Text = "";
            this.txtOriginalDisHomeID.Text = "";
            this.txtParentHomeID.Text = "";
            this.txtOriginalHomeID.Text = "";
            this.labelPhome.Text = "";
            this.txtHomePlan.Text = "";
            this.txtHomeFacade.Text = "";
            this.dropBrand.Text = "";
            this.txtProductID.Text = "";
            this.txtHouseArea.Text = "";
            this.txtHouseAreaSquares.Text = "";
            this.txtAlfrescoArea.Text = "";
            this.txtAlfrescoAreaSquares.Text = "";
            this.txtGarageArea.Text = "";
            this.txtGarageAreaSquares.Text = "";
            this.txtTotalArea.Text = "";
            this.txtTotalAreaSquares.Text = "";
            this.txtMinimumBlockWidth.Text = "";
            this.txtHouseLength.Text = "";
            this.txtHouseWidth.Text = "";
            this.txtGarageBays.Text = "";
            this.dropBedrooms.Text = "";
            this.dropBathrooms.Text = "";
            this.dropStoreys.Text = "";
            this.txtSortOrder.Text = "";
            this.chkActive.Checked = true;
            this.chkDraft.Checked = false;
            this.txtDesc.Text = "";
            this.txtEdesc.Text = "";
            this.txtCstctr.Text = "";
            this.txtSuofm.Text = "";
            this.txtPuofm.Text = "";
            this.txtPrdcat.Text = "";
            this.txtTaxcd.Text = "";
            this.txtPrdsop.Text = "";
            this.txtUsrdef.Text = "";
            this.txtLdesc.Text = "";
            this.txtBrand.Text = "";
            this.txtSpecyear.Text = "";
            this.txtHousecode.Text = "";
            this.txtHousesize.Text = "";
            this.txtHousefacade.Text = "";
            this.txtStorey.Text = "";
            this.txtMark.Text = "";
            this.txtLotAddress.Text = "";
            //this.txtSuburb.Text="";
            this.txtBlockSize.Text="";
            //this.dropRegion.SelectedIndex = 0;
            this.dropSuburb.Text = "";
            this.dropEstate.Text = "";
            this.txtclosedate.Text = "";
            //this.radioButton1.Checked = true;
            //setbuttons1();
        }
        public void ViewEdit()
        {

            //this.txtHomeName.ReadOnly = false;
            //this.dropBrand.Enabled = true;
            //this.txtProductID.ReadOnly = false;
            this.txtHouseArea.ReadOnly = false;
            this.txtHouseAreaSquares.ReadOnly = false;
            this.txtAlfrescoArea.ReadOnly = false;
            this.txtAlfrescoAreaSquares.ReadOnly = false;
            this.txtGarageArea.ReadOnly = false;
            this.txtGarageAreaSquares.ReadOnly = false;
            this.txtTotalArea.ReadOnly = false;
            this.txtTotalAreaSquares.ReadOnly = false;
            this.txtMinimumBlockWidth.ReadOnly = false;
            this.txtHouseLength.ReadOnly = false;
            this.txtHouseWidth.ReadOnly = false;
            this.txtGarageBays.ReadOnly = false;
            this.dropBedrooms.Enabled = true;
            this.dropBathrooms.Enabled = true;
            this.dropStoreys.Enabled = true;
            this.txtSortOrder.ReadOnly = false;
            this.chkActive.Enabled = true;
            this.chkDraft.Enabled = true;
            //this.txtDesc.ReadOnly = false;
            this.txtEdesc.ReadOnly = false;
            this.txtCstctr.ReadOnly = false;
            this.txtSuofm.ReadOnly = false;
            this.txtPuofm.ReadOnly = false;
            this.txtPrdcat.ReadOnly = false;
            this.txtTaxcd.ReadOnly = false;
            this.txtPrdsop.ReadOnly = false;
            this.txtUsrdef.ReadOnly = false;
            //this.txtLdesc.ReadOnly = false;
            this.txtBrand.ReadOnly = false;
            this.txtSpecyear.ReadOnly = false;
            this.txtHousecode.ReadOnly = false;
            this.txtHousesize.ReadOnly = false;
            this.txtHousefacade.ReadOnly = false;
            this.txtStorey.ReadOnly = false;
            this.txtMark.ReadOnly = false;
            //this.btnLookupProduct.Enabled = true;
            //this.btnLookupHomeDisplay.Enabled = true;
            this.btnLookUpParentHome.Enabled = true;

            this.txtLotAddress.ReadOnly = false;
            //this.txtSuburb.ReadOnly = false;
            this.dropSuburb.Enabled = true;
            this.dropEstate.Enabled = true;
            //this.radioButton1.Enabled = true;
            //this.radioButton2.Enabled = true;
            this.txtBlockSize.ReadOnly = false;
            //this.dropRegion.Enabled = true;
            this.dateTimePicker1.Enabled = true;
            this.txtclosedate.Enabled = true;

            btnNew.Enabled = false;
            btnEdit.Enabled = false;
            btnCancelData.Enabled = true;
            btnSaveData.Enabled = true;


            this.btnCopy.Enabled = false;


        }


        private void loadStateDropdown()
        {
            DataSet dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });
            dropdownState.DataSource = dsState.Tables[0];
            dropdownState.DisplayMember = "stateAbbreviation";
            dropdownState.ValueMember = "stateID";
            dropdownState.SelectedValue=MetriconCommon.UserState;

        }

        private void LoadBrandList()
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetBrand]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@stateID",  dropdownState.SelectedValue.ToString() )
                            });

            this.dropBrand.DataSource = dsTemp.Tables[0];
            this.dropBrand.ValueMember = "brandid";
            this.dropBrand.DisplayMember = "brandname";
            this.dropBrand.SelectedIndex = 0;
            //this.dropBrand.Text = "Not Allocated";

        }
        private void LoadRegion()
        {

            //DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetRegion]", new System.Data.SqlClient.SqlParameter[1]
            //                {
            //                   new System.Data.SqlClient.SqlParameter("@stateID",  dropdownState.SelectedValue.ToString() )
            //                });
            //if (dsTemp.Tables[0].Rows.Count > 0)
            //{
            //    this.dropRegion.DataSource = dsTemp.Tables[0];
            //    this.dropRegion.ValueMember = "regionid";
            //    this.dropRegion.DisplayMember = "regionName";
            //    this.dropRegion.SelectedIndex = 0;
            //}

        }
        private void LoadSuburb()
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetDisplaySuburb]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@stateID",  dropdownState.SelectedValue.ToString() )
                            });


            //this.dropSuburb.ValueMember = "idauspost";
            //this.dropSuburb.DisplayMember = "suburb";

            List<string> list = new List<string>();
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    list.Add(dr["suburb"].ToString());
                }
            }

 
            dropSuburb.Items.AddRange(list.ToArray());
            dropSuburb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dropSuburb.AutoCompleteSource = AutoCompleteSource.ListItems;

        }

        private void LoadEstate()
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetDisplayEstate]", new System.Data.SqlClient.SqlParameter[1]
                            {
                               new System.Data.SqlClient.SqlParameter("@stateID",  dropdownState.SelectedValue.ToString() )
                            });


            //this.dropEstate.ValueMember = "idestate";
            //this.dropEstate.DisplayMember = "estatename";

            List<string> list = new List<string>();
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTemp.Tables[0].Rows)
                {
                    list.Add(dr["estatename"].ToString());
                }
            }
            list.Add("");
            dropEstate.Items.AddRange(list.ToArray());
            dropEstate.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            dropEstate.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
        private void btnLookUpParentHome_Click(object sender, EventArgs e)
        {
            if (homeLookupForm == null)
                homeLookupForm = new frmHomeLookup();
            homeLookupForm.ShowDialog();

            if (homeLookupForm.SelectedHome != null && (homeLookupForm.SelectedHome.RowState == DataRowState.Unchanged))
            {
                DataRow row = homeLookupForm.SelectedHome;
                this.txtParentHomeID.Text = row["HomeID"].ToString();
                this.labelPhome.Text = row["HomeName"].ToString();

                if (row["BrandID"].ToString() == "24") // this regional victoria
                {
                    lblFacade.Visible = true;
                    dropFacade.Visible = true;
                    loadFacadeDropDown("1"); // from parenthome button
                    //dropFacade_SelectedIndexChanged(null, null);
                    dropFacade_SelectionChangeCommitted(null, null);
                }
                else
                {
                    lblFacade.Visible = false;
                    dropFacade.Visible = false;
                }

                //if (txtHomeID.Text.ToString() == "")  //if is new display home, copy details from parent home.
                //{
                    this.txtHomePlan.Text = row["HomePlan"].ToString();
                    this.txtHomeFacade.Text = row["Facade"].ToString();
                    this.txtOriginalHomeID.Text = row["HomeID"].ToString();


                    dropdownState.Enabled = true;
                    this.dropdownState.SelectedValue = row["fkStateID"].ToString();
                    dropdownState.Enabled = false;

                    dropBrand.Enabled = true;
                    LoadBrandList();
                    this.dropBrand.SelectedValue = row["BrandID"].ToString();
                    dropBrand.Enabled = false;
                    oldBrandID = Int32.Parse(row["BrandID"].ToString());

                    //LoadRegion();
                    LoadSuburb();


                    this.txtProductID.Text = row["ProductID"].ToString();
                    oldProductID = row["ProductID"].ToString();
                    this.txtHouseArea.Text = row["HouseArea"].ToString();
                    this.txtHouseAreaSquares.Text = row["HouseAreaSquares"].ToString();
                    this.txtAlfrescoArea.Text = row["AlfrescoArea"].ToString();
                    this.txtAlfrescoAreaSquares.Text = row["AlfrescoAreaSquares"].ToString();
                    this.txtGarageArea.Text = row["GarageArea"].ToString();
                    this.txtGarageAreaSquares.Text = row["GarageAreaSquares"].ToString();
                    this.txtTotalArea.Text = row["TotalArea"].ToString();
                    this.txtTotalAreaSquares.Text = row["TotalAreaSquares"].ToString();
                    this.txtMinimumBlockWidth.Text = row["Min_Block_Width"].ToString();
                    this.txtHouseLength.Text = row["HouseLength"].ToString();
                    this.txtHouseWidth.Text = row["HouseWidth"].ToString();
                    this.txtGarageBays.Text = row["GarageBays"].ToString();
                    this.dropBedrooms.Text = row["Bedrooms"].ToString();
                    this.dropBathrooms.Text = row["Bathrooms"].ToString();
                    this.dropStoreys.Text = row["Stories"].ToString();
                    this.txtSortOrder.Text = row["SortOrder"].ToString();
                    this.chkActive.Checked = bool.Parse(row["Active"].ToString());
                    //this.chkDraft.Checked = bool.Parse(row["underReview"].ToString());
                    this.txtDesc.Text = row["DESC"].ToString();
                    this.txtEdesc.Text = row["EDESC"].ToString();
                    this.txtCstctr.Text = row["CSTCTR"].ToString();
                    this.txtSuofm.Text = row["SUOFM"].ToString();
                    this.txtPuofm.Text = row["PUOFM"].ToString();
                    this.txtPrdcat.Text = row["PRDCAT"].ToString();
                    this.txtTaxcd.Text = row["TAXCD"].ToString();
                    this.txtPrdsop.Text = row["PRDSOP"].ToString();
                    this.txtUsrdef.Text = row["USRDEF"].ToString();
                    this.txtLdesc.Text = row["LDESC"].ToString();
                    this.txtBrand.Text = row["Brand"].ToString();
                    this.txtSpecyear.Text = row["SpecYear"].ToString();
                    this.txtHousecode.Text = row["HouseCode"].ToString();
                    this.txtHousesize.Text = row["HouseSize"].ToString();
                    this.txtHousefacade.Text = row["HouseFacade"].ToString();
                    this.txtStorey.Text = row["Storey"].ToString();
                    this.txtMark.Text = row["Mark"].ToString();

                    ViewRead();
                    ViewEdit();
                    
                //}
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            mode = InputFormMode.Edit;
            ViewEdit();
        }

        private void btnCancelData_Click(object sender, EventArgs e)
        {
            ViewRead();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            this.txtHomeID.Text = "";
            this.txtHomePlan.Text = this.txtHomePlan.Text;
            //this.txtProductID.Text = this.txtProductID.Text = "";
            if (dropBrand.SelectedValue.ToString() == "24")
            {
                lblFacade.Visible = true;
                dropFacade.Visible = true;
                loadFacadeDropDown("1");

            }
            btnCopy.Enabled = false;
            
            ViewRead();
            ViewEdit();
            mode = InputFormMode.New;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            mode = InputFormMode.New;
            ClearForm();
            ViewNew();
            ViewEdit();
        }

        private void btnLookupHomeDisplay_Click(object sender, EventArgs e)
        {
            if (homeDisplayLookupForm == null)
                homeDisplayLookupForm = new frmHomeDisplayLookup();
            homeDisplayLookupForm.ShowDialog();

            if (homeDisplayLookupForm.SelectedHomeDisplay != null && (homeDisplayLookupForm.SelectedHomeDisplay.RowState == DataRowState.Unchanged))
            {
                DataRow row = homeDisplayLookupForm.SelectedHomeDisplay;
                this.txtHomeID.Text = row["HomeID"].ToString();
                this.txtOriginalDisHomeID.Text = row["HomeID"].ToString();
                this.txtParentHomeID.Text = row["parentHomeID"].ToString();
                this.txtOriginalHomeID.Text = row["parentHomeID"].ToString();
                this.labelPhome.Text = row["ParentHomename"].ToString();
                this.txtHomePlan.Text = row["HomePlan"].ToString();
                this.txtHomeFacade.Text = row["Facade"].ToString();

                dropdownState.Enabled = true;
                this.dropdownState.SelectedValue = row["fkStateID"].ToString();
                dropdownState.Enabled = false;

                dropBrand.Enabled = true;
                LoadBrandList();
                this.dropBrand.SelectedValue = row["BrandID"].ToString();
                dropBrand.Enabled =false;
                oldBrandID = Int32.Parse(row["BrandID"].ToString());

                if (row["BrandID"].ToString() == "24") // this regional victoria
                {
                    lblFacade.Visible = false;
                    dropFacade.Visible = false;
                    loadFacadeDropDown("0"); // from parenthome button
                    //dropFacade_SelectedIndexChanged(null, null);
                    dropFacade_SelectionChangeCommitted(null, null);
                }
                else
                {
                    lblFacade.Visible = false;
                    dropFacade.Visible = false;
                }

                //LoadRegion();
                LoadSuburb();
                LoadEstate();

                this.txtProductID.Text = row["ProductID"].ToString();
                oldProductID = row["ProductID"].ToString();
                this.txtHouseArea.Text = row["HouseArea"].ToString();
                this.txtHouseAreaSquares.Text = row["HouseAreaSquares"].ToString();
                this.txtAlfrescoArea.Text = row["AlfrescoArea"].ToString();
                this.txtAlfrescoAreaSquares.Text = row["AlfrescoAreaSquares"].ToString();
                this.txtGarageArea.Text = row["GarageArea"].ToString();
                this.txtGarageAreaSquares.Text = row["GarageAreaSquares"].ToString();
                this.txtTotalArea.Text = row["TotalArea"].ToString();
                this.txtTotalAreaSquares.Text = row["TotalAreaSquares"].ToString();
                this.txtMinimumBlockWidth.Text = row["Min_Block_Width"].ToString();
                this.txtHouseLength.Text = row["HouseLength"].ToString();
                this.txtHouseWidth.Text = row["HouseWidth"].ToString();
                this.txtGarageBays.Text = row["GarageBays"].ToString();
                this.dropBedrooms.Text = row["Bedrooms"].ToString();
                this.dropBathrooms.Text = row["Bathrooms"].ToString();
                this.dropStoreys.Text = row["Stories"].ToString();
                this.txtSortOrder.Text = row["SortOrder"].ToString();
                this.chkActive.Checked = bool.Parse(row["Active"].ToString());
                if (row["underReview"] != null && row["underReview"].ToString()!="")
                {
                    this.chkDraft.Checked = bool.Parse(row["underReview"].ToString());
                }
                else
                {
                    this.chkDraft.Checked = false;
                }
                this.txtDesc.Text = row["DESC"].ToString();
                this.txtEdesc.Text = row["EDESC"].ToString();
                this.txtCstctr.Text = row["CSTCTR"].ToString();
                this.txtSuofm.Text = row["SUOFM"].ToString();
                this.txtPuofm.Text = row["PUOFM"].ToString();
                this.txtPrdcat.Text = row["PRDCAT"].ToString();
                this.txtTaxcd.Text = row["TAXCD"].ToString();
                this.txtPrdsop.Text = row["PRDSOP"].ToString();
                this.txtUsrdef.Text = row["USRDEF"].ToString();
                this.txtLdesc.Text = row["LDESC"].ToString();
                this.txtBrand.Text = row["Brand"].ToString();
                this.txtSpecyear.Text = row["SpecYear"].ToString();
                this.txtHousecode.Text = row["HouseCode"].ToString();
                this.txtHousesize.Text = row["HouseSize"].ToString();
                this.txtHousefacade.Text = row["HouseFacade"].ToString();
                this.txtStorey.Text = row["Storey"].ToString();
                this.txtMark.Text = row["Mark"].ToString();

                if (row["displayclosedate"].ToString() != null && row["displayclosedate"].ToString() != "")
                {
                    this.txtclosedate.Text = DateTime.Parse(row["displayclosedate"].ToString()).ToString("dd/MM/yyyy");
                    this.dateTimePicker1.Value = DateTime.Parse(row["displayclosedate"].ToString());
                }
                else
                {
                    dateTimePicker1.Value = DateTime.Today.AddHours(5);
                    this.txtclosedate.Text = "";
                }
 
                this.txtLotAddress.Text = row["lotAddress"].ToString();
                //this.txtSuburb.Text = "";
                if (row["blocksize"].ToString() == "0")
                {
                    this.txtBlockSize.Text = "";
                }
                else
                {
                    this.txtBlockSize.Text = row["blocksize"].ToString();
                }
                //if (row["regionID"].ToString() != "")
                //{
                //    if (dropRegion.FindString(row["regionID"].ToString())>0)
                //    {
                //        this.dropRegion.SelectedValue = Int32.Parse(row["regionID"].ToString());
                //    }
                //    else
                //    {
                //        this.dropRegion.SelectedIndex = 0;
                //    }
                //}
                //else
                //{
                //    this.dropRegion.SelectedIndex =0;
                //}

                dropSuburb.SelectedItem = row["suburb"];
                //this.dropSuburb.SelectedValue = row["suburb"].ToString();
                //this.dropEstate.SelectedValue = row["fkidestate"].ToString();
                this.dropEstate.SelectedItem = row["estatename"].ToString();


                ViewRead();

                btnSaveData.Enabled = false;
                btnEdit.Enabled = true;
                btnCopy.Enabled = true;

            }

        }

        public void SaveForm()
        {
            string action,suburb,postcode,estatename;
            int bedroom, bathroom, storey, brandID, sortOrder, parentHomeID, HomeID,regionID,originalparentHomeID,originaldisplayHomeID,stateID;
            double houseArea, houseAreaSquare, alfrescoArea, alfrescoAreaSquares, garageArea, garageAreaSquares, blocksize;
            double totalArea, totalAreaSquares, minimumBlockWidth, houseLength, houseWidth, garageBays;
            string displayclosedate="";
            //initialize all blank integer field
            if (txtParentHomeID.Text != "" )
            {
                if (txtProductID.Text != "")
                {
                    success = 1;
                    bedroom = MetriconCommon.initIntVariables(dropBedrooms.Text.ToString());
                    bathroom = MetriconCommon.initIntVariables(dropBathrooms.Text.ToString());
                    storey = MetriconCommon.initIntVariables(dropStoreys.Text.ToString());
                    dropBrand.Enabled = true;
                    brandID = MetriconCommon.initIntVariables(dropBrand.SelectedValue.ToString());
                    dropBrand.Enabled = false;
                    stateID = Int32.Parse(dropdownState.SelectedValue.ToString());
                    sortOrder = MetriconCommon.initIntVariables(txtSortOrder.Text.ToString());
                    HomeID = MetriconCommon.initIntVariables(txtHomeID.Text.ToString());
                    parentHomeID = MetriconCommon.initIntVariables(txtParentHomeID.Text.ToString());

                    //regionID = MetriconCommon.initIntVariables(dropRegion.SelectedValue.ToString());
                    originalparentHomeID = MetriconCommon.initIntVariables(txtOriginalHomeID.Text.ToString());
                    originaldisplayHomeID = MetriconCommon.initIntVariables(txtOriginalDisHomeID.Text.ToString());

                    //initialize all blank decimal field
                    houseArea = MetriconCommon.initDoubleVariables(txtHouseArea.Text.ToString());
                    houseAreaSquare = MetriconCommon.initDoubleVariables(txtHouseAreaSquares.Text.ToString());
                    alfrescoArea = MetriconCommon.initDoubleVariables(txtAlfrescoArea.Text.ToString());
                    alfrescoAreaSquares = MetriconCommon.initDoubleVariables(txtAlfrescoAreaSquares.Text.ToString());
                    garageArea = MetriconCommon.initDoubleVariables(txtGarageArea.Text.ToString());
                    garageAreaSquares = MetriconCommon.initDoubleVariables(txtGarageAreaSquares.Text.ToString());
                    totalArea = MetriconCommon.initDoubleVariables(txtTotalArea.Text.ToString());
                    totalAreaSquares = MetriconCommon.initDoubleVariables(txtTotalAreaSquares.Text.ToString());
                    minimumBlockWidth = MetriconCommon.initDoubleVariables(txtMinimumBlockWidth.Text.ToString());
                    houseLength = MetriconCommon.initDoubleVariables(txtHouseLength.Text.ToString());
                    houseWidth = MetriconCommon.initDoubleVariables(txtHouseWidth.Text.ToString());
                    garageBays = MetriconCommon.initDoubleVariables(txtGarageBays.Text.ToString());

                    if (txtclosedate.Text != "")
                    {
                        displayclosedate = dateTimePicker1.Value.ToString("dd/MMM/yyyy");
                    }
                    

                    string sb;
                    try
                    {
                        sb = dropSuburb.SelectedItem.ToString();
                    }
                    catch (Exception ex)
                    {
                        sb = dropSuburb.Text;
                    }

                    try
                    {
                        estatename = dropEstate.SelectedItem.ToString();
                    }
                    catch (Exception ex)
                    {
                        estatename = dropEstate.Text;
                    }

                    if (txtLotAddress.Text == "")
                    {
                        success = 0;
                        MessageBox.Show("Please enter the lot address!");
                        return;
                    }
                    if (sb == "")
                    {
                        suburb = "";
                        postcode="";
                        success = 0;
                        MessageBox.Show("Please select appropriate suburb!");
                        return;
                    }
                    else
                    {
                        suburb = sb.Substring(0, sb.IndexOf("(") - 1);
                        postcode = sb.Substring(sb.IndexOf("(") + 1, sb.IndexOf(")") - sb.IndexOf("(")-1);
                    }

                    if (estatename == "")
                    {
                        success = 0;
                        MessageBox.Show("Please select the estate!");
                        return;
                    }


                    try
                    {
                        if (txtBlockSize.Text != "")
                        {
                            blocksize = double.Parse(txtBlockSize.Text);
                        }
                        else
                        {
                            success = 0;
                            MessageBox.Show("Please select appropriate block size!!");
                            return;
                        }
                    }
                    catch
                    {
                        success = 0;
                        MessageBox.Show("Please select appropriate block size!!");
                        return;
                    }
                    //if (radioButton1.Checked)
                    //{
                    //    if (dropSuburb.SelectedValue != null)
                    //    {
                    //        suburb = dropSuburb.SelectedValue.ToString();
                    //    }
                    //    else
                    //    {
                    //        suburb = "";
                    //        success = 0;
                    //        MessageBox.Show("Please select appropriate suburb!!");
                    //    }
                    //}
                    //else
                    //{
                    //    suburb = txtSuburb.Text.ToString();
                    //}

                    if (mode == InputFormMode.New)
                    {
                        action = "NEW";
                    }
                    else
                    {
                        action = "EDIT";
                    }

                    if (suburb != "")
                    {
                        //execute procedure to save data
                        //this.pictureBox1.Visible = true;
                        MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminAddEditHomeDisplay", new System.Data.SqlClient.SqlParameter[52] 
                                                         {
                                                                 new System.Data.SqlClient.SqlParameter("@HomeID",HomeID)
                                                                , new System.Data.SqlClient.SqlParameter("@HomePlan",txtHomePlan.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@HomeFacade",txtHomeFacade.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@StateID", stateID)
                                                                , new System.Data.SqlClient.SqlParameter("@BrandID", brandID)
                                                                , new System.Data.SqlClient.SqlParameter("@ProductID", txtProductID.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@HouseArea", houseArea)
                                                                , new System.Data.SqlClient.SqlParameter("@HouseAreaSquares", houseAreaSquare)
                                                                , new System.Data.SqlClient.SqlParameter("@AlfrescoArea", alfrescoArea)
                                                                , new System.Data.SqlClient.SqlParameter("@AlfrescoAreaSquares", alfrescoAreaSquares)
                                                                , new System.Data.SqlClient.SqlParameter("@GarageArea", garageArea)
                                                                , new System.Data.SqlClient.SqlParameter("@GarageAreaSquares", garageAreaSquares)
                                                                , new System.Data.SqlClient.SqlParameter("@TotalArea", totalArea)
                                                                , new System.Data.SqlClient.SqlParameter("@TotalAreaSquares", totalAreaSquares)
                                                                , new System.Data.SqlClient.SqlParameter("@Min_Block_Width", minimumBlockWidth)
                                                                , new System.Data.SqlClient.SqlParameter("@HouseLength", houseLength)
                                                                , new System.Data.SqlClient.SqlParameter("@HouseWidth", houseWidth)
                                                                , new System.Data.SqlClient.SqlParameter("@GarageBays", garageBays)
                                                                , new System.Data.SqlClient.SqlParameter("@Bedrooms", bedroom)
                                                                , new System.Data.SqlClient.SqlParameter("@Bathrooms", bathroom)
                                                                , new System.Data.SqlClient.SqlParameter("@Stories", storey)
                                                                , new System.Data.SqlClient.SqlParameter("@DESC", txtDesc.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@EDESC", txtEdesc.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@CSTCTR", txtCstctr.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@SUOFM", txtSuofm.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@PUOFM", txtPuofm.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@PRDCAT", txtPrdcat.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@TAXCD", txtTaxcd.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@PRDSOP", txtPrdsop.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@USRDEF", txtUsrdef.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@LDESC", txtLdesc.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@BRAND", txtBrand.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@SPECYEAR", txtSpecyear.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@HOUSECODE", txtHousecode.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@HOUSESIZE", txtHousesize.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@HOUSEFACADE", txtHousefacade.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@STOREY", txtStorey.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@MARK", txtMark.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@SortOrder", txtSortOrder.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode)
                                                                , new System.Data.SqlClient.SqlParameter("@Active", chkActive.Checked)
                                                                , new System.Data.SqlClient.SqlParameter("@draft", chkDraft.Checked)
                                                                , new System.Data.SqlClient.SqlParameter("@parentHomeID", parentHomeID)
                                                                , new System.Data.SqlClient.SqlParameter("@action",action)
                                                                , new System.Data.SqlClient.SqlParameter("@lotAddress",txtLotAddress.Text)
                                                                , new System.Data.SqlClient.SqlParameter("@suburb",suburb)
                                                                , new System.Data.SqlClient.SqlParameter("@postcode",postcode)
                                                                , new System.Data.SqlClient.SqlParameter("@blocksize",blocksize)
                                                                , new System.Data.SqlClient.SqlParameter("@originalParentHomeID",originalparentHomeID)
                                                                , new System.Data.SqlClient.SqlParameter("@originalDisplayHomeID",originaldisplayHomeID)
                                                                , new System.Data.SqlClient.SqlParameter("@estatename",estatename)
                                                                , new System.Data.SqlClient.SqlParameter("@displayclosedate",displayclosedate)
                                                        }
                      );
                        //get the id of the new house that was inserted
                        //DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select max(homeid) as homeid from home");

                        //this.txtHomeID.Text = dsTemp.Tables[0].Rows.Count == 1 ? dsTemp.Tables[0].Rows[0]["homeid"].ToString() : "";
                        if (action == "NEW")
                        {
                            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select max(homeid) as homeid from home");
                            this.txtHomeID.Text = dsTemp.Tables[0].Rows.Count == 1 ? dsTemp.Tables[0].Rows[0]["homeid"].ToString() : "";
                        }
                        this.pictureBox1.Visible = false;
                    }

                }
                else
                {
                    success = 0;
                    MessageBox.Show("Please select a facade!", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                    
            }
            else
            {
                success = 0;
                MessageBox.Show("Please select a parent home!", "Missing Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
                
        }



        private void btnSaveData_Click(object sender, EventArgs e)
        {
            SaveForm();
            if (success == 1)
            {
                ViewRead();
            }
        }

        private void btnLookupProduct_Click(object sender, EventArgs e)
        {
            if (productLookupForm == null)
                productLookupForm = new frmProductLookup();
            productLookupForm.ShowDialog();
            if ((productLookupForm.SelectedRow != null) && (productLookupForm.SelectedRow.RowState == DataRowState.Unchanged))
                this.txtProductID.Text = productLookupForm.SelectedRow["ProductID"].ToString();
        }

        //private void radioButton1_CheckedChanged(object sender, EventArgs e)
        //{
        //    setbuttons1();
        //}

        //private void radioButton2_CheckedChanged(object sender, EventArgs e)
        //{
        //    setbuttons2();
        //}

        //private void setbuttons1()
        //{
        //    radioButton1.Checked = true;
        //    radioButton2.Checked = false;
        //    dropSuburb.Enabled = true;
        //    txtSuburb.Enabled = false;
        //}
        //private void setbuttons2()
        //{
        //    radioButton2.Checked = true;
        //    radioButton1.Checked = false;
        //    dropSuburb.Enabled = false;
        //    txtSuburb.Enabled = true;
        //}

        private void dropdownState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadBrandList();
            //LoadRegion();
        }
        private void loadFacadeDropDown(string parenthome)
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetFacadeForRegionalVicHomeByHomeID]", new System.Data.SqlClient.SqlParameter[2]
                            {
                                new System.Data.SqlClient.SqlParameter("@homeID",this.txtParentHomeID.Text ),
                                new System.Data.SqlClient.SqlParameter("@parenthome",parenthome )
                            });

            dropFacade.DataSource = dsTemp.Tables[0];
            dropFacade.ValueMember = "productid";
            dropFacade.DisplayMember = "productname";
            dropFacade.SelectedIndex = 0;

        }

        private void dropFacade_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] temp;
            if (dropFacade.SelectedValue.ToString() == "SELECT FACADE,SELECT FACADE,SELECT FACADE")
            {
                txtProductID.Text = "";
            }
            else
            {
                temp=dropFacade.SelectedValue.ToString().Split(',');
                txtProductID.Text = temp[0];
                txtHomePlan.Text = temp[1];
                txtLdesc.Text = temp[2];
            }
        }

        private void dropFacade_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string[] temp;
            if (dropFacade.SelectedValue.ToString() == "SELECT FACADE,SELECT FACADE,SELECT FACADE")
            {
                txtProductID.Text = "";
            }
            else
            {
                temp = dropFacade.SelectedValue.ToString().Split(',');
                txtProductID.Text = temp[0];
                txtHomePlan.Text = temp[1];
                txtLdesc.Text = temp[2];
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           txtclosedate.Text=  dateTimePicker1.Value.ToString("dd/MM/yyyy");
        }
        
    }
}