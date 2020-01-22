using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdvertiseApp.Enums;
using EntityCache.Business.Advertise;
using EntityCache.Business.Advertise.Setting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using PacketParser.BusinessLayer;
using PacketParser.Entities;
using static AdvertiseApp.Classes.Utility;
using PacketParser;
using Cookie = OpenQA.Selenium.Cookie;
using Timer = System.Windows.Forms.Timer;


namespace AdvertiseApp.Classes
{
    public class DivarAdv
    {
        #region Fields
        private IWebDriver _driver;
        public int MaxImgForAdv { get; }
        //private readonly string _advRootPath;
        private static SettingBusiness clsSetting;
        public string AdvRootPath => ConfigurationManager.AppSettings.Get("RootPath");

        public List<Advertise> AdvertiseList { get; set; }
        // public List<string> AdvList { get; }
        #endregion

        private static DivarAdv _me;
        public CancellationTokenSource TokenSource;
        public SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1);

        private DivarAdv()
        {
            // if (string.IsNullOrEmpty(_advRootPath)) _advRootPath = ConfigurationManager.AppSettings.Get("RootPath");
        }

        public static async Task<DivarAdv> GetInstance()
        {
            await GetDataFromSetting();
            return _me ?? (_me = new DivarAdv());
        }
        private static async Task<SettingBusiness> GetDataFromSetting()
        {
            try
            {
                var res = await SettingBusiness.GetAllAsync();
                clsSetting = res.Count == 0 ? new SettingBusiness() : res[0];
                return clsSetting;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return null;
            }
        }

        #region Ctors
        /// <summary>
        /// اگر مسیر آگهی داده نشود از مسیر پیش فرض فایل کانفیگ استفاده خواهد شد
        /// </summary>
        /// <param name="advRootPath"></param>
        /// <param name="maxImgForAnyAdv"></param>
        public DivarAdv(int maxImgForAnyAdv = 3)
        {
            //AdvRootPath = advRootPath;
            //if (string.IsNullOrEmpty(AdvRootPath)) AdvRootPath = ConfigurationManager.AppSettings.Get("RootPath");
            MaxImgForAdv = maxImgForAnyAdv;

            _driver = RefreshDriver(_driver);
        }

        #endregion
        #region MyRegion

        private List<string> lstMessage = new List<string>();
        public async Task StartRegisterAdv(List<long> numbers = null, int count = 1)
        {
            //if (SemaphoreSlim.CurrentCount == 0)
            //{
            //    DialogResult result;
            //    result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Question);
            //    if (result == DialogResult.Yes)
            //        TokenSource?.Cancel();
            //    else return;
            //}
            var counter = 0;
            TokenSource?.Cancel();
            //await SemaphoreSlim.WaitAsync();
            TokenSource = new CancellationTokenSource();

            var monitor = new PerfMonitor();
            var isLogin = false;
            try
            {
                while (await PingHost("185.105.239.1") == false)
                {
                    if (counter == 30)
                        await SetGateway(await GetRandomGeteWay());
                    await Wait(10);
                    lstMessage.Clear();
                    lstMessage.Add("خطای اتصال به شبکه");
                    ShowBalloon("لطفا اتصال به شبکه را چک نمایید", lstMessage);
                    counter++;
                }

                //while (!TokenSource.IsCancellationRequested)
                //{
                //    counter = 0;
                //    SimCardBusiness firstSimCardBusiness = null;
                //اگر نامبر صفر نباشد یعنی کاربر خواسته روی شماره ای خاص آگهی بزند
                //اگر صفر باشد روی تمام سیم کارتها داخل حلقه وایل، آگهی ثبت می شود
                //if (numbers == null || numbers.Count == 0)
                //{

                //    _driver = RefreshDriver(_driver);

                //    //     MessageBox.Show(_driver.WindowHandles.Count.ToString());
                //    while (await PingHost("185.105.239.1"))
                //    {
                //        var simCard =
                //            await SimCardBusiness.GetNextSimCardNumberAsync((short)AdvertiseType.Divar,
                //                (short)clsSetting.DivarSetting.AdvCountInDay, await GetLocalIpAddress());
                //        //کنترل شماره خروجی
                //        if (simCard == 0)
                //        {
                //            ShowBalloon("پر شدن تعداد آگهی در " + await FindGateWay(),
                //                "سیستم در حال تعویض IP یا سایت می باشد");
                //            var gateway = await GetRandomGeteWay();
                //            if (!string.IsNullOrEmpty(gateway))
                //                await SetGateway(gateway);
                //            else
                //                await SetGateway(IP_Store.IP_Mokhaberat.Value);
                //            var currentIp = await GetLocalIpAddress();

                //            if (await FindGateWay() != IP_Store.IP_Mokhaberat.Value) continue;
                //            if (await GoToNextSite(AdvertiseType.Divar, 0)) continue;
                //            await ChangeIp();
                //            //await Wait(90);
                //            while (await GetLocalIpAddress() == null)
                //            {
                //                if (counter >= 8)
                //                    await SetGateway(await GetRandomGeteWay());
                //                await Wait(10);
                //                ShowBalloon("درحال اتصال...", "مودم مخابرات ریست شد. لطفا منتظر بمانید");
                //                counter++;
                //            }

                //            if (await GoToNextSite(AdvertiseType.Divar, 1)) continue;
                //        }



                //        //کنترل تعداد آگهی ارسال شده در هر IP
                //        if (clsSetting?.DivarSetting.CountAdvInIp <= await
                //                   AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                //                       AdvertiseType.Divar))

                //        {
                //            ShowBalloon("پر شدن تعداد آگهی در " + await FindGateWay(),
                //                "سیستم در حال تعویض IP یا سایت می باشد");
                //            var gateway = await GetRandomGeteWay();
                //            if (!string.IsNullOrEmpty(gateway))
                //                await SetGateway(gateway);
                //            else
                //                await SetGateway(IP_Store.IP_Mokhaberat.Value);
                //            var currentIp = await GetLocalIpAddress();

                //            if (await FindGateWay() != IP_Store.IP_Mokhaberat.Value ||
                //                !(clsSetting?.DivarSetting.CountAdvInIp <= await
                //                      AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                //                          AdvertiseType.Divar))) continue;
                //            if (await GoToNextSite(AdvertiseType.Divar, 0)) continue;
                //            await ChangeIp();
                //            //await Wait(90);
                //            while (await GetLocalIpAddress() == null)
                //            {
                //                if (counter == 30)
                //                    await SetGateway(await GetRandomGeteWay());
                //                await Wait(10);
                //                ShowBalloon("درحال اتصال...", "مودم مخابرات ریست شد. لطفا منتظر بمانید");
                //                counter++;
                //            }

                //            if (await GoToNextSite(AdvertiseType.Divar, 1)) continue;

                //        }
                //            firstSimCardBusiness = await SimCardBusiness.GetAsync(simCard);
                //        if (firstSimCardBusiness is null) break;

                //        var lastUseDivar = firstSimCardBusiness.NextUseDivar;

                //        var tt = await AdvTokensBusiness.GetToken(simCard, AdvertiseType.Divar);
                //        var hasToken = tt?.Token ?? null;
                //        if (string.IsNullOrEmpty(hasToken)) continue;


                //        var card1 = simCard;
                //        var startDayOfCurrentMonthOfDateShToMiladi = Calendar.StartDayOfPersianMonth();
                //        var startDayOfNextMonthOfDateShToMiladi = Calendar.EndDayOfPersianMonth().AddDays(1);
                //        //آمار آگهی های ثبت شده برای سیم کارت در ماه جاری
                //        var a1 = await AdvertiseLogBusiness.GetAllSpecialAsync(p => p.SimCardNumber == card1
                //                                                                    && (p.StatusCode ==
                //                                                                        (short)StatusCode
                //                                                                            .Published
                //                                                                        //|| p.StatusCode == (short)StatusCode.Deleted
                //                                                                        || p.StatusCode ==
                //                                                                        (short)StatusCode
                //                                                                            .InPublishQueue
                //                                                                        || p.StatusCode ==
                //                                                                        (short)StatusCode
                //                                                                            .WaitForPayment)
                //                                                                    && p.DateM >=
                //                                                                    startDayOfCurrentMonthOfDateShToMiladi);
                //        if (await Login(simCard) == false ||
                //            await UpdateAllRegisteredAdvOfSimCard(simCard) == false)
                //        {
                //            firstSimCardBusiness.NextUseDivar = lastUseDivar;
                //            await firstSimCardBusiness.SaveAsync();
                //            continue;
                //        }
                //        var registeredAdvCount = a1.Count;
                //        if (registeredAdvCount >= clsSetting?.DivarSetting?.AdvCountInMonth)
                //        {
                //            //تاریخ روز اول ماه شمسی بعد را تنظیم می کند چون تا سر ماه بعد دیگر نیازی به این سیم کارت نیست
                //            firstSimCardBusiness.NextUseDivar = startDayOfNextMonthOfDateShToMiladi;
                //            await firstSimCardBusiness.SaveAsync();
                //            continue;
                //        }

                //        //آمار آگهی های ثبت شده امروز
                //        var currentDate = DateTime.Now.Date;
                //        var a2 = await AdvertiseLogBusiness.GetAllSpecialAsync(p =>
                //            p.SimCardNumber == card1 && p.AdvType == AdvertiseType.Divar
                //                                     && (p.StatusCode == (short)StatusCode.Published
                //                                         //|| p.StatusCode == (short)StatusCode.Deleted
                //                                         || p.StatusCode == (short)StatusCode.InPublishQueue
                //                                         || p.StatusCode == (short)StatusCode.WaitForPayment)
                //                                     && p.DateM >= currentDate);
                //        registeredAdvCount = a2.Count;
                //        if (registeredAdvCount >= clsSetting?.DivarSetting?.AdvCountInDay)
                //        {
                //            //تاریخ فردا رو ست می کند چون تا فردا دیگه نیازی به این سیم کارت نیست
                //            firstSimCardBusiness.NextUseDivar = DateTime.Today.AddDays(1);
                //            await firstSimCardBusiness.SaveAsync();
                //            continue;
                //        }

                //        //اینجا به تعداد تنظیم شده در تنظیمات دیوار منهای تعداد ثبت شده قبلی، آگهی درج می کند
                //        for (var i = 0; i < clsSetting?.DivarSetting?.AdvCountInDay - registeredAdvCount; i++)
                //        {
                //            while (clsSetting?.DivarSetting.CountAdvInIp <= await
                //                       AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                //                           AdvertiseType.Divar))
                //            {
                //                ShowBalloon("پر شدن تعداد آگهی در " + await FindGateWay(),
                //                    "سیستم در حال تعویض IP یا سایت می باشد");
                //                await SetGateway(await GetRandomGeteWay());
                //                var currentIp1 = await GetLocalIpAddress();
                //                if (await FindGateWay() != IP_Store.IP_Mokhaberat.Value ||
                //                    !(clsSetting?.DivarSetting.CountAdvInIp <= await
                //                          AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                //                              AdvertiseType.Divar))) continue;

                //                var countAdvInIp1 = await
                //                    AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                //                        AdvertiseType.Divar);

                //                if (!(clsSetting?.DivarSetting.CountAdvInIp <= countAdvInIp1)) continue;
                //                if (await GoToNextSite(AdvertiseType.Divar, 0)) continue;

                //                await ChangeIp();

                //                while (await GetLocalIpAddress() == null)
                //                {
                //                    if (counter == 30)
                //                        await SetGateway(await GetRandomGeteWay());
                //                    await Wait(10);
                //                    ShowBalloon("درحال اتصال...", "مودم مخابرات ریست شد. لطفا منتظر بمانید");
                //                    counter++;
                //                }


                //                if (await GoToNextSite(AdvertiseType.Divar, 1)) continue;
                //            }

                //            var adv = await GetNextAdv(simCard);
                //            if (adv == null) continue;
                //            await RegisterAdv(adv);
                //            await Wait(1);
                //            var title = "تعداد آگهی های ارسال شده با " + await FindGateWay();
                //            var body = await
                //                AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                //                    AdvertiseType.Divar);

                //            ShowBalloon(title, body.ToString());

                //        }
                //    }

                //    await Wait(10);
                //    ShowBalloon("لطفا اتصال به شبکه را چک نمایید", "خطای اتصال به شبکه");
                //    continue;
                //}

                //else
                //{
                foreach (var number in numbers)
                {


                    var tt = await AdvTokensBusiness.GetToken(number, AdvertiseType.Divar);
                    var hasToken = tt?.Token ?? null;
                    if (string.IsNullOrEmpty(hasToken)) return;
                    if (!await Login(number) || !await UpdateAllRegisteredAdvOfSimCard(number))
                        continue;

                    for (var i = 0; i < count; i++)
                    {
                        var adv = await GetNextAdv(number);
                        if (adv == null) continue;
                        await RegisterAdv(adv);
                        await Wait(1);
                        var title = await FindGateWay();
                        var co = await AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                            AdvertiseType.Divar);
                        lstMessage.Clear();
                        lstMessage.Add($"نوع آگهی: دیوار");
                        lstMessage.Add($"IP اینترنتی: {await GetLocalIpAddress()}");
                        lstMessage.Add($"GateWay: {await FindGateWay()}");
                        lstMessage.Add($"تعداد آگهی ارسال شده: {co}");
                        
                        ShowBalloon(title, lstMessage);
                    }
                }

                //}
                //}
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
            finally
            {
                monitor.Dispose();
            }

            SemaphoreSlim.Release();

        }

        public Task<bool> Login(long simCardNumber)
        {
            PerfMonitor monitor = new PerfMonitor();
            return Task.Run(async () =>
            {
                try
                {

                    _driver = RefreshDriver(_driver);
                    if (!_driver.Url.Contains("divar.ir"))
                        _driver.Navigate().GoToUrl("https://divar.ir");

                    var simBusiness = await AdvTokensBusiness.GetToken(simCardNumber, AdvertiseType.Divar);
                    var tokenInDatabase = simBusiness?.Token ?? null;
                    //     if (string.IsNullOrEmpty(tokenInDatabase)) return false;

                    var listLinkItems = _driver.FindElements(By.TagName("a"));
                    var isLogined = listLinkItems.Any(linkItem => linkItem.Text == @"خروج");

                    //اگر کاربر لاگین شده فعلی همان کاربر مورد نظر است نیازی به لاگین نیست 
                    if (isLogined && !string.IsNullOrEmpty(tokenInDatabase))
                    {
                        var currentTokenOnDivar = _driver.Manage().Cookies.GetCookieNamed("token").Value;
                        if (!string.IsNullOrEmpty(currentTokenOnDivar) && currentTokenOnDivar == tokenInDatabase)
                            return true;
                    }

                    //اگر کاربرلاگین شده کاربر مد نظر ما نیست از آن کاربری خارج می شود
                    if (isLogined)
                    {
                        //foreach (var linkItem in listLinkItems)
                        //{
                        //    if (linkItem.Text != @"خروج") continue;
                        //    linkItem.Click();
                        //    break;
                        //}
                        _driver.Manage().Cookies.DeleteCookieNamed("_gat");
                        _driver.Manage().Cookies.DeleteCookieNamed("token");
                    }

                    //در صورتیکه توکن قبلا ثبت شده باشد لاگین می کند
                    if (!string.IsNullOrEmpty(tokenInDatabase))
                    {
                        var token = new Cookie("token", tokenInDatabase);
                        _driver.Manage().Cookies.AddCookie(token);
                        _driver.Navigate().Refresh();
                    }
                    //اگر قبلا توکن نداشته وارد صفحه دریافت کد تائید لاگین می شود 
                    else
                    {
                        _driver.Navigate().GoToUrl("https://divar.ir/my-divar/my-posts");
                        //کلیک روی دکمه ورود و ثبت نام
                        await Wait();
                        _driver.FindElement(By.ClassName("login-message__login-btn")).Click();
                        await Wait();
                        var currentWindow = _driver.CurrentWindowHandle;
                        _driver.SwitchTo().Window(currentWindow);
                        if (_driver.FindElements(By.Name("mobile")).Count > 0)
                            _driver.FindElement(By.Name("mobile")).SendKeys("0" + simCardNumber);
                    }

                    //انتظار برای لاگین شدن
                    int repeat = 0;
                    //حدود 120 ثانیه فرصت لاگین دارد
                    while (repeat < 20)
                    {

                        //تا زمانی که لاگین اوکی نشده باشد این حلقه تکرار می شود
                        listLinkItems = _driver.FindElements(By.TagName("a"));
                        if (listLinkItems.Count < 5) return false;

                        var isLogin = listLinkItems.Any(linkItem => linkItem.Text == @"خروج");

                        if (isLogin)
                        {
                            //var all = _driver.Manage().Cookies.AllCookies.ToList();
                            tokenInDatabase = _driver.Manage().Cookies.GetCookieNamed("token").Value;
                            if (simBusiness is null)
                            {
                                simBusiness = new AdvTokensBusiness { Guid = Guid.NewGuid() };
                            }

                            simBusiness.Token = tokenInDatabase;
                            simBusiness.Modified = DateTime.Now;
                            simBusiness.Type = AdvertiseType.Divar;
                            simBusiness.Number = simCardNumber;

                            await simBusiness.SaveAsync();

                            _driver.ExecuteJavaScript(@"alert('لاگین انجام شد');");
                            await Wait();
                            _driver.SwitchTo().Alert().Accept();
                            return true;
                        }
                        else
                        {
                            var name = await SimCardBusiness.GetOwnerNameAsync(simCardNumber);
                            var message = $@"مالک: {name} \r\nشماره: {simCardNumber}  \r\nلطفا لاگین نمائید ";
                            _driver.ExecuteJavaScript($"alert('{message}');");

                            await Wait(3);
                            try
                            {
                                _driver.SwitchTo().Alert().Accept();
                                await Wait(3);
                                repeat++;
                            }
                            catch
                            {
                                await Wait(10);
                            }
                        }
                    }

                    return false;
                }
                catch (WebException) { return false; }
                catch (Exception ex)
                {
                    //if (ex.Source == "WebDriver")
                    //{
                    //    if (ex.Message.Contains("chrome not reachable"))
                    //    {
                    //        MessageBox.Show("بروزر کروم بسته شده یا در دسترس نیست");
                    //        LoadDriver();
                    //    }
                    //    else if (ex.Message.Contains("url timed out after 60 seconds"))
                    //    {
                    //        MessageBox.Show("صفحه مورد نظر در مدت زمان 60 ثانیه بارگزاری نشد");
                    //        LoadDriver();

                    //    }
                    //}
                    if (ex.Source != "WebDriver")
                        WebErrorLog.ErrorLogInstance.StartLog(ex);
                    return false;
                }

                finally
                {
                    monitor.Dispose();
                }

            });
        }
        public Task<bool> LoginChat(long simCardNumber)
        {
            var monitor = new PerfMonitor();
            return Task.Run(async () =>
            {
                try
                {
                    _driver = RefreshDriver(_driver);
                    if (!_driver.Url.Contains("https://chat.divar.ir/"))
                        _driver.Navigate().GoToUrl("https://chat.divar.ir/");

                    var simBusiness = await AdvTokensBusiness.GetToken(simCardNumber, AdvertiseType.DivarChat);
                    var tokenInDatabase = simBusiness?.Token ?? null;
                    //     if (string.IsNullOrEmpty(tokenInDatabase)) return false;

                    var listLinkItems = _driver.FindElements(By.TagName("a"));
                    var isLogined = listLinkItems.Any(linkItem => linkItem.Text == @"خروج");

                    //اگر کاربر لاگین شده فعلی همان کاربر مورد نظر است نیازی به لاگین نیست 
                    if (isLogined && !string.IsNullOrEmpty(tokenInDatabase))
                    {
                        var currentTokenOnDivar = _driver.Manage().Cookies.GetCookieNamed("token").Value;
                        if (!string.IsNullOrEmpty(currentTokenOnDivar) && currentTokenOnDivar == tokenInDatabase)
                            return true;
                    }

                    //اگر کاربرلاگین شده کاربر مد نظر ما نیست از آن کاربری خارج می شود
                    if (isLogined)
                    {
                        _driver.Manage().Cookies.DeleteCookieNamed("_gat");
                        _driver.Manage().Cookies.DeleteCookieNamed("token");
                    }

                    //در صورتیکه توکن قبلا ثبت شده باشد لاگین می کند
                    if (!string.IsNullOrEmpty(tokenInDatabase))
                    {
                        var token = new Cookie("token", tokenInDatabase);
                        _driver.Manage().Cookies.AddCookie(token);
                        _driver.Navigate().Refresh();
                    }
                    //اگر قبلا توکن نداشته وارد صفحه دریافت کد تائید لاگین می شود 
                    else
                    {
                        _driver.Navigate().GoToUrl("https://chat.divar.ir/");
                        //کلیک روی دکمه ورود و ثبت نام
                        await Wait();
                        var currentWindow = _driver.CurrentWindowHandle;
                        _driver.SwitchTo().Window(currentWindow);
                        if (_driver.FindElements(By.TagName("input")).Count > 0)
                            _driver.FindElements(By.TagName("input")).FirstOrDefault()
                                ?.SendKeys("0" + simCardNumber + "\n");
                    }

                    //انتظار برای لاگین شدن
                    var repeat = 0;
                    //حدود 120 ثانیه فرصت لاگین دارد
                    while (repeat < 20)
                    {

                        //تا زمانی که لاگین اوکی نشده باشد این حلقه تکرار می شود
                        listLinkItems = _driver.FindElements(By.TagName("a"));
                        if (listLinkItems.Count < 5) return false;

                        var isLogin = listLinkItems.Any(linkItem => linkItem.Text == @"خروج");

                        if (isLogin)
                        {
                            //var all = _driver.Manage().Cookies.AllCookies.ToList();
                            tokenInDatabase = _driver.Manage().Cookies.GetCookieNamed("token").Value;
                            if (simBusiness is null)
                            {
                                simBusiness = new AdvTokensBusiness { Guid = Guid.NewGuid() };
                            }

                            simBusiness.Token = tokenInDatabase;
                            simBusiness.Modified = DateTime.Now;
                            simBusiness.Type = AdvertiseType.DivarChat;
                            simBusiness.Number = simCardNumber;

                            await simBusiness.SaveAsync();

                            _driver.ExecuteJavaScript(@"alert('لاگین انجام شد');");
                            await Wait();
                            _driver.SwitchTo().Alert().Accept();
                            return true;
                        }
                        else
                        {
                            var name = await SimCardBusiness.GetOwnerNameAsync(simCardNumber);
                            var message = $@"مالک: {name} \r\nشماره: {simCardNumber}  \r\nلطفا لاگین نمائید ";
                            _driver.ExecuteJavaScript($"alert('{message}');");

                            await Wait(3);
                            try
                            {
                                _driver.SwitchTo().Alert().Accept();
                                await Wait(3);
                                repeat++;
                            }
                            catch
                            {
                                await Wait(10);
                            }
                        }
                    }

                    return false;
                }
                catch (WebException) { return false; }
                catch (Exception ex)
                {
                    if (ex.Source != "WebDriver")
                        WebErrorLog.ErrorLogInstance.StartLog(ex);
                    return false;
                }

                finally
                {
                    monitor.Dispose();
                }

            });
        }
        private async Task RegisterAdv(AdvertiseLogBusiness adv)
        {
            var ret = new ReturnedSaveFuncInfo();
            PerfMonitor monitor = new PerfMonitor();
            try
            {
                adv.AdvType = AdvertiseType.Divar;
                _driver = RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://divar.ir/new");

                //کلیک کردن روی کتگوری اصلی
                _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text == clsSetting?.DivarSetting?.Category1)?.Click();
                await Wait(1);
                //کلیک روی ساب کتگوری 1
                _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text.Contains("تجهیزات"))?.Click();
                await Wait(1);
                //کلیک روی ساب کتگوری2
                _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text == clsSetting?.DivarSetting?.Category3)?.Click();

                await Wait(1);
                //درج عکسها
                _driver.FindElement(By.ClassName("image-uploader__dropzone")).FindElement(By.TagName("input")).SendKeys(adv.ImagesPath);
                await Wait();
                if (_driver.FindElements(By.ClassName("location-selector__city")).Count <= 0)
                {
                    ret.AddReturnedValue(ReturnedState.Error, "صفحه ثبت آگهی بارگزاری نشده است");
                    return;
                }

                _driver.FindElement(By.ClassName("location-selector__city")).FindElement(By.TagName("input")).SendKeys(adv.City + "\n");

                await Wait();
                var el = _driver.FindElements(By.ClassName("location-selector__district")).Any();
                await Wait();
                if (el)
                {
                    var cty = await CityBusiness.GetAsync(adv?.City);
                    await Wait(1);
                    var cityGuid = cty.Guid;
                    var lst = await RegionBusiness.GetAllAsync(cityGuid, AdvertiseType.Divar);
                    var regionList = lst?.ToList() ?? new List<RegionBusiness>();
                    if (regionList.Count > 0)
                    {
                        var rnd = new Random().Next(0, regionList.Count);
                        var regName = regionList[rnd].Name;
                        await Wait(2);


                        _driver.FindElement(By.ClassName("location-selector__district")).FindElement(By.TagName("input")).SendKeys(regName + "\n");
                        adv.Region = regName;
                    }
                }




                //درج قیمت
                if (adv.Price > 0) _driver.FindElement(By.CssSelector("input[type=tel]")).SendKeys(adv.Price.ToString());
                await Wait();
                //درج عنوان آگهی
                _driver.FindElements(By.CssSelector("input[type=text]")).Last().SendKeys(adv.Title);
                await Wait();
                //درج محتوای آگهی
                var thread = new Thread(() => Clipboard.SetText(adv.Content.Replace('(', '<').Replace(')', '>')));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

                var t = _driver.FindElement(By.TagName("textarea"));
                t.Click();
                await Wait();
                t.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                var thread1 = new Thread(Clipboard.Clear);
                thread1.SetApartmentState(ApartmentState.STA);
                thread1.Start();
                //_driver.FindElement(By.TagName("textarea")).SendKeys(adv.Content.Replace('(', '<').Replace(')', '>'));
                await Wait();

                await Wait();

                var loadImg = _driver.FindElements(By.ClassName("image-item__progress")).ToList();
                while (loadImg.Count > 0)
                {
                    await Wait(2);
                    loadImg = _driver.FindElements(By.ClassName("image-item__progress")).ToList();
                }

                if (_driver.FindElements(By.ClassName("location-selector__district")).Count > 0 &&
                    (string.IsNullOrEmpty(adv.Region) || adv.Region == "-"))
                    _driver.FindElement(By.ClassName("location-selector__district")).FindElement(By.TagName("input"))
                        .SendKeys("\n");

                var but = _driver.FindElements(By.ClassName("submit-post__form__buttons__submit")).Any();
                if (but)
                    //کلیک روی دکمه ثبت آگهی
                    _driver.FindElement(By.ClassName("submit-post__form__buttons__submit")).Click();

                //اگر آگهی با موفقیت ثبت شود لینک مدیریت آگهی ذخیره می شود
                await Wait(1);
                //if (_driver.Url.Contains("manage"))
                //{
                adv.URL = _driver.Url;
                adv.UpdateDesc = @"در صف انتشار";
                adv.AdvStatus = @"در صف انتشار";
                adv.StatusCode = (short)StatusCode.InPublishQueue;
                adv.IP = await GetLocalIpAddress();
                await adv.SaveAsync();
                var sim = await SimCardBusiness.GetAsync(adv.SimCardNumber);
                sim.DivarModified = DateTime.Now;
                await sim.SaveAsync();
                //}
                //اگر درج آگهی با خطا مواجه شود متن خطا ذخیره می شود
                //else
                //{
                //    adv.UpdateDesc = @"خطای درج";
                //    adv.AdvStatus = "خطای درج";
                //    adv.StatusCode = (short)StatusCode.InsertError;
                //    adv.URL = "---";
                //    adv.ErrorImage += await GetScreenShot();
                //    adv.IP = await GetLocalIpAddress();
                //    ret.AddReturnedValue(await adv.SaveAsync());
                //}
                //بعد از درج آگهی در دیتابیس لاگ می شود

            }
            catch (Exception ex)
            {
                // MessageBox.Show(@"در هنگام ثبت آگهی زیر خطا رخ داد\r\n" + adv.Adv + @"\r\n" + ex.Message);
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                ret.AddReturnedValue(ex);
            }
            finally { monitor.Dispose(); }
        }
        private async Task<AdvertiseLogBusiness> GetNextAdv(long simCardNumber)
        {
            var newAdvertiseLogBusiness = new AdvertiseLogBusiness();
            var monitor = new PerfMonitor();
            try
            {
                newAdvertiseLogBusiness.SimCardNumber = simCardNumber;

                #region find visitor and text replacements
                //پیدا کردن ویزیتور ها و متن جایگزین محتوا برای سیم کارت مورد نظر
                var replacements = await VisitorBusiness.GetMasterSlaveAdvReplacementsAsync(newAdvertiseLogBusiness.SimCardNumber);
                if (replacements?.MasterGuid == null) return null;
                if (replacements == null || replacements.MasterGuid == Guid.Empty)
                    throw new Exception($"Replacment error:{replacements?.ToString() ?? "is null"} simeCardNumber:{simCardNumber }");

                newAdvertiseLogBusiness.MasterVisitorGuid = (Guid)replacements.MasterGuid;
                if (replacements.SlaveGuid != null)
                    newAdvertiseLogBusiness.SlaveVisitorGuid = (Guid)replacements.SlaveGuid;
                #endregion

                #region find visitorAdvs
                //لیست آگهی های مرتبط با ویزیتور دریافت می شود
                AdvertiseList = await Advertise.GetAllAsync(AdvRootPath, newAdvertiseLogBusiness.MasterVisitorGuid);
                AdvertiseList = AdvertiseList.Where(q => q.PishNevis == false).ToList();
                if (!(AdvertiseList?.Count > 0)) return null;

                #endregion

                #region findNextAdvIndex


                var nextAdvIndex = new Random().Next(AdvertiseList.Count);
                #endregion

                string path = null;
                if (Path.Combine(clsSetting?.DivarSetting?.PicPath, AdvertiseList[nextAdvIndex].AdvName) ==
                    AdvertiseList[nextAdvIndex].AdvName)
                    path = Path.Combine(AdvertiseList[nextAdvIndex].RootPath,
                        AdvertiseList[nextAdvIndex].AdvName);
                else
                    path = Path.Combine(clsSetting?.DivarSetting?.PicPath, AdvertiseList[nextAdvIndex].AdvName);
                newAdvertiseLogBusiness.Adv = path;

                #region FindNextTitle
                //تایتل آگهی دریافت می شود
                if (!(AdvertiseList[nextAdvIndex].Titles?.Count > 0)) return null;

                var nextTitleIndex = new Random(DateTime.Now.Millisecond).Next(AdvertiseList[nextAdvIndex].Titles.Count);
                newAdvertiseLogBusiness.Title = AdvertiseList[nextAdvIndex].Titles[nextTitleIndex];


                if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Content)) return null;
                #endregion

                #region GetContent
                //کانتنت آگهی دریافت می شود

                newAdvertiseLogBusiness.Content = AdvertiseList[nextAdvIndex].Content
                    .Replace("<master>", replacements.AdvReplacement1)
                    .Replace("<slave>", replacements.AdvReplacement2)
                    .Replace("<tell>", replacements.Phone)
                    .Replace("(", "<")
                    .Replace(")", ">");

                if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Content)) return null;

                #endregion

                #region FindImages
                //عکسهای آگهی دریافت می شود
                newAdvertiseLogBusiness.ImagesPathList = GetNextImages(newAdvertiseLogBusiness.Adv);
                #endregion

                //قیمت آگهی دریافت می شود
                newAdvertiseLogBusiness.Price = AdvertiseList[nextAdvIndex].Price;
                var city = await CityBusiness.GetNextRandomCityAsync(newAdvertiseLogBusiness.MasterVisitorGuid,
                    AdvertiseType.Divar);
                newAdvertiseLogBusiness.City = city?.CityName;
                newAdvertiseLogBusiness.State = city?.State?.StateName;


                return newAdvertiseLogBusiness;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return null;
            }

            finally { monitor.Dispose(); }
        }
        private List<string> GetNextImages(string advFullPath, int imgCount = 3)
        {
            var resultImages = new List<string>();
            PerfMonitor monitor = new PerfMonitor();
            try
            {
                if (string.IsNullOrEmpty(advFullPath)) return resultImages;
                //گرفتن تمام عکسهای پوشه و فیلتر کردن عکسهای درست
                var picturesPath = Path.Combine(advFullPath, "Pictures");
                var allImages = Utility.GetFiles(picturesPath, "*.jpg");
                var selectedImages = new List<string>();
                //حذف عکسهای زیر پیکسل 600*600
                foreach (var imgItem in allImages)
                {
                    var img = Image.FromFile(imgItem);
                    if (img.Width < 600 || img.Height < 600)
                        try
                        {
                            img.Dispose();
                            File.Delete(imgItem);
                        }
                        catch
                        {
                            /**/
                        }
                    img.Dispose();
                }
                allImages = Utility.GetFiles(picturesPath, "*.jpg");

                if (allImages.Count <= imgCount) selectedImages = allImages;
                else
                {
                    var indexes = new List<int>();
                    var rnd = new Random();
                    while (indexes.Count < imgCount)
                    {
                        var index = rnd.Next(allImages.Count);
                        if (!indexes.Contains(index))
                            indexes.Add(index);
                    }

                    selectedImages.AddRange(indexes.Select(index => allImages[index]));
                }


                //ویرایش عکسها
                foreach (var img in selectedImages)
                {
                    resultImages.Add(ImageManager.ModifyImage(img));
                }

                return resultImages;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                // MessageBox.Show(@"GetNextImages:" + ex.Message);
                return resultImages;
            }
            finally { monitor.Dispose(); }
        }




        public async Task<List<RegionBusiness>> GetAllRegionFromDivar(List<string> City)
        {
            var region = new List<RegionBusiness>();
            _driver = RefreshDriver(_driver);
            _driver.Navigate().GoToUrl("https://divar.ir/new");
            //کلیک کردن روی کتگوری اصلی
            _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text == clsSetting?.DivarSetting?.Category1)?.Click();
            await Wait();
            //کلیک روی ساب کتگوری 1
            _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text.Contains("تجهیزات"))?.Click();
            await Wait();
            //کلیک روی ساب کتگوری2
            _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text == clsSetting?.DivarSetting?.Category3)?.Click();

            await Wait();
            try
            {
                foreach (var item in City)
                {
                    _driver.FindElement(By.ClassName("location-selector__city")).FindElement(By.TagName("input"))
                        .SendKeys(item + "\n");
                    await Wait();
                    var el = _driver.FindElement(By.ClassName("location-selector__district"))
                        .FindElement(By.TagName("i"));
                    await Wait();
                    el?.Click();
                    var allEl = _driver.FindElement(By.ClassName("location-selector__district"))
                        .FindElements(By.ClassName("item")).Where(q => q.Text != "").ToList();
                    if (allEl.Count <= 0) continue;
                    foreach (var temp in allEl)
                    {
                        var a = await CityBusiness.GetAsync(item);
                        var clsRegionBusiness = new RegionBusiness
                        {
                            Guid = Guid.NewGuid(),
                            CityGuid = a.Guid,
                            Modified = DateTime.Now,
                            Type = AdvertiseType.Divar,
                            Name = temp.Text
                        };
                        region.Add(clsRegionBusiness);
                    }

                }
            }
            catch (Exception e)
            {
                region = null;
                WebErrorLog.ErrorLogInstance.StartLog(e);
            }

            return region;
        }



        public async Task CheckAllTokens()
        {
            if (SemaphoreSlim.CurrentCount == 0)
            {
                var result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    TokenSource?.Cancel();
                else return;
            }

            await SemaphoreSlim.WaitAsync();
            TokenSource = new CancellationTokenSource();

            PerfMonitor monitor = new PerfMonitor();
            try
            {
                _driver = RefreshDriver(_driver);
                var tokenList = await SimCardBusiness.GetAllAsync();
                var numberHasTokenList = tokenList.Where(p => !string.IsNullOrEmpty(p.DivarToken)).Select(p => p.Number)
                    .ToList();
                var simCartBusinessesList = await SimCardBusiness.GetAllAsync();
                var numbersList = simCartBusinessesList.Where(p => p.Status)
                    .Where(p => numberHasTokenList.IndexOf(p.Number) < 0).OrderBy(p => p.OwnerGuid)
                    .ThenBy(p => p.Number).Select(p => p.Number).ToList();

                File.WriteAllLines(Path.Combine(AdvRootPath, "simCardList.txt"),
                    numbersList.Select(number => number.ToString()).ToArray());

                foreach (var number in numbersList)
                {
                    if (TokenSource.IsCancellationRequested) return;
                    if (!await Login(number)) return;
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
            finally
            {
                monitor.Dispose();
                SemaphoreSlim.Release();
            }

        }
        public async Task<bool> UpdateAllAdvStatus(int dayCount = 0)
        {
            if (SemaphoreSlim.CurrentCount == 0)
            {
                DialogResult result;
                result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    TokenSource?.Cancel();
                else return false;
            }

            await SemaphoreSlim.WaitAsync();
            TokenSource = new CancellationTokenSource();
            while (true)
            {
                try
                {
                    _driver = RefreshDriver(_driver);
                    if (dayCount == 0)
                        dayCount = clsSetting.DivarSetting?.DayCountForUpdateState ?? 10;
                    var lastWeek = DateTime.Now.AddDays(-dayCount);
                    var lst = await AdvertiseLogBusiness
                        .GetAllSpecialAsync(p => p.DateM > lastWeek && p.URL.Contains("manage"));
                    var allAdvertiseLog = lst.OrderByDescending(p => p.DateSh).ThenBy(p => p.priorityOfUpdateSt)
                        .ThenBy(p => p.rnd).ToList();
                    if (allAdvertiseLog.Count <= 0) return true;
                    var tryCount = 0;
                    foreach (var adv in allAdvertiseLog)
                    {
                        if (TokenSource.IsCancellationRequested) break;
                        if (tryCount >= 3) continue;
                        try
                        {
                            _driver.Navigate().GoToUrl(adv.URL);
                            await Wait();
                            var element = _driver.FindElement(By.ClassName("manage-header__status"));
                            if (element == null) continue;
                            adv.AdvStatus = element.Text;
                            element = _driver.FindElement(By.ClassName("manage-header__description"));
                            if (element == null) continue;
                            adv.UpdateDesc = element.Text;
                            adv.StatusCode = (short)GetAdvStatusCodeByStatus(adv.AdvStatus);



                            //بروزرسانی آمار بازدید منتشر شده ها
                            if (adv.StatusCode == (short)StatusCode.Published)
                            {
                                var visitCountEl = _driver.FindElement(By.ClassName("post-stats__summary"));
                                if (visitCountEl != null && visitCountEl.Text.Length > 11)
                                {
                                    int.TryParse(visitCountEl.Text.Substring(11).Trim().FixString(), out var cnt);
                                    adv.VisitCount = cnt;
                                }
                            }

                            await adv.SaveAsync();
                            tryCount = 0;
                        }
                        catch (Exception ex)
                        {
                            if (ex.Source != "WebDriver" && !TokenSource.IsCancellationRequested)
                                WebErrorLog.ErrorLogInstance.StartLog(ex);
                            await Wait();
                            tryCount++;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    if (ex.Source != "WebDriver")
                        WebErrorLog.ErrorLogInstance.StartLog(ex);
                    return false;
                }
                finally
                {
                    SemaphoreSlim.Release();
                }
            }

        }
        public async Task<bool> UpdateAdvStatus(AdvertiseLogBusiness adv)
        {
            try
            {
                _driver = RefreshDriver(_driver);
                if (!adv.URL.Contains("manage")) return false;
                if (_driver.Url != adv.URL)
                    _driver.Navigate().GoToUrl(adv.URL);
                await Wait();
                adv.AdvType = AdvertiseType.Divar;
                adv.AdvStatus = _driver.FindElement(By.ClassName("manage-header__status")).Text;
                adv.UpdateDesc = _driver.FindElement(By.ClassName("manage-header__description")).Text;
                adv.StatusCode = (short)GetAdvStatusCodeByStatus(adv.AdvStatus);
                await adv.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public StatusCode GetAdvStatusCodeByUrl(string url)
        {
            try
            {
                if (!url.Contains("manage")) return StatusCode.Unknown;

                _driver.Navigate().GoToUrl(url);

                if (_driver.FindElements(By.ClassName("manage-header__status")).Count <= 0) return StatusCode.Unknown;

                var status = _driver.FindElement(By.ClassName("manage-header__status")).Text;
                return GetAdvStatusCodeByStatus(status);
            }
            catch
            {
                return StatusCode.Unknown;
            }

        }

        private StatusCode GetAdvStatusCodeByStatus(string advStatus)
        {
            switch (advStatus)
            {
                case "در صف انتشار": return StatusCode.InPublishQueue;
                case "منتشر شده": return StatusCode.Published;
                case "نیاز به اصلاح": return StatusCode.EditNeeded;
                case "منتظر پرداخت": return StatusCode.WaitForPayment;
                case "رد شده": return StatusCode.Failed;
                case "حذف شده": return StatusCode.Deleted;
                case "منقضی شده": return StatusCode.Expired;
                case "خطای درج": return StatusCode.InsertError;
                default: return StatusCode.Unknown;
            }
        }

        //public enum StatusCode
        //{
        //    InPublishQueue = 1, //  در صف انتشار,
        //    Published = 2,//  منتشر شده,
        //    EditNeeded = 3,// "نیاز به اصلاح"
        //    WaitForPayment = 4,//  "منتظر پرداخت"
        //    Failed = 5,//  "رد شده"
        //    Deleted = 6,//  "حذف شده"
        //    Expired = 7,//  "منقضی شده"
        //    InsertError = 10,//  "خطای درج"
        //    Unknown = 0//  "نامشخص"
        //}

        public async Task UpdateAdvVisitCount()
        {
            if (SemaphoreSlim.CurrentCount == 0)
            {
                DialogResult result;
                result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    TokenSource?.Cancel();
                else return;
            }

            await SemaphoreSlim.WaitAsync();
            TokenSource = new CancellationTokenSource();

            try
            {
                var allAdvertiseLog = await
                    AdvertiseLogBusiness.GetAllSpecialAsync(p => p.StatusCode == (short)StatusCode.Published);
                if (allAdvertiseLog.Count > 0)
                {
                    _driver = RefreshDriver(_driver);
                    foreach (var adv in allAdvertiseLog)
                    {
                        if (TokenSource.IsCancellationRequested) break;
                        if (adv.URL.Contains("manage"))
                        {
                            _driver.Navigate().GoToUrl(adv.URL);
                            await Wait();
                            //post-stats__summary = بازدید کلی: ۱۸
                            //post-stats__bar
                            var visitElement = _driver.FindElement(By.ClassName("post-stats__summary"));
                            if (visitElement.Text.Length <= 11) continue;
                            int.TryParse(visitElement.Text.Substring(11).Trim().FixString(), out var cnt);
                            adv.VisitCount = cnt;
                            await adv.SaveAsync();
                            await UpdateAdvStatus(adv);
                            await InsertDataInAdvVisitLog(adv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }
        public async Task<bool> InsertDataInAdvVisitLog(AdvertiseLogBusiness adv)
        {
            try
            {
                var cls = new AdvVisitLogBusiness
                {
                    Guid = Guid.NewGuid(),
                    Modified = DateTime.Now,
                    AdvGuid = adv.Guid,
                    DateM = adv.DateM,
                    VisitCount = adv.VisitCount,
                    AdvStatus = adv.AdvStatus,
                    StatusCode = adv.StatusCode,
                    Type = AdvertiseType.Divar
                };
                var res = await cls.SaveAsync();
                return !res.HasError;
            }
            catch
            {
                return false;
            }
        }
        public async Task<string> GetScreenShot()
        {
            try
            {
                var rootPath = Path.Combine(Application.StartupPath, "ScreenShots");
                var savePath = Path.Combine(rootPath, Guid.NewGuid() + ".jpg");

                if (!Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);
                await Wait(3);
                _driver = RefreshDriver(_driver);
                ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile(savePath, ScreenshotImageFormat.Jpeg);

                return savePath;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
            return "";
        }
        public List<DivarCityBusiness> GetAllCityFromDivar()
        {
            var cities = new List<DivarCityBusiness>();
            _driver = RefreshDriver(_driver);
            if (_driver.Url != "https://divar.ir/")
                _driver.Navigate().GoToUrl("https://divar.ir/");
            try
            {
                var cityElements = _driver.FindElements(By.TagName("h2"));
                foreach (var element in cityElements)
                {
                    var a = new DivarCityBusiness
                    {
                        Guid = Guid.NewGuid(),
                        Modified = DateTime.Now,
                        CityName = element.Text
                    };
                    cities.Add(a);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return cities;
        }
        /// <summary>
        /// آگهی از دیوار حذف می شود و در دیتابیس نیز بروزرسانی می شود
        /// </summary>
        /// <param name="adv"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAdvFromDivar(AdvertiseLogBusiness adv)
        {
            try
            {
                if (!adv.URL.Contains("manage")) return false;
                // if (!Login(adv.SimCardNumber)) return false;
                if (!await DeleteAdvFromDivar(adv.URL)) return false;
                return await UpdateAdvStatus(adv);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// فقط از دیوار حذف می شود بروزرسانی دیتابیس انجام نمی شود
        /// </summary>
        /// <param name="url"></param>
        public async Task<bool> DeleteAdvFromDivar(string url)
        {
            try
            {
                if (!url.Contains("manage")) return false;
                _driver = RefreshDriver(_driver);
                if (_driver.Url != url) _driver.Navigate().GoToUrl(url);
                await Wait();
                //کلیک روی دکمه حذف
                _driver.FindElement(By.ClassName("trash")).Click();
                await Wait();
                _driver.SwitchTo().ActiveElement();
                //انتخاب رادیو باتن دومی-از فروش منصرف شدم
                var options = _driver.FindElements(By.ClassName("manage-delete__option"));
                if (options.Count <= 2) return false;
                options[1].Click();
                await Wait();
                //کلیک روی دکمه تائید 
                _driver.FindElement(By.ClassName("manage-delete__actions")).FindElement(By.ClassName("button"))?.Click();
                await Wait();
                if (_driver.Url != url) _driver.Navigate().GoToUrl(url);
                _driver.Navigate().Refresh();
                await Wait();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// حذف تمام آگهی های دیوار با وضعیت مورد نظر از زمان تعیین شده به قبل آن و در دیتابیس نیز بروزرسانی می شود
        /// </summary>
        /// <param name="fromNDayBefore">تعیین روزی که آگهی های ماقبل آن باید حذف شوند</param>
        /// <param name="statusCode">وضعیت آگهی مورد نظر جهت حذف </param>
        public async Task DeleteAllAdvFromDivar(int fromNDayBefore, StatusCode statusCode)
        {
            if (SemaphoreSlim.CurrentCount == 0)
            {
                DialogResult result;
                result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    TokenSource?.Cancel();
                else return;
            }

            await SemaphoreSlim.WaitAsync();
            TokenSource = new CancellationTokenSource();

            PerfMonitor monitor = new PerfMonitor();
            try
            {
                var date = DateTime.Now.AddDays(-1 * fromNDayBefore);
                var advList = await
                    AdvertiseLogBusiness.GetAllSpecialAsync(p => p.DateM < date && p.StatusCode == (short)statusCode);
                foreach (var adv in advList)
                {
                    if (TokenSource.IsCancellationRequested) break;
                    await DeleteAdvFromDivar(adv);
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
            finally
            {
                monitor.Dispose();
                SemaphoreSlim.Release();
            }
        }

        public async Task<bool> UpdateAllRegisteredAdvOfSimCard(long simCardNumber)
        {
            PerfMonitor monitor = new PerfMonitor();
            try
            {
                _driver = RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://divar.ir/my-divar/my-posts");
                var allPost = _driver.FindElements(By.ClassName("my-post"));
                var manageLinks = new List<string>();
                foreach (var post in allPost)
                {
                    var url = post.GetAttribute("href");

                    if (url.Contains("manage")) manageLinks.Add(url);
                }

                foreach (var url in manageLinks)
                {
                    var getUrl = await AdvertiseLogBusiness.GetAsync(url);
                    if (!string.IsNullOrEmpty(getUrl?.URL)) continue;
                    _driver.Navigate().GoToUrl(url);

                    var ls = await AdvertiseLogBusiness.GetAllSpecialAsync(p => p.URL == url);
                    var newAdv = ls.FirstOrDefault();

                    if (newAdv is null) //اگر آگهی قبلا در دیتابیس ثبت نشده باشد
                    {
                        newAdv = new AdvertiseLogBusiness
                        {
                            Adv = "استخراج سیستمی",
                            URL = url,
                            SimCardNumber = simCardNumber,
                            Category = "-",
                            SubCategory1 = "-",
                            SubCategory2 = "-"
                        };

                        //dateM
                        await Wait();
                        var publishDateEl = _driver.FindElement(By.ClassName("post-header__publish-time"));
                        if (publishDateEl != null) newAdv.DateM = GetDateMFromPublishTime(publishDateEl.Text);
                        newAdv.DateSh = Calendar.MiladiToShamsi(newAdv.DateM);
                        //price and city
                        var postFieldElements = _driver.FindElements(By.ClassName("post-fields-item"));
                        foreach (var fieldElement in postFieldElements)
                        {
                            var title = fieldElement.FindElement(By.ClassName("post-fields-item__title")).Text;
                            var value = fieldElement.FindElement(By.ClassName("post-fields-item__value")).Text;

                            switch (title)
                            {
                                case "قیمت":
                                    newAdv.Price = value.FixString().Replace("تومان", "").Replace("٫", "").Trim().ParseToDecimal();
                                    break;
                                case "محل":
                                    newAdv.City = value;
                                    break;
                            }
                        }
                    }
                    newAdv.Modified = DateTime.Now;

                    //status
                    await Wait();
                    newAdv.AdvStatus = _driver.FindElement(By.ClassName("manage-header__status")).Text;
                    newAdv.StatusCode = (short)GetAdvStatusCodeByStatus(newAdv.AdvStatus);

                    //updateDesc - desc
                    var statusDescEl = _driver.FindElement(By.ClassName("manage-header__description"));
                    if (statusDescEl != null) newAdv.UpdateDesc = statusDescEl.Text;

                    //title
                    newAdv.Title = _driver.FindElement(By.ClassName("post-header__title")).Text;

                    //content
                    newAdv.Content = _driver.FindElement(By.ClassName("post-page__description")).Text;

                    //imagesPath
                    if (newAdv.ImagesPathList is null) newAdv.ImagesPathList = new List<string>();
                    var imgElements = _driver.FindElements(By.TagName("img"));
                    foreach (var img in imgElements)
                    {
                        var src = img.GetAttribute("src");
                        if (src.Contains("manage_pictures") && newAdv.ImagesPathList.IndexOf(src) < 0)
                            newAdv.ImagesPathList.Add(img.GetAttribute("src"));
                    }

                    //visit Count
                    var visitCountEl = _driver.FindElement(By.ClassName("post-stats__summary"));
                    if (visitCountEl != null && visitCountEl.Text.Length > 11)
                    {
                        int.TryParse(visitCountEl.Text.Substring(11).Trim().FixString(), out var cnt);
                        newAdv.VisitCount = cnt;
                    }
                    newAdv.SaveAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return false;
            }
            finally { monitor.Dispose(); }
        }
        private DateTime GetDateMFromPublishTime(string publishStr)
        {
            var resultDate = DateTime.Now;
            switch (publishStr)
            {
                case "دیروز":
                    resultDate = resultDate.AddDays(-1);
                    break;
                case "پریروز":
                    resultDate = resultDate.AddDays(-2);
                    break;
                case "۳ روز پیش":
                    resultDate = resultDate.AddDays(-3);
                    break;
                case "۴ روز پیش":
                    resultDate = resultDate.AddDays(-4);
                    break;
                case "۵ روز پیش":
                    resultDate = resultDate.AddDays(-5);
                    break;
                case "۶ روز پیش":
                    resultDate = resultDate.AddDays(-6);
                    break;
                case "هفتهٔ پیش":
                    resultDate = resultDate.AddDays(-7);
                    break;
                case "۱ هفته پیش":
                    resultDate = resultDate.AddDays(-7);
                    break;
                case "۲ هفته پیش":
                    resultDate = resultDate.AddDays(-14);
                    break;
                case "۳ هفته پیش":
                    resultDate = resultDate.AddDays(-21);
                    break;
                case "۴ هفته پیش":
                    resultDate = resultDate.AddDays(-28);
                    break;
                case "۵ هفته پیش":
                    resultDate = resultDate.AddDays(-35);
                    break;
                case "۶ هفته پیش":
                    resultDate = resultDate.AddDays(-42);
                    break;
                case "۷ هفته پیش":
                    resultDate = resultDate.AddDays(-49);
                    break;
            }

            return resultDate;
        }
        public async void UpdateAllRegisteredAdvInDivar()
        {
            var simCardList = await SimCardBusiness.GetAllAsync();
            try
            {
                foreach (var simCardBusiness in simCardList)
                {
                    if (await Login(simCardBusiness.Number))
                        UpdateAllRegisteredAdvOfSimCard(simCardBusiness.Number);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task ViewAdv(long simCard, string url)
        {
            if (SemaphoreSlim.CurrentCount == 0)
            {
                DialogResult result;
                result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    TokenSource?.Cancel();
                else return;
            }

            await SemaphoreSlim.WaitAsync();
            TokenSource = new CancellationTokenSource();

            PerfMonitor monitor = new PerfMonitor();
            try
            {
                if (TokenSource.IsCancellationRequested) return;
                if (await Login(simCard))
                    _driver.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
            finally
            {
                monitor.Dispose();
                SemaphoreSlim.Release();
            }
        }

        public async Task<bool> GetNumbersFromDivar(int Counter)
        {
            var list = new List<string>();
            var path = Path.Combine(Application.StartupPath, "divarNumbers.txt");
            try
            {
                for (var i = 0; i < Counter; i++)
                {
                    var f = await CityBusiness.GetAllAsync();
                    var allcity = f.Where(q => q.isDivarCity).ToList();
                    var rnd = new Random().Next(0, allcity.Count);
                    var city = allcity[rnd].CityName;
                    var number = await SimCardBusiness.GetNextSimCardNumberAsync((short) AdvertiseType.Divar,
                        (short) clsSetting.DivarSetting.AdvCountInDay, (short) clsSetting.SheypoorSetting.AdvCountInDay,
                        await GetLocalIpAddress(), (short) clsSetting.DivarSetting.AdvCountInMonth,
                        (short) clsSetting.SheypoorSetting.AdvCountInMonth,
                        (short) clsSetting.DivarSetting.CountAdvInIp, (short) clsSetting.SheypoorSetting.CountAdvInIp,
                        (short) clsSetting.NiazmandyHaSetting.AdvCountInDay,
                        (short) clsSetting.NiazmandyHaSetting.AdvCountInMonth,
                        (short) clsSetting.NiazmandyHaSetting.CountAdvInIp);
                    var tt = await AdvTokensBusiness.GetToken(number, AdvertiseType.Divar);
                    var hasToken = tt?.Token ?? null;
                    if (string.IsNullOrEmpty(hasToken))
                    { File.WriteAllLines(path, list); return false; }
                    var log = await Login(number);
                    if (!log)
                    { File.WriteAllLines(path, list); return false; }
                    _driver.Navigate().GoToUrl("https://divar.ir/");
                    await Wait();
                    _driver.FindElement(By.ClassName("city-selector")).Click();
                    await Wait();
                    _driver.FindElements(By.TagName("a")).LastOrDefault(q => q.Text == city)?.Click();
                    await Wait(2);
                    for (var j = 0; j < 2; j++)
                    {
                        ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                        await Wait();
                    }
                    var counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
                    await Wait();
                    for (var h = 1; h < counter.Count; h++)
                    {
                        await Wait();
                        var u = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
                        if (u.Count == 0)
                            _driver.Navigate().Back();
                        _driver.FindElements(By.ClassName("col-xs-12"))[h]?.Click();
                        await Wait(1);
                        _driver.FindElement(By.ClassName("post-actions__get-contact")).Click();
                        await Wait();

                        var a = _driver.FindElements(By.ClassName("primary"))
                            .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم");
                        if (a != null)
                            _driver.FindElements(By.ClassName("primary"))
                                .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم")?.Click();
                        await Wait();
                        var txt = _driver.FindElements(By.ClassName("post-fields-item__value")).FirstOrDefault()?.Text;
                        if (txt == "(پنهان‌شده؛ چت کنید)") continue;
                        list.Add(txt.FixString());
                        await Wait();
                        _driver.Navigate().Back();
                    }

                    File.WriteAllLines(path, list);
                }
                var t = list.GroupBy(q => q).Where(q => q.Count() == 1).Select(q => q.Key).ToList();
                File.WriteAllLines(path, t);
                return true;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                File.WriteAllLines(path, list);
                return false;
            }
        }
        public async Task GetChatCount(List<SimCardBusiness> simcard)
        {
            try
            {
                foreach (var sim in simcard)
                {
                    var log = await LoginChat(sim.Number);
                    if (!log) continue;
                    await Wait();
                    var e = _driver.FindElements(By.TagName("input")).ToList();
                    if (e.Count == 1)
                    {
                        var w = await OwnerBusiness.GetAsync(sim.OwnerGuid);
                        var ownerName = w.FullName;
                        _driver.FindElements(By.TagName("input")).FirstOrDefault()?.SendKeys(ownerName + "\n");
                    }
                    var q = _driver.FindElements(By.ClassName("dimmable")).ToList();
                    if (q.Count <= 1) continue;
                    await Wait();
                    var tr = _driver.FindElements(By.ClassName("conversation-item__conversation-status")).ToList();
                    if (tr.Count == 0) continue;
                    await Wait();
                    while (_driver.Url.Contains("https://chat.divar.ir"))
                    {
                        await Wait();
                    }
                    _driver.Navigate().GoToUrl("https://divar.ir/my-divar/my-posts");
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
        }



        #endregion

    }
}
