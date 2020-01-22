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
   public class SettingPersistenceRepository:GenericRepository<Setting>,ISettingRepository
   {
       private dbContext db;

       public SettingPersistenceRepository(dbContext _db):base(_db)
       {
           _db = db;
       }
   }
}
