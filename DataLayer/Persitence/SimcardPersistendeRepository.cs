﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using DataLayer.Core;
using DataLayer.Enums;
using DataLayer.Models;

namespace DataLayer.Persitence
{
    public class SimcardPersistendeRepository : GenericRepository<Simcard>, ISimcardRepository
    {
        private dbContext db;

        public SimcardPersistendeRepository(dbContext _db) : base(_db)
        {
            _db = db;
        }

        public Simcard GetAsync(AdvertiseType type)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    if (type == AdvertiseType.Divar)
                    {
                        var acc = contex.Simcard.AsNoTracking().OrderBy(q => q.NextUseDivar).FirstOrDefault();
                        return acc;
                    }

                    return null;
                }
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public Simcard GetAsync(long number)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    var acc = contex.Simcard.AsNoTracking().SingleOrDefault(q => q.Number == number);
                    return acc;
                }
            }
            catch (Exception exception)
            {
                return null;
            }
        }

        public async Task<long> GetNextSimCardNumberAsync(AdvertiseType type)
        {
            try
            {
                using (var contex = new dbContext())
                {
                    long num = 0;
                    if (type == AdvertiseType.Divar)
                    {
                        var acc = contex.Simcard.AsNoTracking().Where(q => q.NextUseDivar <= DateTime.Now)
                            .OrderBy(q => q.NextUseDivar).ToList();
                        return acc.First().Number;
                    }
                    else if (type == AdvertiseType.DivarChat)
                    {
                        var acc = contex.Simcard.AsNoTracking().Where(q => q.NextUseDivarChat <= DateTime.Now)
                            .OrderBy(q => q.NextUseDivarChat).ToList();
                        return acc.First().Number;
                    }

                    return 0;


                }
            }
            catch (Exception exception)
            {
                return 0;
            }
        }
    }
}
