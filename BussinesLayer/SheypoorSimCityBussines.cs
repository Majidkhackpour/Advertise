using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
   public class SheypoorSimCityBussines:ISheypoorSimCity
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public Guid SimcardGuid { get; set; }
        public Guid StateGuid { get; set; }
        public Guid CityGuid { get; set; }

        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {

                    var a = Mappings.Default.Map<SheypoorSimCity>(this);
                    var res = _context.SheypoorSimCity.Save(a);
                    _context.Set_Save();

                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        public static async Task<List<SheypoorSimCityBussines>> GetAllAsync(Guid simGuid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.SheypoorSimCity.GetAllAsync(simGuid);
                return Mappings.Default.Map<List<SheypoorSimCityBussines>>(a);
            }
        }
        public static bool RemoveAll(List<SheypoorSimCityBussines> list)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var tt = Mappings.Default.Map<List<SheypoorSimCity>>(list);
                    var a = _context.SheypoorSimCity.RemoveAll(tt);
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
