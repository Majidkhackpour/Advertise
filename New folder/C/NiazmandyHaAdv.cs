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
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using PacketParser;
using PacketParser.BusinessLayer;
using PacketParser.Entities;
using Cookie = System.Net.Cookie;
using Keys = System.Windows.Forms.Keys;


namespace AdvertiseApp.Classes
{
    public class NiazmandyHaAdv
    {
        #region Fields

        private IWebDriver _driver;
        private bool _noErrorInRegister = true;
        private readonly string _advRootPath;
        private int _maxImgForAdv;
        private static SettingBusiness cls;
        #endregion

        #region Ctors

        /// <summary>
        /// اگر مسیر آگهی داده نشود از مسیر پیش فرض فایل کانفیگ استفاده خواهد شد
        /// </summary>
        /// <param name="advRootPath"></param>
        /// <param name="maxImgForAnyAdv"></param>
        public NiazmandyHaAdv(string advRootPath = "", int maxImgForAnyAdv = 3)
        {
            _advRootPath = advRootPath;
            if (string.IsNullOrEmpty(_advRootPath)) _advRootPath = ConfigurationManager.AppSettings.Get("RootPath");
            _maxImgForAdv = maxImgForAnyAdv;

            _driver = Utility.RefreshDriver(_driver);

        }

        #endregion

        private List<string> lstMessage = new List<string>();
        private static string AdvRootPath => ConfigurationManager.AppSettings.Get("RootPath");
        public CancellationTokenSource _tokenSource;
        public SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1);
        private static NiazmandyHaAdv _me;
        public static async Task<NiazmandyHaAdv> GetInstance()
        {
            await GetDataFromSetting();
            return _me ?? (_me = new NiazmandyHaAdv());
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
        public async Task<bool> Login(List<long> simCardNumber)
        {
            var monitor = new PerfMonitor();
            try
            {

                foreach (var item in simCardNumber)
                {
                    Utility.CloseAllChromeWindows();
                    await GetInstance();
                    _driver = Utility.RefreshDriver(_driver);
                    var simBusiness = await AdvTokensBusiness.GetToken(item, AdvertiseType.NiazmandyHa);
                    var simcard = await SimCardBusiness.GetAsync(item);
                    var token = simBusiness?.Token;
                    _driver.Navigate().GoToUrl("https://niazmandyha.ir/panel/ads");
                    //   در صورتیکه توکن قبلا ثبت شده باشد لاگین می کند
                    if (!string.IsNullOrEmpty(token))
                    {
                        var cooc = _driver.Manage().Cookies.AllCookies;
                        if (cooc.Count == 3)
                            _driver.Manage().Cookies.DeleteCookieNamed(CookieStore.NiazmandyHaCookieName.Value);

                        _driver.Navigate().Refresh();
                        var a = new OpenQA.Selenium.Cookie(CookieStore.NiazmandyHaCookieName.Value, token);
                        _driver.Manage().Cookies.AddCookie(a);
                        _driver.Navigate().Refresh();

                        _driver.Navigate().GoToUrl("https://niazmandyha.ir/panel/ads");

                        var linksElements = _driver.FindElements(By.ClassName("black"))
                            .FirstOrDefault(q => q.Text == "خروج");
                        if (linksElements != null)
                        {
                            _driver.ExecuteJavaScript(@"alert('لاگین انجام شد');");
                            await Utility.Wait();
                            _driver.SwitchTo().Alert().Accept();
                            continue;
                        }

                        _driver.FindElement(By.Id("username")).SendKeys(simcard.UserName);
                        _driver.FindElement(By.Id("password")).SendKeys("0" + item + '\n');
                        var linksElement = _driver.FindElements(By.ClassName("black"))
                            .FirstOrDefault(q => q.Text == "خروج");
                        if (linksElement != null)
                        {
                            token = _driver.Manage().Cookies.AllCookies.LastOrDefault()?.Value ?? null;
                            simBusiness.Token = token;
                            await simBusiness?.SaveAsync();
                            _driver.ExecuteJavaScript(@"alert('لاگین انجام شد');");
                            await Utility.Wait();
                            _driver.SwitchTo().Alert().Accept();
                            continue;
                        }
                    }


                    //اگر قبلا توکن نداشته و یا توکن اشتباه باشد وارد صفحه ثبت نام می شود 
                    _driver.Manage().Timeouts().PageLoad = new TimeSpan(0, 2, 0);

                    _driver.FindElements(By.TagName("span"))
                        .FirstOrDefault(q => q.Text == "عضویت در نیازمندی ها")?.Click();
                    _driver.FindElement(By.Id("name")).SendKeys(simcard.OwnerName);
                    _driver.FindElement(By.Id("tel")).SendKeys("0" + item);
                    _driver.FindElement(By.Id("email")).SendKeys(simcard.UserName + "@Yahoo.com");
                    _driver.FindElement(By.Id("username")).SendKeys(simcard.UserName);
                    _driver.FindElement(By.Id("password")).SendKeys("0" + item);
                    _driver.FindElements(By.TagName("button")).FirstOrDefault(q => q.Text == "ثبت نام")?.Click();
                    await Utility.Wait();

                    var counter = 0;
                    while (_driver.Url == "https://niazmandyha.ir/login" || _driver.Url == "https://niazmandyha.ir/register")
                    {
                        //تا زمانی که لاگین اوکی نشده باشد این حلقه تکرار می شود
                        counter++;
                        if (counter >= 60)
                            break;
                        var name = await SimCardBusiness.GetOwnerNameAsync(item);
                        var message = $@"مالک: {name} \r\nشماره: {item}  \r\nلطفا لاگین نمائید ";
                        _driver.ExecuteJavaScript($"alert('{message}');");

                        await Utility.Wait(3);
                        try
                        {
                            _driver.SwitchTo().Alert().Accept();
                            await Utility.Wait(3);
                        }
                        catch
                        {
                            await Utility.Wait(15);
                        }


                        var linksElements = _driver.FindElements(By.ClassName("black"))
                            .FirstOrDefault(q => q.Text == "خروج");
                        if (linksElements == null) continue;

                        token = _driver.Manage().Cookies.AllCookies.LastOrDefault()?.Value ?? null;
                        if (string.IsNullOrEmpty(token)) continue;
                        if (simBusiness != null)
                            simBusiness.Token = token;
                        else
                        {
                            simBusiness = new AdvTokensBusiness
                            {
                                Guid = Guid.NewGuid(),
                                Token = token,
                                Modified = DateTime.Now,
                                Number = item,
                                Type = AdvertiseType.NiazmandyHa
                            };
                        }

                        await simBusiness?.SaveAsync();
                        _driver.ExecuteJavaScript(@"alert('لاگین انجام شد');");
                        await Utility.Wait();
                        _driver.SwitchTo().Alert().Accept();
                        Utility.CloseAllChromeWindows();
                        continue;
                    }


                    if (_driver.Url != "https://niazmandyha.ir/panel") return false;
                    token = _driver.Manage().Cookies.AllCookies.LastOrDefault()?.Value ?? null;
                    if (simBusiness != null)
                        simBusiness.Token = token;
                    else
                    {
                        simBusiness = new AdvTokensBusiness
                        {
                            Guid = Guid.NewGuid(),
                            Token = token,
                            Modified = DateTime.Now,
                            Number = item,
                            Type = AdvertiseType.NiazmandyHa
                        };
                    }
                    await simBusiness?.SaveAsync();
                    _driver.ExecuteJavaScript(@"alert('لاگین انجام شد');");
                    await Utility.Wait();
                    _driver.SwitchTo().Alert().Accept();
                    if (counter < 60) return true;

                    var linksElements1 = _driver?.FindElements(By.ClassName("black"))
                                             .FirstOrDefault(q => q.Text == "خروج") ??
                                         null;
                    if (linksElements1 == null)
                    {
                        var msg = $@"فرصت لاگین کردن به اتمام رسید. لطفا دقایقی بعد مجددا امتحان نمایید";
                        _driver.ExecuteJavaScript($"alert('{msg}');");
                        _driver.SwitchTo().Alert().Accept();
                        await Utility.Wait(3);
                    }

                    _driver.Navigate().GoToUrl("https://niazmandyha.ir");
                    await Utility.Wait();
                    simBusiness.Token = null;
                    await simBusiness.SaveAsync();
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

            finally { monitor.Dispose(); }
        }
        public async Task<List<NiazmandyHaCityBusiness>> GetAllCityFromNiazMandyHa()
        {
            var cities = new List<NiazmandyHaCityBusiness>();
            try
            {
                var states = await StateBusiness.GetAllAsync();
                states = states.OrderBy(q => q.StateName).ToList();
                _driver = Utility.RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://niazmandyha.ir/panel/ads");
                var a = await AdvTokensBusiness.GetAllAsync();
                var tok = a.FirstOrDefault(q => !string.IsNullOrEmpty(q.Token) && q.Type == AdvertiseType.NiazmandyHa);
                var sim = await SimCardBusiness.GetAsync(tok.Number);
                _driver.FindElement(By.Id("username")).SendKeys(sim.UserName);
                _driver.FindElement(By.Id("password")).SendKeys("0" + sim.Number + "\n");
                var linksElement = _driver.FindElements(By.ClassName("black"))
                    .FirstOrDefault(q => q.Text == "خروج");
                if (linksElement != null)
                {
                    _driver.Navigate().GoToUrl("https://niazmandyha.ir/panel/ads");
                    _driver.FindElement(By.ClassName("white")).Click();
                    foreach (var item in states)
                    {
                        await Utility.Wait();
                        _driver.FindElement(By.Name("state")).Click();
                        await Utility.Wait();
                        _driver.FindElements(By.TagName("option"))
                            .FirstOrDefault(q => q.Text.Contains(item.StateName))?.Click();
                        await Utility.Wait(1);
                        var tt = _driver.FindElement(By.Name("city")).FindElements(By.TagName("option")).ToList()
                            .Select(q => q?.Text);

                        cities.AddRange(from t in tt
                                        where t != "انتخاب کنید" && t != "همه شهرها"
                                        select new NiazmandyHaCityBusiness()
                                        { Guid = Guid.NewGuid(), Modified = DateTime.Now, CityName = t?.Trim() });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return cities;

        }
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
                while (await Utility.PingHost("185.105.239.1") == false)
                {
                    if (counter == 30)
                        await Utility.SetGateway(await Utility.GetRandomGeteWay());
                    await Utility.Wait(10);
                    lstMessage.Clear();
                    lstMessage.Add("خطای اتصال به شبکه");
                    Utility.ShowBalloon("لطفا اتصال به شبکه را چک نمایید", lstMessage);
                    counter++;
                }


                foreach (var t in numbers)
                {
                    //while (await Utility.PingHost("185.105.239.1"))
                    //{
                    var lstNum = new List<long> { t };
                    var tt = await AdvTokensBusiness.GetToken(t, AdvertiseType.NiazmandyHa);
                    var hasToken = tt?.Token ?? null;
                    if (string.IsNullOrEmpty(hasToken)) return;
                    if (!await Login(lstNum)) continue;
                    //اینجا به تعداد آگهی های درج شده قبلی کاری ندارد و مستیم به تعدادی که کاربر گفته آگهی درج می کند
                    for (var i = 0; i < count; i++)
                    {
                        //while (cls?.NiazmandyHaSetting.CountAdvInIp <= await
                        //           AdvertiseLogBusiness.GetAllAdvInDayFromIP(
                        //               await Utility.GetLocalIpAddress(),
                        //               AdvertiseType.NiazmandyHa))
                        //{
                        //    Utility.ShowBalloon("پر شدن تعداد آگهی در " + await Utility.FindGateWay(),
                        //        "سیستم در حال تعویض IP یا سایت می باشد");
                        //    await Utility.SetGateway(await Utility.GetRandomGeteWay());
                        //    var currentIp1 = await Utility.GetLocalIpAddress();
                        //    if (await Utility.FindGateWay() != IP_Store.IP_Mokhaberat.Value ||
                        //        !(cls?.NiazmandyHaSetting.CountAdvInIp <= await
                        //              AdvertiseLogBusiness.GetAllAdvInDayFromIP(
                        //                  await Utility.GetLocalIpAddress(),
                        //                  AdvertiseType.NiazmandyHa))) continue;
                        //    await Utility.ChangeIp();

                        //    while (await Utility.GetLocalIpAddress() == null)
                        //    {
                        //        if (counter == 30)
                        //            await Utility.SetGateway(await Utility.GetRandomGeteWay());
                        //        await Utility.Wait(10);
                        //        Utility.ShowBalloon("درحال اتصال...", "مودم مخابرات ریست شد. لطفا منتظر بمانید");
                        //        counter++;
                        //    }
                        //}

                        var adv = await GetNextAdv(t);
                        if (adv == null) continue;
                        await RegisterAdv(adv);
                        var title = await Utility.FindGateWay();
                        var co = await AdvertiseLogBusiness.GetAllAdvInDayFromIP(await Utility.GetLocalIpAddress(),
                            AdvertiseType.NiazmandyHa);
                        lstMessage.Clear();
                        lstMessage.Add($"نوع آگهی: نیازمندیها");
                        lstMessage.Add($"IP اینترنتی: {await Utility.GetLocalIpAddress()}");
                        lstMessage.Add($"GateWay: {await Utility.FindGateWay()}");
                        lstMessage.Add($"تعداد آگهی ارسال شده: {co}");

                        Utility.ShowBalloon(title, lstMessage);
                    }

                    lstNum.Clear();
                    //}

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

            SemaphoreSlim.Release();
        }


        private async Task RegisterAdv(AdvertiseLogBusiness adv)
        {
            var ret = new ReturnedSaveFuncInfo();
            var monitor = new PerfMonitor();
            try
            {
                adv.AdvType = AdvertiseType.NiazmandyHa;
                _driver = Utility.RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://niazmandyha.ir/newAd");
                await Utility.Wait();

                //درج عکسها
                _driver.FindElement(By.XPath("//input[@type='file']")).SendKeys(adv.ImagesPath);

                //درج عنوان آگهی
                _driver.FindElement(By.Name("title")).SendKeys("");
                _driver.FindElement(By.Name("title")).SendKeys(adv.Title);


                //کلیک کردن روی کتگوری اصلی
                _driver.FindElements(By.Name("category")).FirstOrDefault()?.Click();
                await Utility.Wait();

                //کلیک روی ساب کتگوری 1
                if (string.IsNullOrEmpty(adv.SubCategory1))
                    adv.SubCategory1 = cls?.NiazmandyHaSetting?.Category1;
                _driver.FindElements(By.TagName("option")).FirstOrDefault(q => q.Text == adv.SubCategory1)?.Click();

                await Utility.Wait(1);



                //درج قیمت

                _driver.FindElement(By.Name("cost"))?.SendKeys("");
                _driver.FindElement(By.Name("cost"))?.SendKeys(adv.Price.ToString());

                await Utility.Wait();


                //درج محتوای آگهی
                var thread = new Thread(() => Clipboard.SetText(adv.Content.Replace('(', '<').Replace(')', '>')));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();

                var t = _driver.FindElement(By.TagName("iframe"));
                t.Click();
                await Utility.Wait(1);
                t.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                var thread1 = new Thread(Clipboard.Clear);
                thread1.SetApartmentState(ApartmentState.STA);
                thread1.Start();


                //انتخاب شهر
                await Utility.Wait();
                _driver.FindElement(By.Name("state")).Click();
                await Utility.Wait();

                _driver.FindElements(By.TagName("option"))?.FirstOrDefault(q => q.Text == adv.State)?.Click();
                await Utility.Wait(1);

                _driver.FindElement(By.Name("city")).Click();
                await Utility.Wait();
                var cc = _driver.FindElements(By.TagName("option")).FirstOrDefault(q => q.Text == adv.City) ??
                         _driver.FindElements(By.TagName("option")).LastOrDefault();
                await Utility.Wait();
                cc?.Click();


                //کلیک روی دکمه ثبت آگهی
                await Utility.Wait(2);
                _driver.FindElements(By.TagName("button")).FirstOrDefault(q => q.Text == "ثبت آگهی")
                    ?.Click();



                //اگر آگهی با موفقیت ثبت شود لینک مدیریت آگهی ذخیره می شود
                await Utility.Wait();
                await Utility.Wait(2);

                adv.URL = await MakeUrl(_driver.Url);
                adv.UpdateDesc = @"در صف انتشار";
                adv.AdvStatus = @"در صف انتشار";
                adv.StatusCode = (short)StatusCode.InPublishQueue;
                adv.AdvType = AdvertiseType.NiazmandyHa;
                adv.IP = await Utility.GetLocalIpAddress();
                adv.SubCategory2 = "-";
                await adv.SaveAsync();
                var sim = await SimCardBusiness.GetAsync(adv.SimCardNumber);
                sim.NiazmandyHaModified = DateTime.Now;
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


        public async Task<string> MakeUrl(string url)
        {
            try
            {
                var len = url.Length;
                var shenase = "---";
                if (len > 37)
                    shenase = url.Remove(0, 37);
                var newURL = $"https://niazmandyha.ir/ads/{shenase}";
                return newURL;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return null;
            }
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
                path = Path.Combine(Path.Combine(cls?.NiazmandyHaSetting?.PicPath, AdvertiseList[nextAdvIndex].AdvName) ==
                                    AdvertiseList[nextAdvIndex].AdvName
                    ? AdvertiseList[nextAdvIndex].RootPath
                    : cls?.NiazmandyHaSetting?.PicPath, AdvertiseList[nextAdvIndex].AdvName);
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
                    cls?.NiazmandyHaSetting?.PicCountInPerAdv ?? 3);
                #endregion

                //قیمت آگهی دریافت می شود
                newAdvertiseLogBusiness.Price = AdvertiseList[nextAdvIndex].Price;

                #region GetCity

                var city = await CityBusiness.GetNextRandomCityAsync(newAdvertiseLogBusiness.MasterVisitorGuid,
                    AdvertiseType.NiazmandyHa);
                newAdvertiseLogBusiness.City = city?.CityName ?? "مشهد";
                newAdvertiseLogBusiness.State = city?.State.StateName ?? "خراسان رضوی";
                #endregion

                #region GetCategory
                if (AdvertiseList[nextAdvIndex].NiazmandyHaCategories != null &&
                    AdvertiseList[nextAdvIndex].NiazmandyHaCategories.Count > 0)
                {
                    newAdvertiseLogBusiness.SubCategory1 = AdvertiseList[nextAdvIndex]?.NiazmandyHaCategories[0] ?? "";
                    newAdvertiseLogBusiness.SubCategory2 = AdvertiseList[nextAdvIndex]?.NiazmandyHaCategories[1] ?? "";
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








        public async Task<bool> UpdateAllAdvStatus(int dayCount = 0)
        {
            if (SemaphoreSlim.CurrentCount == 0)
            {
                DialogResult result;
                result = MessageBox.Show("برنامه در حال اجرای فرایندی دیگر می باشد و در صورت تائید فرایند قبلی متوقف خواهد شد." + "\r\nآیا فرایند قبلی متوقف شود؟", "هشدار", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                    _tokenSource?.Cancel();
                else return false;
            }

            await SemaphoreSlim.WaitAsync();
            _tokenSource = new CancellationTokenSource();
            try
            {
                _driver = Utility.RefreshDriver(_driver);
                if (dayCount == 0)
                    dayCount = cls.NiazmandyHaSetting?.DayCountForUpdateState ?? 10;
                var lastWeek = DateTime.Now.AddDays(-dayCount);
                var lst = await AdvertiseLogBusiness.GetAllSpecialAsync(p =>
                    p.DateM > lastWeek && p.AdvType == AdvertiseType.NiazmandyHa);
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
                        //var sim = await SimCardBusiness.GetAsync(adv.SimCardNumber);
                        if (mobile != adv.SimCardNumber)
                        {
                            var lstNum = new List<long>();
                            lstNum.Add(adv.SimCardNumber);
                            var li = await AdvTokensBusiness.GetToken(adv.SimCardNumber, AdvertiseType.NiazmandyHa);
                            var tok = li.Token;
                            if (string.IsNullOrEmpty(tok)) continue;
                            mobile = adv.SimCardNumber;
                            var log = await Login(lstNum);
                            if (!log)
                            {
                                mobile = 0;
                                continue;
                            }

                            lstNum.Clear();
                        }
                        if (adv.URL == "---") continue;
                        var code = adv.URL.Remove(0, 27) ?? null;
                        await Utility.Wait();
                        var element1 = _driver.FindElements(By.ClassName("text-center")).Any(q => q.Text == code);
                        await Utility.Wait();

                        if (!element1 || string.IsNullOrEmpty(code))
                        {
                            var el = _driver.FindElements(By.ClassName("page-link")).Any();
                            if (!el)
                            {
                                adv.AdvStatus = "رد شده";
                                adv.UpdateDesc = "در انتظار تایید ادمین/ رد شده/ حذف شده";
                                adv.StatusCode = (short)StatusCode.Failed;
                                await adv.SaveAsync();
                                await InsertDataInAdvVisitLog(adv);
                                _driver.Navigate().Back();
                                continue;
                            }

                            _driver.FindElements(By.ClassName("page-link")).FirstOrDefault(q => q.Text.Contains("›"))?.Click();
                            var element11 = _driver.FindElements(By.ClassName("text-center")).Any(q => q.Text == code);
                            await Utility.Wait();
                            if (!element1)
                            {
                                adv.AdvStatus = "رد شده";
                                adv.UpdateDesc = "در انتظار تایید ادمین/ رد شده/ حذف شده";
                                adv.StatusCode = (short)StatusCode.Failed;
                                await adv.SaveAsync();
                                await InsertDataInAdvVisitLog(adv);
                                _driver.Navigate().Back();
                                continue;
                            }
                            await Utility.Wait();
                            adv.AdvStatus = "منتشر شده";
                            adv.UpdateDesc = "آگهی منتشر شده و در لیست آگهی های نیازمندی ها قرار گرفته است";

                            //var counter = _driver.FindElement(By.ClassName("stat-view"))?.Text.FixString() ?? "0";
                            //adv.VisitCount = counter.ParseToInt();
                            adv.StatusCode = (short)StatusCode.Published;
                            adv.AdvType = AdvertiseType.NiazmandyHa;
                            await adv.SaveAsync();
                            await InsertDataInAdvVisitLog(adv);
                            tryCount = 0;
                            _driver.Navigate().Back();
                            await Utility.Wait();
                            continue;
                        }


                        await Utility.Wait();
                        adv.AdvStatus = "منتشر شده";
                        adv.UpdateDesc = "آگهی منتشر شده و در لیست آگهی های نیازمندی ها قرار گرفته است";

                        //var counter = _driver.FindElement(By.ClassName("stat-view"))?.Text.FixString() ?? "0";
                        //adv.VisitCount = counter.ParseToInt();
                        adv.StatusCode = (short)StatusCode.Published;
                        adv.AdvType = AdvertiseType.NiazmandyHa;
                        await adv.SaveAsync();
                        await InsertDataInAdvVisitLog(adv);
                        tryCount = 0;
                        //_driver.Navigate().Back();
                        await Utility.Wait();

                    }
                    catch (Exception ex)
                    {
                        if (ex.Source != "WebDriver" && !_tokenSource.IsCancellationRequested)
                            WebErrorLog.ErrorLogInstance.StartLog(ex);
                        await Utility.Wait();
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
                    Type = AdvertiseType.NiazmandyHa
                };
                var res = await cls.SaveAsync();
                return !res.HasError;
            }
            catch
            {
                return false;
            }
        }

    }
}
