using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class DivarSimCityPersistenceRepository : GenericRepository<DivarSimCity>, IDivarSimCityRepository
    {
        private dbContext db;

        public DivarSimCityPersistenceRepository(dbContext _db) : base(_db)
        {
            db = _db;
        }

        public List<DivarSimCity> GetAllAsync(Guid simGuid)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var list = context.DivarSimCity.AsNoTracking()
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
