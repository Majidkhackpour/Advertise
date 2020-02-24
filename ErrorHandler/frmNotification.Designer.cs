namespace ErrorHandler
{
    partial class frmNotification
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotification));
            this.LeftSide = new System.Windows.Forms.Panel();
            this.RightSide = new System.Windows.Forms.Panel();
            this.Styler = new System.Windows.Forms.Timer(this.components);
            this.ClosingTimer = new System.Windows.Forms.Timer(this.components);
            this.lblMessage = new System.Windows.Forms.Label();
            this.RightSide.SuspendLayout();
            this.SuspendLayout();
            // 
            // LeftSide
            // 
            this.LeftSide.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.LeftSide.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LeftSide.Dock = System.Windows.Forms.DockStyle.Left;
            this.LeftSide.Location = new System.Drawing.Point(0, 0);
            this.LeftSide.Name = "LeftSide";
            this.LeftSide.Size = new System.Drawing.Size(72, 69);
            this.LeftSide.TabIndex = 2;
            // 
            // RightSide
            // 
            this.RightSide.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.RightSide.Controls.Add(this.lblMessage);
            this.RightSide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RightSide.Location = new System.Drawing.Point(0, 0);
            this.RightSide.Name = "RightSide";
            this.RightSide.Size = new System.Drawing.Size(430, 69);
            this.RightSide.TabIndex = 3;
            // 
            // Styler
            // 
            this.Styler.Interval = 1;
            this.Styler.Tick += new System.EventHandler(this.Styler_Tick);
            // 
            // ClosingTimer
            // 
            this.ClosingTimer.Interval = 4000;
            this.ClosingTimer.Tick += new System.EventHandler(this.ClosingTimer_Tick);
            // 
            // lblMessage
            // 
            this.lblMessage.ForeColor = System.Drawing.Color.Silver;
            this.lblMessage.Location = new System.Drawing.Point(78, 9);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(340, 51);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "توضیحات";
            this.lblMessage.Click += new System.EventHandler(this.lblMessage_Click);
            // 
            // frmNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 69);
            this.Controls.Add(this.LeftSide);
            this.Controls.Add(this.RightSide);
            this.Font = new System.Drawing.Font("B Yekan", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmNotification";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.frmNotification_Load);
            this.RightSide.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel LeftSide;
        private System.Windows.Forms.Panel RightSide;
        private System.Windows.Forms.Timer Styler;
        private System.Windows.Forms.Timer ClosingTimer;
        private System.Windows.Forms.Label lblMessage;
    }
}