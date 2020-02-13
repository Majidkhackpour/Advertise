﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
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
    public partial class frmManagerMain : Form
    {
        public frmManagerMain()
        {
            InitializeComponent();
        }

        private void frmManagerMain_Load(object sender, EventArgs e)
        {
            picSimcard.Image = Properties.Resources.simcard;
            picDivarCity.Image = Properties.Resources.government;
            picState.Image = Properties.Resources.urban;
            picSetting.Image = Properties.Resources.support;
            var tt = new ToolTip();
            tt.SetToolTip(picSimcard, "مدیریت سیمکارت ها");
            tt.SetToolTip(picSetting, "تنظیمات");
            tt.SetToolTip(picDivarCity, "دریافت شهرها از سایت دیوار");
            tt.SetToolTip(picState, "بروزرسانی لیست استان های کشور");
        }

        private void lbSimcard_MouseEnter(object sender, EventArgs e)
        {
            lblSetter.GotFocose(lblSimcard);
        }

        private void lblSimcard_MouseLeave(object sender, EventArgs e)
        {
            lblSetter.LostFocose(lblSimcard);
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

        private void lblSimcard_Click(object sender, EventArgs e)
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

        private void lblDivarCity_MouseEnter(object sender, EventArgs e)
        {
            lblSetter.GotFocose(lblDivarCity);
        }

        private void lblDivarCity_MouseLeave(object sender, EventArgs e)
        {
            lblSetter.LostFocose(lblDivarCity);
        }

        private async void picDivarCity_Click(object sender, EventArgs e)
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
                Utility.CloseAllChromeWindows();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }

        private async void lblDivarCity_Click(object sender, EventArgs e)
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

        private async void lblState_Click(object sender, EventArgs e)
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

        private void lblState_MouseEnter(object sender, EventArgs e)
        {
            lblSetter.GotFocose(lblState);
        }

        private void lblState_MouseLeave(object sender, EventArgs e)
        {
            lblSetter.LostFocose(lblState);
        }

        private async void picRegion_Click(object sender, EventArgs e)
        {

        }

        private async void lblRegion_Click(object sender, EventArgs e)
        {
          
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
                FarsiMessegeBox.Show($"تعداد {cityCount} شهر و {regList.Count} منظقه بروزرسانی شد");
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

        private void lblSetting_Click(object sender, EventArgs e)
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

        private void lblSetting_MouseEnter(object sender, EventArgs e)
        {
            lblSetter.GotFocose(lblSetting);
        }

        private void lblSetting_MouseLeave(object sender, EventArgs e)
        {
            lblSetter.LostFocose(lblSetting);
        }

        private void picSimcard_MouseEnter(object sender, EventArgs e)
        {
            picSimcard.Image = Properties.Resources.simcardRed;
        }

        private void picSimcard_MouseLeave(object sender, EventArgs e)
        {
            picSimcard.Image = Properties.Resources.simcard;
        }

        private void picDivarCity_MouseEnter(object sender, EventArgs e)
        {
            picDivarCity.Image = Properties.Resources.governmentRed;
        }

        private void picDivarCity_MouseLeave(object sender, EventArgs e)
        {
            picDivarCity.Image = Properties.Resources.government;
        }


        private void picState_MouseEnter(object sender, EventArgs e)
        {
            picState.Image = Properties.Resources.urbanRed;
        }

        private void picState_MouseLeave(object sender, EventArgs e)
        {
            picState.Image = Properties.Resources.urban;
        }

        private void picSetting_MouseEnter(object sender, EventArgs e)
        {
            picSetting.Image = Properties.Resources.supportRed;
        }

        private void picSetting_MouseLeave(object sender, EventArgs e)
        {
            picSetting.Image = Properties.Resources.support;
        }

        private async void button1_Click(object sender, EventArgs e)
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

        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var allcit = await SheypoorCityBussines.GetAllAsync();
                if (allcit.Count > 0)
                {
                    if (FarsiMessegeBox.Show(
                            "شهرهای شیپور پیش از این مقداردهی شده اند در صورت ادامه باید تمامی تنظیمات سیمکارت ها و آگهی ها دوباره انجام دهید. آیا ادامه میدهید؟.",
                            "هشدار", FMessegeBoxButtons.YesNo, FMessegeBoxIcons.Information) == DialogResult.No)
                        return;
                }

                var sh = await SheypoorAdv.GetInstance();
                var list = await sh.GetAllCityFromSheypoor();
                foreach (var item in list)
                {
                    await item.SaveAsync();
                }

                await SheypoorRegion(list.Count);
                Utility.CloseAllChromeWindows();
            }
            catch (Exception exception)
            {
                FarsiMessegeBox.Show(exception.Message);
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {
            new frmAds().ShowDialog();
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
                FarsiMessegeBox.Show($"تعداد {cityCount} شهر و {finalList.Count} محله بروزرسانی شد");
            }
            catch (Exception ex)
            {
                FarsiMessegeBox.Show(ex.Message);
            }
        }
    }
}
