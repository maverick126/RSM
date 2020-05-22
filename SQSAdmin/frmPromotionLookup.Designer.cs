namespace SQSAdmin
{
    partial class frmPromotionLookup
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
            this.labelPromID = new System.Windows.Forms.Label();
            this.txtPromID = new System.Windows.Forms.TextBox();
            this.labelPromName = new System.Windows.Forms.Label();
            this.txtPromName = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.promotionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pMO006STGDataSet = new SQSAdmin.PMO006STGDataSet();
            this.promotionTableAdapter = new SQSAdmin.PMO006STGDataSetTableAdapters.promotionTableAdapter();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.promotionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // labelPromID
            // 
            this.labelPromID.AutoSize = true;
            this.labelPromID.Location = new System.Drawing.Point(46, 18);
            this.labelPromID.Name = "labelPromID";
            this.labelPromID.Size = new System.Drawing.Size(68, 13);
            this.labelPromID.TabIndex = 0;
            this.labelPromID.Text = "Promotion ID";
            // 
            // txtPromID
            // 
            this.txtPromID.Location = new System.Drawing.Point(153, 13);
            this.txtPromID.Name = "txtPromID";
            this.txtPromID.Size = new System.Drawing.Size(100, 20);
            this.txtPromID.TabIndex = 1;
            // 
            // labelPromName
            // 
            this.labelPromName.AutoSize = true;
            this.labelPromName.Location = new System.Drawing.Point(46, 42);
            this.labelPromName.Name = "labelPromName";
            this.labelPromName.Size = new System.Drawing.Size(85, 13);
            this.labelPromName.TabIndex = 2;
            this.labelPromName.Text = "Promotion Name";
            // 
            // txtPromName
            // 
            this.txtPromName.Location = new System.Drawing.Point(153, 36);
            this.txtPromName.Name = "txtPromName";
            this.txtPromName.Size = new System.Drawing.Size(322, 20);
            this.txtPromName.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(760, 34);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column8,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column9,
            this.Column10});
            this.dataGridView1.DataSource = this.promotionBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(13, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(918, 359);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(751, 446);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(847, 445);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(497, 39);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 8;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // promotionBindingSource
            // 
            this.promotionBindingSource.DataMember = "promotion";
            this.promotionBindingSource.DataSource = this.pMO006STGDataSet;
            // 
            // pMO006STGDataSet
            // 
            this.pMO006STGDataSet.DataSetName = "PMO006STGDataSet";
            this.pMO006STGDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // promotionTableAdapter
            // 
            this.promotionTableAdapter.ClearBeforeFill = true;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "promotionID";
            this.Column1.HeaderText = "PromotionID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "promotionName";
            this.Column2.HeaderText = "Promotion Name";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 120;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "promotionDescription";
            this.Column3.HeaderText = "Promotion Type";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 120;
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "brandName";
            this.Column8.HeaderText = "Brand Name";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "baseProductID";
            this.Column4.HeaderText = "BaseProductID";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "PromotionCost";
            this.Column5.HeaderText = "Promotion Cost";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 120;
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "promotionBudget";
            this.Column6.HeaderText = "Promotion Budget";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Width = 130;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "published";
            this.Column7.HeaderText = "Published";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.DataPropertyName = "stories";
            this.Column9.HeaderText = "Stories";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.DataPropertyName = "active";
            this.Column10.HeaderText = "Active";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // frmPromotionLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 484);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtPromName);
            this.Controls.Add(this.labelPromName);
            this.Controls.Add(this.txtPromID);
            this.Controls.Add(this.labelPromID);
            this.Name = "frmPromotionLookup";
            this.Text = "Promotion Finder";
            this.Load += new System.EventHandler(this.frmPromotionLookup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.promotionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPromID;
        private System.Windows.Forms.TextBox txtPromID;
        private System.Windows.Forms.Label labelPromName;
        private System.Windows.Forms.TextBox txtPromName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.BindingSource promotionBindingSource;
        private PMO006STGDataSet pMO006STGDataSet;
        private SQSAdmin.PMO006STGDataSetTableAdapters.promotionTableAdapter promotionTableAdapter;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
    }
}