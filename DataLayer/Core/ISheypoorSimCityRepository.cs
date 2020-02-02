using System;
using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer.Core
{
    public interface ISheypoorSimCityRepository : IRepository<SheypoorSimCity>
    {
        List<SheypoorSimCity> GetAllAsync(Guid simGuid);
    }
}
