using System;
using System.Collections.Generic;
using DataLayer.Enums;
using DataLayer.Models;

namespace DataLayer.Core
{
    public interface IAdvCategoryRepository : IRepository<AdvCategory>
    {
        List<AdvCategory> GetAllAsync(Guid guid,AdvertiseType type);
    }
}
