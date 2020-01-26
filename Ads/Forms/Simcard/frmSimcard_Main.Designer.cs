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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSimcard_Main));
            this.grpAccount = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.uC_Date1 = new UC_Date.UC_Date();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtOwner = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.btnFinish = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.cmbOperator = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.grpAccount.SuspendLayout();
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
            this.grpAccount.Location = new System.Drawing.Point(12, 55);
            this.grpAccount.Name = "grpAccount";
            this.grpAccount.Size = new System.Drawing.Size(357, 246);
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
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(276, 8);
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
            this.lblHeader.Location = new System.Drawing.Point(191, 6);
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
            this.uC_Date1.Size = new System.Drawing.Size(381, 47);
            this.uC_Date1.TabIndex = 55688;
            // 
            // txtNumber
            // 
            this.txtNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNumber.ForeColor = System.Drawing.Color.Silver;
            this.txtNumber.Location = new System.Drawing.Point(3, 33);
            this.txtNumber.MaxLength = 11;
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(344, 27);
            this.txtNumber.TabIndex = 0;
            this.txtNumber.Enter += new System.EventHandler(this.txtNumber_Enter);
            this.txtNumber.Leave += new System.EventHandler(this.txtNumber_Leave);
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(276, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 20);
            this.label7.TabIndex = 30;
            this.label7.Text = "مالک";
            // 
            // txtOwner
            // 
            this.txtOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOwner.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtOwner.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOwner.ForeColor = System.Drawing.Color.Silver;
            this.txtOwner.Location = new System.Drawing.Point(3, 88);
            this.txtOwner.MaxLength = 3000;
            this.txtOwner.Name = "txtOwner";
            this.txtOwner.Size = new System.Drawing.Size(344, 27);
            this.txtOwner.TabIndex = 1;
            this.txtOwner.Enter += new System.EventHandler(this.txtOwner_Enter);
            this.txtOwner.Leave += new System.EventHandler(this.txtOwner_Leave);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label8.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label8.ForeColor = System.Drawing.Color.Silver;
            this.label8.Location = new System.Drawing.Point(276, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 20);
            this.label8.TabIndex = 30;
            this.label8.Text = "اپراتور";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label9.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label9.ForeColor = System.Drawing.Color.Silver;
            this.label9.Location = new System.Drawing.Point(276, 173);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 20);
            this.label9.TabIndex = 30;
            this.label9.Text = "نام کاربری";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(33)))), ((int)(((byte)(43)))));
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.ForeColor = System.Drawing.Color.Silver;
            this.txtUserName.Location = new System.Drawing.Point(3, 198);
            this.txtUserName.MaxLength = 3000;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(344, 27);
            this.txtUserName.TabIndex = 3;
            this.txtUserName.Enter += new System.EventHandler(this.txtUserName_Enter);
            this.txtUserName.Leave += new System.EventHandler(this.txtUserName_Leave);
            // 
            // line1
            // 
            this.line1.ForeColor = System.Drawing.Color.Silver;
            this.line1.Location = new System.Drawing.Point(0, 307);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(381, 17);
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
            this.btnFinish.Location = new System.Drawing.Point(217, 330);
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
            this.btnCancel.Location = new System.Drawing.Point(12, 330);
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
            // frmSimcard_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(381, 363);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.uC_Date1);
            this.Controls.Add(this.grpAccount);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(397, 402);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(397, 402);
            this.Name = "frmSimcard_Main";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmSimcard_Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSimcard_Main_KeyDown);
            this.grpAccount.ResumeLayout(false);
            this.grpAccount.PerformLayout();
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
    }
}