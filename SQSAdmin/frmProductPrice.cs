using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

using System.Drawing.Imaging;
using System.Net;
using System.Net.Mail;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;

//using WebSupergoo.ABCpdf5;
//using WebSupergoo.ABCpdf5.Objects;
//using WebSupergoo.ABCpdf5.Atoms;
using System.Xml;
using System.Diagnostics;
using SQSAdmin.Common;

namespace SQSAdmin
{
	public partial class frmProductPrice : Form, IInputForm
	{
		frmProductLookup productLookupForm;
		frmPrice priceForm;
		InputFormMode mode;
		frmPag PAGForm;
        string dolpath = "";
        string emailserver = "";
        string recipient = "";
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;

		public frmProductPrice()
		{
            backgroundWorker1 = new BackgroundWorker();
            backgroundWorker2 = new BackgroundWorker();
			InitializeComponent();
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            InitializeBackgroundWorker();
		}
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);

            backgroundWorker2.DoWork += new DoWorkEventHandler(backgroundWorker2_DoWork);
            backgroundWorker2.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker2_RunWorkerCompleted);
        }
        private void frmProductPrice_Load(object sender, EventArgs e)
        {
            
            ClearForm();
            if (productLookupForm == null)
                productLookupForm = new frmProductLookup();
            productLookupForm.ShowDialog();

            loadStateDropDown();
            loadUOM();
            loadCategory();
            loadProductCode();
            loadCostCentre();
            loadPriceDisplayCode();
            loadArea();
            loadGroup();

            if (productLookupForm.SelectedRow != null)
            {
                LoadProductDetails();
            }


            ViewRead();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

		private void btnLookupProduct_Click(object sender, EventArgs e)
		{

			if (productLookupForm == null)
				productLookupForm = new frmProductLookup();
			productLookupForm.ShowDialog();

			if (productLookupForm.SelectedRow != null)
			{
				LoadProductDetails();
			}

			ViewRead();


		}

		private void LoadProductDetails()
		{
			try
			{
				DataRow row = productLookupForm.SelectedRow;
				txtProductID.Text = row["ProductID"].ToString();
				txtProductName.Text = row["ProductName"].ToString();
				txtProductDescription.Text = row["ProductDescription"].ToString();
                txtInternalDesc.Text = row["InternalDescription"].ToString();
                txtAdditonalInfo.Text = row["additionalinfo"].ToString();
				dropUOM.Text = row["UOM"].ToString();
				txtSortOrder.Text = row["SortOrder"].ToString();
				chkActive.Checked = bool.Parse(row["Active"].ToString());
                chkBCActive.Checked = bool.Parse(row["BCActive"].ToString());
                chkminiStart.Checked = bool.Parse(row["minibillStart"].ToString());
                chkminiComplete.Checked = bool.Parse(row["minibillComplete"].ToString());
                chkStandardInclusion.Checked = bool.Parse(row["standardinclusion"].ToString());
                if(row["operationsonly"].ToString()=="1" || row["operationsonly"].ToString().ToUpper() == "TRUE")
                {
                    chkOperationsOnly.Checked = true;
                }
                else
                {
                    chkOperationsOnly.Checked = false;
                }

                if (row["salesandoperations"].ToString() == "1" || row["salesandoperations"].ToString().ToUpper() == "TRUE")
                {
                    chkSalesOperations.Checked = true;
                }
                else
                {
                    chkSalesOperations.Checked = false;
                }

                if (row["packageflyerpromo"] != null && row["packageflyerpromo"].ToString() == "1")
                {
                    chkFlyerPromo.Checked = true;
                }
                else
                {
                    chkFlyerPromo.Checked = false;
                }

                if (row["isStudioMProduct"] != null && row["isStudioMProduct"].ToString().ToUpper() == "TRUE")
                {
                     chkStudioM.Checked = true;
                }
                else
                {
                    chkStudioM.Checked = false;
                }

                if (row["fkProductCategoryID"].ToString() != "")
                {
                    dropProductCategory.SelectedValue = row["fkProductCategoryID"];
                }
                if (row["fkProductCodeID"].ToString() != "")
                {
                    dropProductCode.SelectedValue = row["fkProductCodeID"];
                }
                if (row["fkCostCentreCodeID"].ToString() != "")
                {
                    dropCostCentre.SelectedValue = row["fkCostCentreCodeID"];
                }
                if (row["fkPriceDisplayCodeID"].ToString() != "")
                {
                    dropPriceDisplayCode.SelectedValue = row["fkPriceDisplayCodeID"];
                }
                if (row["uom"].ToString() != "")
                {
                    dropUOM.SelectedValue = row["uom"];
                }

                dropdownState.SelectedValue = row["fkStateID"];

                if (row["defaultQty"].ToString() != "")
                {
                   txtDefaultQty.Text = row["defaultQty"].ToString();
                }
                if (row["defaultareaid"].ToString() != "")
                {
                    dropArea.SelectedValue = row["defaultareaid"];
                }
                if (row["defaultgroupid"].ToString() != "")
                {
                    dropGroup.SelectedValue = row["defaultgroupid"];
                }
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message);
			}

		}

		private void button1_Click(object sender, EventArgs e)
		{

			if (priceForm == null)
				priceForm = new frmPrice();

			if (!string.IsNullOrEmpty(txtProductID.Text))
			{
				priceForm.ProductID = txtProductID.Text;
				priceForm.SelectedProductName = txtProductName.Text;
                priceForm.StateID = dropdownState.SelectedValue.ToString();
				priceForm.ShowDialog();
			}
			
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}


        private void loadUOM()
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("select UOMID,code,Code+' - '+Description as [desc] from tblUOM where active=1 order by [code]");
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                dropUOM.DataSource = dsTemp.Tables[0];
                dropUOM.ValueMember = "Code";
                dropUOM.DisplayMember = "desc";
                dropUOM.SelectedValue = "..";
            }
        }

        private void loadCategory()
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetProductCategory]", new System.Data.SqlClient.SqlParameter[3]
                                {
                                      new System.Data.SqlClient.SqlParameter("@code", "")
                                    , new System.Data.SqlClient.SqlParameter("@desc", "")
                                    , new System.Data.SqlClient.SqlParameter("@active",1)

                                 });
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                dropProductCategory.DataSource = dsTemp.Tables[0];
                dropProductCategory.ValueMember = "ProductCategoryID";
                dropProductCategory.DisplayMember = "dropdown";
                dropProductCategory.SelectedValue = 3;
            }
        }

        private void loadStateDropDown()
        {
            DataSet dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });
            dropdownState.DataSource = dsState.Tables[0];
            dropdownState.DisplayMember = "stateAbbreviation";
            dropdownState.ValueMember = "stateID";
            dropdownState.SelectedValue=MetriconCommon.State;
        }

        private void loadProductCode()
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetProductCode]", new System.Data.SqlClient.SqlParameter[3]
                                {
                                      new System.Data.SqlClient.SqlParameter("@code", "")
                                    , new System.Data.SqlClient.SqlParameter("@desc", "")
                                    , new System.Data.SqlClient.SqlParameter("@active",1)

                                 });
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                dropProductCode.DataSource = dsTemp.Tables[0];
                dropProductCode.ValueMember = "ProductCodeID";
                dropProductCode.DisplayMember = "dropdown";
                dropProductCode.SelectedValue = 52;
            }
        }
        private void loadCostCentre()
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetProductCostCentreCode]", new System.Data.SqlClient.SqlParameter[3]
                                {
                                      new System.Data.SqlClient.SqlParameter("@code", "")
                                    , new System.Data.SqlClient.SqlParameter("@desc", "")
                                    , new System.Data.SqlClient.SqlParameter("@active",1)

                                 });
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                dropCostCentre.DataSource = dsTemp.Tables[0];
                dropCostCentre.ValueMember = "CostCentreCodeID";
                dropCostCentre.DisplayMember = "dropdown";
                dropCostCentre.SelectedValue = 289;
            }
        }
        private void loadPriceDisplayCode()
        {
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetPriceDisplayCode]", new System.Data.SqlClient.SqlParameter[4]
                                {
                                      new System.Data.SqlClient.SqlParameter("@code", "")
                                    , new System.Data.SqlClient.SqlParameter("@desc", "")
                                    , new System.Data.SqlClient.SqlParameter("@active",1)
                                    , new System.Data.SqlClient.SqlParameter("@forpromotion","0")
                                 });
            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                dropPriceDisplayCode.DataSource = dsTemp.Tables[0];
                dropPriceDisplayCode.ValueMember = "PriceDisplayCodeID";
                dropPriceDisplayCode.DisplayMember = "dropdown";
                dropPriceDisplayCode.SelectedValue = 10;
            }
        }

        private void loadArea()
        {
            DataSet dsAllAreas = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAllAreas]", new System.Data.SqlClient.SqlParameter[0] { });
            dropArea.ValueMember = "areaid";
            dropArea.DisplayMember = "areaname";
            dropArea.DataSource = dsAllAreas.Tables[0];
            dropArea.SelectedIndex = -1;
        }

        private void loadGroup()
        {
            DataSet dsAllGroup = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetAllGroups]", new System.Data.SqlClient.SqlParameter[0] { });
            dropGroup.ValueMember = "groupid";
            dropGroup.DisplayMember = "groupname";
            dropGroup.DataSource = dsAllGroup.Tables[0];
            dropGroup.SelectedIndex = -1;
        }

		public void ViewNew()
		{
			mode = InputFormMode.New;
			ClearForm();
			txtProductID.ReadOnly = false;
			txtProductName.ReadOnly = false;
			txtProductDescription.ReadOnly = false;
            txtInternalDesc.ReadOnly = false;
            txtAdditonalInfo.ReadOnly = false;
			dropUOM.Enabled = true;
			txtSortOrder.ReadOnly = false;
			chkActive.Enabled = true;
            chkBCActive.Enabled = true;
            chkminiStart.Enabled = true;
            chkminiComplete.Enabled = true;
            chkFlyerPromo.Enabled = true;
            chkStudioM.Enabled = true;
            chkStandardInclusion.Enabled = true;
            chkOperationsOnly.Enabled = true;
            chkSalesOperations.Enabled = true;

            dropCostCentre.Enabled = true;
            dropPriceDisplayCode.Enabled = true;
            dropProductCategory.Enabled = true;
            dropProductCode.Enabled = true;

            dropCostCentre.SelectedValue = 289;//000
            dropPriceDisplayCode.SelectedValue = 10;//NONE
            dropProductCategory.SelectedValue = 3;//HA
            dropProductCode.SelectedValue = 52;//V
            dropUOM.SelectedValue = "EA";
            dropdownState.Enabled = true;
            dropArea.Enabled = true;
            dropGroup.Enabled = true;
            txtDefaultQty.Enabled = true;

			btnNew.Enabled = false;
			btnEdit.Enabled = false;
			btnCancelData.Enabled = true;
			btnSaveData.Enabled = true;
            btnCopy.Enabled = false;
            btnPAG.Enabled = false;
            btnPrices.Enabled = false;
            btnStudioM.Enabled = false;
            btnSI.Enabled = false;
            btnQRCode.Enabled = false;
            

		}

		public void ViewEdit()
		{

			mode = InputFormMode.Edit;
			txtProductID.ReadOnly = true;
			txtProductName.ReadOnly = false;
			txtProductDescription.ReadOnly = false;
            txtInternalDesc.ReadOnly = false;
            txtAdditonalInfo.ReadOnly = false;
			dropUOM.Enabled = true;
			txtSortOrder.ReadOnly = false;
			chkActive.Enabled = true;
            chkBCActive.Enabled = true;
            chkminiStart.Enabled = true;
            chkminiComplete.Enabled = true;
            chkFlyerPromo.Enabled = true;
            chkStudioM.Enabled = true;
            chkStandardInclusion.Enabled = true;
            chkOperationsOnly.Enabled = true;
            chkSalesOperations.Enabled = true;

            dropCostCentre.Enabled = true;
            dropPriceDisplayCode.Enabled = true;
            dropProductCategory.Enabled = true;
            dropProductCode.Enabled = true;

			btnNew.Enabled = false;
			btnEdit.Enabled = false;
			btnCancelData.Enabled = true;
			btnSaveData.Enabled = true;
            btnCopy.Enabled = true;
            btnPAG.Enabled = true;
            btnPrices.Enabled = true;
            dropdownState.Enabled = false;

            dropArea.Enabled = true;
            dropGroup.Enabled = true;
            txtDefaultQty.Enabled = true;
            btnQRCode.Enabled = true;
            //if (chkStudioM.Checked)
            //{
                btnStudioM.Enabled = true;
            //}
            //else
            //{
            //    btnStudioM.Enabled = false;
            //}
            if (chkStandardInclusion.Checked)
            {
                btnSI.Enabled = true;
            }
            else
            {
                btnSI.Enabled = false;
            }
		}


		public void ViewRead()
		{
			mode = InputFormMode.Read;
			txtProductID.ReadOnly = true;
			txtProductName.ReadOnly = true;
			txtProductDescription.ReadOnly = true;
            txtInternalDesc.ReadOnly = true;
            txtAdditonalInfo.ReadOnly = true;
			dropUOM.Enabled = false;
			txtSortOrder.ReadOnly = true;
			chkActive.Enabled = false;
            chkBCActive.Enabled = false;
            chkminiStart.Enabled = false;
            chkminiComplete.Enabled = false;
            chkFlyerPromo.Enabled = false;
            chkStudioM.Enabled = false;
            chkStandardInclusion.Enabled = false;
            chkOperationsOnly.Enabled = false;
            chkSalesOperations.Enabled = false;

            dropCostCentre.Enabled = false;
            dropPriceDisplayCode.Enabled = false;
            dropProductCategory.Enabled = false;
            dropProductCode.Enabled = false;

			btnNew.Enabled = true;
			btnEdit.Enabled = true;
			btnCancelData.Enabled = false;
			btnSaveData.Enabled = false;

            btnCopy.Enabled = false;
            btnPAG.Enabled = false;
            btnPrices.Enabled = false;
            btnQRCode.Enabled = true;
            dropdownState.Enabled = false;

            dropArea.Enabled = false;
            dropGroup.Enabled = false;
            txtDefaultQty.Enabled = false;
            btnStudioM.Enabled = false;
            btnSI.Enabled = false;
		}


		public void SaveForm()
		{
            int areaid, groupid;
            int active, bcactive, minibillstart, minibillend, flyerpromo, studiomproduct, standardinclusion,  salesoperation;
            double qty;
            if (chkActive.Checked)
            {
                active = 1;
            }
            else
            {
                active = 0;
            }

            if (chkBCActive.Checked)
            {
                bcactive = 1;
            }
            else
            {
                bcactive = 0;
            }
            if (chkminiStart.Checked)
            {
                minibillstart = 1;
            }
            else
            {
                minibillstart = 0;
            }
            if (chkminiComplete.Checked)
            {
                minibillend = 1;
            }
            else
            {
                minibillend = 0;
            }
            if (chkFlyerPromo.Checked)
            {
                flyerpromo = 1;
            }
            else
            {
                flyerpromo = 0;
            }
            if (chkStudioM.Checked)
            {
                studiomproduct = 1;
            }
            else
            {
                studiomproduct = 0;
            }
            if (chkStandardInclusion.Checked)
            {
                standardinclusion = 1;
            }
            else
            {
                standardinclusion = 0;
            }
            if (chkSalesOperations.Checked)
            {
                salesoperation = 3;
            }
            else
            {
                salesoperation = 0;
            }

            if (chkOperationsOnly.Checked)
            {
                salesoperation = 1;
            }


            try
            {
                if (txtDefaultQty.Text != "") qty = Double.Parse(txtDefaultQty.Text); else qty = 0.0;
                if (dropArea.SelectedIndex == -1) areaid = 0; else areaid = Int32.Parse(dropArea.SelectedValue.ToString());
                if (dropGroup.SelectedIndex == -1) groupid = 0; else groupid = Int32.Parse(dropGroup.SelectedValue.ToString());

                //if (!chkminiStart.Checked && chkminiComplete.Checked)
                //{
                //    MessageBox.Show("Mini bill can NOT be compelted without started.");
                //}
                //else
                //{
                    if (mode == InputFormMode.New)
                    {
                        DataSet prod = MetriconCommon.DatabaseManager.ExecuteSQLQuery("SELECT * from [Product] where productID='" + txtProductID.Text.Trim() + "'");

                        if (prod.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show("This productID already exists! Please enter a new one.");
                        }
                        else
                        {
                            if (txtProductID.Text.ToString() != "")
                            {
                                System.Data.SqlClient.SqlParameter[] myParams = new System.Data.SqlClient.SqlParameter[24];
                                myParams[0] = new System.Data.SqlClient.SqlParameter("@ProductID", txtProductID.Text);
                                myParams[1] = new System.Data.SqlClient.SqlParameter("@ProductName", txtProductName.Text);
                                myParams[2] = new System.Data.SqlClient.SqlParameter("@ProductDescription", txtProductDescription.Text);
                                myParams[3] = new System.Data.SqlClient.SqlParameter("@UOM", dropUOM.SelectedValue);
                                myParams[4] = new System.Data.SqlClient.SqlParameter("@SortOrder", txtSortOrder.Text);
                                myParams[5] = new System.Data.SqlClient.SqlParameter("@CreatedBy", MetriconCommon.UserCode);
                                myParams[6] = new System.Data.SqlClient.SqlParameter("@Active", active);
                                myParams[7] = new System.Data.SqlClient.SqlParameter("@BCActive", bcactive);
                                myParams[8] = new System.Data.SqlClient.SqlParameter("@categoryID", Int32.Parse(dropProductCategory.SelectedValue.ToString()));
                                myParams[9] = new System.Data.SqlClient.SqlParameter("@productcodeID", Int32.Parse(dropProductCode.SelectedValue.ToString()));
                                myParams[10] = new System.Data.SqlClient.SqlParameter("@costcentreID", Int32.Parse(dropCostCentre.SelectedValue.ToString()));
                                myParams[11] = new System.Data.SqlClient.SqlParameter("@pricedisplaycodeID", Int32.Parse(dropPriceDisplayCode.SelectedValue.ToString()));
                                myParams[12] = new System.Data.SqlClient.SqlParameter("@minibillstart", minibillstart);
                                myParams[13] = new System.Data.SqlClient.SqlParameter("@minibillcomplete", minibillend);
                                myParams[14] = new System.Data.SqlClient.SqlParameter("@stateID", dropdownState.SelectedValue.ToString());
                                myParams[15] = new System.Data.SqlClient.SqlParameter("@areaID", areaid);
                                myParams[16] = new System.Data.SqlClient.SqlParameter("@groupID", groupid);
                                myParams[17] = new System.Data.SqlClient.SqlParameter("@defaultQty", qty);
                                myParams[18] = new System.Data.SqlClient.SqlParameter("@flyerpromo", flyerpromo);
                                myParams[19] = new System.Data.SqlClient.SqlParameter("@isStudioMProduct", studiomproduct);
                                myParams[20] = new System.Data.SqlClient.SqlParameter("@standardinclusion", standardinclusion);
                                myParams[21] = new System.Data.SqlClient.SqlParameter("@internaldesc", txtInternalDesc.Text);
                                myParams[22] = new System.Data.SqlClient.SqlParameter("@additionalinfo", txtAdditonalInfo.Text);
                                myParams[23] = new System.Data.SqlClient.SqlParameter("@salesoperations", salesoperation);

                            MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminNewProduct", myParams);
                            }
                            else
                            {
                                MessageBox.Show("Please enter a valid product ID!");
                            }
                            ViewRead();
                        }
                        //CreateProductQRCode(txtProductID.Text, txtProductName.Text, txtProductDescription.Text, txtInternalDesc.Text, txtAdditonalInfo.Text, MetriconCommon.UserName, "NEW");
                    }


                    if (mode == InputFormMode.Edit)
                    {
                        System.Data.SqlClient.SqlParameter[] myParams = new System.Data.SqlClient.SqlParameter[23];
                        myParams[0] = new System.Data.SqlClient.SqlParameter("@ProductID", txtProductID.Text);
                        myParams[1] = new System.Data.SqlClient.SqlParameter("@ProductName", txtProductName.Text);
                        myParams[2] = new System.Data.SqlClient.SqlParameter("@ProductDescription", txtProductDescription.Text);
                        myParams[3] = new System.Data.SqlClient.SqlParameter("@UOM", dropUOM.SelectedValue);
                        myParams[4] = new System.Data.SqlClient.SqlParameter("@SortOrder", txtSortOrder.Text);
                        myParams[5] = new System.Data.SqlClient.SqlParameter("@ModifiedBy", MetriconCommon.UserCode);
                        myParams[6] = new System.Data.SqlClient.SqlParameter("@Active", active);
                        myParams[7] = new System.Data.SqlClient.SqlParameter("@BCActive", bcactive);
                        myParams[8] = new System.Data.SqlClient.SqlParameter("@categoryID", Int32.Parse(dropProductCategory.SelectedValue.ToString()));
                        myParams[9] = new System.Data.SqlClient.SqlParameter("@productcodeID", Int32.Parse(dropProductCode.SelectedValue.ToString()));
                        myParams[10] = new System.Data.SqlClient.SqlParameter("@costcentreID", Int32.Parse(dropCostCentre.SelectedValue.ToString()));
                        myParams[11] = new System.Data.SqlClient.SqlParameter("@pricedisplaycodeID", Int32.Parse(dropPriceDisplayCode.SelectedValue.ToString()));
                        myParams[12] = new System.Data.SqlClient.SqlParameter("@minibillstart", minibillstart);
                        myParams[13] = new System.Data.SqlClient.SqlParameter("@minibillcomplete", minibillend);

                        myParams[14] = new System.Data.SqlClient.SqlParameter("@areaID", areaid);
                        myParams[15] = new System.Data.SqlClient.SqlParameter("@groupID", groupid);
                        myParams[16] = new System.Data.SqlClient.SqlParameter("@defaultQty", qty);
                        myParams[17] = new System.Data.SqlClient.SqlParameter("@flyerpromo", flyerpromo);
                        myParams[18] = new System.Data.SqlClient.SqlParameter("@isStudioMProduct", studiomproduct);
                        myParams[19] = new System.Data.SqlClient.SqlParameter("@standardinclusion", standardinclusion);
                        myParams[20] = new System.Data.SqlClient.SqlParameter("@internaldesc", txtInternalDesc.Text);
                        myParams[21] = new System.Data.SqlClient.SqlParameter("@additionalinfo", txtAdditonalInfo.Text);
                        myParams[22] = new System.Data.SqlClient.SqlParameter("@salesoperations", salesoperation);
                    MetriconCommon.DatabaseManager.ExecuteSQLQuery("AdminEditProduct", myParams);
                        ViewRead();
                   //}
                }

                    
            }
            catch (FormatException fex)
            {
                MessageBox.Show("Please enter a number in default qty!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
  		}

        private void CreateProductQRCode(string pproductid, string productname,string productdescription, string internaldesc, string additionalnotes, string createdby, string callfrom)
        {
            //if (ProductNeedCreateQRCode(pproductid))
            //{
                //QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                //String encoding = "Byte";
                //if (encoding == "Byte")
                //{
                //    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                //}
                //else if (encoding == "AlphaNumeric")
                //{
                //    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                //}
                //else if (encoding == "Numeric")
                //{
                //    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                //}
                //try
                //{
                //    int scale = Convert.ToInt16("15");
                //    qrCodeEncoder.QRCodeScale = scale;
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Invalid size!");
                //    return;
                //}
                //try
                //{
                //    int version = Convert.ToInt16("7");
                //    qrCodeEncoder.QRCodeVersion = version;
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Invalid version !");
                //}

                //string errorCorrect = "M";
                //if (errorCorrect == "L")
                //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                //else if (errorCorrect == "M")
                //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                //else if (errorCorrect == "Q")
                //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                //else if (errorCorrect == "H")
                //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

                //Image image;
                //image = qrCodeEncoder.Encode(pproductid);

                string savepath="";
                string displaypath = "";
                XmlDocument doc2 = new XmlDocument();
                doc2.Load(@"http://sqsadmin/sqsadminconfig.xml");
                XmlNodeList nodeList2 = doc2.SelectNodes("connectionStrings/QRImageSavePath");
                foreach (XmlNode node in nodeList2)
                {
                    savepath = @"" + node.SelectSingleNode("path").InnerText;
                }

                //savepath = savepath + @"\" + pproductid + "_QRCode.jpg";
                //image.Save(savepath, System.Drawing.Imaging.ImageFormat.Jpeg);


                doc2.Load(@"http://sqsadmin/sqsadminconfig.xml");
                XmlNodeList nodeList3 = doc2.SelectNodes("connectionStrings/QRImageDisplayPath");
                foreach (XmlNode node in nodeList3)
                {
                    displaypath = @"" + node.SelectSingleNode("path").InnerText;
                }

                displaypath = displaypath + @"/" + pproductid + "_QRCode.jpg";

                //MemoryStream ms = new MemoryStream();
                //image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                string desc = productdescription;

                string urlstring = "";
                XmlDocument doc = new XmlDocument();
                doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
                XmlNodeList nodeList = doc.SelectNodes("connectionStrings/conString");
                foreach (XmlNode node in nodeList)
                {
                    if (node.SelectSingleNode("server").InnerText == MetriconCommon.Environment)
                    {
                        urlstring = node.SelectSingleNode("QRCodeProntLink").InnerText;
                    }
                }

                urlstring = urlstring + @"?productid=" + pproductid;
                ProcessStartInfo sInfo = new ProcessStartInfo(urlstring);
                Process.Start(sInfo);

                //if (internaldesc != null && internaldesc != "")
                //{
                //    desc = desc + "<br>" + internaldesc;
                //}
                //if (additionalnotes != null && additionalnotes != "")
                //{
                //    desc = desc + "<br>" + additionalnotes;
                //}

            /*
                CallParameter cp= new CallParameter();
                cp.productid=pproductid;
                cp.imagestream=ms;
                cp.productdescription = desc;
                cp.additionalnotes = additionalnotes;
                cp.internaldescription = internaldesc;
                cp.productname = productname;
                cp.createdby = createdby;
                cp.createdon = DateTime.Now.ToString("dd/MM/yyyy");
                if (callfrom.ToUpper() == "NEW")
                {
                    backgroundWorker1.RunWorkerAsync(cp);
                }
                else
                {
                    backgroundWorker2.RunWorkerAsync(cp);

                }
             * */
                //SendQRCodeNotification(ms, pproductid);
                /*
                            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_SaveProductQRCodeImage]", new System.Data.SqlClient.SqlParameter[3]
                                            {
                                                  new System.Data.SqlClient.SqlParameter("@productid", pproductid),
                                                  new System.Data.SqlClient.SqlParameter("@image", ms.ToArray()),
                                                  new System.Data.SqlClient.SqlParameter("@usercode", MetriconCommon.UserCode)
                                            });
                            */

            //}
        }
        private void SendQRCodeNotification(MemoryStream m, string productid)
        {
            string html = "";
            string emailBody = "";
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_GetQRCodeNotificationRecipient]", new System.Data.SqlClient.SqlParameter[1]
                                {
                                      new System.Data.SqlClient.SqlParameter("@stateid",MetriconCommon.State)
                                });

            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                recipient = dsTemp.Tables[0].Rows[0]["recipient"].ToString();
            }

            if (recipient != "")
            {

                html = "<html><body style='font-family:arial;verdana'><table><tr><td style='text-align:center'><img alt='' src='data:image/jpeg;base64," + Convert.ToBase64String(m.ToArray()) + "' width='119px' height='119px'/><td/></tr>";
                html = html + "<tr><td>&nbsp;</td></tr><tr><td style='text-align:center'>" + productid + "</td></tr></table></body></html>";

                // get relavant info from config file
                XmlDocument doc = new XmlDocument();
                doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
                XmlNodeList nodeList2 = doc.SelectNodes("connectionStrings/DOLPDFPath");
                foreach (XmlNode node in nodeList2)
                {
                    dolpath = @"" + node.SelectSingleNode("path").InnerText;
                }

                XmlNodeList nodeList3 = doc.SelectNodes("connectionStrings/EmailServer");
                foreach (XmlNode node in nodeList3)
                {
                    emailserver = @"" + node.SelectSingleNode("value").InnerText;
                }

                //Doc theDoc = SetupPDF();
                //int theID = theDoc.AddImageHtml(html);
                //string docname = SavePDF(theDoc, productid);
                try
                {
                    //MailMessage message = new MailMessage("noreply@metricon.com.au", recipient, "New product has been created.", emailBody);
                    //message.IsBodyHtml = true;
                    //if (docname != null && docname != "")
                    //{
                    //    message.Attachments.Add(new Attachment(docname));
                    //}
                    //SmtpClient emailClient = new SmtpClient(emailserver);
                    //emailClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    //emailClient.Send(message);
                }
                catch (Exception ex)
                {

                }
            }
        }
        private bool ProductNeedCreateQRCode(string pproductid)
        {
            bool result = false;
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminCheckProductNeedCreateQRCode]", new System.Data.SqlClient.SqlParameter[1]
                                {
                                      new System.Data.SqlClient.SqlParameter("@productid", pproductid)
                                });
            if (dsTemp.Tables[0].Rows[0]["result"].ToString() == "1" || dsTemp.Tables[0].Rows[0]["result"].ToString().ToUpper() == "TRUE")
            {
                result = true;
            }

            return result;
        }

		public void ClearForm()
		{
			txtProductID.Text = "";
			txtProductName.Text = "";
			txtProductDescription.Text = "";
			dropUOM.Text = "";
			txtSortOrder.Text = "";
			chkActive.Checked = true;
            chkBCActive.Checked = true;
            chkminiStart.Checked = false;
            chkminiComplete.Checked = false;
            chkFlyerPromo.Checked = false;
            chkStudioM.Checked = false;
            dropArea.SelectedIndex = -1;
            dropGroup.SelectedIndex = -1;
            txtDefaultQty.Text = "";
            chkStandardInclusion.Checked = false;
            txtInternalDesc.Text = "";
            txtAdditonalInfo.Text = "";
               //dropProductCode.SelectedIndex = 1;
               //dropProductCategory.SelectedIndex = 1;
               //dropPriceDisplayCode.SelectedIndex = 1;
               //dropCostCentre.SelectedIndex = 1;
		}


		private void btnNew_Click(object sender, EventArgs e)
		{
			ViewNew();
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			ViewEdit();
		}

		private void btnSaveData_Click(object sender, EventArgs e)
		{

            if (formValidated()=="0")
            {
                SaveForm();

                btnCopy.Enabled = true;
                btnPAG.Enabled = true;
                btnPrices.Enabled = true;
            }
            else if (formValidated() == "1")
            {
                MessageBox.Show("The product code does NOT match the state selected.\nPlease ensure approporate code is entered and correct state is selected!");
            }
            else if (formValidated() == "2")
            {
                MessageBox.Show("The productID is incorrect!");
            }
		}
        private string formValidated()
        {
            string result="0";
            string pID = txtProductID.Text;
            string stateID = dropdownState.SelectedValue.ToString();
            if ((pID.Length > 0 && pID.Substring(0, 1) == "N" && stateID == "1") || (pID.Length > 0 && pID.Substring(0, 1) != "N" && stateID == "2"))
            {
                result = "1";
            }
            else if (pID.Length >= 4 && pID.Substring(pID.Length-4,4)=="-NEW")  // this means -NEW is at the end of the productid
            {
                result = "2";
            }
            return result;

        }
		private void btnCancelData_Click(object sender, EventArgs e)
		{
			ViewRead();
			btnCopy.Enabled = true;
			btnPAG.Enabled = true;
			btnPrices.Enabled = true;
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			ViewEdit();
			mode = InputFormMode.New;
			txtProductID.ReadOnly = false;
			txtProductID.Text = txtProductID.Text + "-NEW";
			btnCopy.Enabled = false;
			btnPAG.Enabled = false;
			btnPrices.Enabled = false;
            dropdownState.Enabled = true;
		}

		private void btnPAG_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(txtProductID.Text))
			{
				if (PAGForm == null)
				{

					PAGForm = new frmPag();

				}

				PAGForm.ProductID = txtProductID.Text;
				PAGForm.SelectedProductName = txtProductName.Text;

				PAGForm.ShowDialog();
			}

		}

        private void btnStudioM_Click(object sender, EventArgs e)
        {
            //DataRow row = productLookupForm.SelectedRow;
            //var win = new SQSAdmin_WpfCustomControlLibrary.frmStudioMAttributes(row["productid"].ToString(), int.Parse(dropdownState.SelectedValue.ToString()));
            //ElementHost.EnableModelessKeyboardInterop(win);
            //win.Show();

            DataRow row = productLookupForm.SelectedRow;
            var win = new SQSAdmin_WpfCustomControlLibrary.frmStudioM(row["productid"].ToString(), row["productname"].ToString(), int.Parse(dropdownState.SelectedValue.ToString()), chkStudioM.Checked);
            ElementHost.EnableModelessKeyboardInterop(win);
            win.Show();
        }

        private void btnSI_Click(object sender, EventArgs e)
        {
            DataRow row = productLookupForm.SelectedRow;
            SQSAdmin_WpfCustomControlLibrary.frmConfigureSIToBrand win = new SQSAdmin_WpfCustomControlLibrary.frmConfigureSIToBrand(row["productid"].ToString(), int.Parse(MetriconCommon.State));
            win.ShowDialog();           
        }

/*
        public Doc SetupPDF()
        {
            // Set MediaBox size, and content rectangle
            // A4: w595 h842 
            Doc theDoc = new Doc();
            theDoc.MediaBox.SetRect(0, 0, 595, 842);
            theDoc.Rect.String = "30 35 565 812";
            //theDoc.Rect.Position(0, 0);
            return theDoc;
        }

        public string SavePDF(Doc theDoc, string productid)
        {
            Random R = new Random();
 
            string pdfFilename = dolpath + productid + "_QRCode" + ".pdf";
            theDoc.Save(pdfFilename);
            theDoc.Clear();

            return pdfFilename;

        }
*/
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string html = "";
            string emailBody = "";
            CallParameter cp = (CallParameter)e.Argument;
            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_GetQRCodeNotificationRecipient]", new System.Data.SqlClient.SqlParameter[1]
                                {
                                      new System.Data.SqlClient.SqlParameter("@stateid",MetriconCommon.State)
                                });

            if (dsTemp.Tables[0].Rows.Count > 0)
            {
                recipient = dsTemp.Tables[0].Rows[0]["recipient"].ToString();
            }

            if (recipient != "")
            {
                emailBody = @"<html><head>
                                        <style type='text/css'>
                                            html, body {
                                                margin: 0 auto;
                                                padding: 0;
                                            }
                                        </style>
                                    </head>
                              <body>
                              <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                  <tr>
                                    <td  valign='top'>
                                        <table width='707' border='0' align='center' cellpadding='0' cellspacing='0' style='border-left: 1px solid #2B3443; border-right: 1px solid #2B3443;'>
                                            <tr>
                                                <td width='15' bgcolor='#2B3443'>
                                                    <div></div>
                                                </td>
                                                <td width='15' bgcolor='#2B3443'>
                                                    <div></div>
                                                </td>
                                                <td width='331' height='68' bgcolor='#2B3443'>
                                                    <div>
                                                     </div>
                                                </td>
                                                <td width='331' bgcolor='#2B3443'>
                                                    <div style='margin: 0px 5px; padding: 0px; clear: both; display: block; font-family: arial; font-size: 20px; text-align: right'><span style='color: #fff;'>Retail System Management Notification</span></div>
                                                </td>
                                                <td width='15' bgcolor='#2B3443'>
                                                    <div></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan='5' bgcolor='#FFFFFF' height='30'>
                                                    <div></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width='15'>
                                                    <div></div>
                                                </td>
                                                <td colspan='3'>
                                                    <div style='margin: 5px 0px 5px 0; padding: 0px; clear: both; display: block; color: #2B3443; font-family: Arial; font-size: 26px;'><span style='color: #2B3443;'>New Product QR Code</span></div>
                                                </td>
                                                <td width='5'>
                                                    <div></div>
                                                </td>
                                            </tr>";

                emailBody = emailBody + @" <tr>
                        <td width='15'>
                            <div></div>
                        </td>
                        <td colspan='3'>
                             <table width='100%' border='0' cellspacing='0' cellpadding='0'>
                                <tr>
                                   <td colspan='2'>
                                        <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                           <span style='text-decoration: underline;'>Details</span>
                                        </div>
                                   </td>
                                </tr>
                                ";

                emailBody = emailBody + @"<tr>
                                           <td colspan='2'>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span>Please find QR Code attached of product ["+cp.productid+@"]</span>
                                                </div>
                                           </td>
                                        </tr>
                                        <tr><td>&nbsp;</td></tr>";

                emailBody = emailBody + @"<tr>
                                           <td colspan='2'>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span>QR Code of Size 203 * 203 – Ideal for displaying next to product on wall.<br>
                                                         QR Code of Size 119 * 119 – Ideal for products, not displayed on wall but available in product books.</span>
                                                </div>
                                           </td>
                                        </tr>
                                        <tr><td>&nbsp;</td></tr>";
                emailBody = emailBody + @"<tr>
                                           <td colspan='2'>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span style='font-weight:bold;'>Product Information</span>
                                                </div>
                                           </td>
                                        </tr>
                                        ";

                emailBody = emailBody + @"<tr>
                                           <td width='15%'>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >Code:</span>
                                                </div>
                                           </td>
                                           <td width='85%'>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >"+cp.productid+@"</span>
                                                </div>
                                           </td>
                                        </tr>
                                        ";
                emailBody = emailBody + @"<tr>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >Name:</span>
                                                </div>
                                           </td>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >"+cp.productname+@"</span>
                                                </div>
                                           </td>
                                        </tr>
                                        ";
                 emailBody = emailBody + @"<tr>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >Description:</span>
                                                </div>
                                           </td>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >"+cp.productdescription+@"</span>
                                                </div>
                                           </td>
                                        </tr>
                                        ";
                 emailBody = emailBody + @"<tr>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >Internal Description:</span>
                                                </div>
                                           </td>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >" + cp.internaldescription + @"</span>
                                                </div>
                                           </td>
                                        </tr>
                                        ";
                 emailBody = emailBody + @"<tr>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >Additional Notes:</span>
                                                </div>
                                           </td>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >" + cp.additionalnotes + @"</span>
                                                </div>
                                           </td>
                                        </tr>
                                        ";   
                 emailBody = emailBody + @"<tr>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >Created On:</span>
                                                </div>
                                           </td>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >"+cp.createdon+@"</span>
                                                </div>
                                           </td>
                                        </tr>
                                       ";               
                 emailBody = emailBody + @"<tr>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >Created By:</span>
                                                </div>
                                           </td>
                                           <td>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span >"+cp.createdby+@"</span>
                                                </div>
                                           </td>
                                        </tr>
                                        ";  
                 emailBody = emailBody + @"<tr>
                                           <td colspan='2'>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span style='text-decoration: underline;'>Action</span>
                                                </div>
                                           </td>
                                        </tr>
                                        ";         
                emailBody = emailBody + @"<tr>
                                           <td colspan='2'>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span>Place QR code either on wall next to product displayed or next to product in product book.</span>
                                                </div>
                                           </td>
                                        </tr>
                                        <tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr>";    
                emailBody = emailBody + @"<tr>
                                           <td colspan='2'>
                                                <div style='margin: 15px 0; padding: 0px; clear: both; display: block; color: #5f5f5f; font-family: Arial; font-size: 14px; line-height: 14px;'>
                                                   <span>Please do not reply back to this email as mailbox is unmonitored.</span>
                                                </div>
                                           </td>
                                        </tr>
                                        <tr><td>&nbsp;</td></tr>
                                         </table>
                                    </td>
                                    <td width='15'>
                                        <div></div>
                                    </td>
                                </tr>";

                emailBody = emailBody + @"<tr>
                                                <td colspan='5' bgcolor='#FFFFFF' height='30'>
                                                    <div></div>
                                                </td>
                                           </tr>
                                           <tr>
                                                <td width='15' bgcolor='#2B3443'>
                                                    <div></div>
                                                </td>
                                                <td width='15' bgcolor='#2B3443'>
                                                    <div></div>
                                                </td>
                                                <td bgcolor='#2B3443' colspan='2' height='35' align='center' valign='middle'>
                                                    <div style='margin: 0px; padding: 0px; color: #d0cbcb; font-family: Arial; font-size: 14px;'>© "+ DateTime.Now.Year.ToString()+@" All Rights Reserved. Metricon Homes Pty Ltd. 501 Blackburn Road, Mt. Waverley VIC 3149.</div>
                                                </td>
                                                <td width='15' bgcolor='#2B3443'>
                                                    <div></div>
                                                </td>
                                            </tr>

                  </table>
             </td>
          </tr>
     </table>
  </body>
";



                html = buildQRCodeHTML(cp);

    
                // get relavant info from config file
                XmlDocument doc = new XmlDocument();
                doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
                XmlNodeList nodeList2 = doc.SelectNodes("connectionStrings/DOLPDFPath");
                foreach (XmlNode node in nodeList2)
                {
                    dolpath = @"" + node.SelectSingleNode("path").InnerText;
                }

                XmlNodeList nodeList3 = doc.SelectNodes("connectionStrings/EmailServer");
                foreach (XmlNode node in nodeList3)
                {
                    emailserver = @"" + node.SelectSingleNode("value").InnerText;
                }

                //Doc theDoc = SetupPDF();
                //int theID = theDoc.AddImageHtml(html);
                //string docname = SavePDF(theDoc, cp.productid);
                try
                {
                    //MailMessage message = new MailMessage("noreply@metricon.com.au", recipient, "New product is available - ["+cp.productid+"]", emailBody);
                    //message.IsBodyHtml = true;
                    //if (docname != null && docname != "")
                    //{
                    //    message.Attachments.Add(new Attachment(docname));
                    //}
                    //SmtpClient emailClient = new SmtpClient(emailserver);
                    //emailClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                    //emailClient.Send(message);
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
             
        }


        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

            //CallParameter cp = (CallParameter)e.Argument;
            //string html = buildQRCodeHTML(cp);
            //Doc theDoc = SetupPDF();
            //int theID = theDoc.AddImageHtml(html);
            //e.Result = SavePDF(theDoc, cp.productid);

        }
        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string pdfname = "";
            this.pictureBox1.Visible = false;
            pdfname = e.Result.ToString();

            if (File.Exists(pdfname))
            {
                frmDOLPDF frmDOL = new frmDOLPDF(pdfname);
                frmDOL.ShowDialog();
            }
            else
            {
                MessageBox.Show("Product NOT found!");
            }
        }

        private string buildQRCodeHTML(CallParameter cp)
        {
            string html = "";
            html = "<html><body style='font-family:arial;verdana'><table width='100%'>";
            html = html + "<tr><td style='text-align:center; width:50%'><img alt='' src='data:image/jpeg;base64," + Convert.ToBase64String(cp.imagestream.ToArray()) + "' width='119px' height='119px'/></td>";
            html = html + "<td style='text-align:center; width:50%'><img alt='' src='data:image/jpeg;base64," + Convert.ToBase64String(cp.imagestream.ToArray()) + "' width='203px' height='203px'/></td>";
            html = html + "</tr>";
            html = html + @"<tr><td>&nbsp;</td></tr>
                                <tr><td style='text-align:center;width:50%'>" + cp.productid + "</td><td style='text-align:center;width:50%'>" + cp.productid + "</td></tr>";
            html = html + @"<tr><td>&nbsp;</td></tr>
                                <tr><td style='text-align:center;width:50%'>Size: 119 * 119, Ideal for product book</td><td style='text-align:center;width:50%'>Size: 203 * 203, Ideal for display on the wall</td></tr>";

            html = html + "</table></body></html>";
            return html;
        }

        public class CallParameter
        {
            public MemoryStream imagestream { get; set; }
            public string productid { get; set; }
            public string productname { get; set; }
            public string productdescription { get; set; }
            public string internaldescription { get; set; }
            public string additionalnotes { get; set; }
            public string createdon { get; set; }
            public string createdby { get; set; }
        }

        private void btnQRCode_Click(object sender, EventArgs e)
        {
            //pictureBox1.BackColor = System.Drawing.Color.Transparent;
            //pictureBox1.Parent = txtProductDescription;
            //pictureBox1.Visible = true;
            CreateProductQRCode(txtProductID.Text, txtProductName.Text, txtProductDescription.Text, txtInternalDesc.Text, txtAdditonalInfo.Text, MetriconCommon.UserName, "PRINT");
        }

        private void chkSalesOperations_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSalesOperations.Checked)
            {
                chkOperationsOnly.Checked = false;
            }
        }

        private void chkOperationsOnly_CheckedChanged(object sender, EventArgs e)
        {
            if(chkOperationsOnly.Checked)
            {
                chkSalesOperations.Checked = false;
            }
        }
    }
}