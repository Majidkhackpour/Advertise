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
   public class DivarCityPersistenceRepository:GenericRepository<DivarCity>,IDivarCityRepository
   {
       private dbContext db;
        public DivarCityPersistenceRepository(dbContext _db):base(_db)
        {
            _db = db;
        }

        public DivarCity GetAsync(string city)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.DivarCity.AsNoTracking().FirstOrDefault(q => q.Name == city);
                    return acc;
                }
            }
            catch (Exception exception)
            {
                return null;
            }
        }
   }
}
