namespace SQSAdmin
{
    partial class frmProductCodeDiscounts
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
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.checkBoxSearchBudgeted = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtSearchDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSearchCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxBudgeted = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxSearchActive = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.buttonAddProductSearch = new System.Windows.Forms.Button();
            this.labelProductNotFound = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Checked = true;
            this.checkBoxActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxActive.Location = new System.Drawing.Point(246, 77);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(56, 17);
            this.checkBoxActive.TabIndex = 27;
            this.checkBoxActive.Text = "Active";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(117, 518);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(107, 20);
            this.txtCode.TabIndex = 24;
            this.txtCode.TextChanged += new System.EventHandler(this.txtCode_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 522);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Code";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(442, 85);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(81, 23);
            this.btnSearch.TabIndex = 22;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // checkBoxSearchBudgeted
            // 
            this.checkBoxSearchBudgeted.AutoSize = true;
            this.checkBoxSearchBudgeted.Checked = true;
            this.checkBoxSearchBudgeted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSearchBudgeted.Location = new System.Drawing.Point(345, 66);
            this.checkBoxSearchBudgeted.Name = "checkBoxSearchBudgeted";
            this.checkBoxSearchBudgeted.Size = new System.Drawing.Size(72, 17);
            this.checkBoxSearchBudgeted.TabIndex = 21;
            this.checkBoxSearchBudgeted.Text = "Budgeted";
            this.checkBoxSearchBudgeted.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(402, 70);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Add";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtSearchDesc
            // 
            this.txtSearchDesc.Location = new System.Drawing.Point(101, 90);
            this.txtSearchDesc.Name = "txtSearchDesc";
            this.txtSearchDesc.Size = new System.Drawing.Size(207, 20);
            this.txtSearchDesc.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Product Name:";
            // 
            // txtSearchCode
            // 
            this.txtSearchCode.Location = new System.Drawing.Point(102, 67);
            this.txtSearchCode.Name = "txtSearchCode";
            this.txtSearchCode.Size = new System.Drawing.Size(100, 20);
            this.txtSearchCode.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Product ID:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelProductNotFound);
            this.groupBox1.Controls.Add(this.buttonAddProductSearch);
            this.groupBox1.Controls.Add(this.checkBoxBudgeted);
            this.groupBox1.Controls.Add(this.checkBoxActive);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Location = new System.Drawing.Point(22, 495);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(501, 106);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add a new discounted  product code ";
            // 
            // checkBoxBudgeted
            // 
            this.checkBoxBudgeted.AutoSize = true;
            this.checkBoxBudgeted.Checked = true;
            this.checkBoxBudgeted.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBudgeted.Location = new System.Drawing.Point(95, 76);
            this.checkBoxBudgeted.Name = "checkBoxBudgeted";
            this.checkBoxBudgeted.Size = new System.Drawing.Size(72, 17);
            this.checkBoxBudgeted.TabIndex = 29;
            this.checkBoxBudgeted.Text = "Budgeted";
            this.checkBoxBudgeted.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 121);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(501, 353);
            this.dataGridView1.TabIndex = 15;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(244, 16);
            this.label1.TabIndex = 14;
            this.label1.Text = "Discounted Products Management";
            // 
            // checkBoxSearchActive
            // 
            this.checkBoxSearchActive.AutoSize = true;
            this.checkBoxSearchActive.Checked = true;
            this.checkBoxSearchActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSearchActive.Location = new System.Drawing.Point(345, 91);
            this.checkBoxSearchActive.Name = "checkBoxSearchActive";
            this.checkBoxSearchActive.Size = new System.Drawing.Size(56, 17);
            this.checkBoxSearchActive.TabIndex = 28;
            this.checkBoxSearchActive.Text = "Active";
            this.checkBoxSearchActive.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 545);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Description";
            // 
            // txtDesc
            // 
            this.txtDesc.Enabled = false;
            this.txtDesc.Location = new System.Drawing.Point(117, 541);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(382, 20);
            this.txtDesc.TabIndex = 26;
            // 
            // buttonAddProductSearch
            // 
            this.buttonAddProductSearch.Location = new System.Drawing.Point(213, 21);
            this.buttonAddProductSearch.Name = "buttonAddProductSearch";
            this.buttonAddProductSearch.Size = new System.Drawing.Size(81, 23);
            this.buttonAddProductSearch.TabIndex = 30;
            this.buttonAddProductSearch.Text = "Search";
            this.buttonAddProductSearch.UseVisualStyleBackColor = true;
            this.buttonAddProductSearch.Click += new System.EventHandler(this.buttonAddProductSearch_Click);
            // 
            // labelProductNotFound
            // 
            this.labelProductNotFound.AutoSize = true;
            this.labelProductNotFound.Location = new System.Drawing.Point(307, 26);
            this.labelProductNotFound.Name = "labelProductNotFound";
            this.labelProductNotFound.Size = new System.Drawing.Size(92, 13);
            this.labelProductNotFound.TabIndex = 31;
            this.labelProductNotFound.Text = "Product not found";
            this.labelProductNotFound.Visible = false;
            // 
            // frmProductCodeDiscounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 657);
            this.Controls.Add(this.checkBoxSearchActive);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.checkBoxSearchBudgeted);
            this.Controls.Add(this.txtSearchDesc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSearchCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmProductCodeDiscounts";
            this.Text = "Discounted Products List";
            this.Load += new System.EventHandler(this.frmProductCode_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox checkBoxSearchBudgeted;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtSearchDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSearchCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxSearchActive;
        private System.Windows.Forms.CheckBox checkBoxBudgeted;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Button buttonAddProductSearch;
        private System.Windows.Forms.Label labelProductNotFound;
    }
}