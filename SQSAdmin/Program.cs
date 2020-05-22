using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SQSAdmin
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			frmLogin loginForm = new frmLogin();

			Application.Run(loginForm);

			if (loginForm.Authenticated)
				Application.Run(new frmMain());

		}
	}
}