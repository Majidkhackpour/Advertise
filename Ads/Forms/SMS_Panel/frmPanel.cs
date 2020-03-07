using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using DataLayer;
using ErrorHandler;
using FMessegeBox;

namespace Ads.Forms.SMS_Panel
{
    public partial class frmPanel : Form
    {
        private PanelBussines cls;
        public frmPanel()
        {
            InitializeComponent();
            expandablePanel1.Expanded = false;
            grpAccount.Enabled = false;
            contextMenuStrip1.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
        }
        private async Task LoadData()
        {
            try
            {
                var list = await PanelBussines.GetAllAsync();
                list = list.Where(q => q.Status).OrderBy(q => q.Name).ToList();
                SMSBindingSource.DataSource = list;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private void Clear()
        {
            try
            {
                txtName.Text = txtAPI.Text = txtUserName.Text = txtPassword.Text = "";
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private async void frmPanel_Load(object sender, EventArgs e)
        {
            await LoadData();
            Clear();
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtName);
        }

        private void txtAPI_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtAPI);
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtUserName);
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtPassword);
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtName);
        }

        private void txtAPI_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtAPI);
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtUserName);
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtPassword);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            grpAccount.Enabled = false;
            expandablePanel1.Expanded = false;
        }

        private void frmPanel_KeyDown(object sender, KeyEventArgs e)
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

        private async void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                if (cls.Guid == Guid.Empty)
                {
                    cls.Guid = Guid.NewGuid();
                    cls.DateSabt = DateConvertor.M2SH(DateTime.Now);
                }
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("عنوان پنل نمی تواند خالی باشد", false);
                    txtName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtAPI.Text))
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("api پنل نمی تواند خالی باشد", false);
                    txtAPI.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("نام کاربری نمی تواند خالی باشد", false);
                    txtUserName.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("رمز عبور نمی تواند خالی باشد", false);
                    txtPassword.Focus();
                    return;
                }

                cls.Status = true;
                cls.Name = txtName.Text.Trim();
                cls.API = txtAPI.Text.Trim();
                cls.Password = txtPassword.Text.Trim();
                cls.UserName = txtUserName.Text.Trim();
                await cls.SaveAsync();
                Clear();
                grpAccount.Enabled = false;
                expandablePanel1.Expanded = false;
                await LoadData();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void mnuIns_Click(object sender, EventArgs e)
        {
            try
            {
                cls = new PanelBussines();
                grpAccount.Enabled = true;
                expandablePanel1.Expanded = true;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void mnuEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var guid = (Guid)DGrid[dgGuid.Index, DGrid.CurrentRow.Index].Value;
                cls = await PanelBussines.GetAsync(guid);
                await SetData();
                grpAccount.Enabled = true;
                expandablePanel1.Expanded = true;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async Task SetData()
        {
            try
            {
                txtName.Text = cls.Name;
                txtAPI.Text = cls.API;
                txtUserName.Text = cls.UserName;
                txtPassword.Text = cls.Password;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private async void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount == 0) return;
                if (DGrid.CurrentRow == null) return;
                var accGuid = (Guid)DGrid[dgGuid.Index, DGrid.CurrentRow.Index].Value;
                var Acc = await PanelBussines.GetAsync(accGuid);
                var message = "آیا از حذف پنل " + Acc.Name + " " + "اطمینان دارید؟";

                if (FarsiMessegeBox.Show(message, "حذف", FMessegeBoxButtons.YesNo, FMessegeBoxIcons.Question) !=
                    DialogResult.Yes) return;
                Acc = PanelBussines.Change_Status(accGuid, false);
                await Acc.SaveAsync();
                await LoadData();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
    }
}
