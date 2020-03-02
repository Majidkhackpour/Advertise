using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinesLayer;
using ErrorHandler;
using FMessegeBox;

namespace Ads.Forms.Simcard
{
    public partial class frmAdvertiseLog : Form
    {
        public long Number { get; set; }
        public short Type { get; set; }
        private async Task LoadData(long number, short type, string search = "")
        {
            try
            {
                List<AdvertiseLogBussines> list = null;
                list = number == 0
                    ? AdvertiseLogBussines.GetAllAsync(type, search)
                    : AdvertiseLogBussines.GetAllAsync(number, type, search);
                LogBindingSource.DataSource = list.OrderBy(q => q.DateM);
                lblCounter.Text = LogBindingSource.Count.ToString();
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        public frmAdvertiseLog(long number)
        {
            InitializeComponent();
            Number = number;
            Type = 2;
            rbtnAll.Checked = true;
            dgSimNumber.Visible = false;
            dgUrl.Visible = true;
        }

        public frmAdvertiseLog()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            uC_Date1.Visible = false;
            Number = 0;
            Type = 3;
            rbtnAll.Checked = true;
            dgSimNumber.Visible = true;
            label1.Visible = false;
            dgUrl.Visible = false;
            label2.Visible = false;
            lblCounter.Visible = false;
            line1.Visible = false;
        }

        private async void frmAdvertiseLog_Load(object sender, EventArgs e)
        {
            await LoadData(Number, Type, txtSearch.Text.Trim());
        }

        private async void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                await LoadData(Number, Type, txtSearch.Text.Trim());
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
                await LoadData(Number, Type, txtSearch.Text.Trim());
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void rbtnDivar_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rbtnDivar.Checked) Type = 0;
                await LoadData(Number, Type, txtSearch.Text.Trim());
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
                await LoadData(Number, Type, txtSearch.Text.Trim());
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
    }
}
