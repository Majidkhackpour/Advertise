using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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
using PacketParser;
using PacketParser.BusinessLayer;
using PacketParser.Entities;
using static AdvertiseApp.Classes.Utility;
using Cookie = OpenQA.Selenium.Cookie;

namespace AdvertiseApp.Classes
{
    public class SheypoorAdv
    {
        #region Fields

        private IWebDriver _driver;
        private bool _noErrorInRegister = true;
        private string _advRootPath;
        private int _maxImgForAdv;
        private static SettingBusiness cls;
        #endregion

        #region Ctors

        /// <summary>
        /// اگر مسیر آگهی داده نشود از مسیر پیش فرض فایل کانفیگ استفاده خواهد شد
        /// </summary>
        /// <param name="advRootPath"></param>
        /// <param name="maxImgForAnyAdv"></param>
        public SheypoorAdv(string advRootPath = "", int maxImgForAnyAdv = 3)
        {
            _advRootPath = advRootPath;
            if (string.IsNullOrEmpty(_advRootPath)) _advRootPath = ConfigurationManager.AppSettings.Get("RootPath");
            _maxImgForAdv = maxImgForAnyAdv;

            _driver = RefreshDriver(_driver);

        }

        #endregion
        public SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1);
        private static string AdvRootPath => ConfigurationManager.AppSettings.Get("RootPath");
        private CancellationTokenSource _tokenSource;
        public async Task StartRegisterAdv(List<long> numbers = null, int count = 0)
        {
            //if (SemaphoreSlim.CurrentCount == 0)
            //{
            //    DialogResult result;
            //    result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Question);
            //    if (result == DialogResult.Yes)
            //        _tokenSource?.Cancel();
            //    else return;
            //}
            _tokenSource?.Cancel();
            //await SemaphoreSlim.WaitAsync();
            _tokenSource = new CancellationTokenSource();
            var counter = 0;
            var monitor = new PerfMonitor();
            var isLogin = false;

            try
            {
                while (await PingHost("185.105.239.1") == false)
                {
                    if (counter >= 30)
                        await SetGateway(await GetRandomGeteWay());
                    await Wait(10);
                    lstMessage.Clear();
                    lstMessage.Add("خطای اتصال به شبکه");
                    ShowBalloon("لطفا اتصال به شبکه را چک نمایید", lstMessage);
                    counter++;
                }

                //while (!_tokenSource.IsCancellationRequested)
                //{
                //    counter = 0;
                //    SimCardBusiness firstSimCardBusiness = null;
                //اگر نامبر صفر نباشد یعنی کاربر خواسته روی شماره ای خاص آگهی بزند
                //اگر صفر باشد روی تمام سیم کارتها داخل حلقه وایل، آگهی ثبت می شود
                //if (numbers == null || numbers.Count == 0)
                //{
                //    if (!(cls?.SheypoorSetting?.AdvCountInDay > 0)) return;

                //    _driver = RefreshDriver(_driver);
                //    //     MessageBox.Show(_driver.WindowHandles.Count.ToString());
                //    while (await PingHost("185.105.239.1"))
                //    {
                //        var simCard = await SimCardBusiness.GetNextSimCardNumberAsync(
                //            (short)AdvertiseType.Sheypoor, (short)cls.SheypoorSetting.AdvCountInDay,
                //            await GetLocalIpAddress());
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
                //            if (await GoToNextSite(AdvertiseType.Sheypoor, 0)) continue;
                //            await ChangeIp();
                //            while (await GetLocalIpAddress() == null)
                //            {
                //                if (counter == 30)
                //                    await SetGateway(await GetRandomGeteWay());
                //                await Wait(10);
                //                ShowBalloon("درحال اتصال...", "مودم مخابرات ریست شد. لطفا منتظر بمانید");
                //                counter++;
                //            }

                //            if (await GoToNextSite(AdvertiseType.Sheypoor, 1)) continue;
                //        }


                //        if (cls?.SheypoorSetting.CountAdvInIp <= await
                //                   AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                //                       AdvertiseType.Sheypoor))
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
                //                !(cls?.SheypoorSetting.CountAdvInIp <= await
                //                      AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                //                          AdvertiseType.Sheypoor))) continue;
                //            if (await GoToNextSite(AdvertiseType.Sheypoor, 0)) continue;
                //            await ChangeIp();
                //            while (await GetLocalIpAddress() == null)
                //            {
                //                if (counter == 30)
                //                    await SetGateway(await GetRandomGeteWay());
                //                await Wait(10);
                //                ShowBalloon("درحال اتصال...", "مودم مخابرات ریست شد. لطفا منتظر بمانید");
                //                counter++;
                //            }

                //            if (await GoToNextSite(AdvertiseType.Sheypoor, 1)) continue;
                //        }



                //        firstSimCardBusiness = await SimCardBusiness.GetAsync(simCard);
                //        if (firstSimCardBusiness is null) break;
                //        var lastUseSheypoor = firstSimCardBusiness.NextUseSheypoor;

                //        //اگر توکن شیپور نداشت سیمکارت عوض بشه

                //        var tt = await AdvTokensBusiness.GetToken(simCard, AdvertiseType.Sheypoor);
                //        var hasToken = tt?.Token ?? null;
                //        if (string.IsNullOrEmpty(hasToken)) continue;


                //        var startDayOfCurrentMonthOfDateShToMiladi = Calendar.StartDayOfPersianMonth();
                //        var startDayOfNextMonthOfDateShToMiladi = Calendar.EndDayOfPersianMonth().AddDays(1);
                //        //آمار آگهی های ثبت شده برای سیم کارت در ماه جاری
                //        var a1 = await AdvertiseLogBusiness.GetAllSpecialAsync(p =>
                //            p.SimCardNumber == simCard && p.AdvType == AdvertiseType.Sheypoor
                //                                     && (p.StatusCode == (short)StatusCode.Published
                //                                         || p.StatusCode == (short)StatusCode.InPublishQueue)
                //                                     && p.DateM >= startDayOfCurrentMonthOfDateShToMiladi);
                //        var registeredAdvCountInMonth = a1.Count;
                //        if (registeredAdvCountInMonth >= cls?.SheypoorSetting?.AdvCountInMonth)
                //        {
                //            //تاریخ روز اول ماه شمسی بعد را تنظیم می کند چون تا سر ماه بعد دیگر نیازی به این سیم کارت نیست
                //            firstSimCardBusiness.NextUseSheypoor = startDayOfNextMonthOfDateShToMiladi;
                //            await firstSimCardBusiness.SaveAsync();
                //            continue;
                //        }


                //        //آمار آگهی های ثبت شده امروز
                //        var currentDate = DateTime.Now.Date;
                //        var a2 = await AdvertiseLogBusiness.GetAllSpecialAsync(p =>
                //            p.SimCardNumber == simCard && p.AdvType == AdvertiseType.Sheypoor
                //            && (p.StatusCode == (short)StatusCode.Published
                //                || p.StatusCode == (short)StatusCode.InPublishQueue
                //                || p.StatusCode == (short)StatusCode.WaitForPayment)
                //            && p.DateM >= currentDate);
                //        var registeredAdvCountInDay = a2.Count;
                //        if (registeredAdvCountInDay >= cls?.SheypoorSetting?.AdvCountInDay)
                //        {
                //            //تاریخ فردا رو ست می کند چون تا فردا دیگه نیازی به این سیم کارت نیست
                //            firstSimCardBusiness.NextUseSheypoor = DateTime.Today.AddDays(1);
                //            await firstSimCardBusiness.SaveAsync();
                //            continue;
                //        }

                //        if (cls?.SheypoorSetting?.AdvCountInDay >= (cls?.SheypoorSetting?.AdvCountInMonth - registeredAdvCountInDay)) continue;

                //        if (await Login(simCard) == false)
                //        {
                //            firstSimCardBusiness.NextUseSheypoor = lastUseSheypoor;
                //            await firstSimCardBusiness.SaveAsync();
                //            continue;
                //        }



                //        //اینجا به تعداد تنظیم شده در تنظیمات دیوار منهای تعداد ثبت شده قبلی، آگهی درج می کند
                //        for (var i = 0; i < cls?.SheypoorSetting?.AdvCountInDay - registeredAdvCountInDay; i++)
                //        {
                //            if (cls?.SheypoorSetting?.AdvCountInMonth <= registeredAdvCountInMonth) break;
                //            while (cls?.SheypoorSetting.CountAdvInIp <= await
                //                       AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                //                           AdvertiseType.Sheypoor))
                //            {
                //                ShowBalloon("پر شدن تعداد آگهی در " + await FindGateWay(),
                //                    "سیستم در حال تعویض IP یا سایت می باشد");
                //                await SetGateway(await GetRandomGeteWay());
                //                var currentIp1 = await GetLocalIpAddress();
                //                if (await FindGateWay() != IP_Store.IP_Mokhaberat.Value ||
                //                    !(cls?.SheypoorSetting.CountAdvInIp <= await
                //                          AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                //                              AdvertiseType.Sheypoor))) continue;
                //                var countAdvInIp1 = await
                //                    AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(), AdvertiseType.Sheypoor);
                //                if (!(cls?.SheypoorSetting.CountAdvInIp <= countAdvInIp1)) continue;
                //                if (await GoToNextSite(AdvertiseType.Sheypoor, 0)) continue;
                //                await ChangeIp();

                //                while (await GetLocalIpAddress() == null)
                //                {
                //                    if (counter == 30)
                //                        await SetGateway(await GetRandomGeteWay());
                //                    await Wait(10);
                //                    ShowBalloon("درحال اتصال...", "مودم مخابرات ریست شد. لطفا منتظر بمانید");
                //                    counter++;
                //                }

                //                if (await GoToNextSite(AdvertiseType.Sheypoor, 1)) continue;
                //            }
                //            var adv = await GetNextAdv(simCard);
                //            if (adv == null) continue;
                //            await RegisterAdv(adv);
                //            var title = "تعداد آگهی های ارسال شده با " + await FindGateWay();
                //            var body = await
                //                AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                //                    AdvertiseType.Sheypoor);
                //            ShowBalloon(title, body.ToString());
                //            registeredAdvCountInMonth++;

                //        }
                //    }

                //    await Wait(10);
                //    ShowBalloon("لطفا اتصال به شبکه را چک نمایید", "خطای اتصال به شبکه");
                //    continue;

                //}
                //اگر کاربر روی یک سیم کارت خاص می خواهد تعدادی آگهی درج کند
                //else
                //{
                foreach (var t in numbers)
                {
                    var tt = await AdvTokensBusiness.GetToken(t, AdvertiseType.Sheypoor);
                    var hasToken = tt?.Token ?? null;
                    if (string.IsNullOrEmpty(hasToken)) return;
                    if (!await Login(t)) continue;

                    for (var i = 0; i < count; i++)
                    {
                        //while (cls?.SheypoorSetting.CountAdvInIp <= await
                        //           AdvertiseLogBusiness.GetAllAdvInDayFromIP(
                        //               await GetLocalIpAddress(),
                        //               AdvertiseType.Sheypoor))
                        //{
                        //    ShowBalloon("پر شدن تعداد آگهی در " + await FindGateWay(),
                        //        "سیستم در حال تعویض IP یا سایت می باشد");
                        //    await SetGateway(await GetRandomGeteWay());
                        //    var currentIp1 = await GetLocalIpAddress();
                        //    if (await FindGateWay() != IP_Store.IP_Mokhaberat.Value ||
                        //        !(cls?.SheypoorSetting.CountAdvInIp <= await
                        //              AdvertiseLogBusiness.GetAllAdvInDayFromIP(
                        //                  await GetLocalIpAddress(),
                        //                  AdvertiseType.Sheypoor))) continue;
                        //    await ChangeIp();

                        //    while (await GetLocalIpAddress() == null)
                        //    {
                        //        if (counter == 30)
                        //            await SetGateway(await GetRandomGeteWay());
                        //        await Wait(10);
                        //        ShowBalloon("درحال اتصال...", "مودم مخابرات ریست شد. لطفا منتظر بمانید");
                        //        counter++;
                        //    }
                        //}
                        var adv = await GetNextAdv(t);
                        if (adv == null) continue;
                        await RegisterAdv(adv);
                        var title = await FindGateWay();
                        var co = await AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                            AdvertiseType.Sheypoor);
                        lstMessage.Clear();
                        lstMessage.Add($"نوع آگهی: شیپور");
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


        public async Task RegisterAdv(AdvertiseLogBusiness adv)
        {
            var ret = new ReturnedSaveFuncInfo();
            var monitor = new PerfMonitor();
            try
            {
                var counter = 0;
                adv.AdvType = AdvertiseType.Sheypoor;
                _driver = RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://www.sheypoor.com/listing/new");
                await Wait();

                //کلیک کردن روی کتگوری اصلی
                _driver.FindElements(By.ClassName("form-select")).FirstOrDefault()?.Click();
                await Wait();

                //کلیک روی ساب کتگوری 1
                if (string.IsNullOrEmpty(adv.SubCategory1))
                    adv.SubCategory1 = cls?.SheypoorSetting?.Category1;
                _driver.FindElements(By.ClassName("link")).FirstOrDefault(q => q.Text == adv.SubCategory1)?.Click();

                await Wait();

                //کلیک روی ساب کتگوری2
                if (string.IsNullOrEmpty(adv.SubCategory2))
                    adv.SubCategory2 = cls?.SheypoorSetting?.Category2;
                _driver.FindElements(By.ClassName("link")).FirstOrDefault(q => q.Text == adv.SubCategory2)?.Click();


                //درج عکسها
                _driver.FindElement(By.ClassName("qq-upload-button-selector")).FindElement(By.TagName("input"))
                    .SendKeys(adv.ImagesPath);


                //درج عنوان آگهی
                _driver.FindElement(By.Name("name")).SendKeys("");
                _driver.FindElement(By.Name("name")).SendKeys(adv.Title);
                //await Wait();
                //درج محتوای آگهی

                var thread = new Thread(() => Clipboard.SetText(adv.Content.Replace('(', '<').Replace(')', '>')));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

                var t = _driver.FindElement(By.Id("item-form-description"));
                t.Click();
                await Wait();
                t.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                var thread1 = new Thread(Clipboard.Clear);
                thread1.SetApartmentState(ApartmentState.STA);
                thread1.Start();


                //درج قیمت
                var txtPrice = _driver.FindElements(By.Id("item-form-price")).Count;
                if (adv?.Price > 0 && txtPrice != 0)
                {
                    _driver.FindElement(By.Id("item-form-price"))?.SendKeys("");
                    _driver.FindElement(By.Id("item-form-price"))?.SendKeys(adv.Price.ToString());
                }

                await Wait();

                //انتخاب شهر
                await Wait();
                _driver.FindElements(By.ClassName("form-select")).LastOrDefault()?.Click();
                await Wait(2);
                var a = _driver.FindElements(By.ClassName("mode-district")).Any();
                if (a)
                {
                    _driver.FindElement(By.ClassName("mode-district")).FindElement(By.ClassName("link"))?.Click();
                    await Wait();
                    _driver.FindElement(By.ClassName("mode-city")).FindElement(By.ClassName("link"))?.Click();
                }

                await Wait(1);

                _driver.FindElements(By.TagName("li"))?.FirstOrDefault(q => q.Text.Contains(adv.State))?.Click();
                await Wait(1);

                var cc = _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text.Contains(adv.City));
                cc?.Click();
                var cty = await CityBusiness.GetAsync(adv?.City);
                var randCity = await CityBusiness.GetNextRandomCityAsync(adv.MasterVisitorGuid, AdvertiseType.Sheypoor);
                await Wait(1);
                var cityGuid = !string.IsNullOrEmpty(adv?.City) ? cty.Guid : randCity.Guid;

                var lst = await RegionBusiness.GetAllAsync(cityGuid, AdvertiseType.Sheypoor);
                var regionList = lst?.ToList() ?? new List<RegionBusiness>();
                if (regionList.Count > 0)
                {
                    var rnd = new Random().Next(0, regionList.Count);
                    var regName = regionList[rnd].Name;
                    await Wait(2);
                    _driver.FindElements(By.TagName("li"))?.FirstOrDefault(q => q.Text == regName)
                        ?.Click();
                    adv.Region = regName;
                }
                // await Wait();


                //کلیک روی دکمه ثبت آگهی
                while (_driver.Url == "https://www.sheypoor.com/listing/new")
                {
                    counter++;
                    await Wait(2);
                    _driver.FindElements(By.TagName("button")).FirstOrDefault(q => q.Text == "ثبت آگهی")
                        ?.Click();
                    await Wait();
                    var box = _driver.FindElements(By.ClassName("box")).Any(q => q.Text.Contains("حساب کاربری"));
                    if (box) return;
                    if (counter < 60) continue;
                    await GetScreenShot();
                    adv.URL = "---";
                    adv.UpdateDesc = @"خطای درج";
                    adv.AdvStatus = @"خطای درج";
                    adv.StatusCode = (short)StatusCode.InsertError;
                    adv.AdvType = AdvertiseType.Sheypoor;
                    adv.IP = await GetLocalIpAddress();
                    await adv.SaveAsync();
                    await Wait();
                    counter = 0;
                    _driver.Navigate().GoToUrl("https://www.sheypoor.com");
                    return;
                }



                //اگر آگهی با موفقیت ثبت شود لینک مدیریت آگهی ذخیره می شود
                await Wait();
                await Wait(2);
                counter = 0;
                adv.URL = await MakeUrl(_driver.Url);
                adv.UpdateDesc = @"در صف انتشار";
                adv.AdvStatus = @"در صف انتشار";
                adv.StatusCode = (short)StatusCode.InPublishQueue;
                adv.AdvType = AdvertiseType.Sheypoor;
                adv.IP = await GetLocalIpAddress();
                await adv.SaveAsync();
                await Wait();
                if (!_driver.Url.Contains(adv.URL))
                    _driver.Navigate().GoToUrl("https://www.sheypoor.com");

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

        public async Task ViewAdv(long simCard, string url)
        {
            if (_semaphoreSlim.CurrentCount == 0)
            {
                DialogResult result;
                result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    _tokenSource?.Cancel();
                else return;
            }

            await _semaphoreSlim.WaitAsync();
            _tokenSource = new CancellationTokenSource();

            var monitor = new PerfMonitor();
            try
            {
                if (_tokenSource.IsCancellationRequested) return;
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
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> Login(long simCardNumber)
        {
            PerfMonitor monitor = new PerfMonitor();
            try
            {
                _driver = RefreshDriver(_driver);
                var simBusiness = await AdvTokensBusiness.GetToken(simCardNumber, AdvertiseType.Sheypoor);
                var token = simBusiness?.Token;

                //   در صورتیکه توکن قبلا ثبت شده باشد لاگین می کند
                if (!string.IsNullOrEmpty(token))
                {

                    // token = _driver.Manage().Cookies.GetCookieNamed("ts").ToString().Substring(3, 32);
                    _driver.Navigate().GoToUrl("https://www.sheypoor.com");
                    _driver.Manage().Cookies.DeleteCookieNamed("ts");

                    var newToken = new Cookie("ts", token);
                    _driver.Manage().Cookies.AddCookie(newToken);
                    _driver.Navigate().GoToUrl("https://www.sheypoor.com/session/myListings");
                    var linksElements = _driver.FindElements(By.TagName("a")).ToList();
                    foreach (var link in linksElements)
                    {
                        if (link?.GetAttribute("href") == null || !link.GetAttribute("href").Contains("logout"))
                            continue;
                        _driver.ExecuteJavaScript(@"alert('لاگین انجام شد');");
                        await Wait();
                        _driver.SwitchTo().Alert().Accept();
                        return true;
                    }
                }
                //اگر قبلا توکن نداشته و یا توکن اشتباه باشد وارد صفحه دریافت کد تائید لاگین می شود 
                _driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 2, 0);
                _driver.Navigate().GoToUrl("https://www.sheypoor.com/session/myListings");
                //var all = _driver.Manage().Cookies.AllCookies;
                if (_driver.FindElements(By.Id("username")).Count > 0)
                    _driver.FindElement(By.Id("username")).SendKeys(simCardNumber + "\n");

                //انتظار برای لاگین شدن
                int repeat = 0;
                //حدود 120 ثانیه فرصت لاگین دارد
                while (repeat < 20)
                {
                    //تا زمانی که لاگین اوکی نشده باشد این حلقه تکرار می شود

                    var name = await SimCardBusiness.GetOwnerNameAsync(simCardNumber);
                    var message = $@"مالک: {name} \r\nشماره: {simCardNumber}  \r\nلطفا لاگین نمائید ";
                    _driver.ExecuteJavaScript($"alert('{message}');");
                    //Wait();

                    await Wait(3);
                    try
                    {
                        _driver.SwitchTo().Alert().Accept();
                        await Wait(3);
                        repeat++;
                    }
                    catch
                    {
                        await Wait(15);
                    }

                    var linksElements = _driver?.FindElements(By.TagName("a")).ToList() ?? null;
                    //if (linksElements == null)
                    //{
                    //    var msg = $@"فرصت لاگین کردن به اتمام رسید. لطفا دقایقی بعد مجددا امتحان نمایید";
                    //    _driver.ExecuteJavaScript($"alert('{msg}');");
                    //    return false;
                    //}

                    foreach (var link in linksElements)
                    {
                        if (link?.GetAttribute("href") == null || !link.GetAttribute("href").Contains("logout"))
                            continue;
                        token = _driver.Manage().Cookies.GetCookieNamed("ts").ToString().Substring(3, 32);
                        if (simBusiness != null)
                            simBusiness.Token = token;
                        else
                        {
                            simBusiness = new AdvTokensBusiness()
                            {
                                Type = AdvertiseType.Sheypoor,
                                Token = token,
                                Number = simCardNumber,
                                Modified = DateTime.Now,
                                Guid = Guid.NewGuid(),
                            };
                        }

                        await simBusiness.SaveAsync();
                        _driver.ExecuteJavaScript(@"alert('لاگین انجام شد');");
                        await Wait();
                        _driver.SwitchTo().Alert().Accept();
                        return true;
                    }
                }

                var linksElements1 = _driver?.FindElements(By.TagName("a")).FirstOrDefault(q => q.Text == "خروج") ??
                                     null;
                if (linksElements1 == null)
                {
                    var msg = $@"فرصت لاگین کردن به اتمام رسید. لطفا دقایقی بعد مجددا امتحان نمایید";
                    _driver.ExecuteJavaScript($"alert('{msg}');");
                    _driver.SwitchTo().Alert().Accept();
                    await Wait(3);
                }

                _driver.Navigate().GoToUrl("https://www.sheypoor.com");
                await Wait();
                if (simBusiness == null) return false;
                simBusiness.Token = null;
                await simBusiness.SaveAsync();

                return false;

            }
            catch (WebException) { return false; }
            catch (Exception ex)
            {
                if (ex.Source != "WebDriver")
                    WebErrorLog.ErrorLogInstance.StartLog(ex);
                return false;
            }

            finally { monitor.Dispose(); }
        }

        private async Task<bool> UpdateAllRegisteredAdvOfSimCard(long simCardNumber)
        {
            PerfMonitor monitor = new PerfMonitor();
            try
            {
                _driver = RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://sheypoor.com/session/myListings");
                var allPost = _driver.FindElements(By.ClassName("myListings"));
                var manageLinks = new List<string>();
                foreach (var post in allPost)
                {
                    var url = post.GetAttribute("href");

                    manageLinks.Add(url);
                }

                foreach (var url in manageLinks)
                {
                    var getUrl = await AdvertiseLogBusiness.GetAsync(url);
                    if (!string.IsNullOrEmpty(getUrl?.URL)) continue;
                    _driver.Navigate().GoToUrl(url);

                    var lst = await AdvertiseLogBusiness.GetAllSpecialAsync(p => p.URL == url);
                    var newAdv = lst.FirstOrDefault();

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
                    await newAdv.SaveAsync();
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


        private async Task<string> GetScreenShot()
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
        private static DateTime GetDateMFromPublishTime(string publishStr)
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


        private static StatusCode GetAdvStatusCodeByStatus(string advStatus)
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

        private List<Advertise> AdvertiseList { get; set; }
        private async Task<AdvertiseLogBusiness> GetNextAdv(long simCardNumber)
        {
            var monitor = new PerfMonitor();
            try
            {
                var newAdvertiseLogBusiness = new AdvertiseLogBusiness();
                while (newAdvertiseLogBusiness.Content == null || newAdvertiseLogBusiness.Content == "-")
                {
                    newAdvertiseLogBusiness = new AdvertiseLogBusiness { SimCardNumber = simCardNumber };

                    #region find visitor and text replacements

                    //پیدا کردن ویزیتور ها و متن جایگزین محتوا برای سیم کارت مورد نظر
                    var replacements =
                        await VisitorBusiness.GetMasterSlaveAdvReplacementsAsync(newAdvertiseLogBusiness.SimCardNumber);
                    if (replacements?.MasterGuid == null) continue;

                    newAdvertiseLogBusiness.MasterVisitorGuid = (Guid)replacements.MasterGuid;
                    if (replacements.SlaveGuid != null)
                        newAdvertiseLogBusiness.SlaveVisitorGuid = (Guid)replacements.SlaveGuid;

                    #endregion

                    #region find visitorAdvs

                    //لیست آگهی های مرتبط با ویزیتور دریافت می شود
                    AdvertiseList = await Advertise.GetAllAsync(AdvRootPath, newAdvertiseLogBusiness.MasterVisitorGuid);
                    AdvertiseList = AdvertiseList.Where(q => q.PishNevis == false).ToList();
                    if (!(AdvertiseList?.Count > 0)) continue;

                    #endregion

                    #region findNextAdvIndex


                    var nextAdvIndex = new Random().Next(AdvertiseList.Count);

                    #endregion

                    #region GetPath

                    string path = null;
                    path = Path.Combine(
                        Path.Combine(cls?.SheypoorSetting?.PicPath, AdvertiseList[nextAdvIndex].AdvName) ==
                        AdvertiseList[nextAdvIndex].AdvName
                            ? AdvertiseList[nextAdvIndex].RootPath
                            : cls?.SheypoorSetting?.PicPath, AdvertiseList[nextAdvIndex].AdvName);
                    newAdvertiseLogBusiness.Adv = path;

                    #endregion


                    #region FindNextTitle

                    //تایتل آگهی دریافت می شود
                    if (!(AdvertiseList[nextAdvIndex].Titles?.Count > 0)) continue;

                    var nextTitleIndex =
                        new Random(DateTime.Now.Millisecond).Next(AdvertiseList[nextAdvIndex].Titles.Count);
                    newAdvertiseLogBusiness.Title = AdvertiseList[nextAdvIndex].Titles[nextTitleIndex];


                    if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Content)) continue;

                    #endregion

                    #region GetContent

                    //کانتنت آگهی دریافت می شود
                    var rand = new Random().Next(1, 12);
                    if (rand > 1 && rand <= 4)
                        newAdvertiseLogBusiness.Content = AdvertiseList[nextAdvIndex].Content
                            .Replace("<master>", replacements.AdvReplacement1)
                            .Replace("<slave>", "")
                            .Replace("<tell>", "")
                            .Replace("(", "<")
                            .Replace(")", ">");
                    if (rand > 4 && rand <= 8)
                        newAdvertiseLogBusiness.Content = AdvertiseList[nextAdvIndex].Content
                            .Replace("<master>", "")
                            .Replace("<slave>", replacements.AdvReplacement2)
                            .Replace("<tell>", "")
                            .Replace("(", "<")
                            .Replace(")", ">");
                    if (rand > 8 && rand <= 12)
                        newAdvertiseLogBusiness.Content = AdvertiseList[nextAdvIndex].Content
                            .Replace("<master>", "")
                            .Replace("<slave>", "")
                            .Replace("<tell>", replacements.Phone)
                            .Replace("(", "<")
                            .Replace(")", ">");

                    if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Content))
                        continue;

                    #endregion

                    #region FindImages

                    //عکسهای آگهی دریافت می شود
                    newAdvertiseLogBusiness.ImagesPathList = GetNextImages(newAdvertiseLogBusiness.Adv,
                        cls?.SheypoorSetting?.PicCountInPerAdv ?? 3);

                    #endregion

                    //قیمت آگهی دریافت می شود
                    newAdvertiseLogBusiness.Price = AdvertiseList[nextAdvIndex].Price;

                    #region GetCity

                    var city = await CityBusiness.GetNextRandomCityAsync(newAdvertiseLogBusiness.MasterVisitorGuid,
                        AdvertiseType.Sheypoor);
                    newAdvertiseLogBusiness.City = city?.CityName;
                    newAdvertiseLogBusiness.State = city?.State.StateName;

                    #endregion

                    #region GetCategory

                    if (AdvertiseList[nextAdvIndex].SheypoorCategories != null &&
                        AdvertiseList[nextAdvIndex].SheypoorCategories.Count > 0)
                    {
                        newAdvertiseLogBusiness.SubCategory1 = AdvertiseList[nextAdvIndex]?.SheypoorCategories[0] ?? "";
                        newAdvertiseLogBusiness.SubCategory2 = AdvertiseList[nextAdvIndex]?.SheypoorCategories[1] ?? "";
                    }
                    else
                    {
                        newAdvertiseLogBusiness.SubCategory1 = null;
                        newAdvertiseLogBusiness.SubCategory2 = null;
                    }

                    #endregion

                }
                return newAdvertiseLogBusiness;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return null;
            }

            finally { monitor.Dispose(); }
        }

        public async Task<List<string>> GetAllRegionFromSheypoor(string State, string City)
        {
            var region = new List<string>();
            _driver = RefreshDriver(_driver);
            string Name = "";
            _driver.Navigate().GoToUrl("https://Sheypoor.com/listing/new");
            try
            {
                _driver.FindElements(By.ClassName("form-select")).LastOrDefault()?.Click();
                await Wait(1);
                _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text == State)?.Click();
                await Wait(1);
                _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text == City)?.Click();
                await Wait(1);
                var regions = _driver.FindElements(By.ClassName("list-items"))?.First();
                foreach (var item in regions.Text)
                {
                    if (item != '\n')
                    {
                        if (item != '\r')
                        {
                            Name = Name + item;
                        }
                        else
                        {
                            region.Add(Name);
                            Name = "";
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return region;
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
        public static async Task<SheypoorAdv> GetInstance()
        {
            await GetDataFromSetting();
            return _me ?? (_me = new SheypoorAdv());
        }

        private static async Task<SettingBusiness> GetDataFromSetting()
        {
            try
            {
                var res = await SettingBusiness.GetAllAsync();
                cls = res.Count == 0 ? new SettingBusiness() : res[0];
                return cls;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return null;
            }
        }
        private static SheypoorAdv _me;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        public async Task<bool> UpdateAllAdvStatus(int dayCount = 0)
        {
            if (_semaphoreSlim.CurrentCount == 0)
            {
                DialogResult result;
                result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    _tokenSource?.Cancel();
                else return false;
            }

            await _semaphoreSlim.WaitAsync();
            _tokenSource = new CancellationTokenSource();
            try
            {
                _driver = RefreshDriver(_driver);
                if (dayCount == 0)
                    dayCount = cls.SheypoorSetting?.DayCountForUpdateState ?? 10;

                var lastWeek = DateTime.Now.AddDays(-dayCount);
                var lst = await AdvertiseLogBusiness.GetAllSpecialAsync(p =>
                    p.DateM > lastWeek && p.AdvType == AdvertiseType.Sheypoor);
                var allAdvertiseLog = lst.OrderBy(q => q.SimCardNumber).ThenBy(p => p.DateSh)
                    .ThenBy(p => p.priorityOfUpdateSt).ThenBy(p => p.rnd).ToList();
                if (allAdvertiseLog.Count <= 0) return true;
                var tryCount = 0;
                long mobile = 0;
                foreach (var adv in allAdvertiseLog)
                {
                    if (_tokenSource.IsCancellationRequested) break;
                    if (tryCount >= 3) continue;
                    try
                    {
                        var sim = await SimCardBusiness.GetAsync(adv.SimCardNumber);
                        if (sim.isSheypoorBlocked) continue;
                        if (mobile != adv.SimCardNumber)
                        {
                            var ls = await AdvTokensBusiness.GetToken(adv.SimCardNumber, AdvertiseType.Sheypoor);
                            var tok = ls.Token;
                            if (string.IsNullOrEmpty(tok)) continue;
                            mobile = adv.SimCardNumber;
                            var log = await Login(adv.SimCardNumber);
                            if (!log)
                            {
                                mobile = 0;
                                continue;
                            }
                        }
                        if (adv.URL == "---") continue;
                        var code = adv.URL.Remove(0, 25) ?? null;
                        await Wait();
                        var el = _driver.FindElements(By.TagName("img")).Any(q =>
                            q.GetAttribute("src").Contains("/img/empty-state/mylistings.png"));
                        if (el)
                        {
                            adv.AdvStatus = "رد شده";
                            adv.UpdateDesc = "در انتظار تایید ادمین/ رد شده/ حذف شده";
                            adv.StatusCode = (short)StatusCode.Failed;
                            await adv.SaveAsync();
                            await InsertDataInAdvVisitLog(adv);
                            continue;
                        }

                        var element = _driver.FindElements(By.Id("listing-" + code)).Any();
                        await Wait();

                        if (!element || string.IsNullOrEmpty(code))
                        {
                            adv.AdvStatus = "رد شده";
                            adv.UpdateDesc = "در انتظار تایید ادمین/ رد شده/ حذف شده";
                            adv.StatusCode = (short)StatusCode.Failed;
                            await adv.SaveAsync();
                            await InsertDataInAdvVisitLog(adv);
                            continue;
                        }

                        _driver.FindElement(By.Id("listing-" + code))?.Click();
                        await Wait();
                        adv.AdvStatus = "منتشر شده";
                        adv.UpdateDesc = "آگهی منتشر شده و در لیست آگهی های شیپور قرار گرفته است";
                        //var counter = _driver.FindElement(By.ClassName("total-view"))?.FindElement(By.TagName("strong"))
                        //                  ?
                        //                  .Text.FixString() ?? "0";
                        var counter = _driver.FindElement(By.ClassName("stat-view"))?.Text.FixString() ?? "0";
                        adv.VisitCount = counter.ParseToInt();
                        adv.StatusCode = (short)StatusCode.Published;
                        adv.AdvType = AdvertiseType.Sheypoor;
                        await adv.SaveAsync();
                        await InsertDataInAdvVisitLog(adv);
                        tryCount = 0;
                        _driver.Navigate().Back();
                        await Wait();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Source != "WebDriver" && !_tokenSource.IsCancellationRequested)
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
                _semaphoreSlim.Release();
            }
        }

        private async Task<string> MakeUrl(string url)
        {
            try
            {
                var charUrl = url.ToCharArray();
                var counterUrl = 0;
                var counterCode = 0;
                var newUrl = "";
                var code = "";
                foreach (var item in charUrl)
                {
                    if (counterUrl >= 3)
                        break;
                    newUrl = newUrl + item;
                    if (item == '/')
                        counterUrl++;
                }
                foreach (var item in charUrl)
                {
                    if (item == '/')
                        counterCode++;
                    if (counterCode == 5)
                    {
                        code = code + item;
                    }

                }

                if (code != "")
                    code = code.Remove(0, 1);
                newUrl = newUrl + code;
                return newUrl;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return null;
            }
        }


        public async Task<List<SheypoorCityBusiness>> GetAllCityFromSheypoor()
        {
            var cities = new List<SheypoorCityBusiness>();
            var states = await StateBusiness.GetAllAsync();
            _driver = RefreshDriver(_driver);
            _driver.Navigate().GoToUrl("https://www.sheypoor.com");
            try
            {
                _driver.FindElements(By.ClassName("form-select")).LastOrDefault()?.Click();

                foreach (var stateItem in states)
                {
                    _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text == stateItem.StateName)?.Click();


                    var cc = _driver.FindElements(By.TagName("span"))
                        .Where(d => d.GetAttribute("class").Contains("t-city")).ToList();
                    foreach (var item in cc)
                    {
                        if (item.Text == "") continue;
                        var a = new SheypoorCityBusiness
                        {
                            Guid = Guid.NewGuid(),
                            Modified = DateTime.Now,
                            CityName = item.Text
                        };
                        cities.Add(a);
                    }

                    _driver
                        .FindElements(By.TagName("span"))
                        .FirstOrDefault(d => d.GetAttribute("class").Contains("link") && d.Text.Contains("بازگشت"))?
                        .Click();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return cities;
        }

        private async Task<bool> InsertDataInAdvVisitLog(AdvertiseLogBusiness adv)
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
                    Type = AdvertiseType.Sheypoor
                };
                var res = await cls.SaveAsync();
                return !res.HasError;
            }
            catch
            {
                return false;
            }
        }

        //public async Task<List<string>> GetNumbersFromSheypoor()
        //{
        //    var list = new List<string>();
        //    try
        //    {
        //        var number = await SimCardBusiness.GetNextSimCardNumberAsync((short)AdvertiseType.Sheypoor);
        //        var hasToken = AdvTokensBusiness.GetToken(number, AdvertiseType.Sheypoor).Token;
        //        if (string.IsNullOrEmpty(hasToken)) return null;
        //        var log = await Login(number);
        //        if (!log) return null;
        //        _driver = RefreshDriver(_driver);
        //        _driver.Navigate().GoToUrl("https://sheypoor.com/");
        //        await Wait();
        //        _driver.FindElements(By.ClassName("button")).FirstOrDefault(q => q.Text == "نمایش همه آگهی‌ها")
        //            ?.Click();
        //        await Wait();
        //        for (var i = 0; i < 20; i++)
        //        {
        //            var advList = _driver.FindElements(By.ClassName("button")).Where(q => q.Text == "تماس").ToList();
        //            await Wait();
        //            foreach (var item in advList)
        //            {
        //                item.Click();
        //                await Wait(1);
        //                list.Add(item.Text.FixString());
        //                await Wait(1);
        //            }

        //            _driver.FindElements(By.ClassName("button")).FirstOrDefault(t => t.Text == "بعدی")?.Click();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WebErrorLog.ErrorLogInstance.StartLog(ex);
        //        list = null;
        //    }

        //    return list;
        //}
        public async Task GetChatCount(List<SimCardBusiness> simcard)
        {
            try
            {
                foreach (var sim in simcard)
                {
                    var log = await Login(sim.Number);
                    if (!log) continue;
                    await Wait();
                    var q = _driver.FindElements(By.TagName("small")).ToList();
                    if (q.Count == 0) continue;
                    await Wait();
                    _driver.Navigate().GoToUrl("https://www.sheypoor.com/session/myChats");
                    await Wait(1);
                    var news = _driver.FindElements(By.ClassName("badge"));
                    if (news.Count == 0) continue;
                    foreach (var item in news)
                    {
                        if (string.IsNullOrEmpty(item.Text)) continue;
                        while (_driver.Url == "https://www.sheypoor.com/session/myChats")
                        {
                            await Wait();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
        }

        public async Task FindBlockNumbers()
        {
            //var path = Path.Combine(Application.StartupPath, "SheypoorBlockNumbers.txt");
            //var list = new List<string>();
            var counter = 0;
            while (await PingHost("185.105.239.1") == false)
            {
                if (counter == 30)
                    await SetGateway(await GetRandomGeteWay());
                await Wait(5);
                lstMessage.Clear();
                lstMessage.Add("خطای اتصال به شبکه");
                ShowBalloon("لطفا اتصال به شبکه را چک نمایید", lstMessage);
                counter++;
            }

            _tokenSource?.Cancel();
            await SemaphoreSlim.WaitAsync();

            _tokenSource = new CancellationTokenSource();
            try
            {
                _driver = RefreshDriver(_driver);
                SimCardBusiness firstSimCardBusiness = null;
                while (!_tokenSource.IsCancellationRequested)
                {
                    while (await PingHost("185.105.239.1"))
                    {
                        var a = await SimCardBusiness.GetAllAsync();
                        var lstSim = a.Where(q => q.isSheypoorBlocked == false).Select(q => q.Number).ToList();
                        foreach (var simCard in lstSim)
                        {
                            while (cls?.SheypoorSetting.CountAdvInIp <= await
                                       AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                                           AdvertiseType.Sheypoor))
                            {
                                //ShowBalloon("لطفا IP سیستم خود را تغییر دهید",
                                //    "پر شدن تعداد آگهی در " + await FindGateWay());
                                await SetGateway(await GetRandomGeteWay());
                                var currentIp = await GetLocalIpAddress();
                                if (await FindGateWay() == IP_Store.IP_Mokhaberat.Value && cls?.SheypoorSetting.CountAdvInIp <= await
                                        AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                                            AdvertiseType.Sheypoor))
                                {
                                    await ChangeIp();

                                    while (await GetLocalIpAddress() == null)
                                    {
                                        if (counter == 30)
                                            await SetGateway(await GetRandomGeteWay());
                                        await Wait(10);
                                        lstMessage.Clear();
                                        lstMessage.Add("مودم مخابرات ریست شد. لطفا منتظر بمانید");
                                        ShowBalloon("درحال اتصال...", lstMessage);
                                        counter++;
                                    }
                                }

                                continue;
                            }
                            if (simCard == 0) break;

                            firstSimCardBusiness = await SimCardBusiness.GetAsync(simCard);
                            if (firstSimCardBusiness.isSheypoorBlocked) continue;
                            firstSimCardBusiness.NextUseSheypoor = DateTime.Now.AddMinutes(30);
                            await firstSimCardBusiness.SaveAsync();
                            //اگر توکن شیپور نداشت سیمکارت عوض بشه

                            var tt = await AdvTokensBusiness.GetToken(simCard, AdvertiseType.Sheypoor);
                            var hasToken = tt?.Token ?? null;
                            if (string.IsNullOrEmpty(hasToken)) continue;

                            if (!await Login(simCard)) continue;
                            await Wait();
                            var el2 = _driver.FindElements(By.TagName("img")).Any(q =>
                                q.GetAttribute("src").Contains("/img/empty-state/mylistings.png"));
                            await Wait();
                            if (!el2)
                                continue;

                            var adv = await GetNextAdv(simCard);
                            if (adv == null) continue;
                            await RegisterAdv(adv);
                            await Wait(1);

                            _driver.Navigate().GoToUrl("https://www.sheypoor.com/session/myListings");
                            await Wait(1);

                            var el = _driver.FindElements(By.TagName("img")).Any(q =>
                                q.GetAttribute("src").Contains("/img/empty-state/mylistings.png"));
                            await Wait();
                            if (!el) continue;
                            var sim = await SimCardBusiness.GetAsync(simCard);
                            if (sim.isSheypoorBlocked) continue;
                            sim.isSheypoorBlocked = true;
                            await sim.SaveAsync();
                        }
                    }

                    await Wait(5);
                    lstMessage.Clear();
                    lstMessage.Add("خطای اتصال به شبکه");
                    ShowBalloon("لطفا اتصال به شبکه را چک نمایید", lstMessage);
                    continue;
                }

            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
            }
        }

        private List<string> lstMessage = new List<string>();
    }
}
