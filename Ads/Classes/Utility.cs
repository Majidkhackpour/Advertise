using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinesLayer;
using DataLayer;
using DataLayer.Enums;
using ErrorHandler;
using FMessegeBox;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.IO.File;

namespace Ads.Classes
{
    public static class Utility
    {
        private const string login = "@uuid:autocontrol;password$";
        [DllImport("wtsapi32.dll", SetLastError = true)]
        static extern bool WTSDisconnectSession(IntPtr hServer, int sessionId, bool bWait);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr tokenHandle);
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);


        const int WtsCurrentSession = -1;
        static readonly IntPtr WtsCurrentServerHandle = IntPtr.Zero;

        public static void Logoff()
        {
            if (!WTSDisconnectSession(WtsCurrentServerHandle, WtsCurrentSession, false))
                throw new Win32Exception();
        }

        public static async Task<string> GetNetworkIpAddress()
        {
            try
            {
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                throw new Exception("No network adapters with an IPv4 address in the system!");
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return null;
            }
        }
        public static async Task<string> GetLocalIpAddress()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://ifconfig.me");

                request.UserAgent = "curl";

                string publicIPAddress;

                request.Method = "GET";
                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        publicIPAddress = reader.ReadToEnd();
                    }
                }

                return publicIPAddress.Replace("\n", "");
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
                return null;
            }

        }

        public static void WriteDefaultLogin(string usr, string pwd)
        {
            //creates or opens the key provided.Be very careful while playing with 
            //windows registry.
            RegistryKey rekey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true);

            if (rekey == null)
                MessageBox.Show
                    (@"There has been an error while trying to write to windows registry");
            else
            {
                //these are our hero like values here
                //simply use your RegistryKey objects SetValue method to set these keys
                rekey.SetValue("AutoAdminLogon", "1");
                rekey.SetValue("DefaultUserName", usr);
                rekey.SetValue("DefaultPassword", pwd);
            }
            //close the RegistryKey object
            if (rekey != null) rekey.Close();
        }

        public static async Task Wait(double second = 1)
        {
            await Task.Delay((int)(second * 1000));
        }

        public static List<string> GetFiles(string path, string filePattern = "*.*")
        {
            return Directory.Exists(path) ? Directory.GetFiles(path, filePattern).ToList() : null;
        }
        public static List<string> ReadFile(string path)
        {
            if (Exists(path)) return ReadAllLines(path).ToList();

            var file = Create(path);
            file.Close();
            return Exists(path) ? ReadAllLines(path).ToList() : null;
        }
        public static List<string> GetFolders(string path)
        {
            return Directory.Exists(path) ? Directory.GetDirectories(path).ToList() : null;
        }
        public static string DateSh(DateTime mDateTime)
        {
            var pc = new PersianCalendar();
            return pc.GetYear(mDateTime) + "/" + pc.GetMonth(mDateTime).ToString("00") + "/" + pc.GetDayOfMonth(mDateTime).ToString("00");
        }

        public static void CloseAllChromeWindows()
        {
            try
            {
                var userName = Environment.UserName;
                foreach (var item in Process.GetProcesses())
                    if (item.ProcessName == "chromedriver" || item.ProcessName == "chrome")
                    {
                        var userOfProcess = GetProcessUser(item);
                        if (userOfProcess == userName)
                            item.Kill();
                    }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private static string GetProcessUser(Process process)
        {
            IntPtr processHandle = IntPtr.Zero;
            try
            {
                OpenProcessToken(process.Handle, 8, out processHandle);
                WindowsIdentity wi = new WindowsIdentity(processHandle);
                string user = wi.Name;
                return user.Contains(@"\") ? user.Substring(user.IndexOf(@"\", StringComparison.Ordinal) + 1) : user;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    CloseHandle(processHandle);
                }
            }
        }

        //public static async Task RegisterSheypoor()
        //{
        //    var sheypoor = new SheypoorAdv();
        //    var newAdv = new AdvertiseLogBusiness
        //    {
        //        SimCardNumber = 9154128712,
        //        Adv = "dsfsdf",
        //        ImagesPathList = new List<string>()

        //    };
        //    await sheypoor.RegisterAdv(newAdv);
        //}

        public static List<string> GetAllFolderAdv(string advRootPath)
        {
            if (string.IsNullOrEmpty(advRootPath)) return null;
            var allFolders = GetFolders(advRootPath);

            if (allFolders?.Count > 0) return allFolders.ToList();

            MessageBox.Show(@"متاسفانه هیچ آگهی ای در مسیر زیر یافت نشد.\r\n" + advRootPath);
            return null;
        }

        public static IWebDriver RefreshDriver(IWebDriver driver)
        {
            try
            {
                if (driver?.Title == null)
                {
                    CloseAllChromeWindows();
                    //var driverOSer = ChromeDriverService.CreateDefaultService();
                    //driverOSer.HideCommandPromptWindow = true;
                    driver = new ChromeDriver();
                    driver?.Manage().Window.Maximize();
                }
            }
            catch
            {
                CloseAllChromeWindows();
                //var driverOSer = ChromeDriverService.CreateDefaultService();
                //driverOSer.HideCommandPromptWindow = true;
                driver = new ChromeDriver();
                driver?.Manage().Window.Maximize();
            }
            return driver;
        }

        public static string GetHtmlCode(string url)
        {
            var data = "";
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (receiveStream != null)
                    {
                        readStream = string.IsNullOrWhiteSpace(response.CharacterSet)
                            ? new StreamReader(receiveStream)
                            : new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    data = readStream?.ReadToEnd();

                    response.Close();
                    readStream?.Close();
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                data = null;
            }

            return data;

        }

        public static async Task<bool> GoToNextSite(AdvertiseType type, short mode)
        {
            try
            {
                //mode 0 => حرکت رو به جلو
                //mode 1 => از سر گیری سایت ها بعد از ریستارت مورم
                //SettingBusiness _cls;
                //var res = await SettingBusiness.GetAllAsync();
                //_cls = res.Count == 0 ? new SettingBusiness() : res[0];
                var path = Path.Combine(Application.StartupPath, "SiteRate.txt");
                var lst = File.ReadAllLines(path).ToList();
                if (!lst.Any()) return false;
                var index = "";
                if (mode == 0)
                {
                    for (var i = 0; i < lst.Count; i++)
                    {
                        if (lst[i] != type.ToString()) continue;
                        if (i + 1 == lst.Count)
                        {
                            index = null;
                            break;
                        }

                        index = lst[i + 1];
                        break;
                    }
                }
                else
                {
                    CloseAllChromeWindows();
                    index = lst[0];
                }

                switch (index)
                {
                    case "Divar":
                        //if (_cls.DivarSetting.CountAdvInIp != 0)
                        //{
                        //    var divar = await DivarAdv.GetInstance();
                        //    await divar.StartRegisterAdv();
                        //    return true;
                        //}
                        break;
                    case "Sheypoor":
                        //if (_cls.SheypoorSetting.CountAdvInIp != 0)
                        //{
                        //    var sheypoor = await SheypoorAdv.GetInstance();
                        //    await sheypoor.StartRegisterAdv();
                        //    return true;
                        //}
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return false;
            }
        }

        public static async Task<string> FindGateWay()
        {
            try
            {
                var gateWay = NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(n => n.OperationalStatus == OperationalStatus.Up)
                    .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                    .Select(g => g?.Address)
                    .FirstOrDefault(a => a != null);
                return gateWay?.ToString() ?? null;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return null;
            }
        }
        public static async Task SetGateway(string gateway)
        {
            try
            {
                //var objMc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                //var objMoc = objMc.GetInstances();
                //foreach (var o in objMoc)
                //{
                //    var objMo = (ManagementObject)o;
                //    if (!(bool)objMo["IPEnabled"]) continue;
                //    var newGateway =
                //        objMo.GetMethodParameters("SetGateways");

                //    newGateway["DefaultIPGateway"] = new string[] { gateway };
                //    newGateway["GatewayCostMetric"] = new int[] { 1 };

                //    var setGateway = objMo.InvokeMethod("SetGateways", newGateway, null);
                //}
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        public static async Task<bool> PingHost(string nameOrAddress)
        {
            var pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                var reply = pinger.Send(nameOrAddress);
                pingable = reply?.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                pingable = false;
            }
            finally
            {
                pinger?.Dispose();
            }

            return pingable;
        }

        public static void ShowBalloon(string title, List<string> body)
        {
            var notifyIcon = new NotifyIcon { Visible = true, Icon = SystemIcons.Application };
            try
            {
                notifyIcon.BalloonTipTitle = title;
                var text = "";
                foreach (var item in body)
                {
                    text += item + '\n';
                }
                notifyIcon.BalloonTipText = text;
                notifyIcon.ShowBalloonTip(30000);

            }
            catch
            {
                // ignored
            }
            finally
            {
                notifyIcon.Dispose();
            }
        }

        static List<int> numbers = new List<int>();
        public static async Task<string> GetRandomGeteWay()
        {
            var gateWay = "";
            try
            {
                var randNum = new Random();
                var filePath = Path.Combine(Application.StartupPath, "GateWay.txt");
                var filelst = ReadAllLines(filePath).ToList();


                var num = 0;
                num = randNum.Next(0, filelst.Count);
                var counter = 0;
                while (numbers.Contains(num))
                {
                    num = randNum.Next(0, filelst.Count);
                    if (counter == filelst.Count) return null;
                    counter++;
                }
                numbers.Add(num);
                gateWay = filelst[num];
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                gateWay = null;
            }

            return gateWay;

        }

        private static List<string> lstMessage = new List<string>();

        public static async Task ManageAdvSend(SettingBussines clsSetting)
        {
            try
            {
                var tokenSource = new CancellationTokenSource();

                SimcardBussines firstSimCardBusiness = null;
                var list = await SimcardBussines.GetAllAsync();
                list = list.OrderBy(q => q.NextUse).Where(q => q.Status).ToList();
                foreach (var simCard in list)
                {
                    while (!await Utility.PingHost("www.google.com"))
                    {
                        //خطا در برقراری اتصال به اینترنت
                        await Utility.Wait(10);
                        lstMessage.Clear();
                        lstMessage.Add("خطا در برقراری اتصال به اینترنت");
                        Utility.ShowBalloon("لطفا شبکه خود را چک نمایید", lstMessage);
                    }

                    if (simCard.IsSendAdv)
                    {
                        //کنترل شماره خروجی
                        if (simCard.Number == 0)
                        {
                            lstMessage.Clear();
                            lstMessage.Add("پر شدن تعداد آگهی در IP");
                            Utility.ShowBalloon("پر شدن تعداد آگهی در " + await Utility.FindGateWay(), lstMessage);
                            continue;
                        }

                        //کنترل تعداد آگهی ارسال شده در هر IP
                        while (clsSetting?.CountAdvInIP <=
                               AdvertiseLogBussines.GetAllAdvInDayFromIP(await Utility.GetLocalIpAddress()))
                        {
                            await Utility.Wait(20);
                            lstMessage.Clear();
                            lstMessage.Add("پر شدن تعداد آگهی");
                            Utility.ShowBalloon("پر شدن تعداد آگهی در " + await Utility.FindGateWay(), lstMessage);
                            await SendChat(clsSetting, simCard);
                        }

                        firstSimCardBusiness = await SimcardBussines.GetAsync(simCard.Number);
                        if (firstSimCardBusiness is null) continue;

                        var lastUse = firstSimCardBusiness.NextUse;
                        var card1 = simCard.Number;
                        var startDayOfCurrentMonthOfDateShToMiladi = DateConvertor.StartDayOfPersianMonth();
                        var startDayOfNextMonthOfDateShToMiladi = DateConvertor.EndDayOfPersianMonth().AddDays(1);
                        //آمار آگهی های ثبت شده برای سیم کارت در ماه جاری
                        var a1 = await AdvertiseLogBussines.GetAllAsync();
                        a1 = a1.Where(p => p.SimCardNumber == card1
                                           && (p.StatusCode ==
                                               StatusCode
                                                   .Published
                                               || p.StatusCode ==
                                               StatusCode
                                                   .InPublishQueue
                                               || p.StatusCode ==
                                               StatusCode
                                                   .WaitForPayment)
                                           && p.DateM >=
                                           startDayOfCurrentMonthOfDateShToMiladi).ToList();

                        var registeredAdvCount = a1.Count;
                        if (registeredAdvCount >= clsSetting?.CountAdvInMounth)
                        {
                            //تاریخ روز اول ماه شمسی بعد را تنظیم می کند چون تا سر ماه بعد دیگر نیازی به این سیم کارت نیست
                            firstSimCardBusiness.NextUse = startDayOfNextMonthOfDateShToMiladi;
                            await firstSimCardBusiness.SaveAsync();
                            lstMessage.Clear();
                            lstMessage.Add(
                                $"سیمکارت {simCard.Number} به دلیل پر بودن آگهی ها در ماه موفق به ارسال آگهی نشد");
                            Utility.ShowBalloon("عدم ارسال آگهی", lstMessage);
                            await SendChat(clsSetting, simCard);
                            continue;
                        }

                        //آمار آگهی های ثبت شده امروز
                        var currentDate = DateTime.Now.Date;
                        var a2 = await AdvertiseLogBussines.GetAllAsync();
                        a2 = a2.Where(p =>
                            p.SimCardNumber == card1
                            && (p.StatusCode == StatusCode.Published
                                || p.StatusCode == StatusCode.InPublishQueue
                                || p.StatusCode == StatusCode.WaitForPayment)
                            && p.DateM >= currentDate).ToList();
                        registeredAdvCount = a2.Count;
                        if (registeredAdvCount >= clsSetting?.CountAdvInDay)
                        {
                            //تاریخ فردا رو ست می کند چون تا فردا دیگه نیازی به این سیم کارت نیست
                            firstSimCardBusiness.NextUse = DateTime.Today.AddDays(1);
                            await firstSimCardBusiness.SaveAsync();
                            lstMessage.Clear();
                            lstMessage.Add(
                                $"سیمکارت {simCard.Number} به دلیل پرپودن آگهی ها در روز موفق به ارسال آگهی نشد");
                            Utility.ShowBalloon("عدم ارسال آگهی", lstMessage);
                            await SendChat(clsSetting, simCard);
                            continue;
                        }

                        var divar = await DivarAdv.GetInstance();
                        await divar.StartRegisterAdv(simCard);

                        await Wait(1);

                        var shey = await SheypoorAdv.GetInstance();
                        await shey.StartRegisterAdv(simCard);

                        //await Wait(60);

                        simCard.NextUse = DateTime.Now.AddMinutes(30);
                        await simCard.SaveAsync();
                        await Wait(10);
                    }

                    if (simCard.IsSendChat)
                        await SendChat(clsSetting, simCard);


                    CloseAllChromeWindows();



                    simCard.NextUse = DateTime.Now.AddHours(1);
                    await simCard.SaveAsync();
                }
                await Utility.Wait(10);
                lstMessage.Clear();
                lstMessage.Add("لیست کاملا پیمایش شد");
                Utility.ShowBalloon("اتمام یک دور کامل از پیمایش سیمکارت ها", lstMessage);
                await UpdateAdvStatus(clsSetting?.DayCountForUpdateState ?? 10);

                CloseAllChromeWindows();

            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private static async Task UpdateAdvStatus(int dayCount = 0)
        {
            try
            {
                var divar = await DivarAdv.GetInstance();
                await divar.UpdateAllAdvStatus(dayCount);

                var sheypoor = await SheypoorAdv.GetInstance();
                await sheypoor.UpdateAllAdvStatus(dayCount);

            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        public static string FixString(this string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input)) return "";
                while (input.Contains("  "))
                    input = input.Replace("  ", " ");
                input = input.Replace("*", "×");
                input = input.Replace("'", "");
                input = input.Replace("ھ", "ه");
                input = input.Replace("ك", "ک");
                input = input.Replace("ي", "ی");
                input = input.Replace("۰", "0").Replace("0", "0").Replace("٠", "0");
                input = input.Replace("۱", "1").Replace("١", "1");
                input = input.Replace("۲", "2").Replace("٢", "2");
                input = input.Replace("٣", "3").Replace("۳", "3");
                input = input.Replace("۴", "4").Replace("٤", "4");
                input = input.Replace("٥", "5").Replace("۵", "5");
                input = input.Replace("۶", "6").Replace("٦", "6");
                input = input.Replace("٧", "7").Replace("۷", "7");
                input = input.Replace("۸", "8").Replace("٨", "8");
                input = input.Replace("۹", "9").Replace("٩", "9");
                return input.Trim();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return null;
            }
        }

        private static async Task SendChat(SettingBussines clsSetting, SimcardBussines simCard)
        {
            try
            {
                if (simCard.IsSendChat)
                {
                    var lst = Directory.GetFiles(clsSetting.FierstLevelChatAddress).ToList();
                    var passage1 = new List<string>();
                    foreach (var item in lst)
                    {
                        var a = File.ReadAllText(item);
                        passage1.Add(a);
                    }
                    var lst2 = Directory.GetFiles(clsSetting.SecondLevelChatAddress).ToList();
                    var passage2 = new List<string>();
                    foreach (var item in lst2)
                    {
                        var a = File.ReadAllText(item);
                        passage2.Add(a);
                    }

                    var city = DivarCityBussines.GetAsync(simCard.DivarCityForChat);

                    var cat1 = AdvCategoryBussines.Get(simCard.DivarChatCat1)?.Name ?? "";
                    var cat2 = AdvCategoryBussines.Get(simCard.DivarChatCat2)?.Name ?? "";
                    var cat3 = AdvCategoryBussines.Get(simCard.DivarChatCat3)?.Name ?? "";

                    var date = DateConvertor.M2SH(DateTime.Now);
                    date = date.Replace("/", "_");
                    var fileName = $"{cat1}__{cat2}__{cat3}__{date}";
                    fileName = fileName.Replace(" ", "_");
                    var ff = Path.Combine(clsSetting.Address, fileName + ".txt");
                    var divar = await DivarAdv.GetInstance();
                    await divar.SendChat(passage1, passage2, simCard.ChatCount, city.Name, cat1, cat2, cat3,
                        ff, simCard);

                    var city1 = SheypoorCityBussines.GetAsync(simCard.SheypoorCityForChat);

                    cat1 = AdvCategoryBussines.Get(simCard.SheypoorChatCat1)?.Name ?? "";
                    cat2 = AdvCategoryBussines.Get(simCard.SheypoorChatCat2)?.Name ?? "";
                    fileName = $"{cat1}__{cat2}__{date}";
                    ff = Path.Combine(clsSetting.Address, fileName + ".txt");

                    var shey = await SheypoorAdv.GetInstance();
                    await shey.SendChat(passage1, passage2, simCard.ChatCount, city1.Name, cat1, cat2, null,
                        ff, simCard);

                    simCard.NextUse = DateTime.Now.AddMinutes(30);
                    await simCard.SaveAsync();
                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        public static async Task<bool> CreateBackUp(string dbName, string fileName, BackUpSettingBussines cls)
        {
            try
            {
                var path = Path.Combine(cls.BackUpAddress, fileName);
                var command = @"BACKUP DATABASE " + dbName + "  TO Disk='" + path + "'";
                var dataConnection = new SqlConnection(Properties.Resources.ConnectionString);
                if (dataConnection.State != ConnectionState.Open)
                    dataConnection.Open();
                var cmd = new SqlCommand { Connection = dataConnection, CommandText = command };
                cmd.ExecuteNonQuery();
                cls.LastBackUpDate = DateConvertor.M2SH(DateTime.Now);
                cls.LastBackUpTime = DateTime.Now.Hour + ":" + DateTime.Now.Minute;
                await cls.SaveAsync();
                if (cls.IsSendInEmail)
                {
                    await SendEmail("aradenj2211@gmail.com", "09382420272", cls.EmailAddress, fileName, "فایل پشتیبان",
                        "smtp.gmail.com", 587, path);
                }
                return true;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
                return false;
            }
        }

        public static async Task<bool> RestoreDatabase(string dbName, string fileName)
        {
            var con = new SqlConnection(Properties.Resources.ConnectionString);
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                var cmd1 =
                    new SqlCommand("ALTER DATABASE [" + dbName + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE ", con);
                cmd1.ExecuteNonQuery();
                var cmd2 =
                    new SqlCommand(
                        "USE MASTER RESTORE DATABASE [" + dbName + "] FROM DISK='" + fileName + "' WITH REPLACE", con);
                cmd2.ExecuteNonQuery();
                var cmd3 = new SqlCommand("ALTER DATABASE [" + dbName + "] SET MULTI_USER", con);
                cmd3.ExecuteNonQuery();
                con.Close();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex, "خطا در بازیابی فایل پشتیبان");
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public static async Task SendEmail(string from, string password, string to, string Message, string subject, string host, int port, string file)
        {
            try
            {
                var email = new MailMessage { From = new MailAddress(@from) };
                email.To.Add(to);
                email.Subject = subject;
                email.Body = Message;
                var smtp = new SmtpClient(host, port) { UseDefaultCredentials = false };
                var nc = new NetworkCredential(from, password);
                smtp.Credentials = nc;
                smtp.EnableSsl = true;
                email.IsBodyHtml = true;
                email.Priority = MailPriority.Normal;
                email.BodyEncoding = Encoding.UTF8;

                if (file.Length > 0)
                {
                    var attachment = new Attachment(file);
                    email.Attachments.Add(attachment);
                }

                smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallBack);
                var userstate = "sending ...";
                smtp.SendAsync(email, userstate);
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }
        private static void SendCompletedCallBack(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
                WebErrorLog.ErrorInstence.StartErrorLog(string.Format("{0} send canceled.", e.UserState), false);
            else if (e.Error != null)
                WebErrorLog.ErrorInstence.StartErrorLog(string.Format("{0} send canceled.", e.UserState), false);
            else
                WebErrorLog.ErrorInstence.StartErrorLog("فایل پشتیبان به ایمیل شما ارسال شد", false);
        }
    }
}
