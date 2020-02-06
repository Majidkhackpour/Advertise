using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using DataLayer;
using FMessegeBox;

namespace Ads.Forms.Mains
{
    public partial class frmAdsGroup : Form
    {
        private AdvGroupBussines cls;
        public frmAdsGroup()
        {
            InitializeComponent();
            cls = new AdvGroupBussines();
        }

        public frmAdsGroup(Guid guid)
        {
            InitializeComponent();
            cls = AdvGroupBussines.Get(guid);
        }

        private async Task LoadData()
        {
            try
            {
                var a = new AdvGroupBussines()
                {
                    Guid = Guid.Empty,
                    DateSabt = DateConvertor.M2SH(DateTime.Now),
                    Status = true,
                    Name = "[هیچکدام]",
                    ParentGuid = Guid.Empty
                };
                var list =await AdvGroupBussines.GetAllAsync();
                list = list.Where(q => q.Status && q.ParentGuid == Guid.Empty).ToList();
                list.Add(a);
                list = list.OrderBy(q => q.Name).ToList();
                ParentBindingSource.DataSource = list;
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }

        private async Task SetData()
        {
            try
            {
                await LoadData();
                txtName.Text = cls.Name;
                if (cls.Guid != Guid.Empty)
                    cmbParent.SelectedValue = cls.ParentGuid;
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        private void txtName_Enter(object sender, System.EventArgs e)
        {
            txtSetter.Focus(txt2: txtName);
        }

        private void txtName_Leave(object sender, System.EventArgs e)
        {
            txtSetter.Follow(txt2: txtName);
        }

        private async void frmAdsGroup_Load(object sender, EventArgs e)
        {
            await SetData();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
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
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    FarsiMessegeBox.Show("عنوان گروه نمی تواند خالی باشد");
                    txtName.Focus();
                    return;
                }
                if (!AdvGroupBussines.Check_Name(txtName.Text, cls.Guid))
                {
                    FarsiMessegeBox.Show("عنوان وارد شده تکراری است");
                    txtName.Focus();
                    return;
                }

                cls.Status = true;
                cls.Name = txtName.Text.Trim();
                cls.ParentGuid = (Guid) cmbParent.SelectedValue;
                await cls.SaveAsync();
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void frmAdsGroup_KeyDown(object sender, KeyEventArgs e)
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
    }
}
