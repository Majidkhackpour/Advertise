using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Ads.Classes;
using BussinesLayer;
using DataLayer;

namespace Ads
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnState_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Application.StartupPath, "State.txt");
            var list = File.ReadAllLines(path);
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
        }

        private async void btnDivarCity_Click(object sender, EventArgs e)
        {
            var divar = await DivarAdv.GetInstance();
            var list = divar.GetAllCityFromDivar();
            foreach (var item in list)
            {
                //await item.SaveAsync();
            }
        }

        private async void btnInsSim_Click(object sender, EventArgs e)
        {
            var a = new SimcardBussines
            {
                Guid = Guid.NewGuid(),
                OwnerName = txtOwner.Text,
                DateSabt = DateConvertor.M2SH(DateTime.Now),
                Status = true,
                Number = long.Parse(txtNumber.Text),
                NextUseDivar = DateTime.Now,
                NextUseSheypoor = DateTime.Now,
                NextUseDivarChat = DateTime.Now,
                Operator = txtOperator.Text,
                UserName = txtUserName.Text
            };
            await a.SaveAsync();
        }

        private async void btnLogIn_Click(object sender, EventArgs e)
        {
            var divar = await DivarAdv.GetInstance();
            await divar.Login(long.Parse(txtLogin.Text));
        }

        private async void btnInsSetting_Click(object sender, EventArgs e)
        {
            var r = new SettingBussines
            {
                Guid = Guid.NewGuid(),
                DateSabt = DateConvertor.M2SH(DateTime.Now),
                Status = true,
                DivarCat3 = txtD_Cat3.Text,
                DivarCat1 = txtD_Cat1.Text,
                //DivarPicPath = txtD_Address.Text,
                CountAdvInDayDivar = int.Parse(txtD_CountInDay.Text),
                CountAdvInDaySheypoor = int.Parse(txtSh_CountInDay.Text),
                CountAdvInIPDivar = int.Parse(txtD_CountInIP.Text),
                CountAdvInIPSheypoor = int.Parse(txtSh_CountInIP.Text),
                CountAdvInMounthDivar = int.Parse(txtD_CountInMounth.Text),
                CountAdvInMounthSheypoor = int.Parse(txtSh_CountInMounth.Text),
                DivarCat2 = txtD_Cat2.Text,
                DivarDayCountForUpdateState = 10,
                SheypoorCat1 = txtSh_Cat1.Text,
                SheypoorCat2 = txtSh_Cat2.Text,
                SheypoorCat3 = txtSh_Cat3.Text,
                SheypoorDayCountForUpdateState = 10,
               // SheypoorPicPath = txtSh_Address.Text,
                DivarMaxImgCount = int.Parse(txtDivarMaxImg.Text),
                SheypoorMaxImgCount = int.Parse(txtSheypoorMaxImg.Text)
            };
            await r.SaveAsync();
        }

        private void btnD_Search_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                txtD_Address.Text = folderBrowserDialog1.SelectedPath;
        }

        private void btnSh_Search_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
                txtSh_Address.Text = folderBrowserDialog2.SelectedPath;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //Fill Number ComboBox
            var list1 = await SimcardBussines.GetAllAsync();
            SimcardBindingSource.DataSource = list1;

            //Fill Ads To Grid
            var list2 = await Advertise.GetAllAsync("D:\\DivarTest");
            SimcardAdvBindingSource.DataSource = list2;

            //Fill Divar City To Grid
            var list3 = await DivarCityBussines.GetAllAsync();
            divarCityBussinesBindingSource.DataSource = list3.ToList();
        }

        private async void btnSendAdv_Click(object sender, EventArgs e)
        {
            var divar = await DivarAdv.GetInstance();
            await divar.StartRegisterAdv();
        }

        private async void btnInsSimcardAds_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgSimcardAds.RowCount; i++)
            {
                if ((bool)dgSimcardAds[dg_isChecked.Index, i].Value)
                {
                    var a = new SimcardAdsBussines()
                    {
                        Guid = Guid.NewGuid(),
                        DateSabt = DateConvertor.M2SH(DateTime.Now),
                        Status = true,
                        SimcardGuid = (Guid)cmbSimcard.SelectedValue,
                        AdsName = dgSimcardAds[dg_AdvName.Index, i].Value.ToString()
                    };
                    await a.SaveAsync();
                }
            }

        }

        private async void btnIns_DivarSimCity_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgDivarSimCity.RowCount; i++)
            {
                if ((bool)dgDivarSimCity[dg_Is_Checked.Index, i].Value)
                {
                    var a = new DivarSimCityBussines()
                    {
                        Guid = Guid.NewGuid(),
                        DateSabt = DateConvertor.M2SH(DateTime.Now),
                        Status = true,
                        SimcardGuid = (Guid)cmbSim2.SelectedValue,
                        CityGuid = (Guid)dgDivarSimCity[dg_Guid.Index, i].Value
                    };
                    await a.SaveAsync();
                }
            }
        }

        private async void btnRegion_Click(object sender, EventArgs e)
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
            foreach (var item in regList)
            {
                //await item.SaveAsync();
            }
        }

        private async void btnDivarChatLogin_Click(object sender, EventArgs e)
        {
            var divar = await DivarAdv.GetInstance();
            await divar.LoginChat(long.Parse(textBox1.Text));
        }

        private async void btnSendDivarChat_Click(object sender, EventArgs e)
        {
            var list = new List<string>();
            list.Add("نرم افزار حسابداری نوین پرداز...... تلفن جهت هماهنگی و مشاوره 05137597590");
            var divar = await DivarAdv.GetInstance();
            await divar.SendChat(list, 5, "تهران", null, null, null);
        }
    }
}
