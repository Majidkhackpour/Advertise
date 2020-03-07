using System;
using System.Data.Entity.Migrations;
using System.Linq;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
   public class PanelsPersistenceRepository : GenericRepository<Panels>, IPanelsRepository
    {
        private dbContext dbContext;

        public PanelsPersistenceRepository(dbContext db) : base(db)
        {
            dbContext = db;
        }

        public Panels Change_Status(Guid accGuid, bool status)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var acc = context.Panel.AsNoTracking().FirstOrDefault(q => q.Guid == accGuid);
                    acc.Status = status;
                    context.Panel.AddOrUpdate(acc);
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
