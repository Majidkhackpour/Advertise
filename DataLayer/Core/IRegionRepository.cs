using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Models;

namespace DataLayer.Core
{
   public interface IRegionRepository:IRepository<Region>
   {
       List<Region> GetAllAsync(Guid cityGuid, AdvertiseType type);
   }
}
