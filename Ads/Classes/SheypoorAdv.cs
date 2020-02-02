using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinesLayer;
using DataLayer;
using DataLayer.Enums;
using FMessegeBox;
using OpenQA.Selenium;
using Cookie = OpenQA.Selenium.Cookie;

namespace Ads.Classes
{
    public class SheypoorAdv
    {
        #region Fields

        private IWebDriver _driver;
        private bool _noErrorInRegister = true;
        private string _advRootPath;
        private int _maxImgForAdv;
        private static SettingBussines cls;
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
            // if (string.IsNullOrEmpty(_advRootPath)) _advRootPath = ConfigurationManager.AppSettings.Get("RootPath");
            _maxImgForAdv = maxImgForAnyAdv;

            _driver = Utility.RefreshDriver(_driver);

        }

        #endregion
        public SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1);
        // private static string AdvRootPath => ConfigurationManager.AppSettings.Get("RootPath");
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
            var isLogin = false;

            try
            {
                while (await Utility.PingHost("185.105.239.1") == false)
                {
                    if (counter >= 30)
                        await Utility.SetGateway(await Utility.GetRandomGeteWay());
                    await Utility.Wait(10);
                    lstMessage.Clear();
                    lstMessage.Add("خطای اتصال به شبکه");
                    Utility.ShowBalloon("لطفا اتصال به شبکه را چک نمایید", lstMessage);
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
                    var tt = AdvTokensBussines.GetToken(t, AdvertiseType.Sheypoor);
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
                        var title = await Utility.FindGateWay();
                        var co = AdvertiseLogBussines.GetAllAdvInDayFromIP(await Utility.GetLocalIpAddress(),
                            AdvertiseType.Sheypoor);
                        lstMessage.Clear();
                        lstMessage.Add($"نوع آگهی: شیپور");
                        lstMessage.Add($"IP اینترنتی: {await Utility.GetLocalIpAddress()}");
                        lstMessage.Add($"GateWay: {await Utility.FindGateWay()}");
                        lstMessage.Add($"تعداد آگهی ارسال شده: {co}");

                        Utility.ShowBalloon(title, lstMessage);
                    }
                }
                //}
                //}

            }
            catch (Exception ex)
            {

            }
            finally
            {

            }

            SemaphoreSlim.Release();
        }


        public async Task RegisterAdv(AdvertiseLogBussines adv)
        {
            //try
            //{
            //    var counter = 0;
            //    adv.AdvType = AdvertiseType.Sheypoor;
            //    _driver = Utility.RefreshDriver(_driver);
            //    _driver.Navigate().GoToUrl("https://www.sheypoor.com/listing/new");
            //    await Utility.Wait();

            //    //کلیک کردن روی کتگوری اصلی
            //    _driver.FindElements(By.ClassName("form-select")).FirstOrDefault()?.Click();
            //    await Utility.Wait();

            //    //کلیک روی ساب کتگوری 1
            //    if (string.IsNullOrEmpty(adv.SubCategory1))
            //        adv.SubCategory1 = cls?.SheypoorCat1;
            //    _driver.FindElements(By.ClassName("link")).FirstOrDefault(q => q.Text == adv.SubCategory1)?.Click();

            //    await Utility.Wait();

            //    //کلیک روی ساب کتگوری2
            //    if (string.IsNullOrEmpty(adv.SubCategory2))
            //        adv.SubCategory2 = cls?.SheypoorCat2;
            //    _driver.FindElements(By.ClassName("link")).FirstOrDefault(q => q.Text == adv.SubCategory2)?.Click();


            //    //درج عکسها
            //    foreach (var item in adv.ImagesPathList)
            //    {
            //        try
            //        {
            //            //درج عکسها
            //            _driver.FindElement(By.ClassName("qq-upload-button-selector")).FindElement(By.TagName("input"))
            //                .SendKeys(item);
            //            await Utility.Wait();
            //            // break;
            //        }
            //        catch (Exception e)
            //        {
            //        }
            //    }



            //    //درج عنوان آگهی
            //    _driver.FindElement(By.Name("name")).SendKeys("");
            //    _driver.FindElement(By.Name("name")).SendKeys(adv.Title);
            //    //await Wait();
            //    //درج محتوای آگهی

            //    var thread = new Thread(() => Clipboard.SetText(adv.Content.Replace('(', '<').Replace(')', '>')));
            //    thread.SetApartmentState(ApartmentState.STA);
            //    thread.Start();

            //    var t = _driver.FindElement(By.Id("item-form-description"));
            //    t.Click();
            //    await Utility.Wait();
            //    t.SendKeys(OpenQA.Selenium.Keys.Control + "v");
            //    var thread1 = new Thread(Clipboard.Clear);
            //    thread1.SetApartmentState(ApartmentState.STA);
            //    thread1.Start();


            //    //درج قیمت
            //    var txtPrice = _driver.FindElements(By.Id("item-form-price")).Count;
            //    if (adv?.Price > 0 && txtPrice != 0)
            //    {
            //        _driver.FindElement(By.Id("item-form-price"))?.SendKeys("");
            //        _driver.FindElement(By.Id("item-form-price"))?.SendKeys(adv.Price.ToString());
            //    }

            //    await Utility.Wait();

            //    //انتخاب شهر
            //    await Utility.Wait();
            //    _driver.FindElements(By.ClassName("form-select")).LastOrDefault()?.Click();
            //    await Utility.Wait(2);
            //    var a = _driver.FindElements(By.ClassName("mode-district")).Any();
            //    if (a)
            //    {
            //        _driver.FindElement(By.ClassName("mode-district")).FindElement(By.ClassName("link"))?.Click();
            //        await Utility.Wait();
            //        _driver.FindElement(By.ClassName("mode-city")).FindElement(By.ClassName("link"))?.Click();
            //    }

            //    await Utility.Wait(1);

            //    _driver.FindElements(By.TagName("li"))?.FirstOrDefault(q => q.Text.Contains(adv.State))?.Click();
            //    await Utility.Wait(1);

            //    var cc = _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text.Contains(adv.City));
            //    cc?.Click();
            //    var cty = SheypoorCityBussines.GetAsync(adv?.City);
            //    var simGuid = await SimcardBussines.GetAsync(adv.SimCardNumber);
            //    var cityList = await SheypoorSimCityBussines.GetAllAsync(simGuid.Guid);
            //    var rand = new Random().Next(0, cityList.Count);
            //    await Utility.Wait(1);
            //    var cityGuid = !string.IsNullOrEmpty(adv?.City) ? cty.Guid : cityList[rand].Guid;

            //    var lst = await RegionBussiness.GetAllAsync(cityGuid, AdvertiseType.Sheypoor);
            //    var regionList = lst?.ToList() ?? new List<RegionBussiness>();
            //    if (regionList.Count > 0)
            //    {
            //        var rnd = new Random().Next(0, regionList.Count);
            //        var regName = regionList[rnd].Name;
            //        await Utility.Wait(2);
            //        _driver.FindElements(By.TagName("li"))?.FirstOrDefault(q => q.Text == regName)
            //            ?.Click();
            //        adv.Region = regName;
            //    }
            //    // await Wait();


            //    //کلیک روی دکمه ثبت آگهی
            //    while (_driver.Url == "https://www.sheypoor.com/listing/new")
            //    {
            //        counter++;
            //        await Utility.Wait(2);
            //        _driver.FindElements(By.TagName("button")).FirstOrDefault(q => q.Text == "ثبت آگهی")
            //            ?.Click();
            //        await Utility.Wait();
            //        var box = _driver.FindElements(By.ClassName("box")).Any(q => q.Text.Contains("حساب کاربری"));
            //        if (box) return;
            //        if (counter < 60) continue;
            //        adv.URL = "---";
            //        adv.AdvStatus = @"خطای درج";
            //        adv.StatusCode = StatusCode.InsertError;
            //        adv.AdvType = AdvertiseType.Sheypoor;
            //        adv.IP = await Utility.GetLocalIpAddress();
            //        await adv.SaveAsync();
            //        await Utility.Wait();
            //        counter = 0;
            //        _driver.Navigate().GoToUrl("https://www.sheypoor.com");
            //        return;
            //    }



            //    //اگر آگهی با موفقیت ثبت شود لینک مدیریت آگهی ذخیره می شود
            //    await Utility.Wait();
            //    await Utility.Wait(2);
            //    counter = 0;
            //    adv.URL = await MakeUrl(_driver.Url);
            //    adv.AdvStatus = @"در صف انتشار";
            //    adv.StatusCode = StatusCode.InPublishQueue;
            //    adv.AdvType = AdvertiseType.Sheypoor;
            //    adv.IP = await Utility.GetLocalIpAddress();
            //    await adv.SaveAsync();
            //    await Utility.Wait();
            //    if (!_driver.Url.Contains(adv.URL))
            //        _driver.Navigate().GoToUrl("https://www.sheypoor.com");

            //    //بعد از درج آگهی در دیتابیس لاگ می شود

            //}
            //catch (Exception ex)
            //{
            //}
            //finally { }
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

            try
            {
                if (_tokenSource.IsCancellationRequested) return;
                if (await Login(simCard))
                    _driver.Navigate().GoToUrl(url);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        public async Task<bool> Login(long simCardNumber)
        {
            try
            {
                _driver = Utility.RefreshDriver(_driver);
                var simBusiness = AdvTokensBussines.GetToken(simCardNumber, AdvertiseType.Sheypoor);
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
                        ((IJavaScriptExecutor)_driver).ExecuteScript(@"alert('لاگین انجام شد');");
                        await Utility.Wait();
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

                    var a = await SimcardBussines.GetAsync(simCardNumber);
                    var name = a.OwnerName;
                    var message = $@"مالک: {name} \r\nشماره: {simCardNumber}  \r\nلطفا لاگین نمائید ";
                    ((IJavaScriptExecutor)_driver).ExecuteScript($"alert('{message}');");
                    //Wait();

                    await Utility.Wait(3);
                    try
                    {
                        _driver.SwitchTo().Alert().Accept();
                        await Utility.Wait(3);
                        repeat++;
                    }
                    catch
                    {
                        await Utility.Wait(15);
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
                            simBusiness = new AdvTokensBussines()
                            {
                                Type = AdvertiseType.Sheypoor,
                                Token = token,
                                Number = simCardNumber,
                                Guid = Guid.NewGuid(),
                                DateSabt = DateConvertor.M2SH(DateTime.Now),
                                Status = true
                            };
                        }

                        await simBusiness.SaveAsync();
                        ((IJavaScriptExecutor)_driver).ExecuteScript(@"alert('لاگین انجام شد');");
                        await Utility.Wait();
                        _driver.SwitchTo().Alert().Accept();
                        return true;
                    }
                }

                var linksElements1 = _driver?.FindElements(By.TagName("a")).FirstOrDefault(q => q.Text == "خروج") ??
                                     null;
                if (linksElements1 == null)
                {
                    var msg = $@"فرصت لاگین کردن به اتمام رسید. لطفا دقایقی بعد مجددا امتحان نمایید";
                    ((IJavaScriptExecutor)_driver).ExecuteScript($"alert('{msg}');");
                    _driver.SwitchTo().Alert().Accept();
                    await Utility.Wait(3);
                }

                _driver.Navigate().GoToUrl("https://www.sheypoor.com");
                await Utility.Wait();
                if (simBusiness == null) return false;
                simBusiness.Token = null;
                await simBusiness.SaveAsync();

                return false;

            }
            catch (WebException) { return false; }
            catch (Exception ex)
            {
                return false;
            }

            finally { }
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
        private async Task<AdvertiseLogBussines> GetNextAdv(long simCardNumber)
        {
            var newAdvertiseLogBusiness = new AdvertiseLogBussines();
            try
            {
                newAdvertiseLogBusiness.SimCardNumber = simCardNumber;

                //لیست آگهی های سیمکارت
                var simGuid = await SimcardBussines.GetAsync(simCardNumber);
                var advList = await SimcardAdsBussines.GetAllAsync(simGuid.Guid);
                advList = advList.ToList();

                #region findNextAdvIndex
                AdvertiseList = new List<Advertise>();
                foreach (var item in advList)
                {
                    var adv = await Advertise.GetAsync(Path.Combine(cls?.AdsAddress ?? "", item.AdsName),
                        cls?.AdsAddress);
                    AdvertiseList.Add(adv);
                }

                var nextAdvIndex = new Random().Next(AdvertiseList.Count);
                #endregion

                string path = null;
                if (Path.Combine(cls?.AdsAddress ?? "", AdvertiseList[nextAdvIndex].AdvName) ==
                    AdvertiseList[nextAdvIndex].AdvName)
                    path = Path.Combine(AdvertiseList[nextAdvIndex].RootPath,
                        AdvertiseList[nextAdvIndex].AdvName);
                else
                    path = Path.Combine(cls?.AdsAddress, AdvertiseList[nextAdvIndex].AdvName);
                newAdvertiseLogBusiness.Adv = path;

                #region FindNextTitle

                //تایتل آگهی دریافت می شود
                // if (!(AdvertiseList[nextAdvIndex].Titles?.Count > 0)) return null;
                while (string.IsNullOrEmpty(newAdvertiseLogBusiness.Title) || newAdvertiseLogBusiness.Title == "---")
                {
                    var nextTitleIndex = new Random(DateTime.Now.Millisecond).Next(AdvertiseList[nextAdvIndex].Titles.Count);
                    newAdvertiseLogBusiness.Title = AdvertiseList[nextAdvIndex].Titles[nextTitleIndex];
                }
                //if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Title)) return null;
                #endregion

                #region GetContent
                //کانتنت آگهی دریافت می شود

                while (string.IsNullOrEmpty(newAdvertiseLogBusiness.Content) || newAdvertiseLogBusiness.Content == "---")
                {
                    newAdvertiseLogBusiness.Content = AdvertiseList[nextAdvIndex].Content;
                }

                // if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Content)) return null;

                #endregion

                #region FindImages

                while (newAdvertiseLogBusiness.ImagesPathList == null || newAdvertiseLogBusiness.ImagesPathList.Count == 0)
                {
                    //عکسهای آگهی دریافت می شود
                    newAdvertiseLogBusiness.ImagesPathList =
                        GetNextImages(newAdvertiseLogBusiness.Adv, cls?.SheypoorMaxImgCount ?? 3);
                    if (newAdvertiseLogBusiness.ImagesPathList.Count > 0)
                    {
                        newAdvertiseLogBusiness.ImagePath = "";
                        foreach (var item in newAdvertiseLogBusiness.ImagesPathList)
                        {
                            newAdvertiseLogBusiness.ImagePath += item + "\r\n ";
                        }
                    }
                }


                #endregion

                //قیمت آگهی دریافت می شود
                newAdvertiseLogBusiness.Price = AdvertiseList[nextAdvIndex].Price;

                while (string.IsNullOrEmpty(newAdvertiseLogBusiness.City) || newAdvertiseLogBusiness.City == "---")
                {
                    var Allcity = await SheypoorSimCityBussines.GetAllAsync(simGuid.Guid);
                    var rand = new Random().Next(0, Allcity.Count);
                    var city = Allcity[rand];
                    var cc = SheypoorCityBussines.GetAsync(city.CityGuid);
                    newAdvertiseLogBusiness.City = cc?.Name;
                    newAdvertiseLogBusiness.State = cc?.StateName;
                }
                return newAdvertiseLogBusiness;
            }
            catch (Exception e)
            {
                FarsiMessegeBox.Show(e.Message);
                return null;
            }
        }

        public async Task<List<string>> GetAllRegionFromSheypoor(string State, string City)
        {
            var region = new List<string>();
            string Name = "";
            _driver.Navigate().GoToUrl("https://Sheypoor.com/listing/new");
            try
            {
                _driver.FindElements(By.ClassName("form-select")).LastOrDefault()?.Click();
                await Utility.Wait(1);
                _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text == State)?.Click();
                await Utility.Wait(1);
                _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text == City)?.Click();
                await Utility.Wait(1);
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
                return resultImages;
            }
            finally { }
        }
        public static async Task<SheypoorAdv> GetInstance()
        {
            await GetDataFromSetting();
            return _me ?? (_me = new SheypoorAdv());
        }

        private static async Task<SettingBussines> GetDataFromSetting()
        {
            try
            {
                var res = SettingBussines.GetAll();
                cls = res.Count == 0 ? new SettingBussines() : res[0];
                return cls;
            }
            catch (Exception ex)
            {
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
                _driver = Utility.RefreshDriver(_driver);
                if (dayCount == 0)
                    dayCount = cls?.SheypoorDayCountForUpdateState ?? 10;

                var lastWeek = DateTime.Now.AddDays(-dayCount);
                var lst = await AdvertiseLogBussines.GetAllAsync();
                lst = lst.Where(p =>
                    p.DateM > lastWeek && p.AdvType == AdvertiseType.Sheypoor).ToList();
                var allAdvertiseLog = lst.OrderByDescending(q => q.DateSabt).ToList();
                if (allAdvertiseLog.Count <= 0) return true;
                var tryCount = 0;
                long mobile = 0;
                foreach (var adv in allAdvertiseLog)
                {
                    if (_tokenSource.IsCancellationRequested) break;
                    if (tryCount >= 3) continue;
                    try
                    {
                        var sim = await SimcardBussines.GetAsync(adv.SimCardNumber);
                        if (mobile != adv.SimCardNumber)
                        {
                            var ls = AdvTokensBussines.GetToken(adv.SimCardNumber, AdvertiseType.Sheypoor);
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
                        await Utility.Wait();
                        var el = _driver.FindElements(By.TagName("img")).Any(q =>
                            q.GetAttribute("src").Contains("/img/empty-state/mylistings.png"));
                        if (el)
                        {
                            adv.AdvStatus = "رد شده";
                            adv.StatusCode = StatusCode.Failed;
                            await adv.SaveAsync();
                            continue;
                        }

                        var element = _driver.FindElements(By.Id("listing-" + code)).Any();
                        await Utility.Wait();

                        if (!element || string.IsNullOrEmpty(code))
                        {
                            adv.AdvStatus = "رد شده";
                            adv.StatusCode = StatusCode.Failed;
                            await adv.SaveAsync();
                            continue;
                        }

                        _driver.FindElement(By.Id("listing-" + code))?.Click();
                        await Utility.Wait();
                        adv.AdvStatus = "منتشر شده";
                        //var counter = _driver.FindElement(By.ClassName("total-view"))?.FindElement(By.TagName("strong"))
                        //                  ?
                        //                  .Text.FixString() ?? "0";
                        var counter = _driver.FindElement(By.ClassName("stat-view"))?.Text.ToString() ?? "0";
                        adv.VisitCount = int.Parse(counter);
                        adv.StatusCode = StatusCode.Published;
                        adv.AdvType = AdvertiseType.Sheypoor;
                        await adv.SaveAsync();
                        tryCount = 0;
                        _driver.Navigate().Back();
                        await Utility.Wait();
                    }
                    catch (Exception ex)
                    {
                        await Utility.Wait();
                        tryCount++;
                    }
                }



                return true;
            }
            catch (Exception ex)
            {
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
                return null;
            }
        }


        public async Task<List<SheypoorCityBussines>> GetAllCityFromSheypoor()
        {
            var cities = new List<SheypoorCityBussines>();
            var states = await StateBussiness.GetAllAsync();
            _driver = Utility.RefreshDriver(_driver);
            _driver.Navigate().GoToUrl("https://www.sheypoor.com");
            try
            {
                _driver.FindElements(By.ClassName("form-select")).LastOrDefault()?.Click();

                foreach (var stateItem in states)
                {
                    _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text == stateItem.Name)?.Click();


                    var cc = _driver.FindElements(By.TagName("span"))
                        .Where(d => d.GetAttribute("class").Contains("t-city")).ToList();
                    foreach (var item in cc)
                    {
                        if (item.Text == "") continue;
                        var a = new SheypoorCityBussines
                        {
                            Guid = Guid.NewGuid(),
                            Name = item.Text,
                            DateSabt = DateConvertor.M2SH(DateTime.Now),
                            Status = true
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

        public async Task GetChatCount(List<SimcardBussines> simcard)
        {
            try
            {
                foreach (var sim in simcard)
                {
                    var log = await Login(sim.Number);
                    if (!log) continue;
                    await Utility.Wait();
                    var q = _driver.FindElements(By.TagName("small")).ToList();
                    if (q.Count == 0) continue;
                    await Utility.Wait();
                    _driver.Navigate().GoToUrl("https://www.sheypoor.com/session/myChats");
                    await Utility.Wait(2);
                    var news = _driver.FindElements(By.ClassName("badge"));
                    if (news.Count == 0) continue;
                    foreach (var item in news)
                    {
                        try
                        {
                            await Utility.Wait(2);
                            //if (string.IsNullOrEmpty(item.Text)) continue;
                            while (_driver.Url.Contains("https://www.sheypoor.com/session/myChats"))
                            {
                                await Utility.Wait();
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        private List<string> lstMessage = new List<string>();
        //public async Task SendChat(List<string> msg, int count, string city, string cat1, string cat2, string cat3)
        //{
        //    try
        //    {
        //        if (SemaphoreSlim.CurrentCount == 0)
        //        {
        //            var result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
        //                MessageBoxIcon.Question);
        //            if (result == DialogResult.Yes)
        //                _tokenSource?.Cancel();
        //            else return;
        //        }

        //        var all = await SimcardBussines.GetAllAsync();
        //        for (var k = 0; k <= all.Count; k++)
        //        {
        //            var sim = await SimcardBussines.GetNextSimForChat(AdvertiseType.Sheypoor);
        //            if (sim == 0) return;
        //            var simbus = await SimcardBussines.GetAsync(sim);
        //            await simbus.SaveAsync();
        //            var log = await Login(sim);
        //            if (!log) return;
        //            _driver.Navigate().GoToUrl("https://www.sheypoor.com");
        //            await Utility.Wait();
        //            _driver.FindElements(By.ClassName("pull-left")).FirstOrDefault(q => q.Text == "همه آگهی‌ها")?.Click();
        //            await Utility.Wait();
        //            if (!string.IsNullOrEmpty(cat1))
        //            {
        //                _driver.FindElements(By.ClassName("form-select")).FirstOrDefault()?.Click();
        //                await Utility.Wait();
        //                if (string.IsNullOrEmpty(cat2))
        //                {
        //                    _driver.FindElements(By.TagName("strong")).FirstOrDefault(q => q.Text == cat1)?.Click();
        //                    await Utility.Wait();
        //                }
        //                else
        //                {
        //                    _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text == cat2)
        //                        ?.Click();
        //                    await Utility.Wait();
        //                }
        //            }

        //            if (!string.IsNullOrEmpty(city))
        //            {
        //                _driver.FindElements(By.ClassName("form-select")).LastOrDefault()?.Click();
        //                await Utility.Wait();
        //                var citybus = await SheypoorCityBussines.GetAsync(city);
        //                var statebus = await StateBussiness.GetAsync(citybus.StateGuid);
        //                _driver.FindElements(By.TagName("li"))?.FirstOrDefault(q => q.Text.Contains(statebus.StateName))
        //                    ?.Click();
        //                await Utility.Wait(1);
        //                var cc = _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text.Contains(city));
        //                cc?.Click();
        //                await Utility.Wait(1);
        //            }
        //            var ins = 0;
        //            var name = new List<string>();
        //            var isMatch = false;
        //            var j = 0;
        //            for (var i = 0; ins <= count; i++)
        //            {
        //                if (i >= 24 && i % 24 == 0)
        //                {
        //                    _driver.FindElements(By.ClassName("button")).LastOrDefault(t => t.Text == "بعدی")
        //                        ?.Click();
        //                    await Utility.Wait(2);
        //                    j = 0;
        //                }

        //                var elp = _driver.FindElements(By.ClassName("serp-item"))[j];
        //                if (name.Count <= 0)
        //                    name.Add(elp?.Text);
        //                foreach (var item in name)
        //                {
        //                    if (name.Count == 1)
        //                        break;
        //                    if (elp?.Text == item)
        //                    {
        //                        isMatch = true;
        //                        break;
        //                    }
        //                    else isMatch = false;
        //                }

        //                if (isMatch) continue;
        //                name.Add(elp?.Text);
        //                var rnd = new Random().Next(0, msg.Count);
        //                var send = await SendChat(elp, msg[rnd]);
        //                if (!send) continue;
        //                j++;
        //                ins++;
        //                await Utility.Wait();
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}

        private async Task<bool> SendChat(IWebElement elp, string msg)
        {
            try
            {
                elp?.Click();
                await Utility.Wait();
                var el = _driver.FindElements(By.TagName("span"))
                    .Any(q => q.Text.Contains("چت با"));
                if (!el)
                {
                    _driver.Navigate().Back();
                    return false;
                }
                _driver.FindElements(By.TagName("span"))
                    .FirstOrDefault(q => q.Text.Contains("چت با"))?.Click();
                await Utility.Wait(1);
                var inp = _driver.FindElements(By.TagName("input")).Any();
                while (!inp)
                {
                    await Utility.Wait();
                    inp = _driver.FindElements(By.TagName("input")).Any();
                }

                await Utility.Wait(3);
                _driver.FindElement(By.TagName("input")).SendKeys(msg + '\n');
                await Utility.Wait(1);
                _driver.Navigate().Back();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task GetCategory()
        {
            try
            {
                _driver = Utility.RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://www.sheypoor.com/listing/new");
                await Utility.Wait();
                _driver.FindElements(By.ClassName("form-select")).FirstOrDefault()?.Click();
                await Utility.Wait();
                var listCat = _driver.FindElements(By.TagName("ul")).Where(q => !string.IsNullOrEmpty(q.Text))
                    .ToList();
                listCat = listCat[3].FindElements(By.TagName("li")).ToList();
                var listAll = await AdvCategoryBussines.GetAllAsync();
                listAll = listAll.Where(q => q.Type == AdvertiseType.Sheypoor).ToList();
                if (listAll.Count > 0)
                    AdvCategoryBussines.RemoveAllAsync(listAll);
                foreach (var item in listCat)
                {
                    var a = new AdvCategoryBussines()
                    {
                        Guid = Guid.NewGuid(),
                        Type = AdvertiseType.Sheypoor,
                        Name = item.Text.Trim(),
                        DateSabt = DateConvertor.M2SH(DateTime.Now),
                        ParentGuid = Guid.Empty,
                        Status = true
                    };
                    await a.SaveAsync();
                }
                listAll = await AdvCategoryBussines.GetAllAsync();
                listAll = listAll.Where(q => q.Type == AdvertiseType.Sheypoor).ToList();
                if (listAll.Count <= 0) return;
                foreach (var element in listAll)
                {
                    _driver.FindElements(By.ClassName("link")).FirstOrDefault(q => q.Text == element.Name)?.Click();
                    await Utility.Wait();
                    var listCat2 = _driver.FindElements(By.TagName("ul")).Where(q => !string.IsNullOrEmpty(q.Text))
                        .ToList();
                    listCat2 = listCat2[3].FindElements(By.TagName("li")).ToList();
                    foreach (var item in listCat2)
                    {
                        var a = new AdvCategoryBussines()
                        {
                            Guid = Guid.NewGuid(),
                            Type = AdvertiseType.Sheypoor,
                            Name = item.Text.Trim(),
                            DateSabt = DateConvertor.M2SH(DateTime.Now),
                            ParentGuid = element.Guid,
                            Status = true
                        };
                        await a.SaveAsync();
                    }


                    var newList = await AdvCategoryBussines.GetAllAsync(element.Guid, AdvertiseType.Sheypoor);
                    if (newList.Count <= 0) continue;
                    _driver.FindElements(By.TagName("span")).FirstOrDefault(q => q.Text.Contains("بازگشت"))?.Click();
                    await Utility.Wait();
                }
            }
            catch (Exception ex)
            {
                FarsiMessegeBox.Show(ex.Message);
            }
        }

    }
}
