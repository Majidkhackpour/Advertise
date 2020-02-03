using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Interface.Entities;

namespace DataLayer.Persitence
{
   public class AdvTitlesPersistenceRepository : GenericRepository<AdvTitles>, IAdvTitlesRepository
   {
       private dbContext dbContext;

       public AdvTitlesPersistenceRepository(dbContext db) : base(db)
       {
           dbContext = db;
       }
    }
}
