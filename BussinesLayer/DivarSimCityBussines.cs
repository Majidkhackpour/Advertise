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
       
    }
}
