using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using DataLayer.Enums;
using ErrorHandler;
using FMessegeBox;

namespace Ads.Forms.Simcard
{
    public partial class frmShow_Simcard : Form
    {
        private async Task LoadData(string search = "")
        {
            try
            {
                var list = await SimcardBussines.GetAllAsync(search);
                LogInBindingSource.DataSource = list.Where(q => q.Status).ToList();
                lblCounter.Text = LogInBindingSource.Count.ToString();
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private bool isSelect = false;
        public frmShow_Simcard()
        {
            InitializeComponent();
            contextMenuStrip1.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
        }

        private async void frmShow_Simcard_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                await LoadData(txtSearch.Text.Trim());
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void frmShow_Simcard_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyData)
                {
                    case Keys.Escape:
                        Close();
                        break;
                    case Keys.Insert:
                        mnuIns.PerformClick();
                        break;
                    case Keys.F7:
                        mnuEdit.PerformClick();
                        break;
                    case Keys.Delete:
                        mnuDelete.PerformClick();
                        break;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void mnuIns_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmSimcard_Main();
                if (frm.ShowDialog() == DialogResult.OK)
                    await LoadData(txtSearch.Text);
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
                if (DGrid.RowCount == 0) return;
                if (DGrid.CurrentRow == null) return;
                var _guid = (Guid)DGrid[dgGuid.Index, DGrid.CurrentRow.Index].Value;
                var frm = new frmSimcard_Main(_guid);
                if (frm.ShowDialog() == DialogResult.OK)
                    await LoadData(txtSearch.Text);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void DGrid_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (!isSelect)
                    mnuEdit.PerformClick();
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
                var Acc = SimcardBussines.GetAsync(accGuid);
                var message = "آیا از حذف سیمکارت " + Acc.Number + " " + "اطمینان دارید؟";

                if (FarsiMessegeBox.Show(message, "حذف", FMessegeBoxButtons.YesNo, FMessegeBoxIcons.Question) !=
                    DialogResult.Yes) return;
                Acc = SimcardBussines.Change_Status(accGuid, false);
                await Acc.SaveAsync();
                await LoadData(txtSearch.Text);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void DGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DGrid.Rows[e.RowIndex].Cells["Radif"].Value = e.RowIndex + 1;
        }

        private async void mnuDivarLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var number = (long)DGrid[dg_Number.Index, DGrid.CurrentRow.Index].Value;
                var divar = await DivarAdv.GetInstance();
                await divar.Login(number);
                await LoadData(txtSearch.Text);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void mnuDivarChatLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var number = (long)DGrid[dg_Number.Index, DGrid.CurrentRow.Index].Value;
                var divar = await DivarAdv.GetInstance();
                await divar.LoginChat(number, true);
                await LoadData(txtSearch.Text);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void mnuSheypoorLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var number = (long)DGrid[dg_Number.Index, DGrid.CurrentRow.Index].Value;
                var shey = await SheypoorAdv.GetInstance();
                await shey.Login(number);
                await LoadData(txtSearch.Text);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void mnuLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var number = (long)DGrid[dg_Number.Index, DGrid.CurrentRow.Index].Value;
                new frmAdvertiseLog(number).ShowDialog();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void mnuDeleteDivarToken_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var number = (long)DGrid[dg_Number.Index, DGrid.CurrentRow.Index].Value;
                var token = AdvTokensBussines.GetToken(number, AdvertiseType.Divar);
                await AdvTokensBussines.RemoveAsync(token);
                await LoadData(txtSearch.Text);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private async void mnuDeleteSheypoorToken_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var number = (long)DGrid[dg_Number.Index, DGrid.CurrentRow.Index].Value;
                var token = AdvTokensBussines.GetToken(number, AdvertiseType.Sheypoor);
                await AdvTokensBussines.RemoveAsync(token);
                await LoadData(txtSearch.Text);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private async void mnuDeleteDivarChet_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var number = (long)DGrid[dg_Number.Index, DGrid.CurrentRow.Index].Value;
                var token = AdvTokensBussines.GetToken(number, AdvertiseType.DivarChat);
                await AdvTokensBussines.RemoveAsync(token);
                await LoadData(txtSearch.Text);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
    }
}
