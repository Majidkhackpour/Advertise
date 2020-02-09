using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;

namespace DataLayer.Persitence
{
    public class StateOersistenceRepository : GenericRepository<States>, IStateRepository
    {
        private dbContext db;
        public StateOersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }
    }
}
