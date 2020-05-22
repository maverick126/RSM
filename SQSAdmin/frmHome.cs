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
	public partial class frmHome : Form, IInputForm
	{

		private InputFormMode mode;
		//private DataRow selectedHome;
		private frmHomeLookup homeLookupForm;
		private frmProductLookup productLookupForm;
        public int successful = 1;
        public string oldState="1";
        private System.ComponentModel.BackgroundWorker backgroundWorker1;

		public frmHome()
		{
            backgroundWorker1 = new BackgroundWorker();
            InitializeComponent();
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            InitializeBackgroundWorker();
		}
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

		private void frmHome_Load(object sender, EventArgs e)
		{
            
            loadStateDropdown();
			LoadBrandList();
            btnLookupHome_Click(sender, e);
			ViewRead();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
        private void loadStateDropdown()
        {
            DataSet dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });
            dropdownState.DataSource = dsState.Tables[0];
            dropdownState.DisplayMember = "stateAbbreviation";
            dropdownState.ValueMember = "stateID";
            dropdownState.SelectedValue = MetriconCommon.UserState;

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

		public void ViewNew()
		{
			mode = InputFormMode.New;
			this.txtHomePlan.ReadOnly = false;
            this.txtHomeFacade.ReadOnly = false;
            this.dropdownState.Enabled = true;
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
            this.chkShowOnList.Enabled = true;
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
            this.txtLivingArea.ReadOnly = false;


			btnNew.Enabled = false;
			btnEdit.Enabled = false;
			btnCancelData.Enabled = true;
			btnSaveData.Enabled = true;


			this.btnCopy.Enabled = false;
		}
		public void ViewEdit()
		{
			mode = InputFormMode.Edit;

            this.txtHomePlan.ReadOnly = false;
            this.txtHomeFacade.ReadOnly = false;
            this.txtHomePlan.Enabled = true;
            this.txtHomeFacade.Enabled = true;
            this.dropdownState.Enabled = false;
			this.dropBrand.Enabled = true;
            this.txtProductID.ReadOnly = true;
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
            this.chkShowOnList.Enabled = true;
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
			this.txtLdesc.ReadOnly = true;
			this.txtBrand.ReadOnly = false;
			this.txtSpecyear.ReadOnly = false;
			this.txtHousecode.ReadOnly = false;
			this.txtHousesize.ReadOnly = false;
			this.txtHousefacade.ReadOnly = false;
			this.txtStorey.ReadOnly = false;
			this.txtMark.ReadOnly = false;
			this.btnLookupProduct.Enabled = true;
            this.txtLivingArea.ReadOnly = false;

			btnNew.Enabled = false;
			btnEdit.Enabled = false;
			btnCancelData.Enabled = true;
			btnSaveData.Enabled = true;


			this.btnCopy.Enabled = false;


		}
        public void SaveForm()
		{
                        int bedroom, bathroom, storey,stateID,brandID,sortOrder,originalHomeID, valid;
                        double houseArea, houseAreaSquare, alfrescoArea, alfrescoAreaSquares,garageArea,garageAreaSquares;
                        double totalArea, totalAreaSquares, minimumBlockWidth, houseLength, houseWidth, garageBays, livingarea;

                        DataSet ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminValidateProductCodeAndState", new System.Data.SqlClient.SqlParameter[2]
                            {
                               new System.Data.SqlClient.SqlParameter("@productid",  txtProductID.Text ),
                               new System.Data.SqlClient.SqlParameter("@stateID",  dropdownState.SelectedValue.ToString() )
                            });

                        valid = int.Parse(ds.Tables[0].Rows[0]["valid"].ToString());


                        //if ((txtProductID.Text.Substring(0, 1) == "N" && dropdownState.SelectedValue.ToString() != "2") || (txtProductID.Text.Substring(0, 1) != "N" && dropdownState.SelectedValue.ToString() == "2") ||
                        //    (txtProductID.Text.Substring(0, 1) == "Q" && dropdownState.SelectedValue.ToString() != "3") || (txtProductID.Text.Substring(0, 1) != "Q" && dropdownState.SelectedValue.ToString() == "3") ||
                        //    (txtProductID.Text.Substring(0, 1) == "A" && dropdownState.SelectedValue.ToString() != "4") || (txtProductID.Text.Substring(0, 1) != "A" && dropdownState.SelectedValue.ToString() == "4")
                        //    )
                        if (valid==0)
                        {
                            MessageBox.Show("The ProductID does NOT match the state you selected!\nPlease ensure appropriate code is entered and correct state is selected!!");
                            successful = 0;
                        }
                        else if (txtHomePlan.Text.IndexOf("-NEW") > 0)
                        {
                            MessageBox.Show("The home name is incorrect!!");
                            successful = 0;
                        }
                        else
                        {
                            //initialize all blank integer field
                            bedroom = MetriconCommon.initIntVariables(dropBedrooms.Text.ToString());
                            bathroom = MetriconCommon.initIntVariables(dropBathrooms.Text.ToString());
                            storey = MetriconCommon.initIntVariables(dropStoreys.Text.ToString());
                            brandID = MetriconCommon.initIntVariables(dropBrand.SelectedValue.ToString());
                            sortOrder = MetriconCommon.initIntVariables(txtSortOrder.Text.ToString());
                            originalHomeID = MetriconCommon.initIntVariables(txtOriginalHomeID.Text.ToString());

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
                            try
                            {
                                livingarea = double.Parse(txtLivingArea.Text.ToString());
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Please enter a valid living area number.");
                                return;
                            }

                            stateID = Int32.Parse(dropdownState.SelectedValue.ToString());

                            if (mode == InputFormMode.New)
                            {
                                //execute procedure to save data

                                backgroundWorker1.RunWorkerAsync();

                                MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminAddHome", new System.Data.SqlClient.SqlParameter[44] {
					            new System.Data.SqlClient.SqlParameter("@HomePlan",txtHomePlan.Text)
                                , new System.Data.SqlClient.SqlParameter("@homefacade", txtHomeFacade.Text)
                                , new System.Data.SqlClient.SqlParameter("@originalHomeID", originalHomeID)
                                , new System.Data.SqlClient.SqlParameter("@stateID", stateID)
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
                                , new System.Data.SqlClient.SqlParameter("@showOnPriceList", chkShowOnList.Checked)
                                , new System.Data.SqlClient.SqlParameter("@draft", chkDraft.Checked)
                                , new System.Data.SqlClient.SqlParameter("@livingarea", livingarea)
                                });

                                
                                //get the id of the new house that was inserted
                                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select max(homeid) as homeid from home");

                                this.txtHomeID.Text = dsTemp.Tables[0].Rows.Count == 1 ? dsTemp.Tables[0].Rows[0]["homeid"].ToString() : "";
                                if (pictureBox1.Visible)
                                { this.pictureBox1.Visible = false; }

                                if (oldState != stateID.ToString())
                                {
                                    MessageBox.Show("The home was created successfully, but no any PAG was copied to new home! ");
                                }
                            }
                            else
                            {
                                //execute procedure to update data


                                MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminEditHome", new System.Data.SqlClient.SqlParameter[44] {
					            new System.Data.SqlClient.SqlParameter("@HomePlan",txtHomePlan.Text)
                                , new System.Data.SqlClient.SqlParameter("@homefacade", txtHomeFacade.Text)
                                , new System.Data.SqlClient.SqlParameter("@stateID", stateID)
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
					            , new System.Data.SqlClient.SqlParameter("@SortOrder", sortOrder)
					            , new System.Data.SqlClient.SqlParameter("@ModifiedBy", MetriconCommon.UserCode)
					            , new System.Data.SqlClient.SqlParameter("@Active", chkActive.Checked)

                                , new System.Data.SqlClient.SqlParameter("@showOnPriceList", chkShowOnList.Checked)
                                , new System.Data.SqlClient.SqlParameter("@draft", chkDraft.Checked)
					            , new System.Data.SqlClient.SqlParameter("@HomeID", txtHomeID.Text)
                                , new System.Data.SqlClient.SqlParameter("@livingarea", livingarea)
                                
                                });

                            }
                            successful = 1;

                        }
		}

        public string SaveHomeForm(
            int bedroom, int bathroom, int storey, int brandID, int stateID, int sortOrder, int originalHomeID,
            double houseArea, double houseAreaSquare, double alfrescoArea, double alfrescoAreaSquares, double garageArea, double garageAreaSquares,
            double totalArea, double totalAreaSquares, double minimumBlockWidth, double houseLength, double houseWidth, double garageBays,
            string homeplan, string homefacade, string productid, string desc, string edesc, string cstctr, string suofm, string PUOFM, string PRDCAT, string TAXCD, string PRDSOP, string USRDEF, string LDESC, string BRAND,
            string SPECYEAR, string HOUSECODE, string HOUSESIZE, string HOUSEFACADE, string STOREY, string SOrder, string MARK, bool Active, bool showOnPriceList, bool draft, string CreatedBy, int homeid, double livingarea
            )
        {

                if (mode == InputFormMode.New)
                {
                    //execute procedure to save data

                    MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminAddHome", new System.Data.SqlClient.SqlParameter[44] {
					            new System.Data.SqlClient.SqlParameter("@HomePlan",homeplan)
                                , new System.Data.SqlClient.SqlParameter("@homefacade", homefacade)
                                , new System.Data.SqlClient.SqlParameter("@originalHomeID", originalHomeID)
                                , new System.Data.SqlClient.SqlParameter("@stateID", stateID)
					            , new System.Data.SqlClient.SqlParameter("@BrandID", brandID)
					            , new System.Data.SqlClient.SqlParameter("@ProductID", productid)
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
					            , new System.Data.SqlClient.SqlParameter("@DESC", desc)
					            , new System.Data.SqlClient.SqlParameter("@EDESC", edesc)
					            , new System.Data.SqlClient.SqlParameter("@CSTCTR", cstctr)
					            , new System.Data.SqlClient.SqlParameter("@SUOFM", suofm)
					            , new System.Data.SqlClient.SqlParameter("@PUOFM", PUOFM)
					            , new System.Data.SqlClient.SqlParameter("@PRDCAT", PRDCAT)
					            , new System.Data.SqlClient.SqlParameter("@TAXCD", TAXCD)
					            , new System.Data.SqlClient.SqlParameter("@PRDSOP", PRDSOP)
					            , new System.Data.SqlClient.SqlParameter("@USRDEF", USRDEF)
					            , new System.Data.SqlClient.SqlParameter("@LDESC", LDESC)
					            , new System.Data.SqlClient.SqlParameter("@BRAND", BRAND)
					            , new System.Data.SqlClient.SqlParameter("@SPECYEAR", SPECYEAR)
					            , new System.Data.SqlClient.SqlParameter("@HOUSECODE", HOUSECODE)
					            , new System.Data.SqlClient.SqlParameter("@HOUSESIZE", HOUSESIZE)
					            , new System.Data.SqlClient.SqlParameter("@HOUSEFACADE", HOUSEFACADE)
					            , new System.Data.SqlClient.SqlParameter("@STOREY", STOREY)
					            , new System.Data.SqlClient.SqlParameter("@MARK", MARK)
					            , new System.Data.SqlClient.SqlParameter("@SortOrder",SOrder)
					            , new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy)
					            , new System.Data.SqlClient.SqlParameter("@Active", Active)
                                , new System.Data.SqlClient.SqlParameter("@showOnPriceList",showOnPriceList)
                                , new System.Data.SqlClient.SqlParameter("@draft", draft)
                                , new System.Data.SqlClient.SqlParameter("@livingarea", livingarea)
                    });


                    //get the id of the new house that was inserted
                    DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select max(homeid) as homeid from home");


                    if (oldState != stateID.ToString())
                    {
                        MessageBox.Show("The home was created successfully, but no any PAG was copied to new home! ");
                    }

                    return dsTemp.Tables[0].Rows.Count == 1 ? dsTemp.Tables[0].Rows[0]["homeid"].ToString() : "";
                }
                else
                {
                    //execute procedure to update data


                    MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminEditHome", new System.Data.SqlClient.SqlParameter[44] {
					            new System.Data.SqlClient.SqlParameter("@Homeplan",homeplan)
                                , new System.Data.SqlClient.SqlParameter("@homefacade", homefacade)
                                , new System.Data.SqlClient.SqlParameter("@stateID", stateID)
					            , new System.Data.SqlClient.SqlParameter("@BrandID", brandID)
					            , new System.Data.SqlClient.SqlParameter("@ProductID", productid)
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
					            , new System.Data.SqlClient.SqlParameter("@DESC", desc)
					            , new System.Data.SqlClient.SqlParameter("@EDESC", edesc)
					            , new System.Data.SqlClient.SqlParameter("@CSTCTR", cstctr)
					            , new System.Data.SqlClient.SqlParameter("@SUOFM", suofm)
					            , new System.Data.SqlClient.SqlParameter("@PUOFM", PUOFM)
					            , new System.Data.SqlClient.SqlParameter("@PRDCAT", PRDCAT)
					            , new System.Data.SqlClient.SqlParameter("@TAXCD", TAXCD)
					            , new System.Data.SqlClient.SqlParameter("@PRDSOP", PRDSOP)
					            , new System.Data.SqlClient.SqlParameter("@USRDEF", USRDEF)
					            , new System.Data.SqlClient.SqlParameter("@LDESC", LDESC)
					            , new System.Data.SqlClient.SqlParameter("@BRAND", BRAND)
					            , new System.Data.SqlClient.SqlParameter("@SPECYEAR", SPECYEAR)
					            , new System.Data.SqlClient.SqlParameter("@HOUSECODE", HOUSECODE)
					            , new System.Data.SqlClient.SqlParameter("@HOUSESIZE", HOUSESIZE)
					            , new System.Data.SqlClient.SqlParameter("@HOUSEFACADE", HOUSEFACADE)
					            , new System.Data.SqlClient.SqlParameter("@STOREY", STOREY)
					            , new System.Data.SqlClient.SqlParameter("@MARK", MARK)
					            , new System.Data.SqlClient.SqlParameter("@SortOrder",SOrder)
					            , new System.Data.SqlClient.SqlParameter("@CreatedBy",CreatedBy)
					            , new System.Data.SqlClient.SqlParameter("@Active", Active)
                                , new System.Data.SqlClient.SqlParameter("@showOnPriceList",showOnPriceList)
                                , new System.Data.SqlClient.SqlParameter("@draft", draft)
					            , new System.Data.SqlClient.SqlParameter("@HomeID", homeid) 
                                , new System.Data.SqlClient.SqlParameter("@livingarea", livingarea)
                    
                    });

                    return homeid.ToString();

                }

        }

		public void ClearForm()
		{
			this.txtHomeID.Text = "";
            this.txtOriginalHomeID.Text = "";
			this.txtHomePlan.Text = "";
            this.txtHomeFacade.Text = "";
            this.dropdownState.SelectedValue=MetriconCommon.UserState;
			//this.dropBrand.Text = "";
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
            this.chkShowOnList.Checked = true;
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
            this.txtLivingArea.Text = "";
		}
		public void ViewRead()
		{
			mode = InputFormMode.Read;
			this.txtHomeID.ReadOnly = true;
			this.txtHomePlan.ReadOnly = true;
            this.txtHomeFacade.ReadOnly = true;
            this.txtHomePlan.Enabled = false;
            this.txtHomeFacade.Enabled = false;
            this.dropdownState.Enabled = false;
			this.dropBrand.Enabled = false;
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
            this.chkShowOnList.Enabled = false;
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
            this.txtLivingArea.ReadOnly = true;

			btnNew.Enabled = true;
			btnEdit.Enabled = true;
			btnCancelData.Enabled = false;
			btnSaveData.Enabled = false;

			if (!(string.IsNullOrEmpty(txtHomeID.Text)))
				btnCopy.Enabled = true;
			else
				btnCopy.Enabled = false;


		}

		private void btnLookupHome_Click(object sender, EventArgs e)
		{
			if (homeLookupForm == null)
				homeLookupForm = new frmHomeLookup();
			homeLookupForm.ShowDialog();

			if (homeLookupForm.SelectedHome != null && (homeLookupForm.SelectedHome.RowState == DataRowState.Unchanged))
			{
				DataRow row = homeLookupForm.SelectedHome;
				this.txtHomeID.Text = row["HomeID"].ToString();
                this.txtOriginalHomeID.Text = row["HomeID"].ToString();
				this.txtHomePlan.Text = row["HomePlan"].ToString();
                this.txtHomeFacade.Text = row["Facade"].ToString();
                this.dropdownState.SelectedValue = row["fkStateID"].ToString();
                LoadBrandList();
                oldState = row["fkStateID"].ToString();
				this.dropBrand.SelectedValue = row["BrandID"].ToString();
				this.txtProductID.Text = row["ProductID"].ToString();
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
                this.chkShowOnList.Checked = bool.Parse(row["showOnPriceList"].ToString());
                this.chkDraft.Checked = bool.Parse(row["draft"].ToString());
                this.txtLivingArea.Text = row["livingArea"].ToString();
			}

			ViewRead();

		}

		private void btnLookupProduct_Click(object sender, EventArgs e)
		{
			if (productLookupForm == null)
				productLookupForm = new frmProductLookup();
			productLookupForm.ShowDialog();
            if ((productLookupForm.SelectedRow != null) && (productLookupForm.SelectedRow.RowState == DataRowState.Unchanged))
            {
                this.txtProductID.Text = productLookupForm.SelectedRow["ProductID"].ToString();
                txtHomePlan.Text = productLookupForm.SelectedRow["ProductName"].ToString();
                txtLdesc.Text = productLookupForm.SelectedRow["ProductDescription"].ToString();
                txtProductID.ReadOnly = true;
                txtHomePlan.ReadOnly = false;
                txtHomeFacade.ReadOnly = false;
                txtLdesc.ReadOnly = true;
            }
		}

		private void btnNew_Click(object sender, EventArgs e)
		{
			ClearForm();
			ViewNew();
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
            ViewNew();

			this.txtHomeID.Text = "";
            txtHomePlan.ReadOnly = false;
            txtHomeFacade.ReadOnly = false;
			this.txtProductID.ReadOnly=true;
            this.txtLdesc.ReadOnly = true;
			btnCopy.Enabled = false;

		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			ViewEdit();
		}

		private void btnCancelData_Click(object sender, EventArgs e)
		{
			ViewRead();
		}

		private void btnSaveData_Click(object sender, EventArgs e)
		{
            int homeid=0;
            int valid = 1;
            if (txtHomeID.Text!=null && txtHomeID.Text.Trim()!="")
            {
                homeid=int.Parse(txtHomeID.Text);
            }

            DataSet ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("spa_AdminValidateProductCodeAndState", new System.Data.SqlClient.SqlParameter[2]
                            {
                               new System.Data.SqlClient.SqlParameter("@productid",  txtProductID.Text ),
                               new System.Data.SqlClient.SqlParameter("@stateID",  dropdownState.SelectedValue.ToString() )
                            });

            valid = int.Parse(ds.Tables[0].Rows[0]["valid"].ToString());

            //if ((txtProductID.Text.Substring(0, 1) == "N" && dropdownState.SelectedValue.ToString() != "2") || (txtProductID.Text.Substring(0, 1) != "N" && dropdownState.SelectedValue.ToString() == "2") ||
            //    (txtProductID.Text.Substring(0, 1) == "Q" && dropdownState.SelectedValue.ToString() != "3") || (txtProductID.Text.Substring(0, 1) != "Q" && dropdownState.SelectedValue.ToString() == "3") ||
            //    (txtProductID.Text.Substring(0, 1) == "A" && dropdownState.SelectedValue.ToString() != "4") || (txtProductID.Text.Substring(0, 1) != "A" && dropdownState.SelectedValue.ToString() == "4")
            //    )
            if (valid==0)
            {
                MessageBox.Show("The ProductID does NOT match the state you selected!\nPlease ensure appropriate code is entered and correct state is selected!!");
                successful = 0;
            }
            else if (txtHomePlan.Text.IndexOf("-NEW") > 0)
            {
                MessageBox.Show("The home name is incorrect!!");
                successful = 0;
            }
            else
            {
                try
                {
                    pictureBox1.Visible = true;
                    CallParameter cp = new CallParameter();
                    cp.Bedroom = MetriconCommon.initIntVariables(dropBedrooms.Text.ToString());
                    cp.Bathroom = MetriconCommon.initIntVariables(dropBathrooms.Text.ToString());
                    cp.Storeys = MetriconCommon.initIntVariables(dropStoreys.Text.ToString());
                    cp.BrandID = MetriconCommon.initIntVariables(dropBrand.SelectedValue.ToString());
                    cp.SortOrder = MetriconCommon.initIntVariables(txtSortOrder.Text.ToString());
                    cp.OriginalHomeID = MetriconCommon.initIntVariables(txtOriginalHomeID.Text.ToString());
                    cp.HouseArea = MetriconCommon.initDoubleVariables(txtHouseArea.Text);
                    cp.HouseAreaSquares = MetriconCommon.initDoubleVariables(txtHouseAreaSquares.Text.ToString());
                    cp.AlfrescoArea = MetriconCommon.initDoubleVariables(txtAlfrescoArea.Text.ToString());
                    cp.AlfrescoAreaSquares = MetriconCommon.initDoubleVariables(txtAlfrescoAreaSquares.Text.ToString());
                    cp.GarageArea = MetriconCommon.initDoubleVariables(txtGarageArea.Text.ToString());
                    cp.GarageAreaSquares = MetriconCommon.initDoubleVariables(txtGarageAreaSquares.Text.ToString());
                    cp.TotalArea = MetriconCommon.initDoubleVariables(txtTotalArea.Text.ToString());
                    cp.TotalAreaSquares = MetriconCommon.initDoubleVariables(txtTotalAreaSquares.Text.ToString());
                    cp.MinimumBlockWidth = MetriconCommon.initDoubleVariables(txtMinimumBlockWidth.Text.ToString());
                    cp.HouseLength = MetriconCommon.initDoubleVariables(txtHouseLength.Text.ToString());
                    cp.HouseWidth = MetriconCommon.initDoubleVariables(txtHouseWidth.Text.ToString());
                    cp.GarageBays = MetriconCommon.initDoubleVariables(txtGarageBays.Text.ToString());
                    cp.HomePlan = txtHomePlan.Text;
                    cp.HomeFacade = txtHomeFacade.Text;
                    cp.ProductID = txtProductID.Text;
                    cp.Desc = txtDesc.Text;
                    cp.Edesc = txtEdesc.Text;
                    cp.Cstctr = txtCstctr.Text;
                    cp.Suofm = txtSuofm.Text;
                    cp.Puofm = txtPuofm.Text;
                    cp.Prdcat = txtPrdcat.Text;
                    cp.Taxcd = txtTaxcd.Text;
                    cp.Prdsop = txtPrdsop.Text;
                    cp.Usrdef = txtUsrdef.Text;
                    cp.Ldesc = txtLdesc.Text;
                    cp.BrandName = txtBrand.Text;
                    cp.Specyear = txtSpecyear.Text;
                    cp.Housecode = txtHousecode.Text;
                    cp.Housesize = txtHousesize.Text;
                    cp.Housefacade = txtHousefacade.Text;
                    cp.Storey = txtStorey.Text;
                    cp.SOrder = txtSortOrder.Text;
                    cp.Mark = txtMark.Text;
                    cp.Active = chkActive.Checked;
                    cp.ShowOnList = chkShowOnList.Checked;
                    cp.Draft = chkDraft.Checked;
                    cp.UserCode = MetriconCommon.UserCode;
                    cp.homeid = homeid;
                    cp.State = MetriconCommon.initIntVariables(dropdownState.SelectedValue.ToString());

                    cp.LivingArea = MetriconCommon.initDoubleVariables(txtLivingArea.Text);
                    backgroundWorker1.RunWorkerAsync(cp);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please enter a valid living area number.");
                    pictureBox1.Visible = false;
                    return;
                }
                
            }
        
            //SaveForm();
            //if (successful == 1)
            //{
            //    ViewRead();
            //}

		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txtHouseArea_Leave(object sender, EventArgs e)
		{
			MetriconCommon.ToDouble(ref this.txtHouseArea);
		}

		private void txtHouseAreaSquares_Leave(object sender, EventArgs e)
		{
			MetriconCommon.ToDouble(ref this.txtHouseAreaSquares);

		}

		private void txtAlfrescoArea_Leave(object sender, EventArgs e)
		{
			MetriconCommon.ToDouble(ref this.txtAlfrescoArea);

		}

		private void txtAlfrescoAreaSquares_Leave(object sender, EventArgs e)
		{
			MetriconCommon.ToDouble(ref this.txtAlfrescoAreaSquares);

		}

		private void txtGarageArea_Leave(object sender, EventArgs e)
		{
            MetriconCommon.ToDouble(ref this.txtGarageArea);

		}

		private void txtGarageAreaSquares_Leave(object sender, EventArgs e)
		{
            MetriconCommon.ToDouble(ref this.txtGarageAreaSquares);

		}

		private void txtTotalArea_Leave(object sender, EventArgs e)
		{
            MetriconCommon.ToDouble(ref this.txtTotalArea);

		}

		private void txtTotalAreaSquares_Leave(object sender, EventArgs e)
		{
            MetriconCommon.ToDouble(ref this.txtTotalAreaSquares);

		}

		private void txtMinimumBlockWidth_Leave(object sender, EventArgs e)
		{
            MetriconCommon.ToDouble(ref this.txtMinimumBlockWidth);

		}

		private void txtHouseLength_Leave(object sender, EventArgs e)
		{
            MetriconCommon.ToDouble(ref this.txtHouseLength);

		}

		private void txtHouseWidth_Leave(object sender, EventArgs e)
		{
            MetriconCommon.ToDouble(ref this.txtHouseWidth);

		}

		private void txtGarageBays_Leave(object sender, EventArgs e)
		{
            MetriconCommon.ToDouble(ref this.txtGarageBays);

		}

		private void txtSortOrder_Leave(object sender, EventArgs e)
		{
            //MetriconCommon.ToInteger(ref this.txtSortOrder);

		}

        private void dropdownState_SelectionChangeCommitted(object sender, EventArgs e)
        {
            LoadBrandList();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.pictureBox1.Visible = false;
            if (successful == 1)
            {
                ViewRead();
            }
            this.txtHomeID.Text = e.Result.ToString();
            //pdfname = e.Result.ToString();

            //if (File.Exists(pdfname))
            //{
            //    if (frmDOL != null)
            //    {
            //        frmDOL = null;
            //    }
            //    frmDOL = new frmDOLPDF(pdfname);
            //    frmDOL.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("DOL is NOT found!");
            //}
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;
            CallParameter cp = (CallParameter)e.Argument;
            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            e.Result = SaveHomeForm(
                cp.Bedroom,cp.Bathroom, cp.Storeys, cp.BrandID, cp.State, cp.SortOrder, cp.OriginalHomeID, 
                cp.HouseArea, cp.HouseAreaSquares, cp.AlfrescoArea,cp.AlfrescoAreaSquares,cp.GarageArea,cp.GarageAreaSquares,
                cp.TotalArea, cp.TotalAreaSquares, cp.MinimumBlockWidth, cp.HouseLength, cp.HouseWidth,cp.GarageBays, cp.HomePlan,cp.HomeFacade, cp.ProductID, cp.Desc,
                cp.Edesc,cp.Cstctr, cp.Suofm,cp.Puofm, cp.Prdcat,cp.Taxcd,cp.Prdsop,cp.Usrdef, cp.Ldesc, cp.BrandName, cp.Specyear, cp.Housecode, cp.Housesize,cp.Housefacade,
                cp.Storey,cp.SOrder, cp.Mark,cp.Active, cp.ShowOnList, cp.Draft,cp.UserCode, cp.homeid, cp.LivingArea
                );


        }

        public class CallParameter
        {
               public int Bedroom {get; set;}
               public int Bathroom  {get; set;}
               public int Storeys {get; set;}
               public int BrandID {get; set;}
               public string BrandName { get; set; }
               public int State {get; set;}
               public int SortOrder {get; set;}
               public int OriginalHomeID {get; set;}
               public double HouseArea {get; set;}
               public double HouseAreaSquares { get; set; }
               public double AlfrescoArea { get; set; }
               public double AlfrescoAreaSquares { get; set; }
               public double GarageArea { get; set; }
               public double GarageAreaSquares { get; set; }
               public double TotalArea { get; set; }
               public double TotalAreaSquares { get; set; }
               public double MinimumBlockWidth { get; set; }
               public double HouseLength { get; set; }
               public double HouseWidth { get; set; }
               public double GarageBays { get; set; }
               public double LivingArea { get; set; }
               public string HomePlan {get; set;}
               public string HomeFacade { get; set; }
               public string ProductID {get; set;}
               public string Desc {get; set;}
               public string Edesc {get; set;}
               public string Cstctr {get; set;}
               public string Suofm {get; set;}
               public string Puofm {get; set;}
               public string Prdcat {get; set;}
               public string Taxcd {get; set;}
               public string Prdsop {get; set;}
               public string Usrdef {get; set;}
               public string Ldesc {get; set;}
               public string Specyear {get; set;}
               public string Housecode {get; set;}
               public string Housesize {get; set;}
               public string Housefacade {get; set;}
               public string Storey {get; set;}
               public string SOrder {get; set;}
               public string Mark {get; set;}
               public bool Active {get; set;}
               public bool ShowOnList {get; set;}
               public bool Draft {get; set;}
               public string UserCode {get; set;}
               public int homeid { get; set; }
        }

	}
}