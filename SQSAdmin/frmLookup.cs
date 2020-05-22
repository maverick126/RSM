using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SQSAdmin
{
	public partial class frmLookup : Form
	{
		public frmLookup()
		{
			InitializeComponent();
		} 

		protected virtual void btnSave_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}