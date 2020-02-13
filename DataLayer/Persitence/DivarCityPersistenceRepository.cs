using System;
using System.Collections.Generic;
using System.Linq;
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

        public bool Check_Name(string name)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.DivarCity.AsNoTracking().Where(q => q.Name == name).ToList();
                    return acc.Count == 0;
                }
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public List<DivarCity> GetAllAsync(string search)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.DivarCity.AsNoTracking().Where(q => q.Name.Contains(search)).ToList();
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
