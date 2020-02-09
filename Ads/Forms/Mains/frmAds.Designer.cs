namespace Ads.Forms.Mains
{
    partial class frmAds
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAds));
            this.trvGroup = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuInsGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInsAdv = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.fPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.uC_Date1 = new UC_Date.UC_Date();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnFinish = new DevComponents.DotNetBar.ButtonX();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.grpAccount = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.btnBack = new DevComponents.DotNetBar.ButtonX();
            this.btnClear = new DevComponents.DotNetBar.ButtonX();
            this.btnSaveInList = new DevComponents.DotNetBar.ButtonX();
            this.btnInsNewContent = new DevComponents.DotNetBar.ButtonX();
            this.cmbSheypoorCat2 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SheypoorCat2BingingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbDivarCat3 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.DivarCat3BingingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbGroup = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.ParentBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbSheypoorCat1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SheypoorCat1BingingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbDivarCat2 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.DivarCat2BingingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cmbDivarCat1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.DivarCat1BingingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lineNumbersForRichText2 = new LineNumbersControlForRichTextBox.LineNumbersForRichText();
            this.txtDesc = new System.Windows.Forms.RichTextBox();
            this.lineNumbersForRichText1 = new LineNumbersControlForRichTextBox.LineNumbersForRichText();
            this.txtTitles = new System.Windows.Forms.RichTextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnInsImage = new DevComponents.DotNetBar.ButtonX();
            this.btnDeleteImage = new DevComponents.DotNetBar.ButtonX();
            this.contextMenuStrip1.SuspendLayout();
            this.grpAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SheypoorCat2BingingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DivarCat3BingingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheypoorCat1BingingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DivarCat2BingingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DivarCat1BingingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // trvGroup
            // 
            this.trvGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trvGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.trvGroup.ContextMenuStrip = this.contextMenuStrip1;
            this.trvGroup.ForeColor = System.Drawing.Color.Silver;
            this.trvGroup.Location = new System.Drawing.Point(765, 51);
            this.trvGroup.Name = "trvGroup";
            this.trvGroup.RightToLeftLayout = true;
            this.trvGroup.Size = new System.Drawing.Size(203, 379);
            this.trvGroup.TabIndex = 0;
            this.trvGroup.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvGroup_AfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.contextMenuStrip1.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInsGroup,
            this.mnuInsAdv,
            this.mnuEdit,
            this.mnuDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(151, 100);
            // 
            // mnuInsGroup
            // 
            this.mnuInsGroup.ForeColor = System.Drawing.Color.DarkGray;
            this.mnuInsGroup.Name = "mnuInsGroup";
            this.mnuInsGroup.Size = new System.Drawing.Size(150, 24);
            this.mnuInsGroup.Text = "درج گروه جدید";
            this.mnuInsGroup.Click += new System.EventHandler(this.mnuInsGroup_Click);
            // 
            // mnuInsAdv
            // 
            this.mnuInsAdv.ForeColor = System.Drawing.Color.Silver;
            this.mnuInsAdv.Name = "mnuInsAdv";
            this.mnuInsAdv.Size = new System.Drawing.Size(150, 24);
            this.mnuInsAdv.Text = "درج آگهی جدید";
            this.mnuInsAdv.Click += new System.EventHandler(this.mnuInsAdv_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.ForeColor = System.Drawing.Color.Silver;
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(150, 24);
            this.mnuEdit.Text = "ویرایش";
            this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
            // 
            // mnuDelete
            // 
            this.mnuDelete.ForeColor = System.Drawing.Color.Silver;
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Size = new System.Drawing.Size(150, 24);
            this.mnuDelete.Text = "حذف";
            this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
            // 
            // fPanel
            // 
            this.fPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.fPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.fPanel.ContextMenuStrip = this.contextMenuStrip1;
            this.fPanel.Location = new System.Drawing.Point(12, 51);
            this.fPanel.Name = "fPanel";
            this.fPanel.Size = new System.Drawing.Size(214, 309);
            this.fPanel.TabIndex = 1;
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeader.Font = new System.Drawing.Font("B Yekan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblHeader.ForeColor = System.Drawing.Color.Silver;
            this.lblHeader.Location = new System.Drawing.Point(787, 7);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(180, 24);
            this.lblHeader.TabIndex = 55697;
            this.lblHeader.Text = "مدیریت آگهی ها";
            // 
            // uC_Date1
            // 
            this.uC_Date1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.uC_Date1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_Date1.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.uC_Date1.Location = new System.Drawing.Point(0, 0);
            this.uC_Date1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uC_Date1.Name = "uC_Date1";
            this.uC_Date1.Size = new System.Drawing.Size(980, 47);
            this.uC_Date1.TabIndex = 55698;
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCancel.Location = new System.Drawing.Point(12, 455);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnCancel.Size = new System.Drawing.Size(214, 25);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnCancel.TabIndex = 55700;
            this.btnCancel.Text = "انصراف (ESC)";
            this.btnCancel.TextColor = System.Drawing.Color.Silver;
            this.btnCancel.ThemeAware = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnFinish
            // 
            this.btnFinish.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFinish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnFinish.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnFinish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinish.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnFinish.Location = new System.Drawing.Point(765, 455);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnFinish.Size = new System.Drawing.Size(203, 25);
            this.btnFinish.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnFinish.TabIndex = 55699;
            this.btnFinish.Text = "تایید (F5)";
            this.btnFinish.TextColor = System.Drawing.Color.Silver;
            this.btnFinish.ThemeAware = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // line1
            // 
            this.line1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.line1.ForeColor = System.Drawing.Color.Silver;
            this.line1.Location = new System.Drawing.Point(-8, 430);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(997, 19);
            this.line1.TabIndex = 55701;
            this.line1.Text = "line1";
            this.line1.Thickness = 2;
            // 
            // grpAccount
            // 
            this.grpAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAccount.BackColor = System.Drawing.Color.White;
            this.grpAccount.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpAccount.ContextMenuStrip = this.contextMenuStrip1;
            this.grpAccount.Controls.Add(this.btnBack);
            this.grpAccount.Controls.Add(this.btnClear);
            this.grpAccount.Controls.Add(this.btnSaveInList);
            this.grpAccount.Controls.Add(this.btnInsNewContent);
            this.grpAccount.Controls.Add(this.cmbSheypoorCat2);
            this.grpAccount.Controls.Add(this.cmbDivarCat3);
            this.grpAccount.Controls.Add(this.cmbGroup);
            this.grpAccount.Controls.Add(this.cmbSheypoorCat1);
            this.grpAccount.Controls.Add(this.cmbDivarCat2);
            this.grpAccount.Controls.Add(this.cmbDivarCat1);
            this.grpAccount.Controls.Add(this.label8);
            this.grpAccount.Controls.Add(this.label6);
            this.grpAccount.Controls.Add(this.label9);
            this.grpAccount.Controls.Add(this.label7);
            this.grpAccount.Controls.Add(this.label5);
            this.grpAccount.Controls.Add(this.label17);
            this.grpAccount.Controls.Add(this.lineNumbersForRichText2);
            this.grpAccount.Controls.Add(this.lineNumbersForRichText1);
            this.grpAccount.Controls.Add(this.txtDesc);
            this.grpAccount.Controls.Add(this.txtTitles);
            this.grpAccount.Controls.Add(this.txtPrice);
            this.grpAccount.Controls.Add(this.txtName);
            this.grpAccount.Controls.Add(this.label3);
            this.grpAccount.Controls.Add(this.label1);
            this.grpAccount.Controls.Add(this.label4);
            this.grpAccount.Controls.Add(this.label2);
            this.grpAccount.DisabledBackColor = System.Drawing.Color.Empty;
            this.grpAccount.Location = new System.Drawing.Point(232, 51);
            this.grpAccount.Name = "grpAccount";
            this.grpAccount.Size = new System.Drawing.Size(525, 375);
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
            this.grpAccount.TabIndex = 55702;
            // 
            // btnBack
            // 
            this.btnBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnBack.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnBack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBack.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnBack.Location = new System.Drawing.Point(3, 210);
            this.btnBack.Name = "btnBack";
            this.btnBack.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnBack.Size = new System.Drawing.Size(81, 25);
            this.btnBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnBack.TabIndex = 55700;
            this.btnBack.Text = "قبلی";
            this.btnBack.TextColor = System.Drawing.Color.Silver;
            this.btnBack.ThemeAware = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnClear.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnClear.Location = new System.Drawing.Point(3, 241);
            this.btnClear.Name = "btnClear";
            this.btnClear.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnClear.Size = new System.Drawing.Size(81, 25);
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnClear.TabIndex = 55700;
            this.btnClear.Text = "پاک کردن ...";
            this.btnClear.TextColor = System.Drawing.Color.Silver;
            this.btnClear.ThemeAware = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnSaveInList
            // 
            this.btnSaveInList.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveInList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnSaveInList.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnSaveInList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveInList.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSaveInList.Location = new System.Drawing.Point(101, 241);
            this.btnSaveInList.Name = "btnSaveInList";
            this.btnSaveInList.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnSaveInList.Size = new System.Drawing.Size(81, 25);
            this.btnSaveInList.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnSaveInList.TabIndex = 55700;
            this.btnSaveInList.Text = "ذخیره";
            this.btnSaveInList.TextColor = System.Drawing.Color.Silver;
            this.btnSaveInList.ThemeAware = true;
            this.btnSaveInList.Click += new System.EventHandler(this.btnSaveInList_Click);
            // 
            // btnInsNewContent
            // 
            this.btnInsNewContent.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInsNewContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnInsNewContent.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnInsNewContent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInsNewContent.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnInsNewContent.Location = new System.Drawing.Point(101, 210);
            this.btnInsNewContent.Name = "btnInsNewContent";
            this.btnInsNewContent.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnInsNewContent.Size = new System.Drawing.Size(81, 25);
            this.btnInsNewContent.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnInsNewContent.TabIndex = 55700;
            this.btnInsNewContent.Text = "بعدی";
            this.btnInsNewContent.TextColor = System.Drawing.Color.Silver;
            this.btnInsNewContent.ThemeAware = true;
            this.btnInsNewContent.Click += new System.EventHandler(this.btnInsNewContent_Click);
            // 
            // cmbSheypoorCat2
            // 
            this.cmbSheypoorCat2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbSheypoorCat2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbSheypoorCat2.DataSource = this.SheypoorCat2BingingSource;
            this.cmbSheypoorCat2.DisplayMember = "Name";
            this.cmbSheypoorCat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSheypoorCat2.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbSheypoorCat2.ForeColor = System.Drawing.Color.Black;
            this.cmbSheypoorCat2.ItemHeight = 20;
            this.cmbSheypoorCat2.Location = new System.Drawing.Point(3, 329);
            this.cmbSheypoorCat2.Name = "cmbSheypoorCat2";
            this.cmbSheypoorCat2.Size = new System.Drawing.Size(179, 28);
            this.cmbSheypoorCat2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSheypoorCat2.TabIndex = 34;
            this.cmbSheypoorCat2.ValueMember = "Guid";
            // 
            // SheypoorCat2BingingSource
            // 
            this.SheypoorCat2BingingSource.DataSource = typeof(BussinesLayer.AdvGroupBussines);
            // 
            // cmbDivarCat3
            // 
            this.cmbDivarCat3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbDivarCat3.DataSource = this.DivarCat3BingingSource;
            this.cmbDivarCat3.DisplayMember = "Name";
            this.cmbDivarCat3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivarCat3.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbDivarCat3.ForeColor = System.Drawing.Color.Black;
            this.cmbDivarCat3.ItemHeight = 20;
            this.cmbDivarCat3.Location = new System.Drawing.Point(3, 145);
            this.cmbDivarCat3.Name = "cmbDivarCat3";
            this.cmbDivarCat3.Size = new System.Drawing.Size(179, 28);
            this.cmbDivarCat3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDivarCat3.TabIndex = 34;
            this.cmbDivarCat3.ValueMember = "Guid";
            // 
            // DivarCat3BingingSource
            // 
            this.DivarCat3BingingSource.DataSource = typeof(BussinesLayer.AdvGroupBussines);
            // 
            // cmbGroup
            // 
            this.cmbGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbGroup.DataSource = this.ParentBindingSource;
            this.cmbGroup.DisplayMember = "Name";
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbGroup.ForeColor = System.Drawing.Color.Black;
            this.cmbGroup.ItemHeight = 20;
            this.cmbGroup.Location = new System.Drawing.Point(197, 274);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(309, 28);
            this.cmbGroup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbGroup.TabIndex = 34;
            this.cmbGroup.ValueMember = "Guid";
            // 
            // ParentBindingSource
            // 
            this.ParentBindingSource.DataSource = typeof(BussinesLayer.AdvGroupBussines);
            // 
            // cmbSheypoorCat1
            // 
            this.cmbSheypoorCat1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmbSheypoorCat1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbSheypoorCat1.DataSource = this.SheypoorCat1BingingSource;
            this.cmbSheypoorCat1.DisplayMember = "Name";
            this.cmbSheypoorCat1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSheypoorCat1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbSheypoorCat1.ForeColor = System.Drawing.Color.Black;
            this.cmbSheypoorCat1.ItemHeight = 20;
            this.cmbSheypoorCat1.Location = new System.Drawing.Point(3, 274);
            this.cmbSheypoorCat1.Name = "cmbSheypoorCat1";
            this.cmbSheypoorCat1.Size = new System.Drawing.Size(179, 28);
            this.cmbSheypoorCat1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbSheypoorCat1.TabIndex = 34;
            this.cmbSheypoorCat1.ValueMember = "Guid";
            this.cmbSheypoorCat1.SelectedIndexChanged += new System.EventHandler(this.cmbSheypoorCat1_SelectedIndexChanged);
            // 
            // SheypoorCat1BingingSource
            // 
            this.SheypoorCat1BingingSource.DataSource = typeof(BussinesLayer.AdvCategoryBussines);
            // 
            // cmbDivarCat2
            // 
            this.cmbDivarCat2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbDivarCat2.DataSource = this.DivarCat2BingingSource;
            this.cmbDivarCat2.DisplayMember = "Name";
            this.cmbDivarCat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivarCat2.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbDivarCat2.ForeColor = System.Drawing.Color.Black;
            this.cmbDivarCat2.ItemHeight = 20;
            this.cmbDivarCat2.Location = new System.Drawing.Point(3, 90);
            this.cmbDivarCat2.Name = "cmbDivarCat2";
            this.cmbDivarCat2.Size = new System.Drawing.Size(179, 28);
            this.cmbDivarCat2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDivarCat2.TabIndex = 34;
            this.cmbDivarCat2.ValueMember = "Guid";
            this.cmbDivarCat2.SelectedIndexChanged += new System.EventHandler(this.cmbDivarCat2_SelectedIndexChanged);
            // 
            // DivarCat2BingingSource
            // 
            this.DivarCat2BingingSource.DataSource = typeof(BussinesLayer.AdvGroupBussines);
            // 
            // cmbDivarCat1
            // 
            this.cmbDivarCat1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbDivarCat1.DataSource = this.DivarCat1BingingSource;
            this.cmbDivarCat1.DisplayMember = "Name";
            this.cmbDivarCat1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDivarCat1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbDivarCat1.ForeColor = System.Drawing.Color.Black;
            this.cmbDivarCat1.ItemHeight = 20;
            this.cmbDivarCat1.Location = new System.Drawing.Point(3, 35);
            this.cmbDivarCat1.Name = "cmbDivarCat1";
            this.cmbDivarCat1.Size = new System.Drawing.Size(179, 28);
            this.cmbDivarCat1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDivarCat1.TabIndex = 34;
            this.cmbDivarCat1.ValueMember = "Guid";
            this.cmbDivarCat1.SelectedIndexChanged += new System.EventHandler(this.cmbDivarCat1_SelectedIndexChanged);
            // 
            // DivarCat1BingingSource
            // 
            this.DivarCat1BingingSource.DataSource = typeof(BussinesLayer.AdvCategoryBussines);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label8.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label8.ForeColor = System.Drawing.Color.Silver;
            this.label8.Location = new System.Drawing.Point(17, 305);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(164, 20);
            this.label8.TabIndex = 35;
            this.label8.Text = "دسته بندی سطح دوم شیپور";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(17, 121);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(164, 20);
            this.label6.TabIndex = 35;
            this.label6.Text = "دسته بندی سطح سوم دیوار";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label9.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label9.ForeColor = System.Drawing.Color.Silver;
            this.label9.Location = new System.Drawing.Point(341, 250);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(164, 20);
            this.label9.TabIndex = 35;
            this.label9.Text = "گروه";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(17, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(164, 20);
            this.label7.TabIndex = 35;
            this.label7.Text = "دسته بندی سطح اول شیپور";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(43, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 20);
            this.label5.TabIndex = 35;
            this.label5.Text = "دسته بندی سطح دوم دیوار";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label17.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label17.ForeColor = System.Drawing.Color.Silver;
            this.label17.Location = new System.Drawing.Point(43, 11);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(138, 20);
            this.label17.TabIndex = 35;
            this.label17.Text = "دسته بندی سطح اول دیوار";
            // 
            // lineNumbersForRichText2
            // 
            this.lineNumbersForRichText2.AutoSizing = true;
            this.lineNumbersForRichText2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.lineNumbersForRichText2.BackgroundGradientAlphaColor = System.Drawing.Color.Transparent;
            this.lineNumbersForRichText2.BackgroundGradientBetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbersForRichText2.BackgroundGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbersForRichText2.BorderLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText2.BorderLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText2.BorderLinesThickness = 1F;
            this.lineNumbersForRichText2.DockSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Left;
            this.lineNumbersForRichText2.GridLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText2.GridLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText2.GridLinesThickness = 1F;
            this.lineNumbersForRichText2.LineNumbersAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbersForRichText2.LineNumbersAntiAlias = true;
            this.lineNumbersForRichText2.LineNumbersAsHexadecimal = false;
            this.lineNumbersForRichText2.LineNumbersClippedByItemRectangle = true;
            this.lineNumbersForRichText2.LineNumbersLeadingZeroes = true;
            this.lineNumbersForRichText2.LineNumbersOffset = new System.Drawing.Size(0, 0);
            this.lineNumbersForRichText2.Location = new System.Drawing.Point(197, 210);
            this.lineNumbersForRichText2.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbersForRichText2.MarginLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText2.MarginLinesSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Right;
            this.lineNumbersForRichText2.MarginLinesStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbersForRichText2.MarginLinesThickness = 1F;
            this.lineNumbersForRichText2.Name = "lineNumbersForRichText2";
            this.lineNumbersForRichText2.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbersForRichText2.ParentRichTextBox = this.txtDesc;
            this.lineNumbersForRichText2.SeeThroughMode = false;
            this.lineNumbersForRichText2.ShowBackgroundGradient = true;
            this.lineNumbersForRichText2.ShowBorderLines = true;
            this.lineNumbersForRichText2.ShowGridLines = true;
            this.lineNumbersForRichText2.ShowLineNumbers = true;
            this.lineNumbersForRichText2.ShowMarginLines = true;
            this.lineNumbersForRichText2.Size = new System.Drawing.Size(29, 34);
            this.lineNumbersForRichText2.TabIndex = 33;
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtDesc.ForeColor = System.Drawing.Color.Silver;
            this.txtDesc.Location = new System.Drawing.Point(227, 210);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(279, 34);
            this.txtDesc.TabIndex = 31;
            this.txtDesc.Text = "";
            // 
            // lineNumbersForRichText1
            // 
            this.lineNumbersForRichText1.AutoSizing = true;
            this.lineNumbersForRichText1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.lineNumbersForRichText1.BackgroundGradientAlphaColor = System.Drawing.Color.Transparent;
            this.lineNumbersForRichText1.BackgroundGradientBetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbersForRichText1.BackgroundGradientDirection = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbersForRichText1.BorderLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText1.BorderLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText1.BorderLinesThickness = 1F;
            this.lineNumbersForRichText1.DockSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Left;
            this.lineNumbersForRichText1.GridLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText1.GridLinesStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbersForRichText1.GridLinesThickness = 1F;
            this.lineNumbersForRichText1.LineNumbersAlignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbersForRichText1.LineNumbersAntiAlias = true;
            this.lineNumbersForRichText1.LineNumbersAsHexadecimal = false;
            this.lineNumbersForRichText1.LineNumbersClippedByItemRectangle = true;
            this.lineNumbersForRichText1.LineNumbersLeadingZeroes = true;
            this.lineNumbersForRichText1.LineNumbersOffset = new System.Drawing.Size(0, 0);
            this.lineNumbersForRichText1.Location = new System.Drawing.Point(197, 88);
            this.lineNumbersForRichText1.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbersForRichText1.MarginLinesColor = System.Drawing.Color.SlateGray;
            this.lineNumbersForRichText1.MarginLinesSide = LineNumbersControlForRichTextBox.LineNumbersForRichText.LineNumberDockSide.Right;
            this.lineNumbersForRichText1.MarginLinesStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbersForRichText1.MarginLinesThickness = 1F;
            this.lineNumbersForRichText1.Name = "lineNumbersForRichText1";
            this.lineNumbersForRichText1.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbersForRichText1.ParentRichTextBox = this.txtTitles;
            this.lineNumbersForRichText1.SeeThroughMode = false;
            this.lineNumbersForRichText1.ShowBackgroundGradient = true;
            this.lineNumbersForRichText1.ShowBorderLines = true;
            this.lineNumbersForRichText1.ShowGridLines = true;
            this.lineNumbersForRichText1.ShowLineNumbers = true;
            this.lineNumbersForRichText1.ShowMarginLines = true;
            this.lineNumbersForRichText1.Size = new System.Drawing.Size(29, 96);
            this.lineNumbersForRichText1.TabIndex = 32;
            // 
            // txtTitles
            // 
            this.txtTitles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtTitles.ForeColor = System.Drawing.Color.Silver;
            this.txtTitles.Location = new System.Drawing.Point(227, 88);
            this.txtTitles.Name = "txtTitles";
            this.txtTitles.Size = new System.Drawing.Size(279, 96);
            this.txtTitles.TabIndex = 31;
            this.txtTitles.Text = "";
            // 
            // txtPrice
            // 
            this.txtPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrice.ForeColor = System.Drawing.Color.Silver;
            this.txtPrice.Location = new System.Drawing.Point(197, 331);
            this.txtPrice.MaxLength = 3000;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(309, 27);
            this.txtPrice.TabIndex = 10;
            this.txtPrice.TextChanged += new System.EventHandler(this.txtPrice_TextChanged);
            this.txtPrice.Enter += new System.EventHandler(this.txtPrice_Enter);
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            this.txtPrice.Leave += new System.EventHandler(this.txtPrice_Leave);
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.ForeColor = System.Drawing.Color.Silver;
            this.txtName.Location = new System.Drawing.Point(197, 35);
            this.txtName.MaxLength = 3000;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(309, 27);
            this.txtName.TabIndex = 10;
            this.txtName.Enter += new System.EventHandler(this.txtName_Enter);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(367, 187);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 20);
            this.label3.TabIndex = 30;
            this.label3.Text = "توضیحات";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(367, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 20);
            this.label1.TabIndex = 30;
            this.label1.Text = "عناوین آگهی";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(353, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 20);
            this.label4.TabIndex = 30;
            this.label4.Text = "قیمت";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(353, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "نام آگهی";
            // 
            // btnInsImage
            // 
            this.btnInsImage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnInsImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInsImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnInsImage.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnInsImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInsImage.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnInsImage.Location = new System.Drawing.Point(12, 366);
            this.btnInsImage.Name = "btnInsImage";
            this.btnInsImage.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnInsImage.Size = new System.Drawing.Size(214, 25);
            this.btnInsImage.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnInsImage.TabIndex = 55699;
            this.btnInsImage.Text = "افزودن تصویر ...";
            this.btnInsImage.TextColor = System.Drawing.Color.Silver;
            this.btnInsImage.ThemeAware = true;
            this.btnInsImage.Click += new System.EventHandler(this.btnInsImage_Click);
            // 
            // btnDeleteImage
            // 
            this.btnDeleteImage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDeleteImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnDeleteImage.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnDeleteImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteImage.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnDeleteImage.Location = new System.Drawing.Point(12, 401);
            this.btnDeleteImage.Name = "btnDeleteImage";
            this.btnDeleteImage.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnDeleteImage.Size = new System.Drawing.Size(214, 25);
            this.btnDeleteImage.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnDeleteImage.TabIndex = 55700;
            this.btnDeleteImage.Text = "حدف تصویر";
            this.btnDeleteImage.TextColor = System.Drawing.Color.Silver;
            this.btnDeleteImage.ThemeAware = true;
            this.btnDeleteImage.Click += new System.EventHandler(this.btnDeleteImage_Click);
            // 
            // frmAds
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(980, 487);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.grpAccount);
            this.Controls.Add(this.btnDeleteImage);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnInsImage);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.uC_Date1);
            this.Controls.Add(this.fPanel);
            this.Controls.Add(this.trvGroup);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(996, 526);
            this.Name = "frmAds";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAds_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAds_KeyDown);
            this.contextMenuStrip1.ResumeLayout(false);
            this.grpAccount.ResumeLayout(false);
            this.grpAccount.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SheypoorCat2BingingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DivarCat3BingingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheypoorCat1BingingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DivarCat2BingingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DivarCat1BingingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trvGroup;
        private System.Windows.Forms.FlowLayoutPanel fPanel;
        private System.Windows.Forms.Label lblHeader;
        private UC_Date.UC_Date uC_Date1;
        public DevComponents.DotNetBar.ButtonX btnCancel;
        public DevComponents.DotNetBar.ButtonX btnFinish;
        private DevComponents.DotNetBar.Controls.Line line1;
        public DevComponents.DotNetBar.Controls.GroupPanel grpAccount;
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtDesc;
        private System.Windows.Forms.RichTextBox txtTitles;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtPrice;
        public System.Windows.Forms.Label label4;
        private LineNumbersControlForRichTextBox.LineNumbersForRichText lineNumbersForRichText1;
        private LineNumbersControlForRichTextBox.LineNumbersForRichText lineNumbersForRichText2;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cmbSheypoorCat2;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cmbDivarCat3;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cmbSheypoorCat1;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cmbDivarCat2;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cmbDivarCat1;
        public System.Windows.Forms.Label label8;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label label17;
        public DevComponents.DotNetBar.ButtonX btnInsImage;
        public DevComponents.DotNetBar.ButtonX btnDeleteImage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuInsGroup;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuInsAdv;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cmbGroup;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.BindingSource ParentBindingSource;
        private System.Windows.Forms.BindingSource DivarCat1BingingSource;
        private System.Windows.Forms.BindingSource DivarCat2BingingSource;
        private System.Windows.Forms.BindingSource DivarCat3BingingSource;
        private System.Windows.Forms.BindingSource SheypoorCat1BingingSource;
        private System.Windows.Forms.BindingSource SheypoorCat2BingingSource;
        public DevComponents.DotNetBar.ButtonX btnInsNewContent;
        public DevComponents.DotNetBar.ButtonX btnBack;
        public DevComponents.DotNetBar.ButtonX btnSaveInList;
        public DevComponents.DotNetBar.ButtonX btnClear;
    }
}