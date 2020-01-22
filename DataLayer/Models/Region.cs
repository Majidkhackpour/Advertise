using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Interface.Entities;

namespace DataLayer.Models
{
   public class Region:IRegions
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        [MaxLength(150)]
        public string Name { get; set; }
        public Guid CityGuid { get; set; }
        public AdvertiseType Type { get; set; }
    }
}
