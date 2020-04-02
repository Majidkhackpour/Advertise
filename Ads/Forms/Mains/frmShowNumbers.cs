using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using DataLayer;
using ErrorHandler;

namespace Ads.Forms.Mains
{
    public partial class frmShowNumbers : Form
    {
        public short Type { get; set; }
        private async Task LoadData(short type, string search = "")
        {
            try
            {
                var list = ChatNumberBussines.GetAll(cmbPanel.Text, type, search).OrderByDescending(q => q.DateSabt)
                    .ToList();
                LogBindingSource.DataSource = list;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private async Task LoadCity()
        {
            try
            {
                var a = new DivarCityBussines()
                {
                    Guid = Guid.Empty,
                    Name = "[همه]"
                };
                var list = await DivarCityBussines.GetAllAsync();
                list.Add(a);
                CityBindingSource.DataSource = list.ToList().OrderBy(q => q.Name);
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        public frmShowNumbers()
        {
            InitializeComponent();
            rbtnAll.Checked = true;
            Type = 2;
            contextMenuStrip1.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
        }

        private async void frmShowNumbers_Load(object sender, EventArgs e)
        {
            await LoadData(Type);
            await LoadCity();
        }

        private void DGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DGrid.Rows[e.RowIndex].Cells["Radif"].Value = e.RowIndex + 1;
        }

        private async void rbtnDivar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnDivar.Checked) Type = 0;
                await LoadData(Type, txtSearch.Text);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void rbtnSheypoor_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnSheypoor.Checked) Type = 1;
                await LoadData(Type, txtSearch.Text.Trim());
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void rbtnAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnAll.Checked) Type = 2;
                await LoadData(Type, txtSearch.Text.Trim());
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void cmbPanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadData(Type, txtSearch.Text);
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadData(Type, txtSearch.Text);
        }

        private void mnuLog_Click(object sender, EventArgs e)
        {
            try
            {
                var date = DateConvertor.M2SH(DateTime.Now);
                date = date.Replace("/", "_") + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + ".txt";
                var sfd = new SaveFileDialog { Filter = "Text Files(*.txt)|*.txt", DefaultExt = ".txt", FileName = date };
                if (sfd.ShowDialog() != DialogResult.OK) return;
                var list = new List<string>();
                for (var i = 0; i < DGrid.RowCount; i++)
                    list.Add(DGrid[dgNumber.Index, i].Value.ToString());
                File.WriteAllLines(sfd.FileName, list);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
    }
}
