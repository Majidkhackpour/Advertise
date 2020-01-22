﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Core
{
    public interface ISimcardAdsRepository : IRepository<SimcardAds>
    {
        List<SimcardAds> GetAllAsync(Guid simGuid);
    }
}
