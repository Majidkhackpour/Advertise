using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;

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
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {

                    var a = Mappings.Default.Map<Region>(this);
                    var res = _context.Region.Save(a);
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
