using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinesLayer;
using DataLayer.Enums;
using FMessegeBox;

namespace Ads.Forms.Simcard
{
    public partial class frmShowCity : Form
    {
        public Guid CityGuid { get; set; }
        private AdvertiseType Type;
        private async Task LoadData(AdvertiseType type, string search = "")
        {
            try
            {
                if (type == AdvertiseType.Divar)
                {
                    var list = await DivarCityBussines.GetAllAsync(search);
                    DivarBindingSource.DataSource = list.OrderBy(q => q.Name).Where(q => q.Status).ToList();
                    lblCounter.Text = DivarBindingSource.Count.ToString();
                }

                else if (type == AdvertiseType.Sheypoor)
                {
                    var list = await SheypoorCityBussines.GetAllAsync(search);
                    SheypoorBindingSource.DataSource = list.OrderBy(q => q.Name).Where(q => q.Status).ToList();
                    lblCounter.Text = SheypoorBindingSource.Count.ToString();
                }
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        public frmShowCity(AdvertiseType type)
        {
            InitializeComponent();
            Type = type;
            if (Type == AdvertiseType.Divar)
            {
                dgDivar.Visible = true;
                dgSheypoor.Visible = false;
                lblDesc.Text = "شهرهای دیوار";
            }
            else if (Type == AdvertiseType.Sheypoor)
            {
                dgSheypoor.Visible = true;
                dgDivar.Visible = false;
                lblDesc.Text = "شهرهای شیپور";
            }
        }

        private async void frmShowCity_Load(object sender, EventArgs e)
        {
            await LoadData(Type);
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                await LoadData(Type, txtSearch.Text.Trim());
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void dgSheypoor_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgSheypoor.RowCount == 0) return;
                if (dgSheypoor.CurrentRow == null) return;
                CityGuid = (Guid) dgSheypoor[dgSheypoorCityGuid.Index, dgSheypoor.CurrentRow.Index].Value;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void dgDivar_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgDivar.RowCount == 0) return;
                if (dgDivar.CurrentRow == null) return;
                CityGuid = (Guid)dgDivar[dgDivarCityGuid.Index, dgDivar.CurrentRow.Index].Value;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void dgDivar_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgDivar.Rows[e.RowIndex].Cells["Radif"].Value = e.RowIndex + 1;
        }

        private void dgSheypoor_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgSheypoor.Rows[e.RowIndex].Cells["Radif1"].Value = e.RowIndex + 1;
        }

        private void frmShowCity_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode != Keys.Escape) return;
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }
    }
}
