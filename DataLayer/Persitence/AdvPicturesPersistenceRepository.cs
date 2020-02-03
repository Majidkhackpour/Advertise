using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Interface.Entities;

namespace DataLayer.Persitence
{
   public class AdvPicturesPersistenceRepository : GenericRepository<AdvPictures>, IAdvPicturesRepository
   {
       private dbContext dbContext;

       public AdvPicturesPersistenceRepository(dbContext db) : base(db)
       {
           dbContext = db;
       }
    }
}
