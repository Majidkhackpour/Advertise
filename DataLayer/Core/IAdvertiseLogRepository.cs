using System.Collections.Generic;
using DataLayer.Enums;
using DataLayer.Models;

namespace DataLayer.Core
{
   public interface IAdvertiseLogRepository:IRepository<AdvertiseLog>
   {
       int GetAllAdvInDayFromIP(string ip);
       List<AdvertiseLog> GetAll(long number);
       List<int> GetAdvCountInSpecialMounthAsync(int dayCount, AdvertiseType type);
       List<int> GetPublishedAdvCountInSpecialMounthAsync(int dayCount, AdvertiseType type);
    }
}
