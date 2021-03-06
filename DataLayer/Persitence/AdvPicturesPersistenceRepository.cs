﻿using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Models;
using ErrorHandler;

namespace DataLayer.Persitence
{
    public class AdvPicturesPersistenceRepository : GenericRepository<AdvPictures>, IAdvPicturesRepository
    {
        private dbContext dbContext;

        public AdvPicturesPersistenceRepository(dbContext db) : base(db)
        {
            dbContext = db;
        }

        public List<AdvPictures> GetAllAsync(Guid guid)
        {
            try
            {
                using (var context = new dbContext())
                {
                    var list = context.AdvPictures.AsNoTracking()
                        .Where(q => q.AdvGuid == guid)
                        .ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(e);
                return null;
            }
        }
    }
}
