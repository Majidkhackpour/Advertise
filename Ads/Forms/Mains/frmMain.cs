using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using Ads.Forms.Settings;
using Ads.Forms.Simcard;
using BussinesLayer;
using DataLayer;
using DataLayer.Enums;
using FMessegeBox;

namespace Ads.Forms.Mains
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                picDivarNote.Image = Properties.Resources._22;
                picAdsNote.Image = Properties.Resources._22;
                picSimcardNote.Image = Properties.Resources._22;
                picCityNote.Image = Properties.Resources._22;
                picStateNote.Image = Properties.Resources._22;
                picCategoryNote.Image = Properties.Resources._22;
                picSettingNote.Image = Properties.Resources._22;
                picDivar.Image = Properties.Resources._18;
                picLogo.Image = Properties.Resources.AradPngpng;
                picAds.Image = Properties.Resources._045;
                picSimcard.Image = Properties.Resources._38;
                picCity.Image = Properties.Resources._23;
                picState.Image = Properties.Resources._32;
                picCategory.Image = Properties.Resources._15;
                picSetting.Image = Properties.Resources._02;
                picDivarNote.Visible = false;
                picAdsNote.Visible = false;
                picSimcardNote.Visible = false;
                picCityNote.Visible = false;
                picStateNote.Visible = false;
                picCategoryNote.Visible = false;
                picSettingNote.Visible = false;
                lblCateNote.Visible = false;
                lblCategoryNote.Visible = false;
                lblCityNote.Visible = false;
                lblDivarNote.Visible = false;
                lblSettingNote.Visible = false;
                lblSimcardNote.Visible = false;
                lblStateNote.Visible = false;
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private async void picDivar_Click(object sender, EventArgs e)
        {
            try
            {
                var res = SettingBussines.GetAll();
                var clsSetting = res.Count == 0 ? new SettingBussines() : res[0];
                Invoke(new MethodInvoker(async () => await Utility.ManageAdvSend(clsSetting)));
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void picManager_Click(object sender, EventArgs e)
        {
        }

        private void picCategory_MouseEnter(object sender, EventArgs e)
        {
            picCategoryNote.Visible = true;
            lblCateNote.Visible = true;
        }

        private void picCategory_MouseLeave(object sender, EventArgs e)
        {
            picCategoryNote.Visible = false;
            lblCateNote.Visible = false;
        }

        private void picState_MouseEnter(object sender, EventArgs e)
        {
            picStateNote.Visible = true;
            lblStateNote.Visible = true;
        }

        private void picState_MouseLeave(object sender, EventArgs e)
        {
            picStateNote.Visible = false;
            lblStateNote.Visible = false;
        }

        private void picCity_MouseEnter(object sender, EventArgs e)
        {
            picCityNote.Visible = true;
            lblCityNote.Visible = true;
        }

        private void picCity_MouseLeave(object sender, EventArgs e)
        {
            picCityNote.Visible = false;
            lblCityNote.Visible = false;
        }

        private void picAds_MouseEnter(object sender, EventArgs e)
        {
            picAdsNote.Visible = true;
            lblCategoryNote.Visible = true;
        }

        private void picAds_MouseLeave(object sender, EventArgs e)
        {
            picAdsNote.Visible = false;
            lblCategoryNote.Visible = false;
        }

        private void picSimcard_MouseEnter(object sender, EventArgs e)
        {
            picSimcardNote.Visible = true;
            lblSimcardNote.Visible = true;
        }

        private void picSimcard_MouseLeave(object sender, EventArgs e)
        {
            picSimcardNote.Visible = false;
            lblSimcardNote.Visible = false;
        }

        private void picSetting_MouseEnter(object sender, EventArgs e)
        {
            picSettingNote.Visible = true;
            lblSettingNote.Visible = true;
        }

        private void picSetting_MouseLeave(object sender, EventArgs e)
        {
            picSettingNote.Visible = false;
            lblSettingNote.Visible = false;
        }

        private void picDivar_MouseEnter(object sender, EventArgs e)
        {
            picDivarNote.Visible = true;
            lblDivarNote.Visible = true;
        }

        private void picDivar_MouseLeave(object sender, EventArgs e)
        {
            picDivarNote.Visible = false;
            lblDivarNote.Visible = false;
        }

        private async void picCategory_Click(object sender, EventArgs e)
        {
            try
            {
                var allcit = await AdvCategoryBussines.GetAllAsync();
                if (allcit.Count > 0)
                {
                    if (FarsiMessegeBox.Show(
                            "دسته بندی ها پیش از این مقداردهی شده اند در صورت ادامه باید تمامی تنظیمات سیمکارت ها و آگهی ها دوباره انجام دهید. آیا ادامه میدهید؟.",
                            "هشدار", FMessegeBoxButtons.YesNo, FMessegeBoxIcons.Information) == DialogResult.No)
                        return;
                }

                var sh = await SheypoorAdv.GetInstance();
                await sh.GetCategory();

                var d = await DivarAdv.GetInstance();
                await d.GetCategory();

                Utility.CloseAllChromeWindows();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private async void picState_Click(object sender, EventArgs e)
        {
            try
            {
                var allcit = await StateBussiness.GetAllAsync();
                if (allcit.Count > 0)
                {
                    if (FarsiMessegeBox.Show(
                            "استان ها پیش از این مقداردهی شده اند در صورت ادامه باید تمامی تنظیمات سیمکارت ها و آگهی ها دوباره انجام دهید. آیا ادامه میدهید؟.",
                            "هشدار", FMessegeBoxButtons.YesNo, FMessegeBoxIcons.Information) == DialogResult.No)
                        return;
                }
                var path = Path.Combine(Application.StartupPath, "State.txt");
                if (!File.Exists(path))
                {
                    FarsiMessegeBox.Show(
                        $"مسیر فایل نامعتبر می باشد. لطفا فایل متنی لیست استان ها را در مسیر زیر قرار دهید" + "\r\n" +
                        path);
                    return;
                }

                var list = File.ReadAllLines(path);

                var all = await StateBussiness.GetAllAsync();
                if (all.Count > 0)
                {
                    if (!StateBussiness.RemoveAll(all)) return;
                }

                foreach (var item in list)
                {
                    var stat = new StateBussiness
                    {
                        Guid = Guid.NewGuid(),
                        Name = item,
                        DateSabt = DateConvertor.M2SH(DateTime.Now),
                        Status = true
                    };
                    await stat.SaveAsync();
                }

                FarsiMessegeBox.Show($"تعداد {list.Count()} استان بروزرسانی شد");
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private async void picCity_Click(object sender, EventArgs e)
        {
            try
            {
                var allcit = await DivarCityBussines.GetAllAsync();
                if (allcit.Count > 0)
                {
                    if (FarsiMessegeBox.Show(
                            "شهرهای دیوار پیش از این مقداردهی شده اند در صورت ادامه باید تمامی تنظیمات سیمکارت ها و آگهی ها دوباره انجام دهید. آیا ادامه میدهید؟.",
                            "هشدار", FMessegeBoxButtons.YesNo, FMessegeBoxIcons.Information) == DialogResult.No)
                        return;
                }
                var divar = await DivarAdv.GetInstance();
                var list = divar.GetAllCityFromDivar();
                await DivarCityBussines.SaveAsync(list);
                await DivarRegion(list.Count);
                await SheypoorCity();
                Utility.CloseAllChromeWindows();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }
        private async Task DivarRegion(int cityCount)
        {
            try
            {
                var lst = new List<string>();
                lst.Add("اصفهان");
                lst.Add("اهواز");
                lst.Add("تهران");
                lst.Add("شیراز");
                lst.Add("قم");
                lst.Add("کرج");
                lst.Add("مشهد");
                var divar = await DivarAdv.GetInstance();
                var regList = await divar.GetAllRegionFromDivar(lst);
                await RegionBussiness.SaveAsync(AdvertiseType.Divar, regList);
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private async Task SheypoorCity()
        {
            try
            {
                var sh = await SheypoorAdv.GetInstance();
                var list = await sh.GetAllCityFromSheypoor();
                foreach (var item in list)
                {
                    await item.SaveAsync();
                }

                await SheypoorRegion(list.Count);
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
            }
        }
        private async Task SheypoorRegion(int cityCount)
        {
            try
            {
                var lstTupple = new List<Tuple<string, string>>();
                var finalList = new List<RegionBussiness>();
                lstTupple.Add(new Tuple<string, string>("آذربایجان شرقی", "تبریز"));
                lstTupple.Add(new Tuple<string, string>("اصفهان", "اصفهان"));
                lstTupple.Add(new Tuple<string, string>("البرز", "کرج"));
                lstTupple.Add(new Tuple<string, string>("تهران", "تهران"));
                lstTupple.Add(new Tuple<string, string>("خراسان رضوی", "مشهد"));
                lstTupple.Add(new Tuple<string, string>("خوزستان", "اهواز"));
                lstTupple.Add(new Tuple<string, string>("فارس", "شیراز"));
                lstTupple.Add(new Tuple<string, string>("قم", "قم"));
                lstTupple.Add(new Tuple<string, string>("گیلان", "رشت"));
                lstTupple.Add(new Tuple<string, string>("مازندران", "ساری"));
                foreach (var (item1, item2) in lstTupple)
                {
                    var sheypoor = await SheypoorAdv.GetInstance();
                    var regions = await sheypoor.GetAllRegionFromSheypoor(item1, item2);
                    var a = SheypoorCityBussines.GetAsync(item2);
                    foreach (var aRegion in regions)
                    {
                        var clsRegionBusiness = new RegionBussiness()
                        {
                            Guid = Guid.NewGuid(),
                            CityGuid = a.Guid,
                            DateSabt = DateConvertor.M2SH(DateTime.Now),
                            Type = AdvertiseType.Sheypoor,
                            Name = aRegion
                        };
                        finalList.Add(clsRegionBusiness);
                    }
                }
                await RegionBussiness.SaveAsync(AdvertiseType.Sheypoor, finalList);
            }
            catch (Exception ex)
            {
                FarsiMessegeBox.Show(ex.Message);
            }
        }

        private void picAds_Click(object sender, EventArgs e)
        {
            try
            {
                new frmAds().ShowDialog();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void picSimcard_Click(object sender, EventArgs e)
        {
            try
            {
                new frmShow_Simcard().ShowDialog();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private void picSetting_Click(object sender, EventArgs e)
        {
            try
            {
                new frmAdsSettings().ShowDialog();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }
    }
}
