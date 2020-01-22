using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Models;

namespace DataLayer.Core
{
   public interface IAdvertiseLogRepository:IRepository<AdvertiseLog>
   {
       int GetAllAdvInDayFromIP(string ip, AdvertiseType type);
   }
}
