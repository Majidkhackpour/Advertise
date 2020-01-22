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
    public class AdvertiseLogPersistenceRepository : GenericRepository<AdvertiseLog>, IAdvertiseLogRepository
    {
        private dbContext db;

        public AdvertiseLogPersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }

        public int GetAllAdvInDayFromIP(string ip, AdvertiseType type)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var date = DateConvertor.M2SH(DateTime.Now);
                    var acc = contex.AdvertiseLog.AsNoTracking().Count(q =>
                        q.IP == ip && q.AdvType == type && q.DateSabt == date);
                    return acc;
                }
            }
            catch (Exception exception)
            {
                return 0;
            }
        }
    }
}
