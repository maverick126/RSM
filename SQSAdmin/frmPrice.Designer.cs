namespace SQSAdmin
{
	partial class frmPrice
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBoxBTPTargetMargin = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxCMAPercent = new System.Windows.Forms.TextBox();
            this.labelCMAPercent = new System.Windows.Forms.Label();
            this.textBoxTargetMargin = new System.Windows.Forms.TextBox();
            this.labelTargetMargin = new System.Windows.Forms.Label();
            this.checkBoxApplyCMA = new System.Windows.Forms.CheckBox();
            this.textBoxDBCCost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkAllRegions = new System.Windows.Forms.CheckBox();
            this.dropRegion = new System.Windows.Forms.ComboBox();
            this.chkSamePrice = new System.Windows.Forms.CheckBox();
            this.radioregion = new System.Windows.Forms.RadioButton();
            this.radioregiongroup = new System.Windows.Forms.RadioButton();
            this.dropregiongroup = new System.Windows.Forms.ComboBox();
            this.chkDerivedCost = new System.Windows.Forms.CheckBox();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPriceID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCancelData = new System.Windows.Forms.Button();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.txtSellPrice = new System.Windows.Forms.TextBox();
            this.textBoxBTPCost = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.txtEffectiveDate = new System.Windows.Forms.MaskedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(576, 3);
            this.btnSave.TabIndex = 23;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBoxBTPTargetMargin);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.textBoxCMAPercent);
            this.panel2.Controls.Add(this.labelCMAPercent);
            this.panel2.Controls.Add(this.textBoxTargetMargin);
            this.panel2.Controls.Add(this.labelTargetMargin);
            this.panel2.Controls.Add(this.checkBoxApplyCMA);
            this.panel2.Controls.Add(this.textBoxDBCCost);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.radioregion);
            this.panel2.Controls.Add(this.radioregiongroup);
            this.panel2.Controls.Add(this.dropregiongroup);
            this.panel2.Controls.Add(this.chkDerivedCost);
            this.panel2.Controls.Add(this.txtProductName);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtProductID);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtPriceID);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btnCancelData);
            this.panel2.Controls.Add(this.btnSaveData);
            this.panel2.Controls.Add(this.btnNew);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.txtSellPrice);
            this.panel2.Controls.Add(this.textBoxBTPCost);
            this.panel2.Controls.Add(this.chkActive);
            this.panel2.Controls.Add(this.txtEffectiveDate);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(887, 350);
            this.panel2.TabIndex = 14;
            // 
            // textBoxBTPTargetMargin
            // 
            this.textBoxBTPTargetMargin.Enabled = false;
            this.textBoxBTPTargetMargin.Location = new System.Drawing.Point(329, 142);
            this.textBoxBTPTargetMargin.Name = "textBoxBTPTargetMargin";
            this.textBoxBTPTargetMargin.ReadOnly = true;
            this.textBoxBTPTargetMargin.Size = new System.Drawing.Size(65, 20);
            this.textBoxBTPTargetMargin.TabIndex = 49;
            this.textBoxBTPTargetMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(264, 145);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 50;
            this.label8.Text = "BTP Margin";
            // 
            // textBoxCMAPercent
            // 
            this.textBoxCMAPercent.Location = new System.Drawing.Point(443, 118);
            this.textBoxCMAPercent.Name = "textBoxCMAPercent";
            this.textBoxCMAPercent.ReadOnly = true;
            this.textBoxCMAPercent.Size = new System.Drawing.Size(100, 20);
            this.textBoxCMAPercent.TabIndex = 9;
            this.textBoxCMAPercent.Text = "5";
            this.textBoxCMAPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxCMAPercent.Visible = false;
            // 
            // labelCMAPercent
            // 
            this.labelCMAPercent.AutoSize = true;
            this.labelCMAPercent.Location = new System.Drawing.Point(367, 119);
            this.labelCMAPercent.Name = "labelCMAPercent";
            this.labelCMAPercent.Size = new System.Drawing.Size(70, 13);
            this.labelCMAPercent.TabIndex = 48;
            this.labelCMAPercent.Text = "CMA Percent";
            this.labelCMAPercent.Visible = false;
            // 
            // textBoxTargetMargin
            // 
            this.textBoxTargetMargin.Location = new System.Drawing.Point(150, 194);
            this.textBoxTargetMargin.Name = "textBoxTargetMargin";
            this.textBoxTargetMargin.Size = new System.Drawing.Size(100, 20);
            this.textBoxTargetMargin.TabIndex = 11;
            this.textBoxTargetMargin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxTargetMargin.Enter += new System.EventHandler(this.textBoxTargetMargin_Enter);
            this.textBoxTargetMargin.Leave += new System.EventHandler(this.textBoxTargetMargin_Leave);
            // 
            // labelTargetMargin
            // 
            this.labelTargetMargin.AutoSize = true;
            this.labelTargetMargin.Location = new System.Drawing.Point(12, 194);
            this.labelTargetMargin.Name = "labelTargetMargin";
            this.labelTargetMargin.Size = new System.Drawing.Size(73, 13);
            this.labelTargetMargin.TabIndex = 46;
            this.labelTargetMargin.Text = "Target Margin";
            // 
            // checkBoxApplyCMA
            // 
            this.checkBoxApplyCMA.AutoSize = true;
            this.checkBoxApplyCMA.Location = new System.Drawing.Point(267, 118);
            this.checkBoxApplyCMA.Name = "checkBoxApplyCMA";
            this.checkBoxApplyCMA.Size = new System.Drawing.Size(78, 17);
            this.checkBoxApplyCMA.TabIndex = 6;
            this.checkBoxApplyCMA.Text = "Apply CMA";
            this.checkBoxApplyCMA.UseVisualStyleBackColor = true;
            this.checkBoxApplyCMA.CheckedChanged += new System.EventHandler(this.checkBoxApplyCMA_CheckedChanged);
            // 
            // textBoxDBCCost
            // 
            this.textBoxDBCCost.Location = new System.Drawing.Point(150, 115);
            this.textBoxDBCCost.Name = "textBoxDBCCost";
            this.textBoxDBCCost.Size = new System.Drawing.Size(100, 20);
            this.textBoxDBCCost.TabIndex = 8;
            this.textBoxDBCCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDBCCost.TextChanged += new System.EventHandler(this.textBoxDBCCost_TextChanged);
            this.textBoxDBCCost.Enter += new System.EventHandler(this.textBoxDBCCost_Enter);
            this.textBoxDBCCost.Leave += new System.EventHandler(this.textBoxDBCCost_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 115);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "DBC Cost";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.chkAllRegions);
            this.panel4.Controls.Add(this.dropRegion);
            this.panel4.Controls.Add(this.chkSamePrice);
            this.panel4.Location = new System.Drawing.Point(150, 259);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(726, 29);
            this.panel4.TabIndex = 15;
            // 
            // chkAllRegions
            // 
            this.chkAllRegions.AutoSize = true;
            this.chkAllRegions.Enabled = false;
            this.chkAllRegions.Location = new System.Drawing.Point(221, 8);
            this.chkAllRegions.Name = "chkAllRegions";
            this.chkAllRegions.Size = new System.Drawing.Size(79, 17);
            this.chkAllRegions.TabIndex = 16;
            this.chkAllRegions.Text = "All Regions";
            this.chkAllRegions.UseVisualStyleBackColor = true;
            this.chkAllRegions.CheckedChanged += new System.EventHandler(this.chkAllRegions_CheckedChanged);
            // 
            // dropRegion
            // 
            this.dropRegion.FormattingEnabled = true;
            this.dropRegion.Location = new System.Drawing.Point(1, 6);
            this.dropRegion.Name = "dropRegion";
            this.dropRegion.Size = new System.Drawing.Size(202, 21);
            this.dropRegion.TabIndex = 15;
            // 
            // chkSamePrice
            // 
            this.chkSamePrice.AutoSize = true;
            this.chkSamePrice.Enabled = false;
            this.chkSamePrice.Location = new System.Drawing.Point(307, 8);
            this.chkSamePrice.Name = "chkSamePrice";
            this.chkSamePrice.Size = new System.Drawing.Size(139, 17);
            this.chkSamePrice.TabIndex = 17;
            this.chkSamePrice.Text = "Same price for all region";
            this.chkSamePrice.UseVisualStyleBackColor = true;
            // 
            // radioregion
            // 
            this.radioregion.AutoSize = true;
            this.radioregion.Checked = true;
            this.radioregion.Location = new System.Drawing.Point(50, 259);
            this.radioregion.Name = "radioregion";
            this.radioregion.Size = new System.Drawing.Size(59, 17);
            this.radioregion.TabIndex = 14;
            this.radioregion.TabStop = true;
            this.radioregion.Text = "Region";
            this.radioregion.UseVisualStyleBackColor = true;
            this.radioregion.CheckedChanged += new System.EventHandler(this.radioregion_CheckedChanged);
            // 
            // radioregiongroup
            // 
            this.radioregiongroup.AutoSize = true;
            this.radioregiongroup.Location = new System.Drawing.Point(50, 230);
            this.radioregiongroup.Name = "radioregiongroup";
            this.radioregiongroup.Size = new System.Drawing.Size(91, 17);
            this.radioregiongroup.TabIndex = 12;
            this.radioregiongroup.Text = "Region Group";
            this.radioregiongroup.UseVisualStyleBackColor = true;
            this.radioregiongroup.CheckedChanged += new System.EventHandler(this.radioregiongroup_CheckedChanged);
            // 
            // dropregiongroup
            // 
            this.dropregiongroup.FormattingEnabled = true;
            this.dropregiongroup.Location = new System.Drawing.Point(150, 226);
            this.dropregiongroup.Name = "dropregiongroup";
            this.dropregiongroup.Size = new System.Drawing.Size(202, 21);
            this.dropregiongroup.TabIndex = 13;
            // 
            // chkDerivedCost
            // 
            this.chkDerivedCost.AutoSize = true;
            this.chkDerivedCost.Location = new System.Drawing.Point(370, 66);
            this.chkDerivedCost.Name = "chkDerivedCost";
            this.chkDerivedCost.Size = new System.Drawing.Size(87, 17);
            this.chkDerivedCost.TabIndex = 7;
            this.chkDerivedCost.Text = "Derived Cost";
            this.chkDerivedCost.UseVisualStyleBackColor = true;
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(150, 37);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.ReadOnly = true;
            this.txtProductName.Size = new System.Drawing.Size(367, 20);
            this.txtProductName.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 34;
            this.label7.Text = "Product Name";
            // 
            // txtProductID
            // 
            this.txtProductID.Location = new System.Drawing.Point(150, 11);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.ReadOnly = true;
            this.txtProductID.Size = new System.Drawing.Size(367, 20);
            this.txtProductID.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Product ID";
            // 
            // txtPriceID
            // 
            this.txtPriceID.Location = new System.Drawing.Point(150, 63);
            this.txtPriceID.Name = "txtPriceID";
            this.txtPriceID.ReadOnly = true;
            this.txtPriceID.Size = new System.Drawing.Size(202, 20);
            this.txtPriceID.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 13);
            this.label5.TabIndex = 29;
            this.label5.Text = "Price ID";
            // 
            // btnCancelData
            // 
            this.btnCancelData.Location = new System.Drawing.Point(720, 307);
            this.btnCancelData.Name = "btnCancelData";
            this.btnCancelData.Size = new System.Drawing.Size(75, 23);
            this.btnCancelData.TabIndex = 20;
            this.btnCancelData.Text = "Cancel";
            this.btnCancelData.UseVisualStyleBackColor = true;
            this.btnCancelData.Click += new System.EventHandler(this.btnCancelData_Click);
            // 
            // btnSaveData
            // 
            this.btnSaveData.Location = new System.Drawing.Point(802, 307);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(75, 23);
            this.btnSaveData.TabIndex = 21;
            this.btnSaveData.Text = "Save";
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(558, 307);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 18;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(639, 307);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 19;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Visible = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtSellPrice
            // 
            this.txtSellPrice.Location = new System.Drawing.Point(150, 168);
            this.txtSellPrice.Name = "txtSellPrice";
            this.txtSellPrice.Size = new System.Drawing.Size(100, 20);
            this.txtSellPrice.TabIndex = 10;
            this.txtSellPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSellPrice.Enter += new System.EventHandler(this.txtSellPrice_Enter);
            this.txtSellPrice.Leave += new System.EventHandler(this.txtSellPrice_Leave);
            // 
            // textBoxBTPCost
            // 
            this.textBoxBTPCost.Location = new System.Drawing.Point(150, 142);
            this.textBoxBTPCost.Name = "textBoxBTPCost";
            this.textBoxBTPCost.Size = new System.Drawing.Size(100, 20);
            this.textBoxBTPCost.TabIndex = 5;
            this.textBoxBTPCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxBTPCost.TextChanged += new System.EventHandler(this.textBoxBTPCost_TextChanged);
            this.textBoxBTPCost.Enter += new System.EventHandler(this.textBoxBTPCost_Enter);
            this.textBoxBTPCost.Leave += new System.EventHandler(this.textBoxBTPCost_Leave);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(370, 91);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 4;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // txtEffectiveDate
            // 
            this.txtEffectiveDate.Location = new System.Drawing.Point(150, 89);
            this.txtEffectiveDate.Mask = "00/00/0000";
            this.txtEffectiveDate.Name = "txtEffectiveDate";
            this.txtEffectiveDate.Size = new System.Drawing.Size(202, 20);
            this.txtEffectiveDate.TabIndex = 3;
            this.txtEffectiveDate.ValidatingType = typeof(System.DateTime);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 168);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Sell Price";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "BTP Cost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Effective Date";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dataGridView1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 350);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(887, 293);
            this.panel3.TabIndex = 15;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(876, 281);
            this.dataGridView1.TabIndex = 22;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dataGridView1_CellValidating);
            // 
            // frmPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(887, 674);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.MaximizeBox = false;
            this.Name = "frmPrice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Prices";
            this.Load += new System.EventHandler(this.frmPrice_Load);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.panel3, 0);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox txtEffectiveDate;
		private System.Windows.Forms.CheckBox chkActive;
		private System.Windows.Forms.TextBox txtSellPrice;
		private System.Windows.Forms.TextBox textBoxBTPCost;
		private System.Windows.Forms.Button btnSaveData;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.Button btnEdit;
		private System.Windows.Forms.Button btnCancelData;
		private System.Windows.Forms.TextBox txtPriceID;
        private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtProductID;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtProductName;
		private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkDerivedCost;
        private System.Windows.Forms.ComboBox dropregiongroup;
        private System.Windows.Forms.RadioButton radioregion;
        private System.Windows.Forms.RadioButton radioregiongroup;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkAllRegions;
        private System.Windows.Forms.ComboBox dropRegion;
        private System.Windows.Forms.CheckBox chkSamePrice;
        private System.Windows.Forms.CheckBox checkBoxApplyCMA;
        private System.Windows.Forms.TextBox textBoxDBCCost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTargetMargin;
        private System.Windows.Forms.Label labelTargetMargin;
        private System.Windows.Forms.TextBox textBoxCMAPercent;
        private System.Windows.Forms.Label labelCMAPercent;
        private System.Windows.Forms.TextBox textBoxBTPTargetMargin;
        private System.Windows.Forms.Label label8;
    }
}
