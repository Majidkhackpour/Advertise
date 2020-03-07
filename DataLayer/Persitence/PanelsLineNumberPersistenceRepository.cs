using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;

namespace DataLayer.Persitence
{
    public class PanelsLineNumberPersistenceRepository : GenericRepository<PanelLineNumber>, IPanelsLineNumberRepository
    {
        private dbContext dbContext;

        public PanelsLineNumberPersistenceRepository(dbContext db) : base(db)
        {
            dbContext = db;
        }
    }
}
