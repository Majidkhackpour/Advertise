using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Enums;
using DataLayer.Models;

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
                    var acc = contex.AdvTokens.AsNoTracking().SingleOrDefault(q => q.Number == number && q.Type == type);
                    return acc;
                }
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}
