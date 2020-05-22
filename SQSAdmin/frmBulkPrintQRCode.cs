using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;

//using WebSupergoo.ABCpdf5;
//using WebSupergoo.ABCpdf5.Objects;
//using WebSupergoo.ABCpdf5.Atoms;
using System.Xml;
using SQSAdmin.Common;

namespace SQSAdmin
{
    public partial class frmBulkPrintQRCode : Form
    {
        public frmBulkPrintQRCode()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
/*
        public Doc SetupPDF()
        {
            // Set MediaBox size, and content rectangle
            // A4: w595 h842 
            Doc theDoc = new Doc();
            theDoc.MediaBox.SetRect(0, 0, 595, 842);
            theDoc.Rect.String = "10 15 565 812";
            //theDoc.Rect.Position(0, 0);
            return theDoc;
        }
        public string SavePDF(Doc theDoc, string productid)
        {
            Random R = new Random();
            string dolpath = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
            XmlNodeList nodeList2 = doc.SelectNodes("connectionStrings/DOLPDFPath");
            foreach (XmlNode node in nodeList2)
            {
                dolpath = @"" + node.SelectSingleNode("path").InnerText;
            }

            string pdfFilename = dolpath + productid + "_QRCode" + ".pdf";
            theDoc.Save(pdfFilename);
            theDoc.Clear();

            return pdfFilename;

        }
 * */
        private void btnPrint_Click(object sender, EventArgs e)
        {
            /*
            DataSet ds = GetProducts(cmbType.SelectedValue.ToString(), cmbState.SelectedValue.ToString());
            StringBuilder html=new StringBuilder();
            html.Append( "<html><body style='font-family:arial;verdana'><table width='100%'>");
            int idx = 1;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                MemoryStream ms1 = GetQRCodeImage(dr["productid"].ToString());
                if (idx == 1)
                {
                    html.Append( "<tr>");
                }
                html.Append( "<td style='text-align:center; width:24%'><img alt='' src='data:image/jpeg;base64," + Convert.ToBase64String(ms1.ToArray()) + "' width='119px' height='119px'/><br><br>" + dr["productid"].ToString() + "</td>");

                if (idx == 4)
                {
                    html.Append ("</tr style='height:50px;'><tr><td>&nbsp;</td></tr>");
                }

                idx = idx + 1;
                if(idx>4)
                {
                    idx=1;
                }
            }

            Doc theDoc = SetupPDF();
            int theID = theDoc.AddImageHtml(html.ToString());
            while (true)
            {
                if (!theDoc.Chainable(theID))
                {
                    break;
                }
                theDoc.Page = theDoc.AddPage();
                theID = theDoc.AddImageToChain(theID);
            }

            string pdfname = SavePDF(theDoc, "bulkprint_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString());
            if (File.Exists(pdfname))
            {
                frmDOLPDF frmDOL = new frmDOLPDF(pdfname);
                frmDOL.ShowDialog();
            }
            else
            {
                MessageBox.Show("Product NOT found!");
            }
             * */
        }
        private MemoryStream GetQRCodeImage(string pproductid)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            String encoding = "Byte";
            if (encoding == "Byte")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            }
            else if (encoding == "AlphaNumeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
            }
            else if (encoding == "Numeric")
            {
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
            }
            try
            {
                int scale = Convert.ToInt16("15");
                qrCodeEncoder.QRCodeScale = scale;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid size!");
            }
            try
            {
                int version = Convert.ToInt16("7");
                qrCodeEncoder.QRCodeVersion = version;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid version !");
            }

            string errorCorrect = "M";
            if (errorCorrect == "L")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            else if (errorCorrect == "M")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            else if (errorCorrect == "Q")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            else if (errorCorrect == "H")
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;

            Image image;
            image = qrCodeEncoder.Encode(pproductid);

            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            return ms;
        }


        private void frmBulkPrintQRCode_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            loadState();
            loadType();

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }

        private void loadState()
        {
            DataSet dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });
            cmbState.DataSource = dsState.Tables[0];
            cmbState.DisplayMember = "stateAbbreviation";
            cmbState.ValueMember = "stateID";
            cmbState.SelectedValue = MetriconCommon.UserState;
        }

        private void loadType()
        {
            DataTable tempTable = new DataTable();
            tempTable.TableName = "type";

            tempTable.Columns.Add("typeid");
            tempTable.Columns.Add("typename");
            DataRow dr = tempTable.NewRow();
            dr["typeid"] = "0";
            dr["typename"] = "All products";
            tempTable.Rows.Add(dr);

            DataRow dr2 = tempTable.NewRow();
            dr2["typeid"] = "1";
            dr2["typename"] = "Studio M products only";
            tempTable.Rows.Add(dr2);


            cmbType.DataSource = tempTable;
            cmbType.DisplayMember = "typename";
            cmbType.ValueMember = "typeid";
            cmbType.SelectedValue ="1";
        }

        private DataSet GetProducts(string typeid, string stateid)
        {
            DataSet ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_Admin_GetProductForBulkQRPrint]", new System.Data.SqlClient.SqlParameter[2]
                                {
                                      new System.Data.SqlClient.SqlParameter("@stateid", stateid),
                                      new System.Data.SqlClient.SqlParameter("@printtype", typeid)
                                });
            return ds;
        }

    }
 
}
