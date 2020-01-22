using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityCache.Business.Advertise;
using EntityCache.Business.Advertise.Setting;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PacketParser.Entities;
using static System.IO.File;

namespace AdvertiseApp.Classes
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
                WebErrorLog.ErrorLogInstance.StartLog(ex);
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

        public static async Task Wait(double second = 0.5)
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
                WebErrorLog.ErrorLogInstance.StartLog(ex);
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

        public static async Task RegisterSheypoor()
        {
            var sheypoor = new SheypoorAdv();
            var newAdv = new AdvertiseLogBusiness
            {
                SimCardNumber = 9154128712,
                Adv = "dsfsdf",
                ImagesPathList = new List<string>()

            };
            await sheypoor.RegisterAdv(newAdv);
        }

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
                    driver = new ChromeDriver();
                    driver?.Manage().Window.Maximize();
                }
            }
            catch (Exception ex)
            {
                CloseAllChromeWindows();
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                //  WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
            return driver;
        }

        public static string GetHtmlCode(string url)
        {
            var data = "";
            var monitor = new PerfMonitor();
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
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
            finally { monitor.Dispose(); }

            return data;

        }

        public static async Task<bool> GoToNextSite(AdvertiseType type, short mode)
        {
            try
            {
                //mode 0 => حرکت رو به جلو
                //mode 1 => از سر گیری سایت ها بعد از ریستارت مورم
                SettingBusiness _cls;
                var res = await SettingBusiness.GetAllAsync();
                _cls = res.Count == 0 ? new SettingBusiness() : res[0];
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
                        if (_cls.DivarSetting.CountAdvInIp != 0)
                        {
                            var divar = await DivarAdv.GetInstance();
                            await divar.StartRegisterAdv();
                            return true;
                        }
                        break;
                    case "Sheypoor":
                        if (_cls.SheypoorSetting.CountAdvInIp != 0)
                        {
                            var sheypoor = await SheypoorAdv.GetInstance();
                            await sheypoor.StartRegisterAdv();
                            return true;
                        }
                        break;
                    case "NiazKade":
                        if (_cls.NiazSetting.CountAdvInIp != 0)
                        {
                            var niaz = await NiazAdv.GetInstance();
                            await niaz.StartRegisterAdv();
                            return true;
                        }
                        break;
                    case "NiazmandyHa":
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
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
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return null;
            }
        }
        public static async Task SetGateway(string gateway)
        {
            try
            {
                var objMc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                var objMoc = objMc.GetInstances();
                foreach (var o in objMoc)
                {
                    var objMo = (ManagementObject)o;
                    if (!(bool)objMo["IPEnabled"]) continue;
                    var newGateway =
                        objMo.GetMethodParameters("SetGateways");

                    newGateway["DefaultIPGateway"] = new string[] { gateway };
                    newGateway["GatewayCostMetric"] = new int[] { 1 };

                    var setGateway = objMo.InvokeMethod("SetGateways", newGateway, null);
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
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
                //var frm = new frmNotification(title, body);
                //frm.Show();
            }
            catch
            {
                // ignored
            }
            finally
            {
                //notifyIcon.Dispose();
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
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                gateWay = null;
            }

            return gateWay;

        }

        public static async Task ChangeIp()
        {
            var client = new TcpClient("192.168.1.66", 5555);
            try
            {
                if (client.Connected)
                {
                    client.Close();
                    client.Dispose();
                    client = new TcpClient();
                }
                client.Connect(IPAddress.Parse("192.168.1.66"), 5555);

                // create post data to be sent
                var postDataAsString = @"@uuid:autocontrol;password$";

                var postDataBinary = Encoding.UTF8.GetBytes(postDataAsString);

                // make post request
                client.Client.Send(postDataBinary);

                // get response
                var bytes = new byte[2048];
                var lengthOfResponse = client.Client.Receive(bytes);
                var log = Encoding.UTF8.GetString(bytes, 0, lengthOfResponse);
                if (log == login)
                {
                    //MessageBox.Show("logined");
                    client.Client.Send(Encoding.UTF8.GetBytes(@"@SetRelay001:ON:10000$"));
                    client.Client.Receive(bytes);
                    //MessageBox.Show(System.Text.Encoding.UTF8.GetString(bytes, 0, lengthOfResponse));
                }
                else
                {
                    throw new NotImplementedException("خطای bms");
                }
            }
            catch (Exception e)
            {
                try
                {
                    client.Close();
                    client.Dispose();
                    client = new TcpClient();
                }
                catch
                {


                }
                throw;
            }
        }

        private static List<string> lstMessage = new List<string>();

        public static async Task ManageAdvSend()
        {
            try
            {
                var tokenSource = new CancellationTokenSource();
                var res = await SettingBusiness.GetAllAsync();
                var cls = res.Count == 0 ? new SettingBusiness() : res[0];
                short maxDailyCountDivar = 1;
                short maxMountlyCountDivar = 1;
                short maxAdvCountInIpInDayDivar = 1;


                short maxDailyCountSheypoor = 1;
                short maxMountlyCountSheypoor = 1;
                short maxAdvCountInIpInDaySheypoor = 1;

                short maxDailyCountNiazmandyHa = 1;
                short maxMountlyCountNiazmandyHa = 1;
                short maxAdvCountInIpInDayNiazmandyHa = 1;
                var filePath = Path.Combine(Application.StartupPath, "GateWay.txt");
                var filelst = ReadAllLines(filePath).ToList();
                var GateWayCount = 0;

                while (!tokenSource.IsCancellationRequested)
                {
                    var isIpFull = true;
                    for (var i = (int)AdvertiseType.Divar; i <= (int)AdvertiseType.NiazmandyHa; i++)
                    {
                        var lst = new List<long>();
                        switch (i)
                        {
                            case (int)AdvertiseType.Divar:
                                maxDailyCountDivar = (short)cls.DivarSetting.AdvCountInDay;
                                maxMountlyCountDivar = (short)cls.DivarSetting.AdvCountInMonth;
                                maxAdvCountInIpInDayDivar = (short)cls.DivarSetting.CountAdvInIp;
                                break;
                            case (int)AdvertiseType.Sheypoor:
                                maxDailyCountSheypoor = (short)cls.SheypoorSetting.AdvCountInDay;
                                maxMountlyCountSheypoor = (short)cls.SheypoorSetting.AdvCountInMonth;
                                maxAdvCountInIpInDaySheypoor = (short)cls.SheypoorSetting.CountAdvInIp;
                                break;
                            case (int)AdvertiseType.NiazKade:
                                break;
                            case (int)AdvertiseType.NiazmandyHa:
                                maxDailyCountNiazmandyHa = (short)cls.NiazmandyHaSetting.AdvCountInDay;
                                maxMountlyCountNiazmandyHa = (short)cls.NiazmandyHaSetting.AdvCountInMonth;
                                maxAdvCountInIpInDayNiazmandyHa = (short)cls.NiazmandyHaSetting.CountAdvInIp;
                                break;
                        }

                        var number = await SimCardBusiness.GetNextSimCardNumberAsync(
                            (short)i, maxDailyCountDivar, maxDailyCountSheypoor, await GetLocalIpAddress(),
                            maxMountlyCountDivar, maxMountlyCountSheypoor, maxAdvCountInIpInDayDivar,
                            maxAdvCountInIpInDaySheypoor, maxDailyCountNiazmandyHa, maxMountlyCountNiazmandyHa,
                            maxAdvCountInIpInDayNiazmandyHa);
                        if (number != 0)
                        {
                            isIpFull = false;
                            lst.Add(number);
                            if (i == (int)AdvertiseType.Divar)
                            {
                                if (await PingHost("185.105.239.1"))
                                {
                                    var divar = await DivarAdv.GetInstance();
                                    await divar.StartRegisterAdv(lst, maxDailyCountDivar);
                                }
                            }
                            else if (i == (int)AdvertiseType.Sheypoor)
                            {
                                if (await PingHost("185.105.239.1"))
                                {
                                    var sheypoor = await SheypoorAdv.GetInstance();
                                    await sheypoor.StartRegisterAdv(lst, maxDailyCountSheypoor);
                                }
                            }
                            else if (i == (int)AdvertiseType.NiazKade)
                            {
                                isIpFull = false;
                                continue;
                            }
                            else if (i == (int)AdvertiseType.DivarChat)
                            {
                                isIpFull = false;
                                continue;
                            }
                            else if (i == (int)AdvertiseType.NiazmandyHa)
                            {
                                //if (await PingHost("185.105.239.1"))
                                //{
                                //    var niazmandyHa = await NiazmandyHaAdv.GetInstance();
                                //    await niazmandyHa.StartRegisterAdv(lst, maxDailyCountNiazmandyHa);
                                //}
                                isIpFull = false;
                                continue;
                            }
                        }
                        CloseAllChromeWindows();
                        if (!isIpFull) continue;
                        GateWayCount++;
                        if (GateWayCount > filelst.Count)
                        {
                            numbers = new List<int>();
                            if (await FindGateWay() == IP_Store.IP_Mokhaberat.Value)
                            {
                                await ChangeIp();
                                await Wait(60);
                            }

                            while (await GetLocalIpAddress() == null)
                            {
                                await Wait(10);
                                lstMessage.Clear();
                                lstMessage.Add("مودم مخابرات ریست شد. لطفا منتظر بمانید");
                                ShowBalloon("درحال اتصال...", lstMessage);
                            }
                            await UpdateAdvStatus(1);
                            i = -1;
                            GateWayCount = 0;
                            continue;
                        }

                        var gateway = await GetRandomGeteWay();
                        if (gateway == null) continue;
                        await SetGateway(gateway);
                        lstMessage.Clear();
                        lstMessage.Add(" GateWay تعویض شد. لطفا منتظر بمانید");
                        ShowBalloon("درحال اتصال...", lstMessage);
                        while (await GetLocalIpAddress() == null)
                        {
                            await Wait(2);
                            lstMessage.Add(" GateWay تعویض شد. لطفا منتظر بمانید");
                            ShowBalloon("درحال اتصال...", lstMessage);
                        }

                        //GateWayCount = 0;
                        i = -1;

                    }

                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
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

                var niazmand = await NiazmandyHaAdv.GetInstance();
                await niazmand.UpdateAllAdvStatus(dayCount);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
        }
    }
}
