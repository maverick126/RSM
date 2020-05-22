namespace SQSAdmin
{
    partial class frmPromotion
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
            this.labelPromID = new System.Windows.Forms.Label();
            this.txtPromID = new System.Windows.Forms.TextBox();
            this.btnSearchProm = new System.Windows.Forms.Button();
            this.labelPromName = new System.Windows.Forms.Label();
            this.txtPromName = new System.Windows.Forms.TextBox();
            this.labelPromType = new System.Windows.Forms.Label();
            this.dropPromType = new System.Windows.Forms.ComboBox();
            this.txtPromTypeID = new System.Windows.Forms.TextBox();
            this.labelbaseProductID = new System.Windows.Forms.Label();
            this.txtBaseProductID = new System.Windows.Forms.TextBox();
            this.labelBudgetProductID = new System.Windows.Forms.Label();
            this.txtBudgetProductID = new System.Windows.Forms.TextBox();
            this.labelPromCost = new System.Windows.Forms.Label();
            this.txtPromCost = new System.Windows.Forms.TextBox();
            this.labelPromBudget = new System.Windows.Forms.Label();
            this.txtPromBudget = new System.Windows.Forms.TextBox();
            this.labelSortOrder = new System.Windows.Forms.Label();
            this.txtSortOrder = new System.Windows.Forms.TextBox();
            this.labelStories = new System.Windows.Forms.Label();
            this.txtStories = new System.Windows.Forms.TextBox();
            this.labelBrand = new System.Windows.Forms.Label();
            this.dropBrand = new System.Windows.Forms.ComboBox();
            this.labelPublished = new System.Windows.Forms.Label();
            this.chkPublished = new System.Windows.Forms.CheckBox();
            this.labelEfDate = new System.Windows.Forms.Label();
            this.txtEffectiveDate = new System.Windows.Forms.TextBox();
            this.labelActive = new System.Windows.Forms.Label();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnSaveProm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtBrandID = new System.Windows.Forms.TextBox();
            this.btnSelectProduct = new System.Windows.Forms.Button();
            this.btnAddPAG = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelPromID
            // 
            this.labelPromID.AutoSize = true;
            this.labelPromID.Location = new System.Drawing.Point(35, 23);
            this.labelPromID.Name = "labelPromID";
            this.labelPromID.Size = new System.Drawing.Size(65, 13);
            this.labelPromID.TabIndex = 0;
            this.labelPromID.Text = "PromotionID";
            // 
            // txtPromID
            // 
            this.txtPromID.Enabled = false;
            this.txtPromID.Location = new System.Drawing.Point(161, 17);
            this.txtPromID.Name = "txtPromID";
            this.txtPromID.Size = new System.Drawing.Size(100, 20);
            this.txtPromID.TabIndex = 1;
            // 
            // btnSearchProm
            // 
            this.btnSearchProm.Location = new System.Drawing.Point(267, 17);
            this.btnSearchProm.Name = "btnSearchProm";
            this.btnSearchProm.Size = new System.Drawing.Size(33, 20);
            this.btnSearchProm.TabIndex = 2;
            this.btnSearchProm.Text = "...";
            this.btnSearchProm.UseVisualStyleBackColor = true;
            this.btnSearchProm.Click += new System.EventHandler(this.btnSearchProm_Click);
            // 
            // labelPromName
            // 
            this.labelPromName.AutoSize = true;
            this.labelPromName.Location = new System.Drawing.Point(34, 46);
            this.labelPromName.Name = "labelPromName";
            this.labelPromName.Size = new System.Drawing.Size(85, 13);
            this.labelPromName.TabIndex = 3;
            this.labelPromName.Text = "Promotion Name";
            // 
            // txtPromName
            // 
            this.txtPromName.Location = new System.Drawing.Point(161, 41);
            this.txtPromName.Name = "txtPromName";
            this.txtPromName.Size = new System.Drawing.Size(435, 20);
            this.txtPromName.TabIndex = 4;
            // 
            // labelPromType
            // 
            this.labelPromType.AutoSize = true;
            this.labelPromType.Location = new System.Drawing.Point(34, 72);
            this.labelPromType.Name = "labelPromType";
            this.labelPromType.Size = new System.Drawing.Size(81, 13);
            this.labelPromType.TabIndex = 5;
            this.labelPromType.Text = "Promotion Type";
            // 
            // dropPromType
            // 
            this.dropPromType.FormattingEnabled = true;
            this.dropPromType.Location = new System.Drawing.Point(161, 65);
            this.dropPromType.Name = "dropPromType";
            this.dropPromType.Size = new System.Drawing.Size(262, 21);
            this.dropPromType.TabIndex = 6;
            // 
            // txtPromTypeID
            // 
            this.txtPromTypeID.Location = new System.Drawing.Point(496, 17);
            this.txtPromTypeID.Name = "txtPromTypeID";
            this.txtPromTypeID.Size = new System.Drawing.Size(100, 20);
            this.txtPromTypeID.TabIndex = 7;
            this.txtPromTypeID.Visible = false;
            // 
            // labelbaseProductID
            // 
            this.labelbaseProductID.AutoSize = true;
            this.labelbaseProductID.Location = new System.Drawing.Point(35, 95);
            this.labelbaseProductID.Name = "labelbaseProductID";
            this.labelbaseProductID.Size = new System.Drawing.Size(85, 13);
            this.labelbaseProductID.TabIndex = 8;
            this.labelbaseProductID.Text = "Base Product ID";
            // 
            // txtBaseProductID
            // 
            this.txtBaseProductID.Location = new System.Drawing.Point(161, 89);
            this.txtBaseProductID.Name = "txtBaseProductID";
            this.txtBaseProductID.Size = new System.Drawing.Size(262, 20);
            this.txtBaseProductID.TabIndex = 9;
            // 
            // labelBudgetProductID
            // 
            this.labelBudgetProductID.AutoSize = true;
            this.labelBudgetProductID.Location = new System.Drawing.Point(35, 120);
            this.labelBudgetProductID.Name = "labelBudgetProductID";
            this.labelBudgetProductID.Size = new System.Drawing.Size(95, 13);
            this.labelBudgetProductID.TabIndex = 10;
            this.labelBudgetProductID.Text = "Budget Product ID";
            // 
            // txtBudgetProductID
            // 
            this.txtBudgetProductID.Location = new System.Drawing.Point(161, 112);
            this.txtBudgetProductID.Name = "txtBudgetProductID";
            this.txtBudgetProductID.Size = new System.Drawing.Size(262, 20);
            this.txtBudgetProductID.TabIndex = 11;
            // 
            // labelPromCost
            // 
            this.labelPromCost.AutoSize = true;
            this.labelPromCost.Location = new System.Drawing.Point(36, 143);
            this.labelPromCost.Name = "labelPromCost";
            this.labelPromCost.Size = new System.Drawing.Size(78, 13);
            this.labelPromCost.TabIndex = 12;
            this.labelPromCost.Text = "Promotion Cost";
            // 
            // txtPromCost
            // 
            this.txtPromCost.Location = new System.Drawing.Point(161, 135);
            this.txtPromCost.Name = "txtPromCost";
            this.txtPromCost.Size = new System.Drawing.Size(181, 20);
            this.txtPromCost.TabIndex = 13;
            // 
            // labelPromBudget
            // 
            this.labelPromBudget.AutoSize = true;
            this.labelPromBudget.Location = new System.Drawing.Point(37, 165);
            this.labelPromBudget.Name = "labelPromBudget";
            this.labelPromBudget.Size = new System.Drawing.Size(91, 13);
            this.labelPromBudget.TabIndex = 14;
            this.labelPromBudget.Text = "Promotion Budget";
            // 
            // txtPromBudget
            // 
            this.txtPromBudget.AcceptsReturn = true;
            this.txtPromBudget.Location = new System.Drawing.Point(161, 158);
            this.txtPromBudget.Name = "txtPromBudget";
            this.txtPromBudget.Size = new System.Drawing.Size(181, 20);
            this.txtPromBudget.TabIndex = 15;
            // 
            // labelSortOrder
            // 
            this.labelSortOrder.AutoSize = true;
            this.labelSortOrder.Location = new System.Drawing.Point(37, 188);
            this.labelSortOrder.Name = "labelSortOrder";
            this.labelSortOrder.Size = new System.Drawing.Size(55, 13);
            this.labelSortOrder.TabIndex = 16;
            this.labelSortOrder.Text = "Sort Order";
            // 
            // txtSortOrder
            // 
            this.txtSortOrder.Location = new System.Drawing.Point(161, 181);
            this.txtSortOrder.Name = "txtSortOrder";
            this.txtSortOrder.Size = new System.Drawing.Size(181, 20);
            this.txtSortOrder.TabIndex = 17;
            // 
            // labelStories
            // 
            this.labelStories.AutoSize = true;
            this.labelStories.Location = new System.Drawing.Point(37, 209);
            this.labelStories.Name = "labelStories";
            this.labelStories.Size = new System.Drawing.Size(39, 13);
            this.labelStories.TabIndex = 18;
            this.labelStories.Text = "Stories";
            // 
            // txtStories
            // 
            this.txtStories.Location = new System.Drawing.Point(161, 203);
            this.txtStories.Name = "txtStories";
            this.txtStories.Size = new System.Drawing.Size(181, 20);
            this.txtStories.TabIndex = 19;
            // 
            // labelBrand
            // 
            this.labelBrand.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.labelBrand.AutoSize = true;
            this.labelBrand.Location = new System.Drawing.Point(38, 233);
            this.labelBrand.Name = "labelBrand";
            this.labelBrand.Size = new System.Drawing.Size(35, 13);
            this.labelBrand.TabIndex = 20;
            this.labelBrand.Text = "Brand";
            // 
            // dropBrand
            // 
            this.dropBrand.FormattingEnabled = true;
            this.dropBrand.Location = new System.Drawing.Point(161, 225);
            this.dropBrand.Name = "dropBrand";
            this.dropBrand.Size = new System.Drawing.Size(181, 21);
            this.dropBrand.TabIndex = 21;
            // 
            // labelPublished
            // 
            this.labelPublished.AutoSize = true;
            this.labelPublished.Location = new System.Drawing.Point(40, 302);
            this.labelPublished.Name = "labelPublished";
            this.labelPublished.Size = new System.Drawing.Size(53, 13);
            this.labelPublished.TabIndex = 24;
            this.labelPublished.Text = "Published";
            this.labelPublished.Visible = false;
            // 
            // chkPublished
            // 
            this.chkPublished.AutoSize = true;
            this.chkPublished.Location = new System.Drawing.Point(159, 297);
            this.chkPublished.Name = "chkPublished";
            this.chkPublished.Size = new System.Drawing.Size(15, 14);
            this.chkPublished.TabIndex = 25;
            this.chkPublished.UseVisualStyleBackColor = true;
            this.chkPublished.Visible = false;
            this.chkPublished.CheckedChanged += new System.EventHandler(this.chkPublished_CheckedChanged);
            // 
            // labelEfDate
            // 
            this.labelEfDate.AutoSize = true;
            this.labelEfDate.Location = new System.Drawing.Point(39, 321);
            this.labelEfDate.Name = "labelEfDate";
            this.labelEfDate.Size = new System.Drawing.Size(75, 13);
            this.labelEfDate.TabIndex = 26;
            this.labelEfDate.Text = "Effective Date";
            this.labelEfDate.Visible = false;
            // 
            // txtEffectiveDate
            // 
            this.txtEffectiveDate.Location = new System.Drawing.Point(159, 315);
            this.txtEffectiveDate.Name = "txtEffectiveDate";
            this.txtEffectiveDate.Size = new System.Drawing.Size(100, 20);
            this.txtEffectiveDate.TabIndex = 27;
            this.txtEffectiveDate.Visible = false;
            // 
            // labelActive
            // 
            this.labelActive.AutoSize = true;
            this.labelActive.Location = new System.Drawing.Point(40, 280);
            this.labelActive.Name = "labelActive";
            this.labelActive.Size = new System.Drawing.Size(37, 13);
            this.labelActive.TabIndex = 28;
            this.labelActive.Text = "Active";
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(159, 276);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(15, 14);
            this.chkActive.TabIndex = 29;
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(319, 369);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(66, 23);
            this.btnNew.TabIndex = 30;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(390, 369);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(64, 23);
            this.btnCopy.TabIndex = 31;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnSaveProm
            // 
            this.btnSaveProm.Location = new System.Drawing.Point(461, 369);
            this.btnSaveProm.Name = "btnSaveProm";
            this.btnSaveProm.Size = new System.Drawing.Size(71, 23);
            this.btnSaveProm.TabIndex = 32;
            this.btnSaveProm.Text = "Save";
            this.btnSaveProm.UseVisualStyleBackColor = true;
            this.btnSaveProm.Click += new System.EventHandler(this.btnSaveProm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(539, 369);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 33;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtBrandID
            // 
            this.txtBrandID.Location = new System.Drawing.Point(349, 225);
            this.txtBrandID.Name = "txtBrandID";
            this.txtBrandID.Size = new System.Drawing.Size(100, 20);
            this.txtBrandID.TabIndex = 34;
            this.txtBrandID.Visible = false;
            // 
            // btnSelectProduct
            // 
            this.btnSelectProduct.Location = new System.Drawing.Point(428, 89);
            this.btnSelectProduct.Name = "btnSelectProduct";
            this.btnSelectProduct.Size = new System.Drawing.Size(34, 22);
            this.btnSelectProduct.TabIndex = 36;
            this.btnSelectProduct.Text = "...";
            this.btnSelectProduct.UseVisualStyleBackColor = true;
            this.btnSelectProduct.Click += new System.EventHandler(this.btnSelectProduct_Click);
            // 
            // btnAddPAG
            // 
            this.btnAddPAG.Enabled = false;
            this.btnAddPAG.Location = new System.Drawing.Point(319, 398);
            this.btnAddPAG.Name = "btnAddPAG";
            this.btnAddPAG.Size = new System.Drawing.Size(295, 23);
            this.btnAddPAG.TabIndex = 37;
            this.btnAddPAG.Text = "Add PAG to This Promotion";
            this.btnAddPAG.UseVisualStyleBackColor = true;
            this.btnAddPAG.Click += new System.EventHandler(this.btnAddPAG_Click);
            // 
            // frmPromotion
            // 
            this.AccessibleDescription = "  ";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 437);
            this.Controls.Add(this.btnAddPAG);
            this.Controls.Add(this.btnSelectProduct);
            this.Controls.Add(this.txtBrandID);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveProm);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.labelActive);
            this.Controls.Add(this.txtEffectiveDate);
            this.Controls.Add(this.labelEfDate);
            this.Controls.Add(this.chkPublished);
            this.Controls.Add(this.labelPublished);
            this.Controls.Add(this.dropBrand);
            this.Controls.Add(this.labelBrand);
            this.Controls.Add(this.txtStories);
            this.Controls.Add(this.labelStories);
            this.Controls.Add(this.txtSortOrder);
            this.Controls.Add(this.labelSortOrder);
            this.Controls.Add(this.txtPromBudget);
            this.Controls.Add(this.labelPromBudget);
            this.Controls.Add(this.txtPromCost);
            this.Controls.Add(this.labelPromCost);
            this.Controls.Add(this.txtBudgetProductID);
            this.Controls.Add(this.labelBudgetProductID);
            this.Controls.Add(this.txtBaseProductID);
            this.Controls.Add(this.labelbaseProductID);
            this.Controls.Add(this.txtPromTypeID);
            this.Controls.Add(this.dropPromType);
            this.Controls.Add(this.labelPromType);
            this.Controls.Add(this.txtPromName);
            this.Controls.Add(this.labelPromName);
            this.Controls.Add(this.btnSearchProm);
            this.Controls.Add(this.txtPromID);
            this.Controls.Add(this.labelPromID);
            this.Name = "frmPromotion";
            this.Text = "Promotion";
            this.Load += new System.EventHandler(this.frmPromotion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPromID;
        private System.Windows.Forms.TextBox txtPromID;
        private System.Windows.Forms.Button btnSearchProm;
        private System.Windows.Forms.Label labelPromName;
        private System.Windows.Forms.TextBox txtPromName;
        private System.Windows.Forms.Label labelPromType;
        private System.Windows.Forms.ComboBox dropPromType;
        private System.Windows.Forms.TextBox txtPromTypeID;
        private System.Windows.Forms.Label labelbaseProductID;
        private System.Windows.Forms.TextBox txtBaseProductID;
        private System.Windows.Forms.Label labelBudgetProductID;
        private System.Windows.Forms.TextBox txtBudgetProductID;
        private System.Windows.Forms.Label labelPromCost;
        private System.Windows.Forms.TextBox txtPromCost;
        private System.Windows.Forms.Label labelPromBudget;
        private System.Windows.Forms.TextBox txtPromBudget;
        private System.Windows.Forms.Label labelSortOrder;
        private System.Windows.Forms.TextBox txtSortOrder;
        private System.Windows.Forms.Label labelStories;
        private System.Windows.Forms.TextBox txtStories;
        private System.Windows.Forms.Label labelBrand;
        private System.Windows.Forms.ComboBox dropBrand;
        private System.Windows.Forms.Label labelPublished;
        private System.Windows.Forms.CheckBox chkPublished;
        private System.Windows.Forms.Label labelEfDate;
        private System.Windows.Forms.TextBox txtEffectiveDate;
        private System.Windows.Forms.Label labelActive;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnSaveProm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtBrandID;
        private System.Windows.Forms.Button btnSelectProduct;
        private System.Windows.Forms.Button btnAddPAG;
    }
}