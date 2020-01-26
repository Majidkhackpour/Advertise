namespace Ads.Forms.Simcard
{
    partial class frmSimcard_Main
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSimcard_Main));
            this.grpAccount = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbOperator = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtOwner = new System.Windows.Forms.TextBox();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.uC_Date1 = new UC_Date.UC_Date();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.btnFinish = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgCity = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dg_CityChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dg_CityGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateSabtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cmCity = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCitySelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.cityBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgAds = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.cmAds = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuAdsSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.dg_AdvChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dg_AdvName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rootPathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.oldAdvNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.titleStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.grpAccount.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCity)).BeginInit();
            this.cmCity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cityBindingSource)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAds)).BeginInit();
            this.cmAds.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.adsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grpAccount
            // 
            this.grpAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAccount.BackColor = System.Drawing.Color.White;
            this.grpAccount.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpAccount.Controls.Add(this.cmbOperator);
            this.grpAccount.Controls.Add(this.txtUserName);
            this.grpAccount.Controls.Add(this.txtOwner);
            this.grpAccount.Controls.Add(this.txtNumber);
            this.grpAccount.Controls.Add(this.label9);
            this.grpAccount.Controls.Add(this.label7);
            this.grpAccount.Controls.Add(this.label8);
            this.grpAccount.Controls.Add(this.label2);
            this.grpAccount.DisabledBackColor = System.Drawing.Color.Empty;
            this.grpAccount.Location = new System.Drawing.Point(7, 6);
            this.grpAccount.Name = "grpAccount";
            this.grpAccount.Size = new System.Drawing.Size(361, 241);
            // 
            // 
            // 
            this.grpAccount.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.grpAccount.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.grpAccount.Style.BackColorGradientAngle = 90;
            this.grpAccount.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpAccount.Style.BorderBottomWidth = 3;
            this.grpAccount.Style.BorderColor = System.Drawing.Color.Silver;
            this.grpAccount.Style.BorderColor2 = System.Drawing.Color.Silver;
            this.grpAccount.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpAccount.Style.BorderLeftWidth = 3;
            this.grpAccount.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpAccount.Style.BorderRightWidth = 3;
            this.grpAccount.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.grpAccount.Style.BorderTopWidth = 3;
            this.grpAccount.Style.CornerDiameter = 4;
            this.grpAccount.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.grpAccount.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.grpAccount.Style.TextColor = System.Drawing.Color.Black;
            this.grpAccount.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.grpAccount.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.grpAccount.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.grpAccount.TabIndex = 0;
            // 
            // cmbOperator
            // 
            this.cmbOperator.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbOperator.DisplayMember = "Name";
            this.cmbOperator.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbOperator.ForeColor = System.Drawing.Color.Black;
            this.cmbOperator.ItemHeight = 20;
            this.cmbOperator.Location = new System.Drawing.Point(3, 141);
            this.cmbOperator.Name = "cmbOperator";
            this.cmbOperator.Size = new System.Drawing.Size(344, 28);
            this.cmbOperator.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbOperator.TabIndex = 2;
            this.cmbOperator.ValueMember = "Guid";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.ForeColor = System.Drawing.Color.Silver;
            this.txtUserName.Location = new System.Drawing.Point(1, 198);
            this.txtUserName.MaxLength = 3000;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(344, 27);
            this.txtUserName.TabIndex = 3;
            this.txtUserName.Enter += new System.EventHandler(this.txtUserName_Enter);
            this.txtUserName.Leave += new System.EventHandler(this.txtUserName_Leave);
            // 
            // txtOwner
            // 
            this.txtOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOwner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtOwner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOwner.ForeColor = System.Drawing.Color.Silver;
            this.txtOwner.Location = new System.Drawing.Point(1, 88);
            this.txtOwner.MaxLength = 3000;
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.Size = new System.Drawing.Size(344, 27);
            this.txtOwner.TabIndex = 1;
            this.txtOwner.Enter += new System.EventHandler(this.txtOwner_Enter);
            this.txtOwner.Leave += new System.EventHandler(this.txtOwner_Leave);
            // 
            // txtNumber
            // 
            this.txtNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumber.ForeColor = System.Drawing.Color.Silver;
            this.txtNumber.Location = new System.Drawing.Point(1, 33);
            this.txtNumber.MaxLength = 11;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(344, 27);
            this.txtNumber.TabIndex = 0;
            this.txtNumber.Enter += new System.EventHandler(this.txtNumber_Enter);
            this.txtNumber.Leave += new System.EventHandler(this.txtNumber_Leave);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label9.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label9.ForeColor = System.Drawing.Color.Silver;
            this.label9.Location = new System.Drawing.Point(274, 173);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 20);
            this.label9.TabIndex = 30;
            this.label9.Text = "نام کاربری";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(274, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 20);
            this.label7.TabIndex = 30;
            this.label7.Text = "مالک";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label8.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label8.ForeColor = System.Drawing.Color.Silver;
            this.label8.Location = new System.Drawing.Point(274, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 20);
            this.label8.TabIndex = 30;
            this.label8.Text = "اپراتور";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(274, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "شماره";
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeader.Font = new System.Drawing.Font("B Yekan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblHeader.ForeColor = System.Drawing.Color.Silver;
            this.lblHeader.Location = new System.Drawing.Point(209, 6);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(180, 24);
            this.lblHeader.TabIndex = 55689;
            this.lblHeader.Text = "افزودن سیمکارت جدید";
            // 
            // uC_Date1
            // 
            this.uC_Date1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.uC_Date1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_Date1.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.uC_Date1.Location = new System.Drawing.Point(0, 0);
            this.uC_Date1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uC_Date1.Name = "uC_Date1";
            this.uC_Date1.Size = new System.Drawing.Size(399, 47);
            this.uC_Date1.TabIndex = 55688;
            // 
            // line1
            // 
            this.line1.ForeColor = System.Drawing.Color.Silver;
            this.line1.Location = new System.Drawing.Point(0, 340);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(399, 19);
            this.line1.TabIndex = 55690;
            this.line1.Text = "line1";
            this.line1.Thickness = 2;
            // 
            // btnFinish
            // 
            this.btnFinish.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnFinish.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnFinish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinish.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnFinish.Location = new System.Drawing.Point(240, 363);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnFinish.Size = new System.Drawing.Size(145, 25);
            this.btnFinish.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnFinish.TabIndex = 1;
            this.btnFinish.Text = "تایید (F5)";
            this.btnFinish.TextColor = System.Drawing.Color.Silver;
            this.btnFinish.ThemeAware = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCancel.Location = new System.Drawing.Point(12, 363);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnCancel.Size = new System.Drawing.Size(125, 25);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "انصراف (ESC)";
            this.btnCancel.TextColor = System.Drawing.Color.Silver;
            this.btnCancel.ThemeAware = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(9, 48);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(382, 289);
            this.tabControl1.TabIndex = 55691;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.tabPage1.Controls.Add(this.grpAccount);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(374, 256);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "اطلاعات عمومی";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.tabPage2.Controls.Add(this.dgCity);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(374, 256);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "شهرها";
            // 
            // dgCity
            // 
            this.dgCity.AllowUserToAddRows = false;
            this.dgCity.AllowUserToDeleteRows = false;
            this.dgCity.AllowUserToResizeColumns = false;
            this.dgCity.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgCity.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCity.AutoGenerateColumns = false;
            this.dgCity.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCity.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgCity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCity.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dg_CityChecked,
            this.nameDataGridViewTextBoxColumn,
            this.dg_CityGuid,
            this.dateSabtDataGridViewTextBoxColumn,
            this.statusDataGridViewCheckBoxColumn});
            this.dgCity.ContextMenuStrip = this.cmCity;
            this.dgCity.DataSource = this.cityBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCity.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgCity.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCity.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgCity.Location = new System.Drawing.Point(3, 3);
            this.dgCity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgCity.Name = "dgCity";
            this.dgCity.ReadOnly = true;
            this.dgCity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgCity.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgCity.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgCity.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.dgCity.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgCity.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgCity.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCity.Size = new System.Drawing.Size(368, 250);
            this.dgCity.TabIndex = 55690;
            this.dgCity.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCity_CellClick);
            // 
            // dg_CityChecked
            // 
            this.dg_CityChecked.DataPropertyName = "Is_Checked";
            this.dg_CityChecked.HeaderText = "";
            this.dg_CityChecked.Name = "dg_CityChecked";
            this.dg_CityChecked.ReadOnly = true;
            this.dg_CityChecked.Width = 35;
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "عنوان";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dg_CityGuid
            // 
            this.dg_CityGuid.DataPropertyName = "Guid";
            this.dg_CityGuid.HeaderText = "Guid";
            this.dg_CityGuid.Name = "dg_CityGuid";
            this.dg_CityGuid.ReadOnly = true;
            this.dg_CityGuid.Visible = false;
            // 
            // dateSabtDataGridViewTextBoxColumn
            // 
            this.dateSabtDataGridViewTextBoxColumn.DataPropertyName = "DateSabt";
            this.dateSabtDataGridViewTextBoxColumn.HeaderText = "DateSabt";
            this.dateSabtDataGridViewTextBoxColumn.Name = "dateSabtDataGridViewTextBoxColumn";
            this.dateSabtDataGridViewTextBoxColumn.ReadOnly = true;
            this.dateSabtDataGridViewTextBoxColumn.Visible = false;
            // 
            // statusDataGridViewCheckBoxColumn
            // 
            this.statusDataGridViewCheckBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewCheckBoxColumn.HeaderText = "Status";
            this.statusDataGridViewCheckBoxColumn.Name = "statusDataGridViewCheckBoxColumn";
            this.statusDataGridViewCheckBoxColumn.ReadOnly = true;
            this.statusDataGridViewCheckBoxColumn.Visible = false;
            // 
            // cmCity
            // 
            this.cmCity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.cmCity.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmCity.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCitySelectAll});
            this.cmCity.Name = "contextMenuStrip1";
            this.cmCity.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmCity.Size = new System.Drawing.Size(135, 28);
            // 
            // mnuCitySelectAll
            // 
            this.mnuCitySelectAll.CheckOnClick = true;
            this.mnuCitySelectAll.ForeColor = System.Drawing.Color.Silver;
            this.mnuCitySelectAll.Name = "mnuCitySelectAll";
            this.mnuCitySelectAll.Size = new System.Drawing.Size(134, 24);
            this.mnuCitySelectAll.Text = "انتخاب همه";
            this.mnuCitySelectAll.Click += new System.EventHandler(this.mnuCitySelectAll_Click);
            // 
            // cityBindingSource
            // 
            this.cityBindingSource.DataSource = typeof(BussinesLayer.DivarCityBussines);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.tabPage3.Controls.Add(this.dgAds);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(374, 256);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "آگهی ها";
            // 
            // dgAds
            // 
            this.dgAds.AllowUserToAddRows = false;
            this.dgAds.AllowUserToDeleteRows = false;
            this.dgAds.AllowUserToResizeColumns = false;
            this.dgAds.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dgAds.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgAds.AutoGenerateColumns = false;
            this.dgAds.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAds.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgAds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dg_AdvChecked,
            this.dg_AdvName,
            this.rootPathDataGridViewTextBoxColumn,
            this.oldAdvNameDataGridViewTextBoxColumn,
            this.contentDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn,
            this.titleStringDataGridViewTextBoxColumn});
            this.dgAds.ContextMenuStrip = this.cmAds;
            this.dgAds.DataSource = this.adsBindingSource;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgAds.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgAds.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAds.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgAds.Location = new System.Drawing.Point(3, 3);
            this.dgAds.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgAds.Name = "dgAds";
            this.dgAds.ReadOnly = true;
            this.dgAds.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgAds.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgAds.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgAds.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.dgAds.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgAds.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgAds.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAds.Size = new System.Drawing.Size(368, 250);
            this.dgAds.TabIndex = 55691;
            this.dgAds.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAds_CellClick);
            // 
            // cmAds
            // 
            this.cmAds.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.cmAds.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.cmAds.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAdsSelectAll});
            this.cmAds.Name = "contextMenuStrip1";
            this.cmAds.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmAds.Size = new System.Drawing.Size(181, 50);
            // 
            // mnuAdsSelectAll
            // 
            this.mnuAdsSelectAll.CheckOnClick = true;
            this.mnuAdsSelectAll.ForeColor = System.Drawing.Color.Silver;
            this.mnuAdsSelectAll.Name = "mnuAdsSelectAll";
            this.mnuAdsSelectAll.Size = new System.Drawing.Size(180, 24);
            this.mnuAdsSelectAll.Text = "انتخاب همه";
            this.mnuAdsSelectAll.Click += new System.EventHandler(this.mnuAdsSelectAll_Click);
            // 
            // dg_AdvChecked
            // 
            this.dg_AdvChecked.DataPropertyName = "Is_Checked";
            this.dg_AdvChecked.HeaderText = "";
            this.dg_AdvChecked.Name = "dg_AdvChecked";
            this.dg_AdvChecked.ReadOnly = true;
            this.dg_AdvChecked.Width = 35;
            // 
            // dg_AdvName
            // 
            this.dg_AdvName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dg_AdvName.DataPropertyName = "AdvName";
            this.dg_AdvName.HeaderText = "عنوان";
            this.dg_AdvName.Name = "dg_AdvName";
            this.dg_AdvName.ReadOnly = true;
            // 
            // rootPathDataGridViewTextBoxColumn
            // 
            this.rootPathDataGridViewTextBoxColumn.DataPropertyName = "RootPath";
            this.rootPathDataGridViewTextBoxColumn.HeaderText = "RootPath";
            this.rootPathDataGridViewTextBoxColumn.Name = "rootPathDataGridViewTextBoxColumn";
            this.rootPathDataGridViewTextBoxColumn.ReadOnly = true;
            this.rootPathDataGridViewTextBoxColumn.Visible = false;
            // 
            // oldAdvNameDataGridViewTextBoxColumn
            // 
            this.oldAdvNameDataGridViewTextBoxColumn.DataPropertyName = "OldAdvName";
            this.oldAdvNameDataGridViewTextBoxColumn.HeaderText = "OldAdvName";
            this.oldAdvNameDataGridViewTextBoxColumn.Name = "oldAdvNameDataGridViewTextBoxColumn";
            this.oldAdvNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.oldAdvNameDataGridViewTextBoxColumn.Visible = false;
            // 
            // contentDataGridViewTextBoxColumn
            // 
            this.contentDataGridViewTextBoxColumn.DataPropertyName = "Content";
            this.contentDataGridViewTextBoxColumn.HeaderText = "Content";
            this.contentDataGridViewTextBoxColumn.Name = "contentDataGridViewTextBoxColumn";
            this.contentDataGridViewTextBoxColumn.ReadOnly = true;
            this.contentDataGridViewTextBoxColumn.Visible = false;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "Price";
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.ReadOnly = true;
            this.priceDataGridViewTextBoxColumn.Visible = false;
            // 
            // titleStringDataGridViewTextBoxColumn
            // 
            this.titleStringDataGridViewTextBoxColumn.DataPropertyName = "TitleString";
            this.titleStringDataGridViewTextBoxColumn.HeaderText = "TitleString";
            this.titleStringDataGridViewTextBoxColumn.Name = "titleStringDataGridViewTextBoxColumn";
            this.titleStringDataGridViewTextBoxColumn.ReadOnly = true;
            this.titleStringDataGridViewTextBoxColumn.Visible = false;
            // 
            // adsBindingSource
            // 
            this.adsBindingSource.DataSource = typeof(Ads.Classes.Advertise);
            // 
            // frmSimcard_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(399, 400);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.uC_Date1);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(415, 439);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(415, 439);
            this.Name = "frmSimcard_Main";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmSimcard_Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSimcard_Main_KeyDown);
            this.grpAccount.ResumeLayout(false);
            this.grpAccount.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCity)).EndInit();
            this.cmCity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cityBindingSource)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAds)).EndInit();
            this.cmAds.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.adsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public DevComponents.DotNetBar.Controls.GroupPanel grpAccount;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblHeader;
        private UC_Date.UC_Date uC_Date1;
        public System.Windows.Forms.TextBox txtUserName;
        public System.Windows.Forms.TextBox txtOwner;
        public System.Windows.Forms.TextBox txtNumber;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label8;
        private DevComponents.DotNetBar.Controls.Line line1;
        public DevComponents.DotNetBar.ButtonX btnFinish;
        public DevComponents.DotNetBar.ButtonX btnCancel;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cmbOperator;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgCity;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgAds;
        private System.Windows.Forms.BindingSource cityBindingSource;
        private System.Windows.Forms.BindingSource adsBindingSource;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dg_CityChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dg_CityGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateSabtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn statusDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dg_AdvChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn dg_AdvName;
        private System.Windows.Forms.DataGridViewTextBoxColumn rootPathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn oldAdvNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn contentDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn titleStringDataGridViewTextBoxColumn;
        private System.Windows.Forms.ContextMenuStrip cmCity;
        private System.Windows.Forms.ToolStripMenuItem mnuCitySelectAll;
        private System.Windows.Forms.ContextMenuStrip cmAds;
        private System.Windows.Forms.ToolStripMenuItem mnuAdsSelectAll;
    }
}