//using PacketParser.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using EntityCache.Business.Advertise;
using PacketParser.BusinessLayer;
using PacketParser.Entities;
using static AdvertiseApp.Classes.Utility;
using Exception = System.Exception;

namespace AdvertiseApp.Classes
{
    public class Advertise
    {
        #region Properties
        private readonly bool _isEdit;
        public string RootPath { get; set; }
        public string AdvName { get; set; }
        public string OldAdvName { get; set; }
        public List<string> Images { get; set; }

        public List<string> Titles { get; set; }

        public string Content { get; set; }

        public decimal Price { get; set; }

        public bool PishNevis { get; set; }
        public List<string> DivarCategories { get; set; }
        public List<string> SheypoorCategories { get; set; }
        public List<string> NiazKadeCategories { get; set; }
        public List<string> NiazmandyHaCategories { get; set; }
        //ForUI
        public bool Is_Checked { get; set; }

        public string TitleString
        {
            get
            {
                string _str = null;
                if (Titles.Count <= 0)
                {
                    return _str;
                }

                foreach (var item in Titles)
                {
                    _str = _str + " " + item + " ";
                }

                return _str;
            }
        }
        #endregion

        private Advertise(string advName, string rootPath = "")
        {
            _isEdit = true;
            RootPath = string.IsNullOrEmpty(rootPath) ? ConfigurationManager.AppSettings.Get("RootPath") : rootPath;
            AdvName = advName;
            if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return;
            Titles = AdvTitles();
            Content = AdvContent();
            Price = AdvPrice();
            Images = AdvImages();
            PishNevis = _pishNevis();
            DivarCategories = AdvDivarCat();
            SheypoorCategories = AdvSheypoorCat();
            NiazKadeCategories = AdvNiazKadeCat();
            NiazmandyHaCategories = AdvNiazmandyHaCat();
        }

        public Advertise()
        {
            _isEdit = false;
        }

        private List<string> AdvTitles()
        {
            var ret = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return ret;
                var titlePath = Path.Combine(RootPath, AdvName);
                titlePath = Path.Combine(titlePath, "Titles.txt");
                if (!File.Exists(titlePath)) return ret;
                ret.AddRange(File.ReadAllLines(titlePath));

                return ret;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return ret;
            }
        }
        private List<string> AdvDivarCat()
        {
            var ret = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return ret;
                var catPath = Path.Combine(RootPath, AdvName);
                catPath = Path.Combine(catPath, "DivarCat.txt");
                if (!File.Exists(catPath)) return ret;
                ret.AddRange(File.ReadAllLines(catPath));

                return ret;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return ret;
            }
        }
        private List<string> AdvSheypoorCat()
        {
            var ret = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return ret;
                var catPath = Path.Combine(RootPath, AdvName);
                catPath = Path.Combine(catPath, "SheypoorCat.txt");
                if (!File.Exists(catPath)) return ret;
                ret.AddRange(File.ReadAllLines(catPath));

                return ret;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return ret;
            }
        }
        private List<string> AdvNiazKadeCat()
        {
            var ret = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return ret;
                var catPath = Path.Combine(RootPath, AdvName);
                catPath = Path.Combine(catPath, "NiazKadeCat.txt");
                if (!File.Exists(catPath)) return ret;
                ret.AddRange(File.ReadAllLines(catPath));

                return ret;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return ret;
            }
        }
        private List<string> AdvNiazmandyHaCat()
        {
            var ret = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return ret;
                var catPath = Path.Combine(RootPath, AdvName);
                catPath = Path.Combine(catPath, "NiazmandyHaCat.txt");
                if (!File.Exists(catPath)) return ret;
                ret.AddRange(File.ReadAllLines(catPath));

                return ret;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return ret;
            }
        }
        private List<string> AdvImages()
        {
            var ret = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return ret;

                var picturesPath = Path.Combine(RootPath, AdvName);
                picturesPath = Path.Combine(picturesPath, "Pictures");

                if (!Directory.Exists(picturesPath)) return ret;
                ret.AddRange(Directory.GetFiles(picturesPath, "*.jpg"));

                return ret;
            }

            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return ret;
            }
        }
        private string AdvContent()
        {
            try
            {
                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return "";
                var contentPath = Path.Combine(RootPath, AdvName);
                contentPath = Path.Combine(contentPath, "Content.txt");
                return File.Exists(contentPath) ? File.ReadAllText(contentPath).Trim() : "";
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return "";
            }
        }
        private decimal AdvPrice()
        {
            try
            {
                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return 0;
                var pricePath = Path.Combine(RootPath, AdvName);
                pricePath = Path.Combine(pricePath, "Price.txt");
                var priceStr = "0";
                if (File.Exists(pricePath)) priceStr = File.ReadAllText(pricePath).Trim();

                var resultString = Regex.Match(priceStr, @"\d+").Value;
                return string.IsNullOrEmpty(resultString) ? 0 : decimal.Parse(resultString);
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return 0;
            }
        }

        private bool _pishNevis()
        {
            try
            {
                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return false;
                var _Path = Path.Combine(RootPath, AdvName);
                _Path = Path.Combine(_Path, "PishNevis.txt");
                if (File.Exists(_Path)) return true;
                return false;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return false;
            }
        }

        public async Task<ReturnedSaveFuncInfo> SaveAsync()
        {
            var monitor = new PerfMonitor();
            var ret = new ReturnedSaveFuncInfo();
            try
            {
                if (string.IsNullOrEmpty(RootPath))
                    RootPath = ConfigurationManager.AppSettings.Get("RootPath");

                #region validation

                if (string.IsNullOrEmpty(AdvName))
                    ret.AddReturnedValue(ReturnedState.Error, "آگهی فاقد نام است");
                if (string.IsNullOrEmpty(Content))
                    ret.AddReturnedValue(ReturnedState.Error, "آگهی فاقد محتوا می باشد");
                if (string.IsNullOrEmpty(Price.ToString(CultureInfo.CurrentCulture)))
                    ret.AddReturnedValue(ReturnedState.Error, "آگهی فاقد قیمت می باشد");
                if (Titles?.Count < 1)
                    ret.AddReturnedValue(ReturnedState.Error, "آگهی فاقد عنوان است");
                if (Images?.Count < 1)
                    ret.AddReturnedValue(ReturnedState.Error, "آگهی فاقد تصویر است");

                #endregion

                var advPath = Path.Combine(RootPath, AdvName);

                if (!_isEdit && Directory.Exists(advPath))
                    ret.AddReturnedValue(ReturnedState.Error, "نام آگهی تکراری است");

                if (ret.HasError) return ret;

                //ذخیره آگهی
                await Task.Run(() =>
                {

                    var picturePath = Path.Combine(advPath, "Pictures");
                    if (!Directory.Exists(picturePath)) Directory.CreateDirectory(picturePath);
                    //حذف عسکهای اضافی
                    if (_isEdit)
                    {
                        //حذف عکسهایی که اکنون در لیست عکسهای آگهی نیستند
                        //بعبارتی کاربر هنگام ویرایش آنها را حذف کرده است
                        var oldImages = GetFiles(picturePath);
                        if (oldImages?.Count > 0)
                        {
                            foreach (var image in oldImages)
                            {
                                if (Images.IndexOf(image) < 0)
                                    File.Delete(image);
                            }
                        }
                    }
                    var contentPath = Path.Combine(advPath, "Content.txt");
                    File.WriteAllText(contentPath, Content);

                    var titlesPath = Path.Combine(advPath, "Titles.txt");
                    if (Titles != null) File.WriteAllLines(titlesPath, Titles);

                    var divCatPath = Path.Combine(advPath, "DivarCat.txt");
                    if (DivarCategories != null) File.WriteAllLines(divCatPath, DivarCategories);

                    var sheyCatPath = Path.Combine(advPath, "SheypoorCat.txt");
                    if (SheypoorCategories != null) File.WriteAllLines(sheyCatPath, SheypoorCategories);

                    var niazCatPath = Path.Combine(advPath, "NiazKadeCat.txt");
                    if (NiazKadeCategories != null) File.WriteAllLines(niazCatPath, NiazKadeCategories);
                    var niazmandyHaCatPath = Path.Combine(advPath, "NiazmandyHaCat.txt");
                    if (NiazmandyHaCategories != null) File.WriteAllLines(niazmandyHaCatPath, NiazmandyHaCategories);

                    var pricePath = Path.Combine(advPath, "Price.txt");
                    File.WriteAllText(pricePath, Price.ToString(CultureInfo.CurrentCulture));

                    var pishNevisPath = Path.Combine(advPath, "PishNevis.txt");
                    if (PishNevis) File.WriteAllText(pishNevisPath, "True");
                    else
                        if (File.Exists(pishNevisPath))
                            File.Delete(pishNevisPath);



                    //ذخیره عکسهای جدید کاربر
                    if (Images?.Count > 0)
                    {
                        var oldImages = GetFiles(picturePath);
                        foreach (var image in Images)
                        {
                            if (oldImages.IndexOf(image) < 0)
                                File.Copy(image, Path.Combine(picturePath, $"{Guid.NewGuid()}.jpg"));
                        }
                    }

                    //حذف آگهی قدیمی :در صورتیکه نام آگهی تغییر کند
                    if (!string.IsNullOrEmpty(OldAdvName))
                    {
                        var deleteResult = DeleteAsync(OldAdvName, RootPath);
                        if (deleteResult.Result.HasError)
                        {
                            foreach (var error in deleteResult.Result.ErrorList)
                                ret.AddReturnedValue(ReturnedState.Error, error);
                        }
                    }
                });

                //ویرایش نام آگهی در جدول آگهی های مجاز برای ویزیتور
                var visitorAdv = await VisitorAdvBusiness.GetAllAsync(OldAdvName);
                if (visitorAdv != null && visitorAdv.Count != 0)
                {
                    foreach (var item in visitorAdv)
                    {
                        item.AdvName = AdvName;
                        await item.SaveAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                ret.AddReturnedValue(ex);
            }
            finally { monitor.Dispose(); }
            return ret;
        }
        public static async Task<ReturnedSaveFuncInfo> DeleteAsync(string advName, string rootPath = "")
        {
            var monitor = new PerfMonitor();
            var ret = new ReturnedSaveFuncInfo();
            try
            {
                if (string.IsNullOrEmpty(rootPath))
                    rootPath = ConfigurationManager.AppSettings.Get("RootPath");

                #region validation
                if (string.IsNullOrEmpty(advName))
                {
                    ret.AddReturnedValue(ReturnedState.Error, "نام آگهی مشخص نشده است");
                    return ret;
                }
                if (string.IsNullOrEmpty(rootPath))
                {
                    ret.AddReturnedValue(ReturnedState.Error, "مسیر آگهی مشخص نشده است");
                    return ret;
                }
                var deletePath = Path.Combine(rootPath, advName);
                if (!Directory.Exists(deletePath))
                {
                    ret.AddReturnedValue(ReturnedState.Warning, "آگهی مورد نظر برای حذف، وجود ندارد");
                    return ret;
                }
                #endregion

                await Task.Run(() =>
                 {
                     Directory.Delete(deletePath, true);
                 });

                //حذف داده در جدول آگهی های مجاز برای ویزیتور
                var visitorAdv = await VisitorAdvBusiness.GetAllAsync(advName);
                if (visitorAdv != null && visitorAdv.Count != 0)
                    foreach (var item in visitorAdv)
                       await VisitorAdvBusiness.RemoveByVisitorGuidAsync(item.VisitorGuid);

                return ret;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                ret.AddReturnedValue(ex);
            }
            finally { monitor.Dispose(); }
            return ret;
        }
        /// <summary>
        /// اگر مسیر داده نشود از مسیر پیش فرض استفاده خواهد شد
        /// </summary>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        public static async Task<List<Advertise>> GetAllAsync(string rootPath = "", Guid visitorGuid = new Guid())
        {
            var ret = new List<Advertise>();
            try
            {
                if (string.IsNullOrEmpty(rootPath)) rootPath = ConfigurationManager.AppSettings.Get("RootPath");
                if (string.IsNullOrEmpty(rootPath) || !Directory.Exists(rootPath)) return ret;

                if (visitorGuid != new Guid())
                {
                    var lst = await VisitorAdvBusiness.GetAllAsync(visitorGuid);
                    var visitorAdvList = lst.Select(p => p.AdvName).ToList();
                    foreach (var dir in Directory.GetDirectories(rootPath))
                    {
                        var lastFolderName = Path.GetFileName(dir);
                        if (visitorAdvList.IndexOf(lastFolderName) >= 0)
                        {
                            var newAdv = new Advertise(lastFolderName, rootPath);
                            ret.Add(newAdv);
                        }
                    }
                }
                else
                {
                    foreach (var dir in Directory.GetDirectories(rootPath))
                    {
                        string lastFolderName = Path.GetFileName(dir);
                        var newAdv = new Advertise(lastFolderName, rootPath);
                        ret.Add(newAdv);
                    }
                }

                return ret.Where(p=>p!=null && p.Titles.Any() && !string.IsNullOrEmpty(p.Content) && p.Content.Length>50)?.ToList ()??new List<Advertise>();
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return ret;
            }
        }
        public static Advertise GetByAdvName(string advName, string rootPath = "")
        {
            try
            {
                if (string.IsNullOrEmpty(advName)) return null;

                if (string.IsNullOrEmpty(rootPath)) rootPath = ConfigurationManager.AppSettings.Get("RootPath");

                if (string.IsNullOrEmpty(rootPath) || !Directory.Exists(Path.Combine(rootPath, advName))) return null;
                var resultAdv = new Advertise(advName);
                return resultAdv;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return null;
            }
        }
        public static async Task<List<Advertise>> GetAllAsync(string search, CancellationTokenSource token, int type=0)
        {
            /*
             * type=0 => All
             * type=1 => TaeidShode
             * type=2 => PishNevis
             */
            var monitor = new PerfMonitor();
            const string spliterList = " \\\r\n\t)(-_=+%$#@*|><`~':.?؟،";
            try
            {
                if (string.IsNullOrEmpty(search))
                    search = "";
                token.Cancel();
                List<Advertise> res = null;
                res = await GetAllAsync();
                switch (type)
                {
                    case 1:
                        res = res.Where(q => q.PishNevis == false).ToList();
                        break;
                    case 2:
                        res = res.Where(q => q.PishNevis).ToList();
                        break;
                }

                var searchItems = search.Split(spliterList.ToCharArray())?.ToList();
                if (searchItems?.Count > 0)
                    foreach (var item in searchItems)
                    {
                        token.Cancel();
                        if (!string.IsNullOrEmpty(item) && item.Trim() != "")
                        {
                            token.Cancel();
                            res = res.Where(x =>
                                    (x.AdvName.Contains(item)) || (x.Titles.Contains(item)) ||
                                    (x.Content.Contains(item)))
                                ?.ToList();
                            token.Cancel();
                        }
                        token.Cancel();
                    }
                token.Cancel();
                res = res?.OrderBy(o => o.AdvName).ToList();
                token.Cancel();
                return res;
            }
            catch (OperationCanceledException)
            { return null; }
            catch (Exception ex)
            {
                WebErrorLog.ErrorLogInstance.StartLog(ex);
                return new List<Advertise>();
            }
            finally { monitor.Dispose(); }
        }
    }
}
