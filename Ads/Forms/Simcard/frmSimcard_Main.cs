using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using DataLayer;
using FMessegeBox;

namespace Ads.Forms.Simcard
{
    public partial class frmSimcard_Main : Form
    {
        private SimcardBussines cls;
        public frmSimcard_Main()
        {
            InitializeComponent();
            cmAds.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
            cmCity.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
            cls = new SimcardBussines();
        }

        private async Task FillAds()
        {
            try
            {
                var a = await SettingBussines.GetAllAsync();
                var address = string.IsNullOrEmpty(a[0].AdsAddress) ? Application.StartupPath : a[0].AdsAddress;
                var list2 = await Advertise.GetAllAsync(address);
                adsBindingSource.DataSource = list2;
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        private async Task FillCity()
        {
            try
            {
                var list3 = await DivarCityBussines.GetAllAsync();
                cityBindingSource.DataSource = list3.ToList();
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }

        private async Task SetCity(Guid simGuid)
        {
            try
            {
                if (simGuid == Guid.Empty) return;
                var list = await DivarSimCityBussines.GetAllAsync(simGuid);
                if (list.Count <= 0) return;
                foreach (var item in list)
                    for (var i = 0; i < dgCity.RowCount; i++)
                        if (item.CityGuid == ((Guid?)dgCity[dg_CityGuid.Index, i].Value ?? Guid.Empty))
                            dgCity[dg_CityChecked.Index, i].Value = true;
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        private async Task SetAds(Guid simGuid)
        {
            try
            {
                if (simGuid == Guid.Empty) return;
                var list = await SimcardAdsBussines.GetAllAsync(simGuid);
                if (list.Count <= 0) return;
                foreach (var item in list)
                    for (var i = 0; i < dgAds.RowCount; i++)
                        if (item.AdsName == (dgAds[dg_AdvName.Index, i].Value.ToString() ?? ""))
                            dgAds[dg_AdvChecked.Index, i].Value = true;
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        public frmSimcard_Main(Guid guid)
        {
            InitializeComponent();
            cmAds.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
            cmCity.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
            cls = SimcardBussines.GetAsync(guid);
        }

        private async Task FillComboBox()
        {
            try
            {
                var list = await SimcardBussines.GetAllAsync();
                var a = list.Select(q => q.Operator).Distinct().ToList();
                cmbOperator.DataSource = a;
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
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
                if (string.IsNullOrWhiteSpace(txtNumber.Text))
                {
                    FarsiMessegeBox.Show("لطفا شماره را وارد نمایید");
                    txtNumber.Focus();
                    return;
                }
                if (txtNumber.Text.Length < 10 || txtNumber.Text.Length > 12)
                {
                    FarsiMessegeBox.Show("شماره وارد شده اشتباه است");
                    txtNumber.Focus();
                    return;
                }
                if (!SimcardBussines.Check_Number(long.Parse(txtNumber.Text), cls.Guid))
                {
                    FarsiMessegeBox.Show("شماره وارد شده تکراری است");
                    txtNumber.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtOwner.Text))
                {
                    FarsiMessegeBox.Show("لطفا مالک را وارد نمایید");
                    txtNumber.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbOperator.Text))
                {
                    FarsiMessegeBox.Show("لطفا اپراتور را وارد نمایید");
                    txtNumber.Focus();
                    return;
                }

                var listCity = new List<DivarSimCityBussines>();
                for (int i = 0; i < dgCity.RowCount; i++)
                {
                    if ((bool)dgCity[dg_CityChecked.Index, i].Value)
                    {
                        var a = new DivarSimCityBussines()
                        {
                            Guid = Guid.NewGuid(),
                            DateSabt = DateConvertor.M2SH(DateTime.Now),
                            Status = true,
                            SimcardGuid = cls.Guid,
                            CityGuid = (Guid)dgCity[dg_CityGuid.Index, i].Value
                        };
                        listCity.Add(a);
                    }
                }


                var listAds = new List<SimcardAdsBussines>();
                for (int i = 0; i < dgAds.RowCount; i++)
                {
                    if ((bool)dgAds[dg_AdvChecked.Index, i].Value)
                    {
                        var a = new SimcardAdsBussines()
                        {
                            Guid = Guid.NewGuid(),
                            DateSabt = DateConvertor.M2SH(DateTime.Now),
                            Status = true,
                            SimcardGuid = cls.Guid,
                            AdsName = dgAds[dg_AdvName.Index, i].Value.ToString()
                        };
                       listAds.Add(a);
                    }
                }



                cls.OwnerName = txtOwner.Text;
                cls.Status = true;
                cls.Number = long.Parse(txtNumber.Text);
                cls.NextUseDivar = DateTime.Now;
                cls.NextUseSheypoor = DateTime.Now;
                cls.NextUseDivarChat = DateTime.Now;
                cls.Operator = cmbOperator.Text;
                cls.UserName = txtUserName.Text;
                await cls.SaveAsync(listCity, listAds);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
            finally
            {
                btnFinish.Enabled = true;
            }
        }

        private void txtNumber_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtNumber);
        }

        private void txtOwner_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtOwner);
        }

        private void txtUserName_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtUserName);
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtUserName);
        }

        private void txtOwner_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtOwner);
        }

        private void txtNumber_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtNumber);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmSimcard_Main_KeyDown(object sender, KeyEventArgs e)
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

        private async void frmSimcard_Main_Load(object sender, EventArgs e)
        {
            await FillComboBox();
            await Set_Data();
            var sim = await SimcardBussines.GetAllAsync();
            var _source = new AutoCompleteStringCollection();

            foreach (var item in sim)
            {
                _source.Add(item.OwnerName);
            }

            txtOwner.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtOwner.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtOwner.AutoCompleteCustomSource = _source;
        }
        private async Task Set_Data()
        {
            try
            {
                await FillAds();
                await FillCity();
                txtNumber.Text = cls.Number.ToString();
                txtOwner.Text = cls.OwnerName;
                cmbOperator.Text = cls.Operator;
                txtUserName.Text = cls.UserName;
                await SetCity(cls.Guid);
                await SetAds(cls.Guid);
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }

        private void dgCity_CellClick(object sender, DataGridViewCellEventArgs e)
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
                FarsiMessegeBox.Show(exception.Message);
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
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void dgAds_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgAds.RowCount <= 0) return;
                if (e.ColumnIndex == dg_AdvChecked.Index)
                {
                    if (dgAds.CurrentRow != null)
                    {
                        dgAds[dg_AdvChecked.Index, dgAds.CurrentRow.Index].Value =
                            !(bool)dgAds[dg_AdvChecked.Index, dgAds.CurrentRow.Index].Value;
                    }
                }
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void mnuAdsSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgAds.RowCount <= 0) return;
                if (mnuAdsSelectAll.Checked)
                {
                    foreach (DataGridViewRow row in dgAds.Rows)
                    {
                        if (row.Cells["dg_AdvChecked"] is DataGridViewCheckBoxCell checkBox)
                            checkBox.Value = true;
                    }

                    return;
                }
                //UnCkeched
                foreach (DataGridViewRow row in dgAds.Rows)
                {
                    if (row.Cells["dg_AdvChecked"] is DataGridViewCheckBoxCell checkBox)
                        checkBox.Value = false;
                }
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }
    }
}
