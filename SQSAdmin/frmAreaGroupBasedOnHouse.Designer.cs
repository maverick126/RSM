namespace SQSAdmin
{
    partial class frmAreaGroupBasedOnHouse
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnTurnOn = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.lstHome = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.dropdownState = new System.Windows.Forms.ComboBox();
            this.labelAreaName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Please select house and turn on/off";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(459, 482);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnTurnOn
            // 
            this.btnTurnOn.Location = new System.Drawing.Point(364, 482);
            this.btnTurnOn.Name = "btnTurnOn";
            this.btnTurnOn.Size = new System.Drawing.Size(85, 23);
            this.btnTurnOn.TabIndex = 7;
            this.btnTurnOn.Text = "Save Change";
            this.btnTurnOn.UseVisualStyleBackColor = true;
            this.btnTurnOn.Click += new System.EventHandler(this.btnTurnOn_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(32, 442);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAll.TabIndex = 8;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // lstHome
            // 
            this.lstHome.CheckBoxes = true;
            this.lstHome.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lstHome.GridLines = true;
            this.lstHome.Location = new System.Drawing.Point(32, 95);
            this.lstHome.Name = "lstHome";
            this.lstHome.Size = new System.Drawing.Size(502, 331);
            this.lstHome.TabIndex = 9;
            this.lstHome.UseCompatibleStateImageBehavior = false;
            this.lstHome.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Home Model";
            this.columnHeader1.Width = 497;
            // 
            // dropdownState
            // 
            this.dropdownState.FormattingEnabled = true;
            this.dropdownState.Location = new System.Drawing.Point(79, 63);
            this.dropdownState.Name = "dropdownState";
            this.dropdownState.Size = new System.Drawing.Size(121, 21);
            this.dropdownState.TabIndex = 13;
            this.dropdownState.SelectionChangeCommitted += new System.EventHandler(this.dropdownState_SelectionChangeCommitted);
            // 
            // labelAreaName
            // 
            this.labelAreaName.AutoSize = true;
            this.labelAreaName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAreaName.Location = new System.Drawing.Point(34, 40);
            this.labelAreaName.Name = "labelAreaName";
            this.labelAreaName.Size = new System.Drawing.Size(0, 15);
            this.labelAreaName.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "State";
            // 
            // frmAreaGroupBasedOnHouse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 525);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dropdownState);
            this.Controls.Add(this.labelAreaName);
            this.Controls.Add(this.lstHome);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.btnTurnOn);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "frmAreaGroupBasedOnHouse";
            this.Text = "Configure Area/Group Based On House";
            this.Load += new System.EventHandler(this.frmAreaGroupBasedOnHouse_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnTurnOn;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.ListView lstHome;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ComboBox dropdownState;
        private System.Windows.Forms.Label labelAreaName;
        private System.Windows.Forms.Label label2;
    }
}