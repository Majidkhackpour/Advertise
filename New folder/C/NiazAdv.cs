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
using PacketParser;
using PacketParser.Entities;
using static AdvertiseApp.Classes.Utility;
using PacketParser.BusinessLayer;

namespace AdvertiseApp.Classes
{
    public class NiazAdv
    {
        #region Fields

        private IWebDriver _driver;
        private bool _noErrorInRegister = true;
        private readonly string _advRootPath;
        private int _maxImgForAdv;
        private static SettingBusiness _cls;
        #endregion

        #region Ctors

        /// <summary>
        /// اگر مسیر آگهی داده نشود از مسیر پیش فرض فایل کانفیگ استفاده خواهد شد
        /// </summary>
        /// <param name="advRootPath"></param>
        /// <param name="maxImgForAnyAdv"></param>
        public NiazAdv(string advRootPath = "", int maxImgForAnyAdv = 3)
        {
            _advRootPath = advRootPath;
            if (string.IsNullOrEmpty(_advRootPath)) _advRootPath = ConfigurationManager.AppSettings.Get("RootPath");
            _maxImgForAdv = maxImgForAnyAdv;

            _driver = RefreshDriver(_driver);

        }

        #endregion

        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);
        private static string AdvRootPath => ConfigurationManager.AppSettings.Get("RootPath");
        private CancellationTokenSource _tokenSource;

        private List<string> lstMessage = new List<string>();
        public async Task StartRegisterAdv(List<long> numbers=null , int count = 0)
        {
            //if (_semaphoreSlim.CurrentCount == 0)
            //{
            //    DialogResult result;
            //    result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
            //        MessageBoxIcon.Question);
            //    if (result == DialogResult.Yes)
            //        _tokenSource?.Cancel();
            //    else return;
            //}
            _tokenSource?.Cancel();
            await _semaphoreSlim.WaitAsync();
            _tokenSource = new CancellationTokenSource();

            var monitor = new PerfMonitor();
            var isLogin = false;
            await Task.Run(async () =>
            {
                try
                {
                    while (await PingHost("185.105.239.1") == false)
                    {
                        await Wait(5);
                        lstMessage.Clear();
                        lstMessage.Add("خطای اتصال به شبکه");
                        ShowBalloon("لطفا اتصال به شبکه را چک نمایید", lstMessage);
                    }

                    while (await PingHost("185.105.239.1"))
                    {
                        SimCardBusiness firstSimCardBusiness = null;
                        //اگر نامبر صفر نباشد یعنی کاربر خواسته روی شماره ای خاص آگهی بزند
                        //اگر صفر باشد روی تمام سیم کارتها داخل حلقه وایل، آگهی ثبت می شود
                        if (numbers != null && numbers.Count == 0)
                        {
                            if (!(_cls?.NiazSetting?.AdvCountInDay > 0)) return;

                            _driver = RefreshDriver(_driver);

                            //     MessageBox.Show(_driver.WindowHandles.Count.ToString());
                            while (!_tokenSource.IsCancellationRequested)
                            {
                                var currentIp = "";
                                while (_cls?.NiazSetting.CountAdvInIp <= await
                                           AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                                               AdvertiseType.NiazKade))
                                {
                                    //ShowBalloon("پر شدن تعداد آگهی در " + await FindGateWay(),
                                    //    "سیستم در حال تعویض IP یا سایت می باشد");
                                   await SetGateway(await GetRandomGeteWay());
                                    currentIp = await GetLocalIpAddress();
                                    if (await FindGateWay() != IP_Store.IP_Mokhaberat.Value ||
                                        !(_cls?.NiazSetting.CountAdvInIp <= await
                                              AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                                                  AdvertiseType.NiazKade))) continue;
                                    if (await GoToNextSite(AdvertiseType.NiazKade,0)) continue;
                                    await ChangeIp();
                                    while (await GetLocalIpAddress() == null)
                                    {
                                        await Wait(10);
                                        //ShowBalloon("درحال اتصال...", "مودم مخابرات ریست شد. لطفا منتظر بمانید");
                                    }
                                    if (await GoToNextSite(AdvertiseType.NiazKade, 1)) continue;
                                }
                                //var simCard =
                                //    await SimCardBusiness.GetNextSimCardNumberAsync((short)AdvertiseType.NiazKade, (short)_cls.NiazSetting.AdvCountInDay, await GetLocalIpAddress());
                                //if (simCard == 0) break;

                                //firstSimCardBusiness = await SimCardBusiness.GetAsync(simCard);
                                if (firstSimCardBusiness is null) break;
                                var lastUseNiaz = firstSimCardBusiness.NextUseNiazKade;

                                firstSimCardBusiness.NextUseNiazKade = DateTime.Now.AddMinutes(30);
                                await firstSimCardBusiness.SaveAsync();
                                //اگر توکن نداشت سیمکارت عوض بشه
                                //var tt = await AdvTokensBusiness.GetToken(simCard, AdvertiseType.NiazKade);
                                //var hasToken = tt?.Token ?? null;
                                //if (string.IsNullOrEmpty(hasToken)) continue;

                                var startDayOfCurrentMonthOfDateShToMiladi = Calendar.StartDayOfPersianMonth();
                                var startDayOfNextMonthOfDateShToMiladi = Calendar.EndDayOfPersianMonth().AddDays(1);
                                //آمار آگهی های ثبت شده برای سیم کارت در ماه جاری
                                //var a1 = await AdvertiseLogBusiness.GetAllSpecialAsync(p =>
                                //    p.SimCardNumber == simCard && p.AdvType == AdvertiseType.NiazKade
                                //                               && (p.StatusCode == (short) StatusCode.Published
                                //                                   || p.StatusCode == (short) StatusCode.InPublishQueue)
                                //                               && p.DateM >= startDayOfCurrentMonthOfDateShToMiladi);
                                //var registeredAdvCountInMonth = a1.Count;
                                //if (registeredAdvCountInMonth >= _cls?.NiazSetting?.AdvCountInMonth)
                                //{
                                //    //تاریخ روز اول ماه شمسی بعد را تنظیم می کند چون تا سر ماه بعد دیگر نیازی به این سیم کارت نیست
                                //    firstSimCardBusiness.NextUseNiazKade = startDayOfNextMonthOfDateShToMiladi;
                                //    await firstSimCardBusiness.SaveAsync();
                                //    continue;
                                //}


                                ////آمار آگهی های ثبت شده امروز
                                //var currentDate = DateTime.Now.Date;
                                //var a2 = await AdvertiseLogBusiness.GetAllSpecialAsync(p =>
                                //    p.SimCardNumber == simCard && p.AdvType == AdvertiseType.NiazKade
                                //                               && (p.StatusCode == (short) StatusCode.Published
                                //                                   || p.StatusCode == (short) StatusCode.InPublishQueue
                                //                                   || p.StatusCode == (short) StatusCode.WaitForPayment)
                                //                               && p.DateM >= currentDate);
                                //var registeredAdvCountInDay = a2.Count;

                                //if (registeredAdvCountInDay >= _cls?.NiazSetting?.AdvCountInDay)
                                //{
                                //    //تاریخ فردا رو ست می کند چون تا فردا دیگه نیازی به این سیم کارت نیست
                                //    firstSimCardBusiness.NextUseNiazKade = DateTime.Today.AddDays(1);
                                //    await firstSimCardBusiness.SaveAsync();
                                //    continue;
                                //}

                                //if (await Login(simCard) == false)
                                //{
                                //    firstSimCardBusiness.NextUseNiazKade = lastUseNiaz;
                                //    await firstSimCardBusiness.SaveAsync();
                                //    continue;
                                //}

                                //اینجا به تعداد تنظیم شده در تنظیمات دیوار منهای تعداد ثبت شده قبلی، آگهی درج می کند
                                //for (var i = 0; i < _cls?.NiazSetting?.AdvCountInDay - registeredAdvCountInMonth; i++)
                                //{
                                //    var currentIp1 = "";
                                //    while (_cls?.NiazSetting.CountAdvInIp <= await
                                //               AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                                //                   AdvertiseType.NiazKade))
                                //    {
                                //        ShowBalloon("پر شدن تعداد آگهی در " + await FindGateWay(),
                                //            "سیستم در حال تعویض IP یا سایت می باشد");
                                //       await SetGateway(await GetRandomGeteWay());
                                //        currentIp1 = await GetLocalIpAddress();
                                //        var countAdvInIp1 = await
                                //            AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                                //                AdvertiseType.NiazKade);
                                //        if (!(_cls?.NiazSetting.CountAdvInIp <= countAdvInIp1)) continue;
                                //        if (await GoToNextSite(AdvertiseType.NiazKade,0)) continue;
                                //        await ChangeIp();
                                //        while (await GetLocalIpAddress() == null)
                                //        {
                                //            await Wait(10);
                                //            ShowBalloon("درحال اتصال...", "مودم مخابرات ریست شد. لطفا منتظر بمانید");
                                //        }
                                //        if (await GoToNextSite(AdvertiseType.NiazKade, 1)) continue;
                                //    }
                                //    var adv = await GetNextAdv(simCard);
                                //    if (adv == null) continue;
                                //    await RegisterAdv(adv);

                                //}
                            }

                        }
                        //اگر کاربر روی یک سیم کارت خاص می خواهد تعدادی آگهی درج کند
                        else
                        {
                            foreach (var number in numbers)
                            {
                                if (!await Login(number)) continue;
                                firstSimCardBusiness = await SimCardBusiness.GetAsync(number);
                                if (firstSimCardBusiness is null) return;
                                var tt = await AdvTokensBusiness.GetToken(number, AdvertiseType.NiazKade);
                                var hasToken = tt?.Token ?? null;
                                if (string.IsNullOrEmpty(hasToken)) return;
                                firstSimCardBusiness.NextUseNiazKade = DateTime.Now.AddMinutes(30);
                                await firstSimCardBusiness.SaveAsync();
                                //اینجا به تعداد آگهی های درج شده قبلی کاری ندارد و مستیم به تعدادی که کاربر گفته آگهی درج می کند
                                for (var i = 0; i < count; i++)
                                {
                                    var currentIp = "";
                                    while (_cls?.NiazSetting.CountAdvInIp <= await
                                               AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                                                   AdvertiseType.NiazKade))
                                    {
                                        //ShowBalloon("پر شدن تعداد آگهی در " + await FindGateWay(),
                                        //    "سیستم در حال تعویض IP یا سایت می باشد");
                                       await SetGateway(await GetRandomGeteWay());
                                        currentIp = await GetLocalIpAddress();
                                        if (await FindGateWay() != IP_Store.IP_Mokhaberat.Value ||
                                            !(_cls?.NiazSetting.CountAdvInIp <= await
                                                  AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                                                      AdvertiseType.NiazKade))) continue;
                                        await ChangeIp();
                                        while (await GetLocalIpAddress() == null)
                                        {
                                            await Wait(10);
                                            //ShowBalloon("درحال اتصال...", "مودم مخابرات ریست شد. لطفا منتظر بمانید");
                                        }
                                    }
                                    var adv = await GetNextAdv(number);
                                    if (adv == null) continue;
                                    await RegisterAdv(adv);
                                    //var countAdvInIp1 = await
                                    //    AdvertiseLogBusiness.GetAllAdvInDayFromIP(await GetLocalIpAddress(),
                                    //        AdvertiseType.NiazKade);
                                    //if (!(_cls?.NiazSetting.CountAdvInIp <= countAdvInIp1)) continue;
                                    //await GoToNextSite(AdvertiseType.NiazKade);
                                    //return;
                                }
                            }
                        }
                        

                        _driver.Navigate().GoToUrl("http://www.niazkade.com/panel/myAds");

                        await Wait(5);

                        //ShowBalloon("لطفا اتصال به شبکه را چک نمایید", "خطای اتصال به شبکه");
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    WebErrorLog.ErrorLogInstance.StartLog(ex);
                }
                finally
                {
                    monitor.Dispose();
                }

            });
            _semaphoreSlim.Release();
        }

        private async Task RegisterAdv(AdvertiseLogBusiness adv)
        {
            var ret = new ReturnedSaveFuncInfo();
            var monitor = new PerfMonitor();
            try
            {
                adv.AdvType = AdvertiseType.NiazKade;
                _driver = RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("http://www.niazkade.com/new");
                await Wait(4);





                //کلیک روی ساب کتگوری 1
                if (string.IsNullOrEmpty(adv.SubCategory1))
                    adv.SubCategory1 = _cls?.NiazSetting?.Category1;
                _driver.FindElements(By.ClassName("category-list")).FirstOrDefault(q => q.Text == adv.SubCategory1)?.Click();

                await Wait(2);




                //کلیک روی ساب کتگوری2
                if (string.IsNullOrEmpty(adv.SubCategory2))
                    adv.SubCategory2 = _cls?.NiazSetting?.Category2;
                _driver.FindElements(By.ClassName("category-list")).FirstOrDefault(q => q.Text == adv.SubCategory2)?.Click();
                await Wait();


                //درج عکسها

                _driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(adv.ImagesPath);



                //درج عنوان آگهی
                _driver.FindElement(By.Id("title")).SendKeys("");
                _driver.FindElement(By.Id("title")).SendKeys(adv.Title);
                await Wait();


                //انتخاب شهر
                _driver.FindElements(By.ClassName("vs__search")).FirstOrDefault()?.SendKeys(adv.State + "\n");
                await Wait(2);
                _driver.FindElements(By.ClassName("vs__search")).LastOrDefault()?.SendKeys(adv.City + "\n");
                //await Wait();




                //درج محتوای آگهی
                _driver.FindElement(By.Id("description"))
                    .SendKeys("");
                _driver.FindElement(By.Id("description"))
                    .SendKeys(adv.Content.Replace('(', '<').Replace(')', '>'));
                //await Wait();




                //درج قیمت
                _driver.FindElement(By.Id("price"))?.SendKeys("");
                _driver.FindElement(By.Id("price"))?.SendKeys(adv.Price.ToString());

                await Wait();

                _driver.FindElement(By.Id("phone_number"))?.SendKeys("");
                _driver.FindElement(By.Id("phone_number"))?.SendKeys("0" + adv.SimCardNumber.ToString());

                await Wait(2);
                //کلیک روی دکمه ثبت آگهی


                _driver.FindElements(By.TagName("button")).FirstOrDefault(q => q.Text == "ثبت اگهی")
                     ?.Click();

                //اگر آگهی با موفقیت ثبت شود لینک مدیریت آگهی ذخیره می شود
                await Wait(2);


                adv.URL = _driver.Url;
                adv.UpdateDesc = @"در صف انتشار";
                adv.AdvStatus = @"در صف انتشار";
                adv.StatusCode = (short)StatusCode.InPublishQueue;
                adv.AdvType = AdvertiseType.NiazKade;
                adv.IP = await GetLocalIpAddress();
                await adv.SaveAsync();
                var sim = await SimCardBusiness.GetAsync(adv.SimCardNumber);
                sim.DivarModified = DateTime.Now;
                await sim.SaveAsync();
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
        private List<Advertise> AdvertiseList { get; set; }
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

                #region GetPath

                string path = null;
                path = Path.Combine(Path.Combine(_cls?.NiazSetting?.PicPath, AdvertiseList[nextAdvIndex].AdvName) ==
                                    AdvertiseList[nextAdvIndex].AdvName
                    ? AdvertiseList[nextAdvIndex].RootPath
                    : _cls?.NiazSetting?.PicPath, AdvertiseList[nextAdvIndex].AdvName);
                newAdvertiseLogBusiness.Adv = path;
                #endregion


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
                newAdvertiseLogBusiness.ImagesPathList = GetNextImages(newAdvertiseLogBusiness.Adv,
                    _cls?.NiazSetting?.PicCountInPerAdv ?? 3);
                #endregion

                //قیمت آگهی دریافت می شود
                newAdvertiseLogBusiness.Price = AdvertiseList[nextAdvIndex].Price;

                #region GetCity

                var city = await CityBusiness.GetNextRandomCityAsync(newAdvertiseLogBusiness.MasterVisitorGuid,
                    AdvertiseType.NiazKade);
                newAdvertiseLogBusiness.City = city?.CityName ?? "مشهد";
                newAdvertiseLogBusiness.State = city?.State.StateName ?? "خراسان رضوی";
                #endregion

                #region GetCategory
                if (AdvertiseList[nextAdvIndex].NiazKadeCategories != null &&
                    AdvertiseList[nextAdvIndex].NiazKadeCategories.Count > 0)
                {
                    newAdvertiseLogBusiness.SubCategory1 = AdvertiseList[nextAdvIndex]?.NiazKadeCategories[0] ?? "";
                    newAdvertiseLogBusiness.SubCategory2 = AdvertiseList[nextAdvIndex]?.NiazKadeCategories[1] ?? "";
                }
                else
                {
                    newAdvertiseLogBusiness.SubCategory1 = null;
                    newAdvertiseLogBusiness.SubCategory2 = null;
                }
                #endregion

                return newAdvertiseLogBusiness;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return null;
            }

            finally { monitor.Dispose(); }
        }

        public async Task<bool> Login(long simCardNumber)
        {
            var monitor = new PerfMonitor();
            try
            {
                _driver = RefreshDriver(_driver);
                var js = (IJavaScriptExecutor)_driver;
                var simBusiness = await AdvTokensBusiness.GetToken(simCardNumber, AdvertiseType.NiazKade);
                var token = simBusiness?.Token;

                //   در صورتیکه توکن قبلا ثبت شده باشد لاگین می کند
                if (!string.IsNullOrEmpty(token))
                {
                    _driver.Navigate().GoToUrl("http://www.niazkade.com/panel/myAds");


                    js.ExecuteScript($"localStorage.setItem('user','{token}');");
                    _driver.Navigate().Refresh();

                    var linksElements = _driver.FindElements(By.ClassName("nav-link"))
                        .FirstOrDefault(q => q.Text == "خروج");
                    if (linksElements != null)
                    {
                        _driver.ExecuteJavaScript(@"alert('لاگین انجام شد');");
                        await Wait();
                        _driver.SwitchTo().Alert().Accept();
                        return true;
                    }
                }


                //اگر قبلا توکن نداشته و یا توکن اشتباه باشد وارد صفحه دریافت کد تائید لاگین می شود 
                _driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 2, 0);

                _driver.Navigate().GoToUrl("http://www.niazkade.com/panel/myAds");
                if (_driver.FindElements(By.ClassName("form-control")).Count > 0)
                    _driver.FindElement(By.ClassName("form-control")).SendKeys("0" + simCardNumber + "\n");

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
                    var linksElements = _driver.FindElements(By.ClassName("nav-link")).FirstOrDefault(q => q.Text == "خروج");
                    if (linksElements == null) continue;

                    token = js.ExecuteScript("return localStorage.getItem('user')").ToString();
                    if (simBusiness != null)
                        simBusiness.Token = token;
                    else
                    {
                        simBusiness = new AdvTokensBusiness
                        {
                            Guid = Guid.NewGuid(),
                            Token = token,
                            Modified = DateTime.Now,
                            Number = simCardNumber,
                            Type = AdvertiseType.NiazKade
                        };
                    }
                    await simBusiness?.SaveAsync();
                    _driver.ExecuteJavaScript(@"alert('لاگین انجام شد');");
                    await Wait();
                    _driver.SwitchTo().Alert().Accept();
                    return true;

                }

                var linksElements1 =
                    _driver.FindElements(By.ClassName("nav-link")).FirstOrDefault(q => q.Text == "خروج") ??
                    null;
                if (linksElements1 == null)
                {
                    var msg = $@"فرصت لاگین کردن به اتمام رسید. لطفا دقایقی بعد مجددا امتحان نمایید";
                    _driver.ExecuteJavaScript($"alert('{msg}');");
                    _driver.SwitchTo().Alert().Accept();
                    await Wait(3);
                }

                _driver.Navigate().GoToUrl("http://www.niazkade.com");
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
        private static List<string> GetNextImages(string advFullPath, int imgCount = 3)
        {
            var resultImages = new List<string>();
            var monitor = new PerfMonitor();
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

        public static async Task<NiazAdv> GetInstance()
        {
            await GetDataFromSetting();
            return _me ?? (_me = new NiazAdv());
        }
        private static async Task<SettingBusiness> GetDataFromSetting()
        {
            try
            {
                var res = await SettingBusiness.GetAllAsync();
                _cls = res.Count == 0 ? new SettingBusiness() : res[0];
                return _cls;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return null;
            }
        }
        private static NiazAdv _me;
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

        public async Task<List<NiazCityBusiness>> GetAllCityFromNiazKade()
        {
            var cities = new List<NiazCityBusiness>();
            var states = await StateBusiness.GetAllAsync();
            _driver = RefreshDriver(_driver);
            _driver.Navigate().GoToUrl("https://www.niazkade.com");
            try
            {
                foreach (var stateItem in states)
                {
                    await Wait();
                    _driver.FindElement(By.ClassName("vs__selected-options")).FindElement(By.TagName("input"))
                        ?.SendKeys(stateItem.StateName + "\n");
                    await Wait(2);
                    string name = null;

                    _driver.FindElements(By.ClassName("vs__selected-options")).LastOrDefault()?.Click();
                    await Wait();
                    var cc = _driver
                        .FindElement(By.ClassName("vs__city_select")).FindElements(By.TagName("ul")).ToArray();
                    if (!cc.Any())
                        continue;

                    foreach (var li in cc.ElementAt(0).Text)
                    {
                        if (name == "")
                            continue;
                        if (li == '\r' || li == '\n')
                        {
                            if (string.IsNullOrEmpty(name))
                                continue;
                            var a = new NiazCityBusiness
                            {
                                Guid = Guid.NewGuid(),
                                Modified = DateTime.Now,
                                CityName = name
                            };
                            cities.Add(a);
                            name = null;
                            continue;
                        }
                        name = name + li;
                    }

                    await Wait();
                    _driver.FindElement(By.ClassName("vs__selected-options")).FindElement(By.TagName("input"))
                        ?.SendKeys("");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return cities;
        }


        public async Task<bool> GetNumbersFromNiazKade(int pageCounter)
        {
            var list = new List<string>();
            var path = Path.Combine(Application.StartupPath, "NiazNumbers.txt");
            try
            {
                _driver.Navigate().GoToUrl("https://niazmandyha.ir/ads");
                await Wait(1);


                for (var i = 0; i < pageCounter; i++)
                {
                    await Wait();
                    var counter = _driver.FindElements(By.ClassName("card-body")).ToList();
                    await Wait();
                    for (var h = 1; h < counter.Count; h++)
                    {
                        await Wait();
                        _driver.FindElements(By.ClassName("card-body"))[h - 1]?.FindElements(By.ClassName("btn"))
                            .FirstOrDefault(q => q.Text == "مشاهده آگهی")?.Click();
                        await Wait();
                        var text = _driver.FindElements(By.TagName("a"))
                                       .FirstOrDefault(q => q.Text.Contains("09")) ?? null;

                        await Wait();
                        if (text == null) continue;
                        if (!string.IsNullOrEmpty(text.Text))
                        {
                            list.Add(text.Text.FixString());
                            await Wait();
                        }

                        _driver.Navigate().Back();
                    }

                    await Wait();
                    _driver.FindElements(By.ClassName("page-link")).LastOrDefault()?.Click();
                }


                File.WriteAllLines(path, list);

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
    }
}
