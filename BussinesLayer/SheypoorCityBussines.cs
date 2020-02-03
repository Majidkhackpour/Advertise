using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
   public class SheypoorCityBussines:ISheypoorCity
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public Guid StateGuid { get; set; }
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
            }
        }
    }
}
