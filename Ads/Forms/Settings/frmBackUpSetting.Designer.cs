namespace Ads.Forms.Settings
{
    partial class frmBackUpSetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBackUpSetting));
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtAutoSecond = new System.Windows.Forms.TextBox();
            this.btnFinish = new DevComponents.DotNetBar.ButtonX();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.grpAccount = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.chbAuto = new System.Windows.Forms.CheckBox();
            this.chbIsSentToTelegram = new System.Windows.Forms.CheckBox();
            this.line2 = new DevComponents.DotNetBar.Controls.Line();
            this.btnRestore = new DevComponents.DotNetBar.ButtonX();
            this.btnBackUp = new DevComponents.DotNetBar.ButtonX();
            this.label2 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.uC_Date1 = new UC_Date.UC_Date();
            this.chbIsSendToEmail = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.grpAccount.SuspendLayout();
            this.SuspendLayout();
            // 
            // line1
            // 
            this.line1.ForeColor = System.Drawing.Color.Silver;
            this.line1.Location = new System.Drawing.Point(0, 352);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(399, 19);
            this.line1.TabIndex = 55701;
            this.line1.Text = "line1";
            this.line1.Thickness = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnSearch.Location = new System.Drawing.Point(6, 28);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnSearch.Size = new System.Drawing.Size(23, 27);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "...";
            this.btnSearch.TextColor = System.Drawing.Color.Silver;
            this.btnSearch.ThemeAware = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Enabled = false;
            this.txtAddress.ForeColor = System.Drawing.Color.Silver;
            this.txtAddress.Location = new System.Drawing.Point(32, 28);
            this.txtAddress.MaxLength = 3000;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(319, 27);
            this.txtAddress.TabIndex = 8;
            // 
            // txtAutoSecond
            // 
            this.txtAutoSecond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAutoSecond.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtAutoSecond.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAutoSecond.ForeColor = System.Drawing.Color.Silver;
            this.txtAutoSecond.Location = new System.Drawing.Point(133, 121);
            this.txtAutoSecond.MaxLength = 3000;
            this.txtAutoSecond.Name = "txtAutoSecond";
            this.txtAutoSecond.Size = new System.Drawing.Size(98, 27);
            this.txtAutoSecond.TabIndex = 10;
            // 
            // btnFinish
            // 
            this.btnFinish.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFinish.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnFinish.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnFinish.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFinish.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnFinish.Location = new System.Drawing.Point(213, 370);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnFinish.Size = new System.Drawing.Size(145, 25);
            this.btnFinish.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnFinish.TabIndex = 55698;
            this.btnFinish.Text = "تایید (F5)";
            this.btnFinish.TextColor = System.Drawing.Color.Silver;
            this.btnFinish.ThemeAware = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label12.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label12.ForeColor = System.Drawing.Color.Silver;
            this.label12.Location = new System.Drawing.Point(128, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(222, 20);
            this.label12.TabIndex = 30;
            this.label12.Text = "آدرس ذخیره سازی فایل پشتیبان";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(212, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 20);
            this.label1.TabIndex = 30;
            this.label1.Text = "تهیه فایل پشتیبان هر";
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnCancel.Location = new System.Drawing.Point(20, 370);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnCancel.Size = new System.Drawing.Size(145, 25);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnCancel.TabIndex = 55699;
            this.btnCancel.Text = "انصراف (ESC)";
            this.btnCancel.TextColor = System.Drawing.Color.Silver;
            this.btnCancel.ThemeAware = true;
            // 
            // grpAccount
            // 
            this.grpAccount.BackColor = System.Drawing.Color.White;
            this.grpAccount.CanvasColor = System.Drawing.SystemColors.Control;
            this.grpAccount.Controls.Add(this.chbIsSendToEmail);
            this.grpAccount.Controls.Add(this.chbAuto);
            this.grpAccount.Controls.Add(this.chbIsSentToTelegram);
            this.grpAccount.Controls.Add(this.line2);
            this.grpAccount.Controls.Add(this.btnRestore);
            this.grpAccount.Controls.Add(this.btnBackUp);
            this.grpAccount.Controls.Add(this.btnSearch);
            this.grpAccount.Controls.Add(this.txtAddress);
            this.grpAccount.Controls.Add(this.txtEmailAddress);
            this.grpAccount.Controls.Add(this.txtAutoSecond);
            this.grpAccount.Controls.Add(this.label3);
            this.grpAccount.Controls.Add(this.label12);
            this.grpAccount.Controls.Add(this.label2);
            this.grpAccount.Controls.Add(this.label1);
            this.grpAccount.DisabledBackColor = System.Drawing.Color.Empty;
            this.grpAccount.Location = new System.Drawing.Point(9, 50);
            this.grpAccount.Name = "grpAccount";
            this.grpAccount.Size = new System.Drawing.Size(361, 300);
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
            this.grpAccount.TabIndex = 55697;
            // 
            // chbAuto
            // 
            this.chbAuto.AutoSize = true;
            this.chbAuto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.chbAuto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chbAuto.ForeColor = System.Drawing.Color.Silver;
            this.chbAuto.Location = new System.Drawing.Point(169, 91);
            this.chbAuto.Name = "chbAuto";
            this.chbAuto.Size = new System.Drawing.Size(182, 24);
            this.chbAuto.TabIndex = 51;
            this.chbAuto.Text = "پشتیبان گیری خودکار فعال باشد";
            this.chbAuto.UseVisualStyleBackColor = false;
            this.chbAuto.CheckedChanged += new System.EventHandler(this.chbAuto_CheckedChanged);
            // 
            // chbIsSentToTelegram
            // 
            this.chbIsSentToTelegram.AutoSize = true;
            this.chbIsSentToTelegram.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.chbIsSentToTelegram.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chbIsSentToTelegram.ForeColor = System.Drawing.Color.Silver;
            this.chbIsSentToTelegram.Location = new System.Drawing.Point(176, 61);
            this.chbIsSentToTelegram.Name = "chbIsSentToTelegram";
            this.chbIsSentToTelegram.Size = new System.Drawing.Size(175, 24);
            this.chbIsSentToTelegram.TabIndex = 52;
            this.chbIsSentToTelegram.Text = "ارسال فایل پشتیبان به تلگرام";
            this.chbIsSentToTelegram.UseVisualStyleBackColor = false;
            // 
            // line2
            // 
            this.line2.BackColor = System.Drawing.Color.Transparent;
            this.line2.ForeColor = System.Drawing.Color.Silver;
            this.line2.Location = new System.Drawing.Point(16, 236);
            this.line2.Name = "line2";
            this.line2.Size = new System.Drawing.Size(324, 10);
            this.line2.TabIndex = 55701;
            this.line2.Text = "line1";
            this.line2.Thickness = 2;
            // 
            // btnRestore
            // 
            this.btnRestore.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRestore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnRestore.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnRestore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestore.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnRestore.Location = new System.Drawing.Point(8, 258);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnRestore.Size = new System.Drawing.Size(145, 25);
            this.btnRestore.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnRestore.TabIndex = 55698;
            this.btnRestore.Text = "بازیابی فایل پشتیبان";
            this.btnRestore.TextColor = System.Drawing.Color.Silver;
            this.btnRestore.ThemeAware = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnBackUp
            // 
            this.btnBackUp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnBackUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(22)))), ((int)(((byte)(33)))));
            this.btnBackUp.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.btnBackUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBackUp.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.btnBackUp.Location = new System.Drawing.Point(201, 258);
            this.btnBackUp.Name = "btnBackUp";
            this.btnBackUp.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(14);
            this.btnBackUp.Size = new System.Drawing.Size(145, 25);
            this.btnBackUp.Style = DevComponents.DotNetBar.eDotNetBarStyle.VS2005;
            this.btnBackUp.TabIndex = 55698;
            this.btnBackUp.Text = "ایجاد فایل پشتیبان";
            this.btnBackUp.TextColor = System.Drawing.Color.Silver;
            this.btnBackUp.ThemeAware = true;
            this.btnBackUp.Click += new System.EventHandler(this.btnBackUp_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(32, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 30;
            this.label2.Text = "دقیقه";
            // 
            // lblHeader
            // 
            this.lblHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHeader.Font = new System.Drawing.Font("B Yekan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblHeader.ForeColor = System.Drawing.Color.Silver;
            this.lblHeader.Location = new System.Drawing.Point(-212, 1);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(180, 24);
            this.lblHeader.TabIndex = 55700;
            this.lblHeader.Text = "تنظیمات آگهی";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("B Yekan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(192, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 24);
            this.label5.TabIndex = 55703;
            this.label5.Text = "تنظیمات پشتیبان";
            // 
            // uC_Date1
            // 
            this.uC_Date1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.uC_Date1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_Date1.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.uC_Date1.Location = new System.Drawing.Point(0, 0);
            this.uC_Date1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uC_Date1.Name = "uC_Date1";
            this.uC_Date1.Size = new System.Drawing.Size(380, 47);
            this.uC_Date1.TabIndex = 55702;
            // 
            // chbIsSendToEmail
            // 
            this.chbIsSendToEmail.AutoSize = true;
            this.chbIsSendToEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.chbIsSendToEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chbIsSendToEmail.ForeColor = System.Drawing.Color.Silver;
            this.chbIsSendToEmail.Location = new System.Drawing.Point(180, 155);
            this.chbIsSendToEmail.Name = "chbIsSendToEmail";
            this.chbIsSendToEmail.Size = new System.Drawing.Size(171, 24);
            this.chbIsSendToEmail.TabIndex = 51;
            this.chbIsSendToEmail.Text = "ارسال فایل پشتیبان به ایمیل";
            this.chbIsSendToEmail.UseVisualStyleBackColor = false;
            this.chbIsSendToEmail.CheckedChanged += new System.EventHandler(this.chbIsSendToEmail_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(130, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 20);
            this.label3.TabIndex = 30;
            this.label3.Text = "آدرس ایمیل";
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtEmailAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmailAddress.ForeColor = System.Drawing.Color.Silver;
            this.txtEmailAddress.Location = new System.Drawing.Point(32, 205);
            this.txtEmailAddress.MaxLength = 3000;
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(316, 27);
            this.txtEmailAddress.TabIndex = 10;
            // 
            // frmBackUpSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(380, 404);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.uC_Date1);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grpAccount);
            this.Controls.Add(this.lblHeader);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBackUpSetting";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmBackUpSetting_Load);
            this.grpAccount.ResumeLayout(false);
            this.grpAccount.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.Line line1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        public DevComponents.DotNetBar.ButtonX btnSearch;
        public System.Windows.Forms.TextBox txtAddress;
        public System.Windows.Forms.TextBox txtAutoSecond;
        public DevComponents.DotNetBar.ButtonX btnFinish;
        public System.Windows.Forms.Label label12;
        public System.Windows.Forms.Label label1;
        public DevComponents.DotNetBar.ButtonX btnCancel;
        public DevComponents.DotNetBar.Controls.GroupPanel grpAccount;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label label5;
        private UC_Date.UC_Date uC_Date1;
        private System.Windows.Forms.CheckBox chbAuto;
        private System.Windows.Forms.CheckBox chbIsSentToTelegram;
        private DevComponents.DotNetBar.Controls.Line line2;
        public System.Windows.Forms.Label label2;
        public DevComponents.DotNetBar.ButtonX btnRestore;
        public DevComponents.DotNetBar.ButtonX btnBackUp;
        private System.Windows.Forms.CheckBox chbIsSendToEmail;
        public System.Windows.Forms.TextBox txtEmailAddress;
        public System.Windows.Forms.Label label3;
    }
}