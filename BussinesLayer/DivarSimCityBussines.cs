using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
   public class DivarSimCityBussines:IDivarSimCity
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public Guid SimcardGuid { get; set; }
        public Guid CityGuid { get; set; }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {

                    var a = Mappings.Default.Map<DivarSimCity>(this);
                    var res = _context.DivarSimCity.Save(a);
                    _context.Set_Save();

                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        public static async Task<List<DivarSimCityBussines>> GetAllAsync(Guid simGuid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.DivarSimCity.GetAllAsync(simGuid);
                return Mappings.Default.Map<List<DivarSimCityBussines>>(a);
            }
        }
        public static bool RemoveAll(List<DivarSimCityBussines> list)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var tt = Mappings.Default.Map<List<DivarSimCity>>(list);
                    var a = _context.DivarSimCity.RemoveAll(tt);
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
