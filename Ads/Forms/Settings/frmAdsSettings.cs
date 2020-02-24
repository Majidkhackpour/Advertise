using System;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using ErrorHandler;
using FMessegeBox;

namespace Ads.Forms.Settings
{
    public partial class frmAdsSettings : Form
    {
        private SettingBussines cls;
        public frmAdsSettings()
        {
            InitializeComponent();
            var a = SettingBussines.GetAll();
            cls = a.Count > 0 ? a[0] : new SettingBussines();
        }

        private void SetData()
        {
            try
            {
                txtAdvInDay.Text = cls.CountAdvInDay.ToString();
                txtAdvInMounth.Text = cls.CountAdvInMounth.ToString();
                txtAdvInIP.Text = cls.CountAdvInIP.ToString();
                txtCountPic.Text = cls.MaxImgCount.ToString();
                txtAdvAddress.Text = cls.Address;
                txtUpdateDayCount.Text = cls.DayCountForUpdateState.ToString();
                txtFirstAdd.Text = cls.FierstLevelChatAddress;
                txtSecondAdd.Text = cls.SecondLevelChatAddress;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        private void txtAdvInDay_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtAdvInDay);
        }

        private void txtAdvInMounth_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtAdvInMounth);
        }

        private void txtAdvInIP_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtAdvInIP);
        }

        private void txtCountPic_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtCountPic);
        }

        private void txtAdvAddress_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtAdvAddress);
        }

        private void txtAdvAddress_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtAdvAddress);
        }

        private void txtCountPic_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtCountPic);
        }

        private void txtAdvInIP_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtAdvInIP);
        }

        private void txtAdvInMounth_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtAdvInMounth);
        }

        private void txtAdvInDay_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtAdvInDay);
        }


        private void frmAdsSettings_Load(object sender, EventArgs e)
        {
            SetData();
        }

        private void frmAdsSettings_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        btnFinish.PerformClick();
                        break;
                    case Keys.Escape:
                        btnCancel.PerformClick();
                        break;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnSearchAdv_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                txtAdvAddress.Text = folderBrowserDialog1.SelectedPath;
        }

        private void txtUpdateDayCount_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtUpdateDayCount);
        }

        private void txtUpdateDayCount_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtUpdateDayCount);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                btnFinish.Enabled = false;
                if (int.Parse(txtAdvInDay.Text) > 10)
                {
                    WebErrorLog.ErrorInstence.StartErrorLog(
                        "تعداد آگهی ارسالی به ازای هر سیمکارت در روز نمی تواند بیشتر از 10 آگهی باشد", false);
                    txtAdvInDay.Focus();
                    return;
                }

                if (int.Parse(txtAdvInMounth.Text) > 30)
                {
                    WebErrorLog.ErrorInstence.StartErrorLog(
                        "تعداد آگهی ارسالی به ازای هر سیمکارت در ماه نمی تواند بیشتر از 30 آگهی باشد", false);
                    txtAdvInMounth.Focus();
                    return;
                }

                if (int.Parse(txtCountPic.Text) > 10)
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("تعداد تصاویر آگهی ارسالی نمی تواند بیشتر از 10 تصویر باشد",
                        false);
                    txtCountPic.Focus();
                    return;
                }
                if (int.Parse(txtUpdateDayCount.Text) > 30)
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("روزهای بروزرسانی آگهی نمی تواند بیشتر از 30 روز باشد",
                        false);
                    txtUpdateDayCount.Focus();
                    return;
                }

                cls.CountAdvInDay = int.Parse(txtAdvInDay.Text);
                cls.CountAdvInMounth = int.Parse(txtAdvInMounth.Text);
                cls.CountAdvInIP = int.Parse(txtAdvInIP.Text);
                cls.MaxImgCount = int.Parse(txtCountPic.Text);
                cls.Address = txtAdvAddress.Text;
                cls.DayCountForUpdateState = int.Parse(txtUpdateDayCount.Text);
                cls.FierstLevelChatAddress = txtFirstAdd.Text;
                cls.SecondLevelChatAddress = txtSecondAdd.Text;
                await cls.SaveAsync();
                WebErrorLog.ErrorInstence.StartErrorLog("اطلاعات ذخیره شد", true);
                SetData();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
            finally
            {
                btnFinish.Enabled = true;
            }
        }

        private void txtAdvInDay_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSetter.KeyPress_Whitout_Dot(txtAdvInDay, e);
        }

        private void txtAdvInMounth_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSetter.KeyPress_Whitout_Dot(txtAdvInMounth, e);
        }

        private void txtAdvInIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSetter.KeyPress_Whitout_Dot(txtAdvInIP, e);
        }

        private void txtCountPic_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSetter.KeyPress_Whitout_Dot(txtCountPic, e);
        }

        private void txtUpdateDayCount_KeyPress(object sender, KeyPressEventArgs e)
        {
            txtSetter.KeyPress_Whitout_Dot(txtUpdateDayCount, e);
        }

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
                txtFirstAdd.Text = folderBrowserDialog2.SelectedPath;
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog3.ShowDialog() == DialogResult.OK)
                txtSecondAdd.Text = folderBrowserDialog3.SelectedPath;
        }
    }
}
