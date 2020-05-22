using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Microsoft.Office.Interop.Excel;
using SQSAdmin.Common;

namespace SQSAdmin
{
	public partial class frmMain : Form
	{
		frmProductPrice productPriceForm;
		frmHome homeForm;
        frmHomeDisplay homeDisplayForm;
        frmDisplayHomeDisplayOption displayHomeDisplayOption;
        frmPromotion promotionForm;
        frmQuantityManagement qtymanagement;
        frmAreaGroup areagroup;
        frmConfigAreaGroupByHouseModel areagroupbyhouse;
        frmProductCategory productCategory;
        frmProductCode productCode;
        frmProductCodeDiscounts discountsMgmt;
        frmPriceDisplayCode priceDisplayCode;
        frmProductCostCentre productCostCentre;
        frmUOM uom;
        frmConfigPriceByRegionProductCategory priceconfig;
        frmConfigBenchMarkPercentage benchmark;
        frmPDF frmHelp;
        frmInclusionPackage frmInclusion;
        frmInclusionPackagePag frmInclusionPag;
        frmInclusionPromotion frmInclusionPromo;
        frmProductDisplayOption frmProductDisplayOption;
        frmCopyPAG fCopyPag;
        frmQRCode frmQR;
        frmBulkPrintQRCode frmPrintQR;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;

		public frmMain()
		{

            backgroundWorker1 = new BackgroundWorker();
			InitializeComponent();
            InitializeBackgroundWorker();


            this.Text = "Retail System Management - " + MetriconCommon.WindowTitleInfo;
            foreach (ToolStripMenuItem itm in menuStrip1.Items)
            {
                if (itm.Text == "&Product/Price" && MetriconCommon.UserCode != "FZH".ToUpper() && MetriconCommon.UserCode.ToUpper() != "VCC" && MetriconCommon.UserCode.ToUpper() != "WTK")
                {
                    itm.DropDown.Items[9].Visible = false;
                    break;
                }
            }

		}
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            //backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }
		private void frmMain_Load(object sender, EventArgs e)
		{
            //getGeneralInfo();
            //getPriceConfigException();
            getHomeTemplateException();
            //getOtherException();
            getStudioMProductWithoutPrice("FORMLOAD");
            getStudioMProductWithoutQuestion("FORMLOAD");

            // hide for now, will show it when required later
            textBoxBackColorPicker.Visible = false;
            buttonBackColorPicker.Visible = false;
            // // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			DialogResult dr = MessageBox.Show("Do you really want to exit this appplication?", "Message", MessageBoxButtons.YesNo);
			if (dr == DialogResult.No)
				e.Cancel = true;

		}

        private void button5_Click(object sender, EventArgs e)
        {
            if (promotionForm == null)
                promotionForm = new frmPromotion();

            promotionForm.ShowDialog();
        }

        private void productPriceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (productPriceForm == null)
            {
                productPriceForm = new frmProductPrice();
            }
            productPriceForm.ShowDialog();
        }

        private void productCategoryCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (productCategory == null)
            {
                productCategory = new frmProductCategory();
            }

            productCategory.ShowDialog();
        }

        private void productCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (productCode == null)
            {
                productCode = new frmProductCode();
            }

            productCode.ShowDialog();
        }

        private void priceDisplayCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (priceDisplayCode == null)
            {
                priceDisplayCode = new frmPriceDisplayCode();
            }

            priceDisplayCode.ShowDialog();
        }

        private void productCostCentreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (productCostCentre == null)
            {
                productCostCentre = new frmProductCostCentre();
            }

            productCostCentre.ShowDialog();
        }

        private void unitOfMeasureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (uom == null)
            {
                uom = new frmUOM();
            }

            uom.ShowDialog();
        }


        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (homeForm == null)
                homeForm = new frmHome();

            homeForm.ShowDialog();
        }

        private void displayHomesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (homeDisplayForm == null)
                homeDisplayForm = new frmHomeDisplay();

            homeDisplayForm.ShowDialog();
        }

        private void displayPAGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (displayHomeDisplayOption == null)
                displayHomeDisplayOption = new frmDisplayHomeDisplayOption();

            displayHomeDisplayOption.ShowDialog();
            //this.pictureBox1.Visible = true;
            //backgroundWorker1.RunWorkerAsync();
        }

        private void quantityManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (qtymanagement == null)
            //    qtymanagement = new frmQuantityManagement();

            //qtymanagement.ShowDialog();
            SQSAdmin_WpfCustomControlLibrary.frmQuantityManagement win = new SQSAdmin_WpfCustomControlLibrary.frmQuantityManagement(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        private void areaGroupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (areagroup == null)
                areagroup = new frmAreaGroup();

            areagroup.ShowDialog();
        }

        private void areasGroupsByHouseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (areagroupbyhouse == null)
            {
                areagroupbyhouse = new frmConfigAreaGroupByHouseModel();
            }

            areagroupbyhouse.ShowDialog();
        }


        private void priceConfigurationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (priceconfig == null)
            {
                priceconfig = new frmConfigPriceByRegionProductCategory();
            }

            priceconfig.ShowDialog();
        }

        private void benchmarkPercentageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (benchmark == null)
            {
                benchmark = new frmConfigBenchMarkPercentage();
            }

            benchmark.ShowDialog();
        }

        //private void getGeneralInfo()
        //{
            //try
            //{

            //    DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetGeneralInformation]", new System.Data.SqlClient.SqlParameter[0]
            //                {});

            //    if (dsTemp.Tables[0].Rows[0]["result"].ToString()!="")
            //    {
            //        label1.Text = "Price created on " + DateTime.Now.ToString("dd/MM/yyyy");
            //        txtInfo.Text = dsTemp.Tables[0].Rows[0]["result"].ToString().Replace("affected.","affected.\r\n");
            //    }
            //    else
            //    {
            //        label1.Text = "";
            //    }
            //}
            //catch (Exception ex2)
            //{
            //    MessageBox.Show(ex2.Message.ToString());
            //}
            
       // }
        //private void getPriceConfigException()
        //{
            //try
            //{

            //    DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdmingetPriceConfigException]", new System.Data.SqlClient.SqlParameter[0] { });

            //    if (dsTemp.Tables[0].Rows[0]["result"].ToString()!= "")
            //    {
            //        txtPriceConfigException.Text = dsTemp.Tables[0].Rows[0]["result"].ToString().Replace("<br>", "\r\n");
            //        pic1.Visible = false;
            //    }
            //    else
            //    {
            //        pic1.Visible = true;
            //    }
            //}
            //catch (Exception ex2)
            //{
            //    MessageBox.Show(ex2.Message.ToString());
            //}
        //}

        //private void getOtherException()
        //{
        //    //try
        //    //{

        //    //    DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdmingetHDOException]", new System.Data.SqlClient.SqlParameter[0] { });

        //    //    if (dsTemp.Tables[0].Rows.Count>0  && dsTemp.Tables[0].Rows[0]["message"].ToString() != "")
        //    //    {
        //    //        txtOtherException.Text= dsTemp.Tables[0].Rows[0]["message"].ToString().Replace("<br>", "\r\n");
        //    //    }
        //    //}
        //    //catch (Exception ex2)
        //    //{
        //    //    MessageBox.Show(ex2.Message.ToString());
        //    //}
        //}

        private void getStudioMProductWithoutQuestion(string callfrom)
        {
            try
            {

                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_GetStudioMProductsWithoutQuestions]", new System.Data.SqlClient.SqlParameter[1]
                            {
					             new System.Data.SqlClient.SqlParameter("@stateid",MetriconCommon.State)                         
                            });
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    if (callfrom.ToUpper() == "REFRESH")
                    {
                        gridProductsWithoutQuestion.Columns.Clear();
                    }
                    lblRecord1.Text = dsTemp.Tables[0].Rows.Count.ToString();
                    gridProductsWithoutQuestion.DataSource = dsTemp.Tables[0];

                    gridProductsWithoutQuestion.Columns[0].Width = 100;
                    gridProductsWithoutQuestion.Columns[1].Width = 200;
                    gridProductsWithoutQuestion.Columns[2].AutoSizeMode=DataGridViewAutoSizeColumnMode.Fill;
                    gridProductsWithoutQuestion.Columns[2].Width = 500;
                    gridProductsWithoutQuestion.RowHeadersVisible = false;

                    DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                    btnColumn.HeaderText = "";
                    btnColumn.Text = "Edit";
                    btnColumn.UseColumnTextForButtonValue = true;
                    btnColumn.DisplayIndex = 3;
                    btnColumn.Width = 40;
                    btnColumn.Name = "editcolumn";

                    gridProductsWithoutQuestion.Columns.Add(btnColumn);

                }
                else
                {
                    lblRecord1.Text = "0";
                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message.ToString());
            }

        }

        private void getStudioMProductWithoutPrice(string callfrom)
        {
            try
            {

                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_GetStudioMProductsWithoutPrice]", new System.Data.SqlClient.SqlParameter[1]
                            {
					             new System.Data.SqlClient.SqlParameter("@stateid",MetriconCommon.State)                         
                            });
                if (dsTemp.Tables[0].Rows.Count > 0)
                {
                    if (callfrom.ToUpper() == "REFRESH")
                    {
                        gridProductsWithoutPrice.Columns.Clear();
                    }
                    
                    lblRecord2.Text = dsTemp.Tables[0].Rows.Count.ToString();
                    gridProductsWithoutPrice.DataSource = dsTemp.Tables[0];
                    gridProductsWithoutPrice.Columns[0].Width = 100;
                    gridProductsWithoutPrice.Columns[1].Width = 200;
                    gridProductsWithoutPrice.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    gridProductsWithoutPrice.Columns[2].Width = 500;
                    gridProductsWithoutPrice.RowHeadersVisible = false;

                    DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                    btnColumn.HeaderText = "";
                    btnColumn.Text = "Edit";
                    btnColumn.UseColumnTextForButtonValue = true;
                    btnColumn.DisplayIndex = 3;
                    btnColumn.Width = 40;
                    btnColumn.Name = "editcolumn";

                    gridProductsWithoutPrice.Columns.Add(btnColumn);
                }
                else
                {
                    lblRecord2.Text = "0";
                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message.ToString());
            }



        }
        private void getHomeTemplateException()
        {
            try
            {

                DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spw_jobinterface_checkHomeTemplateMatchSQStoBC]", new System.Data.SqlClient.SqlParameter[0] { });

                if (dsTemp.Tables[0].Rows.Count>0)
                {
                    dataGridView1.DataSource = dsTemp.Tables[0];
                    dataGridView1.Columns["story"].HeaderText = "Stories";
                    dataGridView1.Columns["story"].Width =50;
                    dataGridView1.Columns["story"].DisplayIndex = 0;

                    dataGridView1.Columns["areaname"].HeaderText = "SQS Area";
                    dataGridView1.Columns["areaname"].Width = 110;
                    dataGridView1.Columns["areaname"].DisplayIndex = 1;

                    dataGridView1.Columns["sortorder"].HeaderText = "Sort Order";
                    dataGridView1.Columns["sortorder"].Width = 80;
                    dataGridView1.Columns["sortorder"].DisplayIndex = 2;

                    dataGridView1.Columns["bmsc_desc"].HeaderText = "BC Name";
                    dataGridView1.Columns["bmsc_desc"].Width = 100;
                    dataGridView1.Columns["bmsc_desc"].DisplayIndex = 3;

                    dataGridView1.Columns["bmsc_paragraph"].HeaderText = "BC Para";
                    dataGridView1.Columns["bmsc_paragraph"].Width = 70;
                    dataGridView1.Columns["bmsc_paragraph"].DisplayIndex = 4;

                    pic2.Visible = false;
                }
                else
                {
                    pic2.Visible = true;
                }
            }
            catch (Exception ex2)
            {
                MessageBox.Show(ex2.Message.ToString());
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmHelp == null)
            {
                frmHelp = new frmPDF();
            }

            frmHelp.Owner = this;
            frmHelp.Show();

        }

        private void inclusionPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmInclusion == null)
            {
                frmInclusion = new frmInclusionPackage();
            }
            frmInclusion.ShowDialog();
        }

        private void configureInclusionPackageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmInclusionPag == null)
            {
                frmInclusionPag = new frmInclusionPackagePag();
            }
            frmInclusionPag.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void inclusionPromotionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmInclusionPromo == null)
            {
                frmInclusionPromo = new frmInclusionPromotion();
            }
            frmInclusionPromo.ShowDialog();
        }

        private void productDisplayOptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmProductDisplayOption == null)
            {
                frmProductDisplayOption = new frmProductDisplayOption();
            }
            frmProductDisplayOption.ShowDialog();
            
        }

        private void pAGToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new frmHomeDisplayOption().Show();
        }

        private void copyHomeConToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fCopyPag == null)
            {
                fCopyPag = new frmCopyPAG();
            }
            fCopyPag.ShowDialog();
        }

        //private void promotionToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    //var win = new SQSAdmin_WpfCustomControlLibrary.frmPromotion(int.Parse(MetriconCommon.UserState));
        //    //ElementHost.EnableModelessKeyboardInterop(win);
        //    //win.Show();
        //    SQSAdmin_WpfCustomControlLibrary.frmPromotion win = new SQSAdmin_WpfCustomControlLibrary.frmPromotion(int.Parse(MetriconCommon.UserState));
        //    win.ShowDialog();
        //}

        private void studioMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmStudioMFunctions win = new SQSAdmin_WpfCustomControlLibrary.frmStudioMFunctions(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{

        //}
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                //this.pictureBox1.Visible = false;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            //if (displayHomeDisplayOption == null)
            //    displayHomeDisplayOption = new frmDisplayHomeDisplayOption();
            //e.Result = displayHomeDisplayOption.ShowDialog();
            //this.BeginInvoke((Action)(() =>
            //    {
            //        if (frmHelp == null)
            //        {
            //            frmHelp = new frmPDF();
            //        }

            //        frmHelp.Owner = this;
            //        frmHelp.Show();
            //    }));

            

        }

        //private void standardInclusionOptionToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    SQSAdmin_WpfCustomControlLibrary.frmStandardInclusionMapping win = new SQSAdmin_WpfCustomControlLibrary.frmStandardInclusionMapping(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
        //    win.ShowDialog();
        //}

        private void standardInclusionStandardOptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmStandardInclusionMapping win = new SQSAdmin_WpfCustomControlLibrary.frmStandardInclusionMapping(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        private void configureStandardInclusionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmStandardInclusion win = new SQSAdmin_WpfCustomControlLibrary.frmStandardInclusion(int.Parse(MetriconCommon.UserState));
            win.ShowDialog();
        }

        private void minimumAreaGroupByHomePlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmAreaGroupByHomePlan win = new SQSAdmin_WpfCustomControlLibrary.frmAreaGroupByHomePlan(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        private void createConfigurePromorionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmPromotion win = new SQSAdmin_WpfCustomControlLibrary.frmPromotion(int.Parse(MetriconCommon.UserState));
            win.ShowDialog();
        }

        private void checkItemInPromotionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmPagInPromotion win = new SQSAdmin_WpfCustomControlLibrary.frmPagInPromotion(int.Parse(MetriconCommon.UserState));
            win.ShowDialog();
        }

        private void relatedAreasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.ctrlRelatedAreas win = new SQSAdmin_WpfCustomControlLibrary.ctrlRelatedAreas(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        private void copyProductConfiguratyionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmCopyProductConfiguration win = new SQSAdmin_WpfCustomControlLibrary.frmCopyProductConfiguration(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        private void homeConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmHomeList win = new SQSAdmin_WpfCustomControlLibrary.frmHomeList(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        //private void qRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (frmQR == null)
        //    {
        //        frmQR = new frmQRCode();
        //    }

        //    frmQR.ShowDialog();
        //}

        private void gridProductsWithoutQuestion_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gridProductsWithoutQuestion.Columns["editcolumn"].Index && e.RowIndex >= 0)
            {
                string productid = gridProductsWithoutQuestion.Rows[e.RowIndex].Cells[0].Value.ToString();
                string productname = gridProductsWithoutQuestion.Rows[e.RowIndex].Cells[1].Value.ToString();

                var win = new SQSAdmin_WpfCustomControlLibrary.frmStudioM(productid, productname, int.Parse(MetriconCommon.State), true);
                ElementHost.EnableModelessKeyboardInterop(win);
                win.Show();
            }
        }

        private void btnRefreshQuestion_Click(object sender, EventArgs e)
        {
            getStudioMProductWithoutQuestion("REFRESH");
        }

        private void btnRefreshPrice_Click(object sender, EventArgs e)
        {
            getStudioMProductWithoutPrice("REFRESH");
        }

        private void gridProductsWithoutPrice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == gridProductsWithoutQuestion.Columns["editcolumn"].Index && e.RowIndex >= 0)
            {
                string productid = gridProductsWithoutPrice.Rows[e.RowIndex].Cells[0].Value.ToString();
                string productname = gridProductsWithoutPrice.Rows[e.RowIndex].Cells[1].Value.ToString();

                frmPrice priceForm = new frmPrice();
                priceForm.ProductID = productid;
                priceForm.SelectedProductName = productname;
                priceForm.StateID = MetriconCommon.State;
                priceForm.ShowDialog();

            }
        }

        private void bulkPrintQRCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmPrintQR == null)
            {
                frmPrintQR = new frmBulkPrintQRCode();
            }

            frmPrintQR.ShowDialog();
        }

        private void replaceDOLProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmDOLProductReplacement win = new SQSAdmin_WpfCustomControlLibrary.frmDOLProductReplacement(int.Parse(MetriconCommon.UserState),  MetriconCommon.UserCode);
            win.ShowDialog();
        }

        private void downLoadFormatSheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MetriconCommon.GenerateExcel("Product","");
        }

        private void horizToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MetriconCommon.GenerateExcel("Price", "HORIZONTAL");
        }

        private void verticalFormatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MetriconCommon.GenerateExcel("Price","VERTICAL");
        }



        private void quantityManagementToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmQuantityManagement win = new SQSAdmin_WpfCustomControlLibrary.frmQuantityManagement(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        private void downloadFormatSheetToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MetriconCommon.GenerateExcel("Quantity", "");
        }


        private void importProductToolStripMenuItem1_Click(object sender, EventArgs e)
        {
               var   win = new SQSAdmin_WpfCustomControlLibrary.ImportValidation(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
               ElementHost.EnableModelessKeyboardInterop(win);
                win.Show();
        }

        private void importPriceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.PriceImportValidation win = new SQSAdmin_WpfCustomControlLibrary.PriceImportValidation(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        private void importQuantityToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.QuantityImportValidation win = new SQSAdmin_WpfCustomControlLibrary.QuantityImportValidation(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        private void quantityManagementToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmQuantityManagement win = new SQSAdmin_WpfCustomControlLibrary.frmQuantityManagement(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        private void buttonBackColorPicker_Click(object sender, EventArgs e)
        {
            // Show the color dialog.
            DialogResult result = colorDialog1.ShowDialog();
            // See if user pressed ok.
            if (result == DialogResult.OK)
            {
                // Set form background to the selected color.
                textBoxBackColorPicker.BackColor = colorDialog1.Color;
                classCustomizeScreenLookAndFeel.customizeMyScreen(this, textBoxBackColorPicker.BackColor);
                MetriconCommon.SetSettings("BackColor", "#" + colorDialog1.Color.Name);
            }
        }

        private void disclaimerManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDisclaimerManagement frm = new frmDisclaimerManagement();
            frm.ShowDialog();
        }

        private void attachmentManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmDocumentManagement win = new SQSAdmin_WpfCustomControlLibrary.frmDocumentManagement(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }

        private void disclaimerDaysManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SQSAdmin_WpfCustomControlLibrary.frmBasePriceHoldingDaysManagement win = new SQSAdmin_WpfCustomControlLibrary.frmBasePriceHoldingDaysManagement(int.Parse(MetriconCommon.UserState), MetriconCommon.UserCode);
            win.ShowDialog();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void discountsManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (discountsMgmt == null)
            {
                discountsMgmt = new frmProductCodeDiscounts();
            }

            discountsMgmt.ShowDialog();
        }
    }
}