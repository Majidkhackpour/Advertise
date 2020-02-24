using System;
using System.Data.Entity.Migrations;
using System.Linq;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class AdvGroupPersistenceRepository : GenericRepository<AdvGroup>, IAdvGroupRepositpry
    {
        private dbContext dbContext;

        public AdvGroupPersistenceRepository(dbContext db) : base(db)
        {
            dbContext = db;
        }

        public bool Check_Name(string Name, Guid guid)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.AdvGroups.AsNoTracking().Where(q => q.Name == Name && q.Guid != guid && q.Status)
                        .ToList();
                    if (acc == null || acc.Count == 0)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return false;
            }
        }

        public AdvGroup Change_Status(Guid accGuid, bool state)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var acc = context.AdvGroups.AsNoTracking().FirstOrDefault(q => q.Guid == accGuid);
                    acc.Status = state;
                    context.AdvGroups.AddOrUpdate(acc);
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

        public int ChildCounter(Guid guid)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.AdvGroups.AsNoTracking().Count(q => q.ParentGuid == guid && q.Status);
                    return acc;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return 0;
            }
        }
    }
}
