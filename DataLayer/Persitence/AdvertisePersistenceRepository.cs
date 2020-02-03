using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;

namespace DataLayer.Persitence
{
   public class AdvertisePersistenceRepository : GenericRepository<Advertise>, IAdvertiseRepository
   {
       private dbContext dbContext;

       public AdvertisePersistenceRepository(dbContext db) : base(db)
       {
           dbContext = db;
       }
    }
}
