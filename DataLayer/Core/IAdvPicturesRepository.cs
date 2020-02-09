using System;
using System.Collections.Generic;
using DataLayer.Models;

namespace DataLayer.Core
{
   public interface IAdvPicturesRepository:IRepository<AdvPictures>
   {
       List<AdvPictures> GetAllAsync(Guid guid);
   }
}
