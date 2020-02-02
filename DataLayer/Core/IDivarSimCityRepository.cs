using System;
using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer.Core
{
   public interface IDivarSimCityRepository:IRepository<DivarSimCity>
    {
        List<DivarSimCity> GetAllAsync(Guid simGuid);
    }
}
