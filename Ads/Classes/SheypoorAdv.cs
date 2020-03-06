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
using ErrorHandler;
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
        public async Task StartRegisterAdv(SimcardBussines sim)
        {
            try
            {
                _driver = Utility.RefreshDriver(_driver);
                var tt = AdvTokensBussines.GetToken(sim.Number, AdvertiseType.Sheypoor).Token;
                if (string.IsNullOrEmpty(tt))
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
                //while (adv == null)
                //{
                //    adv = await GetNextAdv(sim);
                //}

                lstMessage.Clear();
                lstMessage.Add($"آگهی {adv.Adv} با {adv.ImagesPathList.Count + 1} تصویر دریافت شد");
                Utility.ShowBalloon("دریافت آگهی",
                    lstMessage);



                await RegisterAdv(adv);
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


        }


        public async Task RegisterAdv(AdvertiseLogBussines adv)
        {
            try
            {
                var counter = 0;
                adv.AdvType = AdvertiseType.Sheypoor;
                _driver = Utility.RefreshDriver(_driver);
                _driver.Navigate().GoToUrl("https://www.sheypoor.com/listing/new");
                await Utility.Wait();

                //کلیک کردن روی کتگوری اصلی
                _driver.FindElements(By.ClassName("form-select")).FirstOrDefault()?.Click();
                await Utility.Wait();

                //کلیک روی ساب کتگوری 1
                _driver.FindElements(By.ClassName("link")).FirstOrDefault(q => q.Text == adv.Category)?.Click();

                await Utility.Wait();

                //کلیک روی ساب کتگوری2
                _driver.FindElements(By.ClassName("link")).FirstOrDefault(q => q.Text == adv.SubCategory1)?.Click();


                //درج عکسها
                foreach (var item in adv.ImagesPathList)
                {
                    try
                    {
                        //درج عکسها
                        _driver.FindElement(By.ClassName("qq-upload-button-selector")).FindElement(By.TagName("input"))
                            .SendKeys(item);
                        await Utility.Wait();
                        // break;
                    }
                    catch
                    {
                    }
                }



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
                await Utility.Wait();
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

                await Utility.Wait();

                //انتخاب شهر
                await Utility.Wait();
                _driver.FindElements(By.ClassName("form-select")).LastOrDefault()?.Click();
                await Utility.Wait(2);
                var a = _driver.FindElements(By.ClassName("mode-district")).Any();
                if (a)
                {
                    _driver.FindElement(By.ClassName("mode-district")).FindElement(By.ClassName("link"))?.Click();
                    await Utility.Wait();
                    _driver.FindElement(By.ClassName("mode-city")).FindElement(By.ClassName("link"))?.Click();
                }

                await Utility.Wait(1);

                _driver.FindElements(By.TagName("li"))?.FirstOrDefault(q => q.Text.Contains(adv.State))?.Click();
                await Utility.Wait(1);

                var cc = _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text.Contains(adv.City));
                cc?.Click();
                var cty = SheypoorCityBussines.GetAsync(adv?.City);
                var simGuid = await SimcardBussines.GetAsync(adv.SimCardNumber);
                var cityList = await SheypoorSimCityBussines.GetAllAsync(simGuid.Guid);
                var rand = new Random().Next(0, cityList.Count);
                await Utility.Wait(1);
                var cityGuid = !string.IsNullOrEmpty(adv?.City) ? cty.Guid : cityList[rand].Guid;

                var lst = await RegionBussiness.GetAllAsync(cityGuid, AdvertiseType.Sheypoor);
                var regionList = lst?.ToList() ?? new List<RegionBussiness>();
                if (regionList.Count > 0)
                {
                    var rnd = new Random().Next(0, regionList.Count);
                    var regName = regionList[rnd].Name;
                    await Utility.Wait(2);
                    _driver.FindElements(By.TagName("li"))?.FirstOrDefault(q => q.Text == regName)
                        ?.Click();
                    adv.Region = regName;
                }
                // await Wait();


                //کلیک روی دکمه ثبت آگهی
                while (_driver.Url == "https://www.sheypoor.com/listing/new")
                {
                    counter++;
                    await Utility.Wait(2);
                    _driver.FindElements(By.TagName("button")).FirstOrDefault(q => q.Text == "ثبت آگهی")
                        ?.Click();
                    await Utility.Wait();
                    var box = _driver.FindElements(By.ClassName("box")).Any(q => q.Text.Contains("حساب کاربری"));
                    if (box) return;
                    if (counter < 60) continue;
                    adv.URL = "---";
                    adv.AdvStatus = @"خطای درج";
                    adv.StatusCode = StatusCode.InsertError;
                    adv.AdvType = AdvertiseType.Sheypoor;
                    adv.IP = await Utility.GetLocalIpAddress();
                    await adv.SaveAsync();
                    await Utility.Wait();
                    counter = 0;
                    _driver.Navigate().GoToUrl("https://www.sheypoor.com");
                    return;
                }



                //اگر آگهی با موفقیت ثبت شود لینک مدیریت آگهی ذخیره می شود
                await Utility.Wait();
                await Utility.Wait(2);
                counter = 0;
                adv.URL = await MakeUrl(_driver.Url);
                adv.AdvStatus = @"در صف انتشار";
                adv.StatusCode = StatusCode.InPublishQueue;
                adv.AdvType = AdvertiseType.Sheypoor;
                adv.IP = await Utility.GetLocalIpAddress();
                await adv.SaveAsync();
                await Utility.Wait();
                if (!_driver.Url.Contains(adv.URL))
                    _driver.Navigate().GoToUrl("https://www.sheypoor.com");

                //بعد از درج آگهی در دیتابیس لاگ می شود

            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
            finally { }
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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
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

                        await simBusiness.SaveAsync(AdvertiseType.Sheypoor, simBusiness.Number);
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
                await simBusiness.SaveAsync(AdvertiseType.Sheypoor, simBusiness.Number);

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

        private List<AdvertiseBussines> AdvertiseList { get; set; }
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
                // if (!(AdvertiseList[nextAdvIndex].Titles?.Count > 0)) return null;
                while (string.IsNullOrEmpty(newAdvertiseLogBusiness.Title) || newAdvertiseLogBusiness.Title == "---")
                {
                    var nextTitleIndex = new Random(DateTime.Now.Millisecond).Next(AllTitles.Count);
                    newAdvertiseLogBusiness.Title = AllTitles[nextTitleIndex].Title;
                }
                //if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Title)) return null;
                #endregion

                #region GetContent
                var AllContent = await AdvContentBussines.GetAllAsync(AdvertiseList[nextAdvIndex].Guid);
                //کانتنت آگهی دریافت می شود

                while (string.IsNullOrEmpty(newAdvertiseLogBusiness.Content) || newAdvertiseLogBusiness.Content == "---")
                {
                    var nextContentIndex = new Random(DateTime.Now.Millisecond).Next(AllContent.Count);
                    newAdvertiseLogBusiness.Content = AllContent[nextContentIndex].Content;
                }

                // if (string.IsNullOrEmpty(newAdvertiseLogBusiness.Content)) return null;

                #endregion

                #region FindImages


                while (newAdvertiseLogBusiness.ImagesPathList == null || newAdvertiseLogBusiness.ImagesPathList.Count == 0)
                {
                    //عکسهای آگهی دریافت می شود
                    newAdvertiseLogBusiness.ImagesPathList =
                        await GetNextImages(AdvertiseList[nextAdvIndex].Guid, cls?.MaxImgCount ?? 3);
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
                newAdvertiseLogBusiness.Price = decimal.Parse(AdvertiseList[nextAdvIndex].Price);

                while (string.IsNullOrEmpty(newAdvertiseLogBusiness.City) || newAdvertiseLogBusiness.City == "---")
                {
                    var Allcity = await SheypoorSimCityBussines.GetAllAsync(simGuid.Guid);
                    var rand = new Random().Next(0, Allcity.Count);
                    var city = Allcity[rand];
                    var cc = SheypoorCityBussines.GetAsync(city.CityGuid);
                    newAdvertiseLogBusiness.City = cc?.Name;
                    newAdvertiseLogBusiness.State = cc?.StateName;
                }


                var guid1 = AdvertiseList[nextAdvIndex]?.SheypoorCatGuid1 ?? Guid.Empty;
                newAdvertiseLogBusiness.Category = AdvCategoryBussines.Get(guid1)?.Name ?? "";

                var guid2 = AdvertiseList[nextAdvIndex]?.SheypoorCatGuid2 ?? Guid.Empty;
                newAdvertiseLogBusiness.SubCategory1 = AdvCategoryBussines.Get(guid2)?.Name ?? "";


                return newAdvertiseLogBusiness;
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
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
                    dayCount = cls?.DayCountForUpdateState ?? 10;

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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
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
                            Status = true,
                            StateGuid = stateItem.Guid
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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }


        private List<string> lstMessage = new List<string>();
        public async Task SendChat(List<string> msg, List<string> msg2, int count, string city, string cat1, string cat2, string cat3, string fileName, SimcardBussines sim)
        {
            try
            {
                //ورود به شیپور
                if (sim.Number == 0) return;
                var log = await Login(sim.Number);
                if (!log) return;
                _driver.Navigate().GoToUrl("https://www.sheypoor.com");
                await Utility.Wait();
                _driver.FindElements(By.ClassName("pull-left")).FirstOrDefault(q => q.Text == "همه آگهی‌ها")?.Click();
                await Utility.Wait();
                //انتخاب دسته بندی
                _driver.FindElements(By.ClassName("form-select")).FirstOrDefault()?.Click();
                await Utility.Wait(2);
                var list = _driver.FindElement(By.ClassName("cols-1-5")).FindElements(By.TagName("li")).ToList();
                await Utility.Wait(1);
                var find = false;
                foreach (var element in list)
                {
                    await Utility.Wait(1);
                    if (element == null) continue;
                    if (element.Text != cat2) continue;
                    element.FindElement(By.TagName("a")).Click();
                    find = true;
                }

                await Utility.Wait(1);

                if (!find)
                {
                    var more = _driver.FindElements(By.TagName("span")).Where(q => q.Text == "بیشتر").ToList();
                    if (more.Count <= 0) return;
                    more.LastOrDefault()?.Click();

                    var category = _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text == cat2);
                    if (category == null) return;
                    category?.Click();
                }

                //انتخاب شهر
                if (!string.IsNullOrEmpty(city))
                {
                    _driver.FindElements(By.ClassName("form-select")).LastOrDefault()?.Click();
                    await Utility.Wait();
                    var citybus = SheypoorCityBussines.GetAsync(city);
                    var statebus = StateBussiness.Get(citybus.StateGuid).Name;
                    _driver.FindElements(By.TagName("li"))?.FirstOrDefault(q => q.Text.Contains(statebus))
                        ?.Click();
                    await Utility.Wait(1);
                    var cc = _driver.FindElements(By.TagName("li")).FirstOrDefault(q => q.Text.Contains(city));
                    cc?.Click();
                    await Utility.Wait(1);
                }
                var ins = 0;

                for (var i = 0; ins <= count; i++)
                {
                    //رفتن به صفحه بعد در صورت نیاز
                    if (i >= 24 && i % 24 == 0)
                    {
                        _driver.FindElements(By.ClassName("button")).LastOrDefault(t => t.Text == "بعدی")
                            ?.Click();
                        await Utility.Wait(2);
                    }

                    var elems = _driver.FindElements(By.ClassName("serp-item")).ToList();
                    if (elems.Count == 0) return;
                    if (elems.Count == i) return;

                    //انتخاب آگهی
                    _driver.FindElements(By.ClassName("serp-item"))[i]?.Click();
                    //دریافت شماره
                    var elo = _driver.FindElements(By.ClassName("button"))
                       .FirstOrDefault(q => q.Text.Contains("تماس با"));

                    await Utility.Wait(1);
                    elo?.Click();
                    await Utility.Wait(2);
                    elo = _driver.FindElements(By.ClassName("button"))
                        .FirstOrDefault(q => q.Text.Contains("تماس با"));


                    var txt = elo?.Text.FixString();

                    //اگر شماره قبلا چت شده بود چت نکن
                    var allNumbers = ChatNumberBussines.GetAll(AdvertiseType.Sheypoor);
                    var n = 0;
                    foreach (var item in allNumbers)
                    {
                        if (txt.FixString() == item.Number)
                            n++;
                    }

                    if (n > 0)
                    {
                        _driver.Navigate().Back();
                        continue;
                    }

                    //  متن اول چت

                    var el = _driver.FindElements(By.TagName("span"))
                        .Any(q => q.Text.Contains("چت با"));
                    if (!el)
                    {
                        //چت ندارد. باید شماره ذخیره شود

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

                    await Utility.Wait(1);
                    var inp = _driver.FindElements(By.TagName("input")).ToList();
                    while (inp.Count == 1)
                    {
                        var ty = _driver.FindElements(By.TagName("h2")).Where(q => q.Text == "کد امنیتی").ToList();
                        if (ty.Count > 0) return;
                        _driver.FindElements(By.TagName("span"))
                            .FirstOrDefault(q => q.Text.Contains("چت با"))?.Click();
                        await Utility.Wait(2);
                        inp = _driver.FindElements(By.TagName("input")).ToList();
                    }

                    await Utility.Wait(3);
                    var rnd = new Random().Next(0, msg.Count);
                    var thread = new Thread(() => Clipboard.SetText(msg[rnd]));
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();

                    var t2 = _driver.FindElements(By.TagName("input")).FirstOrDefault();
                    t2?.Click();
                    await Utility.Wait();
                    t2?.SendKeys(OpenQA.Selenium.Keys.Control + "v");
                    t2?.SendKeys("" + '\n');
                    var thread1 = new Thread(Clipboard.Clear);
                    thread1.SetApartmentState(ApartmentState.STA);
                    thread1.Start();
                    await Utility.Wait(1);
                    // ذخیره شماره در جدول که بعدا کسی باهاش چت نکنه
                    var chatNumbers = new ChatNumberBussines()
                    {
                        Guid = Guid.NewGuid(),
                        Number = txt.FixString(),
                        DateSabt = DateConvertor.M2SH(DateTime.Now),
                        Status = true,
                        Type = AdvertiseType.Sheypoor
                    };
                    await chatNumbers.SaveAsync();
                    ins++;
                    _driver.Navigate().Back();
                    await Utility.Wait(2);
                    continue;
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
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
            }
        }

    }
}
