using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Enums;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class AdvTokensPersistenceRepository : GenericRepository<AdvTokens>, IAdvTokensRepository
    {
        private dbContext db;

        public AdvTokensPersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }

        public AdvTokens GetToken(long number, AdvertiseType type)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.AdvTokens.AsNoTracking()
                        .SingleOrDefault(q => q.Number == number && q.Type == type);
                    return acc;
                }
            }
            catch (ThreadAbortException)
            {
                return null;
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return null;
            }
        }

        public List<AdvTokens> GetAll(AdvertiseType type, long number)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var list = context.AdvTokens.AsNoTracking()
                        .Where(q => q.Type == type && q.Number == number)
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
