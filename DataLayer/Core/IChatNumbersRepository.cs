using System.Collections.Generic;
using DataLayer.Enums;
using DataLayer.Models;

namespace DataLayer.Core
{
    public interface IChatNumbersRepository : IRepository<ChatNumbers>
    {
        List<ChatNumbers> GetAll(AdvertiseType type);
    }
}
