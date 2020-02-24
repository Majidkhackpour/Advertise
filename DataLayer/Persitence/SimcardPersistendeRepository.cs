using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Enums;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class SimcardPersistendeRepository : GenericRepository<Simcard>, ISimcardRepository
    {
        private dbContext db;

        public SimcardPersistendeRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }

        public Simcard GetAsync(AdvertiseType type)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    if (type == AdvertiseType.Divar)
                    {
                        var acc = contex.Simcard.AsNoTracking().OrderBy(q => q.NextUse).FirstOrDefault();
                        return acc;
                    }

                    return null;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return null;
            }
        }

        public Simcard GetAsync(long number)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.Simcard.AsNoTracking().SingleOrDefault(q => q.Number == number);
                    return acc;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return null;
            }
        }

        public async Task<long> GetNextSimCardNumberAsync()
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.Simcard.AsNoTracking().Where(q => q.NextUse <= DateTime.Now)
                        .OrderBy(q => q.NextUse).ToList();
                    return acc.First().Number;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return 0;
            }
        }

        public bool Check_Number(long number, Guid guid)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.Simcard.AsNoTracking().Where(q => q.Number == number && q.Guid != guid && q.Status)
                        .ToList();
                    return acc.Count == 0;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return false;
            }
        }

        public Simcard Change_Status(Guid accGuid, bool status)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var acc = context.Simcard.AsNoTracking().FirstOrDefault(q => q.Guid == accGuid);
                    acc.Status = status;
                    context.Simcard.AddOrUpdate(acc);
                    context.SaveChanges();
                    return acc;
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
