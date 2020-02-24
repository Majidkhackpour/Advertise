using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
   public class AdvContentPersistenceRepository:GenericRepository<AdvContent>,IAdvContentRepository
    {
        private dbContext db;

        public AdvContentPersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }

        public List<AdvContent> GetAllAsync(Guid guid)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var list = context.AdvContents.AsNoTracking()
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
