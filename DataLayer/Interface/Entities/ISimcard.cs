using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface.Entities
{
   public interface ISimcard:IHasGuid
    {
        DateTime NextUseDivar { get; set; }
        DateTime NextUseSheypoor { get; set; }
        DateTime NextUseDivarChat { get; set; }
        long Number { get; set; }
        bool Status { get; set; }
        string Operator { get; set; }
        string UserName { get; set; }
        string OwnerName { get; set; }
    }
}
