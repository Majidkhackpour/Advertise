using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Interface.Entities
{
   public interface IState:IHasGuid
    {
        string Name { get; set; }
    }
}
