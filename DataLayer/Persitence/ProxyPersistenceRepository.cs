using System;
using System.Data.Entity.Migrations;
using System.Linq;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;

namespace DataLayer.Persitence
{
    class ProxyPersistenceRepository : GenericRepository<Proxy>, IProxyRepository
    {
        private dbContext db;

        public ProxyPersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }

        public Proxy Change_Status(Guid accGuid, bool status)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var acc = context.Proxy.AsNoTracking().FirstOrDefault(q => q.Guid == accGuid);
                    acc.Status = status;
                    context.Proxy.AddOrUpdate(acc);
                    context.SaveChanges();
                    return acc;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
