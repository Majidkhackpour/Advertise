namespace Ads.Forms.SMS_Panel
{
    partial class frmSendSMS
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSendSMS));
            this.grpAccount = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbLineNumber = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.LineNumberBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbPanel = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.PanelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbxNumbers = new System.Windows.Forms.ListBox();
            this.dgCity = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.dg_CityChecked = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgNumbers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isSendSmsDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.guidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateSabtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.typeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateMDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmCity = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCitySelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.LogBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnFinish = new DevComponents.DotNetBar.ButtonX();
            this.btnAddGrid = new DevComponents.DotNetBar.ButtonX();
            this.btnDelete = new DevComponents.DotNetBar.ButtonX();
            this.btnAddList = new DevComponents.DotNetBar.ButtonX();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.btnAddDasti = new DevComponents.DotNetBar.ButtonX();
            this.grpAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LineNumberBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCity)).BeginInit();
            this.cmCity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogBindingSource)).BeginInit();
            this.groupPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpAccount
            // 
            this.grpAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAccount.BackColor = System.Drawing.Color.White;
            this.grpAccount.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpAccount.Controls.Add(this.cmbLineNumber);
            this.grpAccount.Controls.Add(this.cmbPanel);
            this.grpAccount.Controls.Add(this.label1);
            this.grpAccount.Controls.Add(this.label15);
            this.grpAccount.DisabledBackColor = System.Drawing.Color.Empty;
            this.grpAccount.Location = new System.Drawing.Point(16, 14);
            this.grpAccount.Margin = new System.Windows.Forms.Padding(4);
            this.grpAccount.Name = "grpAccount";
            this.grpAccount.Size = new System.Drawing.Size(752, 82);
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
            this.grpAccount.TabIndex = 55701;
            // 
            // cmbLineNumber
            // 
            this.cmbLineNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLineNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbLineNumber.DataSource = this.LineNumberBindingSource;
            this.cmbLineNumber.DisplayMember = "LineNumber";
            this.cmbLineNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineNumber.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbLineNumber.ForeColor = System.Drawing.Color.Black;
            this.cmbLineNumber.ItemHeight = 20;
            this.cmbLineNumber.Location = new System.Drawing.Point(99, 32);
            this.cmbLineNumber.Margin = new System.Windows.Forms.Padding(4);
            this.cmbLineNumber.Name = "cmbLineNumber";
            this.cmbLineNumber.Size = new System.Drawing.Size(293, 28);
            this.cmbLineNumber.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbLineNumber.TabIndex = 31;
            this.cmbLineNumber.ValueMember = "Guid";
            // 
            // LineNumberBindingSource
            // 
            this.LineNumberBindingSource.DataSource = typeof(BussinesLayer.PanelLineNumberBussines);
            // 
            // cmbPanel
            // 
            this.cmbPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbPanel.DataSource = this.PanelBindingSource;
            this.cmbPanel.DisplayMember = "Name";
            this.cmbPanel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPanel.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbPanel.ForeColor = System.Drawing.Color.Black;
            this.cmbPanel.ItemHeight = 20;
            this.cmbPanel.Location = new System.Drawing.Point(435, 32);
            this.cmbPanel.Margin = new System.Windows.Forms.Padding(4);
            this.cmbPanel.Name = "cmbPanel";
            this.cmbPanel.Size = new System.Drawing.Size(293, 28);
            this.cmbPanel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbPanel.TabIndex = 31;
            this.cmbPanel.ValueMember = "Guid";
            this.cmbPanel.SelectedIndexChanged += new System.EventHandler(this.cmbPanel_SelectedIndexChanged);
            // 
            // PanelBindingSource
            // 
            this.PanelBindingSource.DataSource = typeof(BussinesLayer.PanelBussines);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(548, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 24);
            this.label1.TabIndex = 30;
            this.label1.Text = "پنل";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label15.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label15.ForeColor = System.Drawing.Color.Silver;
            this.label15.Location = new System.Drawing.Point(212, 4);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(184, 24);
            this.label15.TabIndex = 30;
            this.label15.Text = "شماره خط";
            // 
            // txtMessage
            // 
            this.txtMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.ForeColor = System.Drawing.Color.Silver;
            this.txtMessage.Location = new System.Drawing.Point(16, 123);
            this.txtMessage.MaxLength = 3000;
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessage.Size = new System.Drawing.Size(752, 136);
            this.txtMessage.TabIndex = 55702;
            this.txtMessage.Enter += new System.EventHandler(this.txtMessage_Enter);
            this.txtMessage.Leave += new System.EventHandler(this.txtMessage_Leave);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(599, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 20);
            this.label2.TabIndex = 55703;
            this.label2.Text = "متن ارسالی";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(599, 264);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 20);
            this.label3.TabIndex = 55703;
            this.label3.Text = "شماره های تایید شده";
            // 
            // lbxNumbers
            // 
            this.lbxNumbers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbxNumbers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.lbxNumbers.ForeColor = System.Drawing.Color.Silver;
            this.lbxNumbers.FormattingEnabled = true;
            this.lbxNumbers.ItemHeight = 20;
            this.lbxNumbers.Location = new System.Drawing.Point(588, 287);
            this.lbxNumbers.Name = "lbxNumbers";
            this.lbxNumbers.Size = new System.Drawing.Size(180, 224);
            this.lbxNumbers.TabIndex = 55704;
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
            this.dgNumbers,
            this.isSendSmsDataGridViewCheckBoxColumn,
            this.guidDataGridViewTextBoxColumn,
            this.dateSabtDataGridViewTextBoxColumn,
            this.statusDataGridViewCheckBoxColumn,
            this.typeDataGridViewTextBoxColumn,
            this.dateMDataGridViewTextBoxColumn,
            this.typeNameDataGridViewTextBoxColumn});
            this.dgCity.ContextMenuStrip = this.cmCity;
            this.dgCity.DataSource = this.LogBindingSource;
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
            this.dgCity.Location = new System.Drawing.Point(0, 0);
            this.dgCity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgCity.Name = "dgCity";
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
            this.dgCity.Size = new System.Drawing.Size(452, 222);
            this.dgCity.TabIndex = 55705;
            this.dgCity.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGrid_CellClick);
            // 
            // dg_CityChecked
            // 
            this.dg_CityChecked.DataPropertyName = "isChecked";
            this.dg_CityChecked.HeaderText = "";
            this.dg_CityChecked.Name = "dg_CityChecked";
            this.dg_CityChecked.Width = 35;
            // 
            // dgNumbers
            // 
            this.dgNumbers.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dgNumbers.DataPropertyName = "Number";
            this.dgNumbers.HeaderText = "شماره";
            this.dgNumbers.Name = "dgNumbers";
            this.dgNumbers.ReadOnly = true;
            // 
            // isSendSmsDataGridViewCheckBoxColumn
            // 
            this.isSendSmsDataGridViewCheckBoxColumn.DataPropertyName = "isSendSms";
            this.isSendSmsDataGridViewCheckBoxColumn.HeaderText = "ارسال شده";
            this.isSendSmsDataGridViewCheckBoxColumn.Name = "isSendSmsDataGridViewCheckBoxColumn";
            this.isSendSmsDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isSendSmsDataGridViewCheckBoxColumn.Width = 75;
            // 
            // guidDataGridViewTextBoxColumn
            // 
            this.guidDataGridViewTextBoxColumn.DataPropertyName = "Guid";
            this.guidDataGridViewTextBoxColumn.HeaderText = "Guid";
            this.guidDataGridViewTextBoxColumn.Name = "guidDataGridViewTextBoxColumn";
            this.guidDataGridViewTextBoxColumn.Visible = false;
            // 
            // dateSabtDataGridViewTextBoxColumn
            // 
            this.dateSabtDataGridViewTextBoxColumn.DataPropertyName = "DateSabt";
            this.dateSabtDataGridViewTextBoxColumn.HeaderText = "DateSabt";
            this.dateSabtDataGridViewTextBoxColumn.Name = "dateSabtDataGridViewTextBoxColumn";
            this.dateSabtDataGridViewTextBoxColumn.Visible = false;
            // 
            // statusDataGridViewCheckBoxColumn
            // 
            this.statusDataGridViewCheckBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewCheckBoxColumn.HeaderText = "Status";
            this.statusDataGridViewCheckBoxColumn.Name = "statusDataGridViewCheckBoxColumn";
            this.statusDataGridViewCheckBoxColumn.Visible = false;
            // 
            // typeDataGridViewTextBoxColumn
            // 
            this.typeDataGridViewTextBoxColumn.DataPropertyName = "Type";
            this.typeDataGridViewTextBoxColumn.HeaderText = "Type";
            this.typeDataGridViewTextBoxColumn.Name = "typeDataGridViewTextBoxColumn";
            this.typeDataGridViewTextBoxColumn.Visible = false;
            // 
            // dateMDataGridViewTextBoxColumn
            // 
            this.dateMDataGridViewTextBoxColumn.DataPropertyName = "DateM";
            this.dateMDataGridViewTextBoxColumn.HeaderText = "DateM";
            this.dateMDataGridViewTextBoxColumn.Name = "dateMDataGridViewTextBoxColumn";
            this.dateMDataGridViewTextBoxColumn.Visible = false;
            // 
            // typeNameDataGridViewTextBoxColumn
            // 
            this.typeNameDataGridViewTextBoxColumn.DataPropertyName = "TypeName";
            this.typeNameDataGridViewTextBoxColumn.HeaderText = "TypeName";
            this.typeNameDataGridViewTextBoxColumn.Name = "typeNameDataGridViewTextBoxColumn";
            this.typeNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.typeNameDataGridViewTextBoxColumn.Visible = false;
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
            // LogBindingSource
            // 
            this.LogBindingSource.DataSource = typeof(BussinesLayer.ChatNumberBussines);
            // 
            // groupPanel1
            // 
            this.groupPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.Controls.Add(this.dgCity);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(16, 266);
            this.groupPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(458, 250);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.groupPanel1.Style.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 3;
            this.groupPanel1.Style.BorderColor = System.Drawing.Color.Silver;
            this.groupPanel1.Style.BorderColor2 = System.Drawing.Color.Silver;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 3;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 3;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 3;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColor = System.Drawing.Color.Silver;
            this.groupPanel1.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 55706;
            this.groupPanel1.Text = "شماره های چت شده";
            // 
            // btnFinish
            // 
            this.btnFinish.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnFinish.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnFinish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinish.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnFinish.Location = new System.Drawing.Point(16, 520);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnFinish.Size = new System.Drawing.Size(458, 31);
            this.btnFinish.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnFinish.TabIndex = 55707;
            this.btnFinish.Text = "ارسال";
            this.btnFinish.TextColor = System.Drawing.Color.Silver;
            this.btnFinish.ThemeAware = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnAddGrid
            // 
            this.btnAddGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddGrid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnAddGrid.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAddGrid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddGrid.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnAddGrid.Location = new System.Drawing.Point(478, 291);
            this.btnAddGrid.Name = "btnAddGrid";
            this.btnAddGrid.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnAddGrid.Size = new System.Drawing.Size(105, 55);
            this.btnAddGrid.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnAddGrid.TabIndex = 55707;
            this.btnAddGrid.Text = "افزودن شماره های انتخاب شده از جدول";
            this.btnAddGrid.TextColor = System.Drawing.Color.Silver;
            this.btnAddGrid.ThemeAware = true;
            this.btnAddGrid.Click += new System.EventHandler(this.btnAddGrid_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnDelete.Location = new System.Drawing.Point(478, 352);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnDelete.Size = new System.Drawing.Size(105, 55);
            this.btnDelete.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnDelete.TabIndex = 55707;
            this.btnDelete.Text = "حذف شماره انتخاب شده از لیست نهایی";
            this.btnDelete.TextColor = System.Drawing.Color.Silver;
            this.btnDelete.ThemeAware = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddList
            // 
            this.btnAddList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnAddList.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAddList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddList.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnAddList.Location = new System.Drawing.Point(478, 413);
            this.btnAddList.Name = "btnAddList";
            this.btnAddList.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnAddList.Size = new System.Drawing.Size(105, 55);
            this.btnAddList.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnAddList.TabIndex = 55707;
            this.btnAddList.Text = "انتخاب شماره از شماره های چت نشده";
            this.btnAddList.TextColor = System.Drawing.Color.Silver;
            this.btnAddList.ThemeAware = true;
            this.btnAddList.Click += new System.EventHandler(this.btnAddList_Click);
            // 
            // txtNumber
            // 
            this.txtNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumber.ForeColor = System.Drawing.Color.Silver;
            this.txtNumber.Location = new System.Drawing.Point(588, 522);
            this.txtNumber.MaxLength = 3000;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNumber.Size = new System.Drawing.Size(180, 27);
            this.txtNumber.TabIndex = 55708;
            this.txtNumber.Enter += new System.EventHandler(this.txtNumber_Enter);
            this.txtNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumber_KeyDown);
            this.txtNumber.Leave += new System.EventHandler(this.txtNumber_Leave);
            // 
            // btnAddDasti
            // 
            this.btnAddDasti.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddDasti.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddDasti.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnAddDasti.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnAddDasti.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDasti.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnAddDasti.Location = new System.Drawing.Point(480, 522);
            this.btnAddDasti.Name = "btnAddDasti";
            this.btnAddDasti.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnAddDasti.Size = new System.Drawing.Size(105, 29);
            this.btnAddDasti.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnAddDasti.TabIndex = 55707;
            this.btnAddDasti.Text = "افزودن دستی";
            this.btnAddDasti.TextColor = System.Drawing.Color.Silver;
            this.btnAddDasti.ThemeAware = true;
            this.btnAddDasti.Click += new System.EventHandler(this.btnAddDasti_Click);
            // 
            // frmSendSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.btnAddDasti);
            this.Controls.Add(this.btnAddList);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddGrid);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.groupPanel1);
            this.Controls.Add(this.lbxNumbers);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.grpAccount);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmSendSMS";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSendSMS_Load);
            this.grpAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LineNumberBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PanelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCity)).EndInit();
            this.cmCity.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogBindingSource)).EndInit();
            this.groupPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevComponents.DotNetBar.Controls.GroupPanel grpAccount;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cmbLineNumber;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cmbPanel;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label15;
        private System.Windows.Forms.BindingSource LineNumberBindingSource;
        private System.Windows.Forms.BindingSource PanelBindingSource;
        public System.Windows.Forms.TextBox txtMessage;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox lbxNumbers;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgCity;
        public DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        public DevComponents.DotNetBar.ButtonX btnFinish;
        public DevComponents.DotNetBar.ButtonX btnAddGrid;
        public DevComponents.DotNetBar.ButtonX btnDelete;
        public DevComponents.DotNetBar.ButtonX btnAddList;
        private System.Windows.Forms.BindingSource LogBindingSource;
        private System.Windows.Forms.ContextMenuStrip cmCity;
        private System.Windows.Forms.ToolStripMenuItem mnuCitySelectAll;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dg_CityChecked;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgNumbers;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isSendSmsDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn guidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateSabtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn statusDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateMDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeNameDataGridViewTextBoxColumn;
        public System.Windows.Forms.TextBox txtNumber;
        public DevComponents.DotNetBar.ButtonX btnAddDasti;
    }
}