using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Enums;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class AdvCategoryPersistenceRepository : GenericRepository<AdvCategory>, IAdvCategoryRepository
    {
        private dbContext db;

        public AdvCategoryPersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }

        public List<AdvCategory> GetAllAsync(Guid guid,AdvertiseType type)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var list = context.AdvCategory.AsNoTracking()
                        .Where(q => q.ParentGuid == guid && q.Type == type)
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
