using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinesLayer;
using DataLayer;
using DataLayer.Enums;
using OpenQA.Selenium;
using Cookie = OpenQA.Selenium.Cookie;


namespace Ads.Classes
{
    public class DivarAdv
    {
        #region Fields
        private IWebDriver _driver;
        public int MaxImgForAdv { get; }
        //private readonly string _advRootPath;
        private static SettingBussines clsSetting;
        public string AdvRootPath => Path.Combine(Application.StartupPath, "Advertise");

        public List<Advertise> AdvertiseList;
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
        private static async Task<SettingBussines> GetDataFromSetting()
        {
            try
            {
                var res = await SettingBussines.GetAllAsync();
                clsSetting = res.Count == 0 ? new SettingBussines() : res[0];
                return clsSetting;
            }
            catch (Exception ex)
            {
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

            _driver = Utility.RefreshDriver(_driver);
        }

        #endregion
        #region MyRegion

        private List<string> lstMessage = new List<string>();
        public async Task StartRegisterAdv()
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
            var isLogin = false;
            try
            {
                counter = 0;
                SimcardBussines firstSimCardBusiness = null;
                _driver = Utility.RefreshDriver(_driver);
                while (!await Utility.PingHost("www.google.com"))
                {
                    //خطا در برقراری اتصال به اینترنت
                }

                var insert = false;
                while (!false)
                {
                    var simCard = await SimcardBussines.GetNextSimCardNumberAsync(AdvertiseType.Divar);
                    //کنترل شماره خروجی
                    if (simCard == 0)
                    {
                        lstMessage.Clear();
                        lstMessage.Add("سیستم در حال تعویض IP یا سایت می باشد");
                        Utility.ShowBalloon("پر شدن تعداد آگهی در " + await Utility.FindGateWay(), lstMessage);
                        return;
                    }



                    //کنترل تعداد آگهی ارسال شده در هر IP
                    if (clsSetting?.CountAdvInIPDivar <=
                               AdvertiseLogBussines.GetAllAdvInDayFromIP(await Utility.GetLocalIpAddress(),
                                   AdvertiseType.Divar))

                    {
                        lstMessage.Clear();
                        lstMessage.Add("سیستم در حال تعویض IP یا سایت می باشد");
                        Utility.ShowBalloon("پر شدن تعداد آگهی در " + await Utility.FindGateWay(), lstMessage);
                        return;
                    }





                    firstSimCardBusiness = await SimcardBussines.GetAsync(simCard);
                    if (firstSimCardBusiness is null) continue;

                    var lastUseDivar = firstSimCardBusiness.NextUseDivar;

                    var tt = AdvTokensBussines.GetToken(simCard, AdvertiseType.Divar);
                    var hasToken = tt?.Token ?? null;
                    if (string.IsNullOrEmpty(hasToken))
                    {
                        firstSimCardBusiness.NextUseDivar = DateTime.Now.AddMinutes(30);
                        await firstSimCardBusiness.SaveAsync();
                        continue;
                    }


                    var card1 = simCard;
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
                    if (await Login(simCard) == false)
                    {
                        firstSimCardBusiness.NextUseDivar = lastUseDivar;
                        await firstSimCardBusiness.SaveAsync();
                        continue;
                    }
                    var registeredAdvCount = a1.Count;
                    if (registeredAdvCount >= clsSetting?.CountAdvInMounthDivar)
                    {
                        //تاریخ روز اول ماه شمسی بعد را تنظیم می کند چون تا سر ماه بعد دیگر نیازی به این سیم کارت نیست
                        firstSimCardBusiness.NextUseDivar = startDayOfNextMonthOfDateShToMiladi;
                        await firstSimCardBusiness.SaveAsync();
                        continue;
                    }

                    //آمار آگهی های ثبت شده امروز
                    var currentDate = DateTime.Now.Date;
                    var a2 = await AdvertiseLogBussines.GetAllAsync();
                    a2 = a2.Where(p =>
                        p.SimCardNumber == card1 && p.AdvType == AdvertiseType.Divar
                                                 && (p.StatusCode == StatusCode.Published
                                                     || p.StatusCode == StatusCode.InPublishQueue
                                                     || p.StatusCode == StatusCode.WaitForPayment)
                                                 && p.DateM >= currentDate).ToList();
                    registeredAdvCount = a2.Count;
                    if (registeredAdvCount >= clsSetting?.CountAdvInDayDivar)
                    {
                        //تاریخ فردا رو ست می کند چون تا فردا دیگه نیازی به این سیم کارت نیست
                        firstSimCardBusiness.NextUseDivar = DateTime.Today.AddDays(1);
                        await firstSimCardBusiness.SaveAsync();
                        continue;
                    }

                    //اینجا به تعداد تنظیم شده در تنظیمات دیوار منهای تعداد ثبت شده قبلی، آگهی درج می کند
                    // for (var i = 0; i < clsSetting?.CountAdvInDayDivar - registeredAdvCount; i++)
                    //{
                    var adv = await GetNextAdv(simCard);
                    if (adv == null) continue;
                    await RegisterAdv(adv);
                    await Utility.Wait(1);
                    var title = "تعداد آگهی های ارسال شده با " + await Utility.FindGateWay();
                    var body =
                        AdvertiseLogBussines.GetAllAdvInDayFromIP(await Utility.GetLocalIpAddress(),
                            AdvertiseType.Divar);
                    lstMessage.Clear();
                    lstMessage.Add(body.ToString());
                    Utility.ShowBalloon(title, lstMessage);
                    insert = true;
                    //}
                }


                await Utility.Wait(10);
                lstMessage.Clear();
                lstMessage.Add("خطای اتصال به شبکه");
                Utility.ShowBalloon("لطفا اتصال به شبکه را چک نمایید", lstMessage);



            }
            catch (Exception ex)
            {

            }

            SemaphoreSlim.Release();

        }

        public async Task<bool> Login(long simCardNumber)
        {
            try
            {
                _driver = Utility.RefreshDriver(_driver);
                if (!_driver.Url.Contains("divar.ir"))
                    _driver.Navigate().GoToUrl("https://divar.ir");

                var simBusiness = AdvTokensBussines.GetToken(simCardNumber, AdvertiseType.Divar);
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
                    await Utility.Wait();
                    _driver.FindElement(By.ClassName("login-message__login-btn")).Click();
                    await Utility.Wait();
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
                            simBusiness = new AdvTokensBussines() { Guid = Guid.NewGuid() };
                        }

                        simBusiness.Token = tokenInDatabase;
                        simBusiness.DateSabt = DateConvertor.M2SH(DateTime.Now);
                        simBusiness.Type = AdvertiseType.Divar;
                        simBusiness.Number = simCardNumber;
                        simBusiness.Status = true;

                        await simBusiness.SaveAsync();
                        ((IJavaScriptExecutor)_driver).ExecuteScript(@"alert('لاگین انجام شد');");
                        await Utility.Wait(2);
                        _driver.SwitchTo().Alert().Accept();
                        return true;
                    }
                    else
                    {
                        var a = await SimcardBussines.GetAsync(simCardNumber);
                        var name = a.OwnerName;
                        var message = $@"مالک: {name} \r\nشماره: {simCardNumber}  \r\nلطفا لاگین نمائید ";
                        ((IJavaScriptExecutor)_driver).ExecuteScript($"alert('{message}');");

                        await Utility.Wait(3);
                        try
                        {
                            _driver.SwitchTo().Alert().Accept();
                            await Utility.Wait(3);
                            repeat++;
                        }
                        catch
                        {
                            await Utility.Wait(10);
                        }
                    }
                }

                return false;
            }
            catch (WebException) { return false; }
            catch (Exception ex)
            {
                return false;
            }

            finally
            {

            }
        }
        public async Task<bool> LoginChat(long simCardNumber)
        {
            try
            {
                _driver = Utility.RefreshDriver(_driver);
                if (!_driver.Url.Contains("https://chat.divar.ir/"))
                    _driver.Navigate().GoToUrl("https://chat.divar.ir/");

                var simBusiness = AdvTokensBussines.GetToken(simCardNumber, AdvertiseType.DivarChat);
                var tokenInDatabase = simBusiness?.Token ?? null;
                //if (string.IsNullOrEmpty(tokenInDatabase)) return false;

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
                    await Utility.Wait();
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
                            simBusiness = new AdvTokensBussines()
                            {
                                Guid = Guid.NewGuid(),
                                Token = tokenInDatabase,
                                Type = AdvertiseType.DivarChat,
                                Number = simCardNumber,
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
                    else
                    {
                        var a = await SimcardBussines.GetAsync(simCardNumber);
                        var name = a.OwnerName;
                        var message = $@"مالک: {name} \r\nشماره: {simCardNumber}  \r\nلطفا لاگین نمائید ";
                        ((IJavaScriptExecutor)_driver).ExecuteScript($"alert('{message}');");

                        await Utility.Wait(3);
                        try
                        {
                            _driver.SwitchTo().Alert().Accept();
                            await Utility.Wait(3);
                            repeat++;
                        }
                        catch
                        {
                            await Utility.Wait(10);
                        }
                    }
                }

                return false;
            }
            catch (WebException) { return false; }
            catch (Exception ex)
            {
                return false;
            }

        }
        private async Task RegisterAdv(AdvertiseLogBussines adv)
        {
            try
            {
                adv.AdvType = AdvertiseType.Divar;
                _driver = Utility.RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://divar.ir/new");

                //کلیک کردن روی کتگوری اصلی
                var a1 = _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text == clsSetting?.DivarCat1);
                adv.Category = a1?.Text;
                a1?.Click();

                await Utility.Wait(1);
                //کلیک روی ساب کتگوری 1
                var a2 = _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text.Contains("تجهیزات"));
                adv.SubCategory1 = a2?.Text;
                a2?.Click();
                await Utility.Wait(1);
                //کلیک روی ساب کتگوری2
                var a3 = _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text == clsSetting?.DivarCat3);
                adv.SubCategory2 = a3?.Text;
                a3?.Click();

                await Utility.Wait(1);
                foreach (var item in adv.ImagesPathList)
                {
                    //درج عکسها
                    _driver.FindElement(By.ClassName("image-uploader__dropzone")).FindElement(By.TagName("input")).SendKeys(item);
                    await Utility.Wait();
                }



                if (_driver.FindElements(By.ClassName("location-selector__city")).Count <= 0)
                {
                    return;
                }

                _driver.FindElement(By.ClassName("location-selector__city")).FindElement(By.TagName("input")).SendKeys(adv.City + "\n");

                await Utility.Wait();
                var el = _driver.FindElements(By.ClassName("location-selector__district")).Any();
                await Utility.Wait();
                if (el)
                {
                    var cty = DivarCityBussines.GetAsync(adv?.City);
                    await Utility.Wait(1);
                    var cityGuid = cty.Guid;
                    var lst = await RegionBussiness.GetAllAsync(cityGuid, AdvertiseType.Divar);
                    var regionList = lst?.ToList() ?? new List<RegionBussiness>();
                    if (regionList.Count > 0)
                    {
                        var rnd = new Random().Next(0, regionList.Count);
                        var regName = regionList[rnd].Name;
                        await Utility.Wait(2);


                        _driver.FindElement(By.ClassName("location-selector__district")).FindElement(By.TagName("input")).SendKeys(regName + "\n");
                        adv.Region = regName;
                    }
                }

                //درج قیمت
                if (adv.Price > 0) _driver.FindElement(By.CssSelector("input[type=tel]")).SendKeys(adv.Price.ToString());
                await Utility.Wait();
                //درج عنوان آگهی
                _driver.FindElements(By.CssSelector("input[type=text]")).Last().SendKeys(adv.Title);
                await Utility.Wait();
                //درج محتوای آگهی
                var thread = new Thread(() => Clipboard.SetText(adv.Content.Replace('(', '<').Replace(')', '>')));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

                var t = _driver.FindElement(By.TagName("textarea"));
                t.Click();
                await Utility.Wait();
                t.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                var thread1 = new Thread(Clipboard.Clear);
                thread1.SetApartmentState(ApartmentState.STA);
                thread1.Start();
                //_driver.FindElement(By.TagName("textarea")).SendKeys(adv.Content.Replace('(', '<').Replace(')', '>'));
                await Utility.Wait();

                await Utility.Wait();

                var loadImg = _driver.FindElements(By.ClassName("image-item__progress")).ToList();
                while (loadImg.Count > 0)
                {
                    await Utility.Wait(2);
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
                await Utility.Wait(1);
                //if (_driver.Url.Contains("manage"))
                //{
                adv.URL = _driver.Url;
                adv.StatusCode = StatusCode.InPublishQueue;
                adv.IP = await Utility.GetLocalIpAddress();
                adv.AdvStatus = "در صف انتشار";
                await adv.SaveAsync();
            }
            catch (Exception ex)
            {
            }
        }
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
                    var adv = await Advertise.GetAsync(Path.Combine(clsSetting?.DivarPicPath, item.AdsName),
                        clsSetting?.DivarPicPath);
                    AdvertiseList.Add(adv);
                }

                var nextAdvIndex = new Random().Next(AdvertiseList.Count);
                #endregion

                string path = null;
                if (Path.Combine(clsSetting?.DivarPicPath, AdvertiseList[nextAdvIndex].AdvName) ==
                    AdvertiseList[nextAdvIndex].AdvName)
                    path = Path.Combine(AdvertiseList[nextAdvIndex].RootPath,
                        AdvertiseList[nextAdvIndex].AdvName);
                else
                    path = Path.Combine(clsSetting?.DivarPicPath, AdvertiseList[nextAdvIndex].AdvName);
                newAdvertiseLogBusiness.Adv = path;
                #region FindNextTitle


                //تایتل آگهی دریافت می شود
                if (!(AdvertiseList[nextAdvIndex].Titles?.Count > 0)) return null;

                var nextTitleIndex = new Random(DateTime.Now.Millisecond).Next(AdvertiseList[nextAdvIndex].Titles.Count);
                newAdvertiseLogBusiness.Title = AdvertiseList[nextAdvIndex].Titles[nextTitleIndex];


                if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Title)) return null;
                #endregion

                #region GetContent
                //کانتنت آگهی دریافت می شود

                newAdvertiseLogBusiness.Content = AdvertiseList[nextAdvIndex].Content;

                if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Content)) return null;

                #endregion

                #region FindImages
                //عکسهای آگهی دریافت می شود
                newAdvertiseLogBusiness.ImagesPathList =
                    GetNextImages(newAdvertiseLogBusiness.Adv, clsSetting?.DivarMaxImgCount ?? 3);
                if (newAdvertiseLogBusiness.ImagesPathList.Count > 0)
                {
                    newAdvertiseLogBusiness.ImagePath = "";
                    foreach (var item in newAdvertiseLogBusiness.ImagesPathList)
                    {
                        newAdvertiseLogBusiness.ImagePath += item + "\r\n ";
                    }
                }

                #endregion

                //قیمت آگهی دریافت می شود
                newAdvertiseLogBusiness.Price = AdvertiseList[nextAdvIndex].Price;
                var Allcity = await DivarSimCityBussines.GetAllAsync(simGuid.Guid);
                var rand = new Random().Next(0, Allcity.Count);
                var city = Allcity[rand];
                var cc = DivarCityBussines.GetAsync(city.CityGuid);
                newAdvertiseLogBusiness.City = cc?.Name;
                newAdvertiseLogBusiness.State = "";


                return newAdvertiseLogBusiness;
            }
            catch (Exception ex)
            {

                return null;
            }


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

                // MessageBox.Show(@"GetNextImages:" + ex.Message);
                return resultImages;
            }
        }




        public async Task<List<RegionBussiness>> GetAllRegionFromDivar(List<string> City)
        {
            var region = new List<RegionBussiness>();
            _driver = Utility.RefreshDriver(_driver);
            _driver.Navigate().GoToUrl("https://divar.ir/new");
            //کلیک کردن روی کتگوری اصلی
            _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text == clsSetting?.DivarCat1)?.Click();
            await Utility.Wait();
            //کلیک روی ساب کتگوری 1
            _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text.Contains("تجهیزات"))?.Click();
            await Utility.Wait();
            //کلیک روی ساب کتگوری2
            _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text == clsSetting?.DivarCat3)?.Click();

            await Utility.Wait();
            try
            {
                foreach (var item in City)
                {
                    _driver.FindElement(By.ClassName("location-selector__city")).FindElement(By.TagName("input"))
                        .SendKeys(item + "\n");
                    await Utility.Wait(2);
                    var el = _driver.FindElement(By.ClassName("location-selector__district"))
                        .FindElement(By.TagName("i"));
                    await Utility.Wait();
                    el?.Click();
                    var allEl = _driver.FindElement(By.ClassName("location-selector__district"))
                        .FindElements(By.ClassName("item")).Where(q => q.Text != "").ToList();
                    if (allEl.Count <= 0) continue;
                    foreach (var temp in allEl)
                    {
                        var a = DivarCityBussines.GetAsync(item);
                        var clsRegionBusiness = new RegionBussiness()
                        {
                            Guid = Guid.NewGuid(),
                            CityGuid = a.Guid,
                            DateSabt = DateConvertor.M2SH(DateTime.Now),
                            Type = AdvertiseType.Divar,
                            Name = temp.Text,
                            Status = true
                        };
                        region.Add(clsRegionBusiness);
                    }

                }
            }
            catch (Exception e)
            {
                region = null;
            }

            return region;
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
                    _driver = Utility.RefreshDriver(_driver);
                    if (dayCount == 0)
                        dayCount = clsSetting?.DivarDayCountForUpdateState ?? 10;
                    var lastWeek = DateTime.Now.AddDays(-dayCount);
                    var lst = await AdvertiseLogBussines
                        .GetAllAsync();
                    lst = lst.Where(p => p.DateM > lastWeek && p.URL.Contains("manage")).ToList();
                    var allAdvertiseLog = lst.OrderByDescending(p => p.DateSabt).ToList();
                    if (allAdvertiseLog.Count <= 0) return true;
                    var tryCount = 0;
                    foreach (var adv in allAdvertiseLog)
                    {
                        if (TokenSource.IsCancellationRequested) break;
                        if (tryCount >= 3) continue;
                        try
                        {
                            _driver.Navigate().GoToUrl((string)adv.URL);
                            await Utility.Wait();
                            var element = _driver.FindElement(By.ClassName("manage-header__status"));
                            if (element == null) continue;
                            adv.AdvStatus = element.Text;
                            element = _driver.FindElement(By.ClassName("manage-header__description"));
                            if (element == null) continue;
                            adv.StatusCode = GetAdvStatusCodeByStatus(adv.AdvStatus);
                            //بروزرسانی آمار بازدید منتشر شده ها
                            if (adv.StatusCode == StatusCode.Published)
                            {
                                var visitCountEl = _driver.FindElement(By.ClassName("post-stats__summary"));
                                if (visitCountEl != null && visitCountEl.Text.Length > 11)
                                {
                                    int.TryParse(visitCountEl.Text.Substring(11).Trim(), out var cnt);
                                    adv.VisitCount = cnt;
                                }
                            }

                            await adv.SaveAsync();
                            tryCount = 0;
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
                    SemaphoreSlim.Release();
                }
            }

        }
        public async Task<bool> UpdateAdvStatus(AdvertiseLogBussines adv)
        {
            return true;
            //try
            //{
            //    _driver = Utility.RefreshDriver(_driver);
            //    if (!adv.URL.Contains("manage")) return false;
            //    if (_driver.Url != adv.URL)
            //        _driver.Navigate().GoToUrl(adv.URL);
            //    await Utility.Wait();
            //    adv.AdvType = AdvertiseType.Divar;
            //    adv.StatusCode = (short)GetAdvStatusCodeByStatus(adv.AdvStatus);
            //    await adv.SaveAsync();
            //    return true;
            //}
            //catch
            //{
            //    return false;
            //}
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
                    AdvertiseLogBussines.GetAllAsync();
                allAdvertiseLog = allAdvertiseLog.Where(p => p.StatusCode == StatusCode.Published).ToList();
                if (allAdvertiseLog.Count > 0)
                {
                    _driver = Utility.RefreshDriver(_driver);
                    foreach (var adv in allAdvertiseLog)
                    {
                        if (TokenSource.IsCancellationRequested) break;
                        if (adv.URL.Contains("manage"))
                        {
                            _driver.Navigate().GoToUrl(adv.URL);
                            await Utility.Wait();
                            //post-stats__summary = بازدید کلی: ۱۸
                            //post-stats__bar
                            var visitElement = _driver.FindElement(By.ClassName("post-stats__summary"));
                            if (visitElement.Text.Length <= 11) continue;
                            int.TryParse(visitElement.Text.Substring(11).Trim(), out var cnt);
                            adv.VisitCount = cnt;
                            await adv.SaveAsync();
                            await UpdateAdvStatus(adv);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }
        public async Task<string> GetScreenShot()
        {
            try
            {
                var rootPath = Path.Combine(Application.StartupPath, "ScreenShots");
                var savePath = Path.Combine(rootPath, Guid.NewGuid() + ".jpg");

                if (!Directory.Exists(rootPath)) Directory.CreateDirectory(rootPath);
                await Utility.Wait(3);
                _driver = Utility.RefreshDriver(_driver);
                ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile(savePath, ScreenshotImageFormat.Jpeg);

                return savePath;
            }
            catch (Exception ex)
            {
            }
            return "";
        }
        public List<DivarCityBussines> GetAllCityFromDivar()
        {
            var cities = new List<DivarCityBussines>();
            _driver = Utility.RefreshDriver(_driver);
            if (_driver.Url != "https://divar.ir/")
                _driver.Navigate().GoToUrl("https://divar.ir/");
            try
            {
                var cityElements = _driver.FindElements(By.TagName("h2"));
                foreach (var element in cityElements)
                {
                    var a = new DivarCityBussines
                    {
                        Guid = Guid.NewGuid(),
                        Name = element.Text,
                        DateSabt = DateConvertor.M2SH(DateTime.Now),
                        Status = true
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
        public async Task<bool> DeleteAdvFromDivar(AdvertiseLogBussines adv)
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
                _driver = Utility.RefreshDriver(_driver);
                if (_driver.Url != url) _driver.Navigate().GoToUrl(url);
                await Utility.Wait();
                //کلیک روی دکمه حذف
                _driver.FindElement(By.ClassName("trash")).Click();
                await Utility.Wait();
                _driver.SwitchTo().ActiveElement();
                //انتخاب رادیو باتن دومی-از فروش منصرف شدم
                var options = _driver.FindElements(By.ClassName("manage-delete__option"));
                if (options.Count <= 2) return false;
                options[1].Click();
                await Utility.Wait();
                //کلیک روی دکمه تائید 
                _driver.FindElement(By.ClassName("manage-delete__actions")).FindElement(By.ClassName("button"))?.Click();
                await Utility.Wait();
                if (_driver.Url != url) _driver.Navigate().GoToUrl(url);
                _driver.Navigate().Refresh();
                await Utility.Wait();
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

            try
            {
                var date = DateTime.Now.AddDays(-1 * fromNDayBefore);
                var advList = await
                    AdvertiseLogBussines.GetAllAsync();
                advList = advList.Where(p => p.DateM < date && p.StatusCode == statusCode).ToList();
                foreach (var adv in advList)
                {
                    if (TokenSource.IsCancellationRequested) break;
                    await DeleteAdvFromDivar(adv);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
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
                    var f = await DivarCityBussines.GetAllAsync();
                    var rnd = new Random().Next(0, f.Count);
                    var city = f[rnd].Name;
                    var number = SimcardBussines.GetAsync(AdvertiseType.Divar);
                    var tt = AdvTokensBussines.GetToken(number.Number, AdvertiseType.Divar);
                    var hasToken = tt?.Token ?? null;
                    if (string.IsNullOrEmpty(hasToken))
                    { File.WriteAllLines(path, list); return false; }
                    var log = await Login(number.Number);
                    if (!log)
                    { File.WriteAllLines(path, list); return false; }
                    _driver.Navigate().GoToUrl("https://divar.ir/");
                    await Utility.Wait();
                    _driver.FindElement(By.ClassName("city-selector")).Click();
                    await Utility.Wait();
                    _driver.FindElements(By.TagName("a")).LastOrDefault(q => q.Text == city)?.Click();
                    await Utility.Wait(2);
                    for (var j = 0; j < 2; j++)
                    {
                        ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                        await Utility.Wait();
                    }
                    var counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
                    await Utility.Wait();
                    for (var h = 1; h < counter.Count; h++)
                    {
                        await Utility.Wait();
                        var u = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
                        if (u.Count == 0)
                            _driver.Navigate().Back();
                        _driver.FindElements(By.ClassName("col-xs-12"))[h]?.Click();
                        await Utility.Wait(1);
                        _driver.FindElement(By.ClassName("post-actions__get-contact")).Click();
                        await Utility.Wait();

                        var a = _driver.FindElements(By.ClassName("primary"))
                            .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم");
                        if (a != null)
                            _driver.FindElements(By.ClassName("primary"))
                                .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم")?.Click();
                        await Utility.Wait();
                        var txt = _driver.FindElements(By.ClassName("post-fields-item__value")).FirstOrDefault()?.Text;
                        if (txt == "(پنهان‌شده؛ چت کنید)") continue;
                        list.Add(txt);
                        await Utility.Wait();
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
                File.WriteAllLines(path, list);
                return false;
            }
        }
        public async Task GetChatCount(List<SimcardBussines> simcard)
        {
            try
            {
                foreach (var sim in simcard)
                {
                    var log = await LoginChat(sim.Number);
                    if (!log) continue;
                    await Utility.Wait();
                    var e = _driver.FindElements(By.TagName("input")).ToList();
                    if (e.Count == 1)
                    {
                        var ownerName = sim.OwnerName;
                        _driver.FindElements(By.TagName("input")).FirstOrDefault()?.SendKeys(ownerName + "\n");
                    }
                    var q = _driver.FindElements(By.ClassName("dimmable")).ToList();
                    if (q.Count <= 1) continue;
                    await Utility.Wait();
                    var tr = _driver.FindElements(By.ClassName("conversation-item__conversation-status")).ToList();
                    if (tr.Count == 0) continue;
                    await Utility.Wait();
                    while (_driver.Url.Contains("https://chat.divar.ir"))
                    {
                        await Utility.Wait();
                    }
                    _driver.Navigate().GoToUrl("https://divar.ir/my-divar/my-posts");
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task SendChat(List<string> msg, int count, string city, string cat1, string cat2, string cat3)
        {
            try
            {
                if (SemaphoreSlim.CurrentCount == 0)
                {
                    var result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        TokenSource?.Cancel();
                    else return;
                }

                var all = await SimcardBussines.GetAllAsync();
                for (var k = 0; k <= all.Count; k++)
                {
                    var sim = await SimcardBussines.GetNextSimCardNumberAsync(AdvertiseType.Divar);
                    //--------------------------------------------
                    sim = 9382420272;
                    //--------------------------------------------
                    if (sim == 0) return;
                    var simbus = await SimcardBussines.GetAsync(sim);
                    simbus.NextUseDivarChat = DateTime.Now.AddDays(1);
                    await simbus.SaveAsync();
                    var log = await Login(sim);
                    if (!log) return;
                    _driver.Navigate().GoToUrl("https://divar.ir/");
                    await Utility.Wait();

                    _driver.FindElement(By.ClassName("city-selector")).Click();
                    await Utility.Wait();
                    _driver.FindElements(By.TagName("a")).LastOrDefault(q => q.Text == city)?.Click();
                    await Utility.Wait(2);


                    if (!string.IsNullOrEmpty(cat1))
                    {
                        var p = _driver.FindElements(By.ClassName("category-dropdown__icon")).Any();
                        if (!p) return;
                        _driver.FindElements(By.ClassName("category-dropdown__icon")).FirstOrDefault()?.Click();
                        await Utility.Wait();
                        _driver.FindElements(By.ClassName("category-button")).FirstOrDefault(q => q.Text == cat1)
                            ?.Click();
                        if (string.IsNullOrEmpty(cat2))
                            return;
                        if (string.IsNullOrEmpty(cat3))
                            _driver.FindElements(By.ClassName("category-button")).FirstOrDefault(q => q.Text == cat2)
                                ?.Click();
                        else
                            _driver.FindElements(By.ClassName("category-button")).FirstOrDefault(q => q.Text == cat3)
                                ?.Click();
                        await Utility.Wait();
                    }

                    var j = 0;

                    var counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
                    while (counter.Count <= count)
                    {
                        ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                        await Utility.Wait();
                        counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
                    }

                    for (var i = 0; j < count; i++)
                    {
                        await Utility.Wait();
                        _driver.FindElements(By.ClassName("col-xs-12"))[i + 1]?.Click();
                        await Utility.Wait(1);
                        var el = _driver.FindElements(By.ClassName("post-actions__chat")).Any();
                        if (!el)
                        {
                            _driver.Navigate().Back();
                            continue;
                        }

                        _driver.FindElement(By.ClassName("post-actions__chat")).Click();
                        var qanoon = _driver.FindElements(By.TagName("button"))
                            .Where(q => q.Text == "با قوانین دیوار موافقم").ToList();
                        if (qanoon.Count > 0)
                            qanoon.FirstOrDefault()?.Click();
                        var dc = _driver.WindowHandles.Count;
                        if (dc > 1)
                            _driver.SwitchTo().Window(_driver.WindowHandles[1]);
                        var logEl = _driver.FindElements(By.ClassName("auth__input__view")).Any();
                        if (logEl)
                        {
                            var tt = await LoginChat(sim);
                            if (!tt) continue;
                        }
                        await Utility.Wait(2);
                        var rnd = new Random().Next(0, msg.Count);
                        _driver.FindElement(By.ClassName("chat-box__input")).SendKeys(msg[rnd] + '\n');
                        await Utility.Wait(1);
                        j++;
                        _driver.Close();
                        _driver.SwitchTo().Window(_driver.WindowHandles[0]);
                        _driver.Navigate().Back();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        #endregion

    }
}
