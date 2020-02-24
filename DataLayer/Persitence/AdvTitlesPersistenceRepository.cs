using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class AdvTitlesPersistenceRepository : GenericRepository<AdvTitles>, IAdvTitlesRepository
    {
        private dbContext dbContext;

        public AdvTitlesPersistenceRepository(dbContext db) : base(db)
        {
            dbContext = db;
        }

        public List<AdvTitles> GetAllAsync(Guid guid)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var list = context.AdvTitles.AsNoTracking()
                        .Where(q => q.AdvGuid == guid)
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
