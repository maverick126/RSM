using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using SQSAdmin_WpfCustomControlLibrary;
using System.Collections.Generic;
using HtmlRichText;

namespace SQSAdmin
{
    /// <summary>
    /// Summary description for frmDisclaimerManagement.
    /// </summary>
    public class frmDisclaimerManagement : System.Windows.Forms.Form
	{
		private System.Windows.Forms.SaveFileDialog sfd1;
		private System.Windows.Forms.ImageList imgList1;
		private System.Windows.Forms.OpenFileDialog ofd1;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.ColorDialog colorDialog1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private System.ComponentModel.IContainer components;
        private string fileNameDisclaimer;
        DataSet dsState;
        private ComboBox dropdownState;
        private Label label6;
        private Label label7;
        private ComboBox dropRegion;
        private Label label8;
        private Label label9;
        private ComboBox cmbType;
        private Button buttonSearch;
        private ComboBox cmbBrand;
        private ToolBar toolBar1;
        private ToolBarButton tbbSeparator0;
        private ToolBarButton tbbSection1;
        private ToolBarButton tbbSection2;
        private ToolBarButton tbbSection3;
        private ToolBarButton tbbSectionContent;
        private ToolBarButton tbbSpace8;
        private ToolBarButton tbbSpace4;
        private ToolBarButton tbbSpace2;
        private ToolBarButton tbbSpace1;
        private ToolBarButton tbbSeparator1;
        private ToolBarButton tbbLeft;
        private ToolBarButton tbbRight;
        private ToolBarButton tbbCenter;
        private ToolBarButton tbbBold;
        private ToolBarButton tbbItalic;
        private ToolBarButton tbbUnderline;
        private ToolBarButton tbbSeparator4;
        private ToolBarButton tbbStrikeout;
        private ToolBarButton tbbSuperScript;
        private ToolBarButton tbbSubScript;
        private ToolBarButton tbbFont;
        private ToolBarButton tbbColor;
        private ToolBarButton tbbSeparator3;
        private ToolBarButton tbbSeparator5;
        private ToolBarButton tbbSeparator6;
        private Button buttonSave;
        private Label label11;
        private Label label12;
        private Button buttonPublish;
        private DataGridView dataGridViewDisclaimers;
        private Label label10;
        private DateTimePicker dateTimePickerDepositDate;
        private HtmlRichTextBox htmlRichTextBox1;
        private PresentationControls.CheckBoxComboBox checkBoxComboBoxRegion;
        private PresentationControls.CheckBoxComboBox checkBoxComboBoxBrand;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private Label labelStatusMessageToUser;
        private TextBox textBoxInternalNotesChangeTracking;
        private Label label13;
        private WebBrowser webBrowser1;
        private Button buttonPreviewInWebBrowser;
        private Label label14;
        private Label label15;
        private Button buttonViewInDefaultWebBrowser;
        DataSet dsReg;
        enum DISCLAIMER_STATUS
        {
            DRAFT,
            PUBLISHED
        }
        public frmDisclaimerManagement()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

        }


        private void frmDisclaimerManagement_Shown(object sender, EventArgs e)
        {
            labelStatusMessageToUser.Text = "Please wait, loading..";
            Application.DoEvents();

            loadTypeDropDown();

            labelStatusMessageToUser.Text = "Please wait, loading state..";
            Application.DoEvents();
            loadStateDropdown();

            labelStatusMessageToUser.Text = "Please wait, loading regions..";
            Application.DoEvents();
            loadRegionDropdown();

            labelStatusMessageToUser.Text = "Please wait, loading brands..";
            Application.DoEvents();
            loadBrand();

            cmbBrand.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;

            labelStatusMessageToUser.Text = "Please wait, loading disclaimer..";
            Application.DoEvents();
            buttonSearch_Click(null, null);

            labelStatusMessageToUser.Text = "Ready";
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDisclaimerManagement));
            PresentationControls.CheckBoxProperties checkBoxProperties1 = new PresentationControls.CheckBoxProperties();
            PresentationControls.CheckBoxProperties checkBoxProperties2 = new PresentationControls.CheckBoxProperties();
            this.imgList1 = new System.Windows.Forms.ImageList(this.components);
            this.sfd1 = new System.Windows.Forms.SaveFileDialog();
            this.ofd1 = new System.Windows.Forms.OpenFileDialog();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dropdownState = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dropRegion = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.cmbBrand = new System.Windows.Forms.ComboBox();
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbbSeparator0 = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator1 = new System.Windows.Forms.ToolBarButton();
            this.tbbLeft = new System.Windows.Forms.ToolBarButton();
            this.tbbRight = new System.Windows.Forms.ToolBarButton();
            this.tbbCenter = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator3 = new System.Windows.Forms.ToolBarButton();
            this.tbbBold = new System.Windows.Forms.ToolBarButton();
            this.tbbItalic = new System.Windows.Forms.ToolBarButton();
            this.tbbUnderline = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator4 = new System.Windows.Forms.ToolBarButton();
            this.tbbStrikeout = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator5 = new System.Windows.Forms.ToolBarButton();
            this.tbbSuperScript = new System.Windows.Forms.ToolBarButton();
            this.tbbSubScript = new System.Windows.Forms.ToolBarButton();
            this.tbbSeparator6 = new System.Windows.Forms.ToolBarButton();
            this.tbbFont = new System.Windows.Forms.ToolBarButton();
            this.tbbColor = new System.Windows.Forms.ToolBarButton();
            this.tbbSection1 = new System.Windows.Forms.ToolBarButton();
            this.tbbSection2 = new System.Windows.Forms.ToolBarButton();
            this.tbbSection3 = new System.Windows.Forms.ToolBarButton();
            this.tbbSectionContent = new System.Windows.Forms.ToolBarButton();
            this.tbbSpace8 = new System.Windows.Forms.ToolBarButton();
            this.tbbSpace4 = new System.Windows.Forms.ToolBarButton();
            this.tbbSpace2 = new System.Windows.Forms.ToolBarButton();
            this.tbbSpace1 = new System.Windows.Forms.ToolBarButton();
            this.buttonSave = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.buttonPublish = new System.Windows.Forms.Button();
            this.dataGridViewDisclaimers = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.dateTimePickerDepositDate = new System.Windows.Forms.DateTimePicker();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelStatusMessageToUser = new System.Windows.Forms.Label();
            this.textBoxInternalNotesChangeTracking = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.buttonPreviewInWebBrowser = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.checkBoxComboBoxBrand = new PresentationControls.CheckBoxComboBox();
            this.checkBoxComboBoxRegion = new PresentationControls.CheckBoxComboBox();
            this.htmlRichTextBox1 = new HtmlRichText.HtmlRichTextBox();
            this.buttonViewInDefaultWebBrowser = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisclaimers)).BeginInit();
            this.SuspendLayout();
            // 
            // imgList1
            // 
            this.imgList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList1.ImageStream")));
            this.imgList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList1.Images.SetKeyName(0, "");
            this.imgList1.Images.SetKeyName(1, "");
            this.imgList1.Images.SetKeyName(2, "");
            this.imgList1.Images.SetKeyName(3, "");
            this.imgList1.Images.SetKeyName(4, "");
            this.imgList1.Images.SetKeyName(5, "");
            this.imgList1.Images.SetKeyName(6, "");
            this.imgList1.Images.SetKeyName(7, "");
            this.imgList1.Images.SetKeyName(8, "");
            this.imgList1.Images.SetKeyName(9, "");
            this.imgList1.Images.SetKeyName(10, "");
            this.imgList1.Images.SetKeyName(11, "");
            this.imgList1.Images.SetKeyName(12, "");
            this.imgList1.Images.SetKeyName(13, "");
            this.imgList1.Images.SetKeyName(14, "space_8.png");
            this.imgList1.Images.SetKeyName(15, "space_4.png");
            this.imgList1.Images.SetKeyName(16, "space_2.png");
            this.imgList1.Images.SetKeyName(17, "space_1.png");
            // 
            // sfd1
            // 
            this.sfd1.DefaultExt = "htm";
            this.sfd1.Filter = "Html Files|*.htm;*.html|Rich Text Files|*.rtf|Plain Text File|*.txt|All Files|*.*" +
    "";
            this.sfd1.Title = "Save As";
            // 
            // ofd1
            // 
            this.ofd1.DefaultExt = "htm";
            this.ofd1.Filter = "Html Files|*.htm;*.html|Rich Text Files|*.rtf|Plain Text File|*.txt|All Files|*.*" +
    "";
            this.ofd1.Title = "Open File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(696, 20);
            this.label1.TabIndex = 4;
            this.label1.Text = "This function allows user configure the disclaimers for print out in SQS and MRS." +
    "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(272, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "State:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(650, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Region:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Type:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(435, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Brand:";
            // 
            // dropdownState
            // 
            this.dropdownState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropdownState.FormattingEnabled = true;
            this.dropdownState.Location = new System.Drawing.Point(52, 36);
            this.dropdownState.Name = "dropdownState";
            this.dropdownState.Size = new System.Drawing.Size(101, 21);
            this.dropdownState.TabIndex = 0;
            this.dropdownState.SelectedIndexChanged += new System.EventHandler(this.dropdownState_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "State:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(162, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Region:";
            // 
            // dropRegion
            // 
            this.dropRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropRegion.FormattingEnabled = true;
            this.dropRegion.Location = new System.Drawing.Point(213, 36);
            this.dropRegion.Name = "dropRegion";
            this.dropRegion.Size = new System.Drawing.Size(101, 21);
            this.dropRegion.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(322, 38);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Brand:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(472, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Type:";
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(515, 36);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(101, 21);
            this.cmbType.TabIndex = 7;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSearch.Location = new System.Drawing.Point(1127, 36);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(192, 23);
            this.buttonSearch.TabIndex = 9;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // cmbBrand
            // 
            this.cmbBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBrand.FormattingEnabled = true;
            this.cmbBrand.Location = new System.Drawing.Point(364, 36);
            this.cmbBrand.Name = "cmbBrand";
            this.cmbBrand.Size = new System.Drawing.Size(101, 21);
            this.cmbBrand.TabIndex = 5;
            // 
            // toolBar1
            // 
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbbSeparator0,
            this.tbbSeparator1,
            this.tbbLeft,
            this.tbbRight,
            this.tbbCenter,
            this.tbbSeparator3,
            this.tbbBold,
            this.tbbItalic,
            this.tbbUnderline,
            this.tbbSeparator4,
            this.tbbStrikeout,
            this.tbbSeparator5,
            this.tbbSuperScript,
            this.tbbSubScript,
            this.tbbSeparator6,
            this.tbbFont,
            this.tbbColor,
            this.tbbSection1,
            this.tbbSection2,
            this.tbbSection3,
            this.tbbSectionContent,
            this.tbbSpace8,
            this.tbbSpace4,
            this.tbbSpace2,
            this.tbbSpace1});
            this.toolBar1.ButtonSize = new System.Drawing.Size(24, 24);
            this.toolBar1.CausesValidation = false;
            this.toolBar1.Divider = false;
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolBar1.ImageList = this.imgList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Margin = new System.Windows.Forms.Padding(1);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(1329, 26);
            this.toolBar1.TabIndex = 11;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.tb1_ButtonClick);
            // 
            // tbbSeparator0
            // 
            this.tbbSeparator0.Name = "tbbSeparator0";
            this.tbbSeparator0.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbSeparator1
            // 
            this.tbbSeparator1.Name = "tbbSeparator1";
            this.tbbSeparator1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbLeft
            // 
            this.tbbLeft.ImageIndex = 0;
            this.tbbLeft.Name = "tbbLeft";
            this.tbbLeft.ToolTipText = "Left";
            // 
            // tbbRight
            // 
            this.tbbRight.ImageIndex = 1;
            this.tbbRight.Name = "tbbRight";
            this.tbbRight.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbRight.ToolTipText = "Right";
            // 
            // tbbCenter
            // 
            this.tbbCenter.ImageIndex = 2;
            this.tbbCenter.Name = "tbbCenter";
            this.tbbCenter.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbCenter.ToolTipText = "Center";
            // 
            // tbbSeparator3
            // 
            this.tbbSeparator3.Name = "tbbSeparator3";
            this.tbbSeparator3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbBold
            // 
            this.tbbBold.ImageIndex = 3;
            this.tbbBold.Name = "tbbBold";
            this.tbbBold.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbBold.ToolTipText = "Bold";
            // 
            // tbbItalic
            // 
            this.tbbItalic.ImageIndex = 4;
            this.tbbItalic.Name = "tbbItalic";
            this.tbbItalic.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbItalic.ToolTipText = "Italic";
            // 
            // tbbUnderline
            // 
            this.tbbUnderline.ImageIndex = 5;
            this.tbbUnderline.Name = "tbbUnderline";
            this.tbbUnderline.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbUnderline.ToolTipText = "Underline";
            // 
            // tbbSeparator4
            // 
            this.tbbSeparator4.Name = "tbbSeparator4";
            this.tbbSeparator4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbStrikeout
            // 
            this.tbbStrikeout.ImageIndex = 6;
            this.tbbStrikeout.Name = "tbbStrikeout";
            this.tbbStrikeout.ToolTipText = "Strikeout";
            // 
            // tbbSeparator5
            // 
            this.tbbSeparator5.Name = "tbbSeparator5";
            this.tbbSeparator5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbSuperScript
            // 
            this.tbbSuperScript.ImageIndex = 12;
            this.tbbSuperScript.Name = "tbbSuperScript";
            this.tbbSuperScript.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbSuperScript.ToolTipText = "Super";
            // 
            // tbbSubScript
            // 
            this.tbbSubScript.ImageIndex = 13;
            this.tbbSubScript.Name = "tbbSubScript";
            this.tbbSubScript.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.tbbSubScript.ToolTipText = "Sub";
            // 
            // tbbSeparator6
            // 
            this.tbbSeparator6.Name = "tbbSeparator6";
            this.tbbSeparator6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // tbbFont
            // 
            this.tbbFont.ImageIndex = 8;
            this.tbbFont.Name = "tbbFont";
            this.tbbFont.ToolTipText = "Font";
            // 
            // tbbColor
            // 
            this.tbbColor.ImageIndex = 9;
            this.tbbColor.Name = "tbbColor";
            this.tbbColor.ToolTipText = "Color";
            // 
            // tbbSection1
            // 
            this.tbbSection1.ImageIndex = 7;
            this.tbbSection1.Name = "tbbSection1";
            this.tbbSection1.ToolTipText = "Section1";
            // 
            // tbbSection2
            // 
            this.tbbSection2.ImageIndex = 7;
            this.tbbSection2.Name = "tbbSection2";
            this.tbbSection2.ToolTipText = "Section2";
            // 
            // tbbSection3
            // 
            this.tbbSection3.ImageIndex = 7;
            this.tbbSection3.Name = "tbbSection3";
            this.tbbSection3.ToolTipText = "Section3";
            // 
            // tbbSectionContent
            // 
            this.tbbSectionContent.ImageIndex = 1;
            this.tbbSectionContent.Name = "tbbSectionContent";
            this.tbbSectionContent.ToolTipText = "SectionContent";
            // 
            // tbbSpace8
            // 
            this.tbbSpace8.ImageIndex = 14;
            this.tbbSpace8.Name = "tbbSpace8";
            this.tbbSpace8.ToolTipText = "Space8";
            // 
            // tbbSpace4
            // 
            this.tbbSpace4.ImageIndex = 15;
            this.tbbSpace4.Name = "tbbSpace4";
            this.tbbSpace4.ToolTipText = "Space4";
            // 
            // tbbSpace2
            // 
            this.tbbSpace2.ImageIndex = 16;
            this.tbbSpace2.Name = "tbbSpace2";
            this.tbbSpace2.ToolTipText = "Space2";
            // 
            // tbbSpace1
            // 
            this.tbbSpace1.ImageIndex = 17;
            this.tbbSpace1.Name = "tbbSpace1";
            this.tbbSpace1.ToolTipText = "Space1";
            // 
            // buttonSave
            // 
            this.buttonSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSave.Location = new System.Drawing.Point(1127, 702);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(192, 23);
            this.buttonSave.TabIndex = 14;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(897, 706);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(38, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Brand:";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(668, 706);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 13);
            this.label12.TabIndex = 16;
            this.label12.Text = "Region:";
            // 
            // buttonPublish
            // 
            this.buttonPublish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPublish.Location = new System.Drawing.Point(10, 702);
            this.buttonPublish.Name = "buttonPublish";
            this.buttonPublish.Size = new System.Drawing.Size(102, 23);
            this.buttonPublish.TabIndex = 22;
            this.buttonPublish.Text = "Publish";
            this.buttonPublish.UseVisualStyleBackColor = true;
            this.buttonPublish.Click += new System.EventHandler(this.buttonPublish_Click);
            // 
            // dataGridViewDisclaimers
            // 
            this.dataGridViewDisclaimers.AllowUserToAddRows = false;
            this.dataGridViewDisclaimers.AllowUserToDeleteRows = false;
            this.dataGridViewDisclaimers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewDisclaimers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDisclaimers.Location = new System.Drawing.Point(10, 65);
            this.dataGridViewDisclaimers.Name = "dataGridViewDisclaimers";
            this.dataGridViewDisclaimers.ReadOnly = true;
            this.dataGridViewDisclaimers.RowTemplate.Height = 24;
            this.dataGridViewDisclaimers.Size = new System.Drawing.Size(1309, 179);
            this.dataGridViewDisclaimers.TabIndex = 23;
            this.dataGridViewDisclaimers.SelectionChanged += new System.EventHandler(this.dataGridViewDisclaimers_SelectionChanged);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(331, 707);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Disclaimer Effective Date:";
            // 
            // dateTimePickerDepositDate
            // 
            this.dateTimePickerDepositDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePickerDepositDate.Location = new System.Drawing.Point(464, 705);
            this.dateTimePickerDepositDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dateTimePickerDepositDate.Name = "dateTimePickerDepositDate";
            this.dateTimePickerDepositDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerDepositDate.TabIndex = 24;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Disclaimer Revision Number";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 300;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Deposit Date";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 300;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Status";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 300;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Last Modified Date";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 300;
            // 
            // labelStatusMessageToUser
            // 
            this.labelStatusMessageToUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatusMessageToUser.AutoSize = true;
            this.labelStatusMessageToUser.Location = new System.Drawing.Point(12, 682);
            this.labelStatusMessageToUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStatusMessageToUser.Name = "labelStatusMessageToUser";
            this.labelStatusMessageToUser.Size = new System.Drawing.Size(35, 13);
            this.labelStatusMessageToUser.TabIndex = 29;
            this.labelStatusMessageToUser.Text = "status";
            // 
            // textBoxInternalNotesChangeTracking
            // 
            this.textBoxInternalNotesChangeTracking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxInternalNotesChangeTracking.Location = new System.Drawing.Point(10, 626);
            this.textBoxInternalNotesChangeTracking.Multiline = true;
            this.textBoxInternalNotesChangeTracking.Name = "textBoxInternalNotesChangeTracking";
            this.textBoxInternalNotesChangeTracking.Size = new System.Drawing.Size(1307, 47);
            this.textBoxInternalNotesChangeTracking.TabIndex = 33;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 607);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(233, 13);
            this.label13.TabIndex = 32;
            this.label13.Text = "Internal Notes: (Change Tracking Purpose Only)";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(10, 470);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1309, 134);
            this.webBrowser1.TabIndex = 34;
            // 
            // buttonPreviewInWebBrowser
            // 
            this.buttonPreviewInWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPreviewInWebBrowser.Location = new System.Drawing.Point(976, 445);
            this.buttonPreviewInWebBrowser.Name = "buttonPreviewInWebBrowser";
            this.buttonPreviewInWebBrowser.Size = new System.Drawing.Size(171, 21);
            this.buttonPreviewInWebBrowser.TabIndex = 35;
            this.buttonPreviewInWebBrowser.Text = "Preview";
            this.buttonPreviewInWebBrowser.UseVisualStyleBackColor = true;
            this.buttonPreviewInWebBrowser.Click += new System.EventHandler(this.buttonPreviewInWebBrowser_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 251);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(164, 13);
            this.label14.TabIndex = 36;
            this.label14.Text = "Disclaimer Template Change Pad";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 453);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(171, 13);
            this.label15.TabIndex = 37;
            this.label15.Text = "Disclaimer Template Preview Pane";
            // 
            // checkBoxComboBoxBrand
            // 
            this.checkBoxComboBoxBrand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            checkBoxProperties1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxComboBoxBrand.CheckBoxProperties = checkBoxProperties1;
            this.checkBoxComboBoxBrand.DisplayMemberSingleItem = "";
            this.checkBoxComboBoxBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.checkBoxComboBoxBrand.FormattingEnabled = true;
            this.checkBoxComboBoxBrand.Location = new System.Drawing.Point(939, 705);
            this.checkBoxComboBoxBrand.Name = "checkBoxComboBoxBrand";
            this.checkBoxComboBoxBrand.Size = new System.Drawing.Size(182, 21);
            this.checkBoxComboBoxBrand.TabIndex = 28;
            // 
            // checkBoxComboBoxRegion
            // 
            this.checkBoxComboBoxRegion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            checkBoxProperties2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBoxComboBoxRegion.CheckBoxProperties = checkBoxProperties2;
            this.checkBoxComboBoxRegion.DisplayMemberSingleItem = "";
            this.checkBoxComboBoxRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.checkBoxComboBoxRegion.FormattingEnabled = true;
            this.checkBoxComboBoxRegion.Location = new System.Drawing.Point(716, 705);
            this.checkBoxComboBoxRegion.Name = "checkBoxComboBoxRegion";
            this.checkBoxComboBoxRegion.Size = new System.Drawing.Size(176, 21);
            this.checkBoxComboBoxRegion.TabIndex = 27;
            // 
            // htmlRichTextBox1
            // 
            this.htmlRichTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.htmlRichTextBox1.Location = new System.Drawing.Point(10, 267);
            this.htmlRichTextBox1.Name = "htmlRichTextBox1";
            this.htmlRichTextBox1.Size = new System.Drawing.Size(1310, 175);
            this.htmlRichTextBox1.TabIndex = 26;
            this.htmlRichTextBox1.Text = "";
            // 
            // buttonViewInDefaultWebBrowser
            // 
            this.buttonViewInDefaultWebBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonViewInDefaultWebBrowser.Location = new System.Drawing.Point(1150, 445);
            this.buttonViewInDefaultWebBrowser.Name = "buttonViewInDefaultWebBrowser";
            this.buttonViewInDefaultWebBrowser.Size = new System.Drawing.Size(171, 21);
            this.buttonViewInDefaultWebBrowser.TabIndex = 38;
            this.buttonViewInDefaultWebBrowser.Text = "Show in Web Browser";
            this.buttonViewInDefaultWebBrowser.UseVisualStyleBackColor = true;
            this.buttonViewInDefaultWebBrowser.Click += new System.EventHandler(this.buttonViewInDefaultWebBrowser_Click);
            // 
            // frmDisclaimerManagement
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1329, 736);
            this.Controls.Add(this.buttonViewInDefaultWebBrowser);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.buttonPreviewInWebBrowser);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.textBoxInternalNotesChangeTracking);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.labelStatusMessageToUser);
            this.Controls.Add(this.checkBoxComboBoxBrand);
            this.Controls.Add(this.checkBoxComboBoxRegion);
            this.Controls.Add(this.htmlRichTextBox1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.dateTimePickerDepositDate);
            this.Controls.Add(this.dataGridViewDisclaimers);
            this.Controls.Add(this.buttonPublish);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.toolBar1);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbBrand);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.dropRegion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dropdownState);
            this.Name = "frmDisclaimerManagement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " ";
            this.Shown += new System.EventHandler(this.frmDisclaimerManagement_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDisclaimers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void tb1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			//Switch based on the tooltip of the button pressed
			//OR: This could be changed to switch on the actual button pressed (e.g. e.Button and the case would be tbbBold)
			switch(e.Button.ToolTipText)
			{
				case "Bold":
				{
					if (htmlRichTextBox1.SelectionFont != null) 
					{
						htmlRichTextBox1.SelectionFont = new Font(htmlRichTextBox1.SelectionFont, htmlRichTextBox1.SelectionFont.Style ^ FontStyle.Bold);
					}
                    
				}
					break;

				case "Italic":
				{
					if (htmlRichTextBox1.SelectionFont != null) 
					{
						htmlRichTextBox1.SelectionFont = new Font(htmlRichTextBox1.SelectionFont, htmlRichTextBox1.SelectionFont.Style ^ FontStyle.Italic);
					}
				}
					break;

				case "Underline":
				{
					if (htmlRichTextBox1.SelectionFont != null) 
					{
						htmlRichTextBox1.SelectionFont = new Font(htmlRichTextBox1.SelectionFont, htmlRichTextBox1.SelectionFont.Style ^ FontStyle.Underline);
					}
				}
					break;

				case "Strikeout":
				{
					if (htmlRichTextBox1.SelectionFont != null) 
					{
						htmlRichTextBox1.SelectionFont = new Font(htmlRichTextBox1.SelectionFont, htmlRichTextBox1.SelectionFont.Style ^ FontStyle.Strikeout);
					}
				}
					break;

				case "Super":
				{
					if (tbbSuperScript.Pushed)
					{
						htmlRichTextBox1.SetSuperScript(true);
						htmlRichTextBox1.SetSubScript(false);
					}
					else
					{
						htmlRichTextBox1.SetSuperScript(false);
					}
				}
					break;

				case "Sub":
				{
					if (tbbSubScript.Pushed)
					{
						htmlRichTextBox1.SetSubScript(true);
						htmlRichTextBox1.SetSuperScript(false);
					}
					else
					{
						htmlRichTextBox1.SetSubScript(false);
					}
				}
					break;
				case "Left":
				{
					//change horizontal alignment to left
					htmlRichTextBox1.SelectionAlignment = HorizontalAlignment.Left; 
				}
					break;

				case "Right":
				{
					//change horizontal alignment to right
					htmlRichTextBox1.SelectionAlignment = HorizontalAlignment.Right;
				}
					break;

				case "Center":
				{
					//change horizontal alignment to center
					htmlRichTextBox1.SelectionAlignment = HorizontalAlignment.Center;
				}
					break;

				case "Open":
				{
					try
					{
						if ((ofd1.ShowDialog() == DialogResult.OK) && (ofd1.FileName.Length > 0))
						{
							string strExt = System.IO.Path.GetExtension(ofd1.FileName).ToLower();

							if (strExt == ".rtf")
							{
								htmlRichTextBox1.LoadFile(ofd1.FileName, RichTextBoxStreamType.RichText);
							}
							else if (strExt == ".txt") 
							{
								htmlRichTextBox1.LoadFile(ofd1.FileName, RichTextBoxStreamType.PlainText);
							}
							else if ((strExt == ".htm") || (strExt == ".html")) 
							{
                                    // Read the HTML format
                                fileNameDisclaimer = ofd1.FileName;
                                StreamReader sr = File.OpenText(fileNameDisclaimer);
								string strHTML = sr.ReadToEnd();
								sr.Close();

								htmlRichTextBox1.AddHTML(strHTML);
							}
						}
					}
					catch
					{
						MessageBox.Show("There was an error loading the file: " + ofd1.FileName);
					}
				}
					break;

				case "Save":
				{
					if ((sfd1.ShowDialog() == DialogResult.OK) && (sfd1.FileName.Length > 0))
					{
						string strExt = System.IO.Path.GetExtension(sfd1.FileName).ToLower();

						if (strExt == ".rtf")
						{
							htmlRichTextBox1.SaveFile(sfd1.FileName);
						}
						else if (strExt == ".txt") 
						{
							htmlRichTextBox1.SaveFile(sfd1.FileName, RichTextBoxStreamType.PlainText);
						}
						else if ((strExt == ".htm") || (strExt == ".html")) 
						{
							try
							{
								// save as HTML format
								string strText = htmlRichTextBox1.GetHTML(true, true);

								if (File.Exists(sfd1.FileName)) 
									File.Delete(sfd1.FileName);

								StreamWriter sr = File.CreateText(sfd1.FileName);
								sr.Write(strText);
								sr.Close();
							}
							catch
							{
								MessageBox.Show("There was an error saving the file: " + sfd1.FileName);
							}
						}
					}
				}
					break;

				case "Font":
				{
					if (htmlRichTextBox1.SelectionFont != null)
						fontDialog1.Font = htmlRichTextBox1.SelectionFont;
					else
						fontDialog1.Font = htmlRichTextBox1.Font;

					if (fontDialog1.ShowDialog() == DialogResult.OK)
					{
						if (htmlRichTextBox1.SelectionFont != null)
							htmlRichTextBox1.SelectionFont = fontDialog1.Font;
						else
							htmlRichTextBox1.Font = fontDialog1.Font;
					}
				}
				break;

				case "Color":
				    {
					    colorDialog1.Color = htmlRichTextBox1.SelectionColor;

					    if (colorDialog1.ShowDialog() == DialogResult.OK)
					    {
						    htmlRichTextBox1.SelectionColor = colorDialog1.Color;
					    }
				    }
				    break;
                case "Section1":
                    {
                        string selText = htmlRichTextBox1.SelectedText;
                        if (selText.Contains("<SECTION1>"))
                            selText = selText.Replace("<SECTION1>", string.Empty).Replace("</SECTION_END>", string.Empty);
                        else
                            selText = "<SECTION1>" + selText + "</SECTION_END>";
                        Clipboard.SetText(selText);
                        htmlRichTextBox1.Paste();
                    }
                    break;
                case "Section2":
                    {
                        string selText = htmlRichTextBox1.SelectedText;
                        if (selText.Contains("<SECTION2>"))
                            selText = selText.Replace("<SECTION2>", string.Empty).Replace("</SECTION_END>", string.Empty);
                        else
                            selText = "<SECTION2>" + selText + "</SECTION_END>";
                        Clipboard.SetText(selText);
                        htmlRichTextBox1.Paste();
                    }
                    break;
                case "Section3":
                    {
                        string selText = htmlRichTextBox1.SelectedText;
                        if (selText.Contains("<SECTION3>"))
                            selText = selText.Replace("<SECTION3>", string.Empty).Replace("</SECTION_END>", string.Empty);
                        else
                            selText = "<SECTION3>" + selText + "</SECTION_END>";
                        Clipboard.SetText(selText);
                        htmlRichTextBox1.Paste();
                    }
                    break;
                case "SectionContent":
                    {
                        string selText = htmlRichTextBox1.SelectedText;
                        if (selText.Contains("<TEXT>"))
                            selText = selText.Replace("<TEXT>", string.Empty).Replace("</TEXT>", string.Empty);
                        else
                            selText = "<TEXT>" + selText + "</TEXT>";
                        Clipboard.SetText(selText);
                        htmlRichTextBox1.Paste();
                    }
                    break;
                case "Space8":
                    {
                        string selText = htmlRichTextBox1.SelectedText;
                        if (selText.Contains("<SPACE8>"))
                            selText = selText.Replace("<SPACE8>", string.Empty);
                        else
                            selText = "<SPACE8>" + selText;
                        Clipboard.SetText(selText);
                        htmlRichTextBox1.Paste();
                    }
                    break;
                case "Space4":
                    {
                        string selText = htmlRichTextBox1.SelectedText;
                        if (selText.Contains("<SPACE4>"))
                            selText = selText.Replace("<SPACE4>", string.Empty);
                        else
                            selText = "<SPACE4>" + selText;
                        Clipboard.SetText(selText);
                        htmlRichTextBox1.Paste();
                    }
                    break;
                case "Space2":
                    {
                        string selText = htmlRichTextBox1.SelectedText;
                        if (selText.Contains("<SPACE2>"))
                            selText = selText.Replace("<SPACE2>", string.Empty);
                        else
                            selText = "<SPACE2>" + selText;
                        if (string.IsNullOrWhiteSpace(selText))
                        {
                            selText = " ";
                        }
                        Clipboard.SetText(selText);
                        htmlRichTextBox1.Paste();
                    }
                    break;
                case "Space1":
                    {
                        string selText = htmlRichTextBox1.SelectedText;
                        if (selText.Contains("<SPACE1>"))
                            selText = selText.Replace("<SPACE1>", string.Empty);
                        else
                            selText = "<SPACE1>" + selText;
                        Clipboard.SetText(selText);
                        htmlRichTextBox1.Paste();
                    }
                    break;
            }
			
			UpdateToolbar(); //Update the toolbar buttons
		}

		private void htmlRichTextBox1_SelectionChanged(object sender, System.EventArgs e)
		{
			if (!htmlRichTextBox1.InternalUpdating)
				UpdateToolbar(); //Update the toolbar buttons
		}

		/// <summary>
		///     Update the toolbar button statuses
		/// </summary>
		public void UpdateToolbar()
		{
			//This is done incase 2 different fonts are selected at the same time
			//If that is the case there is no selection font so I use the default
			//font instead.
			Font fnt;
			
			if (htmlRichTextBox1.SelectionFont != null)
				fnt = htmlRichTextBox1.SelectionFont;
			else
				fnt = htmlRichTextBox1.Font;

            //Do all the toolbar button checks
            tbbBold.Pushed = fnt.Bold; //bold button
            tbbItalic.Pushed = fnt.Italic; //italic button
            tbbUnderline.Pushed = fnt.Underline; //underline button
            tbbStrikeout.Pushed = fnt.Strikeout; //strikeout button
            tbbLeft.Pushed = (htmlRichTextBox1.SelectionAlignment == HorizontalAlignment.Left); //justify left
            tbbCenter.Pushed = (htmlRichTextBox1.SelectionAlignment == HorizontalAlignment.Center); //justify center
            tbbRight.Pushed = (htmlRichTextBox1.SelectionAlignment == HorizontalAlignment.Right); //justify right

            tbbSuperScript.Pushed = htmlRichTextBox1.IsSuperScript();
            tbbSubScript.Pushed = htmlRichTextBox1.IsSubScript();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            SearchDisclaimers();
        }

        private void loadTypeDropDown()
        {
            // to add 
            // cmbType.Items.Add(new KeyValuePair<string, string>("1", "SQS Estimate"));
            // cmbType.Items.Add(new KeyValuePair<string, string>("2", "SQS Order Form"));
            // cmbType.Items.Add(new KeyValuePair<string, string>("3", "MRS Contract"));
            // cmbType.Items.Add(new KeyValuePair<string, string>("4", "MRS PC"));
            // cmbType.Items.Add(new KeyValuePair<string, string>("5", "MRS Variation"));
            // cmbType.Items.Add(new KeyValuePair<string, string>("6", "Price List"));
            SQSAdminWCFService.SQSAdminServiceClient client = new SQSAdminWCFService.SQSAdminServiceClient();
            client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
            DataSet dsDisclaimerTypes = client.SQSAdmin_Generic_GetSQSConfiguration("DisclaimerType", "");
            cmbType.DataSource = dsDisclaimerTypes.Tables[0];
            cmbType.DisplayMember = "codeText";
            cmbType.ValueMember = "codeValue";

            cmbType.SelectedIndex = -1;           
        }

        private void loadStateDropdown()
        {
            dsState = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetState]", new System.Data.SqlClient.SqlParameter[0] { });
            dropdownState.DataSource = dsState.Tables[0];
            dropdownState.DisplayMember = "stateAbbreviation";
            dropdownState.ValueMember = "stateID";
            int indexSelected = dropdownState.FindString(MetriconCommon.UserStateName);
            if (indexSelected >= 0)
            {
                dropdownState.SelectedIndex = indexSelected;
            }
        }

        private void loadRegionDropdown()
        {

            dsReg = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spa_AdminGetRegion]", new System.Data.SqlClient.SqlParameter[1]
                        {
                               new System.Data.SqlClient.SqlParameter("@stateID", dropdownState.SelectedValue.ToString() )
                        });
            if (dsReg.Tables[0].Rows.Count > 0)
            {
                DataRow row = dsReg.Tables[0].NewRow();
                row["regionID"] = -1;
                row["regionName"] = "Show All";
                dsReg.Tables[0].Rows.InsertAt(row, 0);
                row = dsReg.Tables[0].NewRow();
                row["regionID"] = 0;
                row["regionName"] = "Default";
                dsReg.Tables[0].Rows.InsertAt(row, 1);
                dropRegion.DataSource = dsReg.Tables[0];
                dropRegion.DisplayMember = "regionName";
                dropRegion.ValueMember = "regionID";
                dropRegion.Text = dsReg.Tables[0].Rows[0][1].ToString();
                //if (label9.Text.Trim() != "")
                //    dropRegion.SelectedValue = label9.Text;
                //else
                //    dropRegion.SelectedIndex = 0;
                //checkBoxComboBoxRegion.DataSource = dsReg.Tables[0];
                //checkBoxComboBoxRegion.DisplayMember = "regionName";
                //checkBoxComboBoxRegion.ValueMember = "regionID";
                ComboboxItem item;
                checkBoxComboBoxRegion.Items.Clear();
                foreach (DataRow rowItem in dsReg.Tables[0].Rows)
                {
                    if (rowItem["regionID"].ToString() == "-1")
                        continue;
                    item = new ComboboxItem();
                    item.Text = rowItem["regionName"].ToString();
                    item.Value = rowItem["regionID"];
                    checkBoxComboBoxRegion.Items.Add(item);
                }
                item = new ComboboxItem();
                item.Text = "Select All";
                item.Value = "-1";
                checkBoxComboBoxRegion.Items.Insert(1, item);
                checkBoxComboBoxRegion.SelectedIndex = -1;
            }
            else
            {
                dropRegion.DataSource = null;
                dropRegion.Items.Clear();

                checkBoxComboBoxRegion.Items.Clear();
            }

        }

        private void loadBrand()
        {

            DataSet dsTemp = MetriconCommon.DatabaseManager.ExecuteSQLQuery("[spw_GetBrandByState]", new System.Data.SqlClient.SqlParameter[1]
                            {
                                new System.Data.SqlClient.SqlParameter("@stateID", dropdownState.SelectedValue.ToString())
                            }

            );

            DataRow row = dsTemp.Tables[0].NewRow();
            row["BrandID"] = -1;
            row["BrandName"] = "Show All";
            dsTemp.Tables[0].Rows.InsertAt(row, 0);
            row = dsTemp.Tables[0].NewRow();
            row["BrandID"] = 0;
            row["BrandName"] = "Default";
            dsTemp.Tables[0].Rows.InsertAt(row, 1);
            cmbBrand.DataSource = dsTemp.Tables[0];
            cmbBrand.DisplayMember = "BrandName";
            cmbBrand.ValueMember = "BrandID";

            //checkBoxComboBoxRegion.DataSource = dsTemp.Tables[0];
            //checkBoxComboBoxRegion.DisplayMember = "BrandName";
            //checkBoxComboBoxRegion.ValueMember = "brandid";
            //checkBoxComboBoxRegion.SelectedValue = "0";

            checkBoxComboBoxBrand.Items.Clear();
            ComboboxItem item;
            foreach (DataRow rowItem in dsTemp.Tables[0].Rows)
            {
                if (rowItem["brandid"].ToString() == "-1")
                    continue;
                item = new ComboboxItem();
                item.Text = rowItem["BrandName"].ToString();
                item.Value = rowItem["brandid"];
                checkBoxComboBoxBrand.Items.Add(item);
            }
            item = new ComboboxItem();
            item.Text = "Select All";
            item.Value = "-1";
            checkBoxComboBoxBrand.Items.Insert(1, item);
            checkBoxComboBoxBrand.SelectedIndex = -1;
        }

        private void dropdownState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(dropdownState.SelectedValue.ToString()) && dropdownState.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                loadRegionDropdown();
                loadBrand();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            DisclaimerSave(DISCLAIMER_STATUS.DRAFT);
        }

        private void buttonPublish_Click(object sender, EventArgs e)
        {
            DisclaimerSave(DISCLAIMER_STATUS.PUBLISHED);
        }

        private void DisclaimerSave(DISCLAIMER_STATUS status)
        {
            try
            {
                //// save as HTML format
                //string strText = htmlRichTextBox1.GetHTML(true, true);

                //if (File.Exists(fileNameDisclaimer))
                //    File.Delete(fileNameDisclaimer);

                //StreamWriter sr = File.CreateText(fileNameDisclaimer);
                //sr.Write(strText);
                //sr.Close();

                //StreamReader sr2 = File.OpenText(fileNameDisclaimer);
                //string strHTML = sr2.ReadToEnd();
                //sr.Close();

                //htmlRichTextBox1.Text = string.Empty;
                //htmlRichTextBox1.AddHTML(strHTML);
                string type = cmbType.SelectedValue.ToString();
                string state = dropdownState.Text;
                string regionids = string.Empty;
                string brandids = string.Empty;
                string disclaimerText = htmlRichTextBox1.GetHTML(true, true);
                string disclaimerHTML = string.Empty;

                disclaimerText = disclaimerText.Replace("<TEXT>", "<li>").Replace("</TEXT>", "</li>");
                disclaimerText = disclaimerText.Replace("<SECTION1>", "<ol type=1>");
                disclaimerText = disclaimerText.Replace("<SECTION2>", "<ol type=a>");
                disclaimerText = disclaimerText.Replace("<SECTION3>", "<ol type=i>");
                disclaimerText = disclaimerText.Replace("</SECTION_END>", "</ol>");
                disclaimerText = disclaimerText.Replace("<SPACE8>", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                disclaimerText = disclaimerText.Replace("<SPACE4>", "&nbsp;&nbsp;&nbsp;&nbsp;");
                disclaimerText = disclaimerText.Replace("<SPACE2>", "&nbsp;&nbsp;");
                disclaimerText = disclaimerText.Replace("<SPACE1>", "&nbsp;");

                foreach (var itemRegion in checkBoxComboBoxRegion.CheckBoxItems)
                {
                    if (!itemRegion.Checked || (itemRegion.ComboBoxItem.GetType().Name != "ComboboxItem" || ((ComboboxItem)itemRegion.ComboBoxItem).Value == "-1" || ((ComboboxItem)itemRegion.ComboBoxItem).Value == "0"))
                        continue;
                    regionids += ((ComboboxItem)itemRegion.ComboBoxItem).Value.ToString() + ",";
                }
                foreach (var itemBrand in checkBoxComboBoxBrand.CheckBoxItems)
                {
                    if (!itemBrand.Checked || (itemBrand.ComboBoxItem.GetType().Name != "ComboboxItem" || ((ComboboxItem)itemBrand.ComboBoxItem).Value == "-1" || ((ComboboxItem)itemBrand.ComboBoxItem).Value == "0"))
                        continue;
                    brandids += ((ComboboxItem)itemBrand.ComboBoxItem).Value.ToString() + ",";
                }
                if (regionids.Length > 0)
                    regionids = regionids.Substring(0, regionids.Length - 1);
                if (brandids.Length > 0)
                    brandids = brandids.Substring(0, brandids.Length - 1);

                if (regionids.Length < 1)
                {
                    MessageBox.Show("Please select one or more regions", "Validation failure");
                }
                else if (brandids.Length < 1)
                {
                    MessageBox.Show("Please select one or more brands", "Validation failure");
                }
                else if (MessageBox.Show("This will modify the current disclaimer in the system, would you like to use the new disclaimer?", "Disclaimer Update", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    labelStatusMessageToUser.Text = "Please wait, saving.. ";
                    Application.DoEvents();
                    SQSAdminWCFService.SQSAdminServiceClient client = new SQSAdminWCFService.SQSAdminServiceClient();
                    client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
                    disclaimerHTML = client.SQSAdmin_SaveDisclaimer(type, state, regionids, brandids, disclaimerText, textBoxInternalNotesChangeTracking.Text.ToString(), dateTimePickerDepositDate.Value.ToString("MM/dd/yyyy"), (int)status, MetriconCommon.UserCode);

                    MessageBox.Show("Success, Save disclaimer has completed.", "Confirmation");
                    labelStatusMessageToUser.Text = "Success, Save disclaimer has completed.";

                    //htmlRichTextBox1.Text = string.Empty;
                    //htmlRichTextBox1.AddHTML(disclaimerHTML);
                    //htmlRichTextBox1.SelectionStart = 0;

                    SearchDisclaimers(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error saving the file: " + sfd1.FileName + ", error: " + ex.Message);
            }
        }

        private void SearchDisclaimers(bool resetSaveSelections = true)
        {
            try
            {
                string type = cmbType.SelectedValue.ToString();
                string state = dropdownState.SelectedValue.ToString();
                int typeid = 0;
                int stateid = 0;
                int regionid = 0;
                int brandid = 0;

                int.TryParse(type, out typeid);
                int.TryParse(state, out stateid);
                int.TryParse(dropRegion.SelectedValue.ToString(), out regionid);
                int.TryParse(cmbBrand.SelectedValue.ToString(), out brandid);

                SQSAdminWCFService.SQSAdminServiceClient client = new SQSAdminWCFService.SQSAdminServiceClient();
                client.Endpoint.Address = new System.ServiceModel.EndpointAddress(CommonVariables.WcfEndpoint);
                DataSet dsDisclaimers = client.SQSAdmin_SearchDisclaimers(typeid, stateid, regionid, brandid);
                dataGridViewDisclaimers.DataSource = dsDisclaimers.Tables[0];
                dataGridViewDisclaimers.Columns[0].Width = 100;
                dataGridViewDisclaimers.Columns[0].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridViewDisclaimers.Columns[1].Width = 200;
                dataGridViewDisclaimers.Columns[2].Width = 200;
                dataGridViewDisclaimers.Columns[3].Width = 125;
                dataGridViewDisclaimers.Columns[4].Width = 100;
                dataGridViewDisclaimers.Columns[5].Width = 150;
                dataGridViewDisclaimers.Columns[6].Width = 150;
                dataGridViewDisclaimers.Columns[6].Width = 100;
                //string rtfText = HtmlToRtf(disclaimer);

                //// Read the HTML format
                //fileNameDisclaimer = ofd1.FileName;
                //        StreamReader sr = File.OpenText(fileNameDisclaimer);
                //        string strHTML = sr.ReadToEnd();
                //        sr.Close();

                ////////htmlRichTextBox1.Text = string.Empty;
                ////////htmlRichTextBox1.AddHTML(disclaimerHTML);
                ////////htmlRichTextBox1.SelectionStart = 0;
                if (resetSaveSelections)
                {
                    checkBoxComboBoxRegion.ClearSelection();
                    checkBoxComboBoxBrand.ClearSelection();
                    //int index = checkBoxComboBoxRegion.FindStringExact(dropRegion.Text);
                    //if (index > 0)
                    //{
                    //    checkBoxComboBoxRegion.CheckBoxItems[index].Checked = true;
                    //}
                    //index = checkBoxComboBoxBrand.FindStringExact(cmbBrand.Text);
                    //if (index > 0)
                    //{
                    //    checkBoxComboBoxBrand.CheckBoxItems[index].Checked = true;
                    //}
                }

                //htmlRichTextBox1.Text = string.Empty;
                //if (dataGridViewDisclaimers.Rows.Count > 0)
                //{
                //    string disclaimerHTML = dataGridViewDisclaimers.Rows[0].Cells["Disclaimer HTML"].Value.ToString();
                //    htmlRichTextBox1.AddHTML(disclaimerHTML);
                //    htmlRichTextBox1.SelectionStart = 0;
                //}
                buttonPreviewInWebBrowser_Click(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error when loading the disclaimer: " + ex.Message);
            }
            labelStatusMessageToUser.Text = "Ready";
        }

        private void dataGridViewDisclaimers_SelectionChanged(object sender, EventArgs e)
        {
            if (((DataGridView)sender).CurrentRow != null)
            {
                string disclaimerHTML = ((DataGridView)sender).CurrentRow.Cells["Disclaimer HTML"].Value.ToString();
                string internalNotes = ((DataGridView)sender).CurrentRow.Cells["InternalNotes"].Value.ToString();
                htmlRichTextBox1.Text = string.Empty;
                disclaimerHTML = disclaimerHTML.Replace("<li>", "<TEXT>").Replace("</li>", "</TEXT>");
                disclaimerHTML = disclaimerHTML.Replace("<ol type=1>", "<SECTION1>").Replace("<ol type=\"1\">", "<SECTION1>").Replace("<ol>", "<SECTION1>");
                disclaimerHTML = disclaimerHTML.Replace("<ol type=a>", "<SECTION2>").Replace("<ol type=\"a\">", "<SECTION2>");
                disclaimerHTML = disclaimerHTML.Replace("<ol type=i>", "<SECTION3>").Replace("<ol type=\"i\">", "<SECTION3>");
                disclaimerHTML = disclaimerHTML.Replace("</ol>", "</SECTION_END>");
                disclaimerHTML = disclaimerHTML.Replace("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;", "<SPACE8>").Replace("&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;", "<SPACE8>");
                disclaimerHTML = disclaimerHTML.Replace("&nbsp;&nbsp;&nbsp;&nbsp;", "<SPACE4>").Replace("&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;", "<SPACE4>");
                disclaimerHTML = disclaimerHTML.Replace("&nbsp;&nbsp;", "<SPACE2>").Replace("&amp;nbsp;&amp;nbsp;", "<SPACE2>");
                disclaimerHTML = disclaimerHTML.Replace("&nbsp;", "<SPACE1>").Replace("&amp;nbsp;", "<SPACE1>");
                disclaimerHTML = disclaimerHTML.Replace("<table ", "<font ");
                disclaimerHTML = disclaimerHTML.Replace("</table>", "</font>");
                if (disclaimerHTML.Contains("p { margin: 2px; }"))
                    disclaimerHTML = disclaimerHTML.Replace("p { margin: 2px; }", string.Empty);
                htmlRichTextBox1.AddHTML(disclaimerHTML);
                htmlRichTextBox1.SelectionStart = 0;
                textBoxInternalNotesChangeTracking.Text = internalNotes;

                buttonPreviewInWebBrowser_Click(sender, e);
            }
        }

        private void buttonPreviewInWebBrowser_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(GeneratePreviewHTMLFile());
        }

        private void buttonViewInDefaultWebBrowser_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(GeneratePreviewHTMLFile());
        }

        private string GeneratePreviewHTMLFile()
        {
            string htmlFileName = Directory.GetCurrentDirectory() + "\\test.html";
            string disclaimerText = htmlRichTextBox1.GetHTML(true, true);
            disclaimerText = disclaimerText.Replace("<TEXT>", "<li>").Replace("</TEXT>", "</li>");
            disclaimerText = disclaimerText.Replace("<SECTION1>", "<ol type=1>");
            disclaimerText = disclaimerText.Replace("<SECTION2>", "<ol type=a>");
            disclaimerText = disclaimerText.Replace("<SECTION3>", "<ol type=i>");
            disclaimerText = disclaimerText.Replace("</SECTION_END>", "</ol>");
            disclaimerText = disclaimerText.Replace("<SPACE8>", "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            disclaimerText = disclaimerText.Replace("<SPACE4>", "&nbsp;&nbsp;&nbsp;&nbsp;");
            disclaimerText = disclaimerText.Replace("<SPACE2>", "&nbsp;&nbsp;");
            disclaimerText = disclaimerText.Replace("<SPACE1>", "&nbsp;");
            File.WriteAllText(htmlFileName, disclaimerText);

            return "file://" + htmlFileName;
        }
    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }
}
