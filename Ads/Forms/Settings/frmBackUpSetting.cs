using System;
using System.IO;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using DataLayer;
using ErrorHandler;
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
            TelegramBot.SubmitEvent -= TelegramBotOnSubmitEvent;
            TelegramBot.SubmitEvent += TelegramBotOnSubmitEvent;
        }

        public string FilePath { get; set; }
        public string FileName { get; set; }
        private async void TelegramBotOnSubmitEvent()
        {
            try
            {
                if (cls.IsSendInEmail)
                {
                    await Utility.SendEmail("aradenj2211@gmail.com", "09382420272", cls.EmailAddress, FileName,
                        "فایل پشتیبان",
                        "smtp.gmail.com", 587, FilePath);
                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
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
                chbIsSendToEmail.Checked = cls.IsSendInEmail;
                txtAutoSecond.Enabled = chbAuto.Checked;
                txtEmailAddress.Enabled = chbIsSendToEmail.Checked;
                txtEmailAddress.Text = cls.EmailAddress;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
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
                cls.AutoTime = txtAutoSecond.Text.ParseToInt() * 60;
                cls.IsSendInEmail = chbIsSendToEmail.Checked;
                cls.EmailAddress = txtEmailAddress.Text;
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

        private async void btnBackUp_Click(object sender, EventArgs e)
        {
            try
            {
                var date = DateConvertor.M2SH(DateTime.Now);
                date = date.Replace("/", "_") + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + ".bak";
                FilePath = Path.Combine(cls.BackUpAddress, date);
                FileName = date;
                if (await Utility.CreateBackUp("Ads", date, cls))
                    WebErrorLog.ErrorInstence.StartErrorLog("پشتیبان گیری با موفقیت انجام شد", true);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void chbIsSendToEmail_CheckedChanged(object sender, EventArgs e)
        {
            txtEmailAddress.Enabled = chbIsSendToEmail.Checked;
        }

        private async void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Filter = "BackUp Files (*.bak)|*.bak";
                ofd.Title = "فایل پشتیبان خود را انتخاب نمایید";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (await Utility.RestoreDatabase("Ads", ofd.FileName))
                        WebErrorLog.ErrorInstence.StartErrorLog("بازیابی فایل پشتیبان با موفقیت انجام شد", true);
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnTelegramBotSetting_Click(object sender, EventArgs e)
        {
            try
            {
                new frmTelegramBotSetting().ShowDialog();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
    }
}
