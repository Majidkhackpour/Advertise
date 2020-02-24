using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class SimcardAdsPersistenceRepository : GenericRepository<SimcardAds>, ISimcardAdsRepository
    {
        private dbContext db;

        public SimcardAdsPersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }

        public List<SimcardAds> GetAllAsync(Guid simGuid)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var list = context.SimcardAds.AsNoTracking()
                        .Where(q => q.SimcardGuid == simGuid)
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
