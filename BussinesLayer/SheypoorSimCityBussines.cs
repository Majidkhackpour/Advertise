using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Interface.Entities;

namespace BussinesLayer
{
   public class SheypoorSimCityBussines:ISheypoorSimCity
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public Guid SimcardGuid { get; set; }
        public Guid StateGuid { get; set; }
        public Guid CityGuid { get; set; }
    }
}
