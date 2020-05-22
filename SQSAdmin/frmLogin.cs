using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using System.Configuration;
using System.Web;
using System.Configuration.Assemblies;
using System.IO;
using System.Xml;
using SQSAdmin.Common;
using System.DirectoryServices.AccountManagement;

namespace SQSAdmin
{
	public partial class frmLogin : Form
	{
        StringBuilder firstname ;
        StringBuilder Lastname ;
        string username;
        string userstate;

		public frmLogin()
		{
			InitializeComponent();
		}

		private void frmLogin_Load(object sender, EventArgs e)
		{
			// TODO: This line of code loads data into the 'pMO006DataSet.Administrator' table. You can move, or remove it, as needed.
            getLogonUser();
			LoadUserList();
            LoadState();
            setVersion();

            // hide for now, will show it when required later
            //classCustomizeScreenLookAndFeel.Configuration_Set();
            //// classCustomizeScreenLookAndFeel.customizeMyScreen(this);
        }
        private void getLogonUser()
        {

            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);
            UserPrincipal user = UserPrincipal.Current;
            String Wholename = user.DisplayName;
            username = user.SamAccountName;
            String[] Partialnames = Wholename.ToString().Split(new char[] { ' ' });

            firstname = new StringBuilder();
            Lastname = new StringBuilder();

            firstname.Append(Partialnames[0]);
            Lastname.Append(Partialnames[1]);
            //firstname = new StringBuilder();
            //Lastname = new StringBuilder();
            bool foundsecondflag = false;
            //try
            //{
            //    for (int i = 0; i < Name.Length; i++)
            //    {
            //        String s = Convert.ToString(Name[i]);
            //        if (i == 0)
            //        {
            //            firstname.Append(Name[i]);
            //        }
            //        else if (foundsecondflag == true)
            //        {
            //            Lastname.Append(Name[i]);
            //        }
            //        else
            //        {
            //            if (Name[i].ToString().CompareTo(Name[i].ToString().ToUpper()) != 0)
            //            {
            //                firstname.Append(Name[i]);
            //            }
            //            else
            //            {
            //                Lastname.Append(Name[i]);
            //                foundsecondflag = true;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }

		private void LoadUserList()
		{
            DataSet ds = MetriconCommon.DatabaseManager.ExecuteSQLQuery("SELECT * FROM ADMINISTRATOR where active=1 and AdministratorLogonName='" + username.ToString().ToLower()+"'");
            if (ds.Tables[0].Rows.Count == 1)
            {
                this.comboBox1.DataSource = ds.Tables[0];
                this.comboBox1.DisplayMember = "AdministratorName";
                this.comboBox1.ValueMember = "AdministratorLogonName";
                this.comboBox1.SelectedValue = username.ToString().ToLower();
                //MetriconCommon.State = ds.Tables[0].Rows[0]["stateid"].ToString();
                userstate = ds.Tables[0].Rows[0]["stateid"].ToString();

                this.comboBox1.Enabled = false;
                btnLogin.Enabled = true;
                label2.Text = "";
            }
            else
            {
                //if not a permitted user, just disable the login button
                btnLogin.Enabled = false;
                label2.Text = label2.Text + firstname.ToString().ToLower();
            }
		}


        private void LoadState()
        {
            DataSet dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });
            cmbState.DataSource = dsState.Tables[0];
            cmbState.DisplayMember = "stateAbbreviation";
            cmbState.ValueMember = "stateID";

            if (userstate != null)
            {
                cmbState.SelectedValue = userstate;
            }
        }

		private void btnLogin_Click(object sender, EventArgs e)
		{
			this.Authenticated = true;
            if (radDev.Checked)
            {
                MetriconCommon.Environment = "DEV";
                SQSAdmin_WpfCustomControlLibrary.CommonVariables.Environment = "DEV";
            }
            else if (radTest.Checked)
            {
                MetriconCommon.Environment = "STG";
                SQSAdmin_WpfCustomControlLibrary.CommonVariables.Environment = "STG";
            }
            else
            {
                MetriconCommon.Environment = "LIVE";
                SQSAdmin_WpfCustomControlLibrary.CommonVariables.Environment = "LIVE";
            }

            MetriconCommon.State = cmbState.SelectedValue.ToString();
            MetriconCommon.UserState = cmbState.SelectedValue.ToString();
            MetriconCommon.UserStateName = cmbState.Text;

            MetriconCommon.WindowTitleInfo= "Logged in as " + MetriconCommon.UserName + " Using " + cmbState.Text +" " + MetriconCommon.Environment + " environment";


            SQSAdmin_WpfCustomControlLibrary.CommonVariables.UserCode = ((DataRowView)comboBox1.SelectedItem)["AdministratorID"].ToString();
            SQSAdmin_WpfCustomControlLibrary.CommonVariables.WcfEndpoint = MetriconCommon.getWcfEndpoint();
            SQSAdmin_WpfCustomControlLibrary.CommonVariables.WindowTitleInfo = MetriconCommon.WindowTitleInfo;
            this.Close();

		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox1.SelectedIndex != -1)
			{
				MetriconCommon.UserCode = ((DataRowView)comboBox1.SelectedItem)["AdministratorID"].ToString();
				MetriconCommon.UserName = ((DataRowView)comboBox1.SelectedItem)["AdministratorName"].ToString();
				
			}

		}

		private void radLive_CheckedChanged(object sender, EventArgs e)
		{
			if (radLive.Checked)
			{
				MetriconCommon.Environment = "LIVE";
				MetriconCommon.DatabaseManager.SetConnectionString();
				LoadUserList();
                LoadState();
			}
		}

		private void radTest_CheckedChanged(object sender, EventArgs e)
		{
			if (radTest.Checked)
			{
				MetriconCommon.Environment = "STG";
				MetriconCommon.DatabaseManager.SetConnectionString();

				LoadUserList();
                LoadState();

			}
		}
        private void radDev_CheckedChanged(object sender, EventArgs e)
        {
            if (radDev.Checked)
            {
                MetriconCommon.Environment = "DEV";
                MetriconCommon.DatabaseManager.SetConnectionString();

                LoadUserList();
                LoadState();

            }
        }
		private void button2_Click(object sender, EventArgs e)
		{
			
			MessageBox.Show(radTest.Checked? global::SQSAdmin.Properties.Settings.Default.PMO006AdminTest : global::SQSAdmin.Properties.Settings.Default.PMO006AdminLive);

		}

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void setVersion()
        {

            string value = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(@"http://sqsadmin/sqsadminconfig.xml");
            XmlNodeList nodeList = doc.SelectNodes("connectionStrings/Version");
            foreach (XmlNode node in nodeList)
            {
                value = @"" + node.SelectSingleNode("value").InnerText;
            }

            this.Text = "Retail System Management " + value;
        }
	}
}