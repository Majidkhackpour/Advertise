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
   public class ChatNumbersPersistenceRepository : GenericRepository<ChatNumbers>, IChatNumbersRepository
    {
        private dbContext db;

        public ChatNumbersPersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }

        public List<ChatNumbers> GetAll(AdvertiseType type)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var list = context.ChatNumbers.AsNoTracking()
                        .Where(q => q.Type == type)
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
