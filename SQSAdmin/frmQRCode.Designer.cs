namespace SQSAdmin
{
    partial class frmQRCode
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
            this.txtEncodeData = new System.Windows.Forms.TextBox();
            this.btngenerate = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnsave = new System.Windows.Forms.Button();
            this.picEncode = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnfind = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.picDecode = new System.Windows.Forms.PictureBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEncode)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDecode)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 643);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ProductID";
            // 
            // txtEncodeData
            // 
            this.txtEncodeData.Location = new System.Drawing.Point(71, 640);
            this.txtEncodeData.Name = "txtEncodeData";
            this.txtEncodeData.Size = new System.Drawing.Size(201, 20);
            this.txtEncodeData.TabIndex = 2;
            // 
            // btngenerate
            // 
            this.btngenerate.Location = new System.Drawing.Point(288, 637);
            this.btngenerate.Name = "btngenerate";
            this.btngenerate.Size = new System.Drawing.Size(116, 23);
            this.btngenerate.TabIndex = 3;
            this.btngenerate.Text = "Generate QR Code";
            this.btngenerate.UseVisualStyleBackColor = true;
            this.btngenerate.Click += new System.EventHandler(this.btngenerate_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(40, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 700);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnsave);
            this.tabPage1.Controls.Add(this.picEncode);
            this.tabPage1.Controls.Add(this.btngenerate);
            this.tabPage1.Controls.Add(this.txtEncodeData);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 674);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Generate QR Code";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnsave
            // 
            this.btnsave.Location = new System.Drawing.Point(410, 637);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(75, 23);
            this.btnsave.TabIndex = 4;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // picEncode
            // 
            this.picEncode.Location = new System.Drawing.Point(20, 6);
            this.picEncode.Name = "picEncode";
            this.picEncode.Size = new System.Drawing.Size(670, 625);
            this.picEncode.TabIndex = 0;
            this.picEncode.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnfind);
            this.tabPage2.Controls.Add(this.btnOpen);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.picDecode);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 674);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Find Product";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnfind
            // 
            this.btnfind.Location = new System.Drawing.Point(543, 641);
            this.btnfind.Name = "btnfind";
            this.btnfind.Size = new System.Drawing.Size(75, 23);
            this.btnfind.TabIndex = 4;
            this.btnfind.Text = "Find Product";
            this.btnfind.UseVisualStyleBackColor = true;
            this.btnfind.Click += new System.EventHandler(this.btnfind_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(452, 642);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 3;
            this.btnOpen.Text = "Open File";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 651);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ProductID";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(99, 648);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(332, 20);
            this.textBox2.TabIndex = 1;
            // 
            // picDecode
            // 
            this.picDecode.Location = new System.Drawing.Point(18, 6);
            this.picDecode.Name = "picDecode";
            this.picDecode.Size = new System.Drawing.Size(702, 629);
            this.picDecode.TabIndex = 0;
            this.picDecode.TabStop = false;
            // 
            // frmQRCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 724);
            this.Controls.Add(this.tabControl1);
            this.Name = "frmQRCode";
            this.Text = "frmQRCode";
            this.Load += new System.EventHandler(this.frmQRCode_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEncode)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDecode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picEncode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEncodeData;
        private System.Windows.Forms.Button btngenerate;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnfind;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.PictureBox picDecode;
    }
}