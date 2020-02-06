using System;
using System.Threading.Tasks;
using DataLayer.Models;

namespace DataLayer.Core
{
   public interface IAdvertiseRepository:IRepository<Advertise>
   {
       Task<Advertise> GetAsync(string city);
       bool Check_Name(string Name, Guid guid);
       Advertise Change_Status(Guid accGuid, bool state);
    }
}
