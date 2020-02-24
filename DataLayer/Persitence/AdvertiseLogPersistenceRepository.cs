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
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
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
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return null;
            }
        }

        public List<int> GetAdvCountInSpecialMounthAsync(int dayCount, AdvertiseType type)
        {
            try
            {
                var lstReturn = new List<int>();
                var firstDate = DateTime.Now.AddDays(-dayCount);
                var secondDate = DateTime.Now;
                using (var contex = new dbContext())
                {
                    for (var i = firstDate; i <= secondDate; i = i.AddDays(1))
                    {
                        var counter = 0;
                        var strI = DateConvertor.M2SH(i);
                        if (type != AdvertiseType.All)
                            counter = contex.AdvertiseLog.AsNoTracking()
                                .Count(q => q.DateSabt == strI && q.AdvType == type);
                        else
                            counter = contex.AdvertiseLog.AsNoTracking()
                                .Count(q => q.DateSabt == strI);
                        lstReturn.Add(counter);
                    }
                }
                return lstReturn;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return null;
            }
        }

        public List<int> GetPublishedAdvCountInSpecialMounthAsync(int dayCount, AdvertiseType type)
        {
            try
            {
                var lstReturn = new List<int>();
                var firstDate = DateTime.Now.AddDays(-dayCount);
                var secondDate = DateTime.Now;
                using (var contex = new dbContext())
                {
                    for (var i = firstDate; i <= secondDate; i = i.AddDays(1))
                    {
                        var counter = 0;
                        var strI = DateConvertor.M2SH(i);
                        if (type != AdvertiseType.All)
                            counter = contex.AdvertiseLog.AsNoTracking()
                                .Count(q => (q.DateSabt == strI) && (q.StatusCode == StatusCode.Published) &&
                                            (q.AdvType == type));
                        else
                            counter = contex.AdvertiseLog.AsNoTracking()
                                .Count(q => (q.DateSabt == strI) && (q.StatusCode == StatusCode.Published));
                        lstReturn.Add(counter);
                    }
                }

                return lstReturn;
            }
            catch (Exception ex)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(ex);
                return null;
            }
        }
    }
}
