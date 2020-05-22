namespace SQSAdmin
{
    partial class frmConfigPriceByRegionProductCategory
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.dropRegion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dropdownState = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.bytype = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.dropProductCat = new System.Windows.Forms.ComboBox();
            this.txtProductCat = new System.Windows.Forms.Label();
            this.dropdownState2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(772, 525);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.txtDate);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Controls.Add(this.btnSearch);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.dropRegion);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.dropdownState);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(764, 499);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Edit Configuration";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(318, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "In this screen, you only can turn on/off a future price configuration";
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(479, 49);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(92, 20);
            this.txtDate.TabIndex = 8;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(25, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(713, 394);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(663, 47);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(393, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Effective Date";
            // 
            // dropRegion
            // 
            this.dropRegion.FormattingEnabled = true;
            this.dropRegion.Location = new System.Drawing.Point(233, 48);
            this.dropRegion.Name = "dropRegion";
            this.dropRegion.Size = new System.Drawing.Size(130, 21);
            this.dropRegion.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(185, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Region";
            // 
            // dropdownState
            // 
            this.dropdownState.FormattingEnabled = true;
            this.dropdownState.Location = new System.Drawing.Point(61, 47);
            this.dropdownState.Name = "dropdownState";
            this.dropdownState.Size = new System.Drawing.Size(99, 21);
            this.dropdownState.TabIndex = 1;
            this.dropdownState.SelectionChangeCommitted += new System.EventHandler(this.dropdownState_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "State";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.chkSelectAll);
            this.tabPage2.Controls.Add(this.dropProductCat);
            this.tabPage2.Controls.Add(this.txtProductCat);
            this.tabPage2.Controls.Add(this.dropdownState2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(764, 499);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "New Configuration";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.CausesValidation = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.selected,
            this.bytype});
            this.dataGridView2.Location = new System.Drawing.Point(18, 108);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 4;
            this.dataGridView2.Size = new System.Drawing.Size(730, 347);
            this.dataGridView2.TabIndex = 19;
            // 
            // selected
            // 
            this.selected.HeaderText = "";
            this.selected.Name = "selected";
            this.selected.Width = 35;
            // 
            // bytype
            // 
            this.bytype.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.bytype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bytype.HeaderText = "Type";
            this.bytype.Items.AddRange(new object[] {
            "By Amount",
            "By Percent"});
            this.bytype.Name = "bytype";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(375, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "SQS will generate new price automatically only when the effective date is due.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(598, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "In this section, you can configure the new price to  regions by specify the incre" +
                "ase amount(or percentage) and effective date. ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(615, 461);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(133, 23);
            this.button1.TabIndex = 16;
            this.button1.Text = "Save Configuration";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(28, 467);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(107, 17);
            this.chkSelectAll.TabIndex = 15;
            this.chkSelectAll.Text = "Select All Region";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // dropProductCat
            // 
            this.dropProductCat.FormattingEnabled = true;
            this.dropProductCat.Location = new System.Drawing.Point(314, 69);
            this.dropProductCat.Name = "dropProductCat";
            this.dropProductCat.Size = new System.Drawing.Size(211, 21);
            this.dropProductCat.TabIndex = 13;
            // 
            // txtProductCat
            // 
            this.txtProductCat.AutoSize = true;
            this.txtProductCat.Location = new System.Drawing.Point(217, 73);
            this.txtProductCat.Name = "txtProductCat";
            this.txtProductCat.Size = new System.Drawing.Size(89, 13);
            this.txtProductCat.TabIndex = 12;
            this.txtProductCat.Text = "Product Category";
            // 
            // dropdownState2
            // 
            this.dropdownState2.FormattingEnabled = true;
            this.dropdownState2.Location = new System.Drawing.Point(62, 68);
            this.dropdownState2.Name = "dropdownState2";
            this.dropdownState2.Size = new System.Drawing.Size(105, 21);
            this.dropdownState2.TabIndex = 11;
            this.dropdownState2.SelectionChangeCommitted += new System.EventHandler(this.dropdownState2_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "State";
            // 
            // frmConfigPriceByRegionProductCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 552);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmConfigPriceByRegionProductCategory";
            this.Text = "Price Configuration";
            this.Load += new System.EventHandler(this.frmConfigPriceByRegionProductCategory_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.ComboBox dropProductCat;
        private System.Windows.Forms.Label txtProductCat;
        private System.Windows.Forms.ComboBox dropdownState2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox dropRegion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox dropdownState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn selected;
        private System.Windows.Forms.DataGridViewComboBoxColumn bytype;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.Label label2;

    }
}