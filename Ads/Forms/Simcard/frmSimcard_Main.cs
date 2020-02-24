using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using DataLayer;
using DataLayer.Enums;
using ErrorHandler;
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
            cmSheypoor.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
            cls = new SimcardBussines();
            lblHeader.Text = "افزودن سیمکارت جدید";
        }

        private async Task FillAds()
        {
            try
            {
                var a = SettingBussines.GetAll();
                var list2 = await AdvertiseBussines.GetAllAsync();
                adsBindingSource.DataSource = list2;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        private async Task FillCity()
        {
            try
            {
                var list3 = await DivarCityBussines.GetAllAsync();
                cityBindingSource.DataSource = list3.ToList();

                var list4 = await SheypoorCityBussines.GetAllAsync();
                SheypoorCityBindingSource.DataSource = list4;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
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



                var list1 = await SheypoorSimCityBussines.GetAllAsync(simGuid);
                if (list1.Count <= 0) return;
                foreach (var item in list1)
                    for (var i = 0; i < dgSheypoorCity.RowCount; i++)
                        if (item.CityGuid == ((Guid?)dgSheypoorCity[dg_SheypoorCityGuid.Index, i].Value ?? Guid.Empty))
                            dgSheypoorCity[dg_SheypoorCityChecked.Index, i].Value = true;

            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
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
                        if (item.Advertise == ((Guid?)dgAds[dg_AdvGuid.Index, i].Value ?? Guid.Empty))
                            dgAds[dg_AdvChecked.Index, i].Value = true;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        public frmSimcard_Main(Guid guid)
        {
            InitializeComponent();
            cmAds.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
            cmCity.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
            cmSheypoor.Renderer = new ToolStripProfessionalRenderer(new ContextMenuSetter());
            cls = SimcardBussines.GetAsync(guid);
            lblHeader.Text = "ویرایش سیمکارت";
        }

        private async Task FillComboBox()
        {
            try
            {
                var list = await SimcardBussines.GetAllAsync();
                var a = list.Select(q => q.Operator).Distinct().ToList();
                cmbOperator.DataSource = a;

                var divarCat1 = await AdvCategoryBussines.GetAllAsync(Guid.Empty, AdvertiseType.Divar);
                divarCat1 = divarCat1.OrderBy(q => q.Name).ToList();
                DivarCat1BingingSource.DataSource = divarCat1;


                var SheypoorCat1 = await AdvCategoryBussines.GetAllAsync(Guid.Empty, AdvertiseType.Sheypoor);
                SheypoorCat1 = SheypoorCat1.OrderBy(q => q.Name).ToList();
                SheypoorCat1BingingSource.DataSource = SheypoorCat1;

                var dCity = await DivarCityBussines.GetAllAsync();
                dCity = dCity.OrderBy(q => q.Name).ToList();
                DivarCBindingSource.DataSource = dCity;
                cmbDivarCity.SelectedIndex = 0;

                var shCity = await SheypoorCityBussines.GetAllAsync();
                shCity = shCity.OrderBy(q => q.Name).ToList();
                SheypoorCBindingSource.DataSource = shCity;
                cmbSheypoorCity.SelectedIndex = 0;

                var divarChat1 = await AdvCategoryBussines.GetAllAsync(Guid.Empty, AdvertiseType.Divar);
                divarChat1 = divarChat1.OrderBy(q => q.Name).ToList();
                DivarChatBindingSource1.DataSource = divarChat1;

                var SheypoorChat1 = await AdvCategoryBussines.GetAllAsync(Guid.Empty, AdvertiseType.Sheypoor);
                SheypoorChat1 = SheypoorChat1.OrderBy(q => q.Name).ToList();
                SheypoorChatBindingSource1.DataSource = SheypoorChat1;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
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
                    WebErrorLog.ErrorInstence.StartErrorLog("لطفا شماره را وارد نمایید", false);
                    txtNumber.Focus();
                    return;
                }
                if (txtNumber.Text.Length < 10 || txtNumber.Text.Length > 12)
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("شماره وارد شده اشتباه است", false);
                    txtNumber.Focus();
                    return;
                }
                if (!SimcardBussines.Check_Number(long.Parse(txtNumber.Text), cls.Guid))
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("شماره وارد شده تکراری است", false);
                    txtNumber.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtOwner.Text))
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("لطفا مالک را وارد نمایید", false);
                    txtNumber.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(cmbOperator.Text))
                {
                    WebErrorLog.ErrorInstence.StartErrorLog("لطفا اپراتور را وارد نمایید", false);
                    txtNumber.Focus();
                    return;
                }

                if (!chbIsEnableNumber.Checked && !chbIsEnableChat.Checked)
                {
                    WebErrorLog.ErrorInstence.StartErrorLog(
                        "بنابر قوانین سایت دیوار و شیپور، حداقل یکی از گزینه های نمایش شماره یا ارسال چت باید فعال باشد",
                        false);
                    chbIsEnableNumber.Focus();
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


                var listCitySh = new List<SheypoorSimCityBussines>();
                for (int i = 0; i < dgSheypoorCity.RowCount; i++)
                {
                    if ((bool)dgSheypoorCity[dg_SheypoorCityChecked.Index, i].Value)
                    {
                        var a = new SheypoorSimCityBussines()
                        {
                            Guid = Guid.NewGuid(),
                            DateSabt = DateConvertor.M2SH(DateTime.Now),
                            Status = true,
                            SimcardGuid = cls.Guid,
                            CityGuid = (Guid)dgSheypoorCity[dg_SheypoorCityGuid.Index, i].Value,
                            StateGuid = (Guid)dgSheypoorCity[dg_StateGuid.Index, i].Value
                        };
                        listCitySh.Add(a);
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
                            Advertise = (Guid)dgAds[dg_AdvGuid.Index, i].Value
                        };
                        listAds.Add(a);
                    }
                }



                cls.OwnerName = txtOwner.Text;
                cls.Status = true;
                cls.Number = long.Parse(txtNumber.Text);
                cls.NextUse = DateTime.Now;
                cls.Operator = cmbOperator.Text;
                cls.UserName = txtUserName.Text;
                cls.DivarCatGuid1 = (Guid?)cmbDivarCat1.SelectedValue ?? null;
                cls.DivarCatGuid2 = (Guid?)cmbDivarCat2.SelectedValue ?? null;
                cls.DivarCatGuid3 = (Guid?)cmbDivarCat3.SelectedValue ?? null;
                cls.SheypoorCatGuid1 = (Guid?)cmbSheypoorCat1.SelectedValue ?? null;
                cls.SheypoorCatGuid2 = (Guid?)cmbSheypoorCat2.SelectedValue ?? null;
                cls.IsEnableChat = chbIsEnableChat.Checked;
                cls.IsEnableNumber = chbIsEnableNumber.Checked;
                cls.IsSendAdv = chbIsSendAdv.Checked;
                cls.IsSendChat = chbIsSendChat.Checked;
                cls.ChatCount = int.Parse(txtChatCount.Text);
                cls.DivarCityForChat = (Guid)cmbDivarCity.SelectedValue;
                cls.SheypoorCityForChat = (Guid)cmbSheypoorCity.SelectedValue;
                cls.DivarChatCat1 = (Guid)cmbDivarChat1.SelectedValue;
                cls.DivarChatCat2 = (Guid)cmbDivarChat2.SelectedValue;
                cls.DivarChatCat3 = (Guid?)cmbDivarChat3.SelectedValue ?? Guid.Empty;
                cls.SheypoorChatCat1 = (Guid)cmbSheypoorChat1.SelectedValue;
                cls.SheypoorChatCat2 = (Guid)cmbSheypoorChat2.SelectedValue;

                await cls.SaveAsync(listCity, listAds, listCitySh);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
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
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
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
                txtChatCount.Text = cls.ChatCount.ToString();
                chbIsEnableNumber.Checked = cls.IsEnableNumber;
                chbIsEnableChat.Checked = cls.IsEnableChat;
                chbIsSendChat.Checked = cls.IsSendChat;
                chbIsSendAdv.Checked = cls.IsSendAdv;
                cmbDivarCity.SelectedValue = cls.DivarCityForChat;
                cmbSheypoorCity.SelectedValue = cls.SheypoorCityForChat;
                if (cls.Guid != Guid.Empty)
                {
                    cmbDivarCat1.SelectedValue = cls?.DivarCatGuid1 ?? null;
                    cmbDivarCat2.SelectedValue = cls?.DivarCatGuid2 ?? null;
                    cmbDivarCat3.SelectedValue = cls?.DivarCatGuid3 ?? Guid.Empty;
                    cmbSheypoorCat1.SelectedValue = cls?.SheypoorCatGuid1 ?? null;
                    cmbSheypoorCat2.SelectedValue = cls?.SheypoorCatGuid2;

                    cmbDivarChat1.SelectedValue = cls?.DivarChatCat1 ?? Guid.Empty;
                    cmbDivarChat2.SelectedValue = cls?.DivarChatCat2 ?? Guid.Empty;
                    cmbDivarChat3.SelectedValue = cls?.DivarChatCat3 ?? Guid.Empty;
                    cmbSheypoorChat1.SelectedValue = cls?.SheypoorChatCat1 ?? Guid.Empty;
                    cmbSheypoorChat2.SelectedValue = cls?.SheypoorChatCat2 ?? Guid.Empty;
                }
                await SetCity(cls.Guid);
                await SetAds(cls.Guid);
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
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
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
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
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void dgSheypoorCity_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgSheypoorCity.RowCount <= 0) return;
                if (e.ColumnIndex == dg_SheypoorCityChecked.Index)
                {
                    if (dgSheypoorCity.CurrentRow != null)
                    {
                        dgSheypoorCity[dg_SheypoorCityChecked.Index, dgSheypoorCity.CurrentRow.Index].Value =
                            !(bool)dgSheypoorCity[dg_SheypoorCityChecked.Index, dgSheypoorCity.CurrentRow.Index].Value;
                    }
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void mnuSheypoorSelectAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgSheypoorCity.RowCount <= 0) return;
                if (mnuSheypoorSelectAll.Checked)
                {
                    foreach (DataGridViewRow row in dgSheypoorCity.Rows)
                    {
                        if (row.Cells["dg_SheypoorCityChecked"] is DataGridViewCheckBoxCell checkBox)
                            checkBox.Value = true;
                    }

                    return;
                }
                //UnCkeched
                foreach (DataGridViewRow row in dgSheypoorCity.Rows)
                {
                    if (row.Cells["dg_SheypoorCityChecked"] is DataGridViewCheckBoxCell checkBox)
                        checkBox.Value = false;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void cmbDivarCat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var divarCat3 = await AdvCategoryBussines.GetAllAsync((Guid)cmbDivarCat2.SelectedValue, AdvertiseType.Divar);
                divarCat3 = divarCat3.OrderBy(q => q.Name).ToList();
                DivarCat3BingingSource.DataSource = divarCat3;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void cmbDivarCat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var divarCat2 = await AdvCategoryBussines.GetAllAsync((Guid)cmbDivarCat1.SelectedValue, AdvertiseType.Divar);
                divarCat2 = divarCat2.OrderBy(q => q.Name).ToList();
                DivarCat2BingingSource.DataSource = divarCat2;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void cmbSheypoorCat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var sheypoorCat2 = await AdvCategoryBussines.GetAllAsync((Guid)cmbSheypoorCat1.SelectedValue, AdvertiseType.Sheypoor);
                sheypoorCat2 = sheypoorCat2.OrderBy(q => q.Name).ToList();
                SheypoorCat2BingingSource.DataSource = sheypoorCat2;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void cmbDivarChat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var divarCat2 = await AdvCategoryBussines.GetAllAsync((Guid)cmbDivarChat1.SelectedValue, AdvertiseType.Divar);
                divarCat2 = divarCat2.OrderBy(q => q.Name).ToList();
                DivarChatBindingSource2.DataSource = divarCat2;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void cmbDivarChat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var divarCat3 = await AdvCategoryBussines.GetAllAsync((Guid)cmbDivarChat2.SelectedValue, AdvertiseType.Divar);
                divarCat3 = divarCat3.OrderBy(q => q.Name).ToList();
                DivarChatBindingSource3.DataSource = divarCat3;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void cmbSheypoorChat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var sheypoorCat2 = await AdvCategoryBussines.GetAllAsync((Guid)cmbSheypoorChat1.SelectedValue, AdvertiseType.Sheypoor);
                sheypoorCat2 = sheypoorCat2.OrderBy(q => q.Name).ToList();
                SheypoorChatBindingSource2.DataSource = sheypoorCat2;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void txtChatCount_Enter_1(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtChatCount);
        }

        private void txtChatCount_Leave_1(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtChatCount);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmShowCity(AdvertiseType.Divar);
                if (frm.ShowDialog() != DialogResult.OK) return;
                if (frm.CityGuid == Guid.Empty) return;
                cmbDivarCity.SelectedValue = frm.CityGuid;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }

        }

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmShowCity(AdvertiseType.Sheypoor);
                if (frm.ShowDialog() != DialogResult.OK) return;
                if (frm.CityGuid == Guid.Empty) return;
                cmbSheypoorCity.SelectedValue = frm.CityGuid;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }

        }
    }
}
