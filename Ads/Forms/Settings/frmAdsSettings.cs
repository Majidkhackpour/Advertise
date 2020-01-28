using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
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
                txtAdvInDay.Text = cls.CountAdvInDayDivar.ToString();
                txtAdvInMounth.Text = cls.CountAdvInMounthDivar.ToString();
                txtAdvInIP.Text = cls.CountAdvInIPDivar.ToString();
                txtCat1.Text = cls.DivarCat1;
                txtCat2.Text = cls.DivarCat2;
                txtCat3.Text = cls.DivarCat3;
                txtCountPic.Text = cls.DivarMaxImgCount.ToString();
                txtAdvAddress.Text = cls.AdsAddress;
                txtUpdateDayCount.Text = cls.DivarDayCountForUpdateState.ToString();
                cmbDeleteDay.Text = cls.DivarDayCountForDelete.ToString();
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
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

        private void txtCat3_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtCat3);
        }

        private void txtCat2_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtCat2);
        }

        private void txtCat1_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtCat1);
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

        private void txtCat1_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtCat1);
        }

        private void txtCat2_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtCat2);
        }

        private void txtCat3_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtCat3);
        }

        private void frmAdsSettings_Load(object sender, EventArgs e)
        {
            cmbDeleteDay.SelectedIndex = 0;
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
                FarsiMessegeBox.Show(exception.Message);
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
                    FarsiMessegeBox.Show("تعداد آگهی ارسالی به ازای هر سیمکارت در روز نمی تواند بیشتر از 10 آگهی باشد");
                    txtAdvInDay.Focus();
                    return;
                }

                if (int.Parse(txtAdvInMounth.Text) > 30)
                {
                    FarsiMessegeBox.Show("تعداد آگهی ارسالی به ازای هر سیمکارت در ماه نمی تواند بیشتر از 30 آگهی باشد");
                    txtAdvInMounth.Focus();
                    return;
                }

                if (int.Parse(txtCountPic.Text) > 10)
                {
                    FarsiMessegeBox.Show("تعداد تصاویر آگهی ارسالی نمی تواند بیشتر از 10 تصویر باشد");
                    txtCountPic.Focus();
                    return;
                }
                if (int.Parse(txtUpdateDayCount.Text) > 30)
                {
                    FarsiMessegeBox.Show("روزهای بروزرسانی آگهی نمی تواند بیشتر از 30 روز باشد");
                    txtUpdateDayCount.Focus();
                    return;
                }

                cls.CountAdvInDayDivar = int.Parse(txtAdvInDay.Text);
                cls.CountAdvInMounthDivar = int.Parse(txtAdvInMounth.Text);
                cls.CountAdvInIPDivar = int.Parse(txtAdvInIP.Text);
                cls.DivarCat1 = txtCat1.Text;
                cls.DivarCat2 = txtCat2.Text;
                cls.DivarCat3 = txtCat3.Text;
                cls.DivarMaxImgCount = int.Parse(txtCountPic.Text);
                cls.AdsAddress = txtAdvAddress.Text;
                cls.DivarDayCountForUpdateState = int.Parse(txtUpdateDayCount.Text);
                int counter = 0;
                if (cmbDeleteDay.SelectedIndex == 0) counter = 1;
                else if (cmbDeleteDay.SelectedIndex == 1) counter = 7;
                else if (cmbDeleteDay.SelectedIndex == 2) counter = 15;
                else if (cmbDeleteDay.SelectedIndex == 3) counter = 30;
                cls.DivarDayCountForDelete = counter;


                cls.CountAdvInDaySheypoor = int.Parse(txtAdvInDay.Text);
                cls.CountAdvInMounthSheypoor = int.Parse(txtAdvInMounth.Text);
                cls.CountAdvInIPSheypoor = int.Parse(txtAdvInIP.Text);
                cls.SheypoorCat1 = txtCat1.Text;
                cls.SheypoorCat2 = txtCat2.Text;
                cls.SheypoorCat3 = txtCat3.Text;
                cls.SheypoorMaxImgCount = int.Parse(txtCountPic.Text);
                cls.AdsAddress = txtAdvAddress.Text;
                cls.SheypoorDayCountForUpdateState = int.Parse(txtUpdateDayCount.Text);
                await cls.SaveAsync();
                FarsiMessegeBox.Show("اطلاعات ذخیره شد");
                SetData();
            }
            catch (Exception ex)
            {
                FarsiMessegeBox.Show(ex.Message);
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
    }
}
