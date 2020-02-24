using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Enums;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class RegionPersistenceRepository : GenericRepository<Region>, IRegionRepository
    {
        private dbContext db;

        public RegionPersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }

        public List<Region> GetAllAsync(Guid cityGuid, AdvertiseType type)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var list = context.Region.AsNoTracking()
                        .Where(q => (q.CityGuid == cityGuid) && q.Type == type)?
                        .ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
                return null;
            }
        }
    }
}
