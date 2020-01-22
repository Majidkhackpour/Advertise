using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;

namespace DataLayer.Interface.Entities
{
   public interface IAdvTokens:IHasGuid
    {
        long Number { get; set; }
        AdvertiseType Type { get; set; }
        string Token { get; set; }

    }
}
