namespace SQSAdmin
{
	partial class frmHomeDisplayOption
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHomeDisplayOption));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.StandardOption = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GeneralOption = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.StandardInclusion = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Active = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.HomeID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HomeDisplayID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Suburb = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewCheckBoxColumn2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewCheckBoxColumn3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedBy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModifiedDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adminSearchHomeDisplayOptionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.pMO006STGDataSet = new SQSAdmin.PMO006STGDataSet();
            this.panel5 = new System.Windows.Forms.Panel();
            this.fillToolStrip = new System.Windows.Forms.ToolStrip();
            this.productAreaGroupIDToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.productAreaGroupIDToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.activeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inactiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.fillToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.adminSearchHomeDisplayOptionTableAdapter = new SQSAdmin.PMO006STGDataSetTableAdapters.AdminSearchHomeDisplayOptionTableAdapter();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adminSearchHomeDisplayOptionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSet)).BeginInit();
            this.panel5.SuspendLayout();
            this.fillToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 499);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(861, 31);
            this.panel1.TabIndex = 13;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnOK.Location = new System.Drawing.Point(783, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "Cancel";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(861, 499);
            this.panel3.TabIndex = 31;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dataGridView1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(0, 29);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(861, 470);
            this.panel6.TabIndex = 34;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StandardOption,
            this.GeneralOption,
            this.StandardInclusion,
            this.Active,
            this.HomeID,
            this.HomeDisplayID,
            this.Suburb,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewCheckBoxColumn1,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewCheckBoxColumn2,
            this.dataGridViewCheckBoxColumn3,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn1,
            this.ModifiedBy,
            this.ModifiedDate});
            this.dataGridView1.DataSource = this.adminSearchHomeDisplayOptionBindingSource;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(861, 470);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // StandardOption
            // 
            this.StandardOption.DataPropertyName = "StandardOption";
            this.StandardOption.HeaderText = "StandardOption";
            this.StandardOption.Name = "StandardOption";
            // 
            // GeneralOption
            // 
            this.GeneralOption.DataPropertyName = "GeneralOption";
            this.GeneralOption.HeaderText = "GeneralOption";
            this.GeneralOption.Name = "GeneralOption";
            // 
            // StandardInclusion
            // 
            this.StandardInclusion.DataPropertyName = "StandardInclusion";
            this.StandardInclusion.HeaderText = "StandardInclusion";
            this.StandardInclusion.Name = "StandardInclusion";
            // 
            // Active
            // 
            this.Active.DataPropertyName = "Active";
            this.Active.HeaderText = "Active";
            this.Active.Name = "Active";
            this.Active.ReadOnly = true;
            // 
            // HomeID
            // 
            this.HomeID.DataPropertyName = "HomeID";
            this.HomeID.HeaderText = "HomeID";
            this.HomeID.Name = "HomeID";
            this.HomeID.ReadOnly = true;
            // 
            // HomeDisplayID
            // 
            this.HomeDisplayID.DataPropertyName = "HomeDisplayID";
            this.HomeDisplayID.HeaderText = "HomeDisplayID";
            this.HomeDisplayID.Name = "HomeDisplayID";
            // 
            // Suburb
            // 
            this.Suburb.DataPropertyName = "suburb";
            this.Suburb.HeaderText = "Display Home Suburb";
            this.Suburb.Name = "Suburb";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ProductID";
            this.dataGridViewTextBoxColumn2.HeaderText = "ProductID";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "HomeName";
            this.dataGridViewTextBoxColumn3.HeaderText = "HomeName";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Quantity";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.dataGridViewTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewTextBoxColumn4.HeaderText = "Quantity";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn1
            // 
            this.dataGridViewCheckBoxColumn1.DataPropertyName = "AddExtraDesc";
            this.dataGridViewCheckBoxColumn1.HeaderText = "AddExtraDesc";
            this.dataGridViewCheckBoxColumn1.Name = "dataGridViewCheckBoxColumn1";
            this.dataGridViewCheckBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "EnterDesc";
            this.dataGridViewTextBoxColumn5.HeaderText = "EnterDesc";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn2
            // 
            this.dataGridViewCheckBoxColumn2.DataPropertyName = "ChangePrice";
            this.dataGridViewCheckBoxColumn2.HeaderText = "ChangePrice";
            this.dataGridViewCheckBoxColumn2.Name = "dataGridViewCheckBoxColumn2";
            this.dataGridViewCheckBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewCheckBoxColumn3
            // 
            this.dataGridViewCheckBoxColumn3.DataPropertyName = "ChangeQty";
            this.dataGridViewCheckBoxColumn3.HeaderText = "ChangeQty";
            this.dataGridViewCheckBoxColumn3.Name = "dataGridViewCheckBoxColumn3";
            this.dataGridViewCheckBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ProductAreaGroupID";
            this.dataGridViewTextBoxColumn6.HeaderText = "ProductAreaGroupID";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "OptionID";
            this.dataGridViewTextBoxColumn1.HeaderText = "OptionID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // ModifiedBy
            // 
            this.ModifiedBy.DataPropertyName = "ModifiedBy";
            this.ModifiedBy.HeaderText = "ModifiedBy";
            this.ModifiedBy.Name = "ModifiedBy";
            // 
            // ModifiedDate
            // 
            this.ModifiedDate.DataPropertyName = "ModifiedDate";
            this.ModifiedDate.HeaderText = "ModifiedDate";
            this.ModifiedDate.Name = "ModifiedDate";
            // 
            // adminSearchHomeDisplayOptionBindingSource
            // 
            this.adminSearchHomeDisplayOptionBindingSource.DataMember = "AdminSearchHomeDisplayOption";
            this.adminSearchHomeDisplayOptionBindingSource.DataSource = this.bindingSource1;
            // 
            // bindingSource1
            // 
            this.bindingSource1.AllowNew = true;
            this.bindingSource1.DataSource = this.pMO006STGDataSet;
            this.bindingSource1.Position = 0;
            // 
            // pMO006STGDataSet
            // 
            this.pMO006STGDataSet.DataSetName = "PMO006STGDataSet";
            this.pMO006STGDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.fillToolStrip);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(861, 29);
            this.panel5.TabIndex = 33;
            // 
            // fillToolStrip
            // 
            this.fillToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.fillToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productAreaGroupIDToolStripLabel,
            this.productAreaGroupIDToolStripTextBox,
            this.toolStripButton1,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.toolStripDropDownButton1,
            this.toolStripComboBox1,
            this.toolStripSeparator2,
            this.fillToolStripButton,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripSeparator3});
            this.fillToolStrip.Location = new System.Drawing.Point(0, 0);
            this.fillToolStrip.Name = "fillToolStrip";
            this.fillToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.fillToolStrip.Size = new System.Drawing.Size(629, 25);
            this.fillToolStrip.TabIndex = 32;
            this.fillToolStrip.Text = "Displays";
            // 
            // productAreaGroupIDToolStripLabel
            // 
            this.productAreaGroupIDToolStripLabel.Name = "productAreaGroupIDToolStripLabel";
            this.productAreaGroupIDToolStripLabel.Size = new System.Drawing.Size(126, 22);
            this.productAreaGroupIDToolStripLabel.Text = "Product Area Group ID";
            // 
            // productAreaGroupIDToolStripTextBox
            // 
            this.productAreaGroupIDToolStripTextBox.Name = "productAreaGroupIDToolStripTextBox";
            this.productAreaGroupIDToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            this.productAreaGroupIDToolStripTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.productAreaGroupIDToolStripTextBox_KeyDown);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton1.Text = "Find PAG";
            this.toolStripButton1.ToolTipText = "Find PAG from product";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Status";
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allToolStripMenuItem,
            this.activeToolStripMenuItem,
            this.inactiveToolStripMenuItem});
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(13, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.allToolStripMenuItem.Text = "All";
            // 
            // activeToolStripMenuItem
            // 
            this.activeToolStripMenuItem.Name = "activeToolStripMenuItem";
            this.activeToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.activeToolStripMenuItem.Text = "Active";
            // 
            // inactiveToolStripMenuItem
            // 
            this.inactiveToolStripMenuItem.Name = "inactiveToolStripMenuItem";
            this.inactiveToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.inactiveToolStripMenuItem.Text = "Inactive";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "All",
            "Active",
            "Inactive"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // fillToolStripButton
            // 
            this.fillToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fillToolStripButton.Name = "fillToolStripButton";
            this.fillToolStripButton.Size = new System.Drawing.Size(46, 22);
            this.fillToolStripButton.Text = "Search";
            this.fillToolStripButton.Click += new System.EventHandler(this.fillToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(105, 22);
            this.toolStripButton2.Text = "Configure Homes";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // adminSearchHomeDisplayOptionTableAdapter
            // 
            this.adminSearchHomeDisplayOptionTableAdapter.ClearBeforeFill = true;
            // 
            // frmHomeDisplayOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 530);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "frmHomeDisplayOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home  Configuration";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmHomeDisplayOption_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adminSearchHomeDisplayOptionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSet)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.fillToolStrip.ResumeLayout(false);
            this.fillToolStrip.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.DataGridView dataGridView1;
		//private System.Windows.Forms.DataGridViewTextBoxColumn optionIDDataGridViewTextBoxColumn;
		//private System.Windows.Forms.DataGridViewTextBoxColumn productIDDataGridViewTextBoxColumn;
		//private System.Windows.Forms.DataGridViewTextBoxColumn homeNameDataGridViewTextBoxColumn;
		//private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
		//private System.Windows.Forms.DataGridViewCheckBoxColumn addExtraDescDataGridViewCheckBoxColumn;
		//private System.Windows.Forms.DataGridViewTextBoxColumn enterDescDataGridViewTextBoxColumn;
		//private System.Windows.Forms.DataGridViewCheckBoxColumn changePriceDataGridViewCheckBoxColumn;
		//private System.Windows.Forms.DataGridViewCheckBoxColumn changeQtyDataGridViewCheckBoxColumn;
		//private System.Windows.Forms.DataGridViewTextBoxColumn productAreaGroupIDDataGridViewTextBoxColumn;
		private System.Windows.Forms.BindingSource adminSearchHomeDisplayOptionBindingSource;
		private System.Windows.Forms.BindingSource bindingSource1;
		private PMO006STGDataSet pMO006STGDataSet;
		private SQSAdmin.PMO006STGDataSetTableAdapters.AdminSearchHomeDisplayOptionTableAdapter adminSearchHomeDisplayOptionTableAdapter;
		private System.Windows.Forms.ToolStrip fillToolStrip;
		private System.Windows.Forms.ToolStripLabel productAreaGroupIDToolStripLabel;
		private System.Windows.Forms.ToolStripTextBox productAreaGroupIDToolStripTextBox;
		private System.Windows.Forms.ToolStripButton fillToolStripButton;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem activeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inactiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn StandardOption;
        private System.Windows.Forms.DataGridViewCheckBoxColumn GeneralOption;
        private System.Windows.Forms.DataGridViewCheckBoxColumn StandardInclusion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Active;
        private System.Windows.Forms.DataGridViewTextBoxColumn HomeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn HomeDisplayID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Suburb;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dataGridViewCheckBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedBy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ModifiedDate;


	}
}