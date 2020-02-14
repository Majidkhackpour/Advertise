using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Enums;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
   public class AdvertiseLogBussines:IAdvertiseLog
   {
       public Guid Guid { get; set; } = Guid.NewGuid();
       public string DateSabt { get; set; } = DateConvertor.M2SH(DateTime.Now);
        public bool Status { get; set; }
        public string Title { get; set; }
        public string Content { get; set; } = "---";
        public long SimCardNumber { get; set; }
        public string State { get; set; } = "---";
        public string City { get; set; } = "---";
        public string Region { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string SubCategory1 { get; set; } = "---";
        public string SubCategory2 { get; set; } = "---";
        public string URL { get; set; } = "---";
        public string IP { get; set; } = "---";
        public int VisitCount { get; set; } = 0;
        public DateTime DateM { get; set; } = DateTime.Now;
        public StatusCode StatusCode { get; set; }
        public AdvertiseType AdvType { get; set; }
        public string ImagePath { get; set; } = "---";
        public string Adv { get; set; } = "---";
        public string AdvStatus { get; set; } = "---";
        public List<string> ImagesPathList { get; set; }
        public string TypeName => AdvType.GetDisplay();
        public string StatusName => StatusCode.GetDisplay();
        public static int GetAllAdvInDayFromIP(string ip)
        {
            using (var _context = new UnitOfWorkLid())
            {
                return _context.AdvertiseLog.GetAllAdvInDayFromIP(ip);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<AdvertiseLog>(this);
                    var res = _context.AdvertiseLog.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
            }
        }
        public static async Task<List<AdvertiseLogBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvertiseLog.GetAll();
                return Mappings.Default.Map<List<AdvertiseLogBussines>>(a);
            }
        }
        public static List<AdvertiseLogBussines> GetAllAsync(long number,short type,string search="")
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvertiseLog.GetAll(number);
                if (type == 0)
                    a = a.Where(q => q.AdvType == AdvertiseType.Divar).ToList();
                else if (type == 1)
                    a = a.Where(q => q.AdvType == AdvertiseType.Sheypoor).ToList();
                a = a.Where(q => q.Title.Contains(search) || q.City.Contains(search)).ToList();
                return Mappings.Default.Map<List<AdvertiseLogBussines>>(a);
            }
        }

        public static async Task<List<int>> GetAdvCountInSpecialMounthAsync(int dayCount, AdvertiseType type)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvertiseLog.GetAdvCountInSpecialMounthAsync(dayCount,type);
                return a;
            }
        }
        public static async Task<List<int>> GetPublishedAdvCountInSpecialMounthAsync(int dayCount, AdvertiseType type)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.AdvertiseLog.GetPublishedAdvCountInSpecialMounthAsync(dayCount, type);
                return a;
            }
        }
    }
}
