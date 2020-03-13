using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using ErrorHandler;

namespace Ads.Forms.SMS_Panel
{
    public partial class frmSendSMS : Form
    {
        private async Task LoadData()
        {
            try
            {
                await LoadPanels();
                await LoadLineNumbers();
                await LoadNumbers();
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
                PanelBindingSource.DataSource = list;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        private async Task LoadLineNumbers()
        {
            try
            {
                if (cmbPanel.SelectedValue == null) return;
                var list = await PanelLineNumberBussines.GetAllAsync();
                list = list.Where(q => q.Status && q.PanelGuid == (Guid)cmbPanel.SelectedValue)
                    .OrderBy(q => q.LineNumber).ToList();
                LineNumberBindingSource.DataSource = list;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private async Task LoadNumbers()
        {
            try
            {
                var list = ChatNumberBussines.GetAll().OrderByDescending(q => q.DateSabt).ToList();
                LogBindingSource.DataSource = list;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        public frmSendSMS()
        {
            InitializeComponent();
            cmCity.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
        }

        private async void frmSendSMS_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void cmbPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                await LoadLineNumbers();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void DGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgCity.RowCount <= 0) return;
                if (e.ColumnIndex == dg_CityChecked.Index)
                {
                    if (dgCity.CurrentRow != null)
                    {
                        dgCity[dg_CityChecked.Index, dgCity.CurrentRow.Index].Value =
                            !(bool)dgCity[dg_CityChecked.Index, dgCity.CurrentRow.Index].Value;
                    }
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void mnuCitySelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgCity.RowCount <= 0) return;
                if (mnuCitySelectAll.Checked)
                {
                    foreach (DataGridViewRow row in dgCity.Rows)
                    {
                        if (row.Cells["dg_CityChecked"] is DataGridViewCheckBoxCell checkBox)
                            checkBox.Value = true;
                    }

                    return;
                }
                //UnCkeched
                foreach (DataGridViewRow row in dgCity.Rows)
                {
                    if (row.Cells["dg_CityChecked"] is DataGridViewCheckBoxCell checkBox)
                        checkBox.Value = false;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void txtMessage_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtMessage);
        }

        private void txtMessage_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtMessage);
        }

        private void btnAddGrid_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgCity.RowCount <= 0) return;
                for (var i = 0; i < dgCity.RowCount; i++)
                {
                    if (!(bool) dgCity[dg_CityChecked.Index, i].Value) continue;
                    var number = dgCity[dgNumbers.Index, i].Value.ToString().ParseToLong();
                    if (number != 0)
                        lbxNumbers.Items.Add(number);
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void txtNumber_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtNumber);
        }

        private void txtNumber_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtNumber);
        }

        private void btnAddDasti_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNumber.Text)) return;
                if (txtNumber.Text.ParseToLong() == 0) return;
                lbxNumbers.Items.Add(txtNumber.Text.ParseToLong());
                txtNumber.Text = "";
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (lbxNumbers.Items.Count <= 0) return;
                if (lbxNumbers.SelectedItem == null) return;
                lbxNumbers.Items.Remove(lbxNumbers.SelectedItem);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnAddList_Click(object sender, EventArgs e)
        {
            try
            {
                var ofd = new OpenFileDialog
                {
                    Filter = @"Text Files (*.txt)|*.txt", Title = @"فایل شماره های خود را انتخاب نمایید"
                };
                if (ofd.ShowDialog() != DialogResult.OK) return;
                var list = File.ReadAllLines(ofd.FileName).ToList();
                if (list.Count <= 0) return;
                foreach (var item in list)
                {
                    var number = item.ParseToLong();
                    if (number == 0) continue;
                    lbxNumbers.Items.Add(number);
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
    }
}
