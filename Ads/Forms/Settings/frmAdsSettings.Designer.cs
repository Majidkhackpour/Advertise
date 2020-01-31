namespace Ads.Forms.Settings
{
    partial class frmAdsSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdsSettings));
            this.lblHeader = new System.Windows.Forms.Label();
            this.grpAccount = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.cmbDeleteDay = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.txtCat3 = new System.Windows.Forms.TextBox();
            this.btnSearchAdv = new DevComponents.DotNetBar.ButtonX();
            this.txtAdvAddress = new System.Windows.Forms.TextBox();
            this.txtUpdateDayCount = new System.Windows.Forms.TextBox();
            this.txtCountPic = new System.Windows.Forms.TextBox();
            this.txtAdvInIP = new System.Windows.Forms.TextBox();
            this.txtCat2 = new System.Windows.Forms.TextBox();
            this.txtAdvInMounth = new System.Windows.Forms.TextBox();
            this.txtCat1 = new System.Windows.Forms.TextBox();
            this.txtAdvInDay = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnFinish = new DevComponents.DotNetBar.ButtonX();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.uC_Date1 = new UC_Date.UC_Date();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.grpAccount.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeader.Font = new System.Drawing.Font("B Yekan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblHeader.ForeColor = System.Drawing.Color.Silver;
            this.lblHeader.Location = new System.Drawing.Point(192, 6);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(180, 24);
            this.lblHeader.TabIndex = 55691;
            this.lblHeader.Text = "تنظیمات آگهی";
            // 
            // grpAccount
            // 
            this.grpAccount.BackColor = System.Drawing.Color.White;
            this.grpAccount.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpAccount.Controls.Add(this.cmbDeleteDay);
            this.grpAccount.Controls.Add(this.txtCat3);
            this.grpAccount.Controls.Add(this.btnSearchAdv);
            this.grpAccount.Controls.Add(this.txtAdvAddress);
            this.grpAccount.Controls.Add(this.txtUpdateDayCount);
            this.grpAccount.Controls.Add(this.txtCountPic);
            this.grpAccount.Controls.Add(this.txtAdvInIP);
            this.grpAccount.Controls.Add(this.txtCat2);
            this.grpAccount.Controls.Add(this.txtAdvInMounth);
            this.grpAccount.Controls.Add(this.txtCat1);
            this.grpAccount.Controls.Add(this.txtAdvInDay);
            this.grpAccount.Controls.Add(this.label10);
            this.grpAccount.Controls.Add(this.label12);
            this.grpAccount.Controls.Add(this.label17);
            this.grpAccount.Controls.Add(this.label1);
            this.grpAccount.Controls.Add(this.label15);
            this.grpAccount.Controls.Add(this.label9);
            this.grpAccount.Controls.Add(this.label6);
            this.grpAccount.Controls.Add(this.label7);
            this.grpAccount.Controls.Add(this.label4);
            this.grpAccount.Controls.Add(this.label2);
            this.grpAccount.DisabledBackColor = System.Drawing.Color.Empty;
            this.grpAccount.Location = new System.Drawing.Point(12, 55);
            this.grpAccount.Name = "grpAccount";
            this.grpAccount.Size = new System.Drawing.Size(361, 313);
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
            // cmbDeleteDay
            // 
            this.cmbDeleteDay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbDeleteDay.DisplayMember = "Name";
            this.cmbDeleteDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDeleteDay.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
            this.cmbDeleteDay.ForeColor = System.Drawing.Color.Black;
            this.cmbDeleteDay.ItemHeight = 20;
            this.cmbDeleteDay.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4});
            this.cmbDeleteDay.Location = new System.Drawing.Point(17, 197);
            this.cmbDeleteDay.Name = "cmbDeleteDay";
            this.cmbDeleteDay.Size = new System.Drawing.Size(139, 28);
            this.cmbDeleteDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbDeleteDay.TabIndex = 7;
            this.cmbDeleteDay.ValueMember = "Guid";
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "یک روز";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "یک هفته";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "پانزده روز";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "یک ماه";
            // 
            // txtCat3
            // 
            this.txtCat3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCat3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtCat3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCat3.ForeColor = System.Drawing.Color.Silver;
            this.txtCat3.Location = new System.Drawing.Point(17, 143);
            this.txtCat3.MaxLength = 300;
            this.txtCat3.Name = "txtCat3";
            this.txtCat3.Size = new System.Drawing.Size(139, 27);
            this.txtCat3.TabIndex = 5;
            this.txtCat3.Enter += new System.EventHandler(this.txtCat3_Enter);
            this.txtCat3.Leave += new System.EventHandler(this.txtCat3_Leave);
            // 
            // btnSearchAdv
            // 
            this.btnSearchAdv.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearchAdv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnSearchAdv.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnSearchAdv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchAdv.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSearchAdv.Location = new System.Drawing.Point(165, 260);
            this.btnSearchAdv.Name = "btnSearchAdv";
            this.btnSearchAdv.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnSearchAdv.Size = new System.Drawing.Size(23, 27);
            this.btnSearchAdv.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnSearchAdv.TabIndex = 9;
            this.btnSearchAdv.Text = "...";
            this.btnSearchAdv.TextColor = System.Drawing.Color.Silver;
            this.btnSearchAdv.ThemeAware = true;
            this.btnSearchAdv.Click += new System.EventHandler(this.btnSearchAdv_Click);
            // 
            // txtAdvAddress
            // 
            this.txtAdvAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdvAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtAdvAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdvAddress.Enabled = false;
            this.txtAdvAddress.ForeColor = System.Drawing.Color.Silver;
            this.txtAdvAddress.Location = new System.Drawing.Point(194, 260);
            this.txtAdvAddress.MaxLength = 3000;
            this.txtAdvAddress.Name = "txtAdvAddress";
            this.txtAdvAddress.Size = new System.Drawing.Size(139, 27);
            this.txtAdvAddress.TabIndex = 8;
            this.txtAdvAddress.Enter += new System.EventHandler(this.txtAdvAddress_Enter);
            this.txtAdvAddress.Leave += new System.EventHandler(this.txtAdvAddress_Leave);
            // 
            // txtUpdateDayCount
            // 
            this.txtUpdateDayCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUpdateDayCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtUpdateDayCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUpdateDayCount.ForeColor = System.Drawing.Color.Silver;
            this.txtUpdateDayCount.Location = new System.Drawing.Point(17, 260);
            this.txtUpdateDayCount.MaxLength = 3000;
            this.txtUpdateDayCount.Name = "txtUpdateDayCount";
            this.txtUpdateDayCount.Size = new System.Drawing.Size(139, 27);
            this.txtUpdateDayCount.TabIndex = 10;
            this.txtUpdateDayCount.Enter += new System.EventHandler(this.txtUpdateDayCount_Enter);
            this.txtUpdateDayCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUpdateDayCount_KeyPress);
            this.txtUpdateDayCount.Leave += new System.EventHandler(this.txtUpdateDayCount_Leave);
            // 
            // txtCountPic
            // 
            this.txtCountPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCountPic.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtCountPic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCountPic.ForeColor = System.Drawing.Color.Silver;
            this.txtCountPic.Location = new System.Drawing.Point(194, 198);
            this.txtCountPic.MaxLength = 3000;
            this.txtCountPic.Name = "txtCountPic";
            this.txtCountPic.Size = new System.Drawing.Size(139, 27);
            this.txtCountPic.TabIndex = 6;
            this.txtCountPic.Enter += new System.EventHandler(this.txtCountPic_Enter);
            this.txtCountPic.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCountPic_KeyPress);
            this.txtCountPic.Leave += new System.EventHandler(this.txtCountPic_Leave);
            // 
            // txtAdvInIP
            // 
            this.txtAdvInIP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdvInIP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtAdvInIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdvInIP.ForeColor = System.Drawing.Color.Silver;
            this.txtAdvInIP.Location = new System.Drawing.Point(194, 143);
            this.txtAdvInIP.MaxLength = 3000;
            this.txtAdvInIP.Name = "txtAdvInIP";
            this.txtAdvInIP.Size = new System.Drawing.Size(139, 27);
            this.txtAdvInIP.TabIndex = 4;
            this.txtAdvInIP.Enter += new System.EventHandler(this.txtAdvInIP_Enter);
            this.txtAdvInIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdvInIP_KeyPress);
            this.txtAdvInIP.Leave += new System.EventHandler(this.txtAdvInIP_Leave);
            // 
            // txtCat2
            // 
            this.txtCat2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCat2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtCat2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCat2.ForeColor = System.Drawing.Color.Silver;
            this.txtCat2.Location = new System.Drawing.Point(17, 88);
            this.txtCat2.MaxLength = 300;
            this.txtCat2.Name = "txtCat2";
            this.txtCat2.Size = new System.Drawing.Size(139, 27);
            this.txtCat2.TabIndex = 3;
            this.txtCat2.Enter += new System.EventHandler(this.txtCat2_Enter);
            this.txtCat2.Leave += new System.EventHandler(this.txtCat2_Leave);
            // 
            // txtAdvInMounth
            // 
            this.txtAdvInMounth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdvInMounth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtAdvInMounth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdvInMounth.ForeColor = System.Drawing.Color.Silver;
            this.txtAdvInMounth.Location = new System.Drawing.Point(194, 88);
            this.txtAdvInMounth.MaxLength = 3000;
            this.txtAdvInMounth.Name = "txtAdvInMounth";
            this.txtAdvInMounth.Size = new System.Drawing.Size(139, 27);
            this.txtAdvInMounth.TabIndex = 2;
            this.txtAdvInMounth.Enter += new System.EventHandler(this.txtAdvInMounth_Enter);
            this.txtAdvInMounth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdvInMounth_KeyPress);
            this.txtAdvInMounth.Leave += new System.EventHandler(this.txtAdvInMounth_Leave);
            // 
            // txtCat1
            // 
            this.txtCat1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCat1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtCat1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCat1.ForeColor = System.Drawing.Color.Silver;
            this.txtCat1.Location = new System.Drawing.Point(17, 33);
            this.txtCat1.MaxLength = 300;
            this.txtCat1.Name = "txtCat1";
            this.txtCat1.Size = new System.Drawing.Size(139, 27);
            this.txtCat1.TabIndex = 1;
            this.txtCat1.Enter += new System.EventHandler(this.txtCat1_Enter);
            this.txtCat1.Leave += new System.EventHandler(this.txtCat1_Leave);
            // 
            // txtAdvInDay
            // 
            this.txtAdvInDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAdvInDay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtAdvInDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAdvInDay.ForeColor = System.Drawing.Color.Silver;
            this.txtAdvInDay.Location = new System.Drawing.Point(194, 33);
            this.txtAdvInDay.MaxLength = 11;
            this.txtAdvInDay.Name = "txtAdvInDay";
            this.txtAdvInDay.Size = new System.Drawing.Size(139, 27);
            this.txtAdvInDay.TabIndex = 0;
            this.txtAdvInDay.Enter += new System.EventHandler(this.txtAdvInDay_Enter);
            this.txtAdvInDay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAdvInDay_KeyPress);
            this.txtAdvInDay.Leave += new System.EventHandler(this.txtAdvInDay_Leave);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label10.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label10.ForeColor = System.Drawing.Color.Silver;
            this.label10.Location = new System.Drawing.Point(17, 118);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(138, 20);
            this.label10.TabIndex = 30;
            this.label10.Text = "دسته بندی سطح سه";
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label12.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label12.ForeColor = System.Drawing.Color.Silver;
            this.label12.Location = new System.Drawing.Point(213, 235);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 20);
            this.label12.TabIndex = 30;
            this.label12.Text = "آدرس آگهی ها";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label17.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label17.ForeColor = System.Drawing.Color.Silver;
            this.label17.Location = new System.Drawing.Point(17, 173);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(138, 20);
            this.label17.TabIndex = 30;
            this.label17.Text = "حذف آگهی ها تا";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(17, 235);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 20);
            this.label1.TabIndex = 30;
            this.label1.Text = "روز بروزرسانی";
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label15.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label15.ForeColor = System.Drawing.Color.Silver;
            this.label15.Location = new System.Drawing.Point(194, 173);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(138, 20);
            this.label15.TabIndex = 30;
            this.label15.Text = "تعداد تصویر هر آگهی";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label9.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label9.ForeColor = System.Drawing.Color.Silver;
            this.label9.Location = new System.Drawing.Point(194, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(138, 20);
            this.label9.TabIndex = 30;
            this.label9.Text = "تعداد آگهی در هر IP";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(17, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(138, 20);
            this.label6.TabIndex = 30;
            this.label6.Text = "دسته بندی سطح دو";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(194, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 20);
            this.label7.TabIndex = 30;
            this.label7.Text = "تعداد آگهی ارسال در ماه";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(3, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 20);
            this.label4.TabIndex = 30;
            this.label4.Text = "دسته بندی سطح یک";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(180, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "تعداد آگهی ارسالی در روز";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCancel.Location = new System.Drawing.Point(12, 393);
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
            // btnFinish
            // 
            this.btnFinish.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnFinish.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnFinish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinish.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnFinish.Location = new System.Drawing.Point(228, 393);
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
            // line1
            // 
            this.line1.ForeColor = System.Drawing.Color.Silver;
            this.line1.Location = new System.Drawing.Point(4, 370);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(399, 19);
            this.line1.TabIndex = 55695;
            this.line1.Text = "line1";
            this.line1.Thickness = 2;
            // 
            // uC_Date1
            // 
            this.uC_Date1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.uC_Date1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_Date1.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.uC_Date1.Location = new System.Drawing.Point(0, 0);
            this.uC_Date1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uC_Date1.Name = "uC_Date1";
            this.uC_Date1.Size = new System.Drawing.Size(383, 47);
            this.uC_Date1.TabIndex = 55696;
            // 
            // frmAdsSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(383, 427);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.uC_Date1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.grpAccount);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(399, 466);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(399, 466);
            this.Name = "frmAdsSettings";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmAdsSettings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAdsSettings_KeyDown);
            this.grpAccount.ResumeLayout(false);
            this.grpAccount.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeader;
        public DevComponents.DotNetBar.Controls.GroupPanel grpAccount;
        public System.Windows.Forms.TextBox txtAdvInIP;
        public System.Windows.Forms.TextBox txtAdvInMounth;
        public System.Windows.Forms.TextBox txtAdvInDay;
        public System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Label label2;
        public DevComponents.DotNetBar.Controls.ComboBoxEx cmbDeleteDay;
        public DevComponents.DotNetBar.ButtonX btnCancel;
        public DevComponents.DotNetBar.ButtonX btnFinish;
        private DevComponents.DotNetBar.Controls.Line line1;
        public System.Windows.Forms.TextBox txtCat3;
        public System.Windows.Forms.TextBox txtAdvAddress;
        public System.Windows.Forms.TextBox txtCountPic;
        public System.Windows.Forms.TextBox txtCat2;
        public System.Windows.Forms.TextBox txtCat1;
        public System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.Label label17;
        public System.Windows.Forms.Label label15;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label label4;
        public DevComponents.DotNetBar.ButtonX btnSearchAdv;
        private UC_Date.UC_Date uC_Date1;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        public System.Windows.Forms.TextBox txtUpdateDayCount;
        public System.Windows.Forms.Label label1;
    }
}