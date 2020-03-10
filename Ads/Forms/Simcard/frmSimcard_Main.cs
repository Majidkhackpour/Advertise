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
                var list2 = await AdvertiseBussines.GetAllAsync();
                list2 = list2.Where(q => q.Status).ToList();
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
                PostCat1BindingSource.DataSource = divarCat1;


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

                var sheypoorChat1 = await AdvCategoryBussines.GetAllAsync(Guid.Empty, AdvertiseType.Sheypoor);
                sheypoorChat1 = sheypoorChat1.OrderBy(q => q.Name).ToList();
                SheypoorChatBindingSource1.DataSource = sheypoorChat1;

                var postCity = await DivarCityBussines.GetAllAsync();
                postCity = postCity.OrderBy(q => q.Name).ToList();
                PostCityBindingSource.DataSource = postCity;
                cmbPostCity.SelectedIndex = 0;
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
                cls.DivarPostCat1 = (Guid?)cmbPostCat1.SelectedValue ?? null;
                cls.DivarPostCat2 = (Guid?)cmbPostCat2.SelectedValue ?? null;
                cls.DivarPostCat3 = (Guid?)cmbPostCat3.SelectedValue ?? null;
                cls.IsEnableChat = chbIsEnableChat.Checked;
                cls.IsEnableNumber = chbIsEnableNumber.Checked;
                cls.IsSendAdv = chbIsSendAdvDivar.Checked;
                cls.IsSendAdvSheypoor = chbIsSendAdvSheypoor.Checked;
                cls.IsSendChat = chbIsSendChatDivar.Checked;
                cls.IsSendChatSheypoor = chbIsSendChatSheypoor.Checked;
                cls.ChatCount = int.Parse(txtChatCount.Text);
                cls.DivarCityForChat = (Guid)cmbDivarCity.SelectedValue;
                cls.SheypoorCityForChat = (Guid)cmbSheypoorCity.SelectedValue;
                cls.DivarChatCat1 = (Guid)cmbDivarChat1.SelectedValue;
                cls.DivarChatCat2 = (Guid)cmbDivarChat2.SelectedValue;
                cls.DivarChatCat3 = (Guid?)cmbDivarChat3.SelectedValue ?? Guid.Empty;
                cls.SheypoorChatCat1 = (Guid)cmbSheypoorChat1.SelectedValue;
                cls.SheypoorChatCat2 = (Guid)cmbSheypoorChat2.SelectedValue;
                cls.isSendPostToTelegram = chbIsSendPostToTelegram.Checked;
                cls.ChannelForSendPost = txtChannel.Text;
                cls.PostCount = txtPostCount.Text?.ParseToInt() ?? null;
                cls.CityForGetPost = (Guid?)cmbPostCity.SelectedValue ?? null;
                cls.DescriptionForPost = txtPostDescription.Text;
                cls.isSendSecondChat = chbIsSendSecondText.Checked;
                cls.FirstChatPassage = txtFirstChatPassage1.Text;
                cls.FirstChatPassage2 = txtFirstChatPassage2.Text;
                cls.FirstChatPassage3 = txtFirstChatPassage3.Text;
                cls.FirstChatPassage4 = txtFirstChatPassage4.Text;
                cls.SecondChatPassage = txtSecondChatPassage1.Text;
                cls.FirstChatPassage2 = txtFirstChatPassage2.Text;
                cls.FirstChatPassage3 = txtFirstChatPassage3.Text;
                cls.FirstChatPassage4 = txtFirstChatPassage4.Text;
                cls.SMS_Description = txtSMS.Text;

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
                chbIsSendChatDivar.Checked = cls.IsSendChat;
                chbIsSendChatSheypoor.Checked = cls.IsSendChatSheypoor;
                chbIsSendAdvDivar.Checked = cls.IsSendAdv;
                chbIsSendAdvSheypoor.Checked = cls.IsSendAdvSheypoor;
                cmbDivarCity.SelectedValue = cls.DivarCityForChat;
                cmbSheypoorCity.SelectedValue = cls.SheypoorCityForChat;
                chbIsSendPostToTelegram.Checked = cls.isSendPostToTelegram;
                txtChannel.Text = cls.ChannelForSendPost;
                txtPostCount.Text = cls.PostCount?.ToString() ?? "";
                txtPostDescription.Text = cls.DescriptionForPost;
                chbIsSendSecondText.Checked = cls.isSendSecondChat;
                chbIsSendChatDivar.Checked = cls.isSendSecondChat;
                txtFirstChatPassage1.Text = cls.FirstChatPassage;
                txtFirstChatPassage2.Text = cls.FirstChatPassage2;
                txtFirstChatPassage3.Text = cls.FirstChatPassage3;
                txtFirstChatPassage4.Text = cls.FirstChatPassage4;
                txtSecondChatPassage1.Text = cls.SecondChatPassage;
                txtSecondChatPassage2.Text = cls.SecondChatPassage2;
                txtSecondChatPassage3.Text = cls.SecondChatPassage3;
                txtSecondChatPassage4.Text = cls.SecondChatPassage4;
                txtSMS.Text = cls.SMS_Description;
                if (cls.Guid != Guid.Empty)
                {
                    cmbPostCat1.SelectedValue = cls?.DivarPostCat1 ?? Guid.Empty;
                    if (cmbPostCat1.SelectedValue != null && (Guid)cmbPostCat1.SelectedValue != Guid.Empty)
                        cmbPostCat1_SelectedIndexChanged(null, null);
                    cmbPostCat2.SelectedValue = cls?.DivarPostCat2 ?? Guid.Empty;
                    if (cmbPostCat2.SelectedValue != null && (Guid)cmbPostCat2.SelectedValue != Guid.Empty)
                        cmbPostCat2_SelectedIndexChanged(null, null);
                    cmbPostCat3.SelectedValue = cls?.DivarPostCat3 ?? Guid.Empty;
                    cmbPostCity.SelectedValue = cls?.CityForGetPost ?? Guid.Empty;

                    cmbDivarChat1.SelectedValue = cls?.DivarChatCat1 ?? Guid.Empty;
                    cmbDivarChat2.SelectedValue = cls?.DivarChatCat2 ?? Guid.Empty;
                    cmbDivarChat3.SelectedValue = cls?.DivarChatCat3 ?? Guid.Empty;
                    cmbSheypoorChat1.SelectedValue = cls?.SheypoorChatCat1 ?? Guid.Empty;
                    cmbSheypoorChat2.SelectedValue = cls?.SheypoorChatCat2 ?? Guid.Empty;
                }
                grpTelegram.Enabled = chbIsSendPostToTelegram.Checked;
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
        
        private void txtChatCount_Enter_1(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtChatCount);
        }

        private void txtChatCount_Leave_1(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtChatCount);
        }
        
        private async void cmbPostCat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPostCat2.SelectedValue == null) return;
                var divarCat3 = await AdvCategoryBussines.GetAllAsync((Guid)cmbPostCat2.SelectedValue, AdvertiseType.Divar);
                divarCat3 = divarCat3.OrderBy(q => q.Name).ToList();
                PostCat3BindingSource.DataSource = divarCat3;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void cmbPostCat1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbPostCat1.SelectedValue == null) return;
                var divarCat2 = await AdvCategoryBussines.GetAllAsync((Guid)cmbPostCat1.SelectedValue, AdvertiseType.Divar);
                divarCat2 = divarCat2.OrderBy(q => q.Name).ToList();
                PostCat2BindingSource.DataSource = divarCat2;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void cmbDivarChat1_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void chbIsSendPostToTelegram_CheckedChanged(object sender, EventArgs e)
        {
            grpTelegram.Enabled = chbIsSendPostToTelegram.Checked;
        }

        private void btnSearchPostCity_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmShowCity(AdvertiseType.Divar);
                if (frm.ShowDialog() != DialogResult.OK) return;
                if (frm.CityGuid == Guid.Empty) return;
                cmbPostCity.SelectedValue = frm.CityGuid;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void txtChannel_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtChannel);
        }

        private void txtPostCount_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtPostCount);
        }

        private void txtPostDescription_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtPostDescription);
        }

        private void txtFirstChatPassage_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtFirstChatPassage1);
        }

        private void txtSecondChatPassage_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtSecondChatPassage1);
        }

        private void txtSecondChatPassage_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtSecondChatPassage1);
        }

        private void txtFirstChatPassage_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtFirstChatPassage1);
        }

        private void txtPostDescription_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtPostDescription);
        }

        private void txtPostCount_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtPostCount);
        }

        private void txtChannel_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtChannel);
        }

        private async void cmbPostCat1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (cmbPostCat1.SelectedValue == null) return;
                var divarCat2 = await AdvCategoryBussines.GetAllAsync((Guid)cmbPostCat1.SelectedValue, AdvertiseType.Divar);
                divarCat2 = divarCat2.OrderBy(q => q.Name).ToList();
                PostCat2BindingSource.DataSource = divarCat2;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async void cmbPostCat2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (cmbPostCat2.SelectedValue == null) return;
                var divarCat3 = await AdvCategoryBussines.GetAllAsync((Guid)cmbPostCat2.SelectedValue, AdvertiseType.Divar);
                divarCat3 = divarCat3.OrderBy(q => q.Name).ToList();
                PostCat3BindingSource.DataSource = divarCat3;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void txtFirstChatPassage2_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtFirstChatPassage2);
        }

        private void txtFirstChatPassage3_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtFirstChatPassage3);
        }

        private void txtFirstChatPassage4_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtFirstChatPassage4);
        }

        private void txtSecondChatPassage1_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtSecondChatPassage1);
        }

        private void txtFirstChatPassage1_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtFirstChatPassage1);
        }

        private void txtSecondChatPassage2_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtSecondChatPassage2);
        }

        private void txtSecondChatPassage3_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtSecondChatPassage3);
        }

        private void txtSecondChatPassage4_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtSecondChatPassage4);
        }

        private void txtFirstChatPassage1_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtFirstChatPassage1);
        }

        private void txtFirstChatPassage2_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtFirstChatPassage2);
        }

        private void txtFirstChatPassage3_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtFirstChatPassage3);
        }

        private void txtFirstChatPassage4_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtFirstChatPassage4);
        }

        private void txtSecondChatPassage1_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtSecondChatPassage1);
        }

        private void txtSecondChatPassage2_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtSecondChatPassage2);
        }

        private void txtSecondChatPassage3_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtSecondChatPassage3);
        }

        private void txtSecondChatPassage4_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtSecondChatPassage4);
        }

        private void txtSMS_Enter(object sender, EventArgs e)
        {
            txtSetter.Focus(txt2: txtSMS);
        }

        private void txtSMS_Leave(object sender, EventArgs e)
        {
            txtSetter.Follow(txt2: txtSMS);
        }
    }
}
