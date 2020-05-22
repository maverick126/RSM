using SQSAdmin.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQSAdmin
{
    public partial class formBase : Form
    {
        public Color backColor;

        public formBase()
        {
            InitializeComponent();

            classCustomizeScreenLookAndFeel.customizeMyScreen(this, backColor);
        }
    }
}
