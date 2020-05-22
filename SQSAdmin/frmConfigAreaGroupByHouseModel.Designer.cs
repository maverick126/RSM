namespace SQSAdmin
{
    partial class frmConfigAreaGroupByHouseModel
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
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnGroupSearch = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnshowallgroup = new System.Windows.Forms.Button();
            this.btnGroupClear = new System.Windows.Forms.Button();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.labelGroupName = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClearArea = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSearchArea = new System.Windows.Forms.Button();
            this.txtAreaName = new System.Windows.Forms.TextBox();
            this.labelAreaName = new System.Windows.Forms.Label();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.pMO006STGDataSet = new SQSAdmin.PMO006STGDataSet();
            this.pMO006STGDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 100);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(346, 448);
            this.dataGridView2.TabIndex = 3;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            this.dataGridView2.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_RowHeaderMouseClick);
            // 
            // btnGroupSearch
            // 
            this.btnGroupSearch.Location = new System.Drawing.Point(167, 51);
            this.btnGroupSearch.Name = "btnGroupSearch";
            this.btnGroupSearch.Size = new System.Drawing.Size(52, 20);
            this.btnGroupSearch.TabIndex = 2;
            this.btnGroupSearch.Text = "Search";
            this.btnGroupSearch.UseVisualStyleBackColor = true;
            this.btnGroupSearch.Click += new System.EventHandler(this.btnGroupSearch_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnshowallgroup);
            this.groupBox2.Controls.Add(this.btnGroupClear);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.btnGroupSearch);
            this.groupBox2.Controls.Add(this.txtGroupName);
            this.groupBox2.Controls.Add(this.labelGroupName);
            this.groupBox2.Location = new System.Drawing.Point(484, 33);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(358, 563);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Group Management";
            // 
            // btnshowallgroup
            // 
            this.btnshowallgroup.Location = new System.Drawing.Point(270, 52);
            this.btnshowallgroup.Name = "btnshowallgroup";
            this.btnshowallgroup.Size = new System.Drawing.Size(72, 20);
            this.btnshowallgroup.TabIndex = 7;
            this.btnshowallgroup.Text = "Show All";
            this.btnshowallgroup.UseVisualStyleBackColor = true;
            this.btnshowallgroup.Click += new System.EventHandler(this.btnshowallgroup_Click);
            // 
            // btnGroupClear
            // 
            this.btnGroupClear.Location = new System.Drawing.Point(222, 51);
            this.btnGroupClear.Name = "btnGroupClear";
            this.btnGroupClear.Size = new System.Drawing.Size(43, 20);
            this.btnGroupClear.TabIndex = 6;
            this.btnGroupClear.Text = "Clear";
            this.btnGroupClear.UseVisualStyleBackColor = true;
            this.btnGroupClear.Click += new System.EventHandler(this.btnGroupClear_Click);
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(77, 51);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(87, 20);
            this.txtGroupName.TabIndex = 1;
            // 
            // labelGroupName
            // 
            this.labelGroupName.AutoSize = true;
            this.labelGroupName.Location = new System.Drawing.Point(9, 55);
            this.labelGroupName.Name = "labelGroupName";
            this.labelGroupName.Size = new System.Drawing.Size(67, 13);
            this.labelGroupName.TabIndex = 0;
            this.labelGroupName.Text = "Group Name";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(297, 51);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(52, 20);
            this.button9.TabIndex = 2;
            this.button9.Text = "Search";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click_1);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(87, 51);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(156, 20);
            this.txtProductName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Product Name";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(6, 100);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(404, 448);
            this.dataGridView3.TabIndex = 3;
            // 
            // txtProductID
            // 
            this.txtProductID.Location = new System.Drawing.Point(87, 74);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.Size = new System.Drawing.Size(156, 20);
            this.txtProductID.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Product ID";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtProductID);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.button5);
            this.groupBox3.Controls.Add(this.dataGridView3);
            this.groupBox3.Controls.Add(this.button9);
            this.groupBox3.Controls.Add(this.txtProductName);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(848, 33);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(428, 563);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Producr Sort Order";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(352, 51);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(43, 20);
            this.button5.TabIndex = 6;
            this.button5.Text = "Clear";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(387, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 21);
            this.button1.TabIndex = 8;
            this.button1.Text = "Show All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnClearArea);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.btnSearchArea);
            this.groupBox1.Controls.Add(this.txtAreaName);
            this.groupBox1.Controls.Add(this.labelAreaName);
            this.groupBox1.Location = new System.Drawing.Point(10, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 563);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Area Management";
            // 
            // btnClearArea
            // 
            this.btnClearArea.Location = new System.Drawing.Point(326, 50);
            this.btnClearArea.Name = "btnClearArea";
            this.btnClearArea.Size = new System.Drawing.Size(58, 20);
            this.btnClearArea.TabIndex = 6;
            this.btnClearArea.Text = "Clear";
            this.btnClearArea.UseVisualStyleBackColor = true;
            this.btnClearArea.Click += new System.EventHandler(this.btnClearArea_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 100);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(456, 448);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // btnSearchArea
            // 
            this.btnSearchArea.Location = new System.Drawing.Point(258, 51);
            this.btnSearchArea.Name = "btnSearchArea";
            this.btnSearchArea.Size = new System.Drawing.Size(64, 20);
            this.btnSearchArea.TabIndex = 2;
            this.btnSearchArea.Text = "Search";
            this.btnSearchArea.UseVisualStyleBackColor = true;
            this.btnSearchArea.Click += new System.EventHandler(this.btnSearchArea_Click);
            // 
            // txtAreaName
            // 
            this.txtAreaName.Location = new System.Drawing.Point(73, 51);
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(178, 20);
            this.txtAreaName.TabIndex = 1;
            // 
            // labelAreaName
            // 
            this.labelAreaName.AutoSize = true;
            this.labelAreaName.Location = new System.Drawing.Point(11, 56);
            this.labelAreaName.Name = "labelAreaName";
            this.labelAreaName.Size = new System.Drawing.Size(60, 13);
            this.labelAreaName.TabIndex = 0;
            this.labelAreaName.Text = "Area Name";
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToOrderColumns = true;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(16, 638);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(820, 166);
            this.dataGridView4.TabIndex = 9;
            this.dataGridView4.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView4_CellEndEdit);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(14, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 16);
            this.label4.TabIndex = 13;
            // 
            // pMO006STGDataSet
            // 
            this.pMO006STGDataSet.DataSetName = "PMO006STGDataSet";
            this.pMO006STGDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pMO006STGDataSetBindingSource
            // 
            this.pMO006STGDataSetBindingSource.DataSource = this.pMO006STGDataSet;
            this.pMO006STGDataSetBindingSource.Position = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 612);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "PAG details ";
            // 
            // frmConfigAreaGroupByHouseModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 814);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataGridView4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmConfigAreaGroupByHouseModel";
            this.Text = "Config Area Group By House Model";
            this.Load += new System.EventHandler(this.frmConfigAreaGroupByHouseModel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnGroupSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnshowallgroup;
        private System.Windows.Forms.Button btnGroupClear;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label labelGroupName;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label label3;
        private PMO006STGDataSet pMO006STGDataSet;
        private System.Windows.Forms.BindingSource pMO006STGDataSetBindingSource;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.TextBox txtProductID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnClearArea;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSearchArea;
        private System.Windows.Forms.TextBox txtAreaName;
        private System.Windows.Forms.Label labelAreaName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Label label2;
    }
}