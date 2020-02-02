using System;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Models;

namespace DataLayer.Core
{
   public interface ISimcardRepository:IRepository<Simcard>
   {
       Simcard GetAsync(AdvertiseType type);
       Simcard GetAsync(long number);
       Task<long> GetNextSimCardNumberAsync();
       bool Check_Number(long number, Guid guid);
       Simcard Change_Status(Guid accGuid, bool status);
   }
}
