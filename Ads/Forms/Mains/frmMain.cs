using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ads.Classes;
using Ads.Forms.Settings;
using Ads.Forms.Simcard;
using Ads.Forms.SMS_Panel;
using BussinesLayer;
using DataLayer;
using DataLayer.Enums;
using ErrorHandler;
using FMessegeBox;
using TMS.Class;

namespace Ads.Forms.Mains
{
    public partial class frmMain : Form
    {
        private SettingBussines cls;
        public frmMain()
        {
            InitializeComponent();
            expandablePanel1.Expanded = false;
            Utility.SubmitEvent -= UtilityOnSubmitEvent;
            Utility.SubmitEvent += UtilityOnSubmitEvent;
            var a = SettingBussines.GetAll();
            cls = a.Count > 0 ? a[0] : new SettingBussines();
        }

        private void LoadNewForm(Form frm)
        {
            try
            {
                frm.TopLevel = false;
                frm.AutoScroll = true;
                frm.Dock = DockStyle.Fill;
                pnlContent.Controls.Clear();
                pnlContent.Controls.Add(frm);
                pnlContent.AutoScroll = true;
                frm.Show();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        private void UtilityOnSubmitEvent()
        {
            SetBackUpLables();
        }
        private void PictureManager()
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
                picBackUpNote.Image = Properties.Resources._22;
                picDivar.Image = Properties.Resources._18;
                picLogo.Image = Properties.Resources.AradPngpng;
                picAds.Image = Properties.Resources._045;
                picSimcard.Image = Properties.Resources._38;
                picCity.Image = Properties.Resources._23;
                picState.Image = Properties.Resources._32;
                picCategory.Image = Properties.Resources._15;
                picSetting.Image = Properties.Resources._02;
                picBackUp.Image = Properties.Resources._99;
                picDivarNote.Visible = false;
                picAdsNote.Visible = false;
                picSimcardNote.Visible = false;
                picCityNote.Visible = false;
                picStateNote.Visible = false;
                picCategoryNote.Visible = false;
                picSettingNote.Visible = false;
                picBackUpNote.Visible = false;
                lblCateNote.Visible = false;
                lblCategoryNote.Visible = false;
                lblCityNote.Visible = false;
                lblDivarNote.Visible = false;
                lblSettingNote.Visible = false;
                lblSimcardNote.Visible = false;
                lblStateNote.Visible = false;
                lblBackUpNote.Visible = false;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private async Task GetNaqz()
        {
            try
            {
                while (true)
                {
                    var list = NaqzBussines.GetAll();
                    var rand = new Random().Next(0, list.Count);
                    if (InvokeRequired)
                        Invoke(new MethodInvoker(() => lblNaqz.Text = list[rand].Message));
                    else
                        lblNaqz.Text = list[rand].Message;
                    await Task.Delay(6000000);
                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        private async Task GetProxy()
        {
            var server = "در حال اتصال به پروکسی ...";
            try
            {
                var list = await ProxyBussines.GetAllAsync();
                foreach (var pr in list.ToList())
                {
                    var tt = false;
                    if (!pr.Status) continue;
                    var ts = new Thread(new ThreadStart(async () => tt = await PingHost(pr.Server, pr.Port)));
                    ts.Start();
                    if (tt)
                    {
                        server = pr.Server;
                        return;
                    }
                    server = "عدم اتصال به پروکسی";
                }
            }
            catch
            {
                server = "عدم اتصال به پروکسی";
            }

            Invoke(new MethodInvoker(() => lblServerProxy.Text = server));

        }
        private static async Task<bool> PingHost(string strIP, int intPort)
        {
            var blProxy = false;
            try
            {
                var client = new TcpClient(strIP, intPort);
                blProxy = true;
            }
            catch (SocketException)
            {
                return false;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return false;
            }
            return blProxy;
        }
        private async void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                var expDate = DateConvertor.Sh2M("1399/01/01");
                if (DateTime.Now >= expDate) Application.Exit();
                PictureManager();
                var th = new Thread(new ThreadStart(async () => await GetNaqz()));
                th.Start();
                lblDay.Text = lblNewDate.Text = "";
                var PRD = new MaftooxCalendar.MaftooxPersianCalendar.DateWork();
                lblDay.Text = PRD.GetNameDayInMonth();
                lblNewDate.Text = PRD.GetNumberDayInMonth() + " " + PRD.GetNameMonth() + " " + PRD.GetNumberYear();
                timer1_Tick(null, null);
                lblVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                SetBackUpLables();
                var ts = new Thread(new ThreadStart(async () => await GetProxy()));
                ts.Start();
                await GetDelete();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private async Task GetDelete()
        {
            try
            {
                var list = await AdvertiseLogBussines.GetAllAsync();
                var day = DateTime.Now.AddDays(-cls.DayCountForDelete);
                list = list.Where(q =>
                        q.DateM <= day && q.StatusCode != StatusCode.Deleted && q.StatusCode != StatusCode.Expired)
                    .ToList();
                if (list.Count <= 0) return;
                if (FarsiMessegeBox.Show(
                        $"تعداد {list.Count} آگهی وجود دارد که زمان حذف آنها فرارسیده است. آیا مایلید حذف کنید؟",
                        "حذف آگهی ها از سایت", FMessegeBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var divar = await DivarAdv.GetInstance();
                    await divar.DeleteAllAdvFromDivar(list.OrderBy(q => q.DateM).ToList());
                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private void SetBackUpLables()
        {
            try
            {
                var a = BackUpSettingBussines.GetAll();
                var cls = a.Count > 0 ? a[0] : new BackUpSettingBussines();
                lblLastBackUp.Text = cls.LastBackUpTime + " " + cls.LastBackUpDate;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        private void picDivar_Click(object sender, EventArgs e)
        {
            try
            {
                var res = SettingBussines.GetAll();
                var clsSetting = res.Count == 0 ? new SettingBussines() : res[0];
                var t = new Thread(async () => await Utility.ManageAdvSend(clsSetting));
                t.Start();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void picManager_Click(object sender, EventArgs e)
        {
            WebErrorLog.ErrorInstence.StartErrorLog(new ApplicationException());
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
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
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

                WebErrorLog.ErrorInstence.StartErrorLog($"تعداد {list.Count()} استان بروزرسانی شد", true);
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
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
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
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
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
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
                WebErrorLog.ErrorInstence.StartErrorLog(e);
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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
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
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
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
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
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
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void picBackUp_MouseEnter(object sender, EventArgs e)
        {
            picBackUpNote.Visible = true;
            lblBackUpNote.Visible = true;
        }

        private void picBackUp_MouseLeave(object sender, EventArgs e)
        {
            picBackUpNote.Visible = false;
            lblBackUpNote.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHour.Text = DateTime.Now.Hour.ToString();
            lblMin.Text = DateTime.Now.Minute.ToString();
            lblSec.Visible = !lblSec.Visible;
        }




        private void picBackUp_Click(object sender, EventArgs e)
        {
            try
            {
                new frmBackUpSetting().ShowDialog();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void lblProxy_Click(object sender, EventArgs e)
        {
            try
            {
                new frmProxy().ShowDialog();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            try
            {
                LoadNewForm(new frmChart());
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnAdvLog_Click(object sender, EventArgs e)
        {
            try
            {
                LoadNewForm(new frmAdvertiseLog());
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnShow_Numbers_Click(object sender, EventArgs e)
        {
            try
            {
                LoadNewForm(new frmShowNumbers());
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnSMS_Panel_Click(object sender, EventArgs e)
        {
            try
            {
                LoadNewForm(new frmPanel());
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnLineNumber_Click(object sender, EventArgs e)
        {
            try
            {
                LoadNewForm(new frmLineNumbers());
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new frmSendSMS();
                frm.ShowDialog();
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
    }
}
