using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using DataLayer;
using DataLayer.Enums;
using FMessegeBox;

namespace Ads.Forms.Settings
{
    public partial class frmProxy : Form
    {
        private ProxyBussines cls;
        private async Task LoadData()
        {
            try
            {
                var list = await ProxyBussines.GetAllAsync();
                ProxyBindingSource.DataSource = list.Where(q => q.Status).ToList();
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        public frmProxy()
        {
            InitializeComponent();
            contextMenuStrip1.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
            rbtnMtproto.Checked = true;
            cls = new ProxyBussines();
        }
        private async Task Set_Data()
        {
            try
            {
                txtPassword.Text = txtPort.Text = txtSecret.Text = txtServer.Text = txtUserName.Text = "";
                await LoadData();
                grpAccount.Enabled = false;
                if (cls.Type == ProxyType.MtProto) rbtnMtproto.Checked = true;
                else rbtnSocks5.Checked = true;
                txtPassword.Text = cls.Password;
                txtPort.Text = cls.Port.ToString();
                txtSecret.Text = cls.Secret;
                txtServer.Text = cls.Server;
                txtUserName.Text = cls.UserName;
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        private async void frmProxy_Load(object sender, EventArgs e)
        {
            await Set_Data();
        }

        private void mnuIns_Click(object sender, EventArgs e)
        {
            try
            {
                cls = new ProxyBussines();
                grpAccount.Enabled = true;
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void rbtnMtproto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnMtproto.Checked)
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void rbtnSocks5_CheckedChanged(object sender, EventArgs e)
        {
            if (!rbtnSocks5.Checked)
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUserName.Enabled = true;
                txtPassword.Enabled = true;
            }
        }

        private void txtServer_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtServer);
        }

        private void txtPort_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtPort);
        }

        private void txtSecret_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtSecret);
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtUserName);
        }

        private void txtPassword_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtPassword);
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtPassword);
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtUserName);
        }

        private void txtSecret_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtSecret);
        }

        private void txtPort_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtPort);
        }

        private void txtServer_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtServer);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmProxy_KeyDown(object sender, KeyEventArgs e)
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

                if (string.IsNullOrWhiteSpace(txtServer.Text))
                {
                    FarsiMessegeBox.Show("لطفا نام سرور را وارد نمایید");
                    txtServer.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPort.Text))
                {
                    FarsiMessegeBox.Show("لطفا شماره پورت را وارد نمایید");
                    txtPort.Focus();
                    return;
                }

                cls.Password = txtPassword.Text;
                cls.Port = txtPort.Text.ParseToInt();
                cls.Secret = txtSecret.Text;
                cls.UserName = txtUserName.Text;
                cls.Server = txtServer.Text;
                cls.Type = rbtnMtproto.Checked ? ProxyType.MtProto : ProxyType.Socks5;
                cls.Status = true;

                await cls.SaveAsync();
                cls = new ProxyBussines();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
            finally
            {
                btnFinish.Enabled = true;
                await Set_Data();
            }
        }

        private async void mnuEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var guid = (Guid) DGrid[dgGuid.Index, DGrid.CurrentRow.Index].Value;
                cls =ProxyBussines.Get(guid);
                await Set_Data();
                grpAccount.Enabled = true;
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private async void mnuDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var guid = (Guid)DGrid[dgGuid.Index, DGrid.CurrentRow.Index].Value;
                var accGuid = (Guid)DGrid[dgGuid.Index, DGrid.CurrentRow.Index].Value;
                var Acc = ProxyBussines.Get(accGuid);
                var message = "آیا از حذف پروکسی " + Acc.Server + " " + "اطمینان دارید؟";

                if (FarsiMessegeBox.Show(message, "حذف", FMessegeBoxButtons.YesNo, FMessegeBoxIcons.Question) !=
                    DialogResult.Yes) return;
                Acc = ProxyBussines.Change_Status(accGuid, false);
                await Acc.SaveAsync();
                await Set_Data();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void DGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DGrid.Rows[e.RowIndex].Cells["Radif"].Value = e.RowIndex + 1;
        }
    }
}
