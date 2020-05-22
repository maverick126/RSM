namespace SQSAdmin
{
    partial class frmAreaGroup
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnChangeByHouse = new System.Windows.Forms.Button();
            this.btnClearArea = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDoubleSortOrder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAreaCancel = new System.Windows.Forms.Button();
            this.btnSaveNewArea = new System.Windows.Forms.Button();
            this.txtAreaSortOrder = new System.Windows.Forms.TextBox();
            this.labelSortOrder = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.txtNewAreaName = new System.Windows.Forms.TextBox();
            this.labelAreaNameNew = new System.Windows.Forms.Label();
            this.btnNewArea = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSearchArea = new System.Windows.Forms.Button();
            this.txtAreaName = new System.Windows.Forms.TextBox();
            this.labelAreaName = new System.Windows.Forms.Label();
            this.pMO006STGDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pMO006STGDataSet = new SQSAdmin.PMO006STGDataSet();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btnshowallgroup = new System.Windows.Forms.Button();
            this.btnGroupClear = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtStudioMsortorder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGroupCancel = new System.Windows.Forms.Button();
            this.btnSaveNewGroup = new System.Windows.Forms.Button();
            this.txtNewGroupSortOrder = new System.Windows.Forms.TextBox();
            this.labelNewGroupSortOrder = new System.Windows.Forms.Label();
            this.chkActive2 = new System.Windows.Forms.CheckBox();
            this.txtNewGroupName = new System.Windows.Forms.TextBox();
            this.labelnewgroupname = new System.Windows.Forms.Label();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnGroupSearch = new System.Windows.Forms.Button();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.labelGroupName = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtProductID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.button9 = new System.Windows.Forms.Button();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSet)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.btnChangeByHouse);
            this.groupBox1.Controls.Add(this.btnClearArea);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.btnNewArea);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.btnSearchArea);
            this.groupBox1.Controls.Add(this.txtAreaName);
            this.groupBox1.Controls.Add(this.labelAreaName);
            this.groupBox1.Location = new System.Drawing.Point(8, 34);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(468, 754);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Area Management";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(387, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(61, 21);
            this.button1.TabIndex = 8;
            this.button1.Text = "Show All";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnChangeByHouse
            // 
            this.btnChangeByHouse.Location = new System.Drawing.Point(301, 631);
            this.btnChangeByHouse.Name = "btnChangeByHouse";
            this.btnChangeByHouse.Size = new System.Drawing.Size(161, 23);
            this.btnChangeByHouse.TabIndex = 7;
            this.btnChangeByHouse.Text = "Change Area Status By House";
            this.btnChangeByHouse.UseVisualStyleBackColor = true;
            this.btnChangeByHouse.Click += new System.EventHandler(this.btnChangeByHouse_Click);
            // 
            // btnClearArea
            // 
            this.btnClearArea.Location = new System.Drawing.Point(326, 33);
            this.btnClearArea.Name = "btnClearArea";
            this.btnClearArea.Size = new System.Drawing.Size(58, 20);
            this.btnClearArea.TabIndex = 6;
            this.btnClearArea.Text = "Clear";
            this.btnClearArea.UseVisualStyleBackColor = true;
            this.btnClearArea.Click += new System.EventHandler(this.btnClearArea_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtDoubleSortOrder);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnAreaCancel);
            this.panel1.Controls.Add(this.btnSaveNewArea);
            this.panel1.Controls.Add(this.txtAreaSortOrder);
            this.panel1.Controls.Add(this.labelSortOrder);
            this.panel1.Controls.Add(this.chkActive);
            this.panel1.Controls.Add(this.txtNewAreaName);
            this.panel1.Controls.Add(this.labelAreaNameNew);
            this.panel1.Location = new System.Drawing.Point(9, 662);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(453, 86);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // txtDoubleSortOrder
            // 
            this.txtDoubleSortOrder.Location = new System.Drawing.Point(148, 56);
            this.txtDoubleSortOrder.Name = "txtDoubleSortOrder";
            this.txtDoubleSortOrder.Size = new System.Drawing.Size(155, 20);
            this.txtDoubleSortOrder.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Sort Order(Double story)";
            // 
            // btnAreaCancel
            // 
            this.btnAreaCancel.Location = new System.Drawing.Point(394, 52);
            this.btnAreaCancel.Name = "btnAreaCancel";
            this.btnAreaCancel.Size = new System.Drawing.Size(51, 23);
            this.btnAreaCancel.TabIndex = 6;
            this.btnAreaCancel.Text = "Cancel";
            this.btnAreaCancel.UseVisualStyleBackColor = true;
            this.btnAreaCancel.Click += new System.EventHandler(this.btnAreaCancel_Click);
            // 
            // btnSaveNewArea
            // 
            this.btnSaveNewArea.Location = new System.Drawing.Point(333, 52);
            this.btnSaveNewArea.Name = "btnSaveNewArea";
            this.btnSaveNewArea.Size = new System.Drawing.Size(56, 23);
            this.btnSaveNewArea.TabIndex = 5;
            this.btnSaveNewArea.Text = "Save";
            this.btnSaveNewArea.UseVisualStyleBackColor = true;
            this.btnSaveNewArea.Click += new System.EventHandler(this.btnSaveNewArea_Click);
            // 
            // txtAreaSortOrder
            // 
            this.txtAreaSortOrder.Location = new System.Drawing.Point(148, 32);
            this.txtAreaSortOrder.Name = "txtAreaSortOrder";
            this.txtAreaSortOrder.Size = new System.Drawing.Size(155, 20);
            this.txtAreaSortOrder.TabIndex = 4;
            // 
            // labelSortOrder
            // 
            this.labelSortOrder.AutoSize = true;
            this.labelSortOrder.Location = new System.Drawing.Point(20, 35);
            this.labelSortOrder.Name = "labelSortOrder";
            this.labelSortOrder.Size = new System.Drawing.Size(118, 13);
            this.labelSortOrder.TabIndex = 3;
            this.labelSortOrder.Text = "Sort Order (Single stroy)";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(337, 12);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 2;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // txtNewAreaName
            // 
            this.txtNewAreaName.Location = new System.Drawing.Point(148, 9);
            this.txtNewAreaName.Name = "txtNewAreaName";
            this.txtNewAreaName.Size = new System.Drawing.Size(155, 20);
            this.txtNewAreaName.TabIndex = 1;
            // 
            // labelAreaNameNew
            // 
            this.labelAreaNameNew.AutoSize = true;
            this.labelAreaNameNew.Location = new System.Drawing.Point(20, 14);
            this.labelAreaNameNew.Name = "labelAreaNameNew";
            this.labelAreaNameNew.Size = new System.Drawing.Size(60, 13);
            this.labelAreaNameNew.TabIndex = 0;
            this.labelAreaNameNew.Text = "Area Name";
            // 
            // btnNewArea
            // 
            this.btnNewArea.Location = new System.Drawing.Point(11, 632);
            this.btnNewArea.Name = "btnNewArea";
            this.btnNewArea.Size = new System.Drawing.Size(75, 23);
            this.btnNewArea.TabIndex = 4;
            this.btnNewArea.Text = "New Area";
            this.btnNewArea.UseVisualStyleBackColor = true;
            this.btnNewArea.Click += new System.EventHandler(this.btnNewArea_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 83);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(456, 534);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_RowHeaderMouseClick);
            // 
            // btnSearchArea
            // 
            this.btnSearchArea.Location = new System.Drawing.Point(258, 34);
            this.btnSearchArea.Name = "btnSearchArea";
            this.btnSearchArea.Size = new System.Drawing.Size(64, 20);
            this.btnSearchArea.TabIndex = 2;
            this.btnSearchArea.Text = "Search";
            this.btnSearchArea.UseVisualStyleBackColor = true;
            this.btnSearchArea.Click += new System.EventHandler(this.btnSearchArea_Click);
            // 
            // txtAreaName
            // 
            this.txtAreaName.Location = new System.Drawing.Point(73, 34);
            this.txtAreaName.Name = "txtAreaName";
            this.txtAreaName.Size = new System.Drawing.Size(178, 20);
            this.txtAreaName.TabIndex = 1;
            // 
            // labelAreaName
            // 
            this.labelAreaName.AutoSize = true;
            this.labelAreaName.Location = new System.Drawing.Point(11, 39);
            this.labelAreaName.Name = "labelAreaName";
            this.labelAreaName.Size = new System.Drawing.Size(60, 13);
            this.labelAreaName.TabIndex = 0;
            this.labelAreaName.Text = "Area Name";
            // 
            // pMO006STGDataSetBindingSource
            // 
            this.pMO006STGDataSetBindingSource.DataSource = this.pMO006STGDataSet;
            this.pMO006STGDataSetBindingSource.Position = 0;
            // 
            // pMO006STGDataSet
            // 
            this.pMO006STGDataSet.DataSetName = "PMO006STGDataSet";
            this.pMO006STGDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.btnshowallgroup);
            this.groupBox2.Controls.Add(this.btnGroupClear);
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Controls.Add(this.btnNewGroup);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Controls.Add(this.btnGroupSearch);
            this.groupBox2.Controls.Add(this.txtGroupName);
            this.groupBox2.Controls.Add(this.labelGroupName);
            this.groupBox2.Location = new System.Drawing.Point(482, 34);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(458, 754);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Group Management";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(169, 629);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(175, 23);
            this.button2.TabIndex = 8;
            this.button2.Text = "Change Group Status By House";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnshowallgroup
            // 
            this.btnshowallgroup.Location = new System.Drawing.Point(270, 35);
            this.btnshowallgroup.Name = "btnshowallgroup";
            this.btnshowallgroup.Size = new System.Drawing.Size(72, 20);
            this.btnshowallgroup.TabIndex = 7;
            this.btnshowallgroup.Text = "Show All";
            this.btnshowallgroup.UseVisualStyleBackColor = true;
            this.btnshowallgroup.Click += new System.EventHandler(this.btnshowallgroup_Click);
            // 
            // btnGroupClear
            // 
            this.btnGroupClear.Location = new System.Drawing.Point(222, 34);
            this.btnGroupClear.Name = "btnGroupClear";
            this.btnGroupClear.Size = new System.Drawing.Size(43, 20);
            this.btnGroupClear.TabIndex = 6;
            this.btnGroupClear.Text = "Clear";
            this.btnGroupClear.UseVisualStyleBackColor = true;
            this.btnGroupClear.Click += new System.EventHandler(this.btnGroupClear_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtStudioMsortorder);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnGroupCancel);
            this.panel2.Controls.Add(this.btnSaveNewGroup);
            this.panel2.Controls.Add(this.txtNewGroupSortOrder);
            this.panel2.Controls.Add(this.labelNewGroupSortOrder);
            this.panel2.Controls.Add(this.chkActive2);
            this.panel2.Controls.Add(this.txtNewGroupName);
            this.panel2.Controls.Add(this.labelnewgroupname);
            this.panel2.Location = new System.Drawing.Point(10, 663);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(335, 85);
            this.panel2.TabIndex = 5;
            this.panel2.Visible = false;
            // 
            // txtStudioMsortorder
            // 
            this.txtStudioMsortorder.Location = new System.Drawing.Point(126, 59);
            this.txtStudioMsortorder.Name = "txtStudioMsortorder";
            this.txtStudioMsortorder.Size = new System.Drawing.Size(83, 20);
            this.txtStudioMsortorder.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Studio M Sort Order";
            // 
            // btnGroupCancel
            // 
            this.btnGroupCancel.Location = new System.Drawing.Point(276, 57);
            this.btnGroupCancel.Name = "btnGroupCancel";
            this.btnGroupCancel.Size = new System.Drawing.Size(54, 23);
            this.btnGroupCancel.TabIndex = 6;
            this.btnGroupCancel.Text = "Cancel";
            this.btnGroupCancel.UseVisualStyleBackColor = true;
            this.btnGroupCancel.Click += new System.EventHandler(this.btnGroupCancel_Click);
            // 
            // btnSaveNewGroup
            // 
            this.btnSaveNewGroup.Location = new System.Drawing.Point(223, 56);
            this.btnSaveNewGroup.Name = "btnSaveNewGroup";
            this.btnSaveNewGroup.Size = new System.Drawing.Size(46, 23);
            this.btnSaveNewGroup.TabIndex = 5;
            this.btnSaveNewGroup.Text = "Save";
            this.btnSaveNewGroup.UseVisualStyleBackColor = true;
            this.btnSaveNewGroup.Click += new System.EventHandler(this.btnSaveNewGroup_Click);
            // 
            // txtNewGroupSortOrder
            // 
            this.txtNewGroupSortOrder.Location = new System.Drawing.Point(87, 35);
            this.txtNewGroupSortOrder.Name = "txtNewGroupSortOrder";
            this.txtNewGroupSortOrder.Size = new System.Drawing.Size(123, 20);
            this.txtNewGroupSortOrder.TabIndex = 4;
            // 
            // labelNewGroupSortOrder
            // 
            this.labelNewGroupSortOrder.AutoSize = true;
            this.labelNewGroupSortOrder.Location = new System.Drawing.Point(17, 40);
            this.labelNewGroupSortOrder.Name = "labelNewGroupSortOrder";
            this.labelNewGroupSortOrder.Size = new System.Drawing.Size(55, 13);
            this.labelNewGroupSortOrder.TabIndex = 3;
            this.labelNewGroupSortOrder.Text = "Sort Order";
            // 
            // chkActive2
            // 
            this.chkActive2.AutoSize = true;
            this.chkActive2.Checked = true;
            this.chkActive2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive2.Location = new System.Drawing.Point(228, 12);
            this.chkActive2.Name = "chkActive2";
            this.chkActive2.Size = new System.Drawing.Size(56, 17);
            this.chkActive2.TabIndex = 2;
            this.chkActive2.Text = "Active";
            this.chkActive2.UseVisualStyleBackColor = true;
            // 
            // txtNewGroupName
            // 
            this.txtNewGroupName.Location = new System.Drawing.Point(87, 10);
            this.txtNewGroupName.Name = "txtNewGroupName";
            this.txtNewGroupName.Size = new System.Drawing.Size(123, 20);
            this.txtNewGroupName.TabIndex = 1;
            // 
            // labelnewgroupname
            // 
            this.labelnewgroupname.AutoSize = true;
            this.labelnewgroupname.Location = new System.Drawing.Point(18, 15);
            this.labelnewgroupname.Name = "labelnewgroupname";
            this.labelnewgroupname.Size = new System.Drawing.Size(67, 13);
            this.labelnewgroupname.TabIndex = 0;
            this.labelnewgroupname.Text = "Group Name";
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Location = new System.Drawing.Point(12, 630);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(75, 23);
            this.btnNewGroup.TabIndex = 4;
            this.btnNewGroup.Text = "New Group";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 83);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(446, 534);
            this.dataGridView2.TabIndex = 3;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            this.dataGridView2.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_CellMouseClick);
            this.dataGridView2.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView2_RowHeaderMouseClick);
            // 
            // btnGroupSearch
            // 
            this.btnGroupSearch.Location = new System.Drawing.Point(167, 34);
            this.btnGroupSearch.Name = "btnGroupSearch";
            this.btnGroupSearch.Size = new System.Drawing.Size(52, 20);
            this.btnGroupSearch.TabIndex = 2;
            this.btnGroupSearch.Text = "Search";
            this.btnGroupSearch.UseVisualStyleBackColor = true;
            this.btnGroupSearch.Click += new System.EventHandler(this.btnGroupSearch_Click);
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(77, 34);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(87, 20);
            this.txtGroupName.TabIndex = 1;
            // 
            // labelGroupName
            // 
            this.labelGroupName.AutoSize = true;
            this.labelGroupName.Location = new System.Drawing.Point(9, 38);
            this.labelGroupName.Name = "labelGroupName";
            this.labelGroupName.Size = new System.Drawing.Size(67, 13);
            this.labelGroupName.TabIndex = 0;
            this.labelGroupName.Text = "Group Name";
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
            this.groupBox3.Location = new System.Drawing.Point(946, 34);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(428, 754);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Producr Sort Order";
            // 
            // txtProductID
            // 
            this.txtProductID.Location = new System.Drawing.Point(87, 57);
            this.txtProductID.Name = "txtProductID";
            this.txtProductID.Size = new System.Drawing.Size(156, 20);
            this.txtProductID.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Product ID";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(367, 34);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(43, 20);
            this.button5.TabIndex = 6;
            this.button5.Text = "Clear";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(6, 83);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(416, 534);
            this.dataGridView3.TabIndex = 3;
            this.dataGridView3.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellEndEdit);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(312, 34);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(52, 20);
            this.button9.TabIndex = 2;
            this.button9.Text = "Search";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(87, 34);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(156, 20);
            this.txtProductName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Product Name";
            // 
            // frmAreaGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1386, 850);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "frmAreaGroup";
            this.Text = "Configure Areas/Groups";
            this.Load += new System.EventHandler(this.frmAreaGroup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pMO006STGDataSet)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnSearchArea;
        private System.Windows.Forms.TextBox txtAreaName;
        private System.Windows.Forms.Label labelAreaName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelSortOrder;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.TextBox txtNewAreaName;
        private System.Windows.Forms.Label labelAreaNameNew;
        private System.Windows.Forms.Button btnNewArea;
        private System.Windows.Forms.Button btnSaveNewArea;
        private System.Windows.Forms.TextBox txtAreaSortOrder;
        private System.Windows.Forms.Button btnClearArea;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGroupClear;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSaveNewGroup;
        private System.Windows.Forms.TextBox txtNewGroupSortOrder;
        private System.Windows.Forms.Label labelNewGroupSortOrder;
        private System.Windows.Forms.CheckBox chkActive2;
        private System.Windows.Forms.TextBox txtNewGroupName;
        private System.Windows.Forms.Label labelnewgroupname;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnGroupSearch;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Label labelGroupName;
        private System.Windows.Forms.Button btnAreaCancel;
        private System.Windows.Forms.Button btnGroupCancel;
        private System.Windows.Forms.Button btnChangeByHouse;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnshowallgroup;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtProductID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.BindingSource pMO006STGDataSetBindingSource;
        private PMO006STGDataSet pMO006STGDataSet;
        private System.Windows.Forms.TextBox txtDoubleSortOrder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStudioMsortorder;
        private System.Windows.Forms.Label label4;
    }
}