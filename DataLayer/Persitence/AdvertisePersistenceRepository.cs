using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class AdvertisePersistenceRepository : GenericRepository<Advertise>, IAdvertiseRepository
    {
        private dbContext dbContext;

        public AdvertisePersistenceRepository(dbContext db) : base(db)
        {
            dbContext = db;
        }

        public async Task<Advertise> GetAsync(string city)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.Advertise.AsNoTracking().SingleOrDefault(q => q.AdvName == city);
                    return acc;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return null;
            }
        }

        public bool Check_Name(string Name, Guid guid)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.Advertise.AsNoTracking()
                        .Where(q => q.AdvName == Name && q.Guid != guid && q.Status).ToList();
                    return acc.Count == 0;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return false;
            }
        }

        public Advertise Change_Status(Guid accGuid, bool state)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var acc = context.Advertise.AsNoTracking().FirstOrDefault(q => q.Guid == accGuid);
                    acc.Status = state;
                    context.Advertise.AddOrUpdate(acc);
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
