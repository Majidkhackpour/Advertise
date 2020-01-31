using System;
using System.Windows.Forms;
using Ads.Classes;
using Ads.Forms.Settings;
using Ads.Forms.Simcard;
using FMessegeBox;

namespace Ads.Forms.Mains
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, System.EventArgs e)
        {
            try
            {
                picDivar.Image = Properties.Resources.ir_divar_512x512;
                picSheypoor.Image = Properties.Resources.Sheypoor_portrate;
                picManager.Image = Properties.Resources._03;
                picLogIn.Image = Properties.Resources._19;
                picLogo.Image = Properties.Resources.AradPngpng;
                picDivarChat.Image = Properties.Resources.chat;
                ToolTip tt = new ToolTip();
                tt.SetToolTip(picManager,"پنل مدیریت");
                tt.SetToolTip(picDivar, "ارسال آگهی به دیوار");
                tt.SetToolTip(picSheypoor, "ارسال آگهی به شیپور");
                tt.SetToolTip(picLogIn, "لاگین");
                tt.SetToolTip(picDivarChat, "چت دیوار");
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        #region lblManage
        private void lblDivar_MouseEnter(object sender, EventArgs e)
        {
            lblSetter.GotFocose(lblDivar);
        }

        private void lblManager_MouseEnter(object sender, EventArgs e)
        {
            lblSetter.GotFocose(lblManager);
        }

        private void lblSheypoor_MouseEnter(object sender, EventArgs e)
        {
            lblSetter.GotFocose(lblSheypoor);
        }

        private void lblDivarLogIn_MouseEnter(object sender, EventArgs e)
        {
            lblSetter.GotFocose(lblLogIn);
        }

        private void lblDivarLogIn_MouseLeave(object sender, EventArgs e)
        {
            lblSetter.LostFocose(lblLogIn);
        }

        private void lblSheypoor_MouseLeave(object sender, EventArgs e)
        {
            lblSetter.LostFocose(lblSheypoor);
        }

        private void lblDivar_MouseLeave(object sender, EventArgs e)
        {
            lblSetter.LostFocose(lblDivar);
        }

        private void lblManager_MouseLeave(object sender, EventArgs e)
        {
            lblSetter.LostFocose(lblManager);
        }
        #endregion

        private async void picDivar_Click(object sender, EventArgs e)
        {
            try
            {
                var divar = await DivarAdv.GetInstance();
                await divar.StartRegisterAdv();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void picLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                new frmSimcardLogin().ShowDialog();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void lblLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                new frmSimcardLogin().ShowDialog();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void picManager_Click(object sender, EventArgs e)
        {
            try
            {
                new frmManagerMain().ShowDialog();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void lblManager_Click(object sender, EventArgs e)
        {
            try
            {
                new frmManagerMain().ShowDialog();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void picSheypoor_Click(object sender, EventArgs e)
        {
            FarsiMessegeBox.Show("ارسال آگهی شیپور در ورژن بعد قابل دسترسی خواهد بود");
        }

        private void lblSheypoor_Click(object sender, EventArgs e)
        {
            FarsiMessegeBox.Show("ارسال آگهی شیپور در ورژن بعد قابل دسترسی خواهد بود");
        }

        private void picDivarChat_MouseEnter(object sender, EventArgs e)
        {
            picDivarChat.Image = Properties.Resources.chatRed;
        }

        private void picDivarChat_MouseLeave(object sender, EventArgs e)
        {
            picDivarChat.Image = Properties.Resources.chat;
        }

        private void lblDivarChat_MouseEnter(object sender, EventArgs e)
        {
            lblSetter.GotFocose(lblDivarChat);
        }

        private void lblDivarChat_MouseLeave(object sender, EventArgs e)
        {
            lblSetter.LostFocose(lblDivarChat);
        }

        private void picDivarChat_Click(object sender, EventArgs e)
        {
            try
            {
                new frmSendChat().ShowDialog();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void lblDivarChat_Click(object sender, EventArgs e)
        {
            try
            {
                new frmSendChat().ShowDialog();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private async void lblDivar_Click(object sender, EventArgs e)
        {
            try
            {
                var divar = await DivarAdv.GetInstance();
                await divar.StartRegisterAdv();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }
    }
}
