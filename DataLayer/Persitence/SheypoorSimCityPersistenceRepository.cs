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
   public class SheypoorSimCityPersistenceRepository:GenericRepository<SheypoorSimCity>,ISheypoorSimCityRepository
   {
       private dbContext db;

       public SheypoorSimCityPersistenceRepository(dbContext _db):base(_db)
       {
           _db = db;
       }
   }
}
