using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

namespace BussinesLayer
{
    public class SimcardAdsBussines : ISimcardAds
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public Guid SimcardGuid { get; set; }
        public Guid Advertise { get; set; }

        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {

                    var a = Mappings.Default.Map<SimcardAds>(this);
                    var res = _context.SimcardAds.Save(a);
                    _context.Set_Save();

                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
            }
        }
        public static async Task<List<SimcardAdsBussines>> GetAllAsync(Guid simGuid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.SimcardAds.GetAllAsync(simGuid);
                return Mappings.Default.Map<List<SimcardAdsBussines>>(a);
            }
        }
        public static bool RemoveAll(List<SimcardAdsBussines> list)
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var tt = Mappings.Default.Map<List<SimcardAds>>(list);
                    var a = _context.SimcardAds.RemoveAll(tt);
                    _context.Set_Save();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

    }
}
