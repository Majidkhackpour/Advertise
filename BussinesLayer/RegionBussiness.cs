using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
  public  class RegionBussiness:IRegions
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public Guid CityGuid { get; set; }
        public AdvertiseType Type { get; set; }
        public static async Task<List<RegionBussiness>> GetAllAsync(Guid cityGuid,AdvertiseType type)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Region.GetAllAsync(cityGuid, type);
                return Mappings.Default.Map<List<RegionBussiness>>(a);
            }
        }
        public static async Task<List<RegionBussiness>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Region.GetAll();
                return Mappings.Default.Map<List<RegionBussiness>>(a);
            }
        }
        public static async Task SaveAsync(AdvertiseType type,List<RegionBussiness>lst)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var all = await GetAllAsync();
                    all = all.Where(q => q.Type == type).ToList();
                    if (all.Count > 0)
                    {
                        if (!RemoveAll(all)) return;
                    }

                    foreach (var item in lst)
                    {
                        var a = Mappings.Default.Map<Region>(item);
                        var res = _context.Region.Save(a);
                    }
                 
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        public static bool RemoveAll(List<RegionBussiness> list)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var tt = Mappings.Default.Map<List<Region>>(list);
                    var a = _context.Region.RemoveAll(tt);
                    _context.Set_Save();
                    return true;
                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
                return false;
            }
        }
    }
}
