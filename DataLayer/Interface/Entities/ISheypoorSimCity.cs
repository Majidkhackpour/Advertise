using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface.Entities
{
   public interface ISheypoorSimCity:IHasGuid
    {
        Guid SimcardGuid { get; set; }
        Guid StateGuid { get; set; }
        Guid CityGuid { get; set; }
    }
}
