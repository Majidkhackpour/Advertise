using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinesLayer;
using DataLayer;
using DataLayer.Enums;
using ErrorHandler;
using FMessegeBox;
using MihaZupan;
using OpenQA.Selenium;
using Telegram.Bot;
using Cookie = OpenQA.Selenium.Cookie;


namespace Ads.Classes
{
    public class DivarAdv
    {
        #region Fields
        private IWebDriver _driver;
        public int MaxImgForAdv { get; }
        private static SettingBussines clsSetting;
        public string AdvRootPath => Path.Combine(Application.StartupPath, "Advertise");

        public List<AdvertiseBussines> AdvertiseList;
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
                var res = SettingBussines.GetAll();
                clsSetting = res.Count == 0 ? new SettingBussines() : res[0];
                return clsSetting;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
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
        public async Task StartRegisterAdv(SimcardBussines sim)
        {
            try
            {
                _driver = Utility.RefreshDriver(_driver);
                var tt = AdvTokensBussines.GetToken(sim.Number, AdvertiseType.Divar);
                var hasToken = tt?.Token ?? null;
                if (string.IsNullOrEmpty(hasToken))
                {
                    sim.NextUse = DateTime.Now.AddMinutes(120);
                    await sim.SaveAsync();
                    lstMessage.Clear();
                    lstMessage.Add($"سیمکارت {sim.Number} به دلیل لاگین نبودن موفق به ارسال آگهی نشد");
                    Utility.ShowBalloon("عدم ارسال آگهی", lstMessage);
                    return;
                }
                if (await Login(sim.Number) == false)
                {
                    sim.NextUse = DateTime.Now.AddMinutes(120);
                    await sim.SaveAsync();
                    lstMessage.Clear();
                    lstMessage.Add($"سیمکارت {sim.Number} به دلیل لاگین نبودن موفق به ارسال آگهی نشد");
                    Utility.ShowBalloon("عدم ارسال آگهی", lstMessage);
                    return;
                }
                var adv = await GetNextAdv(sim);
                if (adv == null)
                {
                    lstMessage.Clear();
                    lstMessage.Add($"سیمکارت {sim.Number} به دلیل عدم دریافت آگهی موفق به ارسال آگهی نشد");
                    Utility.ShowBalloon("عدم ارسال آگهی", lstMessage);
                    return;
                }

                lstMessage.Clear();
                lstMessage.Add($"آگهی {adv.Adv} با {adv.ImagesPathList.Count + 1} تصویر دریافت شد");
                Utility.ShowBalloon("دریافت آگهی",
                    lstMessage);



                await RegisterAdv(adv, sim);
                await Utility.Wait(1);
                var title = "تعداد آگهی های ارسال شده با " + await Utility.FindGateWay();
                var body =
                    AdvertiseLogBussines.GetAllAdvInDayFromIP(await Utility.GetLocalIpAddress());
                lstMessage.Clear();
                lstMessage.Add(body.ToString());
                Utility.ShowBalloon(title, lstMessage);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
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

                        await simBusiness.SaveAsync(AdvertiseType.Divar, simBusiness.Number);
                        var message = $@"شماره: {simCardNumber}  \r\nلاگین انجام شد ";
                        ((IJavaScriptExecutor)_driver).ExecuteScript($"alert('{message}');");
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
            catch (StaleElementReferenceException rf)
            {
                return false;
            }
            catch (WebException er)
            {
                return false;

            }
            catch (WebDriverException ef)
            {
                return false;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
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
                await Utility.Wait(2);

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


                await Utility.Wait();
                var code = _driver.FindElements(By.TagName("button")).Any(q => q.Text == "دریافت کد تایید");
                if (code)
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



                        await simBusiness.SaveAsync(AdvertiseType.DivarChat, simBusiness.Number);

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
            catch (NoAlertPresentException)
            {
                return false;
            }
            catch (WebException)
            {
                return false;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return false;
            }

        }
        private async Task RegisterAdv(AdvertiseLogBussines adv, SimcardBussines sim)
        {
            try
            {
                adv.AdvType = AdvertiseType.Divar;
                _driver = Utility.RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://divar.ir/new");
                await Utility.Wait(1);
                //کلیک کردن روی کتگوری اصلی
                var a1 = _driver.FindElements(By.ClassName("expanded-category-selector__item")).FirstOrDefault(p => p.Text == adv.Category);
                adv.Category = a1?.Text;
                a1?.Click();

                await Utility.Wait(2);
                //کلیک روی ساب کتگوری 1
                var a2 = _driver.FindElements(By.ClassName("expanded-category-selector__item")).FirstOrDefault(p => p.Text == adv.SubCategory1);
                adv.SubCategory1 = a2?.Text;
                a2?.Click();
                await Utility.Wait();
                //کلیک روی ساب کتگوری2
                var a3 = _driver.FindElements(By.ClassName("expanded-category-selector__item")).FirstOrDefault(p => p.Text == adv.SubCategory2);
                adv.SubCategory2 = a3?.Text;
                a3?.Click();

                while (_driver.FindElements(By.ClassName("location-selector__city")).Count <= 0)
                {
                    await Utility.Wait(1);
                }

                await Utility.Wait(2);
                if (adv.ImagesPathList.Count == 0)
                {
                    ((IJavaScriptExecutor)_driver).ExecuteScript(@"alert('ربات موفق به دریافت تصاویر آگهی نشد');");
                    return;
                }

                foreach (var item in adv.ImagesPathList)
                {
                    try
                    {
                        //درج عکسها
                        _driver.FindElement(By.ClassName("image-uploader__dropzone")).FindElement(By.TagName("input[type=file]")).SendKeys(item);
                        await Utility.Wait();
                        //break;
                    }
                    catch
                    {
                    }
                }



                await Utility.Wait(2);
                _driver.FindElement(By.ClassName("location-selector__city")).FindElement(By.TagName("input")).SendKeys(adv.City + "\n");

                await Utility.Wait(2);
                var el = _driver.FindElements(By.ClassName("location-selector__district")).Any();
                await Utility.Wait(1);
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

                await Utility.Wait(1);


                var checkchat = _driver.FindElements(By.Id("root_contact_chat_enabled")).Any();
                if (checkchat)
                {
                    var tttttt = _driver.FindElement(By.Id("root_contact_chat_enabled")).Selected;
                    await Utility.Wait(1);
                    if (tttttt != sim.IsEnableChat)
                        _driver.FindElement(By.Id("root_contact_chat_enabled")).Click();
                }


                await Utility.Wait(1);
                var checkshoemobile = _driver.FindElements(By.Id("root_contact_hide_phone")).Any();
                if (checkshoemobile)
                {
                    var eeeeee = _driver.FindElement(By.Id("root_contact_hide_phone")).Selected;
                    await Utility.Wait(1);
                    if (eeeeee == sim.IsEnableNumber)
                        _driver.FindElement(By.Id("root_contact_hide_phone")).Click();
                }
                await Utility.Wait(1);

                var radio = _driver.FindElements(By.ClassName("radion")).ToList();
                await Utility.Wait(1);
                if (radio.Count > 0)
                {
                    await Utility.Wait(1);
                    _driver.FindElements(By.TagName("span")).FirstOrDefault(q => q.Text.Contains("فروشی"))?.Click();
                }
                await Utility.Wait(1);
                //درج قیمت
                if (adv.Price > 0) _driver.FindElement(By.TagName("input")).SendKeys(adv.Price.ToString());
                await Utility.Wait(1);
                //درج عنوان آگهی
                _driver.FindElements(By.TagName("input")).Last().SendKeys(adv.Title);
                await Utility.Wait(1);
                //درج محتوای آگهی
                var thread = new Thread(() => Clipboard.SetText(adv.Content.Replace('(', '<').Replace(')', '>')));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

                var t = _driver.FindElement(By.TagName("textarea"));
                t.Click();
                await Utility.Wait(1);
                t.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                var thread1 = new Thread(Clipboard.Clear);
                thread1.SetApartmentState(ApartmentState.STA);
                thread1.Start();
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
                await Utility.Wait(1);
                var but = _driver.FindElements(By.TagName("button")).Any(q => q.Text.Contains("ارسال آگهی"));
                if (but)
                    //کلیک روی دکمه ثبت آگهی
                    _driver.FindElements(By.TagName("button")).FirstOrDefault(q => q.Text.Contains("ارسال آگهی"))
                        ?.Click();


                await Utility.Wait(1);

                adv.URL = _driver.Url;
                adv.StatusCode = StatusCode.InPublishQueue;
                adv.IP = await Utility.GetLocalIpAddress();
                adv.AdvStatus = "در صف انتشار";
                await adv.SaveAsync();

            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }
        private async Task<AdvertiseLogBussines> GetNextAdv(SimcardBussines simCardNumber)
        {
            var newAdvertiseLogBusiness = new AdvertiseLogBussines();
            try
            {
                newAdvertiseLogBusiness.SimCardNumber = simCardNumber.Number;

                //لیست آگهی های سیمکارت
                var simGuid = await SimcardBussines.GetAsync(simCardNumber.Number);
                var advList = await SimcardAdsBussines.GetAllAsync(simGuid.Guid);
                advList = advList.ToList();

                #region findNextAdvIndex

                AdvertiseList = new List<AdvertiseBussines>();
                foreach (var item in advList)
                {
                    var adv = await AdvertiseBussines.GetAsync(item.Advertise);
                    AdvertiseList.Add(adv);
                }

                var nextAdvIndex = new Random().Next(AdvertiseList.Count);
                #endregion

                newAdvertiseLogBusiness.Adv = AdvertiseList[nextAdvIndex].AdvName;
                #region FindNextTitle

                var AllTitles = await AdvTitlesBussines.GetAllAsync(AdvertiseList[nextAdvIndex].Guid);

                //تایتل آگهی دریافت می شود
                var nextTitleIndex = new Random(DateTime.Now.Millisecond).Next(AllTitles.Count);
                newAdvertiseLogBusiness.Title = AllTitles[nextTitleIndex].Title;

                if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Title) || newAdvertiseLogBusiness.Title == "---")
                {
                    WebErrorLog.ErrorInstence.StartErrorLog(new ArgumentNullException(),
                        $"آگهی {newAdvertiseLogBusiness.Adv}موفق به دریافت عنوان نشد");
                    return null;
                }

                #endregion

                #region GetContent
                //کانتنت آگهی دریافت می شود

                var AllContent = await AdvContentBussines.GetAllAsync(AdvertiseList[nextAdvIndex].Guid);
                //کانتنت آگهی دریافت می شود

                    var nextContentIndex = new Random(DateTime.Now.Millisecond).Next(AllContent.Count);
                    newAdvertiseLogBusiness.Content = AllContent[nextContentIndex].Content;

                if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Content) || newAdvertiseLogBusiness.Content == "---")
                {
                    WebErrorLog.ErrorInstence.StartErrorLog(new ArgumentNullException(),
                        $"آگهی {newAdvertiseLogBusiness.Adv} موفق به دریافت توضیحات نشد");
                    return null;
                }

                // if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Content)) return null;

                #endregion

                #region FindImages

                //عکسهای آگهی دریافت می شود
                newAdvertiseLogBusiness.ImagesPathList =
                    await GetNextImages(AdvertiseList[nextAdvIndex].Guid, clsSetting?.MaxImgCount ?? 3);
                if (newAdvertiseLogBusiness.ImagesPathList.Count > 0)
                {
                    newAdvertiseLogBusiness.ImagePath = "";
                    foreach (var item in newAdvertiseLogBusiness.ImagesPathList)
                    {
                        newAdvertiseLogBusiness.ImagePath += item + "\r\n";
                    }
                }

                if (newAdvertiseLogBusiness.ImagesPathList == null || newAdvertiseLogBusiness.ImagesPathList.Count == 0)
                {
                    WebErrorLog.ErrorInstence.StartErrorLog(new ArgumentNullException(),
                        $"آگهی {newAdvertiseLogBusiness.Adv} موفق به دریاقت تصویر نشد");
                    return null;
                }


                #endregion

                //قیمت آگهی دریافت می شود
                newAdvertiseLogBusiness.Price = decimal.Parse(AdvertiseList[nextAdvIndex].Price);
                while (string.IsNullOrEmpty(newAdvertiseLogBusiness.City) || newAdvertiseLogBusiness.City == "---")
                {
                    var Allcity = await DivarSimCityBussines.GetAllAsync(simGuid.Guid);
                    var rand = new Random().Next(0, Allcity.Count);
                    var city = Allcity[rand];
                    var cc = DivarCityBussines.GetAsync(city.CityGuid);
                    newAdvertiseLogBusiness.City = cc?.Name;
                    newAdvertiseLogBusiness.State = "";
                }

                var guid1 = AdvertiseList[nextAdvIndex]?.DivarCatGuid1 ?? Guid.Empty;
                newAdvertiseLogBusiness.Category = AdvCategoryBussines.Get(guid1)?.Name ?? "";

                var guid2 = AdvertiseList[nextAdvIndex]?.DivarCatGuid2 ?? Guid.Empty;
                newAdvertiseLogBusiness.SubCategory1 = AdvCategoryBussines.Get(guid2)?.Name ?? "";

                var guid3 = AdvertiseList[nextAdvIndex]?.DivarCatGuid3 ?? Guid.Empty;
                newAdvertiseLogBusiness.SubCategory2 = AdvCategoryBussines.Get(guid3)?.Name ?? "";

                return newAdvertiseLogBusiness;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return null;
            }
        }
        private async Task<List<string>> GetNextImages(Guid advGuid, int imgCount = 3)
        {
            var resultImages = new List<string>();

            try
            {
                //گرفتن تمام عکسهای پوشه و فیلتر کردن عکسهای درست
                var allImages = await AdvPicturesBussines.GetAllAsync(advGuid);
                var selectedImages = new List<string>();
                //حذف عکسهای زیر پیکسل 600*600
                foreach (var imgItem in allImages)
                {
                    var img = Image.FromFile(imgItem.PathGuid);
                    if (img.Width < 600 || img.Height < 600)
                        try
                        {
                            img.Dispose();
                            File.Delete(imgItem.PathGuid);
                        }
                        catch
                        {
                            /**/
                        }
                    img.Dispose();
                }

                if (allImages.Count <= imgCount)
                {
                    foreach (var item in allImages)
                    {
                        selectedImages.Add(item.PathGuid + "\r\n");
                    }
                }
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

                    selectedImages.AddRange(indexes.Select(index => allImages[index].PathGuid));
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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return resultImages;
            }
        }




        public async Task<List<RegionBussiness>> GetAllRegionFromDivar(List<string> City)
        {
            var region = new List<RegionBussiness>();
            var b1 = await AdvCategoryBussines.GetAllAsync(Guid.Empty, AdvertiseType.Divar);
            var Allcat1 = b1.ToList();
            var rand = new Random().Next(0, Allcat1.Count);
            var cat1 = Allcat1[rand];

            b1 = await AdvCategoryBussines.GetAllAsync(cat1.Guid, AdvertiseType.Divar);
            Allcat1 = b1.ToList();
            rand = new Random().Next(0, Allcat1.Count);
            var cat2 = Allcat1[rand];

            b1 = await AdvCategoryBussines.GetAllAsync(cat2.Guid, AdvertiseType.Divar);
            Allcat1 = b1.ToList();
            rand = new Random().Next(0, Allcat1.Count);
            var cat3 = Allcat1[rand];
            _driver = Utility.RefreshDriver(_driver);
            _driver.Navigate().GoToUrl("https://divar.ir/new");
            //کلیک کردن روی کتگوری اصلی
            _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text == cat1.Name)?.Click();
            await Utility.Wait(2);
            //کلیک روی ساب کتگوری 1
            _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text == cat2.Name)?.Click();
            await Utility.Wait(2);
            //کلیک روی ساب کتگوری2
            var catrand = _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).Any();
            if (catrand)
                _driver.FindElements(By.ClassName("submit-post__category-selector--open__item")).FirstOrDefault(p => p.Text == cat3.Name)?.Click();

            await Utility.Wait(2);
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
                WebErrorLog.ErrorInstence.StartErrorLog(e);
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
                        dayCount = clsSetting?.DayCountForUpdateState ?? 10;
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
                            WebErrorLog.ErrorInstence.StartErrorLog(ex);
                            await Utility.Wait();
                            tryCount++;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    WebErrorLog.ErrorInstence.StartErrorLog(ex);
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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        public async Task SendChat(List<string> msg, List<string> msg2, int count, string city, string cat1, string cat2, string cat3, string fileName, SimcardBussines sim)
        {
            try
            {
                //if (SemaphoreSlim.CurrentCount == 0)
                //{
                //    var result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
                //        MessageBoxIcon.Question);
                //    if (result == DialogResult.Yes)
                //        TokenSource?.Cancel();
                //    else return;
                //}


                //توکن چت نداشت برگرد
                var log2 = AdvTokensBussines.GetToken(sim.Number, AdvertiseType.DivarChat);
                if (log2 != null && string.IsNullOrEmpty(log2.Token)) return;

                //ورود به دیوار
                var log = await Login(sim.Number);
                if (!log) return;
                _driver.Navigate().GoToUrl("https://divar.ir/");
                await Utility.Wait();
                //انتخاب شهر
                _driver.FindElement(By.ClassName("city-selector")).Click();
                await Utility.Wait();
                _driver.FindElements(By.TagName("a")).LastOrDefault(q => q.Text == city)?.Click();
                await Utility.Wait(2);

                //انتخاب دسته بندی
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

                var ele = _driver.FindElements(By.ClassName("col-xs-12")).Any();
                while (!ele)
                {
                    ele = _driver.FindElements(By.ClassName("col-xs-12")).Any();
                }

                var j = 0;
                //اسکرول تا تعداد مشخص
                var counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
                while (counter.Count <= count)
                {
                    ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                    await Utility.Wait();
                    counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
                }

                for (var i = 0; j < count; i++)
                {
                    //انتخاب آگهی
                    await Utility.Wait();
                    _driver.FindElements(By.ClassName("col-xs-12"))[i + 1]?.Click();
                    await Utility.Wait(1);
                    //دریافت شماره آگهی
                    await Utility.Wait(5);
                    _driver.FindElement(By.ClassName("post-actions__get-contact")).Click();
                    await Utility.Wait();

                    var a = _driver.FindElements(By.ClassName("primary"))
                        .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم");
                    if (a != null)
                        _driver.FindElements(By.ClassName("primary"))
                            .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم")?.Click();
                    await Utility.Wait(1);
                    //چت
                    var el = _driver.FindElements(By.ClassName("post-actions__chat")).Any();
                    var txt = _driver.FindElements(By.ClassName("post-fields-item__value")).FirstOrDefault()?.Text;
                    if (txt == "(پنهان‌شده؛ چت کنید)")
                    {
                        //شروع چت
                        _driver.FindElement(By.ClassName("post-actions__chat")).Click();
                        var qanoon = _driver.FindElements(By.TagName("button"))
                            .Where(q => q.Text == "با قوانین دیوار موافقم").ToList();
                        if (qanoon.Count > 0)
                            qanoon.FirstOrDefault()?.Click();
                        var dc = _driver.WindowHandles.Count;
                        if (dc > 1)
                            _driver.SwitchTo().Window(_driver.WindowHandles[1]);
                        await Utility.Wait(2);
                        var logEl = _driver.FindElements(By.ClassName("auth__input__view")).Any();
                        if (logEl)
                        {
                            var tt = await LoginChat(sim.Number);
                            if (!tt) continue;
                        }
                        await Utility.Wait(2);
                        var rnd = new Random().Next(0, msg.Count);
                        await Utility.Wait(2);
                        try
                        {

                            //ارسال متن اول
                            var thread = new Thread(() => Clipboard.SetText(msg[rnd]));
                            thread.SetApartmentState(ApartmentState.STA);
                            thread.Start();
                            var f = _driver.FindElements(By.ClassName("chat-box__input")).Any();
                            while (!f)
                            {
                                f = _driver.FindElements(By.ClassName("chat-box__input")).Any();
                            }
                            var t = _driver.FindElement(By.ClassName("chat-box__input"));

                            t.Click();
                            await Utility.Wait();
                            t.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                            t.SendKeys("" + '\n');
                            var thread1 = new Thread(Clipboard.Clear);
                            thread1.SetApartmentState(ApartmentState.STA);
                            thread1.Start();





                            //_driver.FindElement(By.ClassName("chat-box__input")).SendKeys(msg[rnd] + '\n');
                            await Utility.Wait(2);

                            if (sim.isSendSecondChat)
                            {
                                //اگر کاربر جواب داده بود، متن دوم رو بفرست
                                var allChat = _driver.FindElements(By.ClassName("dimmable"))
                                    .Where(q => q.Text.Contains("پیام جدید") && !q.Text.Contains("پستچی دیوار"))
                                    .ToList();
                                await Utility.Wait(1);
                                if (allChat.Count > 0)
                                {
                                    foreach (var element in allChat)
                                    {
                                        element.Click();
                                        var rnd2 = new Random().Next(0, msg2.Count);
                                        await Utility.Wait(1);


                                        var thread10 = new Thread(() => Clipboard.SetText(msg2[rnd2]));
                                        thread10.SetApartmentState(ApartmentState.STA);
                                        thread10.Start();

                                        var t2 = _driver.FindElement(By.ClassName("chat-box__input"));
                                        t2.Click();
                                        await Utility.Wait();
                                        t2.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                                        t2.SendKeys("" + '\n');
                                        var thread101 = new Thread(Clipboard.Clear);
                                        thread101.SetApartmentState(ApartmentState.STA);
                                        thread101.Start();



                                        //_driver.FindElement(By.ClassName("chat-box__input")).SendKeys(msg2[rnd2] + '\n');
                                        await Utility.Wait(2);
                                    }
                                }
                            }

                            // ذخیره شماره در جدول که بعدا کسی باهاش چت نکنه
                            var chatNumbers = new ChatNumberBussines()
                            {
                                Guid = Guid.NewGuid(),
                                Number = txt.FixString(),
                                DateSabt = DateConvertor.M2SH(DateTime.Now),
                                Status = true,
                                Type = AdvertiseType.Divar
                            };
                            await chatNumbers.SaveAsync();
                            j++;
                        }
                        catch (Exception e)
                        {
                            WebErrorLog.ErrorInstence.StartErrorLog(e);
                        }
                        _driver.Close();
                        _driver.SwitchTo().Window(_driver.WindowHandles[0]);
                        _driver.Navigate().Back();
                        break;
                    }
                    else
                    {
                        if (!el)
                        {
                            //ذخیره شماره

                            if (File.Exists(fileName))
                            {
                                var numbers = File.ReadAllLines(fileName).ToList();
                                numbers.Add(txt.FixString());
                                //غیر تکراری بودن شماره
                                numbers = numbers.GroupBy(q => q).Where(q => q.Count() == 1).Select(q => q.Key).ToList();
                                File.WriteAllLines(fileName, numbers);
                            }
                            else
                            {
                                File.WriteAllText(fileName, txt.FixString());
                            }
                            _driver.Navigate().Back();
                            continue;
                        }
                        //اگر شماره قبلا چت شده بود چت نکن
                        var allNumbers = ChatNumberBussines.GetAll(AdvertiseType.Divar);
                        if (allNumbers == null)
                        {
                            _driver.Navigate().Back();
                            continue;
                        }
                        var n = 0;
                        foreach (var item in allNumbers)
                        {
                            if (item == null && string.IsNullOrEmpty(item.Number)) continue;
                            if (txt.FixString() == item.Number)
                                n++;
                        }

                        if (n > 0)
                        {
                            _driver.Navigate().Back();
                            continue;
                        }
                        //شروع چت
                        _driver.FindElement(By.ClassName("post-actions__chat")).Click();
                        var qanoon = _driver.FindElements(By.TagName("button"))
                            .Where(q => q.Text == "با قوانین دیوار موافقم").ToList();
                        if (qanoon.Count > 0)
                            qanoon.FirstOrDefault()?.Click();
                        var dc = _driver.WindowHandles.Count;
                        if (dc > 1)
                            _driver.SwitchTo().Window(_driver.WindowHandles[1]);
                        await Utility.Wait(2);
                        var logEl = _driver.FindElements(By.ClassName("auth__input__view")).Any();
                        if (logEl)
                        {
                            var tt = await LoginChat(sim.Number);
                            if (!tt) continue;
                        }
                        await Utility.Wait(2);
                        var rnd = new Random().Next(0, msg.Count);
                        await Utility.Wait(2);
                        try
                        {
                            var f = _driver.FindElements(By.ClassName("chat-box__input")).Any();
                            while (!f)
                            {
                                f = _driver.FindElements(By.ClassName("chat-box__input")).Any();
                            }
                            //ارسال متن اول
                            var thread = new Thread(() => Clipboard.SetText(msg[rnd]));
                            thread.SetApartmentState(ApartmentState.STA);
                            thread.Start();

                            var t = _driver.FindElement(By.ClassName("chat-box__input"));
                            t.Click();
                            await Utility.Wait();
                            t.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                            t.SendKeys("" + '\n');
                            var thread1 = new Thread(Clipboard.Clear);
                            thread1.SetApartmentState(ApartmentState.STA);
                            thread1.Start();





                            //_driver.FindElement(By.ClassName("chat-box__input")).SendKeys(msg[rnd] + '\n');
                            await Utility.Wait(2);


                            //اگر کاربر جواب داده بود، متن دوم رو بفرست
                            var allChat = _driver.FindElements(By.ClassName("dimmable"))
                                .Where(q => q.Text.Contains("پیام جدید") && !q.Text.Contains("پستچی دیوار")).ToList();
                            await Utility.Wait(1);
                            if (allChat.Count > 0)
                            {
                                foreach (var element in allChat)
                                {
                                    element.Click();
                                    var rnd2 = new Random().Next(0, msg2.Count);
                                    await Utility.Wait(1);


                                    var thread10 = new Thread(() => Clipboard.SetText(msg2[rnd2]));
                                    thread10.SetApartmentState(ApartmentState.STA);
                                    thread10.Start();

                                    var t2 = _driver.FindElement(By.ClassName("chat-box__input"));
                                    t2.Click();
                                    await Utility.Wait();
                                    t2.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                                    t2.SendKeys("" + '\n');
                                    var thread101 = new Thread(Clipboard.Clear);
                                    thread101.SetApartmentState(ApartmentState.STA);
                                    thread101.Start();



                                    //_driver.FindElement(By.ClassName("chat-box__input")).SendKeys(msg2[rnd2] + '\n');
                                    await Utility.Wait(2);
                                }
                            }

                            // ذخیره شماره در جدول که بعدا کسی باهاش چت نکنه
                            var chatNumbers = new ChatNumberBussines()
                            {
                                Guid = Guid.NewGuid(),
                                Number = txt.FixString(),
                                DateSabt = DateConvertor.M2SH(DateTime.Now),
                                Status = true,
                                Type = AdvertiseType.Divar
                            };
                            await chatNumbers.SaveAsync();
                            j++;
                        }
                        catch (Exception e)
                        {
                            WebErrorLog.ErrorInstence.StartErrorLog(e);
                        }
                        _driver.Close();
                        _driver.SwitchTo().Window(_driver.WindowHandles[0]);
                        _driver.Navigate().Back();
                    }
                }
            }
            catch (StaleElementReferenceException) { }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        public async Task GetCategory()
        {
            try
            {
                _driver = Utility.RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://divar.ir/new");
                await Utility.Wait();
                var listCat = _driver.FindElements(By.ClassName("submit-post__category-selector--open__item"))
                    .ToList();
                var listAll = await AdvCategoryBussines.GetAllAsync();
                listAll = listAll.Where(q => q.Type == AdvertiseType.Divar).ToList();
                if (listAll.Count > 0)
                    AdvCategoryBussines.RemoveAllAsync(listAll);
                foreach (var item in listCat)
                {
                    var a = new AdvCategoryBussines()
                    {
                        Guid = Guid.NewGuid(),
                        Type = AdvertiseType.Divar,
                        Name = item.Text.Trim(),
                        DateSabt = DateConvertor.M2SH(DateTime.Now),
                        ParentGuid = Guid.Empty,
                        Status = true
                    };
                    await a.SaveAsync();
                }
                listAll = await AdvCategoryBussines.GetAllAsync();
                listAll = listAll.Where(q => q.Type == AdvertiseType.Divar).ToList();
                if (listAll.Count <= 0) return;
                foreach (var element in listAll)
                {
                    _driver.FindElements(By.ClassName("submit-post__category-selector--open__item"))
                        .FirstOrDefault(q => q.Text == element.Name)?.Click();
                    await Utility.Wait();
                    var listCat2 = _driver.FindElements(By.ClassName("submit-post__category-selector--open__item"))
                        .ToList();
                    foreach (var item in listCat2)
                    {
                        var a = new AdvCategoryBussines()
                        {
                            Guid = Guid.NewGuid(),
                            Type = AdvertiseType.Divar,
                            Name = item.Text.Trim(),
                            DateSabt = DateConvertor.M2SH(DateTime.Now),
                            ParentGuid = element.Guid,
                            Status = true
                        };
                        await a.SaveAsync();
                    }


                    var newList = await AdvCategoryBussines.GetAllAsync(element.Guid, AdvertiseType.Divar);
                    if (newList.Count <= 0) continue;
                    foreach (var item in newList)
                    {
                        _driver.FindElements(By.ClassName("submit-post__category-selector--open__item"))
                            .FirstOrDefault(q => q.Text == item.Name)?.Click();
                        await Utility.Wait();
                        var listCat3 = _driver.FindElements(By.ClassName("submit-post__category-selector--open__item"))
                            .ToList();
                        if (listCat3.Count <= 0)
                        {
                            await Utility.Wait(2);
                            var element10 = _driver
                                .FindElements(By.ClassName("submit-post__category-selector--close__button")).Any();
                            while (!element10)
                            {
                                element10 = _driver
                                    .FindElements(By.ClassName("submit-post__category-selector--close__button")).Any();
                                await Utility.Wait(2);
                            }
                            _driver.FindElement(By.ClassName("submit-post__category-selector--close__button")).Click();
                            await Utility.Wait();
                            continue;
                        }
                        foreach (var ne in listCat3)
                        {
                            var a = new AdvCategoryBussines()
                            {
                                Guid = Guid.NewGuid(),
                                Type = AdvertiseType.Divar,
                                Name = ne.Text.Trim(),
                                DateSabt = DateConvertor.M2SH(DateTime.Now),
                                ParentGuid = item.Guid,
                                Status = true
                            };
                            await a.SaveAsync();
                        }

                        await Utility.Wait(2);
                        _driver.FindElement(By.ClassName("submit-post__category-selector--open__back__icon")).Click();
                        await Utility.Wait();
                    }

                    var a10 = _driver.FindElements(By.ClassName("submit-post__category-selector--open__back__icon"))
                        .Any();
                    if (a10)
                        _driver.FindElement(By.ClassName("submit-post__category-selector--open__back__icon")).Click();
                    await Utility.Wait();
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        public async Task GetPost(long number, string cat1, string cat2, string cat3, string city, int count, string chatId, string desc)
        {
            try
            {
                _driver = Utility.RefreshDriver(_driver);
                var log = await Login(number);
                if (!log) return;
                _driver.Navigate().GoToUrl("https://divar.ir/");
                await Utility.Wait();
                //انتخاب شهر
                _driver.FindElement(By.ClassName("city-selector")).Click();
                await Utility.Wait();
                _driver.FindElements(By.TagName("a")).LastOrDefault(q => q.Text == city)?.Click();
                await Utility.Wait(2);
                var savePathFile = Path.Combine(Application.StartupPath, "TelegramImages");
                if (!Directory.Exists(savePathFile)) Directory.CreateDirectory(savePathFile);
                //testBanner__.jpg حتما در پوشه برنامه موجود شود
                var bannerPath = Path.Combine(Application.StartupPath, "testBanner__.jpg");
                if (!File.Exists(bannerPath)) return;
                await Utility.Wait(3);
                //انتخاب دسته بندی

                if (!string.IsNullOrEmpty(cat1))
                {
                    await Utility.Wait(1);
                    var p = _driver.FindElements(By.ClassName("category-dropdown__icon")).Any();
                    if (!p) return;
                    _driver.FindElements(By.ClassName("category-dropdown__icon")).FirstOrDefault()?.Click();
                    await Utility.Wait(1);
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

                var counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
                while (counter.Count <= 0)
                {
                    counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
                }
                while (counter.Count <= count)
                {
                    ((IJavaScriptExecutor)_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
                    await Utility.Wait();
                    counter = _driver.FindElements(By.ClassName("col-xs-12")).ToList();
                }

                //دریافت آگهی ها
                for (var i = 0; i < count; i++)
                {
                    await Utility.Wait();
                    _driver.FindElements(By.ClassName("col-xs-12"))[i + 1]?.Click();
                    await Utility.Wait(2);
                    var noPic = _driver.FindElements(By.ClassName("no-picture-image")).Any();
                    if (noPic)
                    {
                        _driver.Navigate().Back();
                        await Utility.Wait(2);
                        continue;
                    }
                    var im = _driver.FindElements(By.TagName("img")).ToList();
                    if (im.Count > 0)
                    {
                        //دریافت اولین تصویر آگهی
                        var ul = _driver.FindElements(By.ClassName("slick-dots")).Any();
                        List<IWebElement> li = null;
                        if (ul)
                        {
                            //اگر چندتا عکس داشت، اولی رو بردار
                            li = _driver.FindElement(By.ClassName("slick-dots")).FindElements(By.TagName("li"))
                                .ToList();
                        }
                        else
                        {
                            //اگر یک عکس داشت بردار
                            li = new List<IWebElement>();
                            li.Add(_driver.FindElement(By.ClassName("slick-track")));
                        }

                        await Utility.Wait(2);
                        var src = li.FirstOrDefault()?.FindElement(By.TagName("img"))
                            .GetAttribute("src");
                        var path = Path.Combine(savePathFile, Guid.NewGuid() + ".jpg");
                        var pathsave = Path.Combine(savePathFile, Guid.NewGuid() + ".jpg");
                        var finnalPath = Path.Combine(savePathFile, Guid.NewGuid() + ".jpg");
                        //دانلود تصویر
                        DownloadImage(src, path);
                        //ایجاد تصویر با بنر
                        CreateNewImage(path, bannerPath, pathsave);
                        //دریافت محتویات پست
                        var title = _driver.FindElement(By.ClassName("post-header__title")).Text.FixString();
                        await Utility.Wait(1);
                        var content = _driver.FindElement(By.ClassName("post-page__description")).Text;
                        //دریافت شماره پست 
                        await Utility.Wait(2);
                        _driver.FindElement(By.ClassName("post-actions__get-contact")).Click();
                        await Utility.Wait(2);

                        var a = _driver.FindElements(By.ClassName("primary"))
                            .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم");
                        await Utility.Wait(1);
                        if (a != null)
                            _driver.FindElements(By.ClassName("primary"))
                                .FirstOrDefault(q => q.Text == "با قوانین دیوار موافقم")?.Click();
                        await Utility.Wait();
                        var pr = _driver.FindElements(By.ClassName("post-fields-item__value"))
                                     .FirstOrDefault(q => q.Text.Contains("تومان") || q.Text.Contains("توافقی"))?.Text
                                     ?.FixString() ?? "توافقی";

                        await Utility.Wait(1);
                        var num = "";
                        var txt = _driver.FindElements(By.ClassName("post-fields-item__value")).FirstOrDefault()?.Text;
                        if (txt == "(پنهان‌شده؛ چت کنید)") txt = "";
                        if (!string.IsNullOrEmpty(txt)) num = txt.FixString();
                        var passage = title + "\r\n" + content + "\r\n" + num + "\r\n" + desc;
                        //ایجاد تصویر نهایی
                        WriteTextOnImage(title, num, pr, pathsave, finnalPath);


                        //ارسال به تلگرام
                        var the = new Thread(async () => await BotInitial(chatId, passage, finnalPath));
                        the.Start(); _driver.Navigate().Back();
                        await Utility.Wait(1);
                    }
                    else
                    {
                        _driver.Navigate().Back();
                        await Utility.Wait(1);
                    }
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

        private void DownloadImage(string src, string path)
        {
            var webClient = new WebClient();
            webClient.DownloadFile(src, path);
        }

        private void CreateNewImage(string bodyPath, string bannerPath, string savePath)
        {
            try
            {
                var banner = Image.FromFile(bannerPath);
                var body = Image.FromFile(bodyPath);
                var bitmap = new Bitmap(body.Width, body.Height);
                var canvas = Graphics.FromImage(bitmap);
                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                canvas.DrawImage(body, new Rectangle(0, 0, body.Width, body.Height),
                    new Rectangle(0, 0, body.Width, body.Height), GraphicsUnit.Pixel);
                canvas.DrawImage(banner, 0, body.Height - 75, body.Width, 75);
                canvas.Save();
                bitmap.Save(savePath);
                bitmap.Dispose();
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
        }

        private void WriteTextOnImage(string text, string num, string pric, string filePath, string savePath)
        {
            try
            {
                var firstText = text;
                var number = num;
                var price = pric;
                var link = "@bazarcheh_mashhad";

                var imageFilePath = filePath;
                var bitmap = (Bitmap)Image.FromFile(imageFilePath);


                var firstLocation = new PointF();
                if (firstText.Length < 20)
                    firstLocation = new PointF(bitmap.Width - 200, bitmap.Height - 70);
                else if (firstText.Length >= 20 && firstText.Length < 30)
                    firstLocation = new PointF(bitmap.Width - 300, bitmap.Height - 70);
                else if (firstText.Length >= 30 && firstText.Length < 40)
                    firstLocation = new PointF(bitmap.Width - 415, bitmap.Height - 70);
                else if (firstText.Length > 40)
                    firstLocation = new PointF(bitmap.Width - 435, bitmap.Height - 70);
                var numberLocation = new PointF(number.Length * 20, bitmap.Height - 30);
                var linkLocation = new PointF(bitmap.Width - 150, bitmap.Height - 20);
                var priceLocation = new PointF(firstText.Length, bitmap.Height - 65);

                if (firstText.Length > 40)
                    firstText = "..." + firstText.Remove(38, firstText.Length - 38);

                var graphics = Graphics.FromImage(bitmap);


                var arialFont = new Font("B Mehr", 18);
                var numberFont = new Font("B Yekan", 14);
                var linkFont = new Font("B Yekan", 8);
                var priceFont = new Font("B Morvarid", 18);



                graphics.DrawString(firstText, arialFont, Brushes.Black, firstLocation);
                graphics.DrawString(number, numberFont, Brushes.Red, numberLocation);
                graphics.DrawString(link, linkFont, Brushes.Black, linkLocation);
                graphics.DrawString(price, priceFont, Brushes.White, priceLocation);

                bitmap.Save(savePath);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }



        private async Task BotInitial(string chatid, string caption, string fileName)
        {
            try
            {
                var tel = await TelegramBot.GetInstance();
                var ts = new Thread(new ThreadStart(async () =>
                    await tel.StartSending(TelegramSendType.SendPost, chatId: chatid, fileName: fileName,
                        caption: caption)));
                ts.Start();
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
            }
            //var proxy = new HttpToSocks5Proxy("192.168.1.11", 1080) { ResolveHostnamesLocally = false };
            //_bot = new TelegramBotClient(@"942511223:AAFxQXqFRm10gmo_ls9Ng20WKsk6kLcgPZw", proxy);
        }
        private TelegramBotClient _bot;

        #endregion

    }
}
