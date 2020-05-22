using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SQSAdmin.Common;

namespace SQSAdmin
{
    public partial class frmDOLPDF : Form
    {
        private string PDF;
        public frmDOLPDF()
        {
            InitializeComponent();
        }
        public frmDOLPDF(string PDFName)
        {
            InitializeComponent();
            PDF = PDFName;
        }
        private void frmDOLPDF_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            try
            {
                if (File.Exists(PDF))
                {
                    //axAcroPDF1.LoadFile(PDF);
                    //axAcroPDF1.Show();
                    var acro = (AcroPDFLib.IAcroAXDocShim)axAcroPDF1.GetOcx();
                    acro.LoadFile(PDF);
                }
                else
                {
                    MessageBox.Show("DOL is NOT found!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
    }
}