using System;
using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer.Core
{
    public interface IAdvContentRepository : IRepository<AdvContent>
    {
        List<AdvContent> GetAllAsync(Guid guid);
    }
}
