using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface.Entities
{
   public interface ISimcardAds:IHasGuid
    {
        Guid SimcardGuid { get; set; }
        Guid Advertise { get; set; }
    }
}
