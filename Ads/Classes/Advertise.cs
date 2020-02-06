////using PacketParser.Entities;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text.RegularExpressions;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using Exception = System.Exception;

//namespace Ads.Classes
//{
//    public class Advertise
//    {
//        #region Properties
//        private readonly bool _isEdit;
//        public string RootPath { get; set; }
//        public string AdvName { get; set; }
//        public string OldAdvName { get; set; }
//        public List<string> Images { get; set; }

//        public List<string> Titles { get; set; }

//        public string Content { get; set; }

//        public decimal Price { get; set; }

//        public List<string> DivarCategories { get; set; }
//        public List<string> SheypoorCategories { get; set; }
//        //ForUI
//        public bool Is_Checked { get; set; }

//        public string TitleString
//        {
//            get
//            {
//                string _str = null;
//                if (Titles.Count <= 0)
//                {
//                    return _str;
//                }

//                foreach (var item in Titles)
//                {
//                    _str = _str + " " + item + " ";
//                }

//                return _str;
//            }
//        }
//        #endregion

//        private string Pathh = Path.Combine(Application.StartupPath, "Advertise");
//        private Advertise(string advName, string rootPath = "")
//        {
//            _isEdit = true;
//            RootPath = string.IsNullOrEmpty(rootPath) ? Pathh : rootPath;
//            AdvName = advName;
//            if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return;
//            Titles = AdvTitles();
//            Content = AdvContent();
//            Price = AdvPrice();
//            Images = AdvImages();
//            DivarCategories = AdvDivarCat();
//            SheypoorCategories = AdvSheypoorCat();
//        }

//        public Advertise()
//        {
//            _isEdit = false;
//        }

//        private List<string> AdvTitles()
//        {
//            var ret = new List<string>();
//            try
//            {
//                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return ret;
//                var titlePath = Path.Combine(RootPath, AdvName);
//                titlePath = Path.Combine(titlePath, "Titles.txt");
//                if (!File.Exists(titlePath)) return ret;
//                ret.AddRange(File.ReadAllLines(titlePath));

//                return ret;
//            }
//            catch (Exception ex)
//            {
//                return ret;
//            }
//        }
//        private List<string> AdvDivarCat()
//        {
//            var ret = new List<string>();
//            try
//            {
//                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return ret;
//                var catPath = Path.Combine(RootPath, AdvName);
//                catPath = Path.Combine(catPath, "DivarCat.txt");
//                if (!File.Exists(catPath)) return ret;
//                ret.AddRange(File.ReadAllLines(catPath));

//                return ret;
//            }
//            catch (Exception ex)
//            {
//                return ret;
//            }
//        }
//        private List<string> AdvSheypoorCat()
//        {
//            var ret = new List<string>();
//            try
//            {
//                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return ret;
//                var catPath = Path.Combine(RootPath, AdvName);
//                catPath = Path.Combine(catPath, "SheypoorCat.txt");
//                if (!File.Exists(catPath)) return ret;
//                ret.AddRange(File.ReadAllLines(catPath));

//                return ret;
//            }
//            catch (Exception ex)
//            {
//                return ret;
//            }
//        }
//        private List<string> AdvImages()
//        {
//            var ret = new List<string>();
//            try
//            {
//                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return ret;

//                var picturesPath = Path.Combine(RootPath, AdvName);
//                picturesPath = Path.Combine(picturesPath, "Pictures");

//                if (!Directory.Exists(picturesPath)) return ret;
//                ret.AddRange(Directory.GetFiles(picturesPath, "*.jpg"));

//                return ret;
//            }

//            catch (Exception ex)
//            {
//                return ret;
//            }
//        }
//        private string AdvContent()
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return "";
//                var contentPath = Path.Combine(RootPath, AdvName);
//                contentPath = Path.Combine(contentPath, "Content.txt");
//                return File.Exists(contentPath) ? File.ReadAllText(contentPath).Trim() : "";
//            }
//            catch (Exception ex)
//            {
//                return "";
//            }
//        }
//        private decimal AdvPrice()
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(AdvName) || string.IsNullOrEmpty(RootPath)) return 0;
//                var pricePath = Path.Combine(RootPath, AdvName);
//                pricePath = Path.Combine(pricePath, "Price.txt");
//                var priceStr = "0";
//                if (File.Exists(pricePath)) priceStr = File.ReadAllText(pricePath).Trim();

//                var resultString = Regex.Match(priceStr, @"\d+").Value;
//                return string.IsNullOrEmpty(resultString) ? 0 : decimal.Parse(resultString);
//            }
//            catch (Exception ex)
//            {
//                return 0;
//            }
//        }
        
//        public async Task<bool> SaveAsync()
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(RootPath))
//                    RootPath = Path.Combine(Application.StartupPath, "Advertise");


//                var advPath = Path.Combine(RootPath, AdvName);

//                if (!_isEdit && Directory.Exists(advPath))
//                {
//                    MessageBox.Show("نام آگهی تکراری است");
//                    return false;
//                }

//                //ذخیره آگهی
//                await Task.Run(async () =>
//                {

//                    var picturePath = Path.Combine(advPath, "Pictures");
//                    if (!Directory.Exists(picturePath)) Directory.CreateDirectory(picturePath);
//                    //حذف عسکهای اضافی
//                    if (_isEdit)
//                    {
//                        //حذف عکسهایی که اکنون در لیست عکسهای آگهی نیستند
//                        //بعبارتی کاربر هنگام ویرایش آنها را حذف کرده است
//                        var oldImages = Utility.GetFiles(picturePath);
//                        if (oldImages?.Count > 0)
//                        {
//                            foreach (var image in oldImages)
//                            {
//                                if (Images.IndexOf(image) < 0)
//                                    File.Delete(image);
//                            }
//                        }
//                    }
//                    var contentPath = Path.Combine(advPath, "Content.txt");
//                    File.WriteAllText(contentPath, Content);

//                    var titlesPath = Path.Combine(advPath, "Titles.txt");
//                    if (Titles != null) File.WriteAllLines(titlesPath, Titles);

//                    var divCatPath = Path.Combine(advPath, "DivarCat.txt");
//                    if (DivarCategories != null) File.WriteAllLines(divCatPath, DivarCategories);

//                    var sheyCatPath = Path.Combine(advPath, "SheypoorCat.txt");
//                    if (SheypoorCategories != null) File.WriteAllLines(sheyCatPath, SheypoorCategories);

//                    var niazCatPath = Path.Combine(advPath, "NiazKadeCat.txt");

//                    var pricePath = Path.Combine(advPath, "Price.txt");
//                    File.WriteAllText(pricePath, Price.ToString(CultureInfo.CurrentCulture));



//                    //ذخیره عکسهای جدید کاربر
//                    if (Images?.Count > 0)
//                    {
//                        var oldImages = Utility.GetFiles(picturePath);
//                        foreach (var image in Images)
//                        {
//                            if (oldImages.IndexOf(image) < 0)
//                                File.Copy(image, Path.Combine(picturePath, $"{Guid.NewGuid()}.jpg"));
//                        }
//                    }

//                    //حذف آگهی قدیمی :در صورتیکه نام آگهی تغییر کند
//                    if (!string.IsNullOrEmpty(OldAdvName))
//                    {
//                        var deleteResult = await DeleteAsync(OldAdvName, RootPath);
//                        if (!deleteResult)
//                        {
//                            return;
//                        }
//                    }
//                });

//                //ویرایش نام آگهی در جدول آگهی های مجاز برای ویزیتور
//                //var visitorAdv = await VisitorAdvBusiness.GetAllAsync(OldAdvName);
//                //if (visitorAdv != null && visitorAdv.Count != 0)
//                //{
//                //    foreach (var item in visitorAdv)
//                //    {
//                //        item.AdvName = AdvName;
//                //        await item.SaveAsync();
//                //    }
//                //}
//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }
//        public static async Task<bool> DeleteAsync(string advName, string rootPath = "")
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(rootPath))
//                    rootPath = Path.Combine(Application.StartupPath, "Advertise");

                
               
//                var deletePath = Path.Combine(rootPath, advName);
               
               

//                await Task.Run(() =>
//                 {
//                     Directory.Delete(deletePath, true);
//                 });

//                //حذف داده در جدول آگهی های مجاز برای ویزیتور
//                //var visitorAdv = await VisitorAdvBusiness.GetAllAsync(advName);
//                //if (visitorAdv != null && visitorAdv.Count != 0)
//                //    foreach (var item in visitorAdv)
//                //       await VisitorAdvBusiness.RemoveByVisitorGuidAsync(item.VisitorGuid);

//                return true;
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//        }

//        public static async Task<Advertise> GetAsync(string directory,string rootPath="")
//        {
//            var ret = new List<Advertise>();
//            try
//            {
//                string lastFolderName = Path.GetFileName(directory);
//                    var newAdv = new Advertise(lastFolderName, rootPath);
//                    ret.Add(newAdv);

//                    return ret.SingleOrDefault(p =>
//                               p != null && p.Titles.Any() && !string.IsNullOrEmpty(p.Content) &&
//                               p.Content.Length > 50) ?? new Advertise();
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }
//        public static async Task<List<Advertise>> GetAllAsync(string rootPath = "")
//        {
//            var ret = new List<Advertise>();
//            try
//            {
//                if (string.IsNullOrEmpty(rootPath)) rootPath = Path.Combine(Application.StartupPath, "Advertise");
//                if (string.IsNullOrEmpty(rootPath) || !Directory.Exists(rootPath)) return ret;

//                    foreach (var dir in Directory.GetDirectories(rootPath))
//                    {
//                        string lastFolderName = Path.GetFileName(dir);
//                        var newAdv = new Advertise(lastFolderName, rootPath);
//                        ret.Add(newAdv);
//                    }

//                return ret.Where(p=>p!=null && p.Titles.Any() && !string.IsNullOrEmpty(p.Content) && p.Content.Length>50)?.ToList ()??new List<Advertise>();
//            }
//            catch (Exception ex)
//            {
//                return ret;
//            }
//        }
//        public static Advertise GetByAdvName(string advName, string rootPath = "")
//        {
//            try
//            {
//                if (string.IsNullOrEmpty(advName)) return null;

//                if (string.IsNullOrEmpty(rootPath)) rootPath = Path.Combine(Application.StartupPath, "Advertise");

//                if (string.IsNullOrEmpty(rootPath) || !Directory.Exists(Path.Combine(rootPath, advName))) return null;
//                var resultAdv = new Advertise(advName);
//                return resultAdv;
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }
//        public static async Task<List<Advertise>> GetAllAsync(string search, CancellationTokenSource token)
//        {
//            const string spliterList = " \\\r\n\t)(-_=+%$#@*|><`~':.?؟،";
//            try
//            {
//                if (string.IsNullOrEmpty(search))
//                    search = "";
//                token.Cancel();
//                List<Advertise> res = null;
//                res = await GetAllAsync();

//                var searchItems = search.Split(spliterList.ToCharArray())?.ToList();
//                if (searchItems?.Count > 0)
//                    foreach (var item in searchItems)
//                    {
//                        token.Cancel();
//                        if (!string.IsNullOrEmpty(item) && item.Trim() != "")
//                        {
//                            token.Cancel();
//                            res = res.Where(x =>
//                                    (x.AdvName.Contains(item)) || (x.Titles.Contains(item)) ||
//                                    (x.Content.Contains(item)))
//                                ?.ToList();
//                            token.Cancel();
//                        }
//                        token.Cancel();
//                    }
//                token.Cancel();
//                res = res?.OrderBy(o => o.AdvName).ToList();
//                token.Cancel();
//                return res;
//            }
//            catch (OperationCanceledException)
//            { return null; }
//            catch (Exception ex)
//            {
//                return new List<Advertise>();
//            }
//        }
//    }
//}
