using System;
using System.Windows.Forms;
using BussinesLayer;
using DataLayer;
using ErrorHandler;

namespace Ads.Forms.Settings
{
    public partial class frmTelegramBotSetting : Form
    {
        private TelegramBotSettingBussines cls;
        public frmTelegramBotSetting()
        {
            InitializeComponent();
            var a = TelegramBotSettingBussines.GetAll();
            cls = a.Count > 0 ? a[0] : new TelegramBotSettingBussines();
        }
        private void SetData()
        {
            try
            {
                txtToken.Text = cls.Token;
                txtBackUpChannel.Text = cls.ChanelForAds;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private void frmTelegramBotSetting_Load(object sender, EventArgs e)
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

                cls.ChanelForAds = txtBackUpChannel.Text.Trim();
                cls.Status = true;
                cls.Token = txtToken.Text.Trim();
                await cls.SaveAsync();
                WebErrorLog.ErrorInstence.StartErrorLog("اطلاعات ذخیره شد", true);
                Close();
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

        private void frmTelegramBotSetting_KeyDown(object sender, KeyEventArgs e)
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
    }
}
