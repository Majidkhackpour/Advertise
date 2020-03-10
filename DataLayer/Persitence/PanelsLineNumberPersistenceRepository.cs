using System;
using System.Data.Entity.Migrations;
using System.Linq;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class PanelsLineNumberPersistenceRepository : GenericRepository<PanelLineNumber>, IPanelsLineNumberRepository
    {
        private dbContext dbContext;

        public PanelsLineNumberPersistenceRepository(dbContext db) : base(db)
        {
            dbContext = db;
        }

        public PanelLineNumber Change_Status(Guid accGuid, bool status)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var acc = context.PanelLineNumber.AsNoTracking().FirstOrDefault(q => q.Guid == accGuid);
                    acc.Status = status;
                    context.PanelLineNumber.AddOrUpdate(acc);
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
