using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;

namespace DataLayer.Persitence
{
   public class AdvGroupPersistenceRepository:GenericRepository<AdvGroup>,IAdvGroupRepositpry
   {
       private dbContext dbContext;

       public AdvGroupPersistenceRepository(dbContext db):base(db)
       {
           dbContext = db;
       }
   }
}
