using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;

namespace DataLayer.Interface.Entities
{
   public interface IRegions:IHasGuid
    {
        string Name { get; set; }
        Guid CityGuid { get; set; }
        AdvertiseType Type { get; set; }
    }
}
