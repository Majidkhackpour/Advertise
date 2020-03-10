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
    public partial class frmLineNumbers : Form
    {
        private PanelLineNumberBussines cls;
        private SettingBussines clsSetting;
        public frmLineNumbers()
        {
            InitializeComponent();
            grpAccount.Enabled = false;
            contextMenuStrip1.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
            var a = SettingBussines.GetAll();
            clsSetting = a.Count > 0 ? a[0] : new SettingBussines();
        }
        private async Task LoadData()
        {
            try
            {
                var list = await PanelLineNumberBussines.GetAllAsync();
                list = list.Where(q => q.Status).OrderBy(q => q.LineNumber).ToList();
                SMSBindingSource.DataSource = list;
                await LoadPanels();
                await CheckDefault();
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private async Task LoadPanels()
        {
            try
            {
                var list = await PanelBussines.GetAllAsync();
                list = list.Where(q => q.Status).OrderBy(q => q.Name).ToList();
                ParentBindingSource.DataSource = list;
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
                txtLineNumber.Text = "";
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private async void frmLineNumbers_Load(object sender, EventArgs e)
        {
            await LoadData();
            Clear();
        }

        private void txtLineNumber_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtLineNumber);
        }

        private void txtLineNumber_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtLineNumber);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            grpAccount.Enabled = false;
        }

        private void frmLineNumbers_KeyDown(object sender, KeyEventArgs e)
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

        private void mnuIns_Click(object sender, EventArgs e)
        {
            try
            {
                cls = new PanelLineNumberBussines();
                grpAccount.Enabled = true;
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
                cls = await PanelLineNumberBussines.GetAsync(guid);
                await SetData();
                grpAccount.Enabled = true;
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
                txtLineNumber.Text = cls.LineNumber.ToString();
                cmbPanel.SelectedValue = cls.PanelGuid;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private async void mnuDefault_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var guid = (Guid)DGrid[dgGuid.Index, DGrid.CurrentRow.Index].Value;
                clsSetting.LineNumberGuid = guid;
                await clsSetting.SaveAsync();
                await CheckDefault();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        private async Task CheckDefault()
        {
            try
            {
                if (clsSetting?.PanelGuid == null) return;
                var guid = clsSetting.LineNumberGuid;
                for (var i = 0; i < DGrid.RowCount; i++)
                    DGrid[dgIsDefault.Index, i].Value = false;
                for (var i = 0; i < DGrid.RowCount; i++)
                {
                    if ((Guid)(DGrid[dgGuid.Index, i].Value) != guid) continue;
                    DGrid[dgIsDefault.Index, i].Value = true;
                    return;
                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
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
                if (string.IsNullOrEmpty(txtLineNumber.Text))
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("شماره خط پنل نمی تواند خالی باشد", false);
                    txtLineNumber.Focus();
                    return;
                }
                if (txtLineNumber.Text.ParseToLong() == 0)
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("شماره خط معتبر نمی باشد", false);
                    txtLineNumber.Focus();
                    return;
                }
                cls.Status = true;
                cls.LineNumber = txtLineNumber.Text.ParseToLong();
                cls.PanelGuid = (Guid)cmbPanel.SelectedValue;
                await cls.SaveAsync();
                Clear();
                grpAccount.Enabled = false;
                await LoadData();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount == 0) return;
                if (DGrid.CurrentRow == null) return;
                var accGuid = (Guid)DGrid[dgGuid.Index, DGrid.CurrentRow.Index].Value;
                var Acc = await PanelLineNumberBussines.GetAsync(accGuid);
                var message = "آیا از حذف شماره " + Acc.LineNumber + " " + "اطمینان دارید؟";

                if (FarsiMessegeBox.Show(message, "حذف", FMessegeBoxButtons.YesNo, FMessegeBoxIcons.Question) !=
                    DialogResult.Yes) return;
                Acc = PanelLineNumberBussines.Change_Status(accGuid, false);
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
