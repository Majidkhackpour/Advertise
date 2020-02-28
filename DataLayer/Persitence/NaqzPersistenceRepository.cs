using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;

namespace DataLayer.Persitence
{
    public class NaqzPersistenceRepository : GenericRepository<Naqz>, INaqzRepository
    {
        private dbContext db;

        public NaqzPersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }
    }
}
