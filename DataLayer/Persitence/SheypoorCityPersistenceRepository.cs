﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class SheypoorCityPersistenceRepository : GenericRepository<SheypoorCity>, ISheypoorCityRepository
    {
        private dbContext db;

        public SheypoorCityPersistenceRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }

        public SheypoorCity GetAsync(string city)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.SheypoorCity.AsNoTracking().FirstOrDefault(q => q.Name == city);
                    return acc;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return null;
            }
        }

        public bool Check_Name(string name)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.SheypoorCity.AsNoTracking().Where(q => q.Name == name).ToList();
                    return acc.Count == 0;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return false;
            }
        }

        public List<SheypoorCity> GetAllAsync(string search)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.SheypoorCity.AsNoTracking().Where(q => q.Name.Contains(search)).ToList();
                    return acc;
                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
                return null;
            }
        }
    }
}
