namespace SQSAdmin
{
    partial class frmAddPAGtoPromotion
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
            this.dropGroup3 = new System.Windows.Forms.ComboBox();
            this.dropArea2 = new System.Windows.Forms.ComboBox();
            this.btnClear2 = new System.Windows.Forms.Button();
            this.dropArea = new System.Windows.Forms.ComboBox();
            this.labelArea = new System.Windows.Forms.Label();
            this.btnSearchPromID = new System.Windows.Forms.Button();
            this.txtPromID = new System.Windows.Forms.TextBox();
            this.labelPromotionID = new System.Windows.Forms.Label();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.btnClose = new System.Windows.Forms.Button();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.btnSaveToHome = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.labelGroup2 = new System.Windows.Forms.Label();
            this.labelArea2 = new System.Windows.Forms.Label();
            this.txtPromID2 = new System.Windows.Forms.TextBox();
            this.labelpromotionID2 = new System.Windows.Forms.Label();
            this.labelGroup = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.labelActive = new System.Windows.Forms.Label();
            this.btnSaveChange = new System.Windows.Forms.Button();
            this.groupAdding = new System.Windows.Forms.GroupBox();
            this.btnchangeStatus = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtPromName = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.colPAGID = new System.Windows.Forms.ColumnHeader();
            this.colArea = new System.Windows.Forms.ColumnHeader();
            this.colGroup = new System.Windows.Forms.ColumnHeader();
            this.colProductID = new System.Windows.Forms.ColumnHeader();
            this.colProductName = new System.Windows.Forms.ColumnHeader();
            this.colActive = new System.Windows.Forms.ColumnHeader();
            this.colQty = new System.Windows.Forms.ColumnHeader();
            this.dropGroup = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupAdding.SuspendLayout();
            this.SuspendLayout();
            // 
            // dropGroup3
            // 
            this.dropGroup3.FormattingEnabled = true;
            this.dropGroup3.Location = new System.Drawing.Point(120, 68);
            this.dropGroup3.Name = "dropGroup3";
            this.dropGroup3.Size = new System.Drawing.Size(192, 21);
            this.dropGroup3.TabIndex = 75;
            this.dropGroup3.SelectedIndexChanged += new System.EventHandler(this.dropDownChange);
            // 
            // dropArea2
            // 
            this.dropArea2.FormattingEnabled = true;
            this.dropArea2.Location = new System.Drawing.Point(120, 45);
            this.dropArea2.Name = "dropArea2";
            this.dropArea2.Size = new System.Drawing.Size(192, 21);
            this.dropArea2.TabIndex = 74;
            this.dropArea2.SelectedIndexChanged += new System.EventHandler(this.dropDownChange);
            // 
            // btnClear2
            // 
            this.btnClear2.Location = new System.Drawing.Point(388, 68);
            this.btnClear2.Name = "btnClear2";
            this.btnClear2.Size = new System.Drawing.Size(75, 23);
            this.btnClear2.TabIndex = 73;
            this.btnClear2.Text = "Clear";
            this.btnClear2.UseVisualStyleBackColor = true;
            this.btnClear2.Click += new System.EventHandler(this.btnClear2_Click);
            // 
            // dropArea
            // 
            this.dropArea.DisplayMember = "areaid";
            this.dropArea.FormattingEnabled = true;
            this.dropArea.Location = new System.Drawing.Point(120, 76);
            this.dropArea.Name = "dropArea";
            this.dropArea.Size = new System.Drawing.Size(192, 21);
            this.dropArea.TabIndex = 4;
            this.dropArea.ValueMember = "areaid";
            // 
            // labelArea
            // 
            this.labelArea.AutoSize = true;
            this.labelArea.Location = new System.Drawing.Point(15, 81);
            this.labelArea.Name = "labelArea";
            this.labelArea.Size = new System.Drawing.Size(29, 13);
            this.labelArea.TabIndex = 3;
            this.labelArea.Text = "Area";
            // 
            // btnSearchPromID
            // 
            this.btnSearchPromID.Location = new System.Drawing.Point(321, 27);
            this.btnSearchPromID.Name = "btnSearchPromID";
            this.btnSearchPromID.Size = new System.Drawing.Size(36, 20);
            this.btnSearchPromID.TabIndex = 2;
            this.btnSearchPromID.Text = "...";
            this.btnSearchPromID.UseVisualStyleBackColor = true;
            this.btnSearchPromID.Click += new System.EventHandler(this.btnSearchPromID_Click);
            // 
            // txtPromID
            // 
            this.txtPromID.Enabled = false;
            this.txtPromID.Location = new System.Drawing.Point(120, 26);
            this.txtPromID.Name = "txtPromID";
            this.txtPromID.Size = new System.Drawing.Size(192, 20);
            this.txtPromID.TabIndex = 1;
            // 
            // labelPromotionID
            // 
            this.labelPromotionID.AutoSize = true;
            this.labelPromotionID.Location = new System.Drawing.Point(14, 31);
            this.labelPromotionID.Name = "labelPromotionID";
            this.labelPromotionID.Size = new System.Drawing.Size(68, 13);
            this.labelPromotionID.TabIndex = 0;
            this.labelPromotionID.Text = "Promotion ID";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Active";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Product NAme";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(939, 774);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "ProductID";
            this.columnHeader4.Width = 65;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Area";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "PAG ID";
            this.columnHeader1.Width = 89;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dropGroup3);
            this.groupBox1.Controls.Add(this.dropArea2);
            this.groupBox1.Controls.Add(this.btnClear2);
            this.groupBox1.Controls.Add(this.txtQuantity);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.chkSelectAll);
            this.groupBox1.Controls.Add(this.btnSaveToHome);
            this.groupBox1.Controls.Add(this.listView2);
            this.groupBox1.Controls.Add(this.labelGroup2);
            this.groupBox1.Controls.Add(this.labelArea2);
            this.groupBox1.Controls.Add(this.txtPromID2);
            this.groupBox1.Controls.Add(this.labelpromotionID2);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(547, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(485, 726);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add PAG to Promotion";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(173, 620);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(290, 20);
            this.txtQuantity.TabIndex = 68;
            this.txtQuantity.Text = "1";
            this.txtQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 626);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 13);
            this.label7.TabIndex = 67;
            this.label7.Text = "Quantity";
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(19, 587);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAll.TabIndex = 18;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // btnSaveToHome
            // 
            this.btnSaveToHome.Location = new System.Drawing.Point(283, 677);
            this.btnSaveToHome.Name = "btnSaveToHome";
            this.btnSaveToHome.Size = new System.Drawing.Size(180, 23);
            this.btnSaveToHome.TabIndex = 17;
            this.btnSaveToHome.Text = "Add Selected To Promotion";
            this.btnSaveToHome.UseVisualStyleBackColor = true;
            this.btnSaveToHome.Click += new System.EventHandler(this.btnSaveToHome_Click);
            // 
            // listView2
            // 
            this.listView2.CheckBoxes = true;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.listView2.GridLines = true;
            this.listView2.Location = new System.Drawing.Point(15, 96);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(452, 479);
            this.listView2.TabIndex = 13;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Group";
            // 
            // labelGroup2
            // 
            this.labelGroup2.AutoSize = true;
            this.labelGroup2.Location = new System.Drawing.Point(29, 76);
            this.labelGroup2.Name = "labelGroup2";
            this.labelGroup2.Size = new System.Drawing.Size(36, 13);
            this.labelGroup2.TabIndex = 16;
            this.labelGroup2.Text = "Group";
            // 
            // labelArea2
            // 
            this.labelArea2.AutoSize = true;
            this.labelArea2.Location = new System.Drawing.Point(29, 54);
            this.labelArea2.Name = "labelArea2";
            this.labelArea2.Size = new System.Drawing.Size(29, 13);
            this.labelArea2.TabIndex = 14;
            this.labelArea2.Text = "Area";
            // 
            // txtPromID2
            // 
            this.txtPromID2.Enabled = false;
            this.txtPromID2.Location = new System.Drawing.Point(120, 23);
            this.txtPromID2.Name = "txtPromID2";
            this.txtPromID2.Size = new System.Drawing.Size(192, 20);
            this.txtPromID2.TabIndex = 13;
            // 
            // labelpromotionID2
            // 
            this.labelpromotionID2.AutoSize = true;
            this.labelpromotionID2.Location = new System.Drawing.Point(28, 29);
            this.labelpromotionID2.Name = "labelpromotionID2";
            this.labelpromotionID2.Size = new System.Drawing.Size(68, 13);
            this.labelpromotionID2.TabIndex = 0;
            this.labelpromotionID2.Text = "Promotion ID";
            // 
            // labelGroup
            // 
            this.labelGroup.AutoSize = true;
            this.labelGroup.Location = new System.Drawing.Point(14, 104);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(36, 13);
            this.labelGroup.TabIndex = 5;
            this.labelGroup.Text = "Group";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Promotion Name";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(378, 137);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(120, 132);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(15, 14);
            this.chkActive.TabIndex = 9;
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // labelActive
            // 
            this.labelActive.AutoSize = true;
            this.labelActive.Location = new System.Drawing.Point(16, 131);
            this.labelActive.Name = "labelActive";
            this.labelActive.Size = new System.Drawing.Size(37, 13);
            this.labelActive.TabIndex = 8;
            this.labelActive.Text = "Active";
            // 
            // btnSaveChange
            // 
            this.btnSaveChange.Location = new System.Drawing.Point(546, 752);
            this.btnSaveChange.Name = "btnSaveChange";
            this.btnSaveChange.Size = new System.Drawing.Size(138, 23);
            this.btnSaveChange.TabIndex = 14;
            this.btnSaveChange.Text = "Change Selected Status";
            this.btnSaveChange.UseVisualStyleBackColor = true;
            // 
            // groupAdding
            // 
            this.groupAdding.Controls.Add(this.btnchangeStatus);
            this.groupAdding.Controls.Add(this.btnSaveChange);
            this.groupAdding.Controls.Add(this.btnClear);
            this.groupAdding.Controls.Add(this.txtPromName);
            this.groupAdding.Controls.Add(this.label1);
            this.groupAdding.Controls.Add(this.btnSearch);
            this.groupAdding.Controls.Add(this.chkActive);
            this.groupAdding.Controls.Add(this.labelActive);
            this.groupAdding.Controls.Add(this.listView1);
            this.groupAdding.Controls.Add(this.dropGroup);
            this.groupAdding.Controls.Add(this.labelGroup);
            this.groupAdding.Controls.Add(this.dropArea);
            this.groupAdding.Controls.Add(this.labelArea);
            this.groupAdding.Controls.Add(this.btnSearchPromID);
            this.groupAdding.Controls.Add(this.txtPromID);
            this.groupAdding.Controls.Add(this.labelPromotionID);
            this.groupAdding.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupAdding.Location = new System.Drawing.Point(36, 22);
            this.groupAdding.Name = "groupAdding";
            this.groupAdding.Size = new System.Drawing.Size(478, 723);
            this.groupAdding.TabIndex = 3;
            this.groupAdding.TabStop = false;
            this.groupAdding.Text = "Search Existing PAG";
            // 
            // btnchangeStatus
            // 
            this.btnchangeStatus.Location = new System.Drawing.Point(278, 674);
            this.btnchangeStatus.Name = "btnchangeStatus";
            this.btnchangeStatus.Size = new System.Drawing.Size(175, 23);
            this.btnchangeStatus.TabIndex = 15;
            this.btnchangeStatus.Text = "Remove Selected From List";
            this.btnchangeStatus.UseVisualStyleBackColor = true;
            this.btnchangeStatus.Click += new System.EventHandler(this.btnchangeStatus_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(300, 136);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 13;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtPromName
            // 
            this.txtPromName.Enabled = false;
            this.txtPromName.Location = new System.Drawing.Point(121, 51);
            this.txtPromName.Name = "txtPromName";
            this.txtPromName.Size = new System.Drawing.Size(235, 20);
            this.txtPromName.TabIndex = 12;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPAGID,
            this.colArea,
            this.colGroup,
            this.colProductID,
            this.colProductName,
            this.colActive,
            this.colQty});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(7, 167);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(446, 484);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // colPAGID
            // 
            this.colPAGID.Text = "PAG ID";
            // 
            // colArea
            // 
            this.colArea.Text = "Area";
            // 
            // colGroup
            // 
            this.colGroup.Text = "Group";
            // 
            // colProductID
            // 
            this.colProductID.Text = "ProductID";
            this.colProductID.Width = 65;
            // 
            // colProductName
            // 
            this.colProductName.Text = "Product NAme";
            // 
            // colActive
            // 
            this.colActive.Text = "Active";
            // 
            // colQty
            // 
            this.colQty.Text = "Quantity";
            // 
            // dropGroup
            // 
            this.dropGroup.FormattingEnabled = true;
            this.dropGroup.Location = new System.Drawing.Point(120, 101);
            this.dropGroup.Name = "dropGroup";
            this.dropGroup.Size = new System.Drawing.Size(192, 21);
            this.dropGroup.TabIndex = 6;
            // 
            // frmAddPAGtoPromotion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 816);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupAdding);
            this.Name = "frmAddPAGtoPromotion";
            this.Text = "Add PAG To Promotion";
            this.Load += new System.EventHandler(this.frmAddPAGtoPromotion_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupAdding.ResumeLayout(false);
            this.groupAdding.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox dropGroup3;
        private System.Windows.Forms.ComboBox dropArea2;
        private System.Windows.Forms.Button btnClear2;
        private System.Windows.Forms.ComboBox dropArea;
        private System.Windows.Forms.Label labelArea;
        private System.Windows.Forms.Button btnSearchPromID;
        private System.Windows.Forms.TextBox txtPromID;
        private System.Windows.Forms.Label labelPromotionID;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Button btnSaveToHome;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label labelGroup2;
        private System.Windows.Forms.Label labelArea2;
        private System.Windows.Forms.TextBox txtPromID2;
        private System.Windows.Forms.Label labelpromotionID2;
        private System.Windows.Forms.Label labelGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label labelActive;
        private System.Windows.Forms.Button btnSaveChange;
        private System.Windows.Forms.GroupBox groupAdding;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtPromName;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader colPAGID;
        private System.Windows.Forms.ColumnHeader colArea;
        private System.Windows.Forms.ColumnHeader colGroup;
        private System.Windows.Forms.ColumnHeader colProductID;
        private System.Windows.Forms.ColumnHeader colProductName;
        private System.Windows.Forms.ColumnHeader colActive;
        private System.Windows.Forms.ComboBox dropGroup;
        private System.Windows.Forms.ColumnHeader colQty;
        private System.Windows.Forms.Button btnchangeStatus;

    }
}