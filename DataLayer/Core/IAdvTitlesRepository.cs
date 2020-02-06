using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;
using DataLayer.Models;

namespace DataLayer.Core
{
   public interface IAdvTitlesRepository:IRepository<AdvTitles>
   {
       List<AdvTitles> GetAllAsync(Guid guid);
   }
}
