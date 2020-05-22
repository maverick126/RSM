using System;
using System.Windows.Forms;
using System.Drawing;

namespace SQSAdmin.Common
{
    internal static class classCustomizeScreenLookAndFeel
    {
        public static Color backColorSelected = Color.Lavender;

        public static void Configuration_Set()
        {
            try
            {
                // default #ccddff
                backColorSelected = ColorTranslator.FromHtml(MetriconCommon.GetSettings("BackColor").ToString());
            }
            catch(Exception ex)
            {
                // log error ex
            }
        }

        public static void customizeMyScreen(Form myForm, Color? backcolor = null)
        {
            if (backcolor != null)
            {
                backColorSelected = backcolor ?? Color.Lavender;
            }
            myForm.BackColor = backColorSelected;
            
            foreach (Control ctrl in myForm.Controls)
            {
                if (ctrl.Name== "buttonBackColorPicker")
                {
                    continue;
                }
                ctrl.BackColor = backColorSelected;
                if (ctrl.GetType().Name=="GroupBox" || ctrl.GetType().Name == "Panel" || ctrl.GetType().Name == "DataGrid")
                {
                    foreach (Control ctrlGroup in ctrl.Controls)
                    {
                        ctrlGroup.BackColor = backColorSelected;
                    }
                }
            }
        }
    }
}
