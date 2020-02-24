using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
   public class DivarCityBussines:IDivarCity
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public bool Is_Checked { get; set; }

        public static async Task<List<DivarCityBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.City.GetAll();
                return Mappings.Default.Map<List<DivarCityBussines>>(a);
            }
        }
        public static DivarCityBussines GetAsync(string city)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.City.GetAsync(city);
                return Mappings.Default.Map<DivarCityBussines>(a);
            }
        }
        public static DivarCityBussines GetAsync(Guid cityGuid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.City.Get(cityGuid);
                return Mappings.Default.Map<DivarCityBussines>(a);
            }
        }

        public static bool RemoveAll(List<DivarCityBussines> list)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var tt= Mappings.Default.Map<List<DivarCity>>(list);
                    var a = _context.City.RemoveAll(tt);
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
        public static bool Check_Name(string name)
        {
            using (var _context = new UnitOfWorkLid())
                return _context.City.Check_Name(name);
        }
        public static async Task SaveAsync(List<DivarCityBussines>lst)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var all = await GetAllAsync();
                    if (all.Count > 0)
                    {
                        if (!RemoveAll(all)) return;
                    }

                    foreach (var item in lst)
                    {
                        if (!Check_Name(item.Name)) return;
                        var a = Mappings.Default.Map<DivarCity>(item);
                        var res = _context.City.Save(a);
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
        public static async Task<List<DivarCityBussines>> GetAllAsync(string search)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.City.GetAllAsync(search);
                return Mappings.Default.Map<List<DivarCityBussines>>(a);
            }
        }
    }
}
