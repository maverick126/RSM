using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using SQSAdmin.Common;

namespace SQSAdmin
{
    public partial class frmPDF : Form
    {
        private AxAcroPDFLib.AxAcroPDF axAcroPDF1;
        public frmPDF()
        {
            InitializeComponent();
        }

        private void frmPDF_Load(object sender, EventArgs e)
        {
            this.Text = this.Text + " - " + MetriconCommon.WindowTitleInfo;
            string path="";
            try
            {
                
                XmlDocument doc = new XmlDocument();
                doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
                XmlNodeList nodeList = doc.SelectNodes("connectionStrings/userManualPath");
                foreach (XmlNode node in nodeList)
                {
                   path =@""+ node.SelectSingleNode("path").InnerText;
                }

                if (File.Exists(path))
                {
                    //axAcroPDF1.ref(path);
                    //axAcroPDF1.Show();
                    var acro = (AcroPDFLib.IAcroAXDocShim)axAcroPDF1.GetOcx();
                    acro.LoadFile(path);
                
                }
                else
                {
                    MessageBox.Show("User manual is NOT found!");
                }

                // classCustomizeScreenLookAndFeel.customizeMyScreen(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}