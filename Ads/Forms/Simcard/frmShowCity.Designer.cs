namespace Ads.Forms.Simcard
{
    partial class frmShowCity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowCity));
            this.label2 = new System.Windows.Forms.Label();
            this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblDesc = new System.Windows.Forms.Label();
            this.line1 = new DevComponents.DotNetBar.Controls.Line();
            this.lblCounter = new System.Windows.Forms.Label();
            this.dgDivar = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.Radif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgDivarCityGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateSabtDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isCheckedDataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.DivarBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.uC_Date1 = new UC_Date.UC_Date();
            this.dgSheypoor = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.SheypoorBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Radif1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgSheypoorCityGuid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateSabtDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateGuidDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.isCheckedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.stateNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgDivar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DivarBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSheypoor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheypoorBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.Font = new System.Drawing.Font("B Yekan", 12F);
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(328, 534);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(255, 24);
            this.label2.TabIndex = 55692;
            this.label2.Text = "تعداد شهر ثبت شده:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.txtSearch.Location = new System.Drawing.Point(66, 59);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PreventEnterBeep = true;
            this.txtSearch.Size = new System.Drawing.Size(652, 27);
            this.txtSearch.TabIndex = 55688;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch.WatermarkText = "مورد جستجو را وارد نمایید ...";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblDesc
            // 
            this.lblDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDesc.Font = new System.Drawing.Font("B Yekan", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.lblDesc.ForeColor = System.Drawing.Color.Silver;
            this.lblDesc.Location = new System.Drawing.Point(407, 8);
            this.lblDesc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(364, 24);
            this.lblDesc.TabIndex = 55687;
            this.lblDesc.Text = "انتخاب شهر";
            // 
            // line1
            // 
            this.line1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.line1.ForeColor = System.Drawing.Color.Silver;
            this.line1.Location = new System.Drawing.Point(-65, 518);
            this.line1.Margin = new System.Windows.Forms.Padding(4);
            this.line1.Name = "line1";
            this.line1.Size = new System.Drawing.Size(915, 12);
            this.line1.TabIndex = 55690;
            this.line1.Text = "line1";
            this.line1.Thickness = 2;
            // 
            // lblCounter
            // 
            this.lblCounter.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblCounter.Font = new System.Drawing.Font("B Yekan", 12F);
            this.lblCounter.ForeColor = System.Drawing.Color.Silver;
            this.lblCounter.Location = new System.Drawing.Point(197, 534);
            this.lblCounter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(85, 24);
            this.lblCounter.TabIndex = 55691;
            this.lblCounter.Text = "00";
            this.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgDivar
            // 
            this.dgDivar.AllowUserToAddRows = false;
            this.dgDivar.AllowUserToDeleteRows = false;
            this.dgDivar.AllowUserToResizeColumns = false;
            this.dgDivar.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgDivar.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgDivar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgDivar.AutoGenerateColumns = false;
            this.dgDivar.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDivar.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgDivar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDivar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Radif,
            this.dgDivarCityGuid,
            this.dateSabtDataGridViewTextBoxColumn1,
            this.statusDataGridViewCheckBoxColumn1,
            this.nameDataGridViewTextBoxColumn1,
            this.isCheckedDataGridViewCheckBoxColumn1});
            this.dgDivar.DataSource = this.DivarBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDivar.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgDivar.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgDivar.Location = new System.Drawing.Point(12, 103);
            this.dgDivar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgDivar.Name = "dgDivar";
            this.dgDivar.ReadOnly = true;
            this.dgDivar.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgDivar.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgDivar.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgDivar.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.dgDivar.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgDivar.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgDivar.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDivar.Size = new System.Drawing.Size(766, 404);
            this.dgDivar.TabIndex = 55689;
            this.dgDivar.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgDivar_CellFormatting);
            this.dgDivar.DoubleClick += new System.EventHandler(this.dgDivar_DoubleClick);
            // 
            // Radif
            // 
            this.Radif.HeaderText = "ردیف";
            this.Radif.Name = "Radif";
            this.Radif.ReadOnly = true;
            this.Radif.Width = 50;
            // 
            // dgDivarCityGuid
            // 
            this.dgDivarCityGuid.DataPropertyName = "Guid";
            this.dgDivarCityGuid.HeaderText = "Guid";
            this.dgDivarCityGuid.Name = "dgDivarCityGuid";
            this.dgDivarCityGuid.ReadOnly = true;
            this.dgDivarCityGuid.Visible = false;
            // 
            // dateSabtDataGridViewTextBoxColumn1
            // 
            this.dateSabtDataGridViewTextBoxColumn1.DataPropertyName = "DateSabt";
            this.dateSabtDataGridViewTextBoxColumn1.HeaderText = "DateSabt";
            this.dateSabtDataGridViewTextBoxColumn1.Name = "dateSabtDataGridViewTextBoxColumn1";
            this.dateSabtDataGridViewTextBoxColumn1.ReadOnly = true;
            this.dateSabtDataGridViewTextBoxColumn1.Visible = false;
            // 
            // statusDataGridViewCheckBoxColumn1
            // 
            this.statusDataGridViewCheckBoxColumn1.DataPropertyName = "Status";
            this.statusDataGridViewCheckBoxColumn1.HeaderText = "Status";
            this.statusDataGridViewCheckBoxColumn1.Name = "statusDataGridViewCheckBoxColumn1";
            this.statusDataGridViewCheckBoxColumn1.ReadOnly = true;
            this.statusDataGridViewCheckBoxColumn1.Visible = false;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            this.nameDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn1.HeaderText = "عنوان";
            this.nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            this.nameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // isCheckedDataGridViewCheckBoxColumn1
            // 
            this.isCheckedDataGridViewCheckBoxColumn1.DataPropertyName = "Is_Checked";
            this.isCheckedDataGridViewCheckBoxColumn1.HeaderText = "Is_Checked";
            this.isCheckedDataGridViewCheckBoxColumn1.Name = "isCheckedDataGridViewCheckBoxColumn1";
            this.isCheckedDataGridViewCheckBoxColumn1.ReadOnly = true;
            this.isCheckedDataGridViewCheckBoxColumn1.Visible = false;
            // 
            // DivarBindingSource
            // 
            this.DivarBindingSource.DataSource = typeof(BussinesLayer.DivarCityBussines);
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
            this.uC_Date1.TabIndex = 55686;
            // 
            // dgSheypoor
            // 
            this.dgSheypoor.AllowUserToAddRows = false;
            this.dgSheypoor.AllowUserToDeleteRows = false;
            this.dgSheypoor.AllowUserToResizeColumns = false;
            this.dgSheypoor.AllowUserToResizeRows = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dgSheypoor.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgSheypoor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSheypoor.AutoGenerateColumns = false;
            this.dgSheypoor.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSheypoor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgSheypoor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSheypoor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Radif1,
            this.dgSheypoorCityGuid,
            this.dateSabtDataGridViewTextBoxColumn,
            this.statusDataGridViewCheckBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.stateGuidDataGridViewTextBoxColumn,
            this.isCheckedDataGridViewCheckBoxColumn,
            this.stateNameDataGridViewTextBoxColumn});
            this.dgSheypoor.DataSource = this.SheypoorBindingSource;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgSheypoor.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgSheypoor.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgSheypoor.Location = new System.Drawing.Point(12, 103);
            this.dgSheypoor.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgSheypoor.Name = "dgSheypoor";
            this.dgSheypoor.ReadOnly = true;
            this.dgSheypoor.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dgSheypoor.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSheypoor.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgSheypoor.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.dgSheypoor.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgSheypoor.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dgSheypoor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSheypoor.Size = new System.Drawing.Size(766, 406);
            this.dgSheypoor.TabIndex = 55689;
            this.dgSheypoor.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgSheypoor_CellFormatting);
            this.dgSheypoor.DoubleClick += new System.EventHandler(this.dgSheypoor_DoubleClick);
            // 
            // SheypoorBindingSource
            // 
            this.SheypoorBindingSource.DataSource = typeof(BussinesLayer.SheypoorCityBussines);
            // 
            // Radif1
            // 
            this.Radif1.HeaderText = "ردیف";
            this.Radif1.Name = "Radif1";
            this.Radif1.ReadOnly = true;
            this.Radif1.Width = 50;
            // 
            // dgSheypoorCityGuid
            // 
            this.dgSheypoorCityGuid.DataPropertyName = "Guid";
            this.dgSheypoorCityGuid.HeaderText = "Guid";
            this.dgSheypoorCityGuid.Name = "dgSheypoorCityGuid";
            this.dgSheypoorCityGuid.ReadOnly = true;
            this.dgSheypoorCityGuid.Visible = false;
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
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.HeaderText = "عنوان";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stateGuidDataGridViewTextBoxColumn
            // 
            this.stateGuidDataGridViewTextBoxColumn.DataPropertyName = "StateGuid";
            this.stateGuidDataGridViewTextBoxColumn.HeaderText = "StateGuid";
            this.stateGuidDataGridViewTextBoxColumn.Name = "stateGuidDataGridViewTextBoxColumn";
            this.stateGuidDataGridViewTextBoxColumn.ReadOnly = true;
            this.stateGuidDataGridViewTextBoxColumn.Visible = false;
            // 
            // isCheckedDataGridViewCheckBoxColumn
            // 
            this.isCheckedDataGridViewCheckBoxColumn.DataPropertyName = "Is_Checked";
            this.isCheckedDataGridViewCheckBoxColumn.HeaderText = "Is_Checked";
            this.isCheckedDataGridViewCheckBoxColumn.Name = "isCheckedDataGridViewCheckBoxColumn";
            this.isCheckedDataGridViewCheckBoxColumn.ReadOnly = true;
            this.isCheckedDataGridViewCheckBoxColumn.Visible = false;
            // 
            // stateNameDataGridViewTextBoxColumn
            // 
            this.stateNameDataGridViewTextBoxColumn.DataPropertyName = "StateName";
            this.stateNameDataGridViewTextBoxColumn.HeaderText = "استان";
            this.stateNameDataGridViewTextBoxColumn.Name = "stateNameDataGridViewTextBoxColumn";
            this.stateNameDataGridViewTextBoxColumn.ReadOnly = true;
            this.stateNameDataGridViewTextBoxColumn.Width = 200;
            // 
            // frmShowCity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(47)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.line1);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.uC_Date1);
            this.Controls.Add(this.dgSheypoor);
            this.Controls.Add(this.dgDivar);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmShowCity";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmShowCity_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmShowCity_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgDivar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DivarBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgSheypoor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SheypoorBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource DivarBindingSource;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
        private System.Windows.Forms.Label lblDesc;
        private DevComponents.DotNetBar.Controls.Line line1;
        private System.Windows.Forms.Label lblCounter;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgDivar;
        private UC_Date.UC_Date uC_Date1;
        private System.Windows.Forms.BindingSource SheypoorBindingSource;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgSheypoor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Radif;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgDivarCityGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateSabtDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn statusDataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCheckedDataGridViewCheckBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Radif1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgSheypoorCityGuid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateSabtDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn statusDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateGuidDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isCheckedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateNameDataGridViewTextBoxColumn;
    }
}