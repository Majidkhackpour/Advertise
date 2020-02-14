using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinesLayer;
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
                var list = AdvertiseLogBussines.GetAllAsync(number, type, search);
                LogBindingSource.DataSource = list.OrderBy(q => q.DateM);
                lblCounter.Text = LogBindingSource.Count.ToString();
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        public frmAdvertiseLog(long number)
        {
            InitializeComponent();
            Number = number;
            Type = 2;
            rbtnAll.Checked = true;
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
                FarsiMessegeBox.Show(exception.Message);
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
                FarsiMessegeBox.Show(exception.Message);
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
                FarsiMessegeBox.Show(exception.Message);
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
                FarsiMessegeBox.Show(exception.Message);
            }
        }
    }
}
