using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using ErrorHandler.Properties;

namespace ErrorHandler
{
    public partial class frmNotification : Form
    {
        private int x = 1;
        public frmNotification(string message,bool isSuccess)
        {
            InitializeComponent();
            TopMost = true;
            lblMessage.Text = message;
            LeftSide.BackgroundImage = isSuccess ? Resources.Like : Resources.DisLike;
        }

        public frmNotification(Exception ex)
        {
            InitializeComponent();
            lblMessage.Text = ex.Message;
            LeftSide.BackgroundImage = Resources.DisLike;
        }

        private void ClosingTimer_Tick(object sender, System.EventArgs e)
        {
            Close();
        }

        private void frmNotification_Load(object sender, System.EventArgs e)
        {
            Styler.Start();
            var player = new SoundPlayer(Resources.iPhone_tone);
            player.Play();
            ClosingTimer.Start();
        }

        private void Styler_Tick(object sender, System.EventArgs e)
        {
            x += 20;
            var workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - this.x, workingArea.Bottom - Size.Height - 30);
            if (this.Location.X == workingArea.Right - this.Size.Width
                || this.Location.X < workingArea.Right - this.Size.Width)
            {
                this.Styler.Stop();
            }
        }

        private void lblMessage_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
