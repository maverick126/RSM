namespace SQSAdmin
{
    partial class frmCopyPAG
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripCBState = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripCBBrand = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnSearch = new System.Windows.Forms.ToolStripButton();
            this.cbDestBrand = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.gridDestinationHome = new System.Windows.Forms.DataGridView();
            this.DestinationSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtDestHome = new System.Windows.Forms.TextBox();
            this.gridSourceHome = new System.Windows.Forms.DataGridView();
            this.SourceSelected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cbSourceArea = new System.Windows.Forms.ComboBox();
            this.cbDestinationArea = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.chbDeleteExisting = new System.Windows.Forms.CheckBox();
            this.chbCreateNewPAG = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radHomeFacade = new System.Windows.Forms.RadioButton();
            this.radHomePlan = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDestinationHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSourceHome)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripCBState,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.toolStripCBBrand,
            this.toolStripSeparator2,
            this.toolStripLabel3,
            this.toolStripTextBox1,
            this.toolStripSeparator3,
            this.toolStripBtnSearch});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(758, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel1.Text = "State";
            // 
            // toolStripCBState
            // 
            this.toolStripCBState.Name = "toolStripCBState";
            this.toolStripCBState.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(38, 22);
            this.toolStripLabel2.Text = "Brand";
            // 
            // toolStripCBBrand
            // 
            this.toolStripCBBrand.Name = "toolStripCBBrand";
            this.toolStripCBBrand.Size = new System.Drawing.Size(121, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(40, 22);
            this.toolStripLabel3.Text = "Home";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(150, 25);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripBtnSearch
            // 
            this.toolStripBtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripBtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnSearch.Name = "toolStripBtnSearch";
            this.toolStripBtnSearch.Size = new System.Drawing.Size(46, 22);
            this.toolStripBtnSearch.Text = "&Search";
            this.toolStripBtnSearch.Click += new System.EventHandler(this.toolStripBtnSearch_Click);
            // 
            // cbDestBrand
            // 
            this.cbDestBrand.FormattingEnabled = true;
            this.cbDestBrand.Location = new System.Drawing.Point(60, 34);
            this.cbDestBrand.Name = "cbDestBrand";
            this.cbDestBrand.Size = new System.Drawing.Size(166, 21);
            this.cbDestBrand.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Brand";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(251, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Home";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(601, 31);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // gridDestinationHome
            // 
            this.gridDestinationHome.AllowUserToAddRows = false;
            this.gridDestinationHome.AllowUserToDeleteRows = false;
            this.gridDestinationHome.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridDestinationHome.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DestinationSelected});
            this.gridDestinationHome.Location = new System.Drawing.Point(15, 53);
            this.gridDestinationHome.Name = "gridDestinationHome";
            this.gridDestinationHome.Size = new System.Drawing.Size(661, 145);
            this.gridDestinationHome.TabIndex = 7;
            // 
            // DestinationSelected
            // 
            this.DestinationSelected.HeaderText = "Select";
            this.DestinationSelected.Name = "DestinationSelected";
            this.DestinationSelected.Width = 50;
            // 
            // txtDestHome
            // 
            this.txtDestHome.Location = new System.Drawing.Point(292, 34);
            this.txtDestHome.Name = "txtDestHome";
            this.txtDestHome.Size = new System.Drawing.Size(193, 20);
            this.txtDestHome.TabIndex = 8;
            // 
            // gridSourceHome
            // 
            this.gridSourceHome.AllowUserToAddRows = false;
            this.gridSourceHome.AllowUserToDeleteRows = false;
            this.gridSourceHome.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSourceHome.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SourceSelected});
            this.gridSourceHome.Location = new System.Drawing.Point(15, 49);
            this.gridSourceHome.Name = "gridSourceHome";
            this.gridSourceHome.Size = new System.Drawing.Size(661, 145);
            this.gridSourceHome.TabIndex = 9;
            this.gridSourceHome.CurrentCellDirtyStateChanged += new System.EventHandler(this.gridSourceHome_CurrentCellDirtyStateChanged);
            // 
            // SourceSelected
            // 
            this.SourceSelected.HeaderText = "Select";
            this.SourceSelected.Name = "SourceSelected";
            this.SourceSelected.Width = 50;
            // 
            // cbSourceArea
            // 
            this.cbSourceArea.FormattingEnabled = true;
            this.cbSourceArea.Location = new System.Drawing.Point(59, 19);
            this.cbSourceArea.Name = "cbSourceArea";
            this.cbSourceArea.Size = new System.Drawing.Size(150, 21);
            this.cbSourceArea.TabIndex = 10;
            // 
            // cbDestinationArea
            // 
            this.cbDestinationArea.FormattingEnabled = true;
            this.cbDestinationArea.Location = new System.Drawing.Point(49, 19);
            this.cbDestinationArea.Name = "cbDestinationArea";
            this.cbDestinationArea.Size = new System.Drawing.Size(166, 21);
            this.cbDestinationArea.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Area";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Area";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(283, 707);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(165, 33);
            this.btnCopy.TabIndex = 14;
            this.btnCopy.Text = "&Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // chbDeleteExisting
            // 
            this.chbDeleteExisting.AutoSize = true;
            this.chbDeleteExisting.Checked = true;
            this.chbDeleteExisting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbDeleteExisting.Enabled = false;
            this.chbDeleteExisting.Location = new System.Drawing.Point(227, 22);
            this.chbDeleteExisting.Name = "chbDeleteExisting";
            this.chbDeleteExisting.Size = new System.Drawing.Size(273, 17);
            this.chbDeleteExisting.TabIndex = 15;
            this.chbDeleteExisting.Text = "Delete configuration for destination area before copy";
            this.chbDeleteExisting.UseVisualStyleBackColor = true;
            this.chbDeleteExisting.CheckedChanged += new System.EventHandler(this.chbDeleteExisting_CheckedChanged);
            // 
            // chbCreateNewPAG
            // 
            this.chbCreateNewPAG.AutoSize = true;
            this.chbCreateNewPAG.Checked = true;
            this.chbCreateNewPAG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbCreateNewPAG.Enabled = false;
            this.chbCreateNewPAG.ImageAlign = System.Drawing.ContentAlignment.TopRight;
            this.chbCreateNewPAG.Location = new System.Drawing.Point(521, 24);
            this.chbCreateNewPAG.Name = "chbCreateNewPAG";
            this.chbCreateNewPAG.Size = new System.Drawing.Size(155, 17);
            this.chbCreateNewPAG.TabIndex = 16;
            this.chbCreateNewPAG.Text = "Create new PAG if not exist";
            this.chbCreateNewPAG.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.chbCreateNewPAG.UseVisualStyleBackColor = true;
            this.chbCreateNewPAG.CheckedChanged += new System.EventHandler(this.chbCreateNewPAG_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radHomeFacade);
            this.groupBox1.Controls.Add(this.radHomePlan);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtDestHome);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbDestBrand);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(20, 320);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(694, 142);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search Destination Home";
            // 
            // radHomeFacade
            // 
            this.radHomeFacade.AutoSize = true;
            this.radHomeFacade.Location = new System.Drawing.Point(171, 106);
            this.radHomeFacade.Name = "radHomeFacade";
            this.radHomeFacade.Size = new System.Drawing.Size(92, 17);
            this.radHomeFacade.TabIndex = 11;
            this.radHomeFacade.TabStop = true;
            this.radHomeFacade.Text = "Home Facade";
            this.radHomeFacade.UseVisualStyleBackColor = true;
            this.radHomeFacade.CheckedChanged += new System.EventHandler(this.radHomeFacade_CheckedChanged);
            // 
            // radHomePlan
            // 
            this.radHomePlan.AutoSize = true;
            this.radHomePlan.Checked = true;
            this.radHomePlan.Location = new System.Drawing.Point(60, 107);
            this.radHomePlan.Name = "radHomePlan";
            this.radHomePlan.Size = new System.Drawing.Size(77, 17);
            this.radHomePlan.TabIndex = 10;
            this.radHomePlan.TabStop = true;
            this.radHomePlan.Text = "Home Plan";
            this.radHomePlan.UseVisualStyleBackColor = true;
            this.radHomePlan.CheckedChanged += new System.EventHandler(this.radHomePlan_CheckedChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Show destination homes by:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbSourceArea);
            this.groupBox2.Controls.Add(this.gridSourceHome);
            this.groupBox2.Location = new System.Drawing.Point(20, 103);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(694, 211);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Source Home";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chbCreateNewPAG);
            this.groupBox3.Controls.Add(this.chbDeleteExisting);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cbDestinationArea);
            this.groupBox3.Controls.Add(this.gridDestinationHome);
            this.groupBox3.Location = new System.Drawing.Point(20, 488);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(694, 213);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Destination Home";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(60, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(610, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Copy home configurations from a selected source home and area to a selected desti" +
    "nation home and area.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(674, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Note: Configurations in the destination area will be removed first and PAG in des" +
    "tination area will be created automatically if they are not exists.";
            // 
            // frmCopyPAG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 757);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCopyPAG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy Home Configuration";
            this.Load += new System.EventHandler(this.frmCopyPAG_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDestinationHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridSourceHome)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripCBState;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDestBrand;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripCBBrand;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripBtnSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView gridDestinationHome;
        private System.Windows.Forms.TextBox txtDestHome;
        private System.Windows.Forms.DataGridView gridSourceHome;
        private System.Windows.Forms.ComboBox cbSourceArea;
        private System.Windows.Forms.ComboBox cbDestinationArea;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.CheckBox chbDeleteExisting;
        private System.Windows.Forms.CheckBox chbCreateNewPAG;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn DestinationSelected;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SourceSelected;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radHomeFacade;
        private System.Windows.Forms.RadioButton radHomePlan;
        private System.Windows.Forms.Label label7;

    }
}