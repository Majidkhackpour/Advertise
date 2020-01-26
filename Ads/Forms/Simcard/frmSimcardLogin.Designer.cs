namespace Ads.Forms.Simcard
{
    partial class frmSimcardLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSimcardLogin));
            this.uC_Date1 = new UC_Date.UC_Date();
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label1 = new System.Windows.Forms.Label();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.lblCounter = new System.Windows.Forms.Label();
            this.DGrid = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.guidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Radif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ownerNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateSabtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nextUseDivarDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nextUseSheypoorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nextUseDivarChatDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dg_Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.operatorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuLoginDivar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoginChat = new System.Windows.Forms.ToolStripMenuItem();
            this.LogInBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogInBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // uC_Date1
            // 
            this.uC_Date1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.uC_Date1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uC_Date1.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.uC_Date1.Location = new System.Drawing.Point(0, 0);
            this.uC_Date1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uC_Date1.Name = "uC_Date1";
            this.uC_Date1.Size = new System.Drawing.Size(784, 47);
            this.uC_Date1.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSearch.Border.Class = "TextBoxBorder";
            this.txtSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSearch.Location = new System.Drawing.Point(66, 56);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PreventEnterBeep = true;
            this.txtSearch.Size = new System.Drawing.Size(652, 27);
            this.txtSearch.TabIndex = 55681;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch.WatermarkText = "مورد جستجو را وارد نمایید ...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("B Yekan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(407, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(364, 24);
            this.label1.TabIndex = 55680;
            this.label1.Text = "لاگین سیمکارت ها";
            // 
            // line1
            // 
            this.line1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.line1.ForeColor = System.Drawing.Color.Silver;
            this.line1.Location = new System.Drawing.Point(-65, 515);
            this.line1.Margin = new System.Windows.Forms.Padding(4);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(915, 12);
            this.line1.TabIndex = 55683;
            this.line1.Text = "line1";
            this.line1.Thickness = 2;
            // 
            // lblCounter
            // 
            this.lblCounter.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblCounter.Font = new System.Drawing.Font("B Yekan", 12F);
            this.lblCounter.ForeColor = System.Drawing.Color.Silver;
            this.lblCounter.Location = new System.Drawing.Point(197, 531);
            this.lblCounter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(85, 24);
            this.lblCounter.TabIndex = 55684;
            this.lblCounter.Text = "00";
            this.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DGrid
            // 
            this.DGrid.AllowUserToAddRows = false;
            this.DGrid.AllowUserToDeleteRows = false;
            this.DGrid.AllowUserToResizeColumns = false;
            this.DGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.DGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGrid.AutoGenerateColumns = false;
            this.DGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.guidDataGridViewTextBoxColumn,
            this.Radif,
            this.ownerNameDataGridViewTextBoxColumn,
            this.dateSabtDataGridViewTextBoxColumn,
            this.nextUseDivarDataGridViewTextBoxColumn,
            this.nextUseSheypoorDataGridViewTextBoxColumn,
            this.nextUseDivarChatDataGridViewTextBoxColumn,
            this.dg_Number,
            this.statusDataGridViewCheckBoxColumn,
            this.operatorDataGridViewTextBoxColumn,
            this.userNameDataGridViewTextBoxColumn});
            this.DGrid.ContextMenuStrip = this.contextMenuStrip1;
            this.DGrid.DataSource = this.LogInBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGrid.DefaultCellStyle = dataGridViewCellStyle3;
            this.DGrid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.DGrid.Location = new System.Drawing.Point(8, 100);
            this.DGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DGrid.Name = "DGrid";
            this.DGrid.ReadOnly = true;
            this.DGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.DGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.DGrid.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.DGrid.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.DGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGrid.Size = new System.Drawing.Size(770, 404);
            this.DGrid.TabIndex = 55682;
            this.DGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DGrid_CellFormatting);
            // 
            // guidDataGridViewTextBoxColumn
            // 
            this.guidDataGridViewTextBoxColumn.DataPropertyName = "Guid";
            this.guidDataGridViewTextBoxColumn.HeaderText = "Guid";
            this.guidDataGridViewTextBoxColumn.Name = "guidDataGridViewTextBoxColumn";
            this.guidDataGridViewTextBoxColumn.ReadOnly = true;
            this.guidDataGridViewTextBoxColumn.Visible = false;
            // 
            // Radif
            // 
            this.Radif.HeaderText = "ردیف";
            this.Radif.Name = "Radif";
            this.Radif.ReadOnly = true;
            this.Radif.Width = 50;
            // 
            // ownerNameDataGridViewTextBoxColumn
            // 
            this.ownerNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ownerNameDataGridViewTextBoxColumn.DataPropertyName = "OwnerName";
            this.ownerNameDataGridViewTextBoxColumn.HeaderText = "مالک";
            this.ownerNameDataGridViewTextBoxColumn.Name = "ownerNameDataGridViewTextBoxColumn";
            this.ownerNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateSabtDataGridViewTextBoxColumn
            // 
            this.dateSabtDataGridViewTextBoxColumn.DataPropertyName = "DateSabt";
            this.dateSabtDataGridViewTextBoxColumn.HeaderText = "DateSabt";
            this.dateSabtDataGridViewTextBoxColumn.Name = "dateSabtDataGridViewTextBoxColumn";
            this.dateSabtDataGridViewTextBoxColumn.ReadOnly = true;
            this.dateSabtDataGridViewTextBoxColumn.Visible = false;
            // 
            // nextUseDivarDataGridViewTextBoxColumn
            // 
            this.nextUseDivarDataGridViewTextBoxColumn.DataPropertyName = "NextUseDivar";
            this.nextUseDivarDataGridViewTextBoxColumn.HeaderText = "NextUseDivar";
            this.nextUseDivarDataGridViewTextBoxColumn.Name = "nextUseDivarDataGridViewTextBoxColumn";
            this.nextUseDivarDataGridViewTextBoxColumn.ReadOnly = true;
            this.nextUseDivarDataGridViewTextBoxColumn.Visible = false;
            // 
            // nextUseSheypoorDataGridViewTextBoxColumn
            // 
            this.nextUseSheypoorDataGridViewTextBoxColumn.DataPropertyName = "NextUseSheypoor";
            this.nextUseSheypoorDataGridViewTextBoxColumn.HeaderText = "NextUseSheypoor";
            this.nextUseSheypoorDataGridViewTextBoxColumn.Name = "nextUseSheypoorDataGridViewTextBoxColumn";
            this.nextUseSheypoorDataGridViewTextBoxColumn.ReadOnly = true;
            this.nextUseSheypoorDataGridViewTextBoxColumn.Visible = false;
            // 
            // nextUseDivarChatDataGridViewTextBoxColumn
            // 
            this.nextUseDivarChatDataGridViewTextBoxColumn.DataPropertyName = "NextUseDivarChat";
            this.nextUseDivarChatDataGridViewTextBoxColumn.HeaderText = "NextUseDivarChat";
            this.nextUseDivarChatDataGridViewTextBoxColumn.Name = "nextUseDivarChatDataGridViewTextBoxColumn";
            this.nextUseDivarChatDataGridViewTextBoxColumn.ReadOnly = true;
            this.nextUseDivarChatDataGridViewTextBoxColumn.Visible = false;
            // 
            // dg_Number
            // 
            this.dg_Number.DataPropertyName = "Number";
            this.dg_Number.HeaderText = "شماره";
            this.dg_Number.Name = "dg_Number";
            this.dg_Number.ReadOnly = true;
            this.dg_Number.Width = 150;
            // 
            // statusDataGridViewCheckBoxColumn
            // 
            this.statusDataGridViewCheckBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewCheckBoxColumn.HeaderText = "Status";
            this.statusDataGridViewCheckBoxColumn.Name = "statusDataGridViewCheckBoxColumn";
            this.statusDataGridViewCheckBoxColumn.ReadOnly = true;
            this.statusDataGridViewCheckBoxColumn.Visible = false;
            // 
            // operatorDataGridViewTextBoxColumn
            // 
            this.operatorDataGridViewTextBoxColumn.DataPropertyName = "Operator";
            this.operatorDataGridViewTextBoxColumn.HeaderText = "اپراتور";
            this.operatorDataGridViewTextBoxColumn.Name = "operatorDataGridViewTextBoxColumn";
            this.operatorDataGridViewTextBoxColumn.ReadOnly = true;
            this.operatorDataGridViewTextBoxColumn.Width = 110;
            // 
            // userNameDataGridViewTextBoxColumn
            // 
            this.userNameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
            this.userNameDataGridViewTextBoxColumn.HeaderText = "UserName";
            this.userNameDataGridViewTextBoxColumn.Name = "userNameDataGridViewTextBoxColumn";
            this.userNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.userNameDataGridViewTextBoxColumn.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.contextMenuStrip1.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLoginDivar,
            this.mnuLoginChat});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 52);
            // 
            // mnuLoginDivar
            // 
            this.mnuLoginDivar.ForeColor = System.Drawing.Color.DarkGray;
            this.mnuLoginDivar.Name = "mnuLoginDivar";
            this.mnuLoginDivar.Size = new System.Drawing.Size(154, 24);
            this.mnuLoginDivar.Text = "لاگین دیوار";
            this.mnuLoginDivar.Click += new System.EventHandler(this.mnuLoginDivar_Click);
            // 
            // mnuLoginChat
            // 
            this.mnuLoginChat.ForeColor = System.Drawing.Color.Silver;
            this.mnuLoginChat.Name = "mnuLoginChat";
            this.mnuLoginChat.Size = new System.Drawing.Size(154, 24);
            this.mnuLoginChat.Text = "لاگین چت دیوار";
            this.mnuLoginChat.Click += new System.EventHandler(this.mnuLoginChat_Click);
            // 
            // LogInBindingSource
            // 
            this.LogInBindingSource.DataSource = typeof(BussinesLayer.SimcardBussines);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.Font = new System.Drawing.Font("B Yekan", 12F);
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(328, 531);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 24);
            this.label2.TabIndex = 55685;
            this.label2.Text = "تعداد سیمکارت ثبت شده:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmSimcardLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.DGrid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.uC_Date1);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "frmSimcardLogin";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSimcardLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSimcardLogin_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGrid)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogInBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UC_Date.UC_Date uC_Date1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.Controls.Line line1;
        private System.Windows.Forms.Label lblCounter;
        private DevComponents.DotNetBar.Controls.DataGridViewX DGrid;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.BindingSource LogInBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem mnuLoginDivar;
        private System.Windows.Forms.ToolStripMenuItem mnuLoginChat;
        private System.Windows.Forms.DataGridViewTextBoxColumn guidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Radif;
        private System.Windows.Forms.DataGridViewTextBoxColumn ownerNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateSabtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nextUseDivarDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nextUseSheypoorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nextUseDivarChatDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dg_Number;
        private System.Windows.Forms.DataGridViewCheckBoxColumn statusDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn operatorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
    }
}