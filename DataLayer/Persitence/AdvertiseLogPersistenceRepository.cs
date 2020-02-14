using System;
using System.Collections.Generic;
using System.Linq;
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

        public int GetAllAdvInDayFromIP(string ip)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var date = DateConvertor.M2SH(DateTime.Now);
                    var acc = contex.AdvertiseLog.AsNoTracking().Count(q =>
                        q.IP == ip && q.DateSabt == date);
                    return acc;
                }
            }
            catch (Exception exception)
            {
                return 0;
            }
        }

        public List<AdvertiseLog> GetAll(long number)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.AdvertiseLog.AsNoTracking().Where(q =>
                        q.SimCardNumber == number).ToList();
                    return acc;
                }
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public List<int> GetAdvCountInSpecialMounthAsync(int dayCount, AdvertiseType type)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var lst = new List<int>();
                    for (var i = DateTime.Now.AddDays(-dayCount); i >= DateTime.Now; i = DateTime.Now.AddDays(1))
                    {
                        var acc = contex.AdvertiseLog.AsNoTracking().Count(q =>
                            q.DateM>=i);
                    }

                    return lst;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<int> GetPublishedAdvCountInSpecialMounthAsync(int dayCount, AdvertiseType type)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
