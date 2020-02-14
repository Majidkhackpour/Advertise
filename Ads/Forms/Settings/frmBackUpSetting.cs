using System;
using System.Windows.Forms;
using BussinesLayer;
using DataLayer;
using FMessegeBox;

namespace Ads.Forms.Settings
{
    public partial class frmBackUpSetting : Form
    {
        private BackUpSettingBussines cls;
        public frmBackUpSetting()
        {
            InitializeComponent();
            var a = BackUpSettingBussines.GetAll();
            cls = a.Count > 0 ? a[0] : new BackUpSettingBussines();
        }
        private void SetData()
        {
            try
            {
                txtAddress.Text = cls.BackUpAddress;
                var sec = cls.AutoTime ?? 0;
                if (sec > 0)
                    sec /= 60;
                txtAutoSecond.Text = sec.ToString();
                chbAuto.Checked = cls.AutoBackUp;
                chbIsSentToTelegram.Checked = cls.IsSendInTelegram;
                txtAutoSecond.Enabled = chbAuto.Checked;
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                txtAddress.Text = folderBrowserDialog1.SelectedPath;
        }

        private void chbAuto_CheckedChanged(object sender, EventArgs e)
        {
            txtAutoSecond.Enabled = chbAuto.Checked;
        }

        private void frmBackUpSetting_Load(object sender, EventArgs e)
        {
            SetData();
        }

        private async void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                btnFinish.Enabled = false;
                if (cls.Guid == Guid.Empty)
                {
                    cls.Guid = Guid.NewGuid();
                    cls.DateSabt = DateConvertor.M2SH(DateTime.Now);
                }

                cls.BackUpAddress = txtAddress.Text;
                cls.AutoBackUp = chbAuto.Checked;
                cls.Status = true;
                cls.IsSendInTelegram = chbIsSentToTelegram.Checked;
                cls.AutoTime = txtAutoSecond.Text.ParseToInt()*60;
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
    }
}
