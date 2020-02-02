using DataLayer.Enums;
using DataLayer.Models;

namespace DataLayer.Core
{
   public interface IAdvertiseLogRepository:IRepository<AdvertiseLog>
   {
       int GetAllAdvInDayFromIP(string ip, AdvertiseType type);
   }
}
