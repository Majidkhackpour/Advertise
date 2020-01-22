using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;

namespace DataLayer.Persitence
{
   public class SheypoorCityPersistenceRepository:GenericRepository<SheypoorCity>,ISheypoorCityRepository
   {
       private dbContext db;

       public SheypoorCityPersistenceRepository(dbContext _db):base(_db)
       {
           _db = db;
       }
   }
}
