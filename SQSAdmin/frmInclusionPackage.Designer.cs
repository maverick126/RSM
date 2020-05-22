namespace SQSAdmin
{
    partial class frmInclusionPackage
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
            this.label1 = new System.Windows.Forms.Label();
            this.dropRegionGroup = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dropBrand = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dropType = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.dropNewType = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dropNewBrand = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dropNewRegionGroup = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Region Group";
            // 
            // dropRegionGroup
            // 
            this.dropRegionGroup.FormattingEnabled = true;
            this.dropRegionGroup.Location = new System.Drawing.Point(98, 23);
            this.dropRegionGroup.Name = "dropRegionGroup";
            this.dropRegionGroup.Size = new System.Drawing.Size(124, 21);
            this.dropRegionGroup.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Brand";
            // 
            // dropBrand
            // 
            this.dropBrand.FormattingEnabled = true;
            this.dropBrand.Location = new System.Drawing.Point(273, 23);
            this.dropBrand.Name = "dropBrand";
            this.dropBrand.Size = new System.Drawing.Size(111, 21);
            this.dropBrand.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(399, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Type";
            // 
            // dropType
            // 
            this.dropType.FormattingEnabled = true;
            this.dropType.Location = new System.Drawing.Point(437, 22);
            this.dropType.Name = "dropType";
            this.dropType.Size = new System.Drawing.Size(104, 21);
            this.dropType.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(577, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(25, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(633, 413);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Controls.Add(this.dropNewType);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dropNewBrand);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.dropNewRegionGroup);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(25, 520);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(633, 86);
            this.panel1.TabIndex = 8;
            this.panel1.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(546, 48);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(464, 49);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(293, 49);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 12;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // dropNewType
            // 
            this.dropNewType.FormattingEnabled = true;
            this.dropNewType.Location = new System.Drawing.Point(99, 45);
            this.dropNewType.Name = "dropNewType";
            this.dropNewType.Size = new System.Drawing.Size(152, 21);
            this.dropNewType.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Type";
            // 
            // dropNewBrand
            // 
            this.dropNewBrand.FormattingEnabled = true;
            this.dropNewBrand.Location = new System.Drawing.Point(293, 19);
            this.dropNewBrand.Name = "dropNewBrand";
            this.dropNewBrand.Size = new System.Drawing.Size(151, 21);
            this.dropNewBrand.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(257, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Brand";
            // 
            // dropNewRegionGroup
            // 
            this.dropNewRegionGroup.FormattingEnabled = true;
            this.dropNewRegionGroup.Location = new System.Drawing.Point(99, 19);
            this.dropNewRegionGroup.Name = "dropNewRegionGroup";
            this.dropNewRegionGroup.Size = new System.Drawing.Size(152, 21);
            this.dropNewRegionGroup.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Region Group";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(25, 485);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(132, 23);
            this.btnNew.TabIndex = 9;
            this.btnNew.Text = "New Inclusion Package";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // frmInclusionPackage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(691, 628);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dropType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dropBrand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dropRegionGroup);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmInclusionPackage";
            this.Text = "Inclusion Package";
            this.Load += new System.EventHandler(this.frmInclusionPackage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox dropRegionGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox dropBrand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox dropType;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.ComboBox dropNewType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox dropNewBrand;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox dropNewRegionGroup;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnNew;
    }
}