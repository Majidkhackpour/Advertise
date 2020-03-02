using System;
using System.Linq;
using System.Windows.Forms;
using BussinesLayer;
using ErrorHandler;

namespace Ads.Forms.Mains
{
    public partial class frmShowNumbers : Form
    {
        private void LoadData(string search = "")
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
        public frmShowNumbers()
        {
            InitializeComponent();
        }

        private void frmShowNumbers_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void DGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DGrid.Rows[e.RowIndex].Cells["Radif"].Value = e.RowIndex + 1;
        }
    }
}
