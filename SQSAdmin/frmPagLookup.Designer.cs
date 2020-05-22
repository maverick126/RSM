namespace SQSAdmin
{
	partial class frmPagLookup
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnLookupPAG = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lstPAG = new System.Windows.Forms.ListView();
            this.colPAGID = new System.Windows.Forms.ColumnHeader();
            this.colArea = new System.Windows.Forms.ColumnHeader();
            this.colGroup = new System.Windows.Forms.ColumnHeader();
            this.Active = new System.Windows.Forms.ColumnHeader();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 439);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(861, 31);
            this.panel1.TabIndex = 15;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSave.Location = new System.Drawing.Point(783, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtProductID
            // 
            this.txtProductID.Location = new System.Drawing.Point(148, 3);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.ReadOnly = true;
            this.txtProductID.Size = new System.Drawing.Size(440, 20);
            this.txtProductID.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Product ID";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnLookupPAG);
            this.panel2.Controls.Add(this.txtProductID);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(861, 34);
            this.panel2.TabIndex = 18;
            // 
            // btnLookupPAG
            // 
            this.btnLookupPAG.Location = new System.Drawing.Point(592, 3);
            this.btnLookupPAG.Name = "btnLookupPAG";
            this.btnLookupPAG.Size = new System.Drawing.Size(25, 20);
            this.btnLookupPAG.TabIndex = 18;
            this.btnLookupPAG.Text = "...";
            this.btnLookupPAG.UseVisualStyleBackColor = true;
            this.btnLookupPAG.Click += new System.EventHandler(this.btnLookupPAG_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lstPAG);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 34);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(861, 405);
            this.panel3.TabIndex = 19;
            // 
            // lstPAG
            // 
            this.lstPAG.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPAGID,
            this.colArea,
            this.colGroup,
            this.Active});
            this.lstPAG.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPAG.FullRowSelect = true;
            this.lstPAG.GridLines = true;
            this.lstPAG.Location = new System.Drawing.Point(0, 0);
            this.lstPAG.Name = "lstPAG";
            this.lstPAG.Size = new System.Drawing.Size(861, 405);
            this.lstPAG.TabIndex = 1;
            this.lstPAG.UseCompatibleStateImageBehavior = false;
            this.lstPAG.View = System.Windows.Forms.View.Details;
            this.lstPAG.SelectedIndexChanged += new System.EventHandler(this.lstPAG_SelectedIndexChanged);
            this.lstPAG.DoubleClick += new System.EventHandler(this.lstPAG_DoubleClick);
            // 
            // colPAGID
            // 
            this.colPAGID.Text = "PAG ID";
            this.colPAGID.Width = 200;
            // 
            // colArea
            // 
            this.colArea.Text = "Area";
            this.colArea.Width = 200;
            // 
            // colGroup
            // 
            this.colGroup.Text = "Group";
            this.colGroup.Width = 200;
            // 
            // Active
            // 
            this.Active.Text = "Active";
            this.Active.Width = 109;
            // 
            // frmPagLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 470);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmPagLookup";
            this.Text = "Search PAG";
            this.Load += new System.EventHandler(this.frmPagLookup_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox txtProductID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ListView lstPAG;
		private System.Windows.Forms.ColumnHeader colPAGID;
		private System.Windows.Forms.ColumnHeader colArea;
		private System.Windows.Forms.ColumnHeader colGroup;
		private System.Windows.Forms.Button btnLookupPAG;
        private System.Windows.Forms.ColumnHeader Active;
	}
}