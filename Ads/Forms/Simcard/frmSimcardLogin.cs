using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using FMessegeBox;

namespace Ads.Forms.Simcard
{
    public partial class frmSimcardLogin : Form
    {
        private async Task LoadData(string search = "")
        {
            try
            {
                var list = await SimcardBussines.GetAllAsync(search);
                LogInBindingSource.DataSource = list;
                lblCounter.Text = LogInBindingSource.Count.ToString();
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        public frmSimcardLogin()
        {
            InitializeComponent();
            contextMenuStrip1.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
        }

        private async void frmSimcardLogin_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void mnuLoginDivar_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var number = (long) DGrid[dg_Number.Index, DGrid.CurrentRow.Index].Value;
                var divar = await DivarAdv.GetInstance();
                await divar.Login(number);
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private async void mnuLoginChat_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGrid.RowCount <= 0) return;
                if (DGrid.CurrentRow == null) return;
                var number = (long)DGrid[dg_Number.Index, DGrid.CurrentRow.Index].Value;
                var divar = await DivarAdv.GetInstance();
                await divar.LoginChat(number);
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                await LoadData(txtSearch.Text.Trim());
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void frmSimcardLogin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyData)
                {
                    case Keys.Escape:
                        Close();
                        break;
                }
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
