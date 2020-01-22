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
   public class AdvTokens:IAdvTokens
    {
        [Key]
        public Guid Guid { get; set; }
        [MaxLength(15)]
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public long Number { get; set; }
        public AdvertiseType Type { get; set; }
        public string Token { get; set; }
    }
}
