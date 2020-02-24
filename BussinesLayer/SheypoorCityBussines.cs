using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
   public class SheypoorCityBussines:ISheypoorCity
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public Guid StateGuid { get; set; }
        public bool Is_Checked { get; set; }
        public string StateName => StateBussiness.Get(StateGuid).Name;
        public static SheypoorCityBussines GetAsync(string city)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.SheypoorCity.GetAsync(city);
                return Mappings.Default.Map<SheypoorCityBussines>(a);
            }
        }
        public static SheypoorCityBussines GetAsync(Guid cityGuid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.SheypoorCity.Get(cityGuid);
                return Mappings.Default.Map<SheypoorCityBussines>(a);
            }
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<SheypoorCity>(this);
                    var res = _context.SheypoorCity.Save(a);
                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        public static async Task<List<SheypoorCityBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.SheypoorCity.GetAll();
                return Mappings.Default.Map<List<SheypoorCityBussines>>(a);
            }
        }
        public static async Task<List<SheypoorCityBussines>> GetAllAsync(string search)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.SheypoorCity.GetAllAsync(search);
                return Mappings.Default.Map<List<SheypoorCityBussines>>(a);
            }
        }
    }
}
