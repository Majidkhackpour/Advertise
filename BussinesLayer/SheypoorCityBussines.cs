using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;

namespace BussinesLayer
{
   public class SheypoorCityBussines:ISheypoorCity
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public Guid StateGuid { get; set; }
    }
}
